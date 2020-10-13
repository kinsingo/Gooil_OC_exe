Imports StandardDI.StandardDI

Friend Class PacketSerial
    Private Command As SerialCommand
    Public Data As List(Of String)

    Public Sub New(ByVal PayloadString As String)
        If PayloadString Is Nothing Then Return
        Dim CommandAndData() As String = PayloadString.Split(CChar(","))

        Command = GetCommand(CommandAndData(0))
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

    Public Sub Send(TheDI As StandardDI)
        If TheDI.Initialized = False Then
            Dim Err As String = "The serial port is not initialized."
            MessageBox.Show(Err, "Serial Port Error")
            Return
        End If

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
            MessageBox.Show(ex.Message, "Serial Port Error")
            TheDI.WriteToLog("Send Error(" & PayloadString & "): " & ex.Message)
        End Try

    End Sub

    Public Function Request() As SerialCommand
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