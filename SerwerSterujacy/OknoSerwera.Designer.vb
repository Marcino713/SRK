<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndOknoSerwera
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblStanWczytania = New System.Windows.Forms.Label()
        Me.btnWczytaj = New System.Windows.Forms.Button()
        Me.btnPrzegladaj = New System.Windows.Forms.Button()
        Me.txtSciezka = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblStanSerwera = New System.Windows.Forms.Label()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnRozlacz = New System.Windows.Forms.Button()
        Me.btnOdswiez = New System.Windows.Forms.Button()
        Me.lvPosterunki = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblStanWczytania)
        Me.GroupBox1.Controls.Add(Me.btnWczytaj)
        Me.GroupBox1.Controls.Add(Me.btnPrzegladaj)
        Me.GroupBox1.Controls.Add(Me.txtSciezka)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(829, 77)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Plik połączeń"
        '
        'lblStanWczytania
        '
        Me.lblStanWczytania.AutoSize = True
        Me.lblStanWczytania.Location = New System.Drawing.Point(141, 50)
        Me.lblStanWczytania.Name = "lblStanWczytania"
        Me.lblStanWczytania.Size = New System.Drawing.Size(0, 13)
        Me.lblStanWczytania.TabIndex = 4
        '
        'btnWczytaj
        '
        Me.btnWczytaj.Location = New System.Drawing.Point(59, 45)
        Me.btnWczytaj.Name = "btnWczytaj"
        Me.btnWczytaj.Size = New System.Drawing.Size(75, 23)
        Me.btnWczytaj.TabIndex = 3
        Me.btnWczytaj.Text = "Wczytaj"
        Me.btnWczytaj.UseVisualStyleBackColor = True
        '
        'btnPrzegladaj
        '
        Me.btnPrzegladaj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrzegladaj.Location = New System.Drawing.Point(748, 18)
        Me.btnPrzegladaj.Name = "btnPrzegladaj"
        Me.btnPrzegladaj.Size = New System.Drawing.Size(75, 22)
        Me.btnPrzegladaj.TabIndex = 2
        Me.btnPrzegladaj.Text = "Przeglądaj..."
        Me.btnPrzegladaj.UseVisualStyleBackColor = True
        '
        'txtSciezka
        '
        Me.txtSciezka.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSciezka.Location = New System.Drawing.Point(60, 19)
        Me.txtSciezka.Name = "txtSciezka"
        Me.txtSciezka.Size = New System.Drawing.Size(682, 20)
        Me.txtSciezka.TabIndex = 1
        Me.txtSciezka.Text = "C:\Users\Marcin\Desktop\Testy\Testy pol\a.pol"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ścieżka:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.lblStanSerwera)
        Me.GroupBox2.Controls.Add(Me.btnStop)
        Me.GroupBox2.Controls.Add(Me.btnStart)
        Me.GroupBox2.Controls.Add(Me.txtHaslo)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtPort)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 95)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(147, 421)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Kontrola"
        '
        'lblStanSerwera
        '
        Me.lblStanSerwera.AutoSize = True
        Me.lblStanSerwera.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStanSerwera.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStanSerwera.Location = New System.Drawing.Point(6, 152)
        Me.lblStanSerwera.Name = "lblStanSerwera"
        Me.lblStanSerwera.Size = New System.Drawing.Size(0, 13)
        Me.lblStanSerwera.TabIndex = 6
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(5, 126)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(137, 23)
        Me.btnStop.TabIndex = 7
        Me.btnStop.Text = "Zatrzymaj serwer"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(5, 97)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(137, 23)
        Me.btnStart.TabIndex = 6
        Me.btnStart.Text = "Uruchom serwer"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(6, 71)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtHaslo.Size = New System.Drawing.Size(135, 20)
        Me.txtHaslo.TabIndex = 5
        Me.txtHaslo.Text = "a"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Hasło:"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(6, 32)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(135, 20)
        Me.txtPort.TabIndex = 4
        Me.txtPort.Text = "100"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Port:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnRozlacz)
        Me.GroupBox3.Controls.Add(Me.btnOdswiez)
        Me.GroupBox3.Controls.Add(Me.lvPosterunki)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Location = New System.Drawing.Point(165, 95)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(676, 421)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Posterunki i klienci"
        '
        'btnRozlacz
        '
        Me.btnRozlacz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRozlacz.Location = New System.Drawing.Point(515, 392)
        Me.btnRozlacz.Name = "btnRozlacz"
        Me.btnRozlacz.Size = New System.Drawing.Size(75, 23)
        Me.btnRozlacz.TabIndex = 9
        Me.btnRozlacz.Text = "Rozłącz"
        Me.btnRozlacz.UseVisualStyleBackColor = True
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOdswiez.Location = New System.Drawing.Point(596, 392)
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(75, 23)
        Me.btnOdswiez.TabIndex = 10
        Me.btnOdswiez.Text = "Odśwież"
        Me.btnOdswiez.UseVisualStyleBackColor = True
        '
        'lvPosterunki
        '
        Me.lvPosterunki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPosterunki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lvPosterunki.FullRowSelect = True
        Me.lvPosterunki.HideSelection = False
        Me.lvPosterunki.Location = New System.Drawing.Point(6, 32)
        Me.lvPosterunki.MultiSelect = False
        Me.lvPosterunki.Name = "lvPosterunki"
        Me.lvPosterunki.Size = New System.Drawing.Size(664, 354)
        Me.lvPosterunki.TabIndex = 8
        Me.lvPosterunki.UseCompatibleStateImageBehavior = False
        Me.lvPosterunki.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Nazwa"
        Me.ColumnHeader1.Width = 130
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Plik"
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Adres"
        Me.ColumnHeader3.Width = 80
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Data podłączenia"
        Me.ColumnHeader4.Width = 120
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Ostatnie zapytanie"
        Me.ColumnHeader5.Width = 120
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(218, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Dostępne posterunki i obsługujący je klienci:"
        '
        'wndOknoSerwera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 528)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "wndOknoSerwera"
        Me.Text = "Serwer sterowania ruchem kolejowym"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblStanWczytania As Label
    Friend WithEvents btnWczytaj As Button
    Friend WithEvents btnPrzegladaj As Button
    Friend WithEvents txtSciezka As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblStanSerwera As Label
    Friend WithEvents btnStop As Button
    Friend WithEvents btnStart As Button
    Friend WithEvents txtHaslo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtPort As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnOdswiez As Button
    Friend WithEvents lvPosterunki As ListView
    Friend WithEvents Label6 As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents btnRozlacz As Button
End Class
