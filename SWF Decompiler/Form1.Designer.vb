<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        OpenFileDialog1 = New OpenFileDialog()
        SaveFileDialog1 = New SaveFileDialog()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button5 = New Button()
        Button6 = New Button()
        Button7 = New Button()
        Button8 = New Button()
        Button9 = New Button()
        Button10 = New Button()
        Button11 = New Button()
        Button12 = New Button()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(29, 42)
        Button1.Name = "Button1"
        Button1.Size = New Size(143, 23)
        Button1.TabIndex = 0
        Button1.Text = "Compress Deflate"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(178, 42)
        Button2.Name = "Button2"
        Button2.Size = New Size(143, 23)
        Button2.TabIndex = 1
        Button2.Text = "Decompress Deflate"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(29, 71)
        Button3.Name = "Button3"
        Button3.Size = New Size(143, 26)
        Button3.TabIndex = 2
        Button3.Text = "Compress Gzip"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(178, 74)
        Button4.Name = "Button4"
        Button4.Size = New Size(143, 23)
        Button4.TabIndex = 3
        Button4.Text = "Decompress Gzip"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(29, 103)
        Button5.Name = "Button5"
        Button5.Size = New Size(143, 23)
        Button5.TabIndex = 4
        Button5.Text = "Compress Zlib"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(178, 103)
        Button6.Name = "Button6"
        Button6.Size = New Size(143, 23)
        Button6.TabIndex = 5
        Button6.Text = "Decompress Zlib"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(29, 132)
        Button7.Name = "Button7"
        Button7.Size = New Size(143, 23)
        Button7.TabIndex = 6
        Button7.Text = "Compress Lzma"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' Button8
        ' 
        Button8.Location = New Point(178, 132)
        Button8.Name = "Button8"
        Button8.Size = New Size(143, 23)
        Button8.TabIndex = 7
        Button8.Text = "DeCompress Lzma"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' Button9
        ' 
        Button9.Location = New Point(29, 161)
        Button9.Name = "Button9"
        Button9.Size = New Size(143, 23)
        Button9.TabIndex = 8
        Button9.Text = "Compress Brotli"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' Button10
        ' 
        Button10.Location = New Point(178, 161)
        Button10.Name = "Button10"
        Button10.Size = New Size(143, 23)
        Button10.TabIndex = 9
        Button10.Text = "Decompress Brotli"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' Button11
        ' 
        Button11.Location = New Point(340, 42)
        Button11.Name = "Button11"
        Button11.Size = New Size(172, 23)
        Button11.TabIndex = 10
        Button11.Text = "Try Get Xml"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' Button12
        ' 
        Button12.Location = New Point(340, 71)
        Button12.Name = "Button12"
        Button12.Size = New Size(172, 23)
        Button12.TabIndex = 11
        Button12.Text = "Try Get Json"
        Button12.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button12)
        Controls.Add(Button11)
        Controls.Add(Button10)
        Controls.Add(Button9)
        Controls.Add(Button8)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form1"
        ResumeLayout(False)
    End Sub

    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button12 As Button

End Class
