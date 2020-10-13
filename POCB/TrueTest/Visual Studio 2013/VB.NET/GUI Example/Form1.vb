Imports TrueTestEngine
Imports RadiantCommon
Imports System.Windows.Forms
Imports System.Drawing

Public Class Form1

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Initialize TrueTest
        TrueTest.Initialize(My.Settings.CalibrationDatabase, My.Settings.SequenceXMLPathFileName, My.Settings.MeasurementDatabasePathFileName, Me)
        'The strings are passed in by reference, so if the user browses to select a new file during initialization, the values will be automatically updated
        'Therefore we still need to save My.Settings.
        'The last argument is the main application form, which in most VB.NET cases is Me.  If using a language other than .NET to create GUIs, you can set this to Nothing (null).
        My.Settings.Save()

        'add some event handlers to handle events coming from TrueTest
        AddTrueTestEventHandlers()

        'Hard-code some specific settings in TrueTest for this Demo application.
        TrueTest.OperatingMode = TrueTest.OperatingModeEnum.UseDatabaseUserSelect 'pulls measurements from a database instead of measuring new with the camera
        TrueTest.AppSettings.SaveAnalysisResultsToDatabase = True 'makes sure to save all results into the measurement database
        TrueTest.SaveMeasurementsToDatabase = True 'if the user wants to make a camera measurement, make sure it gets saved to the database

        Me.Text = TrueTest.Sequence.Name
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        TrueTest.Shutdown()
    End Sub

    Private Sub AddTrueTestEventHandlers()
        AddHandler TrueTest._MeasurementComplete, AddressOf MeasurementComplete
        AddHandler TrueTest._MeasurementChanged, AddressOf MeasurementChanged
        AddHandler TrueTest._DefectListChanged, AddressOf DefectListChanged
        AddHandler TrueTest._ROIArrayChanged, AddressOf ROIArrayChanged
        AddHandler TrueTest._AnalysisComplete, AddressOf AnalysisComplete
        AddHandler TrueTest.SequenceComplete, AddressOf SequenceComplete
    End Sub

    Private Sub RunButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunButton.Click
        EnableButtons(False) ' disables buttons on the main form

        'clear and reset the bitmap control
        RiBitmapCtl1.ROIArray = Nothing
        RiBitmapCtl1.ClearDefects()
        RiBitmapCtl1.Measurement = Nothing
        RiBitmapCtl1.SetBackColor(Color.Black)

        Application.DoEvents() 'forces a redraw of the bitmap control

        'ask for the serial number.  If the user quits or cancels, exit without starting the sequence
        If TrueTest.ShowSerialNumberDialog() <> System.Windows.Forms.DialogResult.OK Then
            EnableButtons(True)
            Return
        End If

        'start running the sequence
        TrueTest.SequenceRunAll()
    End Sub

    Private Sub EnableButtons(ByVal Enabled As Boolean)
        If InvokeRequired Then BeginInvoke(New Action(Sub() EnableButtons(Enabled))) : Exit Sub
        RunButton.Enabled = Enabled
        EditButton.Enabled = Enabled
        If Enabled Then RunButton.Focus()
    End Sub

    Private Sub EditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditButton.Click
        TrueTest.ShowEditSequenceDialog() 'shows a form to edit the sequence
    End Sub

#Region "TrueTest Event Handling"

    Public Sub MeasurementComplete(ByVal sender As Object, ByVal e As String)
        If TrueTest.AppSettings.DisplayCameraMeasurements = False Then
            Return
        End If

        Dim obj As Object = ObjectRepository.GetItem(e, ObjectRepository.ReadWriteEnum.ReadOnlyNoWait)
        If obj IsNot Nothing Then
            Dim ea As TrueTestEngine.MeasurementCompleteEventArgs = CType(obj, TrueTestEngine.MeasurementCompleteEventArgs)
            UpdateBitmap(ea.Measurement)
        End If
    End Sub

    Private Sub MeasurementChanged(ByVal sender As Object, ByVal e As String)
        If TrueTest.AppSettings.DisplayUpdatedMeasurements = False Then
            Return
        End If

        Dim obj As Object = ObjectRepository.GetItem(e, ObjectRepository.ReadWriteEnum.ReadOnlyNoWait)
        If obj IsNot Nothing Then
            Dim ea As TrueTestEngine.MeasurementChangedEventArgs = CType(obj, TrueTestEngine.MeasurementChangedEventArgs)
            UpdateBitmap(ea.Measurement)
        End If
    End Sub

    Private Sub UpdateBitmap(ByVal m As MeasurementBase)
        If InvokeRequired Then BeginInvoke(New Action(Sub() UpdateBitmap(m))) : Exit Sub

        Me.RiBitmapCtl1.Measurement = m
    End Sub

    Private Sub DefectListChanged(ByVal sender As Object, ByVal e As String)
        If TrueTest.AppSettings.DisplayDefectList = False Then
            Return
        End If

        Dim obj As Object = ObjectRepository.GetItem(e, ObjectRepository.ReadWriteEnum.ReadOnlyNoWait)
        If obj IsNot Nothing Then
            Dim ea As TrueTestEngine.DefectListChangedEventArgs = CType(obj, TrueTestEngine.DefectListChangedEventArgs)
            SetDefectList(ea.DefectList)
        End If
    End Sub

    Friend Sub SetDefectList(ByVal DefectList As List(Of DefectBase))
        Me.RiBitmapCtl1.SetDefectList(DefectList)
        Application.DoEvents()
    End Sub

    Private Sub ROIArrayChanged(ByVal sender As Object, ByVal e As String)
        Dim obj As Object = ObjectRepository.GetItem(e, ObjectRepository.ReadWriteEnum.ReadOnlyNoWait)
        If obj IsNot Nothing Then
            Dim ea As TrueTestEngine.ROIArrayChangedEventArgs = CType(obj, TrueTestEngine.ROIArrayChangedEventArgs)
            SetROIArray(ea.ROIArray)
        End If
    End Sub

    Friend Sub SetROIArray(ByVal ROIArray() As RegionOfInterest)
        Me.RiBitmapCtl1.ROIArray = ROIArray
        Application.DoEvents()
    End Sub

    Private Sub SequenceComplete(ByVal sender As Object, ByVal e As TrueTestEngine.SequenceCompleteEventsArgs)
        Dim s As String = "Failed Tests= " & e.NumFailedTests.ToString
        MessageBox.Show("Sequence Complete: " & e.SequenceName & vbCrLf & s)
        EnableButtons(True)

        If e.PassFail = TrueTest.AnalysisResultEnum.Fail Then
            '*FAIL*
        End If
    End Sub

    Private Sub AnalysisComplete(ByVal sender As Object, ByVal e As String)
        Dim obj As Object = ObjectRepository.GetItem(e, ObjectRepository.ReadWriteEnum.ReadOnlyNoWait)
        If obj IsNot Nothing Then
            Dim ea As TrueTestEngine.AnalysisCompleteEventArgs = CType(obj, TrueTestEngine.AnalysisCompleteEventArgs)

            Dim s As String = ea.Results(0).Name & "= " & ea.Results(0).Value.ToString
            MessageBox.Show("Analysis Complete: " & ea.AnalysisUserName & vbCrLf & s & vbCrLf & ea.PassFail.ToString)

            If ea.PassFail = TrueTest.AnalysisResultEnum.Fail Then
                TrueTest.SequenceStop() 'cancel the rest of the sequence
            End If
        End If
    End Sub
#End Region
End Class