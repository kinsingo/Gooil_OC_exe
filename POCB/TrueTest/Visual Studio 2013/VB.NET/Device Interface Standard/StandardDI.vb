Imports System.IO
Imports System
Imports System.ComponentModel
Imports System.IO.Ports
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports TrueTestPatternGenerator
Imports TrueTestEngine

<Serializable()>
Public Class StandardDI
    Inherits PatternGeneratorBase
    Implements IDisposable
    Private SequenceIsRunning As Boolean = False
    Private CommandDelivered As Boolean = False
    Private NumberOfSequenceExposures As Integer = 0
    Private ExposuresCompleted As Integer = 0
    Private AckRecieved As Boolean = False
    Private RequestedPattern As Integer

    Enum SerialCommand
        SerialNumber
        SerialAcknowledge
        DeviceReady
        ShowPattern
        PatternAcknowledge
        Unload
        UnloadAcknowledge
        Result
        ResultAcknowledge
    End Enum

    Public Enum TerminatorEnum
        None = 0
        Cr = 1
        Lf = 2
        CrLf = 3
        LfCr = 4
    End Enum

#Region "Fixture Serial Port Parameters"
    <XmlIgnore(), NonSerialized(), Browsable(False)>
    Friend WithEvents SerialPort As SerialPort = Nothing

    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("The serial port name used to communicate with the fixture PLC.")>
    Public Property PortName As String = "COM1"

    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("Parity of communication protocol.")>
    Public Property Parity As IO.Ports.Parity = IO.Ports.Parity.None

    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("Number of databits in communication protocol.")>
    Public Property DataBits As Integer = 8

    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("Stop Bits in communication protocol.")>
    Public Property StopBits As StopBits = StopBits.One

    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("The Baud Rate of communication protocol.")>
    Public Property BaudRate As Integer = 9600

    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("The number of miliseconds to wait before considering communcation dropped.")>
    Public Property WaitTime As Integer = 3000

    Private mTerminator As String = vbCrLf
    <CategoryAttribute("Fixture Serial Port Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("The terminating character in communication protocol.")>
    Public Property Terminator As TerminatorEnum
        Get
            Select Case mTerminator
                Case ""
                    Return TerminatorEnum.None
                Case vbCr
                    Return TerminatorEnum.Cr
                Case vbLf
                    Return TerminatorEnum.Lf
                Case vbCrLf
                    Return TerminatorEnum.CrLf
                Case vbLf & vbCr
                    Return TerminatorEnum.LfCr
                Case Else
                    Return TerminatorEnum.CrLf
            End Select
        End Get
        Set(ByVal term As TerminatorEnum)
            Select Case term
                Case TerminatorEnum.None
                    mTerminator = ""
                Case TerminatorEnum.Cr
                    mTerminator = vbCr
                Case TerminatorEnum.Lf
                    mTerminator = vbLf
                Case TerminatorEnum.CrLf
                    mTerminator = vbCrLf
                Case TerminatorEnum.LfCr
                    mTerminator = vbLf & vbCr
                Case Else
                    mTerminator = vbCrLf
            End Select
        End Set
    End Property

    Friend Initialized As Boolean = False

#End Region

#Region "Log File Communication"

    <CategoryAttribute("Log File Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("The file path of the log file")>
    Public Property LogFilePath As String = "C:\Radiant Vision Systems Data\TrueTest\AppData"

    <CategoryAttribute("Log File Parameters"), BrowsableAttribute(True),
    DescriptionAttribute("Determines if a cummunications log will be written>")>
    Public Property WriteToCommunicationsLog As Boolean = True
#End Region

#Region "Initialization and ShutDown"
    ''' <summary>
    ''' Overwrites Initialize() in PatternGeneraorBase.vb so that the serial port will be initialized along with the PG.
    ''' </summary>
    Protected Overrides Function Initialize() As Boolean
        Initialized = False

        AddEventHandlers()

        Return InitializeSerialPort()
    End Function

    Friend Sub AddEventHandlers()
        AddHandler TrueTest.ExposureComplete, AddressOf ExposureComplete
        AddHandler TrueTest.SequenceComplete, AddressOf SequenceComplete
        AddHandler TrueTest.SequenceRunAllStarted, AddressOf PrepareForSequenceRunALL
    End Sub

    Friend Sub RemoveHandlers()
        RemoveHandler TrueTest.ExposureComplete, AddressOf ExposureComplete
        RemoveHandler TrueTest.SequenceComplete, AddressOf SequenceComplete
        RemoveHandler TrueTest.SequenceRunAllStarted, AddressOf PrepareForSequenceRunALL
    End Sub

    ''' <summary>
    ''' Initializes sereial port.
    ''' </summary>
    Private Function InitializeSerialPort() As Boolean
        Try
            SerialPort = New SerialPort(PortName, BaudRate, Parity, DataBits, StopBits)
            SerialPort.ReceivedBytesThreshold = 1
            SerialPort.DiscardNull = True
            SerialPort.NewLine = mTerminator
            SerialPort.WriteTimeout = WaitTime
            SerialPort.ReadTimeout = WaitTime

            SerialPort.Open()

            Initialized = True
            Me.IsInitialized = True 'Initialization for pattern generator
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Fixture Serial Port Error")
            SerialPort = Nothing
        End Try

        Return Initialized
    End Function

    Protected Overrides Function IsInitializationRequired(ByVal NewPGObject As TrueTestPatternGenerator.PatternGeneratorBase) As Boolean
        If NewPGObject Is Nothing Then Return False
        Return True 'reinitialize always
    End Function

    ''' <summary>
    ''' Overwrites ShutDown() in PatternGeneraorBase.vb so that the serial port will be closed along with the PG.
    ''' </summary>
    Protected Overrides Function ShutDown() As Boolean

        If Initialized = False Then Return True
        Close()
        RemoveHandlers()
        Me.IsInitialized = False
        Return True

    End Function

    ''' <summary>
    ''' Closes serial port.
    ''' </summary>
    Private Sub Close()
        Try
            If SerialPort IsNot Nothing Then
                If SerialPort.IsOpen Then
                    SerialPort.Close()
                    Initialized = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Fixture Serial Port Error")
        End Try

    End Sub
#End Region

#Region "Serial Port Send/Receive Functions"

    ''' <summary>
    ''' Listens for commands from serial port and calls functions depending upon command received.
    ''' </summary>
    Private Sub SerialPort_DataReceived(sender As Object, e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        Try
            '20ms Sleep introduced in order to allow the buffer to completely fill.
            Threading.Thread.Sleep(20)

            Dim RecievedString As String = SerialPort.ReadLine()

            Dim ReceivedPacket As New PacketSerial(RecievedString)

            Select Case ReceivedPacket.Request
                Case SerialCommand.SerialNumber
                    Dim SerialNumber As String = ReceivedPacket.Data(0)
                    WriteToLog("Received: SN," & SerialNumber)
                    TrueTest.SerialNumber = SerialNumber
                    SerialPort.WriteLine("SA," & SerialNumber)
                    Exit Sub
                Case SerialCommand.DeviceReady
                    WriteToLog("Received: RD")
                    TrueTest.MDIForm.BeginInvoke(Sub() TrueTest.SequenceRunAll())  'Runs the sequence
                    Exit Sub
                Case SerialCommand.PatternAcknowledge
                    WriteToLog("Received: PA," + ReceivedPacket.Data(0))
                    AckRecieved = True
                Case SerialCommand.UnloadAcknowledge
                    WriteToLog("Received: UA")
                Case SerialCommand.ResultAcknowledge
                    WriteToLog("Received: RA," + ReceivedPacket.Data(0))
                Case Else
                    WriteToLog("Unrecognized command: " & RecievedString)
            End Select

        Catch ex As Exception

            MessageBox.Show(ex.Message, "SerialPort Error")
            WriteToLog("Receive Error: " & ex.Message)

        End Try
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub ExposureComplete(ByVal sender As Object, ByVal e As TrueTestEngine.ExposureCompleteEventArgs)
        ExposuresCompleted += 1
        WriteToLog("Exposure Number " & ExposuresCompleted & " is complete.")
        If ExposuresCompleted >= NumberOfSequenceExposures Then
            'We've taken all of the exposures and we can unload the panel now
            Dim UnloadPacket As New PacketSerial(SerialCommand.Unload)
            UnloadPacket.Send(Me)
        End If
    End Sub

    Public Sub MeasurementComplete(ByVal sender As Object, ByVal e As TrueTestEngine.MeasurementCompleteEventArgs)
        WriteToLog("Measurement Complete...")
    End Sub

    Public Sub AnalysisComplete(ByVal sender As Object, ByVal e As AnalysisCompleteEventArgs)
        WriteToLog("Analysis finished...")
    End Sub

    Public Sub SequenceComplete(ByVal sender As Object, ByVal e As TrueTestEngine.SequenceCompleteEventsArgs)
        WriteToLog("Sequence Complete...")

        Dim ResultString As String = If(e.PassFail = TrueTest.AnalysisResultEnum.Pass, "OK", "NG")

        Dim DataStringList As New List(Of String)
        DataStringList.Add(ResultString)

        Dim ResultPacket As New PacketSerial(SerialCommand.Result, DataStringList)
        ResultPacket.Send(Me)
        SequenceIsRunning = False
    End Sub

#End Region

    ''' <summary>
    ''' Blocking call. Doesn't finish until the requested pattern is confirmed to be displayed.
    ''' </summary>
    ''' 
    Protected Overrides Sub ShowPattern(ByVal p As TrueTestPatternGenerator.TrueTestPattern)
        If p Is Nothing Then Exit Sub

        If Not p.Pattern.GetType().Equals(GetType(SimplePattern)) Then Exit Sub

        AckRecieved = False
        Dim pattern As SimplePattern = CType(p.Pattern, SimplePattern)
        Dim PatternNumber As String = pattern.imagenumber.ToString
        Dim PacketDataList As New List(Of String)
        PacketDataList.Add(PatternNumber)
        Dim PatternPacket As New PacketSerial(SerialCommand.ShowPattern, PacketDataList)
        PatternPacket.Send(Me)

        If Not WaitForAck() Then
            'Fixture Timed out changing the pattern
            WriteToLog("Fixture did not send the Pattern Ready (PR) command after TrueTest requested pattern #" & PatternNumber)
            TrueTest.SequenceStop() 'Cancel sequence
        End If

    End Sub

    Private Function WaitForAck() As Boolean
        'Wait for ack from board

        'loop in increments of 10ms
        Dim Timeout As Integer = WaitTime / 10
        Dim TimeoutCounter As Integer = 0
        Do
            System.Threading.Thread.Sleep(10)
            TimeoutCounter += 1
        Loop Until AckRecieved Or TimeoutCounter > Timeout
        If TimeoutCounter >= Timeout Then
            'never got anything back
            WriteToLog("No Ack from Board, Please Check Connection and Power")
        End If

        If AckRecieved Then
            AckRecieved = False
            Return True
        End If

        AckRecieved = False
        Return AckRecieved

    End Function

#Region "Log Files and Reports"
    Private LogSyncLock As New Object
    Friend Sub WriteToLog(ByVal Message As String)
        Dim dt As Date = Now
        Dim LogFilename As String = LogFilePath & "\" & dt.ToString("yyyyMMdd") & " Comm Log.txt"
        Dim FileExists As Boolean = File.Exists(LogFilename)

        Try
            SyncLock LogSyncLock
                Dim sw As New StreamWriter(LogFilename, True)

                If FileExists = False Then
                    sw.WriteLine("Log created" & vbTab & dt.ToString("yyyy/MM/dd HH:mm:ss.fff"))
                End If

                If Message IsNot Nothing Then
                    Message = Message & vbTab & dt.ToString("yyyy/MM/dd HH:mm:ss.fff")
                    sw.WriteLine(Message)
                End If

                sw.Close()
            End SyncLock
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Log File Error")
        End Try
    End Sub

    Private Shared Function ValidateFolder(ByRef Folder As String) As Boolean
        If Folder.Length = 0 Then Return False
        Folder = Folder & CStr(IIf(Folder(Folder.Length - 1) = "\", "", "\"))

        If IO.Directory.Exists(Folder) Then
            Return True
        Else
            Try
                Dim DirInfo As IO.DirectoryInfo = IO.Directory.CreateDirectory(Folder)
                Return DirInfo.Exists

            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
                Return False

            End Try
        End If
    End Function

#End Region

    ''' <summary>
    ''' Checks the sequence for each unique measurement, as analyses may reuse measurements already taken for others. 
    ''' </summary>
    Sub PrepareForSequenceRunAll(sender As Object, e As EventArgs)
        'Calculate how many exposures are going to be required to run the sequence
        Dim PatternList As New List(Of PatternSetup)
        NumberOfSequenceExposures = 0
        For Each SeqStep As TrueTestEngine.SequenceItem In TrueTest.Sequence.Items
            If Not SeqStep.Selected Then
                Continue For
            End If
            Dim ps As PatternSetup = TrueTest.Sequence.GetPatternSetupByName(SeqStep.PatternSetupName)
            If ps Is Nothing Then Continue For
            'Check to see if we already counted this pattern's exposures
            Dim RepeatPattern As Boolean = False

            For Each P As PatternSetup In PatternList
                If P = ps Then
                    RepeatPattern = True
                    Exit For
                End If
            Next

            If Not RepeatPattern Then
                PatternList.Add(ps)
                NumberOfSequenceExposures += 1
            End If
        Next
        WriteToLog("This Sequence will require " & NumberOfSequenceExposures & " camera exposures.")
        'Reset the number of exposures completed
        ExposuresCompleted = 0
    End Sub
End Class
