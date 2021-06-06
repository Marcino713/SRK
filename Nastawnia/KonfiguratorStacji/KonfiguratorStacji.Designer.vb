<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndKonfiguratorStacji
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
        Me.mnuMenu = New System.Windows.Forms.MenuStrip()
        Me.mnuNarzedzia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDodajKostki = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUsunKostki = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuNazwa = New System.Windows.Forms.ToolStripMenuItem()
        Me.pctPulpit = New System.Windows.Forms.PictureBox()
        Me.splOkno = New System.Windows.Forms.SplitContainer()
        Me.pnlPulpit = New System.Windows.Forms.Panel()
        Me.tabUstawienia = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.splKartaPulpit = New System.Windows.Forms.SplitContainer()
        Me.lvPulpitKostki = New System.Windows.Forms.ListView()
        Me.imlKostki = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlKonfRozjazd = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cboKonfRozjazdBok2 = New System.Windows.Forms.ComboBox()
        Me.cboKonfRozjazdBok1 = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbKonfRozjazdBok2Plus = New System.Windows.Forms.RadioButton()
        Me.rbKonfRozjazdBok2Minus = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbKonfRozjazdBok1Plus = New System.Windows.Forms.RadioButton()
        Me.rbKonfRozjazdBok1Minus = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbKonfRozjazdWprost2Plus = New System.Windows.Forms.RadioButton()
        Me.rbKonfRozjazdWprost2Minus = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbKonfRozjazdWprost1Plus = New System.Windows.Forms.RadioButton()
        Me.rbKonfRozjazdWprost1Minus = New System.Windows.Forms.RadioButton()
        Me.cboKonfRozjazdWprost2 = New System.Windows.Forms.ComboBox()
        Me.cboKonfRozjazdWprost1 = New System.Windows.Forms.ComboBox()
        Me.txtKonfRozjazdPredkBoczna = New System.Windows.Forms.TextBox()
        Me.txtKonfRozjazdPredkZasad = New System.Windows.Forms.TextBox()
        Me.txtKonfRozjazdNazwa = New System.Windows.Forms.TextBox()
        Me.txtKonfRozjazdAdres = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlKonfTor = New System.Windows.Forms.Panel()
        Me.txtKonfTorPredkosc = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlKonfSygn = New System.Windows.Forms.Panel()
        Me.pnlKonfSygnSwiatla = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cbKonfSygnPomPas = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnZielPas = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnBiale = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPomDol = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnCzer = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPomGor = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnZiel = New System.Windows.Forms.CheckBox()
        Me.cboKonfSygnOdcinekNast = New System.Windows.Forms.ComboBox()
        Me.txtKonfSygnNazwa = New System.Windows.Forms.TextBox()
        Me.txtKonfSygnAdres = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlKonfPrzycisk = New System.Windows.Forms.Panel()
        Me.cboKonfPrzyciskSygnalizator = New System.Windows.Forms.ComboBox()
        Me.cboKonfPrzyciskTyp = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.splKartaTory = New System.Windows.Forms.SplitContainer()
        Me.lvTory = New System.Windows.Forms.ListView()
        Me.txtTorAdres = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlTorLegenda = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlTorNieprzypisany = New System.Windows.Forms.Panel()
        Me.pnlTorTenOdcinek = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlTorInnyOdcinek = New System.Windows.Forms.Panel()
        Me.btnTorUsun = New System.Windows.Forms.Button()
        Me.txtTorOpis = New System.Windows.Forms.TextBox()
        Me.txtTorNazwa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnTorEdytuj = New System.Windows.Forms.Button()
        Me.btnTorDodaj = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.splLiczniki = New System.Windows.Forms.SplitContainer()
        Me.lvOsie = New System.Windows.Forms.ListView()
        Me.txtLicznikAdres2 = New System.Windows.Forms.TextBox()
        Me.txtLicznikAdres1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboLicznikTor2 = New System.Windows.Forms.ComboBox()
        Me.cboLicznikTor1 = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cboKonfSygnSygnNast = New System.Windows.Forms.ComboBox()
        Me.mnuMenu.SuspendLayout()
        CType(Me.pctPulpit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splOkno, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splOkno.Panel1.SuspendLayout()
        Me.splOkno.Panel2.SuspendLayout()
        Me.splOkno.SuspendLayout()
        Me.pnlPulpit.SuspendLayout()
        Me.tabUstawienia.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.splKartaPulpit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaPulpit.Panel1.SuspendLayout()
        Me.splKartaPulpit.Panel2.SuspendLayout()
        Me.splKartaPulpit.SuspendLayout()
        Me.pnlKonfRozjazd.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlKonfTor.SuspendLayout()
        Me.pnlKonfSygn.SuspendLayout()
        Me.pnlKonfSygnSwiatla.SuspendLayout()
        Me.pnlKonfPrzycisk.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.splKartaTory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaTory.Panel1.SuspendLayout()
        Me.splKartaTory.Panel2.SuspendLayout()
        Me.splKartaTory.SuspendLayout()
        Me.pnlTorLegenda.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.splLiczniki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splLiczniki.Panel1.SuspendLayout()
        Me.splLiczniki.Panel2.SuspendLayout()
        Me.splLiczniki.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMenu
        '
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNarzedzia})
        Me.mnuMenu.Location = New System.Drawing.Point(0, 0)
        Me.mnuMenu.Name = "mnuMenu"
        Me.mnuMenu.Size = New System.Drawing.Size(1006, 24)
        Me.mnuMenu.TabIndex = 0
        Me.mnuMenu.Text = "MenuStrip1"
        '
        'mnuNarzedzia
        '
        Me.mnuNarzedzia.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDodajKostki, Me.mnuUsunKostki, Me.ToolStripSeparator1, Me.mnuNazwa})
        Me.mnuNarzedzia.Name = "mnuNarzedzia"
        Me.mnuNarzedzia.Size = New System.Drawing.Size(70, 20)
        Me.mnuNarzedzia.Text = "Narzędzia"
        '
        'mnuDodajKostki
        '
        Me.mnuDodajKostki.Name = "mnuDodajKostki"
        Me.mnuDodajKostki.Size = New System.Drawing.Size(216, 22)
        Me.mnuDodajKostki.Text = "Dodaj kostki..."
        '
        'mnuUsunKostki
        '
        Me.mnuUsunKostki.Name = "mnuUsunKostki"
        Me.mnuUsunKostki.Size = New System.Drawing.Size(216, 22)
        Me.mnuUsunKostki.Text = "Usuń kostki..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(213, 6)
        '
        'mnuNazwa
        '
        Me.mnuNazwa.Name = "mnuNazwa"
        Me.mnuNazwa.Size = New System.Drawing.Size(216, 22)
        Me.mnuNazwa.Text = "Zmień nazwę posterunku..."
        '
        'pctPulpit
        '
        Me.pctPulpit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctPulpit.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.pctPulpit.Location = New System.Drawing.Point(0, 0)
        Me.pctPulpit.Name = "pctPulpit"
        Me.pctPulpit.Size = New System.Drawing.Size(788, 670)
        Me.pctPulpit.TabIndex = 0
        Me.pctPulpit.TabStop = False
        '
        'splOkno
        '
        Me.splOkno.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splOkno.Location = New System.Drawing.Point(0, 24)
        Me.splOkno.Name = "splOkno"
        '
        'splOkno.Panel1
        '
        Me.splOkno.Panel1.Controls.Add(Me.pnlPulpit)
        '
        'splOkno.Panel2
        '
        Me.splOkno.Panel2.Controls.Add(Me.tabUstawienia)
        Me.splOkno.Size = New System.Drawing.Size(1006, 670)
        Me.splOkno.SplitterDistance = 788
        Me.splOkno.TabIndex = 1
        '
        'pnlPulpit
        '
        Me.pnlPulpit.AllowDrop = True
        Me.pnlPulpit.Controls.Add(Me.pctPulpit)
        Me.pnlPulpit.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.pnlPulpit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPulpit.Location = New System.Drawing.Point(0, 0)
        Me.pnlPulpit.Name = "pnlPulpit"
        Me.pnlPulpit.Size = New System.Drawing.Size(788, 670)
        Me.pnlPulpit.TabIndex = 2
        '
        'tabUstawienia
        '
        Me.tabUstawienia.Controls.Add(Me.TabPage1)
        Me.tabUstawienia.Controls.Add(Me.TabPage2)
        Me.tabUstawienia.Controls.Add(Me.TabPage3)
        Me.tabUstawienia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabUstawienia.Location = New System.Drawing.Point(0, 0)
        Me.tabUstawienia.Name = "tabUstawienia"
        Me.tabUstawienia.SelectedIndex = 0
        Me.tabUstawienia.Size = New System.Drawing.Size(214, 670)
        Me.tabUstawienia.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.splKartaPulpit)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(206, 644)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pulpit"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'splKartaPulpit
        '
        Me.splKartaPulpit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splKartaPulpit.Location = New System.Drawing.Point(3, 3)
        Me.splKartaPulpit.Name = "splKartaPulpit"
        Me.splKartaPulpit.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splKartaPulpit.Panel1
        '
        Me.splKartaPulpit.Panel1.Controls.Add(Me.lvPulpitKostki)
        '
        'splKartaPulpit.Panel2
        '
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygn)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfTor)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfRozjazd)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfPrzycisk)
        Me.splKartaPulpit.Size = New System.Drawing.Size(200, 638)
        Me.splKartaPulpit.SplitterDistance = 319
        Me.splKartaPulpit.TabIndex = 1
        '
        'lvPulpitKostki
        '
        Me.lvPulpitKostki.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvPulpitKostki.HideSelection = False
        Me.lvPulpitKostki.LargeImageList = Me.imlKostki
        Me.lvPulpitKostki.Location = New System.Drawing.Point(0, 0)
        Me.lvPulpitKostki.MultiSelect = False
        Me.lvPulpitKostki.Name = "lvPulpitKostki"
        Me.lvPulpitKostki.Size = New System.Drawing.Size(200, 319)
        Me.lvPulpitKostki.TabIndex = 0
        Me.lvPulpitKostki.UseCompatibleStateImageBehavior = False
        '
        'imlKostki
        '
        Me.imlKostki.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imlKostki.ImageSize = New System.Drawing.Size(48, 48)
        Me.imlKostki.TransparentColor = System.Drawing.Color.Transparent
        '
        'pnlKonfRozjazd
        '
        Me.pnlKonfRozjazd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfRozjazd.Controls.Add(Me.GroupBox3)
        Me.pnlKonfRozjazd.Controls.Add(Me.GroupBox2)
        Me.pnlKonfRozjazd.Controls.Add(Me.txtKonfRozjazdPredkBoczna)
        Me.pnlKonfRozjazd.Controls.Add(Me.txtKonfRozjazdPredkZasad)
        Me.pnlKonfRozjazd.Controls.Add(Me.txtKonfRozjazdNazwa)
        Me.pnlKonfRozjazd.Controls.Add(Me.txtKonfRozjazdAdres)
        Me.pnlKonfRozjazd.Controls.Add(Me.Label16)
        Me.pnlKonfRozjazd.Controls.Add(Me.Label15)
        Me.pnlKonfRozjazd.Controls.Add(Me.Label14)
        Me.pnlKonfRozjazd.Controls.Add(Me.Label13)
        Me.pnlKonfRozjazd.Location = New System.Drawing.Point(43, 34)
        Me.pnlKonfRozjazd.Name = "pnlKonfRozjazd"
        Me.pnlKonfRozjazd.Size = New System.Drawing.Size(260, 337)
        Me.pnlKonfRozjazd.TabIndex = 1
        Me.pnlKonfRozjazd.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.cboKonfRozjazdBok2)
        Me.GroupBox3.Controls.Add(Me.cboKonfRozjazdBok1)
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Controls.Add(Me.Panel3)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 242)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(251, 80)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Jeśli na bok, ustaw rozjazdy:"
        '
        'cboKonfRozjazdBok2
        '
        Me.cboKonfRozjazdBok2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfRozjazdBok2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfRozjazdBok2.FormattingEnabled = True
        Me.cboKonfRozjazdBok2.Location = New System.Drawing.Point(6, 47)
        Me.cboKonfRozjazdBok2.Name = "cboKonfRozjazdBok2"
        Me.cboKonfRozjazdBok2.Size = New System.Drawing.Size(168, 21)
        Me.cboKonfRozjazdBok2.TabIndex = 11
        '
        'cboKonfRozjazdBok1
        '
        Me.cboKonfRozjazdBok1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfRozjazdBok1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfRozjazdBok1.FormattingEnabled = True
        Me.cboKonfRozjazdBok1.Location = New System.Drawing.Point(6, 20)
        Me.cboKonfRozjazdBok1.Name = "cboKonfRozjazdBok1"
        Me.cboKonfRozjazdBok1.Size = New System.Drawing.Size(168, 21)
        Me.cboKonfRozjazdBok1.TabIndex = 10
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.rbKonfRozjazdBok2Plus)
        Me.Panel4.Controls.Add(Me.rbKonfRozjazdBok2Minus)
        Me.Panel4.Location = New System.Drawing.Point(180, 48)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(65, 21)
        Me.Panel4.TabIndex = 9
        '
        'rbKonfRozjazdBok2Plus
        '
        Me.rbKonfRozjazdBok2Plus.AutoSize = True
        Me.rbKonfRozjazdBok2Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdBok2Plus.Name = "rbKonfRozjazdBok2Plus"
        Me.rbKonfRozjazdBok2Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdBok2Plus.TabIndex = 3
        Me.rbKonfRozjazdBok2Plus.TabStop = True
        Me.rbKonfRozjazdBok2Plus.Text = "+"
        Me.rbKonfRozjazdBok2Plus.UseVisualStyleBackColor = True
        '
        'rbKonfRozjazdBok2Minus
        '
        Me.rbKonfRozjazdBok2Minus.AutoSize = True
        Me.rbKonfRozjazdBok2Minus.Location = New System.Drawing.Point(37, 0)
        Me.rbKonfRozjazdBok2Minus.Name = "rbKonfRozjazdBok2Minus"
        Me.rbKonfRozjazdBok2Minus.Size = New System.Drawing.Size(28, 17)
        Me.rbKonfRozjazdBok2Minus.TabIndex = 4
        Me.rbKonfRozjazdBok2Minus.TabStop = True
        Me.rbKonfRozjazdBok2Minus.Text = "-"
        Me.rbKonfRozjazdBok2Minus.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.rbKonfRozjazdBok1Plus)
        Me.Panel3.Controls.Add(Me.rbKonfRozjazdBok1Minus)
        Me.Panel3.Location = New System.Drawing.Point(180, 21)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(65, 21)
        Me.Panel3.TabIndex = 9
        '
        'rbKonfRozjazdBok1Plus
        '
        Me.rbKonfRozjazdBok1Plus.AutoSize = True
        Me.rbKonfRozjazdBok1Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdBok1Plus.Name = "rbKonfRozjazdBok1Plus"
        Me.rbKonfRozjazdBok1Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdBok1Plus.TabIndex = 3
        Me.rbKonfRozjazdBok1Plus.TabStop = True
        Me.rbKonfRozjazdBok1Plus.Text = "+"
        Me.rbKonfRozjazdBok1Plus.UseVisualStyleBackColor = True
        '
        'rbKonfRozjazdBok1Minus
        '
        Me.rbKonfRozjazdBok1Minus.AutoSize = True
        Me.rbKonfRozjazdBok1Minus.Location = New System.Drawing.Point(37, 0)
        Me.rbKonfRozjazdBok1Minus.Name = "rbKonfRozjazdBok1Minus"
        Me.rbKonfRozjazdBok1Minus.Size = New System.Drawing.Size(28, 17)
        Me.rbKonfRozjazdBok1Minus.TabIndex = 4
        Me.rbKonfRozjazdBok1Minus.TabStop = True
        Me.rbKonfRozjazdBok1Minus.Text = "-"
        Me.rbKonfRozjazdBok1Minus.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Panel2)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.cboKonfRozjazdWprost2)
        Me.GroupBox2.Controls.Add(Me.cboKonfRozjazdWprost1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 159)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(251, 77)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Jeśli na wprost, ustaw rozjazdy:"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.rbKonfRozjazdWprost2Plus)
        Me.Panel2.Controls.Add(Me.rbKonfRozjazdWprost2Minus)
        Me.Panel2.Location = New System.Drawing.Point(180, 46)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(65, 21)
        Me.Panel2.TabIndex = 9
        '
        'rbKonfRozjazdWprost2Plus
        '
        Me.rbKonfRozjazdWprost2Plus.AutoSize = True
        Me.rbKonfRozjazdWprost2Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdWprost2Plus.Name = "rbKonfRozjazdWprost2Plus"
        Me.rbKonfRozjazdWprost2Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdWprost2Plus.TabIndex = 3
        Me.rbKonfRozjazdWprost2Plus.TabStop = True
        Me.rbKonfRozjazdWprost2Plus.Text = "+"
        Me.rbKonfRozjazdWprost2Plus.UseVisualStyleBackColor = True
        '
        'rbKonfRozjazdWprost2Minus
        '
        Me.rbKonfRozjazdWprost2Minus.AutoSize = True
        Me.rbKonfRozjazdWprost2Minus.Location = New System.Drawing.Point(37, 0)
        Me.rbKonfRozjazdWprost2Minus.Name = "rbKonfRozjazdWprost2Minus"
        Me.rbKonfRozjazdWprost2Minus.Size = New System.Drawing.Size(28, 17)
        Me.rbKonfRozjazdWprost2Minus.TabIndex = 4
        Me.rbKonfRozjazdWprost2Minus.TabStop = True
        Me.rbKonfRozjazdWprost2Minus.Text = "-"
        Me.rbKonfRozjazdWprost2Minus.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rbKonfRozjazdWprost1Plus)
        Me.Panel1.Controls.Add(Me.rbKonfRozjazdWprost1Minus)
        Me.Panel1.Location = New System.Drawing.Point(180, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(65, 21)
        Me.Panel1.TabIndex = 8
        '
        'rbKonfRozjazdWprost1Plus
        '
        Me.rbKonfRozjazdWprost1Plus.AutoSize = True
        Me.rbKonfRozjazdWprost1Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdWprost1Plus.Name = "rbKonfRozjazdWprost1Plus"
        Me.rbKonfRozjazdWprost1Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdWprost1Plus.TabIndex = 3
        Me.rbKonfRozjazdWprost1Plus.TabStop = True
        Me.rbKonfRozjazdWprost1Plus.Text = "+"
        Me.rbKonfRozjazdWprost1Plus.UseVisualStyleBackColor = True
        '
        'rbKonfRozjazdWprost1Minus
        '
        Me.rbKonfRozjazdWprost1Minus.AutoSize = True
        Me.rbKonfRozjazdWprost1Minus.Location = New System.Drawing.Point(37, 0)
        Me.rbKonfRozjazdWprost1Minus.Name = "rbKonfRozjazdWprost1Minus"
        Me.rbKonfRozjazdWprost1Minus.Size = New System.Drawing.Size(28, 17)
        Me.rbKonfRozjazdWprost1Minus.TabIndex = 4
        Me.rbKonfRozjazdWprost1Minus.TabStop = True
        Me.rbKonfRozjazdWprost1Minus.Text = "-"
        Me.rbKonfRozjazdWprost1Minus.UseVisualStyleBackColor = True
        '
        'cboKonfRozjazdWprost2
        '
        Me.cboKonfRozjazdWprost2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfRozjazdWprost2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfRozjazdWprost2.FormattingEnabled = True
        Me.cboKonfRozjazdWprost2.Location = New System.Drawing.Point(6, 45)
        Me.cboKonfRozjazdWprost2.Name = "cboKonfRozjazdWprost2"
        Me.cboKonfRozjazdWprost2.Size = New System.Drawing.Size(168, 21)
        Me.cboKonfRozjazdWprost2.TabIndex = 5
        '
        'cboKonfRozjazdWprost1
        '
        Me.cboKonfRozjazdWprost1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfRozjazdWprost1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfRozjazdWprost1.FormattingEnabled = True
        Me.cboKonfRozjazdWprost1.Location = New System.Drawing.Point(6, 18)
        Me.cboKonfRozjazdWprost1.Name = "cboKonfRozjazdWprost1"
        Me.cboKonfRozjazdWprost1.Size = New System.Drawing.Size(168, 21)
        Me.cboKonfRozjazdWprost1.TabIndex = 2
        '
        'txtKonfRozjazdPredkBoczna
        '
        Me.txtKonfRozjazdPredkBoczna.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfRozjazdPredkBoczna.Location = New System.Drawing.Point(6, 133)
        Me.txtKonfRozjazdPredkBoczna.Name = "txtKonfRozjazdPredkBoczna"
        Me.txtKonfRozjazdPredkBoczna.Size = New System.Drawing.Size(251, 20)
        Me.txtKonfRozjazdPredkBoczna.TabIndex = 7
        '
        'txtKonfRozjazdPredkZasad
        '
        Me.txtKonfRozjazdPredkZasad.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfRozjazdPredkZasad.Location = New System.Drawing.Point(6, 94)
        Me.txtKonfRozjazdPredkZasad.Name = "txtKonfRozjazdPredkZasad"
        Me.txtKonfRozjazdPredkZasad.Size = New System.Drawing.Size(251, 20)
        Me.txtKonfRozjazdPredkZasad.TabIndex = 6
        '
        'txtKonfRozjazdNazwa
        '
        Me.txtKonfRozjazdNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfRozjazdNazwa.Location = New System.Drawing.Point(6, 55)
        Me.txtKonfRozjazdNazwa.Name = "txtKonfRozjazdNazwa"
        Me.txtKonfRozjazdNazwa.Size = New System.Drawing.Size(251, 20)
        Me.txtKonfRozjazdNazwa.TabIndex = 5
        '
        'txtKonfRozjazdAdres
        '
        Me.txtKonfRozjazdAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfRozjazdAdres.Location = New System.Drawing.Point(6, 16)
        Me.txtKonfRozjazdAdres.Name = "txtKonfRozjazdAdres"
        Me.txtKonfRozjazdAdres.Size = New System.Drawing.Size(251, 20)
        Me.txtKonfRozjazdAdres.TabIndex = 4
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 117)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(155, 13)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Prędkość w kierunku bocznym:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 78)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(173, 13)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Prędkość w kierunku zasadniczym:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(3, 39)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(43, 13)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Nazwa:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(37, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Adres:"
        '
        'pnlKonfTor
        '
        Me.pnlKonfTor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfTor.Controls.Add(Me.txtKonfTorPredkosc)
        Me.pnlKonfTor.Controls.Add(Me.Label12)
        Me.pnlKonfTor.Location = New System.Drawing.Point(93, 3)
        Me.pnlKonfTor.Name = "pnlKonfTor"
        Me.pnlKonfTor.Size = New System.Drawing.Size(170, 52)
        Me.pnlKonfTor.TabIndex = 0
        Me.pnlKonfTor.Visible = False
        '
        'txtKonfTorPredkosc
        '
        Me.txtKonfTorPredkosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorPredkosc.BackColor = System.Drawing.Color.White
        Me.txtKonfTorPredkosc.Location = New System.Drawing.Point(6, 16)
        Me.txtKonfTorPredkosc.Name = "txtKonfTorPredkosc"
        Me.txtKonfTorPredkosc.Size = New System.Drawing.Size(161, 20)
        Me.txtKonfTorPredkosc.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(116, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Prędkość maksymalna:"
        '
        'pnlKonfSygn
        '
        Me.pnlKonfSygn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygn.Controls.Add(Me.cboKonfSygnSygnNast)
        Me.pnlKonfSygn.Controls.Add(Me.Label23)
        Me.pnlKonfSygn.Controls.Add(Me.pnlKonfSygnSwiatla)
        Me.pnlKonfSygn.Controls.Add(Me.cboKonfSygnOdcinekNast)
        Me.pnlKonfSygn.Controls.Add(Me.txtKonfSygnNazwa)
        Me.pnlKonfSygn.Controls.Add(Me.txtKonfSygnAdres)
        Me.pnlKonfSygn.Controls.Add(Me.Label19)
        Me.pnlKonfSygn.Controls.Add(Me.Label18)
        Me.pnlKonfSygn.Controls.Add(Me.Label17)
        Me.pnlKonfSygn.Location = New System.Drawing.Point(10, 61)
        Me.pnlKonfSygn.Name = "pnlKonfSygn"
        Me.pnlKonfSygn.Size = New System.Drawing.Size(202, 355)
        Me.pnlKonfSygn.TabIndex = 1
        Me.pnlKonfSygn.Visible = False
        '
        'pnlKonfSygnSwiatla
        '
        Me.pnlKonfSygnSwiatla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.Label20)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnPomPas)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnZielPas)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnBiale)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnPomDol)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnCzer)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnPomGor)
        Me.pnlKonfSygnSwiatla.Controls.Add(Me.cbKonfSygnZiel)
        Me.pnlKonfSygnSwiatla.Location = New System.Drawing.Point(0, 161)
        Me.pnlKonfSygnSwiatla.Name = "pnlKonfSygnSwiatla"
        Me.pnlKonfSygnSwiatla.Size = New System.Drawing.Size(202, 186)
        Me.pnlKonfSygnSwiatla.TabIndex = 6
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(3, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(93, 13)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "Dostępne światła:"
        '
        'cbKonfSygnPomPas
        '
        Me.cbKonfSygnPomPas.AutoSize = True
        Me.cbKonfSygnPomPas.Location = New System.Drawing.Point(6, 154)
        Me.cbKonfSygnPomPas.Name = "cbKonfSygnPomPas"
        Me.cbKonfSygnPomPas.Size = New System.Drawing.Size(118, 17)
        Me.cbKonfSygnPomPas.TabIndex = 6
        Me.cbKonfSygnPomPas.Text = "Pomarańczowy pas"
        Me.cbKonfSygnPomPas.UseVisualStyleBackColor = True
        '
        'cbKonfSygnZielPas
        '
        Me.cbKonfSygnZielPas.AutoSize = True
        Me.cbKonfSygnZielPas.Location = New System.Drawing.Point(6, 131)
        Me.cbKonfSygnZielPas.Name = "cbKonfSygnZielPas"
        Me.cbKonfSygnZielPas.Size = New System.Drawing.Size(80, 17)
        Me.cbKonfSygnZielPas.TabIndex = 5
        Me.cbKonfSygnZielPas.Text = "Zielony pas"
        Me.cbKonfSygnZielPas.UseVisualStyleBackColor = True
        '
        'cbKonfSygnBiale
        '
        Me.cbKonfSygnBiale.AutoSize = True
        Me.cbKonfSygnBiale.Location = New System.Drawing.Point(6, 108)
        Me.cbKonfSygnBiale.Name = "cbKonfSygnBiale"
        Me.cbKonfSygnBiale.Size = New System.Drawing.Size(51, 17)
        Me.cbKonfSygnBiale.TabIndex = 4
        Me.cbKonfSygnBiale.Text = "Białe"
        Me.cbKonfSygnBiale.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPomDol
        '
        Me.cbKonfSygnPomDol.AutoSize = True
        Me.cbKonfSygnPomDol.Location = New System.Drawing.Point(6, 85)
        Me.cbKonfSygnPomDol.Name = "cbKonfSygnPomDol"
        Me.cbKonfSygnPomDol.Size = New System.Drawing.Size(128, 17)
        Me.cbKonfSygnPomDol.TabIndex = 3
        Me.cbKonfSygnPomDol.Text = "Pomarańczowe dolne"
        Me.cbKonfSygnPomDol.UseVisualStyleBackColor = True
        '
        'cbKonfSygnCzer
        '
        Me.cbKonfSygnCzer.AutoSize = True
        Me.cbKonfSygnCzer.Location = New System.Drawing.Point(6, 62)
        Me.cbKonfSygnCzer.Name = "cbKonfSygnCzer"
        Me.cbKonfSygnCzer.Size = New System.Drawing.Size(73, 17)
        Me.cbKonfSygnCzer.TabIndex = 2
        Me.cbKonfSygnCzer.Text = "Czerwone"
        Me.cbKonfSygnCzer.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPomGor
        '
        Me.cbKonfSygnPomGor.AutoSize = True
        Me.cbKonfSygnPomGor.Location = New System.Drawing.Point(6, 39)
        Me.cbKonfSygnPomGor.Name = "cbKonfSygnPomGor"
        Me.cbKonfSygnPomGor.Size = New System.Drawing.Size(129, 17)
        Me.cbKonfSygnPomGor.TabIndex = 1
        Me.cbKonfSygnPomGor.Text = "Pomarańczowe górne"
        Me.cbKonfSygnPomGor.UseVisualStyleBackColor = True
        '
        'cbKonfSygnZiel
        '
        Me.cbKonfSygnZiel.AutoSize = True
        Me.cbKonfSygnZiel.Location = New System.Drawing.Point(6, 16)
        Me.cbKonfSygnZiel.Name = "cbKonfSygnZiel"
        Me.cbKonfSygnZiel.Size = New System.Drawing.Size(61, 17)
        Me.cbKonfSygnZiel.TabIndex = 0
        Me.cbKonfSygnZiel.Text = "Zielone"
        Me.cbKonfSygnZiel.UseVisualStyleBackColor = True
        '
        'cboKonfSygnOdcinekNast
        '
        Me.cboKonfSygnOdcinekNast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfSygnOdcinekNast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfSygnOdcinekNast.FormattingEnabled = True
        Me.cboKonfSygnOdcinekNast.Location = New System.Drawing.Point(6, 94)
        Me.cboKonfSygnOdcinekNast.Name = "cboKonfSygnOdcinekNast"
        Me.cboKonfSygnOdcinekNast.Size = New System.Drawing.Size(193, 21)
        Me.cboKonfSygnOdcinekNast.TabIndex = 5
        '
        'txtKonfSygnNazwa
        '
        Me.txtKonfSygnNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfSygnNazwa.Location = New System.Drawing.Point(6, 55)
        Me.txtKonfSygnNazwa.Name = "txtKonfSygnNazwa"
        Me.txtKonfSygnNazwa.Size = New System.Drawing.Size(193, 20)
        Me.txtKonfSygnNazwa.TabIndex = 4
        '
        'txtKonfSygnAdres
        '
        Me.txtKonfSygnAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfSygnAdres.Location = New System.Drawing.Point(6, 16)
        Me.txtKonfSygnAdres.Name = "txtKonfSygnAdres"
        Me.txtKonfSygnAdres.Size = New System.Drawing.Size(193, 20)
        Me.txtKonfSygnAdres.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 78)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(131, 13)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "Odcinek toru następujący:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 39)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 13)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "Nazwa:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(37, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Adres:"
        '
        'pnlKonfPrzycisk
        '
        Me.pnlKonfPrzycisk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfPrzycisk.Controls.Add(Me.cboKonfPrzyciskSygnalizator)
        Me.pnlKonfPrzycisk.Controls.Add(Me.cboKonfPrzyciskTyp)
        Me.pnlKonfPrzycisk.Controls.Add(Me.Label22)
        Me.pnlKonfPrzycisk.Controls.Add(Me.Label21)
        Me.pnlKonfPrzycisk.Location = New System.Drawing.Point(78, 18)
        Me.pnlKonfPrzycisk.Name = "pnlKonfPrzycisk"
        Me.pnlKonfPrzycisk.Size = New System.Drawing.Size(185, 105)
        Me.pnlKonfPrzycisk.TabIndex = 1
        Me.pnlKonfPrzycisk.Visible = False
        '
        'cboKonfPrzyciskSygnalizator
        '
        Me.cboKonfPrzyciskSygnalizator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfPrzyciskSygnalizator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfPrzyciskSygnalizator.FormattingEnabled = True
        Me.cboKonfPrzyciskSygnalizator.Location = New System.Drawing.Point(6, 56)
        Me.cboKonfPrzyciskSygnalizator.Name = "cboKonfPrzyciskSygnalizator"
        Me.cboKonfPrzyciskSygnalizator.Size = New System.Drawing.Size(176, 21)
        Me.cboKonfPrzyciskSygnalizator.TabIndex = 5
        '
        'cboKonfPrzyciskTyp
        '
        Me.cboKonfPrzyciskTyp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfPrzyciskTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfPrzyciskTyp.FormattingEnabled = True
        Me.cboKonfPrzyciskTyp.Location = New System.Drawing.Point(6, 16)
        Me.cboKonfPrzyciskTyp.Name = "cboKonfPrzyciskTyp"
        Me.cboKonfPrzyciskTyp.Size = New System.Drawing.Size(176, 21)
        Me.cboKonfPrzyciskTyp.TabIndex = 4
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(3, 40)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(119, 13)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "Powiązany sygnalizator:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(3, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(75, 13)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Typ przycisku:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.splKartaTory)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(206, 644)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Odcinki torów"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'splKartaTory
        '
        Me.splKartaTory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splKartaTory.Location = New System.Drawing.Point(3, 3)
        Me.splKartaTory.Name = "splKartaTory"
        Me.splKartaTory.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splKartaTory.Panel1
        '
        Me.splKartaTory.Panel1.Controls.Add(Me.lvTory)
        '
        'splKartaTory.Panel2
        '
        Me.splKartaTory.Panel2.Controls.Add(Me.txtTorAdres)
        Me.splKartaTory.Panel2.Controls.Add(Me.Label7)
        Me.splKartaTory.Panel2.Controls.Add(Me.pnlTorLegenda)
        Me.splKartaTory.Panel2.Controls.Add(Me.btnTorUsun)
        Me.splKartaTory.Panel2.Controls.Add(Me.txtTorOpis)
        Me.splKartaTory.Panel2.Controls.Add(Me.txtTorNazwa)
        Me.splKartaTory.Panel2.Controls.Add(Me.Label2)
        Me.splKartaTory.Panel2.Controls.Add(Me.Label1)
        Me.splKartaTory.Panel2.Controls.Add(Me.btnTorEdytuj)
        Me.splKartaTory.Panel2.Controls.Add(Me.btnTorDodaj)
        Me.splKartaTory.Size = New System.Drawing.Size(200, 638)
        Me.splKartaTory.SplitterDistance = 191
        Me.splKartaTory.TabIndex = 0
        '
        'lvTory
        '
        Me.lvTory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTory.Location = New System.Drawing.Point(0, 0)
        Me.lvTory.Name = "lvTory"
        Me.lvTory.Size = New System.Drawing.Size(200, 191)
        Me.lvTory.TabIndex = 0
        Me.lvTory.UseCompatibleStateImageBehavior = False
        '
        'txtTorAdres
        '
        Me.txtTorAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTorAdres.Location = New System.Drawing.Point(3, 45)
        Me.txtTorAdres.Name = "txtTorAdres"
        Me.txtTorAdres.Size = New System.Drawing.Size(192, 20)
        Me.txtTorAdres.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(0, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Adres punktu zasilania:"
        '
        'pnlTorLegenda
        '
        Me.pnlTorLegenda.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTorLegenda.Controls.Add(Me.Label4)
        Me.pnlTorLegenda.Controls.Add(Me.Label6)
        Me.pnlTorLegenda.Controls.Add(Me.Label3)
        Me.pnlTorLegenda.Controls.Add(Me.pnlTorNieprzypisany)
        Me.pnlTorLegenda.Controls.Add(Me.pnlTorTenOdcinek)
        Me.pnlTorLegenda.Controls.Add(Me.Label5)
        Me.pnlTorLegenda.Controls.Add(Me.pnlTorInnyOdcinek)
        Me.pnlTorLegenda.Location = New System.Drawing.Point(3, 195)
        Me.pnlTorLegenda.Name = "pnlTorLegenda"
        Me.pnlTorLegenda.Size = New System.Drawing.Size(192, 100)
        Me.pnlTorLegenda.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(155, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Tor przypisany do tego odcinka"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Tor nieprzypisany"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Legenda"
        '
        'pnlTorNieprzypisany
        '
        Me.pnlTorNieprzypisany.BackColor = System.Drawing.Color.Red
        Me.pnlTorNieprzypisany.Location = New System.Drawing.Point(4, 55)
        Me.pnlTorNieprzypisany.Name = "pnlTorNieprzypisany"
        Me.pnlTorNieprzypisany.Size = New System.Drawing.Size(13, 13)
        Me.pnlTorNieprzypisany.TabIndex = 12
        '
        'pnlTorTenOdcinek
        '
        Me.pnlTorTenOdcinek.BackColor = System.Drawing.Color.Red
        Me.pnlTorTenOdcinek.Location = New System.Drawing.Point(4, 17)
        Me.pnlTorTenOdcinek.Name = "pnlTorTenOdcinek"
        Me.pnlTorTenOdcinek.Size = New System.Drawing.Size(13, 13)
        Me.pnlTorTenOdcinek.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(166, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Tor przypisany do innego odcinka"
        '
        'pnlTorInnyOdcinek
        '
        Me.pnlTorInnyOdcinek.BackColor = System.Drawing.Color.Red
        Me.pnlTorInnyOdcinek.Location = New System.Drawing.Point(4, 36)
        Me.pnlTorInnyOdcinek.Name = "pnlTorInnyOdcinek"
        Me.pnlTorInnyOdcinek.Size = New System.Drawing.Size(13, 13)
        Me.pnlTorInnyOdcinek.TabIndex = 10
        '
        'btnTorUsun
        '
        Me.btnTorUsun.Location = New System.Drawing.Point(135, 3)
        Me.btnTorUsun.Name = "btnTorUsun"
        Me.btnTorUsun.Size = New System.Drawing.Size(60, 23)
        Me.btnTorUsun.TabIndex = 2
        Me.btnTorUsun.Text = "Usuń"
        Me.btnTorUsun.UseVisualStyleBackColor = True
        '
        'txtTorOpis
        '
        Me.txtTorOpis.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTorOpis.Location = New System.Drawing.Point(3, 123)
        Me.txtTorOpis.Multiline = True
        Me.txtTorOpis.Name = "txtTorOpis"
        Me.txtTorOpis.Size = New System.Drawing.Size(192, 66)
        Me.txtTorOpis.TabIndex = 5
        '
        'txtTorNazwa
        '
        Me.txtTorNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTorNazwa.Location = New System.Drawing.Point(3, 84)
        Me.txtTorNazwa.Name = "txtTorNazwa"
        Me.txtTorNazwa.Size = New System.Drawing.Size(192, 20)
        Me.txtTorNazwa.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Opis:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Nazwa:"
        '
        'btnTorEdytuj
        '
        Me.btnTorEdytuj.Location = New System.Drawing.Point(69, 3)
        Me.btnTorEdytuj.Name = "btnTorEdytuj"
        Me.btnTorEdytuj.Size = New System.Drawing.Size(60, 23)
        Me.btnTorEdytuj.TabIndex = 1
        Me.btnTorEdytuj.Text = "Edytuj"
        Me.btnTorEdytuj.UseVisualStyleBackColor = True
        '
        'btnTorDodaj
        '
        Me.btnTorDodaj.Location = New System.Drawing.Point(3, 3)
        Me.btnTorDodaj.Name = "btnTorDodaj"
        Me.btnTorDodaj.Size = New System.Drawing.Size(60, 23)
        Me.btnTorDodaj.TabIndex = 0
        Me.btnTorDodaj.Text = "Dodaj"
        Me.btnTorDodaj.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.splLiczniki)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(206, 644)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Liczniki osi"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'splLiczniki
        '
        Me.splLiczniki.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splLiczniki.Location = New System.Drawing.Point(0, 0)
        Me.splLiczniki.Name = "splLiczniki"
        Me.splLiczniki.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splLiczniki.Panel1
        '
        Me.splLiczniki.Panel1.Controls.Add(Me.lvOsie)
        '
        'splLiczniki.Panel2
        '
        Me.splLiczniki.Panel2.Controls.Add(Me.txtLicznikAdres2)
        Me.splLiczniki.Panel2.Controls.Add(Me.txtLicznikAdres1)
        Me.splLiczniki.Panel2.Controls.Add(Me.GroupBox1)
        Me.splLiczniki.Panel2.Controls.Add(Me.Label9)
        Me.splLiczniki.Panel2.Controls.Add(Me.Label8)
        Me.splLiczniki.Size = New System.Drawing.Size(206, 644)
        Me.splLiczniki.SplitterDistance = 260
        Me.splLiczniki.TabIndex = 0
        '
        'lvOsie
        '
        Me.lvOsie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvOsie.Location = New System.Drawing.Point(0, 0)
        Me.lvOsie.Name = "lvOsie"
        Me.lvOsie.Size = New System.Drawing.Size(206, 260)
        Me.lvOsie.TabIndex = 0
        Me.lvOsie.UseCompatibleStateImageBehavior = False
        '
        'txtLicznikAdres2
        '
        Me.txtLicznikAdres2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLicznikAdres2.Location = New System.Drawing.Point(6, 55)
        Me.txtLicznikAdres2.Name = "txtLicznikAdres2"
        Me.txtLicznikAdres2.Size = New System.Drawing.Size(192, 20)
        Me.txtLicznikAdres2.TabIndex = 6
        '
        'txtLicznikAdres1
        '
        Me.txtLicznikAdres1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLicznikAdres1.Location = New System.Drawing.Point(6, 16)
        Me.txtLicznikAdres1.Name = "txtLicznikAdres1"
        Me.txtLicznikAdres1.Size = New System.Drawing.Size(192, 20)
        Me.txtLicznikAdres1.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboLicznikTor2)
        Me.GroupBox1.Controls.Add(Me.cboLicznikTor1)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(192, 107)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Jeśli przejazd od czujnika 1 do 2"
        '
        'cboLicznikTor2
        '
        Me.cboLicznikTor2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboLicznikTor2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLicznikTor2.FormattingEnabled = True
        Me.cboLicznikTor2.Location = New System.Drawing.Point(9, 72)
        Me.cboLicznikTor2.Name = "cboLicznikTor2"
        Me.cboLicznikTor2.Size = New System.Drawing.Size(177, 21)
        Me.cboLicznikTor2.TabIndex = 1
        '
        'cboLicznikTor1
        '
        Me.cboLicznikTor1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboLicznikTor1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLicznikTor1.FormattingEnabled = True
        Me.cboLicznikTor1.Location = New System.Drawing.Point(9, 32)
        Me.cboLicznikTor1.Name = "cboLicznikTor1"
        Me.cboLicznikTor1.Size = New System.Drawing.Size(177, 21)
        Me.cboLicznikTor1.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 13)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Zwiększ licznik:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 13)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Zmniejsz licznik:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Adres licznika 2"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Adres licznika 1"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(3, 118)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(127, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Sygnalizator następujący:"
        '
        'cboKonfSygnSygnNast
        '
        Me.cboKonfSygnSygnNast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfSygnSygnNast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfSygnSygnNast.FormattingEnabled = True
        Me.cboKonfSygnSygnNast.Location = New System.Drawing.Point(6, 134)
        Me.cboKonfSygnSygnNast.Name = "cboKonfSygnSygnNast"
        Me.cboKonfSygnSygnNast.Size = New System.Drawing.Size(193, 21)
        Me.cboKonfSygnSygnNast.TabIndex = 8
        '
        'wndKonfiguratorStacji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 694)
        Me.Controls.Add(Me.splOkno)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "wndKonfiguratorStacji"
        Me.Text = "Konfigurator stacji"
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        CType(Me.pctPulpit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splOkno.Panel1.ResumeLayout(False)
        Me.splOkno.Panel2.ResumeLayout(False)
        CType(Me.splOkno, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splOkno.ResumeLayout(False)
        Me.pnlPulpit.ResumeLayout(False)
        Me.tabUstawienia.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.splKartaPulpit.Panel1.ResumeLayout(False)
        Me.splKartaPulpit.Panel2.ResumeLayout(False)
        CType(Me.splKartaPulpit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaPulpit.ResumeLayout(False)
        Me.pnlKonfRozjazd.ResumeLayout(False)
        Me.pnlKonfRozjazd.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlKonfTor.ResumeLayout(False)
        Me.pnlKonfTor.PerformLayout()
        Me.pnlKonfSygn.ResumeLayout(False)
        Me.pnlKonfSygn.PerformLayout()
        Me.pnlKonfSygnSwiatla.ResumeLayout(False)
        Me.pnlKonfSygnSwiatla.PerformLayout()
        Me.pnlKonfPrzycisk.ResumeLayout(False)
        Me.pnlKonfPrzycisk.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.splKartaTory.Panel1.ResumeLayout(False)
        Me.splKartaTory.Panel2.ResumeLayout(False)
        Me.splKartaTory.Panel2.PerformLayout()
        CType(Me.splKartaTory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaTory.ResumeLayout(False)
        Me.pnlTorLegenda.ResumeLayout(False)
        Me.pnlTorLegenda.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.splLiczniki.Panel1.ResumeLayout(False)
        Me.splLiczniki.Panel2.ResumeLayout(False)
        Me.splLiczniki.Panel2.PerformLayout()
        CType(Me.splLiczniki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splLiczniki.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mnuMenu As MenuStrip
    Friend WithEvents mnuNarzedzia As ToolStripMenuItem
    Friend WithEvents mnuDodajKostki As ToolStripMenuItem
    Friend WithEvents mnuUsunKostki As ToolStripMenuItem
    Friend WithEvents pctPulpit As PictureBox
    Friend WithEvents splOkno As SplitContainer
    Friend WithEvents pnlPulpit As Panel
    Friend WithEvents lvPulpitKostki As ListView
    Friend WithEvents imlKostki As ImageList
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mnuNazwa As ToolStripMenuItem
    Friend WithEvents tabUstawienia As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents splKartaPulpit As SplitContainer
    Friend WithEvents splKartaTory As SplitContainer
    Friend WithEvents lvTory As ListView
    Friend WithEvents Label3 As Label
    Friend WithEvents txtTorOpis As TextBox
    Friend WithEvents txtTorNazwa As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnTorEdytuj As Button
    Friend WithEvents btnTorDodaj As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents pnlTorTenOdcinek As Panel
    Friend WithEvents btnTorUsun As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents pnlTorNieprzypisany As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents pnlTorInnyOdcinek As Panel
    Friend WithEvents pnlTorLegenda As Panel
    Friend WithEvents txtTorAdres As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents splLiczniki As SplitContainer
    Friend WithEvents lvOsie As ListView
    Friend WithEvents txtLicznikAdres2 As TextBox
    Friend WithEvents txtLicznikAdres1 As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cboLicznikTor2 As ComboBox
    Friend WithEvents cboLicznikTor1 As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents pnlKonfTor As Panel
    Friend WithEvents txtKonfTorPredkosc As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents pnlKonfRozjazd As Panel
    Friend WithEvents txtKonfRozjazdPredkBoczna As TextBox
    Friend WithEvents txtKonfRozjazdPredkZasad As TextBox
    Friend WithEvents txtKonfRozjazdNazwa As TextBox
    Friend WithEvents txtKonfRozjazdAdres As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents pnlKonfSygn As Panel
    Friend WithEvents pnlKonfSygnSwiatla As Panel
    Friend WithEvents Label20 As Label
    Friend WithEvents cbKonfSygnPomPas As CheckBox
    Friend WithEvents cbKonfSygnZielPas As CheckBox
    Friend WithEvents cbKonfSygnBiale As CheckBox
    Friend WithEvents cbKonfSygnPomDol As CheckBox
    Friend WithEvents cbKonfSygnCzer As CheckBox
    Friend WithEvents cbKonfSygnPomGor As CheckBox
    Friend WithEvents cbKonfSygnZiel As CheckBox
    Friend WithEvents cboKonfSygnOdcinekNast As ComboBox
    Friend WithEvents txtKonfSygnNazwa As TextBox
    Friend WithEvents txtKonfSygnAdres As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents pnlKonfPrzycisk As Panel
    Friend WithEvents cboKonfPrzyciskTyp As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents cboKonfPrzyciskSygnalizator As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents cboKonfRozjazdBok2 As ComboBox
    Friend WithEvents cboKonfRozjazdBok1 As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents rbKonfRozjazdBok2Plus As RadioButton
    Friend WithEvents rbKonfRozjazdBok2Minus As RadioButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents rbKonfRozjazdBok1Plus As RadioButton
    Friend WithEvents rbKonfRozjazdBok1Minus As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents rbKonfRozjazdWprost2Plus As RadioButton
    Friend WithEvents rbKonfRozjazdWprost2Minus As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents rbKonfRozjazdWprost1Plus As RadioButton
    Friend WithEvents rbKonfRozjazdWprost1Minus As RadioButton
    Friend WithEvents cboKonfRozjazdWprost2 As ComboBox
    Friend WithEvents cboKonfRozjazdWprost1 As ComboBox
    Friend WithEvents cboKonfSygnSygnNast As ComboBox
    Friend WithEvents Label23 As Label
End Class
