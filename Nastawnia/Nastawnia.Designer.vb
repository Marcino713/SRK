<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndNastawnia
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
        Dim Pulpit1 As Zaleznosci.Pulpit = New Zaleznosci.Pulpit()
        Me.mnuMenu = New System.Windows.Forms.MenuStrip()
        Me.mnuNarzedzia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPolaczZSerwerem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRozlaczZSerwerem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZarzadzajSerwerem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDodajPociag = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOswietlenie = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuKonfiguratorStacji = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuNowePolaczenia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOtworzPolaczenia = New System.Windows.Forms.ToolStripMenuItem()
        Me.stStan = New System.Windows.Forms.StatusStrip()
        Me.tslStanPolaczenia = New System.Windows.Forms.ToolStripStatusLabel()
        Me.plpPulpit = New Nastawnia.PulpitSterowniczy()
        Me.mnuMenu.SuspendLayout()
        Me.stStan.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMenu
        '
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNarzedzia})
        Me.mnuMenu.Location = New System.Drawing.Point(0, 0)
        Me.mnuMenu.Name = "mnuMenu"
        Me.mnuMenu.Size = New System.Drawing.Size(645, 24)
        Me.mnuMenu.TabIndex = 0
        Me.mnuMenu.Text = "MenuStrip1"
        '
        'mnuNarzedzia
        '
        Me.mnuNarzedzia.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPolaczZSerwerem, Me.mnuRozlaczZSerwerem, Me.mnuZarzadzajSerwerem, Me.ToolStripSeparator3, Me.mnuDodajPociag, Me.mnuOswietlenie, Me.ToolStripSeparator2, Me.mnuKonfiguratorStacji, Me.ToolStripSeparator1, Me.mnuNowePolaczenia, Me.mnuOtworzPolaczenia})
        Me.mnuNarzedzia.Name = "mnuNarzedzia"
        Me.mnuNarzedzia.Size = New System.Drawing.Size(70, 20)
        Me.mnuNarzedzia.Text = "Narzędzia"
        '
        'mnuPolaczZSerwerem
        '
        Me.mnuPolaczZSerwerem.Name = "mnuPolaczZSerwerem"
        Me.mnuPolaczZSerwerem.Size = New System.Drawing.Size(214, 22)
        Me.mnuPolaczZSerwerem.Text = "Połącz z serwerem..."
        '
        'mnuRozlaczZSerwerem
        '
        Me.mnuRozlaczZSerwerem.Enabled = False
        Me.mnuRozlaczZSerwerem.Name = "mnuRozlaczZSerwerem"
        Me.mnuRozlaczZSerwerem.Size = New System.Drawing.Size(214, 22)
        Me.mnuRozlaczZSerwerem.Text = "Rozłącz..."
        '
        'mnuZarzadzajSerwerem
        '
        Me.mnuZarzadzajSerwerem.Name = "mnuZarzadzajSerwerem"
        Me.mnuZarzadzajSerwerem.Size = New System.Drawing.Size(214, 22)
        Me.mnuZarzadzajSerwerem.Text = "Zarządzaj serwerem..."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(211, 6)
        '
        'mnuDodajPociag
        '
        Me.mnuDodajPociag.Enabled = False
        Me.mnuDodajPociag.Name = "mnuDodajPociag"
        Me.mnuDodajPociag.Size = New System.Drawing.Size(214, 22)
        Me.mnuDodajPociag.Text = "Dodaj pociąg..."
        '
        'mnuOswietlenie
        '
        Me.mnuOswietlenie.Enabled = False
        Me.mnuOswietlenie.Name = "mnuOswietlenie"
        Me.mnuOswietlenie.Size = New System.Drawing.Size(214, 22)
        Me.mnuOswietlenie.Text = "Sterowanie oświetleniem..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(211, 6)
        '
        'mnuKonfiguratorStacji
        '
        Me.mnuKonfiguratorStacji.Name = "mnuKonfiguratorStacji"
        Me.mnuKonfiguratorStacji.Size = New System.Drawing.Size(214, 22)
        Me.mnuKonfiguratorStacji.Text = "Konfigurator stacji..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(211, 6)
        '
        'mnuNowePolaczenia
        '
        Me.mnuNowePolaczenia.Name = "mnuNowePolaczenia"
        Me.mnuNowePolaczenia.Size = New System.Drawing.Size(214, 22)
        Me.mnuNowePolaczenia.Text = "Nowy plik połączeń..."
        '
        'mnuOtworzPolaczenia
        '
        Me.mnuOtworzPolaczenia.Name = "mnuOtworzPolaczenia"
        Me.mnuOtworzPolaczenia.Size = New System.Drawing.Size(214, 22)
        Me.mnuOtworzPolaczenia.Text = "Otwórz plik połączeń..."
        '
        'stStan
        '
        Me.stStan.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslStanPolaczenia})
        Me.stStan.Location = New System.Drawing.Point(0, 528)
        Me.stStan.Name = "stStan"
        Me.stStan.Size = New System.Drawing.Size(645, 22)
        Me.stStan.TabIndex = 1
        Me.stStan.Text = "StatusStrip1"
        '
        'tslStanPolaczenia
        '
        Me.tslStanPolaczenia.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tslStanPolaczenia.ForeColor = System.Drawing.Color.Red
        Me.tslStanPolaczenia.Name = "tslStanPolaczenia"
        Me.tslStanPolaczenia.Size = New System.Drawing.Size(95, 17)
        Me.tslStanPolaczenia.Text = "Brak połączenia"
        '
        'plpPulpit
        '
        Me.plpPulpit.AllowDrop = True
        Me.plpPulpit.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.plpPulpit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plpPulpit.Location = New System.Drawing.Point(0, 24)
        Me.plpPulpit.MozliwoscZaznaczeniaLamp = False
        Me.plpPulpit.MozliwoscZaznaczeniaToru = False
        Me.plpPulpit.Name = "plpPulpit"
        Me.plpPulpit.projDodatkoweObiekty = Nastawnia.RysujDodatkoweObiekty.Nic
        Me.plpPulpit.projZaznaczonaLampa = Nothing
        Me.plpPulpit.projZaznaczonyLicznik = Nothing
        Me.plpPulpit.projZaznaczonyOdcinek = Nothing
        Me.plpPulpit.Przesuniecie = New System.Drawing.Point(0, 0)
        Pulpit1.Adres = CType(0US, UShort)
        Pulpit1.Nazwa = ""
        Me.plpPulpit.Pulpit = Pulpit1
        Me.plpPulpit.Size = New System.Drawing.Size(645, 504)
        Me.plpPulpit.TabIndex = 2
        Me.plpPulpit.ZaznaczonaKostka = Nothing
        '
        'wndNastawnia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 550)
        Me.Controls.Add(Me.plpPulpit)
        Me.Controls.Add(Me.stStan)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "wndNastawnia"
        Me.Text = "Nastawnia"
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        Me.stStan.ResumeLayout(False)
        Me.stStan.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mnuMenu As MenuStrip
    Friend WithEvents mnuNarzedzia As ToolStripMenuItem
    Friend WithEvents mnuKonfiguratorStacji As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mnuNowePolaczenia As ToolStripMenuItem
    Friend WithEvents mnuOtworzPolaczenia As ToolStripMenuItem
    Friend WithEvents mnuPolaczZSerwerem As ToolStripMenuItem
    Friend WithEvents mnuZarzadzajSerwerem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents mnuRozlaczZSerwerem As ToolStripMenuItem
    Friend WithEvents stStan As StatusStrip
    Friend WithEvents tslStanPolaczenia As ToolStripStatusLabel
    Friend WithEvents plpPulpit As PulpitSterowniczy
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents mnuDodajPociag As ToolStripMenuItem
    Friend WithEvents mnuOswietlenie As ToolStripMenuItem
End Class
