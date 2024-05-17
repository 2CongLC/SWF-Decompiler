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
        SaveFileDialog1 = New SaveFileDialog()
        OpenFileDialog1 = New OpenFileDialog()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        TextBox3 = New TextBox()
        TextBox4 = New TextBox()
        TextBox5 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        TextBox6 = New TextBox()
        Label6 = New Label()
        Label7 = New Label()
        TextBox7 = New TextBox()
        Label8 = New Label()
        Label9 = New Label()
        TextBox8 = New TextBox()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(91, 313)
        Button1.Name = "Button1"
        Button1.Size = New Size(134, 23)
        Button1.TabIndex = 0
        Button1.Text = "Compress CWS"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(91, 342)
        Button2.Name = "Button2"
        Button2.Size = New Size(134, 23)
        Button2.TabIndex = 1
        Button2.Text = "Compress ZWS"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(91, 371)
        Button3.Name = "Button3"
        Button3.Size = New Size(134, 30)
        Button3.TabIndex = 2
        Button3.Text = "DeCompress"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(24, 252)
        Button4.Name = "Button4"
        Button4.Size = New Size(263, 38)
        Button4.TabIndex = 3
        Button4.Text = "SWF Info"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(102, 12)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(185, 23)
        TextBox1.TabIndex = 4
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(102, 41)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(185, 23)
        TextBox2.TabIndex = 5
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(102, 70)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(185, 23)
        TextBox3.TabIndex = 6
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(69, 99)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(73, 23)
        TextBox4.TabIndex = 7
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(197, 99)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(90, 23)
        TextBox5.TabIndex = 8
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(24, 102)
        Label1.Name = "Label1"
        Label1.Size = New Size(39, 15)
        Label1.TabIndex = 9
        Label1.Text = "Width"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(148, 102)
        Label2.Name = "Label2"
        Label2.Size = New Size(43, 15)
        Label2.TabIndex = 10
        Label2.Text = "Heigth"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(24, 15)
        Label3.Name = "Label3"
        Label3.Size = New Size(66, 15)
        Label3.TabIndex = 11
        Label3.Text = "Signature : "
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(36, 44)
        Label4.Name = "Label4"
        Label4.Size = New Size(54, 15)
        Label4.TabIndex = 12
        Label4.Text = "Version : "
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(36, 73)
        Label5.Name = "Label5"
        Label5.Size = New Size(53, 15)
        Label5.TabIndex = 13
        Label5.Text = "Filesize : "
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(102, 141)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(132, 23)
        TextBox6.TabIndex = 14
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(14, 145)
        Label6.Name = "Label6"
        Label6.Size = New Size(75, 15)
        Label6.TabIndex = 15
        Label6.Text = "Frame Rate : "
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(246, 145)
        Label7.Name = "Label7"
        Label7.Size = New Size(23, 15)
        Label7.TabIndex = 16
        Label7.Text = "fps"
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(102, 171)
        TextBox7.Name = "TextBox7"
        TextBox7.Size = New Size(172, 23)
        TextBox7.TabIndex = 17
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(5, 174)
        Label8.Name = "Label8"
        Label8.Size = New Size(85, 15)
        Label8.TabIndex = 18
        Label8.Text = "Frame Count : "
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(17, 206)
        Label9.Name = "Label9"
        Label9.Size = New Size(79, 15)
        Label9.TabIndex = 19
        Label9.Text = "Tags Length : "
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(102, 203)
        TextBox8.Name = "TextBox8"
        TextBox8.Size = New Size(172, 23)
        TextBox8.TabIndex = 20
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(307, 413)
        Controls.Add(TextBox8)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(TextBox7)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(TextBox6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox5)
        Controls.Add(TextBox4)
        Controls.Add(TextBox3)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SWF Decompiler"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBox8 As TextBox

End Class
