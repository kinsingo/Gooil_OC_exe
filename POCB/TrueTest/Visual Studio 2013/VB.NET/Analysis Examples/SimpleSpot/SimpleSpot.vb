Imports System.Xml.Serialization
Imports RadiantCommon
Imports RadiantCommonCS
Imports System.Drawing
Imports TrueTestEngine
Imports TrueTestEngine.TrueTest
Imports System.ComponentModel
Imports RadiantCommonCS.ImageProcessUnSafe

<Serializable()> _
Public Class SimpleSpot
    Inherits DisplayAnalysisBase

    <XmlIgnore(), BrowsableAttribute(False)> _
    Public Overrides ReadOnly Property Name() As String
        Get
            Return "Simple Spot"
        End Get
    End Property

    Public Overrides Property UserName() As String = "Simple Spot"

#Region "Blob Analysis Properties"
    <CategoryAttribute("Blob Analysis"), DescriptionAttribute("The graphical shape used to draw the blobs on the image.")> _
    Public Property BlobDrawShape As RiBitmapCtl.BlobDrawShapeEnum = RiBitmapCtl.BlobDrawShapeEnum.Blobs

    <CategoryAttribute("Blob Analysis"), DescriptionAttribute("The observer viewing-distance of the display in units of the display height (H).")> _
    Public Property ViewingDistance As Single = 6

    <CategoryAttribute("Blob Analysis"), DescriptionAttribute("The spatial scale of the border attenuation function. Typical value = 0.25")> _
    Public Property BorderAttenuationScale As Single = 0

    <CategoryAttribute("Blob Analysis"), DescriptionAttribute("T/F value indicating whether to show intermediate images during processing.")> _
    Public Property ShowProcessing As Boolean = True

    <CategoryAttribute("Blob Analysis"), DescriptionAttribute("The type of measurement image to display.")> _
    Public Property DisplayImage As DisplayImageEnum = DisplayImageEnum.LocalContrast

#End Region

#Region "Simple Spot Properties"

    <CategoryAttribute("Simple Spot"), DescriptionAttribute("Local contrast threshold (%) for bright mura.")> _
    Public Property BrightContrastThreshold As Single = 3 '%

    <XmlIgnore(), CategoryAttribute("Simple Spot"), BrowsableAttribute(True), _
    DescriptionAttribute("The color used to draw blobs in the corner regions.")> _
    Public Property DrawColorCorner As Color = Color.White
    <BrowsableAttribute(False), XmlElement("DrawColorCorner")> _
    Public Property XmlDrawColorCorner() As String
        Get
            Return Serialize(DrawColorCorner)
        End Get
        Set(ByVal value As String)
            DrawColorCorner = CType(Deserialize(value, GetType(Drawing.Color)), Color)
        End Set
    End Property

#End Region

#Region "Blob Size Limits"
    <CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The minimum diameter (millimeters) of blobs to be detected.")> _
    Public Property MinBlobSizeMM As Single = 0

    <CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The maximum diameter (millimeters) of blobs to be detected.")> _
    Public Property MaxBlobSizeMM As Single = 0

    <CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The minimum number of CCD pixels in a blob.")> _
    Public Property MinCCDPixels As Integer = 0

    <CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The maximum number of CCD pixels in a blob.")> _
    Public Property MaxCCDPixels As Integer = 0

    <CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The shape of the blobs which will be counted.")> _
    Public Property BlobShape As ROIDefect.DefectShapeType = ROIDefect.DefectShapeType.All

    <CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The aspect ratio threshold value for blobs to be counted.")> _
    Public Property AspectRatioThreshold As Single = 0

