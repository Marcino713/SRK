﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbObserwatorzy = New System.Windows.Forms.CheckBox()
        Me.txtHasloObserwator = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSymulator = New System.Windows.Forms.Button()
        Me.lblStanWczytania = New System.Windows.Forms.Label()
        Me.btnWczytaj = New System.Windows.Forms.Button()
        Me.btnPrzegladaj = New System.Windows.Forms.Button()
        Me.txtSciezka = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnFiltrujAdresy = New System.Windows.Forms.Button()
        Me.lblStanSerwera = New System.Windows.Forms.Label()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPortTCP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnObserwatorzy = New System.Windows.Forms.Button()
        Me.btnRozlacz = New System.Windows.Forms.Button()
        Me.btnOdswiez = New System.Windows.Forms.Button()
        Me.lvPosterunki = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnKonfZwrotnic = New System.Windows.Forms.Button()
        Me.btnPociagi = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnUartRozlacz = New System.Windows.Forms.Button()
        Me.lblUartStan = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnUartPolacz = New System.Windows.Forms.Button()
        Me.txtUartPort = New System.Windows.Forms.TextBox()
        Me.spKomunikacja = New System.IO.Ports.SerialPort(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cbObserwatorzy)
        Me.GroupBox1.Controls.Add(Me.txtHasloObserwator)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btnSymulator)
        Me.GroupBox1.Controls.Add(Me.lblStanWczytania)
        Me.GroupBox1.Controls.Add(Me.btnWczytaj)
        Me.GroupBox1.Controls.Add(Me.btnPrzegladaj)
        Me.GroupBox1.Controls.Add(Me.txtSciezka)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(980, 77)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Plik połączeń"
        '
        'cbObserwatorzy
        '
        Me.cbObserwatorzy.AutoSize = True
        Me.cbObserwatorzy.Checked = True
        Me.cbObserwatorzy.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbObserwatorzy.Location = New System.Drawing.Point(283, 49)
        Me.cbObserwatorzy.Name = "cbObserwatorzy"
        Me.cbObserwatorzy.Size = New System.Drawing.Size(239, 17)
        Me.cbObserwatorzy.TabIndex = 8
        Me.cbObserwatorzy.Text = "Zezwalaj na połączenia w trybie obserwatora"
        Me.cbObserwatorzy.UseVisualStyleBackColor = True
        '
        'txtHasloObserwator
        '
        Me.txtHasloObserwator.Location = New System.Drawing.Point(679, 47)
        Me.txtHasloObserwator.Name = "txtHasloObserwator"
        Me.txtHasloObserwator.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtHasloObserwator.Size = New System.Drawing.Size(100, 20)
        Me.txtHasloObserwator.TabIndex = 7
        Me.txtHasloObserwator.Text = "b"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(547, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Hasło trybu obserwatora:"
        '
        'btnSymulator
        '
        Me.btnSymulator.Location = New System.Drawing.Point(140, 45)
        Me.btnSymulator.Name = "btnSymulator"
        Me.btnSymulator.Size = New System.Drawing.Size(137, 23)
        Me.btnSymulator.TabIndex = 5
        Me.btnSymulator.Text = "Symulator srk"
        Me.btnSymulator.UseVisualStyleBackColor = True
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
        Me.btnPrzegladaj.Location = New System.Drawing.Point(900, 18)
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
        Me.txtSciezka.Size = New System.Drawing.Size(834, 20)
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
        Me.GroupBox2.Controls.Add(Me.btnFiltrujAdresy)
        Me.GroupBox2.Controls.Add(Me.lblStanSerwera)
        Me.GroupBox2.Controls.Add(Me.btnStop)
        Me.GroupBox2.Controls.Add(Me.btnStart)
        Me.GroupBox2.Controls.Add(Me.txtHaslo)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtPortTCP)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 95)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(147, 202)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Zarządzanie serwerem"
        '
        'btnFiltrujAdresy
        '
        Me.btnFiltrujAdresy.Location = New System.Drawing.Point(5, 155)
        Me.btnFiltrujAdresy.Name = "btnFiltrujAdresy"
        Me.btnFiltrujAdresy.Size = New System.Drawing.Size(136, 23)
        Me.btnFiltrujAdresy.TabIndex = 9
        Me.btnFiltrujAdresy.Text = "Filtruj adresy"
        Me.btnFiltrujAdresy.UseVisualStyleBackColor = True
        '
        'lblStanSerwera
        '
        Me.lblStanSerwera.AutoSize = True
        Me.lblStanSerwera.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStanSerwera.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStanSerwera.Location = New System.Drawing.Point(6, 181)
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
        Me.btnStop.TabIndex = 8
        Me.btnStop.Text = "Zatrzymaj serwer"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(5, 97)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(137, 23)
        Me.btnStart.TabIndex = 7
        Me.btnStart.Text = "Uruchom serwer"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(6, 71)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtHaslo.Size = New System.Drawing.Size(135, 20)
        Me.txtHaslo.TabIndex = 6
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
        'txtPortTCP
        '
        Me.txtPortTCP.Location = New System.Drawing.Point(6, 32)
        Me.txtPortTCP.Name = "txtPortTCP"
        Me.txtPortTCP.Size = New System.Drawing.Size(135, 20)
        Me.txtPortTCP.TabIndex = 5
        Me.txtPortTCP.Text = "100"
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
        Me.GroupBox3.Controls.Add(Me.btnObserwatorzy)
        Me.GroupBox3.Controls.Add(Me.btnRozlacz)
        Me.GroupBox3.Controls.Add(Me.btnOdswiez)
        Me.GroupBox3.Controls.Add(Me.lvPosterunki)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Location = New System.Drawing.Point(165, 95)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(827, 426)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Posterunki i klienci"
        '
        'btnObserwatorzy
        '
        Me.btnObserwatorzy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnObserwatorzy.Location = New System.Drawing.Point(567, 397)
        Me.btnObserwatorzy.Name = "btnObserwatorzy"
        Me.btnObserwatorzy.Size = New System.Drawing.Size(93, 23)
        Me.btnObserwatorzy.TabIndex = 21
        Me.btnObserwatorzy.Text = "Obserwatorzy"
        Me.btnObserwatorzy.UseVisualStyleBackColor = True
        '
        'btnRozlacz
        '
        Me.btnRozlacz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRozlacz.Location = New System.Drawing.Point(666, 397)
        Me.btnRozlacz.Name = "btnRozlacz"
        Me.btnRozlacz.Size = New System.Drawing.Size(75, 23)
        Me.btnRozlacz.TabIndex = 19
        Me.btnRozlacz.Text = "Rozłącz"
        Me.btnRozlacz.UseVisualStyleBackColor = True
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOdswiez.Location = New System.Drawing.Point(747, 397)
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(75, 23)
        Me.btnOdswiez.TabIndex = 20
        Me.btnOdswiez.Text = "Odśwież"
        Me.btnOdswiez.UseVisualStyleBackColor = True
        '
        'lvPosterunki
        '
        Me.lvPosterunki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPosterunki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvPosterunki.FullRowSelect = True
        Me.lvPosterunki.HideSelection = False
        Me.lvPosterunki.Location = New System.Drawing.Point(6, 32)
        Me.lvPosterunki.MultiSelect = False
        Me.lvPosterunki.Name = "lvPosterunki"
        Me.lvPosterunki.Size = New System.Drawing.Size(815, 359)
        Me.lvPosterunki.TabIndex = 18
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
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Adres"
        Me.ColumnHeader3.Width = 50
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
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Adres IP"
        Me.ColumnHeader6.Width = 130
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Obserwatorzy"
        Me.ColumnHeader7.Width = 90
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
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.btnKonfZwrotnic)
        Me.GroupBox4.Controls.Add(Me.btnPociagi)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 443)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(147, 78)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Zarządzanie obiektami"
        '
        'btnKonfZwrotnic
        '
        Me.btnKonfZwrotnic.Enabled = False
        Me.btnKonfZwrotnic.Location = New System.Drawing.Point(5, 48)
        Me.btnKonfZwrotnic.Name = "btnKonfZwrotnic"
        Me.btnKonfZwrotnic.Size = New System.Drawing.Size(137, 23)
        Me.btnKonfZwrotnic.TabIndex = 16
        Me.btnKonfZwrotnic.Text = "Konfiguracja zwrotnic..."
        Me.btnKonfZwrotnic.UseVisualStyleBackColor = True
        '
        'btnPociagi
        '
        Me.btnPociagi.Enabled = False
        Me.btnPociagi.Location = New System.Drawing.Point(5, 19)
        Me.btnPociagi.Name = "btnPociagi"
        Me.btnPociagi.Size = New System.Drawing.Size(137, 23)
        Me.btnPociagi.TabIndex = 15
        Me.btnPociagi.Text = "Pociągi..."
        Me.btnPociagi.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnUartRozlacz)
        Me.GroupBox5.Controls.Add(Me.lblUartStan)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.btnUartPolacz)
        Me.GroupBox5.Controls.Add(Me.txtUartPort)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 303)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(147, 134)
        Me.GroupBox5.TabIndex = 10
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Komunikacja UART"
        '
        'btnUartRozlacz
        '
        Me.btnUartRozlacz.Enabled = False
        Me.btnUartRozlacz.Location = New System.Drawing.Point(5, 87)
        Me.btnUartRozlacz.Name = "btnUartRozlacz"
        Me.btnUartRozlacz.Size = New System.Drawing.Size(137, 23)
        Me.btnUartRozlacz.TabIndex = 13
        Me.btnUartRozlacz.Text = "Rozłącz"
        Me.btnUartRozlacz.UseVisualStyleBackColor = True
        '
        'lblUartStan
        '
        Me.lblUartStan.AutoSize = True
        Me.lblUartStan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUartStan.Location = New System.Drawing.Point(6, 113)
        Me.lblUartStan.Name = "lblUartStan"
        Me.lblUartStan.Size = New System.Drawing.Size(0, 13)
        Me.lblUartStan.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Port:"
        '
        'btnUartPolacz
        '
        Me.btnUartPolacz.Location = New System.Drawing.Point(5, 58)
        Me.btnUartPolacz.Name = "btnUartPolacz"
        Me.btnUartPolacz.Size = New System.Drawing.Size(137, 23)
        Me.btnUartPolacz.TabIndex = 12
        Me.btnUartPolacz.Text = "Połącz"
        Me.btnUartPolacz.UseVisualStyleBackColor = True
        '
        'txtUartPort
        '
        Me.txtUartPort.Location = New System.Drawing.Point(6, 32)
        Me.txtUartPort.Name = "txtUartPort"
        Me.txtUartPort.Size = New System.Drawing.Size(135, 20)
        Me.txtUartPort.TabIndex = 11
        '
        'spKomunikacja
        '
        Me.spKomunikacja.BaudRate = 19200
        '
        'wndOknoSerwera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 533)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
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
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
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
    Friend WithEvents txtPortTCP As TextBox
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
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents btnPociagi As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btnUartPolacz As Button
    Friend WithEvents txtUartPort As TextBox
    Friend WithEvents spKomunikacja As IO.Ports.SerialPort
    Friend WithEvents Label2 As Label
    Friend WithEvents lblUartStan As Label
    Friend WithEvents btnUartRozlacz As Button
    Friend WithEvents btnKonfZwrotnic As Button
    Friend WithEvents btnFiltrujAdresy As Button
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents btnSymulator As Button
    Friend WithEvents cbObserwatorzy As CheckBox
    Friend WithEvents txtHasloObserwator As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents btnObserwatorzy As Button
End Class
