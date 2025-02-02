<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndDodawaniePociagu
    Inherits Wspolne.OknoPrzywracalne

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
        Me.btnDodaj = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.lblDodawanie = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPredkosc = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblOdcinekToru = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numer pociągu:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Liczba osi:"
        '
        'txtNrPociagu
        '
        Me.txtNrPociagu.Location = New System.Drawing.Point(104, 43)
        Me.txtNrPociagu.Name = "txtNrPociagu"
        Me.txtNrPociagu.Size = New System.Drawing.Size(174, 20)
        Me.txtNrPociagu.TabIndex = 1
        '
        'txtLiczbaOsi
        '
        Me.txtLiczbaOsi.Location = New System.Drawing.Point(104, 95)
        Me.txtLiczbaOsi.Name = "txtLiczbaOsi"
        Me.txtLiczbaOsi.Size = New System.Drawing.Size(174, 20)
        Me.txtLiczbaOsi.TabIndex = 3
        '
        'cboSterowalny
        '
        Me.cboSterowalny.AutoSize = True
        Me.cboSterowalny.Location = New System.Drawing.Point(104, 147)
        Me.cboSterowalny.Name = "cboSterowalny"
        Me.cboSterowalny.Size = New System.Drawing.Size(111, 17)
        Me.cboSterowalny.TabIndex = 5
        Me.cboSterowalny.Text = "Pojazd sterowalny"
        Me.cboSterowalny.UseVisualStyleBackColor = True
        '
        'btnDodaj
        '
        Me.btnDodaj.Location = New System.Drawing.Point(152, 170)
        Me.btnDodaj.Name = "btnDodaj"
        Me.btnDodaj.Size = New System.Drawing.Size(60, 23)
        Me.btnDodaj.TabIndex = 6
        Me.btnDodaj.Text = "Dodaj"
        Me.btnDodaj.UseVisualStyleBackColor = True
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Location = New System.Drawing.Point(218, 170)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(60, 23)
        Me.btnAnuluj.TabIndex = 7
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(277, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Wybierz na schemacie tor, na którym znajduje się pociąg."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Nazwa:"
        '
        'txtNazwa
        '
        Me.txtNazwa.Location = New System.Drawing.Point(104, 69)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(174, 20)
        Me.txtNazwa.TabIndex = 2
        '
        'lblDodawanie
        '
        Me.lblDodawanie.AutoSize = True
        Me.lblDodawanie.Location = New System.Drawing.Point(12, 151)
        Me.lblDodawanie.Name = "lblDodawanie"
        Me.lblDodawanie.Size = New System.Drawing.Size(0, 13)
        Me.lblDodawanie.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Prędkość maks.:"
        '
        'txtPredkosc
        '
        Me.txtPredkosc.Location = New System.Drawing.Point(104, 121)
        Me.txtPredkosc.Name = "txtPredkosc"
        Me.txtPredkosc.Size = New System.Drawing.Size(174, 20)
        Me.txtPredkosc.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Wybrany tor:"
        '
        'lblOdcinekToru
        '
        Me.lblOdcinekToru.AutoSize = True
        Me.lblOdcinekToru.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOdcinekToru.Location = New System.Drawing.Point(85, 27)
        Me.lblOdcinekToru.Name = "lblOdcinekToru"
        Me.lblOdcinekToru.Size = New System.Drawing.Size(0, 13)
        Me.lblOdcinekToru.TabIndex = 12
        '
        'wndDodawaniePociagu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 205)
        Me.Controls.Add(Me.lblOdcinekToru)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPredkosc)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblDodawanie)
        Me.Controls.Add(Me.txtNazwa)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnDodaj)
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
    Friend WithEvents btnDodaj As Button
    Friend WithEvents btnAnuluj As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNazwa As TextBox
    Friend WithEvents lblDodawanie As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtPredkosc As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lblOdcinekToru As Label
End Class
