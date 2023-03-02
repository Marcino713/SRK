<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndEdytorWyswietlaczaPeronowego
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPrzewoznik = New System.Windows.Forms.TextBox()
        Me.txtKategoria = New System.Windows.Forms.TextBox()
        Me.txtNumer = New System.Windows.Forms.TextBox()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.txtStacja = New System.Windows.Forms.TextBox()
        Me.txtGodzina = New System.Windows.Forms.TextBox()
        Me.txtOpoznienie = New System.Windows.Forms.TextBox()
        Me.txtPrzez = New System.Windows.Forms.TextBox()
        Me.txtSektory = New System.Windows.Forms.TextBox()
        Me.rbTypOdjazd = New System.Windows.Forms.RadioButton()
        Me.rbTypPrzyjazd = New System.Windows.Forms.RadioButton()
        Me.cboStyl = New System.Windows.Forms.ComboBox()
        Me.pctTablica = New System.Windows.Forms.PictureBox()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.txtUwagi = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        CType(Me.pctTablica, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Przewoźnik:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Kategoria:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Numer:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Nazwa:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Stacja:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Typ:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Godzina [HH:MM]:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 194)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Opóźnienie [min]:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 220)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Przez:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 304)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Sektory:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 472)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(27, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Styl:"
        '
        'txtPrzewoznik
        '
        Me.txtPrzewoznik.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzewoznik.Location = New System.Drawing.Point(113, 12)
        Me.txtPrzewoznik.Name = "txtPrzewoznik"
        Me.txtPrzewoznik.Size = New System.Drawing.Size(239, 20)
        Me.txtPrzewoznik.TabIndex = 1
        '
        'txtKategoria
        '
        Me.txtKategoria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKategoria.Location = New System.Drawing.Point(113, 38)
        Me.txtKategoria.Name = "txtKategoria"
        Me.txtKategoria.Size = New System.Drawing.Size(239, 20)
        Me.txtKategoria.TabIndex = 2
        '
        'txtNumer
        '
        Me.txtNumer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNumer.Location = New System.Drawing.Point(113, 64)
        Me.txtNumer.Name = "txtNumer"
        Me.txtNumer.Size = New System.Drawing.Size(239, 20)
        Me.txtNumer.TabIndex = 3
        '
        'txtNazwa
        '
        Me.txtNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNazwa.Location = New System.Drawing.Point(113, 90)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(239, 20)
        Me.txtNazwa.TabIndex = 4
        '
        'txtStacja
        '
        Me.txtStacja.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStacja.Location = New System.Drawing.Point(113, 116)
        Me.txtStacja.Name = "txtStacja"
        Me.txtStacja.Size = New System.Drawing.Size(239, 20)
        Me.txtStacja.TabIndex = 5
        '
        'txtGodzina
        '
        Me.txtGodzina.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGodzina.Location = New System.Drawing.Point(113, 165)
        Me.txtGodzina.Name = "txtGodzina"
        Me.txtGodzina.Size = New System.Drawing.Size(239, 20)
        Me.txtGodzina.TabIndex = 8
        '
        'txtOpoznienie
        '
        Me.txtOpoznienie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOpoznienie.Location = New System.Drawing.Point(113, 191)
        Me.txtOpoznienie.Name = "txtOpoznienie"
        Me.txtOpoznienie.Size = New System.Drawing.Size(239, 20)
        Me.txtOpoznienie.TabIndex = 9
        '
        'txtPrzez
        '
        Me.txtPrzez.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzez.Location = New System.Drawing.Point(113, 217)
        Me.txtPrzez.Multiline = True
        Me.txtPrzez.Name = "txtPrzez"
        Me.txtPrzez.Size = New System.Drawing.Size(239, 78)
        Me.txtPrzez.TabIndex = 10
        '
        'txtSektory
        '
        Me.txtSektory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSektory.Location = New System.Drawing.Point(113, 301)
        Me.txtSektory.Multiline = True
        Me.txtSektory.Name = "txtSektory"
        Me.txtSektory.Size = New System.Drawing.Size(239, 78)
        Me.txtSektory.TabIndex = 11
        '
        'rbTypOdjazd
        '
        Me.rbTypOdjazd.AutoSize = True
        Me.rbTypOdjazd.Checked = True
        Me.rbTypOdjazd.Location = New System.Drawing.Point(113, 142)
        Me.rbTypOdjazd.Name = "rbTypOdjazd"
        Me.rbTypOdjazd.Size = New System.Drawing.Size(58, 17)
        Me.rbTypOdjazd.TabIndex = 6
        Me.rbTypOdjazd.TabStop = True
        Me.rbTypOdjazd.Text = "Odjazd"
        Me.rbTypOdjazd.UseVisualStyleBackColor = True
        '
        'rbTypPrzyjazd
        '
        Me.rbTypPrzyjazd.AutoSize = True
        Me.rbTypPrzyjazd.Location = New System.Drawing.Point(177, 142)
        Me.rbTypPrzyjazd.Name = "rbTypPrzyjazd"
        Me.rbTypPrzyjazd.Size = New System.Drawing.Size(64, 17)
        Me.rbTypPrzyjazd.TabIndex = 7
        Me.rbTypPrzyjazd.Text = "Przyjazd"
        Me.rbTypPrzyjazd.UseVisualStyleBackColor = True
        '
        'cboStyl
        '
        Me.cboStyl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStyl.FormattingEnabled = True
        Me.cboStyl.Location = New System.Drawing.Point(113, 469)
        Me.cboStyl.Name = "cboStyl"
        Me.cboStyl.Size = New System.Drawing.Size(160, 21)
        Me.cboStyl.TabIndex = 13
        '
        'pctTablica
        '
        Me.pctTablica.Location = New System.Drawing.Point(113, 496)
        Me.pctTablica.Name = "pctTablica"
        Me.pctTablica.Size = New System.Drawing.Size(160, 80)
        Me.pctTablica.TabIndex = 23
        Me.pctTablica.TabStop = False
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.Enabled = False
        Me.btnZapisz.Location = New System.Drawing.Point(277, 582)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(75, 23)
        Me.btnZapisz.TabIndex = 14
        Me.btnZapisz.Text = "Zapisz"
        Me.btnZapisz.UseVisualStyleBackColor = True
        '
        'txtUwagi
        '
        Me.txtUwagi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUwagi.Location = New System.Drawing.Point(113, 385)
        Me.txtUwagi.Multiline = True
        Me.txtUwagi.Name = "txtUwagi"
        Me.txtUwagi.Size = New System.Drawing.Size(239, 78)
        Me.txtUwagi.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 388)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 13)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Uwagi:"
        '
        'wndEdytorWyswietlaczaPeronowego
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 619)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtUwagi)
        Me.Controls.Add(Me.btnZapisz)
        Me.Controls.Add(Me.pctTablica)
        Me.Controls.Add(Me.cboStyl)
        Me.Controls.Add(Me.rbTypPrzyjazd)
        Me.Controls.Add(Me.rbTypOdjazd)
        Me.Controls.Add(Me.txtSektory)
        Me.Controls.Add(Me.txtPrzez)
        Me.Controls.Add(Me.txtOpoznienie)
        Me.Controls.Add(Me.txtGodzina)
        Me.Controls.Add(Me.txtStacja)
        Me.Controls.Add(Me.txtNazwa)
        Me.Controls.Add(Me.txtNumer)
        Me.Controls.Add(Me.txtKategoria)
        Me.Controls.Add(Me.txtPrzewoznik)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "wndEdytorWyswietlaczaPeronowego"
        Me.Text = "Edytor wyświetlacza peronowego"
        CType(Me.pctTablica, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtPrzewoznik As TextBox
    Friend WithEvents txtKategoria As TextBox
    Friend WithEvents txtNumer As TextBox
    Friend WithEvents txtNazwa As TextBox
    Friend WithEvents txtStacja As TextBox
    Friend WithEvents txtGodzina As TextBox
    Friend WithEvents txtOpoznienie As TextBox
    Friend WithEvents txtPrzez As TextBox
    Friend WithEvents txtSektory As TextBox
    Friend WithEvents rbTypOdjazd As RadioButton
    Friend WithEvents rbTypPrzyjazd As RadioButton
    Friend WithEvents cboStyl As ComboBox
    Friend WithEvents pctTablica As PictureBox
    Friend WithEvents btnZapisz As Button
    Friend WithEvents txtUwagi As TextBox
    Friend WithEvents Label12 As Label
End Class
