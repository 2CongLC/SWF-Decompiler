Imports System.Text.Json

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(CompressionAlgorithm.Deflate)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Uncompress(CompressionAlgorithm.Deflate)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(CompressionAlgorithm.Gzip)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Uncompress(CompressionAlgorithm.Gzip)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(CompressionAlgorithm.Zlib)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Uncompress(CompressionAlgorithm.Zlib)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(CompressionAlgorithm.Lzma)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Uncompress(CompressionAlgorithm.Lzma)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Compress(CompressionAlgorithm.Brotli)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                source.Uncompress(CompressionAlgorithm.Brotli)
                IO.File.WriteAllBytes(SaveFileDialog1.FileName, source.ToArray())

                MessageBox.Show("ok")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK Then 'AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                Dim out As XDocument = Nothing
                Dim isxml As Boolean = source.TryGetXml(out)

                MessageBox.Show(isxml)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            OpenFileDialog1.Filter = "All Files(*.*)|*.*"
            SaveFileDialog1.Filter = "All Files(*.*)|*.*"


            If OpenFileDialog1.ShowDialog = DialogResult.OK Then 'AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim source As ByteArray = New ByteArray(IO.File.ReadAllBytes(OpenFileDialog1.FileName))

                Dim out As JsonDocument = Nothing
                Dim isxml As Boolean = source.TryGetJson(out)

                MessageBox.Show(isxml)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class
