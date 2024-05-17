Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.swf)|*.swf"
            SaveFileDialog1.Filter = "All Files(*.swf)|*.swf"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As SWF = New SWF(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(SaveFileDialog1.FileName, SWF.CompressTionTypes.CWS)

                MessageBox.Show("ok")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.swf)|*.swf"
            SaveFileDialog1.Filter = "All Files(*.swf)|*.swf"

            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As SWF = New SWF(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(SaveFileDialog1.FileName, SWF.CompressTionTypes.ZWS)

                MessageBox.Show("ok")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.swf)|*.swf"
            SaveFileDialog1.Filter = "All Files(*.swf)|*.swf"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As SWF = New SWF(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.DeCompress(SaveFileDialog1.FileName)

                MessageBox.Show("ok")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.swf)|*.swf"



            If OpenFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As SWF = New SWF(IO.File.ReadAllBytes(OpenFileDialog1.FileName))
                TextBox1.Text = source.Signature
                TextBox2.Text = source.Version
                TextBox3.Text = source.Filesize
                TextBox4.Text = source.Width
                TextBox5.Text = source.Heigth
                TextBox6.Text = source.FrameRate
                TextBox7.Text = source.FrameCount
                TextBox8.Text = source.Tags


            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class
