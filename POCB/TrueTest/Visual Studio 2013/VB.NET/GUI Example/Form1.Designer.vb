<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim RoiCircle1 As RadiantCommon.ROICircle = New RadiantCommon.ROICircle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.EditButton = New System.Windows.Forms.Button()
        Me.RunButton = New System.Windows.Forms.Button()
        Me.RiBitmapCtl1 = New RadiantCommon.RiBitmapCtl()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.EditButton)
        Me.Panel1.Controls.Add(Me.RunButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(859, 52)
        Me.Panel1.TabIndex = 0
        '
        'EditButton
        '
        Me.EditButton.Location = New System.Drawing.Point(110, 6)
        Me.EditButton.Name = "EditButton"
        Me.EditButton.Size = New System.Drawing.Size(92, 40)
        Me.EditButton.TabIndex = 1
        Me.EditButton.Text = "Edit..."
        Me.EditButton.UseVisualStyleBackColor = True
        '
        'RunButton
        '
        Me.RunButton.Location = New System.Drawing.Point(12, 6)
        Me.RunButton.Name = "RunButton"
        Me.RunButton.Size = New System.Drawing.Size(92, 40)
        Me.RunButton.TabIndex = 0
        Me.RunButton.Text = "Run"
        Me.RunButton.UseVisualStyleBackColor = True
        '
        'RiBitmapCtl1
        '
        Me.RiBitmapCtl1.AllowScrollZoom = True
        Me.RiBitmapCtl1.BitmapImageScaleMode = RadiantCommon.RiBitmapCtl.ImageScaleMode.Fit
        Me.RiBitmapCtl1.BlobDrawShape = RadiantCommon.RiBitmapCtl.BlobDrawShapeEnum.Contours
        Me.RiBitmapCtl1.ContourLevelList = CType(resources.GetObject("RiBitmapCtl1.ContourLevelList"), System.Collections.Generic.List(Of Byte))
        Me.RiBitmapCtl1.CrossHairsDisplay = False
        Me.RiBitmapCtl1.DBSettings = Nothing
        Me.RiBitmapCtl1.DefectsSelectable = True
        Me.RiBitmapCtl1.DetectorSizeInputDisabled = False
        Me.RiBitmapCtl1.DisplayLegend = True
        Me.RiBitmapCtl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RiBitmapCtl1.DoubleBuffering = True
        Me.RiBitmapCtl1.DragMode = RadiantCommon.RiBitmapDragModeType.Normal
        Me.RiBitmapCtl1.EnhanceColorLevel = 0
        Me.RiBitmapCtl1.FalseColorDisplay = True
        Me.RiBitmapCtl1.FalseColorOption = RadiantCommon.MeasurementBase.FalseColorOptions.ZeroToMax
        Me.RiBitmapCtl1.GammaLevel = 0.7!
        Me.RiBitmapCtl1.GridDisplay = False
        Me.RiBitmapCtl1.HelpButtonDisplay = False
        Me.RiBitmapCtl1.HideCursorBox = False
        Me.RiBitmapCtl1.ImageInitialized = False
        Me.RiBitmapCtl1.InterpolateImage = False
        Me.RiBitmapCtl1.LABReferenceColor = CType(resources.GetObject("RiBitmapCtl1.LABReferenceColor"), RadiantCommon.CIEColor)
        Me.RiBitmapCtl1.Location = New System.Drawing.Point(0, 52)
        Me.RiBitmapCtl1.LocationDigits = -1
        Me.RiBitmapCtl1.LogScaleDisplay = False
        Me.RiBitmapCtl1.Measurement = Nothing
        Me.RiBitmapCtl1.Name = "RiBitmapCtl1"
        Me.RiBitmapCtl1.OptionsButtonDisplay = True
        Me.RiBitmapCtl1.Pen = Nothing
        Me.RiBitmapCtl1.PrintButtonDisplay = False
        Me.RiBitmapCtl1.QuantityDisplayed = RadiantCommon.QuantityEnum.Luminance
        Me.RiBitmapCtl1.RegionOfInterest = Nothing
        Me.RiBitmapCtl1.RoadDisplay = False
        Me.RiBitmapCtl1.ROIArray = New RadiantCommon.RegionOfInterest(-1) {}
        Me.RiBitmapCtl1.ROISelectedIndex = -1
        Me.RiBitmapCtl1.ROISelectionEnabled = False
        Me.RiBitmapCtl1.SaveButtonDisplay = False
        Me.RiBitmapCtl1.ShowBlobInfo = True
        Me.RiBitmapCtl1.ShowContourLines = False
        Me.RiBitmapCtl1.ShowCrossHairsButton = True
        Me.RiBitmapCtl1.ShowCursor = True
        Me.RiBitmapCtl1.ShowDistanceUnitLabels = True
        Me.RiBitmapCtl1.ShowOptions = False
        Me.RiBitmapCtl1.Size = New System.Drawing.Size(859, 526)
        Me.RiBitmapCtl1.TabIndex = 1
        Me.RiBitmapCtl1.ToolbarDisplayed = True
        Me.RiBitmapCtl1.ToolbarEnabled = True
        Me.RiBitmapCtl1.TrueColorString = "True Color"
        Me.RiBitmapCtl1.ViewAnglesEnabled = True
        Me.RiBitmapCtl1.ViewRect = New System.Drawing.Rectangle(0, 0, 0, 0)
        RoiCircle1.BackgroundAlpha = 0
        RoiCircle1.BackgroundBrush = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        RoiCircle1.Diameter = 0.0!
        RoiCircle1.DistanceScale = RadiantCommon.DistanceScaleType.Physical
        RoiCircle1.DistanceUnit = RadiantCommon.DistanceUnitType.Millimeters
        RoiCircle1.DrawCircleCrosshairs = False
        RoiCircle1.DrawFill = True
        RoiCircle1.Eval = Nothing
        RoiCircle1.FOI = Nothing
        RoiCircle1.FOIsetID = 0
        RoiCircle1.Guid = New System.Guid("00000000-0000-0000-0000-000000000000")
        RoiCircle1.Info = Nothing
        RoiCircle1.LabelList = Nothing
        RoiCircle1.Location = CType(resources.GetObject("RoiCircle1.Location"), System.Drawing.PointF)
        RoiCircle1.LocationDistanceScale = RadiantCommon.DistanceScaleType.Pixels
        RoiCircle1.LocationDistanceUnit = RadiantCommon.DistanceUnitType.Millimeters
        RoiCircle1.mFOI = Nothing
        RoiCircle1.MinMaxLocationMax = Nothing
        RoiCircle1.MinMaxLocationMin = Nothing
        RoiCircle1.PenColor = System.Drawing.Color.Black
        RoiCircle1.PenDashOffset = 0.0!
        RoiCircle1.PenDashPattern = New Single() {1.0!, 1.0!}
        RoiCircle1.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        RoiCircle1.PenWidth = 1.0!
        RoiCircle1.Radius = 0.0!
        RoiCircle1.Selected = True
        RoiCircle1.SequenceIndex = -1
        RoiCircle1.Transformation = Nothing
        Me.RiBitmapCtl1.VirtualDetectorRegion = RoiCircle1
        Me.RiBitmapCtl1.VirtualDetectorShow = True
        Me.RiBitmapCtl1.WhiteBackgroundDisplay = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 578)
        Me.Controls.Add(Me.RiBitmapCtl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RunButton As System.Windows.Forms.Button
    Friend WithEvents RiBitmapCtl1 As RadiantCommon.RiBitmapCtl
    Friend WithEvents EditButton As System.Windows.Forms.Button

End Class
