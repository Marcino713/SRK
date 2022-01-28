﻿Public Class wndKonfiguratorStacji
    Private ReadOnly NAZWA_OKNA As String
    Private Const FILTR_PLIKU As String = Zaleznosci.Pulpit.OPIS_PLIKU & "|*" & Zaleznosci.Pulpit.ROZSZERZENIE_PLIKU
    Private Const SKALOWANIE_ZMIANA As Single = 0.05
    Private Const SKALOWANIE_MIN As Single = 30
    Private Const SKALOWANIE_MAX As Single = 200
    Private Const ZWIEKSZ_OBROT As Integer = 90
    Private Const KAT_PELNY As Integer = 360

    Private Konfiguracja As New KonfiguracjaRysowania
    Private Pulpit As New Zaleznosci.Pulpit
    Private PoprzedniPunkt As New Point(-1, -1)
    Private LokalizacjaPulpitu As New Point(-1, -1)
    Private PaneleKonfKostek As Panel()
    Private ZdarzeniaWlaczone As Boolean = True
    Private ZaznaczonaLampaNaLiscie As ListViewItem
    Private ZaznaczonyOdcinekNaLiscie As ListViewItem
    Private ZaznaczonyLicznikNaLiscie As ListViewItem
    Private AkceptowaniePrzeciagania As DragDropEffects
    Private PoprzLokalizacjaKostki As New Point(-1, -1)
    Private PierwszaObslugaPrzeciagania As Boolean = True

    Private Delegate Function SprawdzTypKostki(kostka As Zaleznosci.Kostka) As Boolean
    Private Delegate Function PobierzNazweKostki(kostka As Zaleznosci.Kostka) As String

