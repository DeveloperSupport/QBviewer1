Imports System.IO

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()
        If My.Computer.FileSystem.FileExists(TextBox1.Text) Then
            Debug.Print("file exists")
            Label3.Text = "Yes"
            Call CheckQBWfile()
        Else
            Debug.Print("file does not exist")
            Label3.Text = "No"
        End If
    End Sub

    Private Sub CheckQBWfile()

        Try
            Using reader As New BinaryReader(File.Open(TextBox1.Text, FileMode.Open, FileAccess.Read))
                ' Loop through length of file.
                Dim buf As String = ""
                Dim buf2 As String = ""
                Dim pos As Integer = 1
                Dim length As Integer = reader.BaseStream.Length
                Dim qbwposition As Integer = Val(TextBox2.Text)
                While pos < qbwposition
                    Dim value As Integer = reader.ReadByte
                    buf = buf & Hex(value).PadLeft(2, "0") & " "
                    Select Case value
                        Case 32 To 127
                            buf2 = buf2 & Chr(value)
                        Case Else
                            buf2 = buf2 & "."
                    End Select
                    If pos Mod 16 = 0 Then
                        ListBox1.Items.Add(Hex(pos - 16).PadLeft(8, "0") & ": " & buf & " | " & buf2)
                        buf = ""
                        buf2 = ""
                    End If
                    pos += 1
                End While
            End Using
        Catch
            'do nothing
        Finally
            'do nothing
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Filename:"
        Label2.Text = "File Exists?"
        Label3.Text = "?"
        Label4.Text = "Bytes to Read:"
        ListBox1.Font = New Drawing.Font("Consolas", 12, FontStyle.Regular)
        TextBox1.Text = "data.txt"
        TextBox2.Text = "1024"
        Button1.Text = "Read File"
    End Sub
End Class