#End Region

    <XmlIgnore()> Private Property mGlobalReference As CIEColor = Nothing

    Private Property MuraAreaMM As Single

    Protected Overrides Sub Execute_()
        Dim mf As MeasurementF = MyBase.PatternMeasurementList(0).CurrentMeasurement

        If mf.Content = MeasurementBase.MeasurementContentType.TriStimulus Then
            mf = ImageProcess.MakePhotopic(mf)
            MeasurementChanged_(mf)
        End If

        Dim ReferenceConvolutionType As ImageProcess.ConvolutionTypeEnum = ImageProcess.ConvolutionTypeEnum.PadWithZeros

        Dim localContrast As MeasurementF = MyBase.MeasureLocalContrast(mf, ViewingDistance, BorderAttenuationScale, ShowProcessing, ReferenceConvolutionType)
        MeasurementChanged_(localContrast, False, MeasurementBase.FalseColorOptions.MinToMax)

        ResultList.AddRange(AnalyzeLocalContrastImage(localContrast, BlobTypeEnum.Both, Nothing))

        Dim BrightLocalContrast As MeasurementF = ImageProcess.RemoveNegatives(localContrast)
        MeasurementChanged_(BrightLocalContrast)

        Dim T As New RadiantCommon.ThresholdF(BrightContrastThreshold, ImageProcess.ThresholdMethod.AbsoluteValue, ImageProcess.ThresholdType.Floor, 0, True)
        BrightLocalContrast = T.Threshold(BrightLocalContrast, RadiantCommonCS.TristimType.All, Nothing)
        MeasurementChanged_(BrightLocalContrast)

        Dim BlobList As List(Of DefectBase) = FindBlobs(BrightLocalContrast, mf, BrightContrastThreshold + 0.00001F, DrawColorCorner, ImageProcess.BlobType.Bright, MinBlobSizeMM, MaxBlobSizeMM, MinCCDPixels, MaxCCDPixels, BlobShape, AspectRatioThreshold, False)

        Select Case DisplayImage
            Case DisplayImageEnum.Blobs
                Dim Blobs As MeasurementF = ImageProcess.MakeBlobMeasurement(BlobList, Nothing, localContrast)
                If Blobs IsNot Nothing Then
                    OnMeasurementChanged(Blobs)
                    MyBase.PatternMeasurementList(0).CurrentMeasurement = Blobs
                End If
            Case DisplayImageEnum.LocalContrast
                OnShowMeasurement(localContrast, False, MeasurementBase.FalseColorOptions.MinToMax)
                MyBase.PatternMeasurementList(0).CurrentMeasurement = localContrast
            Case DisplayImageEnum.Measurement
                OnMeasurementChanged(mf)
                MyBase.PatternMeasurementList(0).CurrentMeasurement = mf
        End Select

        OnDefectListChanged(BlobList, BlobDrawShape)

        Dim index As Integer = 1
        For Each d As RadiantCommon.DefectBase In BlobList
            ResultList.Add(New Result(Me, "Spot " & index.ToString & " Area", d.NumPixels * localContrast.ScaleFactorCol * localContrast.ScaleFactorRow * 1000 * 1000, "mm^2"))
            index += 1
        Next

        MyBase.PatternMeasurementList(0).DefectList = BlobList
    End Sub

    Private Sub MeasurementChanged_(ByVal m As MeasurementBase, ByVal FalseColorDisplay As Boolean, ByVal FalseColorOption As MeasurementBase.FalseColorOptions, Optional ByVal MinValue As Single = 0, Optional ByVal MaxValue As Single = 0)
        If ShowProcessing = False Then Return
        OnShowMeasurement(m, FalseColorDisplay, FalseColorOption, MinValue, MaxValue)
    End Sub
    Private Sub MeasurementChanged_(ByVal m As MeasurementBase)
        If ShowProcessing = False Then Return
        OnMeasurementChanged(m)
    End Sub

End Class

'**BEGIN PSEUDO CODE***
'The measurement is taken from the repository.
'It is converted to Photopic only with a call to the ImageProcess library.
'A Local Contrast measurement is generated with a call to a member of DisplayAnalysisBase
'The measurement is updated on the screen
'A call to a member of DisplayAnalysisBase is used to analyze the image and provide RMS contrast, AvgAbsContrast, MinContrast & Max Contrast to the results list
'Negative Contrast values are removed with a call to ImageProcess
'Screen Update
'An absolute threshold is created and applied to the local contrast
'Screen update
'A call to “FindBlobs”  produces a list of defects detected in the processed image
'The screen image is updated based on the found defects and the users choice for what type of image to display
'All the blobs that were found are written out to the analysis list with their areas in mm^2
'***END OF LINE***