#Region "Okno"

    Public Sub New()
        InitializeComponent()
        NAZWA_OKNA = Text
    End Sub

    Private Sub wndKonfiguratorStacji_Load() Handles Me.Load
        PaneleKonfKostek = {pnlKonfPrzycisk, pnlKonfRozjazd, pnlKonfSygn, pnlKonfTor, pnlKonfNapis, pnlKonfKier}
        For i As Integer = 0 To PaneleKonfKostek.Length - 1
            PaneleKonfKostek(i).Width = splKartaPulpit.Panel2.Width
            PaneleKonfKostek(i).Location = New Point(0, 0)
        Next

        UtworzListeKostek()
        RysujPulpit()

        UstawAktywnoscPolLamp(False)
        UstawAktywnoscPolLicznikow(False)
        UstawAktywnoscPolTorow(False)

        pnlTorTenOdcinek.BackColor = KOLOR_TOR_TEN_ODCINEK
        pnlTorInnyOdcinek.BackColor = KOLOR_TOR_PRZYPISANY
        pnlTorNieprzypisany.BackColor = KOLOR_TOR_NIEPRZYPISANY

        pnlLicznik1.BackColor = KOLOR_TOR_TEN_ODCINEK
        pnlLicznik2.BackColor = KOLOR_TOR_LICZNIK_ODCINEK_2

        pnlLicznikTor1.BackColor = KOLOR_TOR_TEN_ODCINEK
        pnlLicznikTor2.BackColor = KOLOR_TOR_LICZNIK_ODCINEK_2
    End Sub

    Private Sub wndKonfiguratorStacji_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not PrzetworzPorzucaniePliku()
    End Sub

    Private Sub wndKonfiguratorStacji_Resize() Handles Me.Resize
        RysujPulpit()
    End Sub

    Private Sub tabUstawienia_Selected() Handles tabUstawienia.Selected
        If tabUstawienia.SelectedTab Is tbpPulpit Then
            Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Nic
            PokazKonfSygnTory()
        End If
        If tabUstawienia.SelectedTab Is tbpTory Then
            Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Tory
            OdswiezListeTorow()
        End If
        If tabUstawienia.SelectedTab Is tbpLiczniki Then
            Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Liczniki
            OdswiezListeLicznikow()
            OdswiezListeTorowWLicznikach()
        End If
        If tabUstawienia.SelectedTab Is tbpLampy Then
            Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Lampy
        End If
        RysujPulpit()
    End Sub

    Private Sub DodajKostkeDoListy(kostka As Zaleznosci.Kostka, nazwa As String)
        Static Dim konf As New KonfiguracjaRysowania() With {.Skalowanie = 47, .RysujKrawedzieKostek = False}
        Dim pulpit As New Zaleznosci.Pulpit(1, 1)
        pulpit.Kostki(0, 0) = kostka
        imlKostki.Images.Add(Rysuj(pulpit, konf))
        Dim lvi As New ListViewItem(nazwa, imlKostki.Images.Count - 1)
        lvi.Tag = kostka.GetType()
        lvPulpitKostki.Items.Add(lvi)
    End Sub

    Private Sub UtworzListeKostek()
        DodajKostkeDoListy(New Zaleznosci.Tor(), "Tor")
        DodajKostkeDoListy(New Zaleznosci.TorKoniec(), "Koniec toru")
        DodajKostkeDoListy(New Zaleznosci.Zakret(), "Zakręt")
        DodajKostkeDoListy(New Zaleznosci.RozjazdLewo(), "Rozjazd lewy")
        DodajKostkeDoListy(New Zaleznosci.RozjazdPrawo(), "Rozjazd prawy")
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorManewrowy(), "Sygnalizator manewrowy")
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorPolsamoczynny(), "Sygnalizator półsamoczynny")
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorSamoczynny(), "Sygnalizator samoczynny")
        DodajKostkeDoListy(New Zaleznosci.Przycisk(), "Przycisk")
        DodajKostkeDoListy(New Zaleznosci.PrzyciskTor(), "Przycisk z torem")
        DodajKostkeDoListy(New Zaleznosci.Kierunek(), "Wjazd/wyjazd ze stacji")
        DodajKostkeDoListy(New Zaleznosci.Napis(), "Napis")
    End Sub

    Private Sub UstawTytulOkna()
        If Pulpit.Nazwa = "" Then
            Text = NAZWA_OKNA
        Else
            Text = NAZWA_OKNA & " - " & Pulpit.Nazwa
        End If
    End Sub

    Private Sub CzyscDane(Optional nowyPulpit As Zaleznosci.Pulpit = Nothing)
        Konfiguracja = New KonfiguracjaRysowania
        Pulpit = If(nowyPulpit Is Nothing, New Zaleznosci.Pulpit, nowyPulpit)

        UkryjPaneleKonf()
        OdswiezListeTorow()
        OdswiezListeLicznikow()
        OdswiezListeTorowWLicznikach()
        OdswiezListeLamp()

        pctPulpit.Location = New Point(0, 0)
    End Sub

    Private Function Zapisz(nowyPlik As Boolean) As Boolean
        Dim nowaSciezka As String = Nothing
        Dim wynik As Boolean

        If Pulpit.SciezkaPliku = "" Or nowyPlik Then
            Dim dlg As New SaveFileDialog
            dlg.Filter = FILTR_PLIKU
            If dlg.ShowDialog = DialogResult.OK Then
                nowaSciezka = dlg.FileName
            Else
                Return False
            End If
        End If

        If nowaSciezka IsNot Nothing Then
            wynik = Pulpit.Zapisz(nowaSciezka)
        Else
            wynik = Pulpit.Zapisz()
        End If

        If Not wynik Then
            PokazBlad("Nie udało się zapisać pliku.")
            Return False
        Else
            PokazKomunikat("Plik został zapisany.")
            Return True
        End If
    End Function

    ''' <summary>
    ''' Pyta użytkownika o zapisanie pliku, ewentualnie zapisuje i zwraca wartość określającą, czy można przejść do następnego kroku (np. wczytania pliku)
    ''' </summary>
    Private Function PrzetworzPorzucaniePliku() As Boolean
        Dim wynik As DialogResult = ZadajPytanieTrzyodpowiedziowe("Zapisać plik?")

        If wynik = DialogResult.Yes Then Return Zapisz(False)

        If wynik = DialogResult.Cancel Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region 'Okno

#Region "Menu"

    Private Sub mnuNowy_Click() Handles mnuNowy.Click
        If PrzetworzPorzucaniePliku() Then
            CzyscDane()
            RysujPulpit()
            UstawTytulOkna()
        End If
    End Sub

    Private Sub mnuOtworz_Click() Handles mnuOtworz.Click
        If PrzetworzPorzucaniePliku() Then
            Dim dlg As New OpenFileDialog
            dlg.Filter = FILTR_PLIKU
            If dlg.ShowDialog = DialogResult.OK Then
                Dim pulpitNowy As Zaleznosci.Pulpit = Zaleznosci.Pulpit.Otworz(dlg.FileName)
                If pulpitNowy IsNot Nothing Then
                    CzyscDane(pulpitNowy)
                    RysujPulpit()
                    UstawTytulOkna()
                Else
                    PokazBlad("Nie udało się otworzyć pliku.")
                End If
            End If
        End If
    End Sub

    Private Sub mnuZapisz_Click() Handles mnuZapisz.Click
        Zapisz(False)
    End Sub

    Private Sub mnuZapiszJako_Click() Handles mnuZapiszJako.Click
        Zapisz(True)
    End Sub

    Private Sub mnuDodajKostki_Click() Handles mnuDodajKostki.Click
        Dim wnd As New wndEdytorPowierzchni(wndEdytorPowierzchni.TypEdycji.Dodaj, Pulpit.Szerokosc, Pulpit.Wysokosc)
        If wnd.ShowDialog = DialogResult.OK Then
            Pulpit.PowiekszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek)
            UsunZaznaczenieKostki()
        End If
    End Sub

    Private Sub mnuUsunKostki_Click() Handles mnuUsunKostki.Click
        Dim wnd As New wndEdytorPowierzchni(wndEdytorPowierzchni.TypEdycji.Usun, Pulpit.Szerokosc, Pulpit.Wysokosc)
        If wnd.ShowDialog = DialogResult.OK Then
            Try
                If Pulpit.PomniejszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek) Then
                    UsunZaznaczenieKostki()
                Else
                    PokazBlad("Nie udało się usunąć kostek - w wybranym zakresie usuwania pulpit nie jest pusty.")
                End If
            Catch ex As Exception
                PokazBlad("Wystąpił błąd podczas usuwania kostek:" & vbCrLf & ex.Message)
            End Try
        End If
    End Sub

    Private Sub mnuNazwa_Click() Handles mnuNazwa.Click
        Dim wnd As New wndNazwaStacji(Pulpit.Adres, Pulpit.Nazwa, Pulpit.DataUtworzenia)
        If wnd.ShowDialog = DialogResult.OK Then
            Pulpit.Adres = wnd.Adres
            Pulpit.Nazwa = wnd.Nazwa
            UstawTytulOkna()
        End If
    End Sub

    Private Sub ctxSortuj_Click() Handles ctxSortuj.Click
        If tabUstawienia.SelectedTab Is tbpTory Then
            Pulpit.SortujOdcinkiNazwaRosnaco()
            OdswiezListeTorow()

        ElseIf tabUstawienia.SelectedTab Is tbpLiczniki
            Pulpit.SortujLicznikiAdres1Rosnaco()
            OdswiezListeLicznikow()

        ElseIf tabUstawienia.SelectedTab Is tbpLampy
            Pulpit.SortujLampyAdresRosnaco()
            OdswiezListeLamp()

        End If
    End Sub

#End Region 'Menu

#Region "Zakładka Pulpit"

    Private Sub lvKostki_MouseDown(sender As Object, e As MouseEventArgs) Handles lvPulpitKostki.MouseDown
        Dim lvi As ListViewItem = lvPulpitKostki.GetItemAt(e.X, e.Y)
        If lvi IsNot Nothing Then
            lvi.Selected = True
            AkceptowaniePrzeciagania = DragDropEffects.None
            PierwszaObslugaPrzeciagania = True
            DoDragDrop(Activator.CreateInstance(DirectCast(lvi.Tag, Type)), DragDropEffects.Copy)
        End If
    End Sub


    'Tor
    Private Sub txtKonfTorPredkosc_TextChanged() Handles txtKonfTorPredkosc.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Tor).PredkoscZasadnicza = PobierzLiczbeNieujemna(txtKonfTorPredkosc)
    End Sub


    'Rozjazd
    Private Sub txtKonfRozjazdAdres_TextChanged() Handles txtKonfRozjazdAdres.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).Adres = PobierzKrotkaLiczbeNieujemna(txtKonfRozjazdAdres)
    End Sub

    Private Sub txtKonfRozjazdNazwa_TextChanged() Handles txtKonfRozjazdNazwa.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).Nazwa = txtKonfRozjazdNazwa.Text
        RysujPulpit()
    End Sub

    Private Sub txtKonfRozjazdPredkZasad_TextChanged() Handles txtKonfRozjazdPredkZasad.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).PredkoscZasadnicza = PobierzLiczbeNieujemna(txtKonfRozjazdPredkZasad)
    End Sub

    Private Sub txtKonfRozjazdPredkBoczna_TextChanged() Handles txtKonfRozjazdPredkBoczna.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).PredkoscBoczna = PobierzLiczbeNieujemna(txtKonfRozjazdPredkBoczna)
    End Sub

    Private Sub cboKonfRozjazdWprost1_SelectedIndexChanged() Handles cboKonfRozjazdWprost1.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdWprost1, rbKonfRozjazdWprost1Plus, rbKonfRozjazdWprost1Minus, roz) Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub cboKonfRozjazdWprost2_SelectedIndexChanged() Handles cboKonfRozjazdWprost2.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdWprost2, rbKonfRozjazdWprost2Plus, rbKonfRozjazdWprost2Minus, roz) Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub cboKonfRozjazdBok1_SelectedIndexChanged() Handles cboKonfRozjazdBok1.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdBok1, rbKonfRozjazdBok1Plus, rbKonfRozjazdBok1Minus, roz) Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub cboKonfRozjazdBok2_SelectedIndexChanged() Handles cboKonfRozjazdBok2.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdBok2, rbKonfRozjazdBok2Plus, rbKonfRozjazdBok2Minus, roz) Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub rbKonfRozjazdWprost1Plus_CheckedChanged() Handles rbKonfRozjazdWprost1Plus.CheckedChanged
        If rbKonfRozjazdWprost1Plus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdWprost1Minus_CheckedChanged() Handles rbKonfRozjazdWprost1Minus.CheckedChanged
        If rbKonfRozjazdWprost1Minus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdWprost2Plus_CheckedChanged() Handles rbKonfRozjazdWprost2Plus.CheckedChanged
        If rbKonfRozjazdWprost2Plus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdWprost2Minus_CheckedChanged() Handles rbKonfRozjazdWprost2Minus.CheckedChanged
        If rbKonfRozjazdWprost2Minus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdBok1Plus_CheckedChanged() Handles rbKonfRozjazdBok1Plus.CheckedChanged
        If rbKonfRozjazdBok1Plus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdBok1Minus_CheckedChanged() Handles rbKonfRozjazdBok1Minus.CheckedChanged
        If rbKonfRozjazdBok1Minus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdBok2Plus_CheckedChanged() Handles rbKonfRozjazdBok2Plus.CheckedChanged
        If rbKonfRozjazdBok2Plus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdBok2Minus_CheckedChanged() Handles rbKonfRozjazdBok2Minus.CheckedChanged
        If rbKonfRozjazdBok2Minus.Checked Then DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub


    'Sygnalizacja
    Private Sub txtKonfSygnAdres_TextChanged() Handles txtKonfSygnAdres.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Sygnalizator).Adres = PobierzKrotkaLiczbeNieujemna(txtKonfSygnAdres)
    End Sub

    Private Sub txtKonfSygnNazwa_TextChanged() Handles txtKonfSygnNazwa.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Sygnalizator).Nazwa = txtKonfSygnNazwa.Text
        RysujPulpit()
    End Sub

    Private Sub cboKonfSygnOdcinekNast_SelectedIndexChanged() Handles cboKonfSygnOdcinekNast.SelectedIndexChanged
        If cboKonfSygnOdcinekNast.SelectedItem Is Nothing Then Exit Sub
        Dim el As ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboKonfSygnOdcinekNast.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru))
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Sygnalizator).OdcinekNastepujacy = el.Wartosc
    End Sub

    Private Sub cboKonfSygnSygnNast_SelectedIndexChanged() Handles cboKonfSygnSygnNast.SelectedIndexChanged
        If cboKonfSygnSygnNast.SelectedItem Is Nothing Then Exit Sub
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cboKonfSygnSygnNast.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka))
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.SygnalizatorUzalezniony).SygnalizatorNastepny = DirectCast(el.Wartosc, Zaleznosci.Sygnalizator)
    End Sub

    Private Sub txtKonfSygnPredkosc_TextChanged() Handles txtKonfSygnPredkosc.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Sygnalizator).PredkoscZasadnicza = PobierzLiczbeNieujemna(txtKonfSygnPredkosc)
    End Sub

    Private Sub cbKonfSygnZiel_CheckedChanged() Handles cbKonfSygnZiel.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnZiel, Zaleznosci.DostepneSwiatlaEnum.Zielone)
    End Sub

    Private Sub cbKonfSygnPomGor_CheckedChanged() Handles cbKonfSygnPomGor.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnPomGor, Zaleznosci.DostepneSwiatlaEnum.PomaranczoweGora)
    End Sub

    Private Sub cbKonfSygnCzer_CheckedChanged() Handles cbKonfSygnCzer.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnCzer, Zaleznosci.DostepneSwiatlaEnum.Czerwone)
    End Sub

    Private Sub cbKonfSygnPomDol_CheckedChanged() Handles cbKonfSygnPomDol.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnPomDol, Zaleznosci.DostepneSwiatlaEnum.PomaranczoweDol)
    End Sub

    Private Sub cbKonfSygnBiale_CheckedChanged() Handles cbKonfSygnBiale.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnBiale, Zaleznosci.DostepneSwiatlaEnum.Biale)
    End Sub

    Private Sub cbKonfSygnZielPas_CheckedChanged() Handles cbKonfSygnZielPas.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnZielPas, Zaleznosci.DostepneSwiatlaEnum.ZielonyPas)
    End Sub

    Private Sub cbKonfSygnPomPas_CheckedChanged() Handles cbKonfSygnPomPas.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnPomPas, Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas)
    End Sub


    'Przycisk
    Private Sub cboKonfPrzyciskTyp_SelectedIndexChanged() Handles cboKonfPrzyciskTyp.SelectedIndexChanged
        Dim aktywny As Boolean = False
        Dim el As Object = cboKonfPrzyciskTyp.SelectedItem

        If el Is Nothing Then
            aktywny = False
        Else
            aktywny = True

            Select Case Konfiguracja.ZaznaczonaKostka.Typ
                Case Zaleznosci.TypKostki.Przycisk
                    Dim typ As Zaleznosci.TypPrzyciskuEnum = DirectCast(el, ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)).Wartosc
                    Dim prz As Zaleznosci.Przycisk = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Przycisk)
                    prz.TypPrzycisku = typ
                    If typ = Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow Then aktywny = False
                    cboKonfPrzyciskSygnalizator.Items.Clear()
                    cboKonfPrzyciskSygnalizator.Items.AddRange(PobierzElementyDoComboBox(AddressOf CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweSygnalizatora))
                    ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskSygnalizator, prz.ObslugiwanySygnalizator)

                Case Zaleznosci.TypKostki.PrzyciskTor
                    Dim typ As Zaleznosci.TypPrzyciskuTorEnum = DirectCast(el, ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)).Wartosc
                    Dim prz As Zaleznosci.PrzyciskTor = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                    If (prz.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy And (typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny Or typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy)) Or
                            ((prz.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny Or prz.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy) And typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy) Then
                        prz.ObslugiwanySygnalizator = Nothing
                    End If
                    prz.TypPrzycisku = typ
                    Dim f As SprawdzTypKostki
                    If typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy Then
                        f = AddressOf CzySygnalizatorManewrowy
                    Else
                        f = AddressOf CzySygnalizatorPolsamoczynny
                    End If
                    cboKonfPrzyciskSygnalizator.Items.Clear()
                    cboKonfPrzyciskSygnalizator.Items.AddRange(PobierzElementyDoComboBox(f, AddressOf PobierzNazweSygnalizatora))
                    ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskSygnalizator, prz.ObslugiwanySygnalizator)

            End Select

        End If

        cboKonfPrzyciskSygnalizator.Enabled = aktywny
        RysujPulpit()
    End Sub

    Private Sub cboKonfPrzyciskSygnalizator_SelectedIndexChanged() Handles cboKonfPrzyciskSygnalizator.SelectedIndexChanged
        If cboKonfPrzyciskSygnalizator.SelectedItem Is Nothing Then Exit Sub

        Dim sygn As Zaleznosci.Kostka = DirectCast(cboKonfPrzyciskSygnalizator.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc
        Select Case Konfiguracja.ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Przycisk)
                prz.ObslugiwanySygnalizator = DirectCast(sygn, Zaleznosci.SygnalizatorPolsamoczynny)

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                prz.ObslugiwanySygnalizator = DirectCast(sygn, Zaleznosci.Sygnalizator)

        End Select

        RysujPulpit()
    End Sub

    Private Sub txtKonfPrzyciskPredkosc_TextChanged() Handles txtKonfPrzyciskPredkosc.TextChanged
        If Konfiguracja.ZaznaczonaKostka.Typ = Zaleznosci.TypKostki.PrzyciskTor Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.PrzyciskTor).PredkoscZasadnicza = PobierzLiczbeNieujemna(txtKonfPrzyciskPredkosc)
        End If
    End Sub


    'Napis
    Private Sub txtKonfNapisTekst_TextChanged() Handles txtKonfNapisTekst.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Napis).Tekst = txtKonfNapisTekst.Text
        RysujPulpit()
    End Sub


    'Kierunek
    Private Sub txtKonfKierPredkosc_TextChanged() Handles txtKonfKierPredkosc.TextChanged
        DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Kierunek).PredkoscZasadnicza = PobierzLiczbeNieujemna(txtKonfKierPredkosc)
    End Sub

    Private Sub rbKonfKierZasadniczy_CheckedChanged() Handles rbKonfKierZasadniczy.CheckedChanged
        If rbKonfKierZasadniczy.Checked Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Kierunek).KierunekWlaczany = Zaleznosci.KierunekWlaczanyEnum.Zasadniczy
        End If
    End Sub

    Private Sub rbKonfKierPrzeciwny_CheckedChanged() Handles rbKonfKierPrzeciwny.CheckedChanged
        If rbKonfKierPrzeciwny.Checked Then
            DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Kierunek).KierunekWlaczany = Zaleznosci.KierunekWlaczanyEnum.Przeciwny
        End If
    End Sub


    'Wyświetlanie paneli
    Private Sub UkryjPaneleKonf()
        For i As Integer = 0 To PaneleKonfKostek.Length - 1
            PaneleKonfKostek(i).Visible = False
        Next
    End Sub

    Private Sub PokazPanelKonf()
        Select Case Konfiguracja.ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Tor, Zaleznosci.TypKostki.Zakret
                PokazKonfTor()
            Case Zaleznosci.TypKostki.RozjazdLewo, Zaleznosci.TypKostki.RozjazdPrawo
                PokazKonfRozjazd()
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy, Zaleznosci.TypKostki.SygnalizatorSamoczynny, Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                PokazKonfSygn()
            Case Zaleznosci.TypKostki.Przycisk, Zaleznosci.TypKostki.PrzyciskTor
                PokazKonfPrzycisk()
            Case Zaleznosci.TypKostki.Napis
                PokazKonfNapis()
            Case Zaleznosci.TypKostki.Kierunek
                PokazKonfKier()
        End Select
    End Sub

    Private Sub PokazKonfTor()
        Dim tor As Zaleznosci.Tor = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Tor)

        txtKonfTorPredkosc.Text = tor.PredkoscZasadnicza.ToString()
        pnlKonfTor.Visible = True
    End Sub

    Private Sub PokazKonfRozjazd()
        Dim roz As Zaleznosci.Rozjazd = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Rozjazd)
        txtKonfRozjazdAdres.Text = roz.Adres.ToString
        txtKonfRozjazdNazwa.Text = roz.Nazwa
        txtKonfRozjazdPredkZasad.Text = roz.PredkoscZasadnicza.ToString
        txtKonfRozjazdPredkBoczna.Text = roz.PredkoscBoczna.ToString

        Dim pusty As New ObiektComboBox(Of Zaleznosci.Kostka)(Nothing, "")
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzyRozjazd, AddressOf PobierzNazweRozjazdu)

        cboKonfRozjazdWprost1.Items.Clear()
        cboKonfRozjazdWprost2.Items.Clear()
        cboKonfRozjazdBok1.Items.Clear()
        cboKonfRozjazdBok2.Items.Clear()

        cboKonfRozjazdWprost1.Items.Add(pusty)
        cboKonfRozjazdWprost2.Items.Add(pusty)
        cboKonfRozjazdBok1.Items.Add(pusty)
        cboKonfRozjazdBok2.Items.Add(pusty)

        cboKonfRozjazdWprost1.Items.AddRange(el)
        cboKonfRozjazdWprost2.Items.AddRange(el)
        cboKonfRozjazdBok1.Items.AddRange(el)
        cboKonfRozjazdBok2.Items.AddRange(el)

        ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfRozjazdWprost1, roz.ZaleznosciJesliWprost(0).RozjazdZalezny)
        ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfRozjazdWprost2, roz.ZaleznosciJesliWprost(1).RozjazdZalezny)
        ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfRozjazdBok1, roz.ZaleznosciJesliBok(0).RozjazdZalezny)
        ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfRozjazdBok2, roz.ZaleznosciJesliBok(1).RozjazdZalezny)

        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliWprost(0).RozjazdZalezny, rbKonfRozjazdWprost1Plus, rbKonfRozjazdWprost1Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliWprost(1).RozjazdZalezny, rbKonfRozjazdWprost2Plus, rbKonfRozjazdWprost2Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliBok(0).RozjazdZalezny, rbKonfRozjazdBok1Plus, rbKonfRozjazdBok1Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliBok(1).RozjazdZalezny, rbKonfRozjazdBok2Plus, rbKonfRozjazdBok2Minus)

        If roz.ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdWprost1Plus.Checked = True Else rbKonfRozjazdWprost1Minus.Checked = True
        If roz.ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdWprost2Plus.Checked = True Else rbKonfRozjazdWprost2Minus.Checked = True
        If roz.ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdBok1Plus.Checked = True Else rbKonfRozjazdBok1Minus.Checked = True
        If roz.ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdBok2Plus.Checked = True Else rbKonfRozjazdBok2Minus.Checked = True

        pnlKonfRozjazd.Visible = True
    End Sub

    Private Sub PokazKonfSygnTory()
        Dim sygn As Zaleznosci.Sygnalizator = TryCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Sygnalizator)
        If sygn Is Nothing Then Exit Sub

        cboKonfSygnOdcinekNast.Items.Clear()
        Dim en As IEnumerator(Of Zaleznosci.OdcinekToru) = Pulpit.OdcinkiTorow.OrderBy(Function(t As Zaleznosci.OdcinekToru) t.Nazwa).GetEnumerator()
        Do While en.MoveNext()
            cboKonfSygnOdcinekNast.Items.Add(New ObiektComboBox(Of Zaleznosci.OdcinekToru)(en.Current, en.Current.Nazwa))
        Loop
        ZaznaczElement(cboKonfSygnOdcinekNast, sygn.OdcinekNastepujacy)
    End Sub

    Private Sub PokazKonfSygn()
        Dim sygn As Zaleznosci.Sygnalizator = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Sygnalizator)
        txtKonfSygnAdres.Text = sygn.Adres.ToString
        txtKonfSygnNazwa.Text = sygn.Nazwa.ToString
        cboKonfSygnSygnNast.Enabled = (sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorManewrowy)
        pnlKonfSygnSwiatla.Visible = (sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny)

        PokazKonfSygnTory()

        cboKonfSygnSygnNast.Items.Clear()
        Dim sygn_nast As Zaleznosci.Sygnalizator = Nothing
        If sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorManewrowy Then
            Dim pusty_sygn As New ObiektComboBox(Of Zaleznosci.Kostka)(Nothing, "")
            Dim sygnalizatory As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzySygnalizatorUzalezniony, AddressOf PobierzNazweSygnalizatora)
            cboKonfSygnSygnNast.Items.Add(pusty_sygn)
            cboKonfSygnSygnNast.Items.AddRange(sygnalizatory)
            sygn_nast = DirectCast(sygn, Zaleznosci.SygnalizatorUzalezniony).SygnalizatorNastepny
            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfSygnSygnNast, sygn_nast)
        End If

        txtKonfSygnPredkosc.Text = sygn.PredkoscZasadnicza.ToString()

        If sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Then
            Dim sw As Zaleznosci.DostepneSwiatlaEnum = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny).DostepneSwiatla
            cbKonfSygnZiel.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Zielone) <> 0
            cbKonfSygnPomGor.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweGora) <> 0
            cbKonfSygnCzer.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Czerwone) <> 0
            cbKonfSygnPomDol.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweDol) <> 0
            cbKonfSygnBiale.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Biale) <> 0
            cbKonfSygnZielPas.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.ZielonyPas) <> 0
            cbKonfSygnPomPas.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas) <> 0
        End If

        pnlKonfSygn.Visible = True
    End Sub

    Private Sub PokazKonfPrzycisk()
        cboKonfPrzyciskTyp.Items.Clear()

        Select Case Konfiguracja.ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Przycisk)
                cboKonfPrzyciskTyp.Items.AddRange({
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)(Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy, "Sygnał zastępczy"),
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)(Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow, "Zwolnienie przebiegów")
                })
                cboKonfPrzyciskTyp.SelectedIndex = prz.TypPrzycisku
                pnlKonfPrzyciskPredkosc.Visible = False

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                cboKonfPrzyciskTyp.Items.AddRange({
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny, "Sygnalizator półsamoczynny"),
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy, "Sygnalizator manewrowy"),
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy, "Sygnał manewrowy na sygnalizatorze półsamoczynnym")
                })
                cboKonfPrzyciskTyp.SelectedIndex = prz.TypPrzycisku
                pnlKonfPrzyciskPredkosc.Visible = True
                txtKonfPrzyciskPredkosc.Text = prz.PredkoscZasadnicza.ToString()

        End Select

        pnlKonfPrzycisk.Visible = True
    End Sub

    Private Sub PokazKonfNapis()
        Dim napis As Zaleznosci.Napis = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Napis)

        txtKonfNapisTekst.Text = napis.Tekst
        pnlKonfNapis.Visible = True
    End Sub

    Private Sub PokazKonfKier()
        Dim kierunek As Zaleznosci.Kierunek = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.Kierunek)

        txtKonfKierPredkosc.Text = kierunek.PredkoscZasadnicza.ToString
        If kierunek.KierunekWlaczany = Zaleznosci.KierunekWlaczanyEnum.Zasadniczy Then
            rbKonfKierZasadniczy.Checked = True
        Else
            rbKonfKierPrzeciwny.Checked = True
        End If

        pnlKonfKier.Visible = True
    End Sub

    'Inne
    Private Function PrzetworzZaznaczenieRozjazduZaleznego(cbo As ComboBox, rbPlus As RadioButton, rbMinus As RadioButton, ByRef rozjazd As Zaleznosci.Rozjazd) As Boolean
        If cbo.SelectedItem Is Nothing Then Return False
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cbo.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka))
        AktywujPrzyciskiKonfiguracjiRozjazdu(el.Wartosc, rbPlus, rbMinus)
        rozjazd = DirectCast(el.Wartosc, Zaleznosci.Rozjazd)
        Return True
    End Function

    Private Sub AktywujPrzyciskiKonfiguracjiRozjazdu(rozjazdZalezny As Zaleznosci.Kostka, rbPlus As RadioButton, rbMinus As RadioButton)
        If rozjazdZalezny Is Nothing Then
            rbPlus.Enabled = False
            rbMinus.Enabled = False
        Else
            rbPlus.Enabled = True
            rbMinus.Enabled = True
        End If
    End Sub

    Private Function CzyRozjazd(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.RozjazdLewo Or kostka.Typ = Zaleznosci.TypKostki.RozjazdPrawo
    End Function

    Private Function PobierzNazweRozjazdu(kostka As Zaleznosci.Kostka) As String
        Return DirectCast(kostka, Zaleznosci.Rozjazd).Nazwa
    End Function

    Private Sub UstawDostepneSwiatlo(cb As CheckBox, kolor As Zaleznosci.DostepneSwiatlaEnum)
        Dim sygn As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(Konfiguracja.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny)
        If cb.Checked Then
            sygn.DostepneSwiatla = sygn.DostepneSwiatla Or kolor
        Else
            sygn.DostepneSwiatla = sygn.DostepneSwiatla And (Not kolor)
        End If
    End Sub

    Private Function CzySygnalizatorUzalezniony(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Or kostka.Typ = Zaleznosci.TypKostki.SygnalizatorSamoczynny
    End Function

    Private Function CzySygnalizatorPolsamoczynny(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
    End Function

    Private Function CzySygnalizatorManewrowy(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.SygnalizatorManewrowy
    End Function

    Private Function PobierzNazweSygnalizatora(kostka As Zaleznosci.Kostka) As String
        Return DirectCast(kostka, Zaleznosci.Sygnalizator).Nazwa
    End Function

    Private Function PobierzElementyDoComboBox(sprawdzanie As SprawdzTypKostki, nazwa As PobierzNazweKostki) As ObiektComboBox(Of Zaleznosci.Kostka)()
        Dim kostki As New List(Of ObiektComboBox(Of Zaleznosci.Kostka))

        For x As Integer = 0 To Pulpit.Szerokosc - 1
            For y As Integer = 0 To Pulpit.Wysokosc - 1
                Dim k As Zaleznosci.Kostka = Pulpit.Kostki(x, y)
                If k IsNot Nothing AndAlso sprawdzanie(k) AndAlso k IsNot Konfiguracja.ZaznaczonaKostka Then
                    kostki.Add(New ObiektComboBox(Of Zaleznosci.Kostka)(k, nazwa(k)))
                End If
            Next
        Next

        Return kostki.OrderBy(Function(k As ObiektComboBox(Of Zaleznosci.Kostka)) nazwa(k.Wartosc)).ToArray()
    End Function

#End Region 'Zakładka Pulpit

#Region "Zakładka Odcinki torów"

    Private Sub lvTory_SelectedIndexChanged() Handles lvTory.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyOdcinekNaLiscie = PobierzZaznaczonyElementNaLiscie(lvTory)
        Dim odcinek As Zaleznosci.OdcinekToru = PobierzZaznaczonyElement(Of Zaleznosci.OdcinekToru)(lvTory)
        If odcinek Is Nothing Then
            txtTorAdres.Text = ""
            txtTorNazwa.Text = ""
            txtTorOpis.Text = ""
            UstawAktywnoscPolTorow(False)
        Else
            txtTorAdres.Text = odcinek.Adres.ToString
            txtTorNazwa.Text = odcinek.Nazwa.ToString
            txtTorOpis.Text = odcinek.Opis.ToString
            UstawAktywnoscPolTorow(True)
        End If
        Konfiguracja.ZaznaczonyOdcinek = odcinek
        RysujPulpit()
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnTorDodaj_Click() Handles btnTorDodaj.Click
        Pulpit.OdcinkiTorow.Add(New Zaleznosci.OdcinekToru)
        OdswiezListeTorow()
    End Sub

    Private Sub btnTorUsun_Click() Handles btnTorUsun.Click
        Dim odcinek As Zaleznosci.OdcinekToru = Konfiguracja.ZaznaczonyOdcinek
        If odcinek Is Nothing Then Exit Sub

        If ZadajPytanie("Czy usunąć odcinek torów o nazwie " & odcinek.Nazwa & "?") = DialogResult.Yes Then
            Pulpit.UsunOdcinekToru(odcinek)
            OdswiezListeTorow()
        End If
    End Sub

    Private Sub txtTorAdres_TextChanged() Handles txtTorAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim tor As Zaleznosci.OdcinekToru = Konfiguracja.ZaznaczonyOdcinek
        If tor IsNot Nothing Then
            tor.Adres = PobierzKrotkaLiczbeNieujemna(txtTorAdres)
            ZaznaczonyOdcinekNaLiscie.SubItems(0).Text = tor.Adres.ToString
        End If
    End Sub

    Private Sub txtTorNazwa_TextChanged() Handles txtTorNazwa.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim tor As Zaleznosci.OdcinekToru = Konfiguracja.ZaznaczonyOdcinek
        If tor IsNot Nothing Then
            tor.Nazwa = txtTorNazwa.Text
            ZaznaczonyOdcinekNaLiscie.SubItems(1).Text = tor.Nazwa
        End If
    End Sub

    Private Sub txtTorOpis_TextChanged() Handles txtTorOpis.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim tor As Zaleznosci.OdcinekToru = Konfiguracja.ZaznaczonyOdcinek
        If tor IsNot Nothing Then
            tor.Opis = txtTorOpis.Text
        End If
    End Sub

    Private Sub OdswiezListeTorow()
        Dim odcinek As Zaleznosci.OdcinekToru = Konfiguracja.ZaznaczonyOdcinek
        lvTory.Items.Clear()
        ZaznaczonyOdcinekNaLiscie = Nothing

        Dim en As List(Of Zaleznosci.OdcinekToru).Enumerator = Pulpit.OdcinkiTorow.GetEnumerator
        While en.MoveNext
            Dim o As Zaleznosci.OdcinekToru = en.Current
            Dim lvi As New ListViewItem(New String() {o.Adres.ToString, o.Nazwa.ToString, o.KostkiTory.Count.ToString()})
            lvi.Tag = o
            If o Is odcinek Then
                lvi.Selected = True
                ZaznaczonyOdcinekNaLiscie = lvi
            End If
            lvTory.Items.Add(lvi)
        End While

        If ZaznaczonyOdcinekNaLiscie Is Nothing Then
            lvTory_SelectedIndexChanged()
        End If

        RysujPulpit()
    End Sub

    Private Sub UstawAktywnoscPolTorow(wlaczony As Boolean)
        btnTorUsun.Enabled = wlaczony
        txtTorAdres.Enabled = wlaczony
        txtTorNazwa.Enabled = wlaczony
        txtTorOpis.Enabled = wlaczony
    End Sub

    Private Sub OdswiezLiczbePrzypisanychKostekTorow()
        If lvTory.Items Is Nothing Then Exit Sub

        For i As Integer = 0 To lvTory.Items.Count - 1
            Dim o As Zaleznosci.OdcinekToru = DirectCast(lvTory.Items(i).Tag, Zaleznosci.OdcinekToru)
            lvTory.Items(i).SubItems(2).Text = o.KostkiTory.Count.ToString()
        Next
    End Sub

#End Region 'Zakładka Odcinki torów

#Region "Zakładka Liczniki osi"

    Private Sub lvLiczniki_SelectedIndexChanged() Handles lvLiczniki.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyLicznikNaLiscie = PobierzZaznaczonyElementNaLiscie(lvLiczniki)
        Dim licznik As Zaleznosci.ParaLicznikowOsi = PobierzZaznaczonyElement(Of Zaleznosci.ParaLicznikowOsi)(lvLiczniki)
        If licznik Is Nothing Then
            txtLicznik1Adres.Text = ""
            txtLicznik1X.Text = ""
            txtLicznik1Y.Text = ""
            txtLicznik2Adres.Text = ""
            txtLicznik2X.Text = ""
            txtLicznik2Y.Text = ""
            cboLicznikTor1.SelectedItem = Nothing
            cboLicznikTor2.SelectedItem = Nothing
            UstawAktywnoscPolLicznikow(False)
        Else
            txtLicznik1Adres.Text = licznik.Adres1.ToString
            txtLicznik1X.Text = licznik.X1.ToString
            txtLicznik1Y.Text = licznik.Y1.ToString
            txtLicznik2Adres.Text = licznik.Adres2.ToString
            txtLicznik2X.Text = licznik.X2.ToString
            txtLicznik2Y.Text = licznik.Y2.ToString
            ZaznaczElement(cboLicznikTor1, licznik.Odcinek1)
            ZaznaczElement(cboLicznikTor2, licznik.Odcinek2)
            UstawAktywnoscPolLicznikow(True)
        End If
        Konfiguracja.ZaznaczonyLicznik = licznik
        ZdarzeniaWlaczone = True
        RysujPulpit()
    End Sub

    Private Sub btnLicznikDodaj_Click() Handles btnLicznikDodaj.Click
        Pulpit.LicznikiOsi.Add(New Zaleznosci.ParaLicznikowOsi())
        OdswiezListeLicznikow()
    End Sub

    Private Sub btnLicznikUsun_Click() Handles btnLicznikUsun.Click
        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik Is Nothing Then Exit Sub

        If ZadajPytanie("Czy usunąć parę liczników osi dla torów """ & licznik.Odcinek1?.Nazwa & """ oraz """ & licznik.Odcinek2?.Nazwa & """?") = DialogResult.Yes Then
            Pulpit.LicznikiOsi.Remove(licznik)
            OdswiezListeLicznikow()
        End If
    End Sub

    Private Sub txtLicznik1Adres_TextChanged() Handles txtLicznik1Adres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Adres1 = PobierzKrotkaLiczbeNieujemna(txtLicznik1Adres)
            ZaznaczonyLicznikNaLiscie.SubItems(0).Text = licznik.Adres1.ToString
        End If
    End Sub

    Private Sub txtLicznik1X_TextChanged() Handles txtLicznik1X.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.X1 = PobierzLiczbeNieujemnaRzeczywista(txtLicznik1X)
            RysujPulpit()
        End If
    End Sub

    Private Sub txtLicznik1Y_TextChanged() Handles txtLicznik1Y.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Y1 = PobierzLiczbeNieujemnaRzeczywista(txtLicznik1Y)
            RysujPulpit()
        End If
    End Sub

    Private Sub txtLicznik2Adres_TextChanged() Handles txtLicznik2Adres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Adres2 = PobierzKrotkaLiczbeNieujemna(txtLicznik2Adres)
            ZaznaczonyLicznikNaLiscie.SubItems(1).Text = licznik.Adres2.ToString
        End If
    End Sub

    Private Sub txtLicznik2X_TextChanged() Handles txtLicznik2X.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.X2 = PobierzLiczbeNieujemnaRzeczywista(txtLicznik2X)
            RysujPulpit()
        End If
    End Sub

    Private Sub txtLicznik2Y_TextChanged() Handles txtLicznik2Y.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Y2 = PobierzLiczbeNieujemnaRzeczywista(txtLicznik2Y)
            RysujPulpit()
        End If
    End Sub

    Private Sub cboLicznikTor1_SelectedIndexChanged() Handles cboLicznikTor1.SelectedIndexChanged
        If cboLicznikTor1.SelectedItem Is Nothing Then Exit Sub
        Dim tor As ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboLicznikTor1.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Odcinek1 = tor.Wartosc
            ZaznaczonyLicznikNaLiscie.SubItems(2).Text = licznik.Odcinek1?.Nazwa
        End If
        RysujPulpit()
    End Sub

    Private Sub cboLicznikTor2_SelectedIndexChanged() Handles cboLicznikTor2.SelectedIndexChanged
        If cboLicznikTor2.SelectedItem Is Nothing Then Exit Sub
        Dim tor As ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboLicznikTor2.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Odcinek2 = tor.Wartosc
            ZaznaczonyLicznikNaLiscie.SubItems(3).Text = licznik.Odcinek2?.Nazwa
        End If
        RysujPulpit()
    End Sub

    Private Sub UstawAktywnoscPolLicznikow(wlaczony As Boolean)
        btnLicznikUsun.Enabled = wlaczony
        txtLicznik1Adres.Enabled = wlaczony
        txtLicznik1X.Enabled = wlaczony
        txtLicznik1Y.Enabled = wlaczony
        txtLicznik2Adres.Enabled = wlaczony
        txtLicznik2X.Enabled = wlaczony
        txtLicznik2Y.Enabled = wlaczony
        cboLicznikTor1.Enabled = wlaczony
        cboLicznikTor2.Enabled = wlaczony
    End Sub

    Private Sub OdswiezListeLicznikow()
        Dim licznik As Zaleznosci.ParaLicznikowOsi = Konfiguracja.ZaznaczonyLicznik
        lvLiczniki.Items.Clear()
        ZaznaczonyLicznikNaLiscie = Nothing

        Dim en As List(Of Zaleznosci.ParaLicznikowOsi).Enumerator = Pulpit.LicznikiOsi.GetEnumerator
        While en.MoveNext
            Dim l As Zaleznosci.ParaLicznikowOsi = en.Current
            Dim lvi As New ListViewItem(New String() {l.Adres1.ToString, l.Adres2.ToString, l.Odcinek1?.Nazwa, l.Odcinek2?.Nazwa})
            lvi.Tag = l
            If l Is licznik Then
                lvi.Selected = True
                ZaznaczonyLicznikNaLiscie = lvi
            End If
            lvLiczniki.Items.Add(lvi)
        End While

        If ZaznaczonyLicznikNaLiscie Is Nothing Then
            lvLiczniki_SelectedIndexChanged()
        End If
    End Sub

    Private Sub OdswiezListeTorowWLicznikach()
        OdswiezListeTorowWLicznikach(cboLicznikTor1, Konfiguracja.ZaznaczonyLicznik?.Odcinek1)
        OdswiezListeTorowWLicznikach(cboLicznikTor2, Konfiguracja.ZaznaczonyLicznik?.Odcinek2)
    End Sub

    Private Sub OdswiezListeTorowWLicznikach(cbo As ComboBox, zaznaczony As Zaleznosci.OdcinekToru)
        cbo.Items.Clear()
        cbo.Items.Add(New ObiektComboBox(Of Zaleznosci.OdcinekToru)(Nothing, ""))
        Dim en As IEnumerator(Of Zaleznosci.OdcinekToru) = Pulpit.OdcinkiTorow.OrderBy(Function(f As Zaleznosci.OdcinekToru) f.Nazwa).GetEnumerator()
        While en.MoveNext
            cbo.Items.Add(New ObiektComboBox(Of Zaleznosci.OdcinekToru)(en.Current, en.Current.Nazwa))
        End While
        ZaznaczElement(cbo, zaznaczony)
    End Sub

#End Region 'Zakładka Liczniki osi

#Region "Zakładka Lampy"

    Private Sub lvLampy_SelectedIndexChanged() Handles lvLampy.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonaLampaNaLiscie = PobierzZaznaczonyElementNaLiscie(lvLampy)
        Dim lampa As Zaleznosci.Lampa = PobierzZaznaczonyElement(Of Zaleznosci.Lampa)(lvLampy)
        If lampa Is Nothing Then
            txtLampaAdres.Text = ""
            txtLampaX.Text = ""
            txtLampaY.Text = ""
            UstawAktywnoscPolLamp(False)
        Else
            txtLampaAdres.Text = lampa.Adres.ToString
            txtLampaX.Text = lampa.X.ToString
            txtLampaY.Text = lampa.Y.ToString
            UstawAktywnoscPolLamp(True)
        End If
        Konfiguracja.ZaznaczonaLampa = lampa
        RysujPulpit()
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnLampaDodaj_Click() Handles btnLampaDodaj.Click
        Pulpit.Lampy.Add(New Zaleznosci.Lampa())
        OdswiezListeLamp()
    End Sub

    Private Sub btnLampaUsun_Click() Handles btnLampaUsun.Click
        Dim lampa As Zaleznosci.Lampa = Konfiguracja.ZaznaczonaLampa
        If lampa Is Nothing Then Exit Sub

        If ZadajPytanie("Czy usunąć lampę o adresie " & lampa.Adres.ToString & "?") = DialogResult.Yes Then
            Pulpit.Lampy.Remove(lampa)
            OdswiezListeLamp()
        End If
    End Sub

    Private Sub txtLampaAdres_TextChanged() Handles txtLampaAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = Konfiguracja.ZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.Adres = PobierzKrotkaLiczbeNieujemna(txtLampaAdres)
            ZaznaczonaLampaNaLiscie.SubItems(0).Text = lampa.Adres.ToString
        End If
    End Sub

    Private Sub txtLampaX_TextChanged() Handles txtLampaX.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = Konfiguracja.ZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.X = PobierzLiczbeNieujemnaRzeczywista(txtLampaX)
            ZaznaczonaLampaNaLiscie.SubItems(1).Text = lampa.X.ToString
        End If
        RysujPulpit()
    End Sub

    Private Sub txtLampaY_TextChanged() Handles txtLampaY.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = Konfiguracja.ZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.Y = PobierzLiczbeNieujemnaRzeczywista(txtLampaY)
            ZaznaczonaLampaNaLiscie.SubItems(2).Text = lampa.Y.ToString
        End If
        RysujPulpit()
    End Sub

    Private Sub OdswiezListeLamp()
        Dim lampa As Zaleznosci.Lampa = Konfiguracja.ZaznaczonaLampa
        lvLampy.Items.Clear()
        ZaznaczonaLampaNaLiscie = Nothing

        Dim en As List(Of Zaleznosci.Lampa).Enumerator = Pulpit.Lampy.GetEnumerator
        While en.MoveNext
            Dim l As Zaleznosci.Lampa = en.Current
            Dim lvi As New ListViewItem(New String() {l.Adres.ToString, l.X.ToString, l.Y.ToString})
            lvi.Tag = l
            If l Is lampa Then
                lvi.Selected = True
                ZaznaczonaLampaNaLiscie = lvi
            End If
            lvLampy.Items.Add(lvi)
        End While

        If ZaznaczonaLampaNaLiscie Is Nothing Then
            lvLampy_SelectedIndexChanged()
        End If

        RysujPulpit()
    End Sub

    Private Sub UstawAktywnoscPolLamp(wlaczony As Boolean)
        btnLampaUsun.Enabled = wlaczony
        txtLampaAdres.Enabled = wlaczony
        txtLampaX.Enabled = wlaczony
        txtLampaY.Enabled = wlaczony
    End Sub

#End Region 'Zakładka Lampy

#Region "Pulpit"

    Private Sub pctPulpit_KeyDown(sender As Object, e As KeyEventArgs) Handles pctPulpit.KeyDown
        If Konfiguracja.ZaznaczonaKostka IsNot Nothing Then

            If e.KeyData = Keys.R Then
                Dim obrot As Integer = Konfiguracja.ZaznaczonaKostka.Obrot
                obrot = (obrot + ZWIEKSZ_OBROT) Mod KAT_PELNY
                Konfiguracja.ZaznaczonaKostka.Obrot = obrot
                RysujPulpit()

            ElseIf e.KeyData = Keys.Delete
                Pulpit.UsunKostke(Konfiguracja.ZaznaczonaKostka)
                UsunZaznaczenieKostki()
            End If

        End If
    End Sub

    Private Sub pctPulpit_Click() Handles pctPulpit.Click
        Dim p As Point = PobierzKliknieteWspolrzedneKostki()

        If Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Tory Then
            If CzyKostkaWZakresiePulpitu(p) Then
                Dim kostka As Zaleznosci.Kostka = Pulpit.Kostki(p.X, p.Y)
                If kostka IsNot Nothing AndAlso TypeOf kostka Is Zaleznosci.Tor AndAlso Konfiguracja.ZaznaczonyOdcinek IsNot Nothing Then

                    Dim t As Zaleznosci.Tor = DirectCast(kostka, Zaleznosci.Tor)
                    Dim nalezyDoTegoOdcinka As Boolean = t.NalezyDoOdcinka Is Konfiguracja.ZaznaczonyOdcinek
                    If t.NalezyDoOdcinka IsNot Nothing Then t.NalezyDoOdcinka.KostkiTory.Remove(t)
                    If nalezyDoTegoOdcinka Then
                        t.NalezyDoOdcinka = Nothing
                    Else
                        t.NalezyDoOdcinka = Konfiguracja.ZaznaczonyOdcinek
                        Konfiguracja.ZaznaczonyOdcinek.KostkiTory.Add(t)
                    End If
                    OdswiezLiczbePrzypisanychKostekTorow()

                End If
            End If

        ElseIf Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Lampy
            Dim l As Zaleznosci.Lampa = PobierzKliknietaLampe()

            If l IsNot Nothing Then
                For i As Integer = 0 To lvLampy.Items.Count - 1
                    Dim lvi As ListViewItem = lvLampy.Items(i)
                    If DirectCast(lvi.Tag, Zaleznosci.Lampa) Is l Then
                        lvi.Selected = True
                        Exit For
                    End If
                Next
            End If

        ElseIf Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Nic
            pctPulpit.Focus()
            UkryjPaneleKonf()

            If CzyKostkaNiepusta(p) Then
                Konfiguracja.ZaznaczonaKostka = Pulpit.Kostki(p.X, p.Y)
                PokazPanelKonf()
            Else
                Konfiguracja.ZaznaczonaKostka = Nothing
            End If

        End If

        RysujPulpit()
    End Sub

    Private Sub pctPulpit_MouseWheel(sender As Object, e As MouseEventArgs) Handles pctPulpit.MouseWheel, pnlPulpit.MouseWheel
        Dim sk As Single = Konfiguracja.Skalowanie + e.Delta * SKALOWANIE_ZMIANA
        If sk < SKALOWANIE_MIN Then sk = SKALOWANIE_MIN
        If sk > SKALOWANIE_MAX Then sk = SKALOWANIE_MAX

        Konfiguracja.Skalowanie = sk
        Dim poz As Point = pctPulpit.PointToClient(MousePosition)
        Dim wspx As Double = poz.X / pctPulpit.Size.Width
        Dim wspy As Double = poz.Y / pctPulpit.Size.Height
        RysujPulpit()

        Dim nowy As New Point(CInt(wspx * pctPulpit.Width), CInt(wspy * pctPulpit.Height))
        Dim p As Point = pctPulpit.PointToClient(MousePosition)
        pctPulpit.Location = New Point(pctPulpit.Location.X + p.X - nowy.X, pctPulpit.Location.Y + p.Y - nowy.Y)
    End Sub

    Private Sub pnlPulpit_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlPulpit.MouseMove, pctPulpit.MouseMove
        If e.Button = MouseButtons.Left Then
            If PoprzedniPunkt.X <> -1 Then
                Dim zm As Point = pnlPulpit.PointToClient(MousePosition)
                Dim zmX As Integer = zm.X - PoprzedniPunkt.X
                Dim zmY As Integer = zm.Y - PoprzedniPunkt.Y
                pctPulpit.Location = New Point(LokalizacjaPulpitu.X + zmX, LokalizacjaPulpitu.Y + zmY)
            End If
        End If
    End Sub

    Private Sub pnlPulpit_MouseUp() Handles pnlPulpit.MouseUp, pctPulpit.MouseUp
        PoprzedniPunkt.X = -1
        PoprzedniPunkt.Y = -1
    End Sub

    Private Sub pnlPulpit_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlPulpit.MouseDown, pctPulpit.MouseDown
        If Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Nic And ((ModifierKeys And Keys.Shift) <> 0) Then      'Przesuń kostkę
            Dim p As Point = PobierzKliknieteWspolrzedneKostki()
            If CzyKostkaNiepusta(p) Then
                AkceptowaniePrzeciagania = DragDropEffects.All
                PierwszaObslugaPrzeciagania = True
                DoDragDrop(Pulpit.Kostki(p.X, p.Y), DragDropEffects.Move)
            End If
        Else        'Przesuń cały pulpit
            PoprzedniPunkt = pnlPulpit.PointToClient(MousePosition)
            LokalizacjaPulpitu.X = pctPulpit.Location.X
            LokalizacjaPulpitu.Y = pctPulpit.Location.Y
        End If
    End Sub

    Private Sub pnlPulpit_DragOver(sender As Object, e As DragEventArgs) Handles pnlPulpit.DragOver
        If Not e.Data.GetFormats()(0).StartsWith("Zaleznosci.") Then
            e.Effect = DragDropEffects.None
            Exit Sub
        End If

        Dim p As Point = PobierzKliknieteWspolrzedneKostki()

        If CzyKostkaWZakresiePulpitu(p) AndAlso (
            (PierwszaObslugaPrzeciagania AndAlso (Pulpit.Kostki(p.X, p.Y) Is Nothing Or Pulpit.Kostki(p.X, p.Y) Is PobierzDodawanaKostke(e))) Or
            (p <> PoprzLokalizacjaKostki AndAlso Pulpit.Kostki(p.X, p.Y) Is Nothing)
            ) Then

            If PierwszaObslugaPrzeciagania Then
                AkceptowaniePrzeciagania = DragDropEffects.All
                Konfiguracja.ZaznaczonaKostka = PobierzDodawanaKostke(e)
                UkryjPaneleKonf()
                PokazPanelKonf()
                PierwszaObslugaPrzeciagania = False
            Else
                Pulpit.Kostki(PoprzLokalizacjaKostki.X, PoprzLokalizacjaKostki.Y) = Nothing
            End If

            Pulpit.Kostki(p.X, p.Y) = Konfiguracja.ZaznaczonaKostka
            PoprzLokalizacjaKostki = p
            RysujPulpit()
        End If

        e.Effect = AkceptowaniePrzeciagania
    End Sub

    Private Sub pnlPulpit_Resize() Handles pnlPulpit.Resize
        RysujPulpit()
    End Sub

    Private Sub RysujPulpit()
        Dim img As Image = pctPulpit.Image
        Dim obr As Image = Rysuj(Pulpit, Konfiguracja)
        pctPulpit.Image = obr
        pctPulpit.Size = obr.Size
        img?.Dispose()
    End Sub

    Private Function PobierzDodawanaKostke(e As DragEventArgs) As Zaleznosci.Kostka
        Return DirectCast(e.Data.GetData(e.Data.GetFormats()(0)), Zaleznosci.Kostka)
    End Function

    Private Function CzyKostkaWZakresiePulpitu(wspolrzedne As Point) As Boolean
        Return wspolrzedne.X >= 0 And wspolrzedne.X < Pulpit.Szerokosc And wspolrzedne.Y >= 0 And wspolrzedne.Y < Pulpit.Wysokosc
    End Function

    Private Function CzyKostkaNiepusta(wspolrzedne As Point) As Boolean
        Return CzyKostkaWZakresiePulpitu(wspolrzedne) AndAlso Pulpit.Kostki(wspolrzedne.X, wspolrzedne.Y) IsNot Nothing
    End Function

    Private Function PobierzKliknieteWspolrzedneKostki() As Point
        Dim k As Point = pctPulpit.PointToClient(MousePosition)
        Return New Point(
        CInt(k.X / Konfiguracja.Skalowanie - 0.5),
        CInt(k.Y / Konfiguracja.Skalowanie - 0.5)
        )
    End Function

    Private Function PobierzKliknietaLampe() As Zaleznosci.Lampa
        If Not Konfiguracja.DodatkoweObiekty = RysujDodatkoweObiekty.Lampy Then Return Nothing

        Dim k As Point = pctPulpit.PointToClient(MousePosition)
        Dim s As PointF = New PointF(k.X / Konfiguracja.Skalowanie, k.Y / Konfiguracja.Skalowanie)
        Dim pol As Single = Rysowanie.KOLKO_SZER / 2
        Dim en As List(Of Zaleznosci.Lampa).Enumerator = Pulpit.Lampy.GetEnumerator

        While en.MoveNext
            Dim l As Zaleznosci.Lampa = en.Current
            If (s.X <= l.X + pol) And (s.X >= l.X - pol) And (s.Y <= l.Y + pol) And (s.Y >= l.Y - pol) Then
                Return l
            End If
        End While

        Return Nothing
    End Function

#End Region 'Pulpit

#Region "Reszta"

    Private Sub UsunZaznaczenieKostki()
        UkryjPaneleKonf()
        Konfiguracja.ZaznaczonaKostka = Nothing
        RysujPulpit()
    End Sub

    Private Sub ZaznaczElement(Of T As Class)(cbo As ComboBox, el As T)
        If el Is Nothing Then
            cbo.SelectedItem = Nothing
            Exit Sub
        End If

        For i As Integer = 0 To cbo.Items.Count - 1
            If DirectCast(cbo.Items(i), ObiektComboBox(Of T)).Wartosc Is el Then
                cbo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Function PobierzZaznaczonyElementNaLiscie(lv As ListView) As ListViewItem
        If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count = 0 Then
            Return Nothing
        Else
            Return lv.SelectedItems(0)
        End If
    End Function

    Private Function PobierzLiczbeNieujemna(pole As TextBox) As Integer
        Dim liczba As Integer = 0
        If Integer.TryParse(pole.Text, liczba) Then
            If liczba < 0 Then liczba = 0
        End If

        Return liczba
    End Function

    Private Function PobierzKrotkaLiczbeNieujemna(pole As TextBox) As UShort
        Dim liczba As Integer = PobierzLiczbeNieujemna(pole)
        If liczba >= UShort.MinValue And liczba <= UShort.MaxValue Then
            Return Convert.ToUInt16(liczba)
        Else
            Return 0
        End If
    End Function

    Private Function PobierzLiczbeNieujemnaRzeczywista(pole As TextBox) As Single
        Dim liczba As Single = 0.0
        If Single.TryParse(pole.Text, liczba) Then
            If liczba < 0.0 Then liczba = 0.0
        End If

        Return liczba
    End Function

#End Region 'Reszta

End Class