Imports System.Runtime.CompilerServices
Imports TcpCommunication
Imports System.Net

Public Class FixtureSimulatorForm

#Region "Initialization and Loading"

    Private Shared DataReceived() As Byte = Nothing

    Private outputText As String = Nothing
    Private inputText As String = Nothing
    Private PortOpen As Boolean = False
    Private COMMANDS() As String = {"SN,000000", "SN,111111", "RD", "PA," & "1", "UA", "RA," & "OK"}

    Private WithEvents tcpClient As RvsTcpClient
    Private ServerIP As IPAddress
    Private ServerIPString As String

    Private RequestToSend() As Boolean = {True, False}
    Private DataTerminalReady() As Boolean = {True, False}
    Private Data As List(Of String)
    Private _portName As String = Nothing
    Private ex As System.Exception
    Private Command As TcpCommand

    Enum TcpCommand
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

        commandsListBox.SelectedIndex = 0
        closeButton.Enabled = False
    End Sub
#End Region

#Region "Button Click Events and Functions"

    Private Sub openButton_Click(sender As Object, e As EventArgs) Handles openButton.Click

        Try
            ServerIP = IPAddress.Parse(IPAddressForm.Text)
            tcpClient = New RvsTcpClient(ServerIP, PortForm.Text, 5000)

            tcpClient.Initialize()
            outputText += "The Port has been Opened" + vbCrLf
            UpdateTextboxes()
            PortOpen = True
            openButton.Enabled = False

            closeButton.Enabled = True
        Catch
            outputText += "The Port is occupied" + vbCrLf
            UpdateTextboxes()
        End Try

    End Sub

    Private Sub closeButton_Click(sender As Object, e As EventArgs) Handles closeButton.Click
        tcpClient.Close()
        PortOpen = False
        outputText += "The Port has been Closed" + vbCrLf
        UpdateTextboxes()
        openButton.Enabled = True
        closeButton.Enabled = False
    End Sub

    Private Sub SendListCommand(sender As Object, e As EventArgs) Handles sendButton.Click
        Dim selectedCommand As String = commandsListBox.SelectedItem.ToString()
        Dim dt As Date = Now
        If PortOpen = True Then
            Try
                tcpClient.Send(StrToByteArray(selectedCommand))
            Catch ex As Exception
            End Try
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
            tcpClient.Send(StrToByteArray(CustomCommandBox.Text))
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

