﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wndProjektantPosterunku
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
        Me.components = New System.ComponentModel.Container()
        Dim Pulpit1 As Zaleznosci.Pulpit = New Zaleznosci.Pulpit()
        Me.mnuMenu = New System.Windows.Forms.MenuStrip()
        Me.mnuNarzedzia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNowy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOtworz = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZapisz = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuZapiszJako = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDodajKostki = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUsunKostki = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDanePosterunku = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPredkosciTorow = New System.Windows.Forms.ToolStripMenuItem()
        Me.splOkno = New System.Windows.Forms.SplitContainer()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.tabUstawienia = New System.Windows.Forms.TabControl()
        Me.tbpPulpit = New System.Windows.Forms.TabPage()
        Me.splKartaPulpit = New System.Windows.Forms.SplitContainer()
        Me.lvPulpitKostki = New System.Windows.Forms.ListView()
        Me.imlKostki = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlKonfSygnInfSygnPowtarzany = New System.Windows.Forms.Panel()
        Me.cboKonfSygnInfSygnPowtarzany = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.pnlKonfSygnPowtKolejnosc = New System.Windows.Forms.Panel()
        Me.rbKonfSygnPowtKolejnoscIII = New System.Windows.Forms.RadioButton()
        Me.rbKonfSygnPowtKolejnoscII = New System.Windows.Forms.RadioButton()
        Me.rbKonfSygnPowtKolejnoscI = New System.Windows.Forms.RadioButton()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnlKonfSygnInfSwiatla = New System.Windows.Forms.Panel()
        Me.cbKonfSygnInfPom = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnInfZiel = New System.Windows.Forms.CheckBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.pnlKonfSygnPolsamUstawienia = New System.Windows.Forms.Panel()
        Me.cbKonfSygnPolsamBrakDrogiHamowania = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamManewry = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlKonfTorPodwojny = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cbKonfTorPodwNiezajetosc1 = New System.Windows.Forms.CheckBox()
        Me.cbKonfTorPodwNiezajetosc2 = New System.Windows.Forms.CheckBox()
        Me.cbKonfTorPodwElektr2 = New System.Windows.Forms.CheckBox()
        Me.cbKonfTorPodwElektr1 = New System.Windows.Forms.CheckBox()
        Me.txtKonfTorPodwDlugosc1 = New System.Windows.Forms.TextBox()
        Me.txtKonfTorPodwDlugosc2 = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtKonfTorPodwPredk2 = New System.Windows.Forms.TextBox()
        Me.txtKonfTorPodwPredk1 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblKonfTorPodwojnyDrugi = New System.Windows.Forms.Label()
        Me.lblKonfTorPodwojnyPierwszy = New System.Windows.Forms.Label()
        Me.pnlKonfSygnPolsamSwiatla = New System.Windows.Forms.Panel()
        Me.cbKonfSygnPolsamKierPrzeciwny = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cbKonfSygnPolsamPomPas = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamZielPas = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamBiale = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamPomDol = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamCzer = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamPomGor = New System.Windows.Forms.CheckBox()
        Me.cbKonfSygnPolsamZiel = New System.Windows.Forms.CheckBox()
        Me.pnlKonfRozjazd = New System.Windows.Forms.Panel()
        Me.rbKonfRozjazdZasadniczyMinus = New System.Windows.Forms.RadioButton()
        Me.rbKonfRozjazdZasadniczyPlus = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
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
        Me.pnlKonfTor = New System.Windows.Forms.Panel()
        Me.cbKonfTorNiezajetosc = New System.Windows.Forms.CheckBox()
        Me.cbKonfTorZelektryfikowany = New System.Windows.Forms.CheckBox()
        Me.txtKonfTorDlugosc = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.txtKonfTorPredkosc = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlKonfAdres = New System.Windows.Forms.Panel()
        Me.txtKonfAdres = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.pnlKonfSygnOdcNast = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cboKonfSygnOdcinekNast = New System.Windows.Forms.ComboBox()
        Me.pnlKonfPrzycisk = New System.Windows.Forms.Panel()
        Me.cboKonfPrzyciskObiekt = New System.Windows.Forms.ComboBox()
        Me.cboKonfPrzyciskTyp = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pnlKonfPosiadaPrzycisk = New System.Windows.Forms.Panel()
        Me.cbKonfPrzycisk = New System.Windows.Forms.CheckBox()
        Me.pnlKonfNazwa = New System.Windows.Forms.Panel()
        Me.txtKonfNazwa = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.pnlKonfKier = New System.Windows.Forms.Panel()
        Me.cboKonfKierStawnosc = New System.Windows.Forms.ComboBox()
        Me.rbKonfKierWyjazdPrawo = New System.Windows.Forms.RadioButton()
        Me.rbKonfKierWyjazdLewo = New System.Windows.Forms.RadioButton()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.pnlKonfNapis = New System.Windows.Forms.Panel()
        Me.txtKonfNapisRozmiar = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtKonfNapisTekst = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbpOdcinki = New System.Windows.Forms.TabPage()
        Me.splKartaTory = New System.Windows.Forms.SplitContainer()
        Me.lvOdcinki = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ctxSortowanie = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctmSortuj = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtOdcinekAdres = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlTorLegenda = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlTorKolorNieprzypisany = New System.Windows.Forms.Panel()
        Me.pnlTorKolorTenOdcinek = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlTorKolorInnyOdcinek = New System.Windows.Forms.Panel()
        Me.btnOdcinekUsun = New System.Windows.Forms.Button()
        Me.txtOdcinekOpis = New System.Windows.Forms.TextBox()
        Me.txtOdcinekNazwa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOdcinekDodaj = New System.Windows.Forms.Button()
        Me.tbpLiczniki = New System.Windows.Forms.TabPage()
        Me.splKartaLiczniki = New System.Windows.Forms.SplitContainer()
        Me.lvLiczniki = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.pnlLicznik2 = New System.Windows.Forms.Panel()
        Me.txtLicznik2Y = New System.Windows.Forms.TextBox()
        Me.txtLicznik2X = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtLicznik2Adres = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.pnlLicznik1 = New System.Windows.Forms.Panel()
        Me.txtLicznik1Y = New System.Windows.Forms.TextBox()
        Me.txtLicznik1X = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtLicznik1Adres = New System.Windows.Forms.TextBox()
        Me.btnLicznikUsun = New System.Windows.Forms.Button()
        Me.btnLicznikDodaj = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboLicznikOdcinek2 = New System.Windows.Forms.ComboBox()
        Me.cboLicznikOdcinek1 = New System.Windows.Forms.ComboBox()
        Me.pnlLicznikOdcinek1 = New System.Windows.Forms.Panel()
        Me.pnlLicznikOdcinek2 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbpLampy = New System.Windows.Forms.TabPage()
        Me.splKartaLampy = New System.Windows.Forms.SplitContainer()
        Me.lvLampy = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtLampaY = New System.Windows.Forms.TextBox()
        Me.txtLampaX = New System.Windows.Forms.TextBox()
        Me.txtLampaAdres = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnLampaUsun = New System.Windows.Forms.Button()
        Me.btnLampaDodaj = New System.Windows.Forms.Button()
        Me.tbpPrzejazdy = New System.Windows.Forms.TabPage()
        Me.splKartaPrzejazdy = New System.Windows.Forms.SplitContainer()
        Me.lvPrzejazdy = New System.Windows.Forms.ListView()
        Me.ColumnHeader25 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader24 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader26 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnPrzejazdUsun = New System.Windows.Forms.Button()
        Me.btnPrzejazdDodaj = New System.Windows.Forms.Button()
        Me.tabPrzejazd = New System.Windows.Forms.TabControl()
        Me.tbpPrzejazdOgolne = New System.Windows.Forms.TabPage()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtPrzejazdNumer = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.cbPrzejazdTrybReczny = New System.Windows.Forms.CheckBox()
        Me.cbPrzejazdTrybAutomatyczny = New System.Windows.Forms.CheckBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.pnlPrzejazdKolorInny = New System.Windows.Forms.Panel()
        Me.pnlPrzejazdKolorNieprzypisany = New System.Windows.Forms.Panel()
        Me.pnlPrzejazdKolorPrzypisany = New System.Windows.Forms.Panel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtPrzejazdCzasPodnoszenie = New System.Windows.Forms.TextBox()
        Me.txtPrzejazdCzasOpuszczanie = New System.Windows.Forms.TextBox()
        Me.txtPrzejazdCzasSwiatla = New System.Windows.Forms.TextBox()
        Me.txtPrzejazdNazwa = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.tbpPrzejazdAutomatyzacja = New System.Windows.Forms.TabPage()
        Me.splPrzejazdAutomatyzacja = New System.Windows.Forms.SplitContainer()
        Me.lvPrzejazdAutomatyzacja = New System.Windows.Forms.ListView()
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ctxSortowaniePrzejazdy = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctmSortujPrzejazdy = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlPrzejazdAutomatyzacjaKolorSygnalizator = New System.Windows.Forms.Panel()
        Me.pnlPrzejazdAutomatyzacjaKolorPrzyjazd = New System.Windows.Forms.Panel()
        Me.pnlPrzejazdAutomatyzacjaKolorWyjazd = New System.Windows.Forms.Panel()
        Me.cboPrzejazdAutomatyzacjaSygnalizator = New System.Windows.Forms.ComboBox()
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd = New System.Windows.Forms.ComboBox()
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd = New System.Windows.Forms.ComboBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.btnPrzejazdAutomatyzacjaUsun = New System.Windows.Forms.Button()
        Me.btnPrzejazdAutomatyzacjaDodaj = New System.Windows.Forms.Button()
        Me.tbpPrzejazdRogatki = New System.Windows.Forms.TabPage()
        Me.splPrzejazdRogatki = New System.Windows.Forms.SplitContainer()
        Me.lvPrzejazdRogatki = New System.Windows.Forms.ListView()
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtPrzejazdRogatkaCzasDoZamkniecia = New System.Windows.Forms.TextBox()
        Me.btnPrzejazdRogatkaDodaj = New System.Windows.Forms.Button()
        Me.txtPrzejazdRogatkaY = New System.Windows.Forms.TextBox()
        Me.btnPrzejazdRogatkaUsun = New System.Windows.Forms.Button()
        Me.txtPrzejazdRogatkaX = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtPrzejazdRogatkaAdres = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.tbpPrzejazdSygnDrog = New System.Windows.Forms.TabPage()
        Me.splPrzejazdSygnDrog = New System.Windows.Forms.SplitContainer()
        Me.lvPrzejazdSygnDrog = New System.Windows.Forms.ListView()
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtPrzejazdSygnDrogY = New System.Windows.Forms.TextBox()
        Me.txtPrzejazdSygnDrogX = New System.Windows.Forms.TextBox()
        Me.txtPrzejazdSygnDrogAdres = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.btnPrzejazdSygnDrogUsun = New System.Windows.Forms.Button()
        Me.btnPrzejazdSygnDrogDodaj = New System.Windows.Forms.Button()
        Me.pnlKonfBudynek = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtKonfBudynekTekst = New System.Windows.Forms.TextBox()
        Me.cboKonfBudynekRodzaj = New System.Windows.Forms.ComboBox()
        Me.plpPulpit = New Pulpit.PulpitSterowniczy()
        Me.mnuMenu.SuspendLayout()
        CType(Me.splOkno, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splOkno.Panel1.SuspendLayout()
        Me.splOkno.Panel2.SuspendLayout()
        Me.splOkno.SuspendLayout()
        Me.tabUstawienia.SuspendLayout()
        Me.tbpPulpit.SuspendLayout()
        CType(Me.splKartaPulpit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaPulpit.Panel1.SuspendLayout()
        Me.splKartaPulpit.Panel2.SuspendLayout()
        Me.splKartaPulpit.SuspendLayout()
        Me.pnlKonfSygnInfSygnPowtarzany.SuspendLayout()
        Me.pnlKonfSygnPowtKolejnosc.SuspendLayout()
        Me.pnlKonfSygnInfSwiatla.SuspendLayout()
        Me.pnlKonfSygnPolsamUstawienia.SuspendLayout()
        Me.pnlKonfTorPodwojny.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlKonfSygnPolsamSwiatla.SuspendLayout()
        Me.pnlKonfRozjazd.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlKonfTor.SuspendLayout()
        Me.pnlKonfAdres.SuspendLayout()
        Me.pnlKonfSygnOdcNast.SuspendLayout()
        Me.pnlKonfPrzycisk.SuspendLayout()
        Me.pnlKonfPosiadaPrzycisk.SuspendLayout()
        Me.pnlKonfNazwa.SuspendLayout()
        Me.pnlKonfKier.SuspendLayout()
        Me.pnlKonfNapis.SuspendLayout()
        Me.tbpOdcinki.SuspendLayout()
        CType(Me.splKartaTory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaTory.Panel1.SuspendLayout()
        Me.splKartaTory.Panel2.SuspendLayout()
        Me.splKartaTory.SuspendLayout()
        Me.ctxSortowanie.SuspendLayout()
        Me.pnlTorLegenda.SuspendLayout()
        Me.tbpLiczniki.SuspendLayout()
        CType(Me.splKartaLiczniki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaLiczniki.Panel1.SuspendLayout()
        Me.splKartaLiczniki.Panel2.SuspendLayout()
        Me.splKartaLiczniki.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbpLampy.SuspendLayout()
        CType(Me.splKartaLampy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaLampy.Panel1.SuspendLayout()
        Me.splKartaLampy.Panel2.SuspendLayout()
        Me.splKartaLampy.SuspendLayout()
        Me.tbpPrzejazdy.SuspendLayout()
        CType(Me.splKartaPrzejazdy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splKartaPrzejazdy.Panel1.SuspendLayout()
        Me.splKartaPrzejazdy.Panel2.SuspendLayout()
        Me.splKartaPrzejazdy.SuspendLayout()
        Me.tabPrzejazd.SuspendLayout()
        Me.tbpPrzejazdOgolne.SuspendLayout()
        Me.tbpPrzejazdAutomatyzacja.SuspendLayout()
        CType(Me.splPrzejazdAutomatyzacja, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splPrzejazdAutomatyzacja.Panel1.SuspendLayout()
        Me.splPrzejazdAutomatyzacja.Panel2.SuspendLayout()
        Me.splPrzejazdAutomatyzacja.SuspendLayout()
        Me.ctxSortowaniePrzejazdy.SuspendLayout()
        Me.tbpPrzejazdRogatki.SuspendLayout()
        CType(Me.splPrzejazdRogatki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splPrzejazdRogatki.Panel1.SuspendLayout()
        Me.splPrzejazdRogatki.Panel2.SuspendLayout()
        Me.splPrzejazdRogatki.SuspendLayout()
        Me.tbpPrzejazdSygnDrog.SuspendLayout()
        CType(Me.splPrzejazdSygnDrog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splPrzejazdSygnDrog.Panel1.SuspendLayout()
        Me.splPrzejazdSygnDrog.Panel2.SuspendLayout()
        Me.splPrzejazdSygnDrog.SuspendLayout()
        Me.pnlKonfBudynek.SuspendLayout()
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
        Me.mnuNarzedzia.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNowy, Me.mnuOtworz, Me.mnuZapisz, Me.mnuZapiszJako, Me.ToolStripSeparator1, Me.mnuDodajKostki, Me.mnuUsunKostki, Me.ToolStripSeparator2, Me.mnuDanePosterunku, Me.mnuPredkosciTorow})
        Me.mnuNarzedzia.Name = "mnuNarzedzia"
        Me.mnuNarzedzia.Size = New System.Drawing.Size(70, 20)
        Me.mnuNarzedzia.Text = "Narzędzia"
        '
        'mnuNowy
        '
        Me.mnuNowy.Name = "mnuNowy"
        Me.mnuNowy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mnuNowy.Size = New System.Drawing.Size(209, 22)
        Me.mnuNowy.Text = "Nowy..."
        '
        'mnuOtworz
        '
        Me.mnuOtworz.Name = "mnuOtworz"
        Me.mnuOtworz.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuOtworz.Size = New System.Drawing.Size(209, 22)
        Me.mnuOtworz.Text = "Otwórz..."
        '
        'mnuZapisz
        '
        Me.mnuZapisz.Name = "mnuZapisz"
        Me.mnuZapisz.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuZapisz.Size = New System.Drawing.Size(209, 22)
        Me.mnuZapisz.Text = "Zapisz..."
        '
        'mnuZapiszJako
        '
        Me.mnuZapiszJako.Name = "mnuZapiszJako"
        Me.mnuZapiszJako.Size = New System.Drawing.Size(209, 22)
        Me.mnuZapiszJako.Text = "Zapisz jako..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(206, 6)
        '
        'mnuDodajKostki
        '
        Me.mnuDodajKostki.Name = "mnuDodajKostki"
        Me.mnuDodajKostki.Size = New System.Drawing.Size(209, 22)
        Me.mnuDodajKostki.Text = "Dodaj kostki..."
        '
        'mnuUsunKostki
        '
        Me.mnuUsunKostki.Name = "mnuUsunKostki"
        Me.mnuUsunKostki.Size = New System.Drawing.Size(209, 22)
        Me.mnuUsunKostki.Text = "Usuń kostki..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(206, 6)
        '
        'mnuDanePosterunku
        '
        Me.mnuDanePosterunku.Name = "mnuDanePosterunku"
        Me.mnuDanePosterunku.Size = New System.Drawing.Size(209, 22)
        Me.mnuDanePosterunku.Text = "Zmień dane posterunku..."
        '
        'mnuPredkosciTorow
        '
        Me.mnuPredkosciTorow.Name = "mnuPredkosciTorow"
        Me.mnuPredkosciTorow.Size = New System.Drawing.Size(209, 22)
        Me.mnuPredkosciTorow.Text = "Pokaż prędkości torów"
        '
        'splOkno
        '
        Me.splOkno.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splOkno.Location = New System.Drawing.Point(0, 24)
        Me.splOkno.Name = "splOkno"
        '
        'splOkno.Panel1
        '
        Me.splOkno.Panel1.Controls.Add(Me.plpPulpit)
        Me.splOkno.Panel1.Controls.Add(Me.Label36)
        '
        'splOkno.Panel2
        '
        Me.splOkno.Panel2.Controls.Add(Me.tabUstawienia)
        Me.splOkno.Size = New System.Drawing.Size(1006, 670)
        Me.splOkno.SplitterDistance = 788
        Me.splOkno.TabIndex = 1
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(12, 648)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(468, 13)
        Me.Label36.TabIndex = 3
        Me.Label36.Text = "R - obróc kostkę; Delete - usuń kostkę; przytrzymanie Shift i przeciągnięcie mysz" &
    "y- przesuń kostkę"
        '
        'tabUstawienia
        '
        Me.tabUstawienia.Controls.Add(Me.tbpPulpit)
        Me.tabUstawienia.Controls.Add(Me.tbpOdcinki)
        Me.tabUstawienia.Controls.Add(Me.tbpLiczniki)
        Me.tabUstawienia.Controls.Add(Me.tbpLampy)
        Me.tabUstawienia.Controls.Add(Me.tbpPrzejazdy)
        Me.tabUstawienia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabUstawienia.Location = New System.Drawing.Point(0, 0)
        Me.tabUstawienia.Name = "tabUstawienia"
        Me.tabUstawienia.SelectedIndex = 0
        Me.tabUstawienia.Size = New System.Drawing.Size(214, 670)
        Me.tabUstawienia.TabIndex = 0
        '
        'tbpPulpit
        '
        Me.tbpPulpit.Controls.Add(Me.splKartaPulpit)
        Me.tbpPulpit.Location = New System.Drawing.Point(4, 22)
        Me.tbpPulpit.Name = "tbpPulpit"
        Me.tbpPulpit.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpPulpit.Size = New System.Drawing.Size(206, 644)
        Me.tbpPulpit.TabIndex = 0
        Me.tbpPulpit.Text = "Pulpit"
        Me.tbpPulpit.UseVisualStyleBackColor = True
        '
        'splKartaPulpit
        '
        Me.splKartaPulpit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfBudynek)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygnInfSygnPowtarzany)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygnPowtKolejnosc)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygnInfSwiatla)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygnPolsamUstawienia)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfTorPodwojny)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygnPolsamSwiatla)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfRozjazd)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfTor)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfAdres)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfSygnOdcNast)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfPrzycisk)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfPosiadaPrzycisk)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfNazwa)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfKier)
        Me.splKartaPulpit.Panel2.Controls.Add(Me.pnlKonfNapis)
        Me.splKartaPulpit.Size = New System.Drawing.Size(200, 638)
        Me.splKartaPulpit.SplitterDistance = 250
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
        Me.lvPulpitKostki.Size = New System.Drawing.Size(200, 250)
        Me.lvPulpitKostki.TabIndex = 0
        Me.lvPulpitKostki.UseCompatibleStateImageBehavior = False
        '
        'imlKostki
        '
        Me.imlKostki.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imlKostki.ImageSize = New System.Drawing.Size(48, 48)
        Me.imlKostki.TransparentColor = System.Drawing.Color.Transparent
        '
        'pnlKonfSygnInfSygnPowtarzany
        '
        Me.pnlKonfSygnInfSygnPowtarzany.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnInfSygnPowtarzany.Controls.Add(Me.cboKonfSygnInfSygnPowtarzany)
        Me.pnlKonfSygnInfSygnPowtarzany.Controls.Add(Me.Label38)
        Me.pnlKonfSygnInfSygnPowtarzany.Location = New System.Drawing.Point(50, 13)
        Me.pnlKonfSygnInfSygnPowtarzany.Name = "pnlKonfSygnInfSygnPowtarzany"
        Me.pnlKonfSygnInfSygnPowtarzany.Size = New System.Drawing.Size(170, 42)
        Me.pnlKonfSygnInfSygnPowtarzany.TabIndex = 35
        Me.pnlKonfSygnInfSygnPowtarzany.Visible = False
        '
        'cboKonfSygnInfSygnPowtarzany
        '
        Me.cboKonfSygnInfSygnPowtarzany.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfSygnInfSygnPowtarzany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfSygnInfSygnPowtarzany.FormattingEnabled = True
        Me.cboKonfSygnInfSygnPowtarzany.Location = New System.Drawing.Point(0, 16)
        Me.cboKonfSygnInfSygnPowtarzany.Name = "cboKonfSygnInfSygnPowtarzany"
        Me.cboKonfSygnInfSygnPowtarzany.Size = New System.Drawing.Size(170, 21)
        Me.cboKonfSygnInfSygnPowtarzany.TabIndex = 36
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(0, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(124, 13)
        Me.Label38.TabIndex = 1
        Me.Label38.Text = "Sygnalizator powtarzany:"
        '
        'pnlKonfSygnPowtKolejnosc
        '
        Me.pnlKonfSygnPowtKolejnosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnPowtKolejnosc.Controls.Add(Me.rbKonfSygnPowtKolejnoscIII)
        Me.pnlKonfSygnPowtKolejnosc.Controls.Add(Me.rbKonfSygnPowtKolejnoscII)
        Me.pnlKonfSygnPowtKolejnosc.Controls.Add(Me.rbKonfSygnPowtKolejnoscI)
        Me.pnlKonfSygnPowtKolejnosc.Controls.Add(Me.Label40)
        Me.pnlKonfSygnPowtKolejnosc.Location = New System.Drawing.Point(26, 10)
        Me.pnlKonfSygnPowtKolejnosc.Name = "pnlKonfSygnPowtKolejnosc"
        Me.pnlKonfSygnPowtKolejnosc.Size = New System.Drawing.Size(115, 36)
        Me.pnlKonfSygnPowtKolejnosc.TabIndex = 29
        Me.pnlKonfSygnPowtKolejnosc.Visible = False
        '
        'rbKonfSygnPowtKolejnoscIII
        '
        Me.rbKonfSygnPowtKolejnoscIII.AutoSize = True
        Me.rbKonfSygnPowtKolejnoscIII.Location = New System.Drawing.Point(71, 16)
        Me.rbKonfSygnPowtKolejnoscIII.Name = "rbKonfSygnPowtKolejnoscIII"
        Me.rbKonfSygnPowtKolejnoscIII.Size = New System.Drawing.Size(34, 17)
        Me.rbKonfSygnPowtKolejnoscIII.TabIndex = 32
        Me.rbKonfSygnPowtKolejnoscIII.TabStop = True
        Me.rbKonfSygnPowtKolejnoscIII.Text = "III"
        Me.rbKonfSygnPowtKolejnoscIII.UseVisualStyleBackColor = True
        '
        'rbKonfSygnPowtKolejnoscII
        '
        Me.rbKonfSygnPowtKolejnoscII.AutoSize = True
        Me.rbKonfSygnPowtKolejnoscII.Location = New System.Drawing.Point(34, 16)
        Me.rbKonfSygnPowtKolejnoscII.Name = "rbKonfSygnPowtKolejnoscII"
        Me.rbKonfSygnPowtKolejnoscII.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfSygnPowtKolejnoscII.TabIndex = 31
        Me.rbKonfSygnPowtKolejnoscII.TabStop = True
        Me.rbKonfSygnPowtKolejnoscII.Text = "II"
        Me.rbKonfSygnPowtKolejnoscII.UseVisualStyleBackColor = True
        '
        'rbKonfSygnPowtKolejnoscI
        '
        Me.rbKonfSygnPowtKolejnoscI.AutoSize = True
        Me.rbKonfSygnPowtKolejnoscI.Location = New System.Drawing.Point(0, 16)
        Me.rbKonfSygnPowtKolejnoscI.Name = "rbKonfSygnPowtKolejnoscI"
        Me.rbKonfSygnPowtKolejnoscI.Size = New System.Drawing.Size(28, 17)
        Me.rbKonfSygnPowtKolejnoscI.TabIndex = 30
        Me.rbKonfSygnPowtKolejnoscI.TabStop = True
        Me.rbKonfSygnPowtKolejnoscI.Text = "I"
        Me.rbKonfSygnPowtKolejnoscI.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(56, 13)
        Me.Label40.TabIndex = 3
        Me.Label40.Text = "Kolejność:"
        '
        'pnlKonfSygnInfSwiatla
        '
        Me.pnlKonfSygnInfSwiatla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnInfSwiatla.Controls.Add(Me.cbKonfSygnInfPom)
        Me.pnlKonfSygnInfSwiatla.Controls.Add(Me.cbKonfSygnInfZiel)
        Me.pnlKonfSygnInfSwiatla.Controls.Add(Me.Label39)
        Me.pnlKonfSygnInfSwiatla.Location = New System.Drawing.Point(87, 7)
        Me.pnlKonfSygnInfSwiatla.Name = "pnlKonfSygnInfSwiatla"
        Me.pnlKonfSygnInfSwiatla.Size = New System.Drawing.Size(120, 59)
        Me.pnlKonfSygnInfSwiatla.TabIndex = 40
        Me.pnlKonfSygnInfSwiatla.Visible = False
        '
        'cbKonfSygnInfPom
        '
        Me.cbKonfSygnInfPom.AutoSize = True
        Me.cbKonfSygnInfPom.Location = New System.Drawing.Point(0, 39)
        Me.cbKonfSygnInfPom.Name = "cbKonfSygnInfPom"
        Me.cbKonfSygnInfPom.Size = New System.Drawing.Size(99, 17)
        Me.cbKonfSygnInfPom.TabIndex = 42
        Me.cbKonfSygnInfPom.Text = "Pomarańczowe"
        Me.cbKonfSygnInfPom.UseVisualStyleBackColor = True
        '
        'cbKonfSygnInfZiel
        '
        Me.cbKonfSygnInfZiel.AutoSize = True
        Me.cbKonfSygnInfZiel.Location = New System.Drawing.Point(0, 16)
        Me.cbKonfSygnInfZiel.Name = "cbKonfSygnInfZiel"
        Me.cbKonfSygnInfZiel.Size = New System.Drawing.Size(61, 17)
        Me.cbKonfSygnInfZiel.TabIndex = 41
        Me.cbKonfSygnInfZiel.Text = "Zielone"
        Me.cbKonfSygnInfZiel.UseVisualStyleBackColor = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(93, 13)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "Dostępne światła:"
        '
        'pnlKonfSygnPolsamUstawienia
        '
        Me.pnlKonfSygnPolsamUstawienia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnPolsamUstawienia.Controls.Add(Me.cbKonfSygnPolsamBrakDrogiHamowania)
        Me.pnlKonfSygnPolsamUstawienia.Controls.Add(Me.cbKonfSygnPolsamManewry)
        Me.pnlKonfSygnPolsamUstawienia.Controls.Add(Me.Label13)
        Me.pnlKonfSygnPolsamUstawienia.Location = New System.Drawing.Point(103, 10)
        Me.pnlKonfSygnPolsamUstawienia.Name = "pnlKonfSygnPolsamUstawienia"
        Me.pnlKonfSygnPolsamUstawienia.Size = New System.Drawing.Size(150, 61)
        Me.pnlKonfSygnPolsamUstawienia.TabIndex = 40
        Me.pnlKonfSygnPolsamUstawienia.Visible = False
        '
        'cbKonfSygnPolsamBrakDrogiHamowania
        '
        Me.cbKonfSygnPolsamBrakDrogiHamowania.AutoSize = True
        Me.cbKonfSygnPolsamBrakDrogiHamowania.Location = New System.Drawing.Point(0, 39)
        Me.cbKonfSygnPolsamBrakDrogiHamowania.Name = "cbKonfSygnPolsamBrakDrogiHamowania"
        Me.cbKonfSygnPolsamBrakDrogiHamowania.Size = New System.Drawing.Size(131, 17)
        Me.cbKonfSygnPolsamBrakDrogiHamowania.TabIndex = 42
        Me.cbKonfSygnPolsamBrakDrogiHamowania.Text = "Brak drogi hamowania"
        Me.cbKonfSygnPolsamBrakDrogiHamowania.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamManewry
        '
        Me.cbKonfSygnPolsamManewry.AutoSize = True
        Me.cbKonfSygnPolsamManewry.Location = New System.Drawing.Point(0, 16)
        Me.cbKonfSygnPolsamManewry.Name = "cbKonfSygnPolsamManewry"
        Me.cbKonfSygnPolsamManewry.Size = New System.Drawing.Size(117, 17)
        Me.cbKonfSygnPolsamManewry.TabIndex = 41
        Me.cbKonfSygnPolsamManewry.Text = "Dostępne manewry"
        Me.cbKonfSygnPolsamManewry.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(62, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Ustawienia:"
        '
        'pnlKonfTorPodwojny
        '
        Me.pnlKonfTorPodwojny.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfTorPodwojny.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlKonfTorPodwojny.Location = New System.Drawing.Point(32, 210)
        Me.pnlKonfTorPodwojny.Name = "pnlKonfTorPodwojny"
        Me.pnlKonfTorPodwojny.Size = New System.Drawing.Size(251, 143)
        Me.pnlKonfTorPodwojny.TabIndex = 19
        Me.pnlKonfTorPodwojny.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cbKonfTorPodwNiezajetosc1, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.cbKonfTorPodwNiezajetosc2, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.cbKonfTorPodwElektr2, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbKonfTorPodwElektr1, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.txtKonfTorPodwDlugosc1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.txtKonfTorPodwDlugosc2, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label27, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label28, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtKonfTorPodwPredk2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtKonfTorPodwPredk1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label15, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label16, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblKonfTorPodwojnyDrugi, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblKonfTorPodwojnyPierwszy, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(251, 140)
        Me.TableLayoutPanel1.TabIndex = 20
        '
        'cbKonfTorPodwNiezajetosc1
        '
        Me.cbKonfTorPodwNiezajetosc1.AutoSize = True
        Me.cbKonfTorPodwNiezajetosc1.Location = New System.Drawing.Point(0, 120)
        Me.cbKonfTorPodwNiezajetosc1.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.cbKonfTorPodwNiezajetosc1.Name = "cbKonfTorPodwNiezajetosc1"
        Me.cbKonfTorPodwNiezajetosc1.Size = New System.Drawing.Size(112, 17)
        Me.cbKonfTorPodwNiezajetosc1.TabIndex = 27
        Me.cbKonfTorPodwNiezajetosc1.Text = "Kontr. niezajętości"
        Me.cbKonfTorPodwNiezajetosc1.UseVisualStyleBackColor = True
        '
        'cbKonfTorPodwNiezajetosc2
        '
        Me.cbKonfTorPodwNiezajetosc2.AutoSize = True
        Me.cbKonfTorPodwNiezajetosc2.Location = New System.Drawing.Point(128, 120)
        Me.cbKonfTorPodwNiezajetosc2.Name = "cbKonfTorPodwNiezajetosc2"
        Me.cbKonfTorPodwNiezajetosc2.Size = New System.Drawing.Size(112, 17)
        Me.cbKonfTorPodwNiezajetosc2.TabIndex = 28
        Me.cbKonfTorPodwNiezajetosc2.Text = "Kontr. niezajętości"
        Me.cbKonfTorPodwNiezajetosc2.UseVisualStyleBackColor = True
        '
        'cbKonfTorPodwElektr2
        '
        Me.cbKonfTorPodwElektr2.AutoSize = True
        Me.cbKonfTorPodwElektr2.Location = New System.Drawing.Point(128, 97)
        Me.cbKonfTorPodwElektr2.Name = "cbKonfTorPodwElektr2"
        Me.cbKonfTorPodwElektr2.Size = New System.Drawing.Size(106, 17)
        Me.cbKonfTorPodwElektr2.TabIndex = 26
        Me.cbKonfTorPodwElektr2.Text = "Zelektryfikowany"
        Me.cbKonfTorPodwElektr2.UseVisualStyleBackColor = True
        '
        'cbKonfTorPodwElektr1
        '
        Me.cbKonfTorPodwElektr1.AutoSize = True
        Me.cbKonfTorPodwElektr1.Location = New System.Drawing.Point(0, 97)
        Me.cbKonfTorPodwElektr1.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.cbKonfTorPodwElektr1.Name = "cbKonfTorPodwElektr1"
        Me.cbKonfTorPodwElektr1.Size = New System.Drawing.Size(106, 17)
        Me.cbKonfTorPodwElektr1.TabIndex = 25
        Me.cbKonfTorPodwElektr1.Text = "Zelektryfikowany"
        Me.cbKonfTorPodwElektr1.UseVisualStyleBackColor = True
        '
        'txtKonfTorPodwDlugosc1
        '
        Me.txtKonfTorPodwDlugosc1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorPodwDlugosc1.Location = New System.Drawing.Point(0, 71)
        Me.txtKonfTorPodwDlugosc1.Margin = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.txtKonfTorPodwDlugosc1.Name = "txtKonfTorPodwDlugosc1"
        Me.txtKonfTorPodwDlugosc1.Size = New System.Drawing.Size(122, 20)
        Me.txtKonfTorPodwDlugosc1.TabIndex = 23
        '
        'txtKonfTorPodwDlugosc2
        '
        Me.txtKonfTorPodwDlugosc2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorPodwDlugosc2.Location = New System.Drawing.Point(128, 71)
        Me.txtKonfTorPodwDlugosc2.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.txtKonfTorPodwDlugosc2.Name = "txtKonfTorPodwDlugosc2"
        Me.txtKonfTorPodwDlugosc2.Size = New System.Drawing.Size(123, 20)
        Me.txtKonfTorPodwDlugosc2.TabIndex = 24
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(0, 55)
        Me.Label27.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(51, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Długość:"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(128, 55)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(51, 13)
        Me.Label28.TabIndex = 3
        Me.Label28.Text = "Długosć:"
        '
        'txtKonfTorPodwPredk2
        '
        Me.txtKonfTorPodwPredk2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorPodwPredk2.Location = New System.Drawing.Point(128, 32)
        Me.txtKonfTorPodwPredk2.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.txtKonfTorPodwPredk2.Name = "txtKonfTorPodwPredk2"
        Me.txtKonfTorPodwPredk2.Size = New System.Drawing.Size(123, 20)
        Me.txtKonfTorPodwPredk2.TabIndex = 22
        '
        'txtKonfTorPodwPredk1
        '
        Me.txtKonfTorPodwPredk1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorPodwPredk1.BackColor = System.Drawing.SystemColors.Window
        Me.txtKonfTorPodwPredk1.Location = New System.Drawing.Point(0, 32)
        Me.txtKonfTorPodwPredk1.Margin = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.txtKonfTorPodwPredk1.Name = "txtKonfTorPodwPredk1"
        Me.txtKonfTorPodwPredk1.Size = New System.Drawing.Size(122, 20)
        Me.txtKonfTorPodwPredk1.TabIndex = 21
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(0, 16)
        Me.Label15.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 13)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Prędkość:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(128, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 13)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Prędkość:"
        '
        'lblKonfTorPodwojnyDrugi
        '
        Me.lblKonfTorPodwojnyDrugi.AutoSize = True
        Me.lblKonfTorPodwojnyDrugi.Location = New System.Drawing.Point(128, 0)
        Me.lblKonfTorPodwojnyDrugi.Name = "lblKonfTorPodwojnyDrugi"
        Me.lblKonfTorPodwojnyDrugi.Size = New System.Drawing.Size(49, 13)
        Me.lblKonfTorPodwojnyDrugi.TabIndex = 30
        Me.lblKonfTorPodwojnyDrugi.Text = "Tor drugi"
        '
        'lblKonfTorPodwojnyPierwszy
        '
        Me.lblKonfTorPodwojnyPierwszy.AutoSize = True
        Me.lblKonfTorPodwojnyPierwszy.Location = New System.Drawing.Point(0, 0)
        Me.lblKonfTorPodwojnyPierwszy.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblKonfTorPodwojnyPierwszy.Name = "lblKonfTorPodwojnyPierwszy"
        Me.lblKonfTorPodwojnyPierwszy.Size = New System.Drawing.Size(66, 13)
        Me.lblKonfTorPodwojnyPierwszy.TabIndex = 29
        Me.lblKonfTorPodwojnyPierwszy.Text = "Tor pierwszy"
        '
        'pnlKonfSygnPolsamSwiatla
        '
        Me.pnlKonfSygnPolsamSwiatla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamKierPrzeciwny)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.Label20)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamPomPas)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamZielPas)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamBiale)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamPomDol)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamCzer)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamPomGor)
        Me.pnlKonfSygnPolsamSwiatla.Controls.Add(Me.cbKonfSygnPolsamZiel)
        Me.pnlKonfSygnPolsamSwiatla.Location = New System.Drawing.Point(3, 167)
        Me.pnlKonfSygnPolsamSwiatla.Name = "pnlKonfSygnPolsamSwiatla"
        Me.pnlKonfSygnPolsamSwiatla.Size = New System.Drawing.Size(206, 200)
        Me.pnlKonfSygnPolsamSwiatla.TabIndex = 50
        Me.pnlKonfSygnPolsamSwiatla.Visible = False
        '
        'cbKonfSygnPolsamKierPrzeciwny
        '
        Me.cbKonfSygnPolsamKierPrzeciwny.AutoSize = True
        Me.cbKonfSygnPolsamKierPrzeciwny.Location = New System.Drawing.Point(0, 179)
        Me.cbKonfSygnPolsamKierPrzeciwny.Name = "cbKonfSygnPolsamKierPrzeciwny"
        Me.cbKonfSygnPolsamKierPrzeciwny.Size = New System.Drawing.Size(180, 17)
        Me.cbKonfSygnPolsamKierPrzeciwny.TabIndex = 58
        Me.cbKonfSygnPolsamKierPrzeciwny.Text = "Wskaźnik kierunku przeciwnego"
        Me.cbKonfSygnPolsamKierPrzeciwny.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(93, 13)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "Dostępne światła:"
        '
        'cbKonfSygnPolsamPomPas
        '
        Me.cbKonfSygnPolsamPomPas.AutoSize = True
        Me.cbKonfSygnPolsamPomPas.Location = New System.Drawing.Point(0, 156)
        Me.cbKonfSygnPolsamPomPas.Name = "cbKonfSygnPolsamPomPas"
        Me.cbKonfSygnPolsamPomPas.Size = New System.Drawing.Size(118, 17)
        Me.cbKonfSygnPolsamPomPas.TabIndex = 57
        Me.cbKonfSygnPolsamPomPas.Text = "Pomarańczowy pas"
        Me.cbKonfSygnPolsamPomPas.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamZielPas
        '
        Me.cbKonfSygnPolsamZielPas.AutoSize = True
        Me.cbKonfSygnPolsamZielPas.Location = New System.Drawing.Point(0, 133)
        Me.cbKonfSygnPolsamZielPas.Name = "cbKonfSygnPolsamZielPas"
        Me.cbKonfSygnPolsamZielPas.Size = New System.Drawing.Size(80, 17)
        Me.cbKonfSygnPolsamZielPas.TabIndex = 56
        Me.cbKonfSygnPolsamZielPas.Text = "Zielony pas"
        Me.cbKonfSygnPolsamZielPas.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamBiale
        '
        Me.cbKonfSygnPolsamBiale.AutoSize = True
        Me.cbKonfSygnPolsamBiale.Location = New System.Drawing.Point(0, 110)
        Me.cbKonfSygnPolsamBiale.Name = "cbKonfSygnPolsamBiale"
        Me.cbKonfSygnPolsamBiale.Size = New System.Drawing.Size(51, 17)
        Me.cbKonfSygnPolsamBiale.TabIndex = 55
        Me.cbKonfSygnPolsamBiale.Text = "Białe"
        Me.cbKonfSygnPolsamBiale.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamPomDol
        '
        Me.cbKonfSygnPolsamPomDol.AutoSize = True
        Me.cbKonfSygnPolsamPomDol.Location = New System.Drawing.Point(0, 87)
        Me.cbKonfSygnPolsamPomDol.Name = "cbKonfSygnPolsamPomDol"
        Me.cbKonfSygnPolsamPomDol.Size = New System.Drawing.Size(128, 17)
        Me.cbKonfSygnPolsamPomDol.TabIndex = 54
        Me.cbKonfSygnPolsamPomDol.Text = "Pomarańczowe dolne"
        Me.cbKonfSygnPolsamPomDol.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamCzer
        '
        Me.cbKonfSygnPolsamCzer.AutoSize = True
        Me.cbKonfSygnPolsamCzer.Location = New System.Drawing.Point(0, 64)
        Me.cbKonfSygnPolsamCzer.Name = "cbKonfSygnPolsamCzer"
        Me.cbKonfSygnPolsamCzer.Size = New System.Drawing.Size(73, 17)
        Me.cbKonfSygnPolsamCzer.TabIndex = 53
        Me.cbKonfSygnPolsamCzer.Text = "Czerwone"
        Me.cbKonfSygnPolsamCzer.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamPomGor
        '
        Me.cbKonfSygnPolsamPomGor.AutoSize = True
        Me.cbKonfSygnPolsamPomGor.Location = New System.Drawing.Point(0, 41)
        Me.cbKonfSygnPolsamPomGor.Name = "cbKonfSygnPolsamPomGor"
        Me.cbKonfSygnPolsamPomGor.Size = New System.Drawing.Size(129, 17)
        Me.cbKonfSygnPolsamPomGor.TabIndex = 52
        Me.cbKonfSygnPolsamPomGor.Text = "Pomarańczowe górne"
        Me.cbKonfSygnPolsamPomGor.UseVisualStyleBackColor = True
        '
        'cbKonfSygnPolsamZiel
        '
        Me.cbKonfSygnPolsamZiel.AutoSize = True
        Me.cbKonfSygnPolsamZiel.Location = New System.Drawing.Point(0, 18)
        Me.cbKonfSygnPolsamZiel.Name = "cbKonfSygnPolsamZiel"
        Me.cbKonfSygnPolsamZiel.Size = New System.Drawing.Size(61, 17)
        Me.cbKonfSygnPolsamZiel.TabIndex = 51
        Me.cbKonfSygnPolsamZiel.Text = "Zielone"
        Me.cbKonfSygnPolsamZiel.UseVisualStyleBackColor = True
        '
        'pnlKonfRozjazd
        '
        Me.pnlKonfRozjazd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfRozjazd.Controls.Add(Me.rbKonfRozjazdZasadniczyMinus)
        Me.pnlKonfRozjazd.Controls.Add(Me.rbKonfRozjazdZasadniczyPlus)
        Me.pnlKonfRozjazd.Controls.Add(Me.Label14)
        Me.pnlKonfRozjazd.Controls.Add(Me.GroupBox3)
        Me.pnlKonfRozjazd.Controls.Add(Me.GroupBox2)
        Me.pnlKonfRozjazd.Location = New System.Drawing.Point(23, 75)
        Me.pnlKonfRozjazd.Name = "pnlKonfRozjazd"
        Me.pnlKonfRozjazd.Size = New System.Drawing.Size(260, 187)
        Me.pnlKonfRozjazd.TabIndex = 40
        Me.pnlKonfRozjazd.Visible = False
        '
        'rbKonfRozjazdZasadniczyMinus
        '
        Me.rbKonfRozjazdZasadniczyMinus.AutoSize = True
        Me.rbKonfRozjazdZasadniczyMinus.Location = New System.Drawing.Point(154, -2)
        Me.rbKonfRozjazdZasadniczyMinus.Name = "rbKonfRozjazdZasadniczyMinus"
        Me.rbKonfRozjazdZasadniczyMinus.Size = New System.Drawing.Size(28, 17)
        Me.rbKonfRozjazdZasadniczyMinus.TabIndex = 42
        Me.rbKonfRozjazdZasadniczyMinus.TabStop = True
        Me.rbKonfRozjazdZasadniczyMinus.Text = "-"
        Me.rbKonfRozjazdZasadniczyMinus.UseVisualStyleBackColor = True
        '
        'rbKonfRozjazdZasadniczyPlus
        '
        Me.rbKonfRozjazdZasadniczyPlus.AutoSize = True
        Me.rbKonfRozjazdZasadniczyPlus.Location = New System.Drawing.Point(117, -2)
        Me.rbKonfRozjazdZasadniczyPlus.Name = "rbKonfRozjazdZasadniczyPlus"
        Me.rbKonfRozjazdZasadniczyPlus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdZasadniczyPlus.TabIndex = 41
        Me.rbKonfRozjazdZasadniczyPlus.TabStop = True
        Me.rbKonfRozjazdZasadniczyPlus.Text = "+"
        Me.rbKonfRozjazdZasadniczyPlus.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(111, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Ustawienie na wprost:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.cboKonfRozjazdBok2)
        Me.GroupBox3.Controls.Add(Me.cboKonfRozjazdBok1)
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Controls.Add(Me.Panel3)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 103)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(260, 80)
        Me.GroupBox3.TabIndex = 52
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
        Me.cboKonfRozjazdBok2.Size = New System.Drawing.Size(177, 21)
        Me.cboKonfRozjazdBok2.TabIndex = 57
        '
        'cboKonfRozjazdBok1
        '
        Me.cboKonfRozjazdBok1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfRozjazdBok1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfRozjazdBok1.FormattingEnabled = True
        Me.cboKonfRozjazdBok1.Location = New System.Drawing.Point(6, 20)
        Me.cboKonfRozjazdBok1.Name = "cboKonfRozjazdBok1"
        Me.cboKonfRozjazdBok1.Size = New System.Drawing.Size(177, 21)
        Me.cboKonfRozjazdBok1.TabIndex = 53
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.rbKonfRozjazdBok2Plus)
        Me.Panel4.Controls.Add(Me.rbKonfRozjazdBok2Minus)
        Me.Panel4.Location = New System.Drawing.Point(189, 48)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(65, 21)
        Me.Panel4.TabIndex = 58
        '
        'rbKonfRozjazdBok2Plus
        '
        Me.rbKonfRozjazdBok2Plus.AutoSize = True
        Me.rbKonfRozjazdBok2Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdBok2Plus.Name = "rbKonfRozjazdBok2Plus"
        Me.rbKonfRozjazdBok2Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdBok2Plus.TabIndex = 59
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
        Me.rbKonfRozjazdBok2Minus.TabIndex = 60
        Me.rbKonfRozjazdBok2Minus.TabStop = True
        Me.rbKonfRozjazdBok2Minus.Text = "-"
        Me.rbKonfRozjazdBok2Minus.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.rbKonfRozjazdBok1Plus)
        Me.Panel3.Controls.Add(Me.rbKonfRozjazdBok1Minus)
        Me.Panel3.Location = New System.Drawing.Point(189, 21)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(65, 21)
        Me.Panel3.TabIndex = 54
        '
        'rbKonfRozjazdBok1Plus
        '
        Me.rbKonfRozjazdBok1Plus.AutoSize = True
        Me.rbKonfRozjazdBok1Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdBok1Plus.Name = "rbKonfRozjazdBok1Plus"
        Me.rbKonfRozjazdBok1Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdBok1Plus.TabIndex = 55
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
        Me.rbKonfRozjazdBok1Minus.TabIndex = 56
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
        Me.GroupBox2.Location = New System.Drawing.Point(0, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(260, 77)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Jeśli na wprost, ustaw rozjazdy:"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.rbKonfRozjazdWprost2Plus)
        Me.Panel2.Controls.Add(Me.rbKonfRozjazdWprost2Minus)
        Me.Panel2.Location = New System.Drawing.Point(189, 46)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(65, 21)
        Me.Panel2.TabIndex = 49
        '
        'rbKonfRozjazdWprost2Plus
        '
        Me.rbKonfRozjazdWprost2Plus.AutoSize = True
        Me.rbKonfRozjazdWprost2Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdWprost2Plus.Name = "rbKonfRozjazdWprost2Plus"
        Me.rbKonfRozjazdWprost2Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdWprost2Plus.TabIndex = 50
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
        Me.rbKonfRozjazdWprost2Minus.TabIndex = 51
        Me.rbKonfRozjazdWprost2Minus.TabStop = True
        Me.rbKonfRozjazdWprost2Minus.Text = "-"
        Me.rbKonfRozjazdWprost2Minus.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rbKonfRozjazdWprost1Plus)
        Me.Panel1.Controls.Add(Me.rbKonfRozjazdWprost1Minus)
        Me.Panel1.Location = New System.Drawing.Point(189, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(65, 21)
        Me.Panel1.TabIndex = 45
        '
        'rbKonfRozjazdWprost1Plus
        '
        Me.rbKonfRozjazdWprost1Plus.AutoSize = True
        Me.rbKonfRozjazdWprost1Plus.Location = New System.Drawing.Point(0, 0)
        Me.rbKonfRozjazdWprost1Plus.Name = "rbKonfRozjazdWprost1Plus"
        Me.rbKonfRozjazdWprost1Plus.Size = New System.Drawing.Size(31, 17)
        Me.rbKonfRozjazdWprost1Plus.TabIndex = 46
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
        Me.rbKonfRozjazdWprost1Minus.TabIndex = 47
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
        Me.cboKonfRozjazdWprost2.Size = New System.Drawing.Size(177, 21)
        Me.cboKonfRozjazdWprost2.TabIndex = 48
        '
        'cboKonfRozjazdWprost1
        '
        Me.cboKonfRozjazdWprost1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfRozjazdWprost1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfRozjazdWprost1.FormattingEnabled = True
        Me.cboKonfRozjazdWprost1.Location = New System.Drawing.Point(6, 18)
        Me.cboKonfRozjazdWprost1.Name = "cboKonfRozjazdWprost1"
        Me.cboKonfRozjazdWprost1.Size = New System.Drawing.Size(177, 21)
        Me.cboKonfRozjazdWprost1.TabIndex = 44
        '
        'pnlKonfTor
        '
        Me.pnlKonfTor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfTor.Controls.Add(Me.cbKonfTorNiezajetosc)
        Me.pnlKonfTor.Controls.Add(Me.cbKonfTorZelektryfikowany)
        Me.pnlKonfTor.Controls.Add(Me.txtKonfTorDlugosc)
        Me.pnlKonfTor.Controls.Add(Me.Label63)
        Me.pnlKonfTor.Controls.Add(Me.txtKonfTorPredkosc)
        Me.pnlKonfTor.Controls.Add(Me.Label12)
        Me.pnlKonfTor.Location = New System.Drawing.Point(10, 32)
        Me.pnlKonfTor.Name = "pnlKonfTor"
        Me.pnlKonfTor.Size = New System.Drawing.Size(170, 124)
        Me.pnlKonfTor.TabIndex = 19
        Me.pnlKonfTor.Visible = False
        '
        'cbKonfTorNiezajetosc
        '
        Me.cbKonfTorNiezajetosc.AutoSize = True
        Me.cbKonfTorNiezajetosc.Location = New System.Drawing.Point(0, 104)
        Me.cbKonfTorNiezajetosc.Name = "cbKonfTorNiezajetosc"
        Me.cbKonfTorNiezajetosc.Size = New System.Drawing.Size(123, 17)
        Me.cbKonfTorNiezajetosc.TabIndex = 24
        Me.cbKonfTorNiezajetosc.Text = "Kontrola niezajętości"
        Me.cbKonfTorNiezajetosc.UseVisualStyleBackColor = True
        '
        'cbKonfTorZelektryfikowany
        '
        Me.cbKonfTorZelektryfikowany.AutoSize = True
        Me.cbKonfTorZelektryfikowany.Location = New System.Drawing.Point(0, 81)
        Me.cbKonfTorZelektryfikowany.Name = "cbKonfTorZelektryfikowany"
        Me.cbKonfTorZelektryfikowany.Size = New System.Drawing.Size(106, 17)
        Me.cbKonfTorZelektryfikowany.TabIndex = 23
        Me.cbKonfTorZelektryfikowany.Text = "Zelektryfikowany"
        Me.cbKonfTorZelektryfikowany.UseVisualStyleBackColor = True
        '
        'txtKonfTorDlugosc
        '
        Me.txtKonfTorDlugosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorDlugosc.Location = New System.Drawing.Point(0, 55)
        Me.txtKonfTorDlugosc.Name = "txtKonfTorDlugosc"
        Me.txtKonfTorDlugosc.Size = New System.Drawing.Size(170, 20)
        Me.txtKonfTorDlugosc.TabIndex = 22
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(0, 39)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(97, 13)
        Me.Label63.TabIndex = 12
        Me.Label63.Text = "Długość toru [mm]:"
        '
        'txtKonfTorPredkosc
        '
        Me.txtKonfTorPredkosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfTorPredkosc.BackColor = System.Drawing.Color.White
        Me.txtKonfTorPredkosc.Location = New System.Drawing.Point(0, 16)
        Me.txtKonfTorPredkosc.Name = "txtKonfTorPredkosc"
        Me.txtKonfTorPredkosc.Size = New System.Drawing.Size(170, 20)
        Me.txtKonfTorPredkosc.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(167, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Prędkość maksymalna [km/godz]:"
        '
        'pnlKonfAdres
        '
        Me.pnlKonfAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfAdres.Controls.Add(Me.txtKonfAdres)
        Me.pnlKonfAdres.Controls.Add(Me.Label64)
        Me.pnlKonfAdres.Location = New System.Drawing.Point(176, 340)
        Me.pnlKonfAdres.Name = "pnlKonfAdres"
        Me.pnlKonfAdres.Size = New System.Drawing.Size(200, 39)
        Me.pnlKonfAdres.TabIndex = 9
        Me.pnlKonfAdres.Visible = False
        '
        'txtKonfAdres
        '
        Me.txtKonfAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfAdres.Location = New System.Drawing.Point(0, 16)
        Me.txtKonfAdres.Name = "txtKonfAdres"
        Me.txtKonfAdres.Size = New System.Drawing.Size(200, 20)
        Me.txtKonfAdres.TabIndex = 10
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(0, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(37, 13)
        Me.Label64.TabIndex = 0
        Me.Label64.Text = "Adres:"
        '
        'pnlKonfSygnOdcNast
        '
        Me.pnlKonfSygnOdcNast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfSygnOdcNast.Controls.Add(Me.Label19)
        Me.pnlKonfSygnOdcNast.Controls.Add(Me.cboKonfSygnOdcinekNast)
        Me.pnlKonfSygnOdcNast.Location = New System.Drawing.Point(152, 336)
        Me.pnlKonfSygnOdcNast.Name = "pnlKonfSygnOdcNast"
        Me.pnlKonfSygnOdcNast.Size = New System.Drawing.Size(206, 40)
        Me.pnlKonfSygnOdcNast.TabIndex = 29
        Me.pnlKonfSygnOdcNast.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(131, 13)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "Odcinek toru następujący:"
        '
        'cboKonfSygnOdcinekNast
        '
        Me.cboKonfSygnOdcinekNast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfSygnOdcinekNast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfSygnOdcinekNast.FormattingEnabled = True
        Me.cboKonfSygnOdcinekNast.Location = New System.Drawing.Point(0, 16)
        Me.cboKonfSygnOdcinekNast.Name = "cboKonfSygnOdcinekNast"
        Me.cboKonfSygnOdcinekNast.Size = New System.Drawing.Size(206, 21)
        Me.cboKonfSygnOdcinekNast.TabIndex = 30
        '
        'pnlKonfPrzycisk
        '
        Me.pnlKonfPrzycisk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfPrzycisk.Controls.Add(Me.cboKonfPrzyciskObiekt)
        Me.pnlKonfPrzycisk.Controls.Add(Me.cboKonfPrzyciskTyp)
        Me.pnlKonfPrzycisk.Controls.Add(Me.Label22)
        Me.pnlKonfPrzycisk.Controls.Add(Me.Label21)
        Me.pnlKonfPrzycisk.Location = New System.Drawing.Point(87, 179)
        Me.pnlKonfPrzycisk.Name = "pnlKonfPrzycisk"
        Me.pnlKonfPrzycisk.Size = New System.Drawing.Size(185, 91)
        Me.pnlKonfPrzycisk.TabIndex = 29
        Me.pnlKonfPrzycisk.Visible = False
        '
        'cboKonfPrzyciskObiekt
        '
        Me.cboKonfPrzyciskObiekt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfPrzyciskObiekt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfPrzyciskObiekt.FormattingEnabled = True
        Me.cboKonfPrzyciskObiekt.Location = New System.Drawing.Point(0, 56)
        Me.cboKonfPrzyciskObiekt.Name = "cboKonfPrzyciskObiekt"
        Me.cboKonfPrzyciskObiekt.Size = New System.Drawing.Size(185, 21)
        Me.cboKonfPrzyciskObiekt.TabIndex = 31
        '
        'cboKonfPrzyciskTyp
        '
        Me.cboKonfPrzyciskTyp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfPrzyciskTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfPrzyciskTyp.FormattingEnabled = True
        Me.cboKonfPrzyciskTyp.Location = New System.Drawing.Point(0, 16)
        Me.cboKonfPrzyciskTyp.Name = "cboKonfPrzyciskTyp"
        Me.cboKonfPrzyciskTyp.Size = New System.Drawing.Size(185, 21)
        Me.cboKonfPrzyciskTyp.TabIndex = 30
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(0, 40)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(93, 13)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "Powiązany obiekt:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(75, 13)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Typ przycisku:"
        '
        'pnlKonfPosiadaPrzycisk
        '
        Me.pnlKonfPosiadaPrzycisk.Controls.Add(Me.cbKonfPrzycisk)
        Me.pnlKonfPosiadaPrzycisk.Location = New System.Drawing.Point(135, 331)
        Me.pnlKonfPosiadaPrzycisk.Name = "pnlKonfPosiadaPrzycisk"
        Me.pnlKonfPosiadaPrzycisk.Size = New System.Drawing.Size(118, 22)
        Me.pnlKonfPosiadaPrzycisk.TabIndex = 31
        Me.pnlKonfPosiadaPrzycisk.Visible = False
        '
        'cbKonfPrzycisk
        '
        Me.cbKonfPrzycisk.AutoSize = True
        Me.cbKonfPrzycisk.Location = New System.Drawing.Point(0, 2)
        Me.cbKonfPrzycisk.Name = "cbKonfPrzycisk"
        Me.cbKonfPrzycisk.Size = New System.Drawing.Size(105, 17)
        Me.cbKonfPrzycisk.TabIndex = 32
        Me.cbKonfPrzycisk.Text = "Posiada przycisk"
        Me.cbKonfPrzycisk.UseVisualStyleBackColor = True
        '
        'pnlKonfNazwa
        '
        Me.pnlKonfNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfNazwa.Controls.Add(Me.txtKonfNazwa)
        Me.pnlKonfNazwa.Controls.Add(Me.Label37)
        Me.pnlKonfNazwa.Location = New System.Drawing.Point(95, 298)
        Me.pnlKonfNazwa.Name = "pnlKonfNazwa"
        Me.pnlKonfNazwa.Size = New System.Drawing.Size(200, 39)
        Me.pnlKonfNazwa.TabIndex = 17
        Me.pnlKonfNazwa.Visible = False
        '
        'txtKonfNazwa
        '
        Me.txtKonfNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfNazwa.Location = New System.Drawing.Point(0, 16)
        Me.txtKonfNazwa.Name = "txtKonfNazwa"
        Me.txtKonfNazwa.Size = New System.Drawing.Size(200, 20)
        Me.txtKonfNazwa.TabIndex = 18
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(0, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(43, 13)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "Nazwa:"
        '
        'pnlKonfKier
        '
        Me.pnlKonfKier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfKier.Controls.Add(Me.cboKonfKierStawnosc)
        Me.pnlKonfKier.Controls.Add(Me.rbKonfKierWyjazdPrawo)
        Me.pnlKonfKier.Controls.Add(Me.rbKonfKierWyjazdLewo)
        Me.pnlKonfKier.Controls.Add(Me.Label60)
        Me.pnlKonfKier.Controls.Add(Me.Label59)
        Me.pnlKonfKier.Location = New System.Drawing.Point(40, 236)
        Me.pnlKonfKier.Name = "pnlKonfKier"
        Me.pnlKonfKier.Size = New System.Drawing.Size(167, 88)
        Me.pnlKonfKier.TabIndex = 29
        Me.pnlKonfKier.Visible = False
        '
        'cboKonfKierStawnosc
        '
        Me.cboKonfKierStawnosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfKierStawnosc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfKierStawnosc.FormattingEnabled = True
        Me.cboKonfKierStawnosc.Location = New System.Drawing.Point(0, 55)
        Me.cboKonfKierStawnosc.Name = "cboKonfKierStawnosc"
        Me.cboKonfKierStawnosc.Size = New System.Drawing.Size(167, 21)
        Me.cboKonfKierStawnosc.TabIndex = 32
        '
        'rbKonfKierWyjazdPrawo
        '
        Me.rbKonfKierWyjazdPrawo.AutoSize = True
        Me.rbKonfKierWyjazdPrawo.Location = New System.Drawing.Point(57, 16)
        Me.rbKonfKierWyjazdPrawo.Name = "rbKonfKierWyjazdPrawo"
        Me.rbKonfKierWyjazdPrawo.Size = New System.Drawing.Size(55, 17)
        Me.rbKonfKierWyjazdPrawo.TabIndex = 31
        Me.rbKonfKierWyjazdPrawo.TabStop = True
        Me.rbKonfKierWyjazdPrawo.Text = "Prawo"
        Me.rbKonfKierWyjazdPrawo.UseVisualStyleBackColor = True
        '
        'rbKonfKierWyjazdLewo
        '
        Me.rbKonfKierWyjazdLewo.AutoSize = True
        Me.rbKonfKierWyjazdLewo.Location = New System.Drawing.Point(0, 16)
        Me.rbKonfKierWyjazdLewo.Name = "rbKonfKierWyjazdLewo"
        Me.rbKonfKierWyjazdLewo.Size = New System.Drawing.Size(51, 17)
        Me.rbKonfKierWyjazdLewo.TabIndex = 30
        Me.rbKonfKierWyjazdLewo.TabStop = True
        Me.rbKonfKierWyjazdLewo.Text = "Lewo"
        Me.rbKonfKierWyjazdLewo.UseVisualStyleBackColor = True
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(0, 36)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(57, 13)
        Me.Label60.TabIndex = 13
        Me.Label60.Text = "Stawność:"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(0, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(211, 13)
        Me.Label59.TabIndex = 12
        Me.Label59.Text = "Kierunek wyjazdu (dla kostki nieobróconej):"
        '
        'pnlKonfNapis
        '
        Me.pnlKonfNapis.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfNapis.Controls.Add(Me.txtKonfNapisRozmiar)
        Me.pnlKonfNapis.Controls.Add(Me.Label17)
        Me.pnlKonfNapis.Controls.Add(Me.txtKonfNapisTekst)
        Me.pnlKonfNapis.Controls.Add(Me.Label9)
        Me.pnlKonfNapis.Location = New System.Drawing.Point(56, 257)
        Me.pnlKonfNapis.Name = "pnlKonfNapis"
        Me.pnlKonfNapis.Size = New System.Drawing.Size(144, 87)
        Me.pnlKonfNapis.TabIndex = 1
        Me.pnlKonfNapis.Visible = False
        '
        'txtKonfNapisRozmiar
        '
        Me.txtKonfNapisRozmiar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfNapisRozmiar.Location = New System.Drawing.Point(0, 55)
        Me.txtKonfNapisRozmiar.Name = "txtKonfNapisRozmiar"
        Me.txtKonfNapisRozmiar.Size = New System.Drawing.Size(144, 20)
        Me.txtKonfNapisRozmiar.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(0, 39)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "Rozmiar:"
        '
        'txtKonfNapisTekst
        '
        Me.txtKonfNapisTekst.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfNapisTekst.Location = New System.Drawing.Point(0, 16)
        Me.txtKonfNapisTekst.Name = "txtKonfNapisTekst"
        Me.txtKonfNapisTekst.Size = New System.Drawing.Size(144, 20)
        Me.txtKonfNapisTekst.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Tekst:"
        '
        'tbpOdcinki
        '
        Me.tbpOdcinki.Controls.Add(Me.splKartaTory)
        Me.tbpOdcinki.Location = New System.Drawing.Point(4, 22)
        Me.tbpOdcinki.Name = "tbpOdcinki"
        Me.tbpOdcinki.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpOdcinki.Size = New System.Drawing.Size(206, 644)
        Me.tbpOdcinki.TabIndex = 1
        Me.tbpOdcinki.Text = "Odcinki torów"
        Me.tbpOdcinki.UseVisualStyleBackColor = True
        '
        'splKartaTory
        '
        Me.splKartaTory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splKartaTory.Location = New System.Drawing.Point(3, 3)
        Me.splKartaTory.Name = "splKartaTory"
        Me.splKartaTory.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splKartaTory.Panel1
        '
        Me.splKartaTory.Panel1.Controls.Add(Me.lvOdcinki)
        '
        'splKartaTory.Panel2
        '
        Me.splKartaTory.Panel2.Controls.Add(Me.Label34)
        Me.splKartaTory.Panel2.Controls.Add(Me.txtOdcinekAdres)
        Me.splKartaTory.Panel2.Controls.Add(Me.Label7)
        Me.splKartaTory.Panel2.Controls.Add(Me.pnlTorLegenda)
        Me.splKartaTory.Panel2.Controls.Add(Me.btnOdcinekUsun)
        Me.splKartaTory.Panel2.Controls.Add(Me.txtOdcinekOpis)
        Me.splKartaTory.Panel2.Controls.Add(Me.txtOdcinekNazwa)
        Me.splKartaTory.Panel2.Controls.Add(Me.Label2)
        Me.splKartaTory.Panel2.Controls.Add(Me.Label1)
        Me.splKartaTory.Panel2.Controls.Add(Me.btnOdcinekDodaj)
        Me.splKartaTory.Size = New System.Drawing.Size(200, 638)
        Me.splKartaTory.SplitterDistance = 250
        Me.splKartaTory.TabIndex = 0
        '
        'lvOdcinki
        '
        Me.lvOdcinki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvOdcinki.ContextMenuStrip = Me.ctxSortowanie
        Me.lvOdcinki.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvOdcinki.FullRowSelect = True
        Me.lvOdcinki.HideSelection = False
        Me.lvOdcinki.Location = New System.Drawing.Point(0, 0)
        Me.lvOdcinki.MultiSelect = False
        Me.lvOdcinki.Name = "lvOdcinki"
        Me.lvOdcinki.Size = New System.Drawing.Size(200, 250)
        Me.lvOdcinki.TabIndex = 0
        Me.lvOdcinki.UseCompatibleStateImageBehavior = False
        Me.lvOdcinki.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Adres"
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Nazwa"
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Kostki"
        '
        'ctxSortowanie
        '
        Me.ctxSortowanie.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctmSortuj})
        Me.ctxSortowanie.Name = "ctxOdcinki"
        Me.ctxSortowanie.Size = New System.Drawing.Size(106, 26)
        '
        'ctmSortuj
        '
        Me.ctmSortuj.Name = "ctmSortuj"
        Me.ctmSortuj.Size = New System.Drawing.Size(105, 22)
        Me.ctmSortuj.Text = "Sortuj"
        '
        'Label34
        '
        Me.Label34.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label34.AutoEllipsis = True
        Me.Label34.Location = New System.Drawing.Point(0, 266)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(200, 118)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = "Aby dodać kostkę toru do odcinka, należy zaznaczyć odcinek na liście i kliknąć na" &
    " kostkę na pulpicie."
        '
        'txtOdcinekAdres
        '
        Me.txtOdcinekAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOdcinekAdres.Location = New System.Drawing.Point(0, 45)
        Me.txtOdcinekAdres.Name = "txtOdcinekAdres"
        Me.txtOdcinekAdres.Size = New System.Drawing.Size(200, 20)
        Me.txtOdcinekAdres.TabIndex = 12
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
        Me.pnlTorLegenda.Controls.Add(Me.pnlTorKolorNieprzypisany)
        Me.pnlTorLegenda.Controls.Add(Me.pnlTorKolorTenOdcinek)
        Me.pnlTorLegenda.Controls.Add(Me.Label5)
        Me.pnlTorLegenda.Controls.Add(Me.pnlTorKolorInnyOdcinek)
        Me.pnlTorLegenda.Location = New System.Drawing.Point(0, 195)
        Me.pnlTorLegenda.Name = "pnlTorLegenda"
        Me.pnlTorLegenda.Size = New System.Drawing.Size(200, 73)
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
        'pnlTorKolorNieprzypisany
        '
        Me.pnlTorKolorNieprzypisany.Location = New System.Drawing.Point(4, 55)
        Me.pnlTorKolorNieprzypisany.Name = "pnlTorKolorNieprzypisany"
        Me.pnlTorKolorNieprzypisany.Size = New System.Drawing.Size(13, 13)
        Me.pnlTorKolorNieprzypisany.TabIndex = 12
        '
        'pnlTorKolorTenOdcinek
        '
        Me.pnlTorKolorTenOdcinek.Location = New System.Drawing.Point(4, 17)
        Me.pnlTorKolorTenOdcinek.Name = "pnlTorKolorTenOdcinek"
        Me.pnlTorKolorTenOdcinek.Size = New System.Drawing.Size(13, 13)
        Me.pnlTorKolorTenOdcinek.TabIndex = 7
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
        'pnlTorKolorInnyOdcinek
        '
        Me.pnlTorKolorInnyOdcinek.Location = New System.Drawing.Point(4, 36)
        Me.pnlTorKolorInnyOdcinek.Name = "pnlTorKolorInnyOdcinek"
        Me.pnlTorKolorInnyOdcinek.Size = New System.Drawing.Size(13, 13)
        Me.pnlTorKolorInnyOdcinek.TabIndex = 10
        '
        'btnOdcinekUsun
        '
        Me.btnOdcinekUsun.Location = New System.Drawing.Point(103, 3)
        Me.btnOdcinekUsun.Name = "btnOdcinekUsun"
        Me.btnOdcinekUsun.Size = New System.Drawing.Size(98, 23)
        Me.btnOdcinekUsun.TabIndex = 11
        Me.btnOdcinekUsun.Text = "Usuń"
        Me.btnOdcinekUsun.UseVisualStyleBackColor = True
        '
        'txtOdcinekOpis
        '
        Me.txtOdcinekOpis.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOdcinekOpis.Location = New System.Drawing.Point(0, 123)
        Me.txtOdcinekOpis.Multiline = True
        Me.txtOdcinekOpis.Name = "txtOdcinekOpis"
        Me.txtOdcinekOpis.Size = New System.Drawing.Size(200, 66)
        Me.txtOdcinekOpis.TabIndex = 14
        '
        'txtOdcinekNazwa
        '
        Me.txtOdcinekNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOdcinekNazwa.Location = New System.Drawing.Point(0, 84)
        Me.txtOdcinekNazwa.Name = "txtOdcinekNazwa"
        Me.txtOdcinekNazwa.Size = New System.Drawing.Size(200, 20)
        Me.txtOdcinekNazwa.TabIndex = 13
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
        'btnOdcinekDodaj
        '
        Me.btnOdcinekDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnOdcinekDodaj.Name = "btnOdcinekDodaj"
        Me.btnOdcinekDodaj.Size = New System.Drawing.Size(98, 23)
        Me.btnOdcinekDodaj.TabIndex = 10
        Me.btnOdcinekDodaj.Text = "Dodaj"
        Me.btnOdcinekDodaj.UseVisualStyleBackColor = True
        '
        'tbpLiczniki
        '
        Me.tbpLiczniki.Controls.Add(Me.splKartaLiczniki)
        Me.tbpLiczniki.Location = New System.Drawing.Point(4, 22)
        Me.tbpLiczniki.Name = "tbpLiczniki"
        Me.tbpLiczniki.Size = New System.Drawing.Size(206, 644)
        Me.tbpLiczniki.TabIndex = 2
        Me.tbpLiczniki.Text = "Liczniki osi"
        Me.tbpLiczniki.UseVisualStyleBackColor = True
        '
        'splKartaLiczniki
        '
        Me.splKartaLiczniki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splKartaLiczniki.Location = New System.Drawing.Point(3, 3)
        Me.splKartaLiczniki.Name = "splKartaLiczniki"
        Me.splKartaLiczniki.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splKartaLiczniki.Panel1
        '
        Me.splKartaLiczniki.Panel1.Controls.Add(Me.lvLiczniki)
        '
        'splKartaLiczniki.Panel2
        '
        Me.splKartaLiczniki.Panel2.Controls.Add(Me.GroupBox5)
        Me.splKartaLiczniki.Panel2.Controls.Add(Me.GroupBox4)
        Me.splKartaLiczniki.Panel2.Controls.Add(Me.btnLicznikUsun)
        Me.splKartaLiczniki.Panel2.Controls.Add(Me.btnLicznikDodaj)
        Me.splKartaLiczniki.Panel2.Controls.Add(Me.GroupBox1)
        Me.splKartaLiczniki.Size = New System.Drawing.Size(200, 638)
        Me.splKartaLiczniki.SplitterDistance = 250
        Me.splKartaLiczniki.TabIndex = 0
        '
        'lvLiczniki
        '
        Me.lvLiczniki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lvLiczniki.ContextMenuStrip = Me.ctxSortowanie
        Me.lvLiczniki.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvLiczniki.FullRowSelect = True
        Me.lvLiczniki.HideSelection = False
        Me.lvLiczniki.Location = New System.Drawing.Point(0, 0)
        Me.lvLiczniki.MultiSelect = False
        Me.lvLiczniki.Name = "lvLiczniki"
        Me.lvLiczniki.Size = New System.Drawing.Size(200, 250)
        Me.lvLiczniki.TabIndex = 0
        Me.lvLiczniki.UseCompatibleStateImageBehavior = False
        Me.lvLiczniki.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Adres 1"
        Me.ColumnHeader7.Width = 50
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Adres 2"
        Me.ColumnHeader8.Width = 50
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Zw. licznik"
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Zmn. licznik"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.pnlLicznik2)
        Me.GroupBox5.Controls.Add(Me.txtLicznik2Y)
        Me.GroupBox5.Controls.Add(Me.txtLicznik2X)
        Me.GroupBox5.Controls.Add(Me.Label31)
        Me.GroupBox5.Controls.Add(Me.Label32)
        Me.GroupBox5.Controls.Add(Me.Label33)
        Me.GroupBox5.Controls.Add(Me.txtLicznik2Adres)
        Me.GroupBox5.Location = New System.Drawing.Point(0, 103)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(200, 65)
        Me.GroupBox5.TabIndex = 16
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Licznik 2"
        '
        'pnlLicznik2
        '
        Me.pnlLicznik2.Location = New System.Drawing.Point(182, 13)
        Me.pnlLicznik2.Name = "pnlLicznik2"
        Me.pnlLicznik2.Size = New System.Drawing.Size(13, 13)
        Me.pnlLicznik2.TabIndex = 11
        '
        'txtLicznik2Y
        '
        Me.txtLicznik2Y.Location = New System.Drawing.Point(136, 32)
        Me.txtLicznik2Y.Name = "txtLicznik2Y"
        Me.txtLicznik2Y.Size = New System.Drawing.Size(59, 20)
        Me.txtLicznik2Y.TabIndex = 19
        '
        'txtLicznik2X
        '
        Me.txtLicznik2X.Location = New System.Drawing.Point(71, 32)
        Me.txtLicznik2X.Name = "txtLicznik2X"
        Me.txtLicznik2X.Size = New System.Drawing.Size(59, 20)
        Me.txtLicznik2X.TabIndex = 18
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(136, 16)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(17, 13)
        Me.Label31.TabIndex = 7
        Me.Label31.Text = "Y:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(71, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(17, 13)
        Me.Label32.TabIndex = 6
        Me.Label32.Text = "X:"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(6, 16)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(37, 13)
        Me.Label33.TabIndex = 0
        Me.Label33.Text = "Adres:"
        '
        'txtLicznik2Adres
        '
        Me.txtLicznik2Adres.Location = New System.Drawing.Point(6, 32)
        Me.txtLicznik2Adres.Name = "txtLicznik2Adres"
        Me.txtLicznik2Adres.Size = New System.Drawing.Size(59, 20)
        Me.txtLicznik2Adres.TabIndex = 17
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.pnlLicznik1)
        Me.GroupBox4.Controls.Add(Me.txtLicznik1Y)
        Me.GroupBox4.Controls.Add(Me.txtLicznik1X)
        Me.GroupBox4.Controls.Add(Me.Label30)
        Me.GroupBox4.Controls.Add(Me.Label29)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtLicznik1Adres)
        Me.GroupBox4.Location = New System.Drawing.Point(0, 32)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(200, 65)
        Me.GroupBox4.TabIndex = 12
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Licznik 1"
        '
        'pnlLicznik1
        '
        Me.pnlLicznik1.Location = New System.Drawing.Point(182, 13)
        Me.pnlLicznik1.Name = "pnlLicznik1"
        Me.pnlLicznik1.Size = New System.Drawing.Size(13, 13)
        Me.pnlLicznik1.TabIndex = 10
        '
        'txtLicznik1Y
        '
        Me.txtLicznik1Y.Location = New System.Drawing.Point(136, 32)
        Me.txtLicznik1Y.Name = "txtLicznik1Y"
        Me.txtLicznik1Y.Size = New System.Drawing.Size(59, 20)
        Me.txtLicznik1Y.TabIndex = 15
        '
        'txtLicznik1X
        '
        Me.txtLicznik1X.Location = New System.Drawing.Point(71, 32)
        Me.txtLicznik1X.Name = "txtLicznik1X"
        Me.txtLicznik1X.Size = New System.Drawing.Size(59, 20)
        Me.txtLicznik1X.TabIndex = 14
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(136, 16)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(17, 13)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "Y:"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(71, 16)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(17, 13)
        Me.Label29.TabIndex = 6
        Me.Label29.Text = "X:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Adres:"
        '
        'txtLicznik1Adres
        '
        Me.txtLicznik1Adres.Location = New System.Drawing.Point(6, 32)
        Me.txtLicznik1Adres.Name = "txtLicznik1Adres"
        Me.txtLicznik1Adres.Size = New System.Drawing.Size(59, 20)
        Me.txtLicznik1Adres.TabIndex = 13
        '
        'btnLicznikUsun
        '
        Me.btnLicznikUsun.Location = New System.Drawing.Point(103, 3)
        Me.btnLicznikUsun.Name = "btnLicznikUsun"
        Me.btnLicznikUsun.Size = New System.Drawing.Size(98, 23)
        Me.btnLicznikUsun.TabIndex = 11
        Me.btnLicznikUsun.Text = "Usuń"
        Me.btnLicznikUsun.UseVisualStyleBackColor = True
        '
        'btnLicznikDodaj
        '
        Me.btnLicznikDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnLicznikDodaj.Name = "btnLicznikDodaj"
        Me.btnLicznikDodaj.Size = New System.Drawing.Size(98, 23)
        Me.btnLicznikDodaj.TabIndex = 10
        Me.btnLicznikDodaj.Text = "Dodaj"
        Me.btnLicznikDodaj.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboLicznikOdcinek2)
        Me.GroupBox1.Controls.Add(Me.cboLicznikOdcinek1)
        Me.GroupBox1.Controls.Add(Me.pnlLicznikOdcinek1)
        Me.GroupBox1.Controls.Add(Me.pnlLicznikOdcinek2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 174)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 105)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Jeśli przejazd od licznika 1 do 2"
        '
        'cboLicznikOdcinek2
        '
        Me.cboLicznikOdcinek2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboLicznikOdcinek2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLicznikOdcinek2.FormattingEnabled = True
        Me.cboLicznikOdcinek2.Location = New System.Drawing.Point(6, 72)
        Me.cboLicznikOdcinek2.Name = "cboLicznikOdcinek2"
        Me.cboLicznikOdcinek2.Size = New System.Drawing.Size(189, 21)
        Me.cboLicznikOdcinek2.TabIndex = 22
        '
        'cboLicznikOdcinek1
        '
        Me.cboLicznikOdcinek1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboLicznikOdcinek1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLicznikOdcinek1.FormattingEnabled = True
        Me.cboLicznikOdcinek1.Location = New System.Drawing.Point(6, 32)
        Me.cboLicznikOdcinek1.Name = "cboLicznikOdcinek1"
        Me.cboLicznikOdcinek1.Size = New System.Drawing.Size(189, 21)
        Me.cboLicznikOdcinek1.TabIndex = 21
        '
        'pnlLicznikOdcinek1
        '
        Me.pnlLicznikOdcinek1.Location = New System.Drawing.Point(182, 16)
        Me.pnlLicznikOdcinek1.Name = "pnlLicznikOdcinek1"
        Me.pnlLicznikOdcinek1.Size = New System.Drawing.Size(13, 13)
        Me.pnlLicznikOdcinek1.TabIndex = 7
        '
        'pnlLicznikOdcinek2
        '
        Me.pnlLicznikOdcinek2.Location = New System.Drawing.Point(182, 56)
        Me.pnlLicznikOdcinek2.Name = "pnlLicznikOdcinek2"
        Me.pnlLicznikOdcinek2.Size = New System.Drawing.Size(13, 13)
        Me.pnlLicznikOdcinek2.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(122, 13)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Zwiększ licznik odcinka:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 13)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Zmniejsz licznik odcinka:"
        '
        'tbpLampy
        '
        Me.tbpLampy.Controls.Add(Me.splKartaLampy)
        Me.tbpLampy.Location = New System.Drawing.Point(4, 22)
        Me.tbpLampy.Name = "tbpLampy"
        Me.tbpLampy.Size = New System.Drawing.Size(206, 644)
        Me.tbpLampy.TabIndex = 3
        Me.tbpLampy.Text = "Lampy"
        Me.tbpLampy.UseVisualStyleBackColor = True
        '
        'splKartaLampy
        '
        Me.splKartaLampy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splKartaLampy.Location = New System.Drawing.Point(3, 3)
        Me.splKartaLampy.Name = "splKartaLampy"
        Me.splKartaLampy.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splKartaLampy.Panel1
        '
        Me.splKartaLampy.Panel1.Controls.Add(Me.lvLampy)
        '
        'splKartaLampy.Panel2
        '
        Me.splKartaLampy.Panel2.Controls.Add(Me.txtLampaY)
        Me.splKartaLampy.Panel2.Controls.Add(Me.txtLampaX)
        Me.splKartaLampy.Panel2.Controls.Add(Me.txtLampaAdres)
        Me.splKartaLampy.Panel2.Controls.Add(Me.Label26)
        Me.splKartaLampy.Panel2.Controls.Add(Me.Label25)
        Me.splKartaLampy.Panel2.Controls.Add(Me.Label24)
        Me.splKartaLampy.Panel2.Controls.Add(Me.btnLampaUsun)
        Me.splKartaLampy.Panel2.Controls.Add(Me.btnLampaDodaj)
        Me.splKartaLampy.Size = New System.Drawing.Size(200, 638)
        Me.splKartaLampy.SplitterDistance = 250
        Me.splKartaLampy.TabIndex = 0
        '
        'lvLampy
        '
        Me.lvLampy.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvLampy.ContextMenuStrip = Me.ctxSortowanie
        Me.lvLampy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvLampy.FullRowSelect = True
        Me.lvLampy.HideSelection = False
        Me.lvLampy.Location = New System.Drawing.Point(0, 0)
        Me.lvLampy.MultiSelect = False
        Me.lvLampy.Name = "lvLampy"
        Me.lvLampy.Size = New System.Drawing.Size(200, 250)
        Me.lvLampy.TabIndex = 0
        Me.lvLampy.UseCompatibleStateImageBehavior = False
        Me.lvLampy.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Adres"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "X"
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Y"
        '
        'txtLampaY
        '
        Me.txtLampaY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLampaY.Location = New System.Drawing.Point(0, 123)
        Me.txtLampaY.Name = "txtLampaY"
        Me.txtLampaY.Size = New System.Drawing.Size(200, 20)
        Me.txtLampaY.TabIndex = 14
        '
        'txtLampaX
        '
        Me.txtLampaX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLampaX.Location = New System.Drawing.Point(0, 84)
        Me.txtLampaX.Name = "txtLampaX"
        Me.txtLampaX.Size = New System.Drawing.Size(200, 20)
        Me.txtLampaX.TabIndex = 13
        '
        'txtLampaAdres
        '
        Me.txtLampaAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLampaAdres.Location = New System.Drawing.Point(0, 45)
        Me.txtLampaAdres.Name = "txtLampaAdres"
        Me.txtLampaAdres.Size = New System.Drawing.Size(200, 20)
        Me.txtLampaAdres.TabIndex = 12
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(0, 107)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(17, 13)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "Y:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(0, 68)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(17, 13)
        Me.Label25.TabIndex = 3
        Me.Label25.Text = "X:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(0, 29)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(37, 13)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "Adres:"
        '
        'btnLampaUsun
        '
        Me.btnLampaUsun.Location = New System.Drawing.Point(103, 3)
        Me.btnLampaUsun.Name = "btnLampaUsun"
        Me.btnLampaUsun.Size = New System.Drawing.Size(98, 23)
        Me.btnLampaUsun.TabIndex = 11
        Me.btnLampaUsun.Text = "Usuń"
        Me.btnLampaUsun.UseVisualStyleBackColor = True
        '
        'btnLampaDodaj
        '
        Me.btnLampaDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnLampaDodaj.Name = "btnLampaDodaj"
        Me.btnLampaDodaj.Size = New System.Drawing.Size(98, 23)
        Me.btnLampaDodaj.TabIndex = 10
        Me.btnLampaDodaj.Text = "Dodaj"
        Me.btnLampaDodaj.UseVisualStyleBackColor = True
        '
        'tbpPrzejazdy
        '
        Me.tbpPrzejazdy.Controls.Add(Me.splKartaPrzejazdy)
        Me.tbpPrzejazdy.Location = New System.Drawing.Point(4, 22)
        Me.tbpPrzejazdy.Name = "tbpPrzejazdy"
        Me.tbpPrzejazdy.Size = New System.Drawing.Size(206, 644)
        Me.tbpPrzejazdy.TabIndex = 4
        Me.tbpPrzejazdy.Text = "Przejazdy kolejowo-drogowe"
        Me.tbpPrzejazdy.UseVisualStyleBackColor = True
        '
        'splKartaPrzejazdy
        '
        Me.splKartaPrzejazdy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splKartaPrzejazdy.Location = New System.Drawing.Point(3, 3)
        Me.splKartaPrzejazdy.Name = "splKartaPrzejazdy"
        Me.splKartaPrzejazdy.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splKartaPrzejazdy.Panel1
        '
        Me.splKartaPrzejazdy.Panel1.Controls.Add(Me.lvPrzejazdy)
        '
        'splKartaPrzejazdy.Panel2
        '
        Me.splKartaPrzejazdy.Panel2.Controls.Add(Me.btnPrzejazdUsun)
        Me.splKartaPrzejazdy.Panel2.Controls.Add(Me.btnPrzejazdDodaj)
        Me.splKartaPrzejazdy.Panel2.Controls.Add(Me.tabPrzejazd)
        Me.splKartaPrzejazdy.Size = New System.Drawing.Size(200, 638)
        Me.splKartaPrzejazdy.SplitterDistance = 250
        Me.splKartaPrzejazdy.TabIndex = 0
        '
        'lvPrzejazdy
        '
        Me.lvPrzejazdy.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader25, Me.ColumnHeader11, Me.ColumnHeader14, Me.ColumnHeader24, Me.ColumnHeader26, Me.ColumnHeader12, Me.ColumnHeader13})
        Me.lvPrzejazdy.ContextMenuStrip = Me.ctxSortowanie
        Me.lvPrzejazdy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvPrzejazdy.FullRowSelect = True
        Me.lvPrzejazdy.HideSelection = False
        Me.lvPrzejazdy.Location = New System.Drawing.Point(0, 0)
        Me.lvPrzejazdy.MultiSelect = False
        Me.lvPrzejazdy.Name = "lvPrzejazdy"
        Me.lvPrzejazdy.Size = New System.Drawing.Size(200, 250)
        Me.lvPrzejazdy.TabIndex = 0
        Me.lvPrzejazdy.UseCompatibleStateImageBehavior = False
        Me.lvPrzejazdy.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader25
        '
        Me.ColumnHeader25.Text = "Numer"
        Me.ColumnHeader25.Width = 50
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Nazwa"
        Me.ColumnHeader11.Width = 90
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Tryb"
        Me.ColumnHeader14.Width = 40
        '
        'ColumnHeader24
        '
        Me.ColumnHeader24.Text = "Kostki"
        Me.ColumnHeader24.Width = 50
        '
        'ColumnHeader26
        '
        Me.ColumnHeader26.Text = "Automatyzacja"
        Me.ColumnHeader26.Width = 50
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Rogatki"
        Me.ColumnHeader12.Width = 50
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Sygnalizatory drogowe"
        Me.ColumnHeader13.Width = 50
        '
        'btnPrzejazdUsun
        '
        Me.btnPrzejazdUsun.Location = New System.Drawing.Point(103, 3)
        Me.btnPrzejazdUsun.Name = "btnPrzejazdUsun"
        Me.btnPrzejazdUsun.Size = New System.Drawing.Size(98, 23)
        Me.btnPrzejazdUsun.TabIndex = 2
        Me.btnPrzejazdUsun.Text = "Usuń"
        Me.btnPrzejazdUsun.UseVisualStyleBackColor = True
        '
        'btnPrzejazdDodaj
        '
        Me.btnPrzejazdDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnPrzejazdDodaj.Name = "btnPrzejazdDodaj"
        Me.btnPrzejazdDodaj.Size = New System.Drawing.Size(98, 23)
        Me.btnPrzejazdDodaj.TabIndex = 1
        Me.btnPrzejazdDodaj.Text = "Dodaj"
        Me.btnPrzejazdDodaj.UseVisualStyleBackColor = True
        '
        'tabPrzejazd
        '
        Me.tabPrzejazd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabPrzejazd.Controls.Add(Me.tbpPrzejazdOgolne)
        Me.tabPrzejazd.Controls.Add(Me.tbpPrzejazdAutomatyzacja)
        Me.tabPrzejazd.Controls.Add(Me.tbpPrzejazdRogatki)
        Me.tabPrzejazd.Controls.Add(Me.tbpPrzejazdSygnDrog)
        Me.tabPrzejazd.Location = New System.Drawing.Point(0, 32)
        Me.tabPrzejazd.Name = "tabPrzejazd"
        Me.tabPrzejazd.SelectedIndex = 0
        Me.tabPrzejazd.Size = New System.Drawing.Size(202, 349)
        Me.tabPrzejazd.TabIndex = 3
        '
        'tbpPrzejazdOgolne
        '
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label35)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.txtPrzejazdNumer)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label61)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.cbPrzejazdTrybReczny)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.cbPrzejazdTrybAutomatyczny)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label49)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.pnlPrzejazdKolorInny)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.pnlPrzejazdKolorNieprzypisany)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.pnlPrzejazdKolorPrzypisany)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label48)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label47)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label46)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.txtPrzejazdCzasPodnoszenie)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.txtPrzejazdCzasOpuszczanie)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.txtPrzejazdCzasSwiatla)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.txtPrzejazdNazwa)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label45)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label44)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label43)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label42)
        Me.tbpPrzejazdOgolne.Controls.Add(Me.Label41)
        Me.tbpPrzejazdOgolne.Location = New System.Drawing.Point(4, 22)
        Me.tbpPrzejazdOgolne.Name = "tbpPrzejazdOgolne"
        Me.tbpPrzejazdOgolne.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpPrzejazdOgolne.Size = New System.Drawing.Size(194, 323)
        Me.tbpPrzejazdOgolne.TabIndex = 0
        Me.tbpPrzejazdOgolne.Text = "Ogólne"
        Me.tbpPrzejazdOgolne.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoEllipsis = True
        Me.Label35.Location = New System.Drawing.Point(0, 307)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(194, 112)
        Me.Label35.TabIndex = 20
        Me.Label35.Text = "Aby dodać kostkę przejazdu, należy zaznaczyć przejazd na liście i kliknąć na kost" &
    "kę na pulpicie."
        '
        'txtPrzejazdNumer
        '
        Me.txtPrzejazdNumer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzejazdNumer.Location = New System.Drawing.Point(0, 19)
        Me.txtPrzejazdNumer.Name = "txtPrzejazdNumer"
        Me.txtPrzejazdNumer.Size = New System.Drawing.Size(192, 20)
        Me.txtPrzejazdNumer.TabIndex = 6
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(0, 3)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(41, 13)
        Me.Label61.TabIndex = 19
        Me.Label61.Text = "Numer:"
        '
        'cbPrzejazdTrybReczny
        '
        Me.cbPrzejazdTrybReczny.AutoSize = True
        Me.cbPrzejazdTrybReczny.Location = New System.Drawing.Point(97, 97)
        Me.cbPrzejazdTrybReczny.Name = "cbPrzejazdTrybReczny"
        Me.cbPrzejazdTrybReczny.Size = New System.Drawing.Size(57, 17)
        Me.cbPrzejazdTrybReczny.TabIndex = 9
        Me.cbPrzejazdTrybReczny.Text = "ręczny"
        Me.cbPrzejazdTrybReczny.UseVisualStyleBackColor = True
        '
        'cbPrzejazdTrybAutomatyczny
        '
        Me.cbPrzejazdTrybAutomatyczny.AutoSize = True
        Me.cbPrzejazdTrybAutomatyczny.Location = New System.Drawing.Point(0, 97)
        Me.cbPrzejazdTrybAutomatyczny.Name = "cbPrzejazdTrybAutomatyczny"
        Me.cbPrzejazdTrybAutomatyczny.Size = New System.Drawing.Size(91, 17)
        Me.cbPrzejazdTrybAutomatyczny.TabIndex = 8
        Me.cbPrzejazdTrybAutomatyczny.Text = "automatyczny"
        Me.cbPrzejazdTrybAutomatyczny.UseVisualStyleBackColor = True
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(22, 291)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(214, 13)
        Me.Label49.TabIndex = 18
        Me.Label49.Text = "Kostka nieprzypisana do żadnego przejazdu"
        '
        'pnlPrzejazdKolorInny
        '
        Me.pnlPrzejazdKolorInny.Location = New System.Drawing.Point(3, 272)
        Me.pnlPrzejazdKolorInny.Name = "pnlPrzejazdKolorInny"
        Me.pnlPrzejazdKolorInny.Size = New System.Drawing.Size(13, 13)
        Me.pnlPrzejazdKolorInny.TabIndex = 17
        '
        'pnlPrzejazdKolorNieprzypisany
        '
        Me.pnlPrzejazdKolorNieprzypisany.Location = New System.Drawing.Point(3, 291)
        Me.pnlPrzejazdKolorNieprzypisany.Name = "pnlPrzejazdKolorNieprzypisany"
        Me.pnlPrzejazdKolorNieprzypisany.Size = New System.Drawing.Size(13, 13)
        Me.pnlPrzejazdKolorNieprzypisany.TabIndex = 16
        '
        'pnlPrzejazdKolorPrzypisany
        '
        Me.pnlPrzejazdKolorPrzypisany.Location = New System.Drawing.Point(3, 253)
        Me.pnlPrzejazdKolorPrzypisany.Name = "pnlPrzejazdKolorPrzypisany"
        Me.pnlPrzejazdKolorPrzypisany.Size = New System.Drawing.Size(13, 13)
        Me.pnlPrzejazdKolorPrzypisany.TabIndex = 15
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(22, 272)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(191, 13)
        Me.Label48.TabIndex = 14
        Me.Label48.Text = "Kostka przypisana do innego przejazdu"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(22, 253)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(180, 13)
        Me.Label47.TabIndex = 13
        Me.Label47.Text = "Kostka przypisana do tego przejazdu"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(0, 237)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(49, 13)
        Me.Label46.TabIndex = 12
        Me.Label46.Text = "Legenda"
        '
        'txtPrzejazdCzasPodnoszenie
        '
        Me.txtPrzejazdCzasPodnoszenie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzejazdCzasPodnoszenie.Location = New System.Drawing.Point(0, 211)
        Me.txtPrzejazdCzasPodnoszenie.Name = "txtPrzejazdCzasPodnoszenie"
        Me.txtPrzejazdCzasPodnoszenie.Size = New System.Drawing.Size(192, 20)
        Me.txtPrzejazdCzasPodnoszenie.TabIndex = 12
        '
        'txtPrzejazdCzasOpuszczanie
        '
        Me.txtPrzejazdCzasOpuszczanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzejazdCzasOpuszczanie.Location = New System.Drawing.Point(0, 172)
        Me.txtPrzejazdCzasOpuszczanie.Name = "txtPrzejazdCzasOpuszczanie"
        Me.txtPrzejazdCzasOpuszczanie.Size = New System.Drawing.Size(192, 20)
        Me.txtPrzejazdCzasOpuszczanie.TabIndex = 11
        '
        'txtPrzejazdCzasSwiatla
        '
        Me.txtPrzejazdCzasSwiatla.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzejazdCzasSwiatla.Location = New System.Drawing.Point(0, 133)
        Me.txtPrzejazdCzasSwiatla.Name = "txtPrzejazdCzasSwiatla"
        Me.txtPrzejazdCzasSwiatla.Size = New System.Drawing.Size(192, 20)
        Me.txtPrzejazdCzasSwiatla.TabIndex = 10
        '
        'txtPrzejazdNazwa
        '
        Me.txtPrzejazdNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrzejazdNazwa.Location = New System.Drawing.Point(0, 58)
        Me.txtPrzejazdNazwa.Name = "txtPrzejazdNazwa"
        Me.txtPrzejazdNazwa.Size = New System.Drawing.Size(192, 20)
        Me.txtPrzejazdNazwa.TabIndex = 7
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(0, 195)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(157, 13)
        Me.Label45.TabIndex = 4
        Me.Label45.Text = "Czas podnoszenia rogatek [ms]:"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(0, 156)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(156, 13)
        Me.Label44.TabIndex = 3
        Me.Label44.Text = "Czas opuszczania rogatek [ms]:"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(0, 117)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(269, 13)
        Me.Label43.TabIndex = 2
        Me.Label43.Text = "Czas migania świateł przed opuszczeniem rogatek [ms]:"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(0, 81)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(31, 13)
        Me.Label42.TabIndex = 1
        Me.Label42.Text = "Tryb:"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(0, 42)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(43, 13)
        Me.Label41.TabIndex = 0
        Me.Label41.Text = "Nazwa:"
        '
        'tbpPrzejazdAutomatyzacja
        '
        Me.tbpPrzejazdAutomatyzacja.Controls.Add(Me.splPrzejazdAutomatyzacja)
        Me.tbpPrzejazdAutomatyzacja.Location = New System.Drawing.Point(4, 22)
        Me.tbpPrzejazdAutomatyzacja.Name = "tbpPrzejazdAutomatyzacja"
        Me.tbpPrzejazdAutomatyzacja.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpPrzejazdAutomatyzacja.Size = New System.Drawing.Size(194, 323)
        Me.tbpPrzejazdAutomatyzacja.TabIndex = 1
        Me.tbpPrzejazdAutomatyzacja.Text = "Automatyzacja"
        Me.tbpPrzejazdAutomatyzacja.UseVisualStyleBackColor = True
        '
        'splPrzejazdAutomatyzacja
        '
        Me.splPrzejazdAutomatyzacja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splPrzejazdAutomatyzacja.Location = New System.Drawing.Point(0, 0)
        Me.splPrzejazdAutomatyzacja.Name = "splPrzejazdAutomatyzacja"
        Me.splPrzejazdAutomatyzacja.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splPrzejazdAutomatyzacja.Panel1
        '
        Me.splPrzejazdAutomatyzacja.Panel1.Controls.Add(Me.lvPrzejazdAutomatyzacja)
        '
        'splPrzejazdAutomatyzacja.Panel2
        '
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.pnlPrzejazdAutomatyzacjaKolorSygnalizator)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.pnlPrzejazdAutomatyzacjaKolorPrzyjazd)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.pnlPrzejazdAutomatyzacjaKolorWyjazd)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.cboPrzejazdAutomatyzacjaSygnalizator)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.Label58)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.Label57)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.Label56)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.btnPrzejazdAutomatyzacjaUsun)
        Me.splPrzejazdAutomatyzacja.Panel2.Controls.Add(Me.btnPrzejazdAutomatyzacjaDodaj)
        Me.splPrzejazdAutomatyzacja.Size = New System.Drawing.Size(194, 323)
        Me.splPrzejazdAutomatyzacja.SplitterDistance = 150
        Me.splPrzejazdAutomatyzacja.TabIndex = 0
        '
        'lvPrzejazdAutomatyzacja
        '
        Me.lvPrzejazdAutomatyzacja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPrzejazdAutomatyzacja.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader21, Me.ColumnHeader22, Me.ColumnHeader23})
        Me.lvPrzejazdAutomatyzacja.ContextMenuStrip = Me.ctxSortowaniePrzejazdy
        Me.lvPrzejazdAutomatyzacja.FullRowSelect = True
        Me.lvPrzejazdAutomatyzacja.HideSelection = False
        Me.lvPrzejazdAutomatyzacja.Location = New System.Drawing.Point(0, 2)
        Me.lvPrzejazdAutomatyzacja.MultiSelect = False
        Me.lvPrzejazdAutomatyzacja.Name = "lvPrzejazdAutomatyzacja"
        Me.lvPrzejazdAutomatyzacja.Size = New System.Drawing.Size(192, 148)
        Me.lvPrzejazdAutomatyzacja.TabIndex = 0
        Me.lvPrzejazdAutomatyzacja.UseCompatibleStateImageBehavior = False
        Me.lvPrzejazdAutomatyzacja.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "Wyjazd z odcinka"
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Przyjazd do odcinka"
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "Sygnalizator"
        '
        'ctxSortowaniePrzejazdy
        '
        Me.ctxSortowaniePrzejazdy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctmSortujPrzejazdy})
        Me.ctxSortowaniePrzejazdy.Name = "ctxSortowaniePrzejazdy"
        Me.ctxSortowaniePrzejazdy.Size = New System.Drawing.Size(106, 26)
        '
        'ctmSortujPrzejazdy
        '
        Me.ctmSortujPrzejazdy.Name = "ctmSortujPrzejazdy"
        Me.ctmSortujPrzejazdy.Size = New System.Drawing.Size(105, 22)
        Me.ctmSortujPrzejazdy.Text = "Sortuj"
        '
        'pnlPrzejazdAutomatyzacjaKolorSygnalizator
        '
        Me.pnlPrzejazdAutomatyzacjaKolorSygnalizator.Location = New System.Drawing.Point(114, 109)
        Me.pnlPrzejazdAutomatyzacjaKolorSygnalizator.Name = "pnlPrzejazdAutomatyzacjaKolorSygnalizator"
        Me.pnlPrzejazdAutomatyzacjaKolorSygnalizator.Size = New System.Drawing.Size(13, 13)
        Me.pnlPrzejazdAutomatyzacjaKolorSygnalizator.TabIndex = 10
        '
        'pnlPrzejazdAutomatyzacjaKolorPrzyjazd
        '
        Me.pnlPrzejazdAutomatyzacjaKolorPrzyjazd.Location = New System.Drawing.Point(114, 69)
        Me.pnlPrzejazdAutomatyzacjaKolorPrzyjazd.Name = "pnlPrzejazdAutomatyzacjaKolorPrzyjazd"
        Me.pnlPrzejazdAutomatyzacjaKolorPrzyjazd.Size = New System.Drawing.Size(13, 13)
        Me.pnlPrzejazdAutomatyzacjaKolorPrzyjazd.TabIndex = 9
        '
        'pnlPrzejazdAutomatyzacjaKolorWyjazd
        '
        Me.pnlPrzejazdAutomatyzacjaKolorWyjazd.Location = New System.Drawing.Point(114, 29)
        Me.pnlPrzejazdAutomatyzacjaKolorWyjazd.Name = "pnlPrzejazdAutomatyzacjaKolorWyjazd"
        Me.pnlPrzejazdAutomatyzacjaKolorWyjazd.Size = New System.Drawing.Size(13, 13)
        Me.pnlPrzejazdAutomatyzacjaKolorWyjazd.TabIndex = 8
        '
        'cboPrzejazdAutomatyzacjaSygnalizator
        '
        Me.cboPrzejazdAutomatyzacjaSygnalizator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrzejazdAutomatyzacjaSygnalizator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrzejazdAutomatyzacjaSygnalizator.FormattingEnabled = True
        Me.cboPrzejazdAutomatyzacjaSygnalizator.Location = New System.Drawing.Point(0, 125)
        Me.cboPrzejazdAutomatyzacjaSygnalizator.Name = "cboPrzejazdAutomatyzacjaSygnalizator"
        Me.cboPrzejazdAutomatyzacjaSygnalizator.Size = New System.Drawing.Size(192, 21)
        Me.cboPrzejazdAutomatyzacjaSygnalizator.TabIndex = 7
        '
        'cboPrzejazdAutomatyzacjaOdcinekPrzyjazd
        '
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.FormattingEnabled = True
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.Location = New System.Drawing.Point(0, 85)
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.Name = "cboPrzejazdAutomatyzacjaOdcinekPrzyjazd"
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.Size = New System.Drawing.Size(192, 21)
        Me.cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.TabIndex = 6
        '
        'cboPrzejazdAutomatyzacjaOdcinekWyjazd
        '
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.FormattingEnabled = True
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.Location = New System.Drawing.Point(0, 45)
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.Name = "cboPrzejazdAutomatyzacjaOdcinekWyjazd"
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.Size = New System.Drawing.Size(192, 21)
        Me.cboPrzejazdAutomatyzacjaOdcinekWyjazd.TabIndex = 5
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(0, 109)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(67, 13)
        Me.Label58.TabIndex = 4
        Me.Label58.Text = "Sygnalizator:"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(0, 69)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(105, 13)
        Me.Label57.TabIndex = 3
        Me.Label57.Text = "Przyjazd do odcinka:"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(0, 29)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(94, 13)
        Me.Label56.TabIndex = 2
        Me.Label56.Text = "Wyjazd z odcinka:"
        '
        'btnPrzejazdAutomatyzacjaUsun
        '
        Me.btnPrzejazdAutomatyzacjaUsun.Location = New System.Drawing.Point(98, 3)
        Me.btnPrzejazdAutomatyzacjaUsun.Name = "btnPrzejazdAutomatyzacjaUsun"
        Me.btnPrzejazdAutomatyzacjaUsun.Size = New System.Drawing.Size(95, 23)
        Me.btnPrzejazdAutomatyzacjaUsun.TabIndex = 1
        Me.btnPrzejazdAutomatyzacjaUsun.Text = "Usuń"
        Me.btnPrzejazdAutomatyzacjaUsun.UseVisualStyleBackColor = True
        '
        'btnPrzejazdAutomatyzacjaDodaj
        '
        Me.btnPrzejazdAutomatyzacjaDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnPrzejazdAutomatyzacjaDodaj.Name = "btnPrzejazdAutomatyzacjaDodaj"
        Me.btnPrzejazdAutomatyzacjaDodaj.Size = New System.Drawing.Size(95, 23)
        Me.btnPrzejazdAutomatyzacjaDodaj.TabIndex = 0
        Me.btnPrzejazdAutomatyzacjaDodaj.Text = "Dodaj"
        Me.btnPrzejazdAutomatyzacjaDodaj.UseVisualStyleBackColor = True
        '
        'tbpPrzejazdRogatki
        '
        Me.tbpPrzejazdRogatki.Controls.Add(Me.splPrzejazdRogatki)
        Me.tbpPrzejazdRogatki.Location = New System.Drawing.Point(4, 22)
        Me.tbpPrzejazdRogatki.Name = "tbpPrzejazdRogatki"
        Me.tbpPrzejazdRogatki.Size = New System.Drawing.Size(194, 323)
        Me.tbpPrzejazdRogatki.TabIndex = 2
        Me.tbpPrzejazdRogatki.Text = "Rogatki"
        Me.tbpPrzejazdRogatki.UseVisualStyleBackColor = True
        '
        'splPrzejazdRogatki
        '
        Me.splPrzejazdRogatki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splPrzejazdRogatki.Location = New System.Drawing.Point(0, 0)
        Me.splPrzejazdRogatki.Name = "splPrzejazdRogatki"
        Me.splPrzejazdRogatki.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splPrzejazdRogatki.Panel1
        '
        Me.splPrzejazdRogatki.Panel1.Controls.Add(Me.lvPrzejazdRogatki)
        '
        'splPrzejazdRogatki.Panel2
        '
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.Label18)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.txtPrzejazdRogatkaCzasDoZamkniecia)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.btnPrzejazdRogatkaDodaj)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.txtPrzejazdRogatkaY)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.btnPrzejazdRogatkaUsun)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.txtPrzejazdRogatkaX)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.Label51)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.txtPrzejazdRogatkaAdres)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.Label52)
        Me.splPrzejazdRogatki.Panel2.Controls.Add(Me.Label50)
        Me.splPrzejazdRogatki.Size = New System.Drawing.Size(194, 323)
        Me.splPrzejazdRogatki.SplitterDistance = 150
        Me.splPrzejazdRogatki.TabIndex = 9
        '
        'lvPrzejazdRogatki
        '
        Me.lvPrzejazdRogatki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPrzejazdRogatki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader15, Me.ColumnHeader16, Me.ColumnHeader17})
        Me.lvPrzejazdRogatki.ContextMenuStrip = Me.ctxSortowaniePrzejazdy
        Me.lvPrzejazdRogatki.FullRowSelect = True
        Me.lvPrzejazdRogatki.HideSelection = False
        Me.lvPrzejazdRogatki.Location = New System.Drawing.Point(0, 2)
        Me.lvPrzejazdRogatki.MultiSelect = False
        Me.lvPrzejazdRogatki.Name = "lvPrzejazdRogatki"
        Me.lvPrzejazdRogatki.Size = New System.Drawing.Size(192, 148)
        Me.lvPrzejazdRogatki.TabIndex = 4
        Me.lvPrzejazdRogatki.UseCompatibleStateImageBehavior = False
        Me.lvPrzejazdRogatki.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Adres"
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "X"
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "Y"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(149, 29)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(42, 13)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "Czekaj:"
        '
        'txtPrzejazdRogatkaCzasDoZamkniecia
        '
        Me.txtPrzejazdRogatkaCzasDoZamkniecia.Location = New System.Drawing.Point(149, 45)
        Me.txtPrzejazdRogatkaCzasDoZamkniecia.Name = "txtPrzejazdRogatkaCzasDoZamkniecia"
        Me.txtPrzejazdRogatkaCzasDoZamkniecia.Size = New System.Drawing.Size(43, 20)
        Me.txtPrzejazdRogatkaCzasDoZamkniecia.TabIndex = 10
        '
        'btnPrzejazdRogatkaDodaj
        '
        Me.btnPrzejazdRogatkaDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnPrzejazdRogatkaDodaj.Name = "btnPrzejazdRogatkaDodaj"
        Me.btnPrzejazdRogatkaDodaj.Size = New System.Drawing.Size(95, 23)
        Me.btnPrzejazdRogatkaDodaj.TabIndex = 5
        Me.btnPrzejazdRogatkaDodaj.Text = "Dodaj"
        Me.btnPrzejazdRogatkaDodaj.UseVisualStyleBackColor = True
        '
        'txtPrzejazdRogatkaY
        '
        Me.txtPrzejazdRogatkaY.Location = New System.Drawing.Point(99, 45)
        Me.txtPrzejazdRogatkaY.Name = "txtPrzejazdRogatkaY"
        Me.txtPrzejazdRogatkaY.Size = New System.Drawing.Size(44, 20)
        Me.txtPrzejazdRogatkaY.TabIndex = 9
        '
        'btnPrzejazdRogatkaUsun
        '
        Me.btnPrzejazdRogatkaUsun.Location = New System.Drawing.Point(98, 3)
        Me.btnPrzejazdRogatkaUsun.Name = "btnPrzejazdRogatkaUsun"
        Me.btnPrzejazdRogatkaUsun.Size = New System.Drawing.Size(95, 23)
        Me.btnPrzejazdRogatkaUsun.TabIndex = 6
        Me.btnPrzejazdRogatkaUsun.Text = "Usuń"
        Me.btnPrzejazdRogatkaUsun.UseVisualStyleBackColor = True
        '
        'txtPrzejazdRogatkaX
        '
        Me.txtPrzejazdRogatkaX.Location = New System.Drawing.Point(49, 45)
        Me.txtPrzejazdRogatkaX.Name = "txtPrzejazdRogatkaX"
        Me.txtPrzejazdRogatkaX.Size = New System.Drawing.Size(44, 20)
        Me.txtPrzejazdRogatkaX.TabIndex = 8
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(49, 29)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(17, 13)
        Me.Label51.TabIndex = 1
        Me.Label51.Text = "X:"
        '
        'txtPrzejazdRogatkaAdres
        '
        Me.txtPrzejazdRogatkaAdres.Location = New System.Drawing.Point(0, 45)
        Me.txtPrzejazdRogatkaAdres.Name = "txtPrzejazdRogatkaAdres"
        Me.txtPrzejazdRogatkaAdres.Size = New System.Drawing.Size(43, 20)
        Me.txtPrzejazdRogatkaAdres.TabIndex = 7
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(99, 29)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(17, 13)
        Me.Label52.TabIndex = 2
        Me.Label52.Text = "Y:"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(0, 29)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(37, 13)
        Me.Label50.TabIndex = 0
        Me.Label50.Text = "Adres:"
        '
        'tbpPrzejazdSygnDrog
        '
        Me.tbpPrzejazdSygnDrog.Controls.Add(Me.splPrzejazdSygnDrog)
        Me.tbpPrzejazdSygnDrog.Location = New System.Drawing.Point(4, 22)
        Me.tbpPrzejazdSygnDrog.Name = "tbpPrzejazdSygnDrog"
        Me.tbpPrzejazdSygnDrog.Size = New System.Drawing.Size(194, 323)
        Me.tbpPrzejazdSygnDrog.TabIndex = 3
        Me.tbpPrzejazdSygnDrog.Text = "Sygnalizatory drogowe"
        Me.tbpPrzejazdSygnDrog.UseVisualStyleBackColor = True
        '
        'splPrzejazdSygnDrog
        '
        Me.splPrzejazdSygnDrog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splPrzejazdSygnDrog.Location = New System.Drawing.Point(0, 0)
        Me.splPrzejazdSygnDrog.Name = "splPrzejazdSygnDrog"
        Me.splPrzejazdSygnDrog.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splPrzejazdSygnDrog.Panel1
        '
        Me.splPrzejazdSygnDrog.Panel1.Controls.Add(Me.lvPrzejazdSygnDrog)
        '
        'splPrzejazdSygnDrog.Panel2
        '
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.txtPrzejazdSygnDrogY)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.txtPrzejazdSygnDrogX)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.txtPrzejazdSygnDrogAdres)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.Label55)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.Label54)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.Label53)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.btnPrzejazdSygnDrogUsun)
        Me.splPrzejazdSygnDrog.Panel2.Controls.Add(Me.btnPrzejazdSygnDrogDodaj)
        Me.splPrzejazdSygnDrog.Size = New System.Drawing.Size(194, 323)
        Me.splPrzejazdSygnDrog.SplitterDistance = 150
        Me.splPrzejazdSygnDrog.TabIndex = 0
        '
        'lvPrzejazdSygnDrog
        '
        Me.lvPrzejazdSygnDrog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPrzejazdSygnDrog.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader20})
        Me.lvPrzejazdSygnDrog.ContextMenuStrip = Me.ctxSortowaniePrzejazdy
        Me.lvPrzejazdSygnDrog.FullRowSelect = True
        Me.lvPrzejazdSygnDrog.HideSelection = False
        Me.lvPrzejazdSygnDrog.Location = New System.Drawing.Point(0, 2)
        Me.lvPrzejazdSygnDrog.MultiSelect = False
        Me.lvPrzejazdSygnDrog.Name = "lvPrzejazdSygnDrog"
        Me.lvPrzejazdSygnDrog.Size = New System.Drawing.Size(192, 148)
        Me.lvPrzejazdSygnDrog.TabIndex = 4
        Me.lvPrzejazdSygnDrog.UseCompatibleStateImageBehavior = False
        Me.lvPrzejazdSygnDrog.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Adres"
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "X"
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "Y"
        '
        'txtPrzejazdSygnDrogY
        '
        Me.txtPrzejazdSygnDrogY.Location = New System.Drawing.Point(132, 45)
        Me.txtPrzejazdSygnDrogY.Name = "txtPrzejazdSygnDrogY"
        Me.txtPrzejazdSygnDrogY.Size = New System.Drawing.Size(60, 20)
        Me.txtPrzejazdSygnDrogY.TabIndex = 9
        '
        'txtPrzejazdSygnDrogX
        '
        Me.txtPrzejazdSygnDrogX.Location = New System.Drawing.Point(66, 45)
        Me.txtPrzejazdSygnDrogX.Name = "txtPrzejazdSygnDrogX"
        Me.txtPrzejazdSygnDrogX.Size = New System.Drawing.Size(60, 20)
        Me.txtPrzejazdSygnDrogX.TabIndex = 8
        '
        'txtPrzejazdSygnDrogAdres
        '
        Me.txtPrzejazdSygnDrogAdres.Location = New System.Drawing.Point(0, 45)
        Me.txtPrzejazdSygnDrogAdres.Name = "txtPrzejazdSygnDrogAdres"
        Me.txtPrzejazdSygnDrogAdres.Size = New System.Drawing.Size(60, 20)
        Me.txtPrzejazdSygnDrogAdres.TabIndex = 7
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(132, 29)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(17, 13)
        Me.Label55.TabIndex = 4
        Me.Label55.Text = "Y:"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(66, 29)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(17, 13)
        Me.Label54.TabIndex = 3
        Me.Label54.Text = "X:"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(0, 29)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(37, 13)
        Me.Label53.TabIndex = 2
        Me.Label53.Text = "Adres:"
        '
        'btnPrzejazdSygnDrogUsun
        '
        Me.btnPrzejazdSygnDrogUsun.Location = New System.Drawing.Point(98, 3)
        Me.btnPrzejazdSygnDrogUsun.Name = "btnPrzejazdSygnDrogUsun"
        Me.btnPrzejazdSygnDrogUsun.Size = New System.Drawing.Size(95, 23)
        Me.btnPrzejazdSygnDrogUsun.TabIndex = 6
        Me.btnPrzejazdSygnDrogUsun.Text = "Usuń"
        Me.btnPrzejazdSygnDrogUsun.UseVisualStyleBackColor = True
        '
        'btnPrzejazdSygnDrogDodaj
        '
        Me.btnPrzejazdSygnDrogDodaj.Location = New System.Drawing.Point(-1, 3)
        Me.btnPrzejazdSygnDrogDodaj.Name = "btnPrzejazdSygnDrogDodaj"
        Me.btnPrzejazdSygnDrogDodaj.Size = New System.Drawing.Size(95, 23)
        Me.btnPrzejazdSygnDrogDodaj.TabIndex = 5
        Me.btnPrzejazdSygnDrogDodaj.Text = "Dodaj"
        Me.btnPrzejazdSygnDrogDodaj.UseVisualStyleBackColor = True
        '
        'pnlKonfBudynek
        '
        Me.pnlKonfBudynek.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlKonfBudynek.Controls.Add(Me.cboKonfBudynekRodzaj)
        Me.pnlKonfBudynek.Controls.Add(Me.txtKonfBudynekTekst)
        Me.pnlKonfBudynek.Controls.Add(Me.Label62)
        Me.pnlKonfBudynek.Controls.Add(Me.Label23)
        Me.pnlKonfBudynek.Location = New System.Drawing.Point(8, 10)
        Me.pnlKonfBudynek.Name = "pnlKonfBudynek"
        Me.pnlKonfBudynek.Size = New System.Drawing.Size(124, 82)
        Me.pnlKonfBudynek.TabIndex = 1
        Me.pnlKonfBudynek.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(37, 13)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Tekst:"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(0, 39)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(43, 13)
        Me.Label62.TabIndex = 1
        Me.Label62.Text = "Rodzaj:"
        '
        'txtKonfBudynekTekst
        '
        Me.txtKonfBudynekTekst.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKonfBudynekTekst.Location = New System.Drawing.Point(0, 16)
        Me.txtKonfBudynekTekst.Name = "txtKonfBudynekTekst"
        Me.txtKonfBudynekTekst.Size = New System.Drawing.Size(124, 20)
        Me.txtKonfBudynekTekst.TabIndex = 2
        '
        'cboKonfBudynekRodzaj
        '
        Me.cboKonfBudynekRodzaj.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboKonfBudynekRodzaj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKonfBudynekRodzaj.FormattingEnabled = True
        Me.cboKonfBudynekRodzaj.Location = New System.Drawing.Point(0, 55)
        Me.cboKonfBudynekRodzaj.Name = "cboKonfBudynekRodzaj"
        Me.cboKonfBudynekRodzaj.Size = New System.Drawing.Size(124, 21)
        Me.cboKonfBudynekRodzaj.TabIndex = 3
        '
        'plpPulpit
        '
        Me.plpPulpit.AllowDrop = True
        Me.plpPulpit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plpPulpit.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.plpPulpit.Location = New System.Drawing.Point(0, 0)
        Me.plpPulpit.MozliwoscZaznaczeniaLamp = False
        Me.plpPulpit.MozliwoscZaznaczeniaOdcinka = False
        Me.plpPulpit.Name = "plpPulpit"
        Me.plpPulpit.projDodatkoweObiekty = Pulpit.Dodatki.RysujDodatkoweObiekty.Nic
        Me.plpPulpit.projZaznaczonaLampa = Nothing
        Me.plpPulpit.projZaznaczonyLicznik = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazd = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazdAutomatyzacja = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazdRogatka = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazdSygnDrog = Nothing
        Me.plpPulpit.Przesuniecie = New System.Drawing.Point(0, 0)
        Pulpit1.Adres = CType(0US, UShort)
        Pulpit1.Nazwa = ""
        Pulpit1.SkrotTelegraficzny = ""
        Pulpit1.Typ = Zaleznosci.TypPosterunku.Inny
        Me.plpPulpit.Pulpit = Pulpit1
        Me.plpPulpit.Size = New System.Drawing.Size(790, 645)
        Me.plpPulpit.TabIndex = 4
        Me.plpPulpit.TrybProjektowy = True
        Me.plpPulpit.WarunekZaznaczeniaKostki = Nothing
        Me.plpPulpit.ZaznaczonaKostka = Nothing
        Me.plpPulpit.ZaznaczonyOdcinek = Nothing
        '
        'wndProjektantPosterunku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 694)
        Me.Controls.Add(Me.splOkno)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "wndProjektantPosterunku"
        Me.Text = "Projektant posterunku ruchu"
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        Me.splOkno.Panel1.ResumeLayout(False)
        Me.splOkno.Panel1.PerformLayout()
        Me.splOkno.Panel2.ResumeLayout(False)
        CType(Me.splOkno, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splOkno.ResumeLayout(False)
        Me.tabUstawienia.ResumeLayout(False)
        Me.tbpPulpit.ResumeLayout(False)
        Me.splKartaPulpit.Panel1.ResumeLayout(False)
        Me.splKartaPulpit.Panel2.ResumeLayout(False)
        CType(Me.splKartaPulpit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaPulpit.ResumeLayout(False)
        Me.pnlKonfSygnInfSygnPowtarzany.ResumeLayout(False)
        Me.pnlKonfSygnInfSygnPowtarzany.PerformLayout()
        Me.pnlKonfSygnPowtKolejnosc.ResumeLayout(False)
        Me.pnlKonfSygnPowtKolejnosc.PerformLayout()
        Me.pnlKonfSygnInfSwiatla.ResumeLayout(False)
        Me.pnlKonfSygnInfSwiatla.PerformLayout()
        Me.pnlKonfSygnPolsamUstawienia.ResumeLayout(False)
        Me.pnlKonfSygnPolsamUstawienia.PerformLayout()
        Me.pnlKonfTorPodwojny.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pnlKonfSygnPolsamSwiatla.ResumeLayout(False)
        Me.pnlKonfSygnPolsamSwiatla.PerformLayout()
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
        Me.pnlKonfAdres.ResumeLayout(False)
        Me.pnlKonfAdres.PerformLayout()
        Me.pnlKonfSygnOdcNast.ResumeLayout(False)
        Me.pnlKonfSygnOdcNast.PerformLayout()
        Me.pnlKonfPrzycisk.ResumeLayout(False)
        Me.pnlKonfPrzycisk.PerformLayout()
        Me.pnlKonfPosiadaPrzycisk.ResumeLayout(False)
        Me.pnlKonfPosiadaPrzycisk.PerformLayout()
        Me.pnlKonfNazwa.ResumeLayout(False)
        Me.pnlKonfNazwa.PerformLayout()
        Me.pnlKonfKier.ResumeLayout(False)
        Me.pnlKonfKier.PerformLayout()
        Me.pnlKonfNapis.ResumeLayout(False)
        Me.pnlKonfNapis.PerformLayout()
        Me.tbpOdcinki.ResumeLayout(False)
        Me.splKartaTory.Panel1.ResumeLayout(False)
        Me.splKartaTory.Panel2.ResumeLayout(False)
        Me.splKartaTory.Panel2.PerformLayout()
        CType(Me.splKartaTory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaTory.ResumeLayout(False)
        Me.ctxSortowanie.ResumeLayout(False)
        Me.pnlTorLegenda.ResumeLayout(False)
        Me.pnlTorLegenda.PerformLayout()
        Me.tbpLiczniki.ResumeLayout(False)
        Me.splKartaLiczniki.Panel1.ResumeLayout(False)
        Me.splKartaLiczniki.Panel2.ResumeLayout(False)
        CType(Me.splKartaLiczniki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaLiczniki.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbpLampy.ResumeLayout(False)
        Me.splKartaLampy.Panel1.ResumeLayout(False)
        Me.splKartaLampy.Panel2.ResumeLayout(False)
        Me.splKartaLampy.Panel2.PerformLayout()
        CType(Me.splKartaLampy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaLampy.ResumeLayout(False)
        Me.tbpPrzejazdy.ResumeLayout(False)
        Me.splKartaPrzejazdy.Panel1.ResumeLayout(False)
        Me.splKartaPrzejazdy.Panel2.ResumeLayout(False)
        CType(Me.splKartaPrzejazdy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splKartaPrzejazdy.ResumeLayout(False)
        Me.tabPrzejazd.ResumeLayout(False)
        Me.tbpPrzejazdOgolne.ResumeLayout(False)
        Me.tbpPrzejazdOgolne.PerformLayout()
        Me.tbpPrzejazdAutomatyzacja.ResumeLayout(False)
        Me.splPrzejazdAutomatyzacja.Panel1.ResumeLayout(False)
        Me.splPrzejazdAutomatyzacja.Panel2.ResumeLayout(False)
        Me.splPrzejazdAutomatyzacja.Panel2.PerformLayout()
        CType(Me.splPrzejazdAutomatyzacja, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splPrzejazdAutomatyzacja.ResumeLayout(False)
        Me.ctxSortowaniePrzejazdy.ResumeLayout(False)
        Me.tbpPrzejazdRogatki.ResumeLayout(False)
        Me.splPrzejazdRogatki.Panel1.ResumeLayout(False)
        Me.splPrzejazdRogatki.Panel2.ResumeLayout(False)
        Me.splPrzejazdRogatki.Panel2.PerformLayout()
        CType(Me.splPrzejazdRogatki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splPrzejazdRogatki.ResumeLayout(False)
        Me.tbpPrzejazdSygnDrog.ResumeLayout(False)
        Me.splPrzejazdSygnDrog.Panel1.ResumeLayout(False)
        Me.splPrzejazdSygnDrog.Panel2.ResumeLayout(False)
        Me.splPrzejazdSygnDrog.Panel2.PerformLayout()
        CType(Me.splPrzejazdSygnDrog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splPrzejazdSygnDrog.ResumeLayout(False)
        Me.pnlKonfBudynek.ResumeLayout(False)
        Me.pnlKonfBudynek.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mnuMenu As MenuStrip
    Friend WithEvents mnuNarzedzia As ToolStripMenuItem
    Friend WithEvents mnuDodajKostki As ToolStripMenuItem
    Friend WithEvents mnuUsunKostki As ToolStripMenuItem
    Friend WithEvents splOkno As SplitContainer
    Friend WithEvents lvPulpitKostki As ListView
    Friend WithEvents imlKostki As ImageList
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mnuDanePosterunku As ToolStripMenuItem
    Friend WithEvents tabUstawienia As TabControl
    Friend WithEvents tbpPulpit As TabPage
    Friend WithEvents tbpOdcinki As TabPage
    Friend WithEvents tbpLiczniki As TabPage
    Friend WithEvents splKartaPulpit As SplitContainer
    Friend WithEvents splKartaTory As SplitContainer
    Friend WithEvents lvOdcinki As ListView
    Friend WithEvents Label3 As Label
    Friend WithEvents txtOdcinekOpis As TextBox
    Friend WithEvents txtOdcinekNazwa As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnOdcinekDodaj As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents pnlTorKolorTenOdcinek As Panel
    Friend WithEvents btnOdcinekUsun As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents pnlTorKolorNieprzypisany As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents pnlTorKolorInnyOdcinek As Panel
    Friend WithEvents pnlTorLegenda As Panel
    Friend WithEvents txtOdcinekAdres As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents splKartaLiczniki As SplitContainer
    Friend WithEvents lvLiczniki As ListView
    Friend WithEvents txtLicznik1Adres As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cboLicznikOdcinek2 As ComboBox
    Friend WithEvents cboLicznikOdcinek1 As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents pnlKonfTor As Panel
    Friend WithEvents txtKonfTorPredkosc As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents pnlKonfRozjazd As Panel
    Friend WithEvents txtKonfTorPodwPredk2 As TextBox
    Friend WithEvents txtKonfTorPodwPredk1 As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents pnlKonfSygnPolsamSwiatla As Panel
    Friend WithEvents Label20 As Label
    Friend WithEvents cbKonfSygnPolsamPomPas As CheckBox
    Friend WithEvents cbKonfSygnPolsamZielPas As CheckBox
    Friend WithEvents cbKonfSygnPolsamBiale As CheckBox
    Friend WithEvents cbKonfSygnPolsamPomDol As CheckBox
    Friend WithEvents cbKonfSygnPolsamCzer As CheckBox
    Friend WithEvents cbKonfSygnPolsamPomGor As CheckBox
    Friend WithEvents cbKonfSygnPolsamZiel As CheckBox
    Friend WithEvents cboKonfSygnOdcinekNast As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents pnlKonfPrzycisk As Panel
    Friend WithEvents cboKonfPrzyciskTyp As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents cboKonfPrzyciskObiekt As ComboBox
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
    Friend WithEvents tbpLampy As TabPage
    Friend WithEvents splKartaLampy As SplitContainer
    Friend WithEvents lvLampy As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents txtLampaY As TextBox
    Friend WithEvents txtLampaX As TextBox
    Friend WithEvents txtLampaAdres As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents btnLampaUsun As Button
    Friend WithEvents btnLampaDodaj As Button
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtLicznik1Y As TextBox
    Friend WithEvents txtLicznik1X As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents btnLicznikUsun As Button
    Friend WithEvents btnLicznikDodaj As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtLicznik2Y As TextBox
    Friend WithEvents txtLicznik2X As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents txtLicznik2Adres As TextBox
    Friend WithEvents pnlLicznikOdcinek1 As Panel
    Friend WithEvents pnlLicznikOdcinek2 As Panel
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ColumnHeader10 As ColumnHeader
    Friend WithEvents pnlLicznik2 As Panel
    Friend WithEvents pnlLicznik1 As Panel
    Friend WithEvents pnlKonfNapis As Panel
    Friend WithEvents txtKonfNapisTekst As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents pnlKonfKier As Panel
    Friend WithEvents ctxSortowanie As ContextMenuStrip
    Friend WithEvents ctmSortuj As ToolStripMenuItem
    Friend WithEvents mnuNowy As ToolStripMenuItem
    Friend WithEvents mnuOtworz As ToolStripMenuItem
    Friend WithEvents mnuZapisz As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents mnuZapiszJako As ToolStripMenuItem
    Friend WithEvents Label36 As Label
    Friend WithEvents plpPulpit As Pulpit.PulpitSterowniczy
    Friend WithEvents pnlKonfSygnPowtKolejnosc As Panel
    Friend WithEvents rbKonfSygnPowtKolejnoscIII As RadioButton
    Friend WithEvents rbKonfSygnPowtKolejnoscII As RadioButton
    Friend WithEvents rbKonfSygnPowtKolejnoscI As RadioButton
    Friend WithEvents cboKonfSygnInfSygnPowtarzany As ComboBox
    Friend WithEvents Label40 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents pnlKonfSygnOdcNast As Panel
    Friend WithEvents tbpPrzejazdy As TabPage
    Friend WithEvents splKartaPrzejazdy As SplitContainer
    Friend WithEvents lvPrzejazdy As ListView
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents ColumnHeader12 As ColumnHeader
    Friend WithEvents ColumnHeader13 As ColumnHeader
    Friend WithEvents tabPrzejazd As TabControl
    Friend WithEvents tbpPrzejazdOgolne As TabPage
    Friend WithEvents tbpPrzejazdAutomatyzacja As TabPage
    Friend WithEvents tbpPrzejazdRogatki As TabPage
    Friend WithEvents tbpPrzejazdSygnDrog As TabPage
    Friend WithEvents ColumnHeader14 As ColumnHeader
    Friend WithEvents Label45 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents txtPrzejazdCzasPodnoszenie As TextBox
    Friend WithEvents txtPrzejazdCzasOpuszczanie As TextBox
    Friend WithEvents txtPrzejazdCzasSwiatla As TextBox
    Friend WithEvents txtPrzejazdNazwa As TextBox
    Friend WithEvents pnlPrzejazdKolorNieprzypisany As Panel
    Friend WithEvents pnlPrzejazdKolorPrzypisany As Panel
    Friend WithEvents Label48 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents Label46 As Label
    Friend WithEvents Label49 As Label
    Friend WithEvents pnlPrzejazdKolorInny As Panel
    Friend WithEvents btnPrzejazdUsun As Button
    Friend WithEvents btnPrzejazdDodaj As Button
    Friend WithEvents txtPrzejazdRogatkaY As TextBox
    Friend WithEvents txtPrzejazdRogatkaX As TextBox
    Friend WithEvents txtPrzejazdRogatkaAdres As TextBox
    Friend WithEvents btnPrzejazdRogatkaUsun As Button
    Friend WithEvents btnPrzejazdRogatkaDodaj As Button
    Friend WithEvents lvPrzejazdRogatki As ListView
    Friend WithEvents Label52 As Label
    Friend WithEvents Label51 As Label
    Friend WithEvents Label50 As Label
    Friend WithEvents splPrzejazdRogatki As SplitContainer
    Friend WithEvents ColumnHeader15 As ColumnHeader
    Friend WithEvents ColumnHeader16 As ColumnHeader
    Friend WithEvents ColumnHeader17 As ColumnHeader
    Friend WithEvents splPrzejazdSygnDrog As SplitContainer
    Friend WithEvents lvPrzejazdSygnDrog As ListView
    Friend WithEvents ColumnHeader18 As ColumnHeader
    Friend WithEvents ColumnHeader19 As ColumnHeader
    Friend WithEvents ColumnHeader20 As ColumnHeader
    Friend WithEvents txtPrzejazdSygnDrogY As TextBox
    Friend WithEvents txtPrzejazdSygnDrogX As TextBox
    Friend WithEvents txtPrzejazdSygnDrogAdres As TextBox
    Friend WithEvents Label55 As Label
    Friend WithEvents Label54 As Label
    Friend WithEvents Label53 As Label
    Friend WithEvents btnPrzejazdSygnDrogUsun As Button
    Friend WithEvents btnPrzejazdSygnDrogDodaj As Button
    Friend WithEvents splPrzejazdAutomatyzacja As SplitContainer
    Friend WithEvents lvPrzejazdAutomatyzacja As ListView
    Friend WithEvents ColumnHeader21 As ColumnHeader
    Friend WithEvents ColumnHeader22 As ColumnHeader
    Friend WithEvents ColumnHeader23 As ColumnHeader
    Friend WithEvents cboPrzejazdAutomatyzacjaSygnalizator As ComboBox
    Friend WithEvents cboPrzejazdAutomatyzacjaOdcinekPrzyjazd As ComboBox
    Friend WithEvents cboPrzejazdAutomatyzacjaOdcinekWyjazd As ComboBox
    Friend WithEvents Label58 As Label
    Friend WithEvents Label57 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents btnPrzejazdAutomatyzacjaUsun As Button
    Friend WithEvents btnPrzejazdAutomatyzacjaDodaj As Button
    Friend WithEvents pnlPrzejazdAutomatyzacjaKolorPrzyjazd As Panel
    Friend WithEvents pnlPrzejazdAutomatyzacjaKolorWyjazd As Panel
    Friend WithEvents cbPrzejazdTrybReczny As CheckBox
    Friend WithEvents cbPrzejazdTrybAutomatyczny As CheckBox
    Friend WithEvents ColumnHeader24 As ColumnHeader
    Friend WithEvents pnlPrzejazdAutomatyzacjaKolorSygnalizator As Panel
    Friend WithEvents ctxSortowaniePrzejazdy As ContextMenuStrip
    Friend WithEvents ctmSortujPrzejazdy As ToolStripMenuItem
    Friend WithEvents Label60 As Label
    Friend WithEvents Label59 As Label
    Friend WithEvents cboKonfKierStawnosc As ComboBox
    Friend WithEvents rbKonfKierWyjazdPrawo As RadioButton
    Friend WithEvents rbKonfKierWyjazdLewo As RadioButton
    Friend WithEvents txtPrzejazdNumer As TextBox
    Friend WithEvents Label61 As Label
    Friend WithEvents cbKonfTorNiezajetosc As CheckBox
    Friend WithEvents cbKonfTorZelektryfikowany As CheckBox
    Friend WithEvents txtKonfTorDlugosc As TextBox
    Friend WithEvents Label63 As Label
    Friend WithEvents pnlKonfAdres As Panel
    Friend WithEvents txtKonfAdres As TextBox
    Friend WithEvents Label64 As Label
    Friend WithEvents cbKonfSygnPolsamKierPrzeciwny As CheckBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents cbKonfPrzycisk As CheckBox
    Friend WithEvents rbKonfRozjazdZasadniczyMinus As RadioButton
    Friend WithEvents rbKonfRozjazdZasadniczyPlus As RadioButton
    Friend WithEvents Label14 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents txtKonfTorPodwDlugosc1 As TextBox
    Friend WithEvents txtKonfTorPodwDlugosc2 As TextBox
    Friend WithEvents cbKonfTorPodwElektr1 As CheckBox
    Friend WithEvents cbKonfTorPodwElektr2 As CheckBox
    Friend WithEvents cbKonfTorPodwNiezajetosc1 As CheckBox
    Friend WithEvents cbKonfTorPodwNiezajetosc2 As CheckBox
    Friend WithEvents pnlKonfPosiadaPrzycisk As Panel
    Friend WithEvents txtKonfNapisRozmiar As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents ColumnHeader25 As ColumnHeader
    Friend WithEvents ColumnHeader26 As ColumnHeader
    Friend WithEvents txtPrzejazdRogatkaCzasDoZamkniecia As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents pnlKonfNazwa As Panel
    Friend WithEvents txtKonfNazwa As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents pnlKonfTorPodwojny As Panel
    Friend WithEvents lblKonfTorPodwojnyPierwszy As Label
    Friend WithEvents lblKonfTorPodwojnyDrugi As Label
    Friend WithEvents pnlKonfSygnInfSwiatla As Panel
    Friend WithEvents cbKonfSygnInfPom As CheckBox
    Friend WithEvents cbKonfSygnInfZiel As CheckBox
    Friend WithEvents Label39 As Label
    Friend WithEvents pnlKonfSygnPolsamUstawienia As Panel
    Friend WithEvents cbKonfSygnPolsamBrakDrogiHamowania As CheckBox
    Friend WithEvents cbKonfSygnPolsamManewry As CheckBox
    Friend WithEvents Label13 As Label
    Friend WithEvents pnlKonfSygnInfSygnPowtarzany As Panel
    Friend WithEvents mnuPredkosciTorow As ToolStripMenuItem
    Friend WithEvents pnlKonfBudynek As Panel
    Friend WithEvents cboKonfBudynekRodzaj As ComboBox
    Friend WithEvents txtKonfBudynekTekst As TextBox
    Friend WithEvents Label62 As Label
    Friend WithEvents Label23 As Label
End Class
