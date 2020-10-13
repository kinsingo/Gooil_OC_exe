Public Class PMEngineExampleForm
    Dim mPMObj As PMEngine
    Dim MeasSetup As MeasurementSetup
    Dim Meas As PMMeasurementF

    Private Sub PMEngineCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PMEngineCheckBox.CheckedChanged
        If Me.PMEngineCheckBox.Checked Then
            'create a new pmengine obj
            mPMObj = New PMEngine

            'set the cal database
            mPMObj.SetCalibrationDatabase("C:\Radiant Vision Systems Data\ProMetric\CalibrationData\PM Calibration Demo Camera.calx")

            'start the camera
            mPMObj.InitializeCamera()

            'set the measurement database
            mPMObj.MeasurementDatabaseName = "C:\Radiant Vision Systems Data\ProMetric\UserData\Sample.pmxm"

            'enable miscellaneous controls
            Me.MeasSetupComboBox.Enabled = True
            Me.AdjustExposureButton.Enabled = True
            Me.TakeMeasurementButton.Enabled = True

            'get the list of measurement setups and put into the combobox
            Dim MSList(-1) As ListItem
            mPMObj.GetMeasurementSetupList(MSList)
            CommonFunctions.LoadComboBoxNoSelect(Me.MeasSetupComboBox, MSList)
            Me.MeasSetupComboBox.SelectedIndex = 0
        Else
            'set the PMEngine object to nothing
            mPMObj = Nothing

            'disable controls on the form to prevent errors
            Me.MeasSetupComboBox.Enabled = False
            Me.AdjustExposureButton.Enabled = False
            Me.TakeMeasurementButton.Enabled = False
            Me.GetValueButton.Enabled = False
        End If
    End Sub

    Private Sub MeasSetupComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MeasSetupComboBox.SelectedIndexChanged
        'get the measurementsetup ID from the combobox
        Dim MeasSetupID As Integer = CommonFunctions.GetListItemIDfromComboBox(Me.MeasSetupComboBox)

        'read the measurementsetup from the database
        MeasSetup = mPMObj.ReadMeasurementSetupfromDatabase(MeasSetupID)

        'additionally alter the measurementsetup if needed
        MeasSetup.SubFrameRegion = New System.Drawing.Rectangle(0, 0, 0, 0) 'maximizes subframe
    End Sub


    Private Sub AdjustExposureButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdjustExposureButton.Click
        'Shows the adjust exposure form (exposure times are saved to exposure property)
        mPMObj.ShowAdjustExposureForm(MeasSetup)
    End Sub

    Private Sub TakeMeasurementButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeMeasurementButton.Click
        'Take a measurement
        Meas = mPMObj.TakeMeasurementF(MeasSetup, "Descript", 0, 0, "", "")

        'Enable the get value button
        Me.GetValueButton.Enabled = True
    End Sub

    Private Sub GetValueButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetValueButton.Click
        'Create a circular detector for the size specified
        Dim ROICircleObj As New ROICircle(DistanceUnitType.Millimeters, CSng(Me.DetSizeTextBox.Text))

        'Set other detector properties, such as location
        Dim LocationX As Single = CSng(Me.XLocTextBox.Text)
        Dim LocationY As Single = CSng(Me.YLocTextBox.Text)
        ROICircleObj.LocationDistanceScale = DistanceScaleType.Relative
        ROICircleObj.Center(Meas) = New PointF(LocationX, LocationY)

        'create a new CIEColor object
        Dim CIEColorObj As CIEColor
        CIEColorObj = ROICircleObj.GetColor(Meas)

        'display the luminance value
        Me.ValueTextBox.Text = CStr(CIEColorObj.Lv)

    End Sub

    Private Sub PMEngineExampleForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If mPMObj IsNot Nothing Then mPMObj.Shutdown()
    End Sub
End Class