#Region "TCP/IP Handling"

    Public Sub Tcp_DataReceived(sender As Object, e As TcpCommunication.ClientDataReceivedEventArgs) Handles tcpClient.DataReceived
        Try
            '20ms Sleep introduced in order to allow the buffer to completely fill.
            Threading.Thread.Sleep(20)

            Dim RecievedString As Byte() = e.Data

            Dim ReceivedPacket As New PacketTcp(RecievedString)
            Dim dt As Date = Now
            Dim bmp As New Bitmap(300, 300)
            Dim g As Graphics = Graphics.FromImage(bmp)

            Select Case ReceivedPacket.Request
                Case TcpCommand.SerialAcknowledge
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: SA," + ReceivedPacket.Data(0) + vbCrLf
                Case TcpCommand.ShowPattern
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: SP," + ReceivedPacket.Data(0) + vbCrLf
                    Select Case ReceivedPacket.Data(0)
                        Case 1
                            g.Clear(Color.White)
                            Me.PictureBox1.Image = bmp
                            tcpClient.Send(StrToByteArray("PA,1"))
                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: PA,1" + vbCrLf
                        Case 2
                            g.Clear(Color.Black)
                            Me.PictureBox1.Image = bmp
                            tcpClient.Send(StrToByteArray("PA,2"))

                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: PA,2" + vbCrLf
                        Case 3
                            g.Clear(Color.Red)
                            Me.PictureBox1.Image = bmp
                            tcpClient.Send(StrToByteArray("PA,3"))

                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: PA,3" + vbCrLf
                        Case 4
                            g.Clear(Color.Lime)
                            Me.PictureBox1.Image = bmp
                            tcpClient.Send(StrToByteArray("PA,4"))

                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: PA,4" + vbCrLf
                        Case 5
                            g.Clear(Color.Blue)
                            Me.PictureBox1.Image = bmp
                            tcpClient.Send(StrToByteArray("PA,5"))

                            Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: PA,5" + vbCrLf
                        Case Else
                            Me.inputText += "     " + ReceivedPacket.Data(0) + " is not a defined pattern!" + vbCrLf
                    End Select
                Case TcpCommand.Unload
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: UL" + vbCrLf
                    tcpClient.Send(StrToByteArray("UA"))

                    Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: UA" + vbCrLf
                Case TcpCommand.Result
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command received: RS," + ReceivedPacket.Data(0) + vbCrLf
                    tcpClient.Send(StrToByteArray("RA," + ReceivedPacket.Data(0)))

                    Me.outputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Command sent to application: RA," + ReceivedPacket.Data(0) + vbCrLf
                Case Else
                    Me.inputText += dt.ToString("yyyy/MM/dd HH:mm:ss ") + "Unrecognized command: " & ByteArrayToStr(RecievedString)
            End Select

            Me.UpdateTextboxes()

            DataReceived = Nothing

        Catch ex As Exception
            'Write Error To Output
        End Try
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

    Friend Class PacketTcp
        Public Command As TcpCommand
        Public Data As List(Of String)

        ''' <summary>
        ''' Parses text to known commands
        ''' </summary>
        Public Sub New(ByVal PayloadByteArray As Byte())
            If PayloadByteArray Is Nothing Then Return
            Dim CommandAndData() As String = FixtureSimulatorForm.ByteArrayToStr(PayloadByteArray).Split(CChar(","))

            Command = GetCommand(CommandAndData(0).Trim(CChar("{0}")))
            If CommandAndData.Count > 1 Then
                Data = CommandAndData.ToList
                Data.RemoveAt(0)
            Else
                Data = Nothing
            End If
        End Sub

        Public Sub New(ByVal NewCommand As TcpCommand, Optional ByVal NewData As List(Of String) = Nothing)
            Command = NewCommand
            Data = NewData
        End Sub

        Private Function GetCommand(CommandString As String) As TcpCommand
            Select Case CommandString
                Case "SN"
                    Return TcpCommand.SerialNumber
                Case "SA"
                    Return TcpCommand.SerialAcknowledge
                Case "RD"
                    Return TcpCommand.DeviceReady
                Case "SP"
                    Return TcpCommand.ShowPattern
                Case "PA"
                    Return TcpCommand.PatternAcknowledge
                Case "UL"
                    Return TcpCommand.Unload
                Case "UA"
                    Return TcpCommand.UnloadAcknowledge
                Case "RS"
                    Return TcpCommand.Result
                Case "RA"
                    Return TcpCommand.ResultAcknowledge
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Function GetCommandString(Command As TcpCommand) As String
            Select Case Command
                Case TcpCommand.SerialNumber
                    Return "SN"
                Case TcpCommand.SerialAcknowledge
                    Return "SA"
                Case TcpCommand.DeviceReady
                    Return "RD"
                Case TcpCommand.ShowPattern
                    Return "SP"
                Case TcpCommand.PatternAcknowledge
                    Return "PA"
                Case TcpCommand.Unload
                    Return "UL"
                Case TcpCommand.UnloadAcknowledge
                    Return "UA"
                Case TcpCommand.Result
                    Return "RS"
                Case TcpCommand.ResultAcknowledge
                    Return "RA"
                Case Else
                    Return Nothing
            End Select
        End Function

        ''' <summary>
        ''' Sends Cmd over the tcp client.
        ''' </summary>
        Public Sub Send(TheDI As FixtureSimulatorForm)
            Dim PayloadString As String = GetCommandString(Command)

            Try
                If Data IsNot Nothing Then
                    For Each s As String In Data
                        PayloadString &= "," & s
                    Next
                End If

                TheDI.tcpClient.Send(FixtureSimulatorForm.StrToByteArray(PayloadString))

            Catch ex As Exception
                'Write to Output "Tcp/Ip Error"
            End Try

        End Sub

        Public Function Request() As TcpCommand
            Return Command
        End Function

        Private Sub SendError()
            Throw New NotImplementedException
        End Sub

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
