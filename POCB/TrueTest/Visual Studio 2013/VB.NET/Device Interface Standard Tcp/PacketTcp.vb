Imports StandardDI.StandardDITcp
Imports TcpCommunication
Imports System.Net.Sockets

Friend Class PacketTcp

    Private Command As TcpCommand
    Public Data As List(Of String)

    Public Sub New(ByVal PayloadByteArray As Byte())
        If PayloadByteArray Is Nothing Then Return
        Dim CommandAndData() As String = ByteArrayToStr(PayloadByteArray).Split(CChar(","))

        Command = GetCommand(CommandAndData(0))
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

    Public Sub Send(TheDI As StandardDITcp)
        If TheDI.Initialized = False Then
            Dim Err As String = "The connection is not initialized."
            MessageBox.Show(Err, "Tcp/Ip Error")
            Return
        End If

        If TheDI.DemoMode = False Then
            Dim PayloadString As String = GetCommandString(Command)

            Try
                If Data IsNot Nothing Then
                    For Each s As String In Data
                        PayloadString &= "," & s
                    Next
                End If

                TheDI.SendPacket(StrToByteArray(PayloadString))

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Tcp/Ip Error")
                TheDI.WriteToLog("Send Error(" & PayloadString & "): " & ex.Message)
            End Try
        End If

    End Sub

    Public Function Request() As TcpCommand
        Return Command
    End Function

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