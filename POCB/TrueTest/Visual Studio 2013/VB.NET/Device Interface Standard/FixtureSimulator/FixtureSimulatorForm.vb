Imports System.IO.Ports
Imports System.Runtime.CompilerServices

Public Class FixtureSimulatorForm

#Region "Initialization and Loading"

    Private Shared DataReceived() As Byte = Nothing
    Private _Parity = IO.Ports.Parity.None
    Private _StopBits = IO.Ports.StopBits.One
    Private outputText As String = Nothing
    Private inputText As String = Nothing
    Private PortOpen As Boolean = False
    Private COMMANDS() As String = {"SN,000000", "SN,111111", "RD", "PA," & "1", "UA", "RA," & "OK"}
    Private PortList() As String = {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9"}
    Private BaudRate() As String = {"110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200"}
    Private DataBits() As String = {"8", "6", "7"}
    Private Parity() As String = {"None", "Odd", "Even", "Mark", "Space"}
    Private StopBits() As String = {"1", "2"}
    Private RequestToSend() As Boolean = {True, False}
    Private DataTerminalReady() As Boolean = {True, False}
    Private Data As List(Of String)
    Private _portName As String = Nothing
    Private ex As System.Exception
    Private Command As SerialCommand

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

    Private Sub FixtureSimulatorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        commandsListBox.Items.AddRange(COMMANDS)
        PortListBox.Items.AddRange(PortList)
        BaudRateListBox.Items.AddRange(BaudRate)
        ParityListBox.Items.AddRange(Parity)
        DataBitsListBox.Items.AddRange(DataBits)
        StopBitsListBox.Items.AddRange(StopBits)
        PortListBox.SelectedIndex = 1
        BaudRateListBox.SelectedIndex = 6
        ParityListBox.SelectedIndex = 0
        DataBitsListBox.SelectedIndex = 0
        StopBitsListBox.SelectedIndex = 0
        commandsListBox.SelectedIndex = 0
        closeButton.Enabled = False
    End Sub
#End Region

#Region "Button Click Events and Functions"

    Private Sub openButton_Click(sender As Object, e As EventArgs) Handles openButton.Click
        Dim _BaudRate = CType(BaudRateListBox.SelectedItem.ToString(), Integer)
        Dim Parity_ = ParityListBox.SelectedItem.ToString()
        Select Case Parity_
            Case "None"
                _Parity = IO.Ports.Parity.None
            Case "Odd"
                _Parity = IO.Ports.Parity.Odd
            Case "Even"
                _Parity = IO.Ports.Parity.Even
            Case "Mark"
                _Parity = IO.Ports.Parity.Mark
            Case "Space"
                _Parity = IO.Ports.Parity.Space
        End Select
        Dim _DataBits = CType(DataBitsListBox.SelectedItem.ToString(), Integer)
        Dim StopBits_ = StopBitsListBox.SelectedItem.ToString()
        Select Case StopBits_
            Case "1"
                _StopBits = IO.Ports.StopBits.One
            Case "1.5"
                _StopBits = IO.Ports.StopBits.OnePointFive
            Case "2"
                _StopBits = IO.Ports.StopBits.Two
        End Select
        Dim _Port = PortListBox.SelectedItem.ToString()
        Try
            OpenSerialPort(_Port, _BaudRate, _Parity, _DataBits, _StopBits)
            outputText += "The Port has been Opened" + vbCrLf
            UpdateTextboxes()
            PortOpen = True
            openButton.Enabled = False
            PortListBox.Enabled = False
            BaudRateListBox.Enabled = False
            ParityListBox.Enabled = False
            DataBitsListBox.Enabled = False
            StopBitsListBox.Enabled = False
            closeButton.Enabled = True
        Catch
            outputText += "The Port is occupied" + vbCrLf
            UpdateTextboxes()

            SerialPort = Nothing
        End Try

    End Sub

    Private Sub closeButton_Click(sender As Object, e As EventArgs) Handles closeButton.Click
        CloseSerialPort()
        PortOpen = False
        outputText += "The Port has been Closed" + vbCrLf
        UpdateTextboxes()
        openButton.Enabled = True
        PortListBox.Enabled = True
        BaudRateListBox.Enabled = True
        ParityListBox.Enabled = True
        DataBitsListBox.Enabled = True
        StopBitsListBox.Enabled = True
        closeButton.Enabled = False
    End Sub

    Private Sub SendListCommand(sender As Object, e As EventArgs) Handles sendButton.Click
        Dim selectedCommand As String = commandsListBox.SelectedItem.ToString()
        Dim dt As Date = Now
        If PortOpen = True Then
            SendCommand(selectedCommand)
            outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: " + selectedCommand + vbCrLf
            UpdateTextboxes()
        Else
            outputText += "The Port is not open" + vbCrLf
            UpdateTextboxes()
        End If
    End Sub


    Private Sub SendCustomButton_Click(sender As Object, e As EventArgs) Handles SendCustomButton.Click
        Dim selectedCommand As String = commandsListBox.SelectedItem.ToString()
        Dim dt As Date = Now
        If PortOpen = True Then
            SendCommand(CustomCommandBox.Text)
            outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: " + CustomCommandBox.Text + vbCrLf
            UpdateTextboxes()
        Else
            outputText += "The Port is not open" + vbCrLf
            UpdateTextboxes()
        End If

        If InvokeRequired Then
            CustomCommandBox.BeginInvoke(Sub() outputTextBox.Text = String.Empty)
        Else
            CustomCommandBox.Text = String.Empty
        End If
    End Sub

    'Send the command if "Enter" is pressed while typing
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CustomCommandBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendCustomButton_Click(SendCustomButton, EventArgs.Empty)
        End If
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        inputText = String.Empty
        outputText = String.Empty
        UpdateTextboxes()
    End Sub

    Public Sub UpdateTextboxes()

        Dim inputTextValue As String = inputText
        Dim outputTextValue As String = outputText

        If InvokeRequired Then
            inputTextBox.BeginInvoke(Sub() inputTextBox.Text = System.Convert.ToString(inputTextValue))
        Else
            inputTextBox.Text = inputText
        End If

        If InvokeRequired Then
            outputTextBox.BeginInvoke(Sub() outputTextBox.Text = outputTextValue)
        Else
            outputTextBox.Text = outputText
        End If
    End Sub
#End Region

#Region "Serial Port Handling"

    Public Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles _serialPort.DataReceived
        Try
            '20ms Sleep introduced in order to allow the buffer to completely fill.
            Threading.Thread.Sleep(20)

            Dim RecievedString As String = SerialPort.ReadLine()
            Dim ReceivedPacket As New PacketSerial(RecievedString)
            Dim dt As Date = Now
            Dim bmp As New Bitmap(300, 300)
            Dim g As Graphics = Graphics.FromImage(bmp)

            Select Case ReceivedPacket.Request
                Case SerialCommand.SerialAcknowledge
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: SA," + ReceivedPacket.Data(0) + vbCrLf
                Case SerialCommand.ShowPattern
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: SP," + ReceivedPacket.Data(0) + vbCrLf
                    Select Case ReceivedPacket.Data(0)
                        Case 1
                            g.Clear(Color.White)
                            Me.PictureBox1.Image = bmp
                            SerialPort.WriteLine("PA,1")
                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: PA,1" + vbCrLf
                        Case 2
                            g.Clear(Color.Black)
                            Me.PictureBox1.Image = bmp
                            SerialPort.WriteLine("PA,2")
                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: PA,2" + vbCrLf
                        Case 3
                            g.Clear(Color.Red)
                            Me.PictureBox1.Image = bmp
                            SerialPort.WriteLine("PA,3")
                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: PA,3" + vbCrLf
                        Case 4
                            g.Clear(Color.Lime)
                            Me.PictureBox1.Image = bmp
                            SerialPort.WriteLine("PA,4")
                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: PA,4" + vbCrLf
                        Case 5
                            g.Clear(Color.Blue)
                            Me.PictureBox1.Image = bmp
                            _serialPort.WriteLine("PA,5")
                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: PA,5" + vbCrLf
                        Case Else
                            Me.inputText += "     " + ReceivedPacket.Data(0) + " is not a defined pattern!" + vbCrLf
                    End Select
                Case SerialCommand.Unload
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: UL" + vbCrLf
                    SerialPort.WriteLine("UA")
                    Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: UA" + vbCrLf
                Case SerialCommand.Result
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: RS," + ReceivedPacket.Data(0) + vbCrLf
                    SerialPort.WriteLine("RA," + ReceivedPacket.Data(0))
                    Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to applicaiton: RA," + ReceivedPacket.Data(0) + vbCrLf
                Case Else
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Unrecognized command: " & RecievedString
            End Select

            Me.UpdateTextboxes()

            DataReceived = Nothing
            SerialPort.DiscardInBuffer()

        Catch ex As Exception
            'Write Error To Output
        End Try
    End Sub

    Private WithEvents _serialPort As SerialPort = Nothing
    Public Property SerialPort() As SerialPort
        Get
            Return _serialPort
        End Get
        Set(ByVal value As SerialPort)
            _serialPort = value
        End Set
    End Property

    Sub OpenSerialPort(Portname As String, BaudRate As Integer, Parity As IO.Ports.Parity, DataBits As Integer, StopBits As IO.Ports.StopBits)

        If _serialPort Is Nothing Then
            _serialPort = New SerialPort(Portname, BaudRate, Parity, DataBits, StopBits)
            _serialPort.NewLine = vbCrLf
        End If

        If _serialPort.IsOpen Then
            Return
        Else
            _serialPort.Open()
            _serialPort.ReceivedBytesThreshold = 1
            _serialPort.DiscardNull = True
        End If

    End Sub

    Public Sub CloseSerialPort()
        If _serialPort.IsOpen Then
            _serialPort.Close()
            _serialPort = Nothing
        End If
    End Sub

    Public Sub SendCommand(Command As String)
        If _serialPort.IsOpen Then
            _serialPort.WriteLine(Command)
        End If
    End Sub

    Private Class PacketSerial
        Public Command As SerialCommand
        Public Data As List(Of String)

        ''' <summary>
        ''' Parses text to known commands
        ''' </summary>
        ''' 
        Public Sub New(ByVal PayloadString As String)
            If PayloadString Is Nothing Then Return
            Dim CommandAndData() As String = PayloadString.Split(CChar(","))

            'Why are we trimming?
            Command = GetCommand(CommandAndData(0).Trim(CChar("{0}")))
            If CommandAndData.Count > 1 Then
                Data = CommandAndData.ToList
                Data.RemoveAt(0)
            Else
                Data = Nothing
            End If
        End Sub

        Public Sub New(ByVal NewCommand As SerialCommand, Optional ByVal NewData As List(Of String) = Nothing)
            Command = NewCommand
            Data = NewData
        End Sub

        Private Function GetCommand(CommandString As String) As SerialCommand
            Select Case CommandString
                Case "SN"
                    Return SerialCommand.SerialNumber
                Case "SA"
                    Return SerialCommand.SerialAcknowledge
                Case "RD"
                    Return SerialCommand.DeviceReady
                Case "SP"
                    Return SerialCommand.ShowPattern
                Case "PA"
                    Return SerialCommand.PatternAcknowledge
                Case "UL"
                    Return SerialCommand.Unload
                Case "UA"
                    Return SerialCommand.UnloadAcknowledge
                Case "RS"
                    Return SerialCommand.Result
                Case "RA"
                    Return SerialCommand.ResultAcknowledge
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Function GetCommandString(Command As SerialCommand) As String
            Select Case Command
                Case SerialCommand.SerialNumber
                    Return "SN"
                Case SerialCommand.SerialAcknowledge
                    Return "SA"
                Case SerialCommand.DeviceReady
                    Return "RD"
                Case SerialCommand.ShowPattern
                    Return "SP"
                Case SerialCommand.PatternAcknowledge
                    Return "PA"
                Case SerialCommand.Unload
                    Return "UL"
                Case SerialCommand.UnloadAcknowledge
                    Return "UA"
                Case SerialCommand.Result
                    Return "RS"
                Case SerialCommand.ResultAcknowledge
                    Return "RA"
                Case Else
                    Return Nothing
            End Select
        End Function

        ''' <summary>
        ''' Sends Cmd over serial port.
        ''' </summary>
        Public Sub Send(TheDI As FixtureSimulatorForm)
            Dim PayloadString As String = GetCommandString(Command)

            Try
                If Data IsNot Nothing Then
                    For Each s As String In Data
                        PayloadString &= "," & s
                    Next
                End If

                TheDI.SerialPort.DiscardInBuffer()
                TheDI.SerialPort.WriteLine(PayloadString)

            Catch ex As Exception
                'Write to Output "Serial Port Error"
            End Try

        End Sub

        Public Function Request() As SerialCommand
            Return Command
        End Function

        Private Sub SendError()
            Throw New NotImplementedException
        End Sub

        Private Function StrToByteArray(ByVal str As String) As Byte()
            Dim encoding As New System.Text.ASCIIEncoding
            Return encoding.GetBytes(str & vbCrLf)
        End Function

        Private Function ByteArrayToStr(ByVal ByteArry() As Byte) As String
            Dim encoding As New System.Text.ASCIIEncoding
            Dim Temp As String = encoding.GetString(ByteArry).Split(CChar(vbCrLf))(0)
            Return Temp
        End Function
    End Class

    Public Event CommandReady(Command As String)
    Public Sub OnCommandReady(Command As String)
        RaiseEvent CommandReady(Command)
    End Sub
#End Region

End Class


Public Module Extensions

    'General Invoke Extention
    <Extension()>
    Public Sub InvokeIfRequred(control As Control, action As MethodInvoker)
        If control.InvokeRequired Then
            control.Invoke(action)
        Else
            action()
        End If
    End Sub
End Module
