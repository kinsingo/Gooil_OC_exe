Imports System.Xml.Serialization
Imports RadiantCommon
Imports TrueTestEngine
Imports TrueTestEngine.TrueTest
Imports System.ComponentModel
Imports System.Drawing

'The class name usually matches the test name. This class will become the test
<Serializable()> _
Public Class CSVExport
    'DisplayAnalysisBase contains a lot of good features for tests and we recomend inheriting from it for display testing
    'This includes items like RADA and remove Morie
    Inherits DisplayAnalysisBase

    Private Const CSVExport As String = "CSVExport"

    'This is a must override of DisplayAnalysisBase, so that it can uniquly identify this test
    <XmlIgnore(), Browsable(False)> _
    Public Overrides ReadOnly Property Name As String
        Get
            Return CSVExport
        End Get
    End Property

    'Also a must override, this name can be changed by the user inside of the TrueTest GUI sequence control
    Public Overrides Property UserName As String = CSVExport

    'This property belongs to this test only, add as many properties as you need to the test and they can be configured in real time inside the GUI sequence control
    Public Property FilePath As String = "C:\Radiant Vision Systems Data\TrueTest\UserData\Test.csv"

    'This is where the bluk of the test execution resides, in this must override sub of DisplayAnalysisBase.  Put all your test execution code here
    Protected Overrides Sub Execute_()

        'Store the current measurement in the object "m"
        Dim m As MeasurementF = MyBase.PatternMeasurementList(0).CurrentMeasurement

        'Extract the image array from the measurement for Tristimulus Y
        Dim ImgArry(,) As Single = m.GetTristimulusArrayF(MeasurementBase.TristimlusType.TrisY)

        'Create an empty string to store the image array data
        Dim s As String = ""

        'Open a text file to store the image array data into
        Dim sw As New System.IO.StreamWriter(FilePath, False)

        'Now index through the 2-dimentional array and store the values into our string
        For row As Integer = 0 To m.NbrRows - 1
            For col As Integer = 0 To m.NbrCols - 1
                s &= ImgArry(col, row).ToString & ","
            Next
            'Remove the last comma from each line
            s = s.Remove(s.Count - 1)
            'Write the string into the file
            sw.WriteLine(s)
            'Empty the string for the next line
            s = ""
        Next

        'Close the file
        sw.Close()

    End Sub

End Class
