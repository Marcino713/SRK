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
        Me.splKartaPulpit.SuspendLayout()
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
        CType(Me.splKartaPulpit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaPulpit.ResumeLayout(False)
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
End Class
