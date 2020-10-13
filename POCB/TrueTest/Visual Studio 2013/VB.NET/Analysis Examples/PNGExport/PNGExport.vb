Imports System.Xml.Serialization
Imports RadiantCommon
Imports TrueTestEngine
Imports TrueTestEngine.TrueTest
Imports System.ComponentModel
Imports System.Drawing

'The class name usually matches the test name. This class will become the test
<Serializable()> _
Public Class PNGExport
    'DisplayAnalysisBase contains a lot of good features for tests and we recomend inheriting from it for display testing
    'This includes items like RADA and remove Morie
    Inherits DisplayAnalysisBase

    Private Const PNGExport As String = "PNGExport"

    'This is a must override of DisplayAnalysisBase, so that it can uniquly identify this test
    <XmlIgnore(), Browsable(False)> _
    Public Overrides ReadOnly Property Name As String
        Get
            Return PNGExport
        End Get
    End Property

    'Also a must override, this name can be changed by the user inside of the TrueTest GUI sequence control
    Public Overrides Property UserName As String = PNGExport

    'This property belongs to this test only, add as many properties as you need to the test and they can be configured in real time inside the GUI sequence control
    Public Property FilePath As String = "C:\Radiant Vision Systems Data\TrueTest\UserData\Test.png"

    'This is where the bluk of the test execution resides, in this must override sub of DisplayAnalysisBase.  Put all your test execution code here
    Protected Overrides Sub Execute_()

        'Store the current measurement in the object "m"
        Dim m As MeasurementF = MyBase.PatternMeasurementList(0).CurrentMeasurement

        Dim b As Bitmap = m.CreateBitmapTrueColor(MeasurementBase.TransformMethod.sRGB_D4500, {1, 1, 1}, False, 2.2, False, Nothing)

        b.Save(FilePath, Drawing.Imaging.ImageFormat.Png)

    End Sub

End Class
