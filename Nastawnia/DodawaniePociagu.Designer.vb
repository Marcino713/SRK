<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndDodawaniePociagu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNrPociagu = New System.Windows.Forms.TextBox()
        Me.txtLiczbaOsi = New System.Windows.Forms.TextBox()
        Me.cboSterowalny = New System.Windows.Forms.CheckBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numer pociągu:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Liczba osi:"
        '
        'txtNrPociagu
        '
        Me.txtNrPociagu.Location = New System.Drawing.Point(100, 45)
        Me.txtNrPociagu.Name = "txtNrPociagu"
        Me.txtNrPociagu.Size = New System.Drawing.Size(129, 20)
        Me.txtNrPociagu.TabIndex = 2
        '
        'txtLiczbaOsi
        '
        Me.txtLiczbaOsi.Location = New System.Drawing.Point(100, 71)
        Me.txtLiczbaOsi.Name = "txtLiczbaOsi"
        Me.txtLiczbaOsi.Size = New System.Drawing.Size(129, 20)
        Me.txtLiczbaOsi.TabIndex = 3
        '
        'cboSterowalny
        '
        Me.cboSterowalny.AutoSize = True
        Me.cboSterowalny.Location = New System.Drawing.Point(100, 97)
        Me.cboSterowalny.Name = "cboSterowalny"
        Me.cboSterowalny.Size = New System.Drawing.Size(111, 17)
        Me.cboSterowalny.TabIndex = 4
        Me.cboSterowalny.Text = "Pojazd sterowalny"
        Me.cboSterowalny.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(99, 121)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(62, 23)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Location = New System.Drawing.Point(168, 120)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(62, 23)
        Me.btnAnuluj.TabIndex = 6
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(217, 26)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Wybierz na schemacie tor prosty lub ukośny," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "na którym znajduje się pociąg."
        '
        'wndDodawaniePociagu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 156)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cboSterowalny)
        Me.Controls.Add(Me.txtLiczbaOsi)
        Me.Controls.Add(Me.txtNrPociagu)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "wndDodawaniePociagu"
        Me.Text = "Dodawanie pociągu"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNrPociagu As TextBox
    Friend WithEvents txtLiczbaOsi As TextBox
    Friend WithEvents cboSterowalny As CheckBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnAnuluj As Button
    Friend WithEvents Label3 As Label
End Class
