Public Class wndKonfiguratorStacji
    Private ReadOnly NAZWA_OKNA As String
    Private Const FILTR_PLIKU As String = Zaleznosci.Pulpit.OPIS_PLIKU & "|*" & Zaleznosci.Pulpit.ROZSZERZENIE_PLIKU
    Private Const ROZMIAR_KOSTKI_LISTA As Integer = 48
    Private Shared ReadOnly PUSTY_CBO_KOSTKA As New ObiektComboBox(Of Zaleznosci.Kostka)(Nothing, "")
    Private Shared ReadOnly PUSTY_CBO_ODCINEK_TORU As New ObiektComboBox(Of Zaleznosci.OdcinekToru)(Nothing, "")

    Private WyswietlonyPanelKonf As Panel
    Private ZdarzeniaWlaczone As Boolean = True
    Private ZaznaczonaLampaNaLiscie As ListViewItem
    Private ZaznaczonyOdcinekNaLiscie As ListViewItem
    Private ZaznaczonyLicznikNaLiscie As ListViewItem

    Private Delegate Function SprawdzTypKostki(kostka As Zaleznosci.Kostka) As Boolean
    Private Delegate Function PobierzNazweKostki(kostka As Zaleznosci.Kostka) As String

#Region "Okno"

    Public Sub New()
        InitializeComponent()
        NAZWA_OKNA = Text
    End Sub

    Private Sub wndKonfiguratorStacji_Load() Handles Me.Load
        plpPulpit.TypRysownika = TypRysownika.KlasycznyDirect2D
        plpPulpit.Wysrodkuj()

        Dim PaneleKonfKostek As Panel() = {pnlKonfPrzycisk, pnlKonfRozjazd, pnlKonfSygn, pnlKonfSygnPowt, pnlKonfTor, pnlKonfNapis, pnlKonfKier}
        For i As Integer = 0 To PaneleKonfKostek.Length - 1
            PaneleKonfKostek(i).Width = splKartaPulpit.Panel2.Width
            PaneleKonfKostek(i).Location = New Point(0, 0)
        Next

        UtworzListeKostek()

        UstawAktywnoscPolLamp(False)
        UstawAktywnoscPolLicznikow(False)
        UstawAktywnoscPolOdcinkow(False)

        pnlTorKolorTenOdcinek.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlTorKolorInnyOdcinek.BackColor = plpPulpit.Rysownik.KOLOR_TOR_PRZYPISANY
        pnlTorKolorNieprzypisany.BackColor = plpPulpit.Rysownik.KOLOR_TOR_NIEPRZYPISANY

        pnlLicznik1.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlLicznik2.BackColor = plpPulpit.Rysownik.KOLOR_TOR_LICZNIK_ODCINEK_2

        pnlLicznikOdcinek1.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlLicznikOdcinek2.BackColor = plpPulpit.Rysownik.KOLOR_TOR_LICZNIK_ODCINEK_2
    End Sub

    Private Sub wndKonfiguratorStacji_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not PrzetworzPorzucaniePliku()
    End Sub

    Private Sub tabUstawienia_Selected() Handles tabUstawienia.Selected
        If tabUstawienia.SelectedTab Is tbpPulpit Then
            plpPulpit.projDodatkoweObiekty = RysujDodatkoweObiekty.Nic
            If WyswietlonyPanelKonf Is pnlKonfSygn Then PokazKonfSygnOdcinki()
        End If

        If tabUstawienia.SelectedTab Is tbpOdcinki Then
            plpPulpit.projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow
            OdswiezListeOdcinkow()
        End If

        If tabUstawienia.SelectedTab Is tbpLiczniki Then
            plpPulpit.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki
            OdswiezListeLicznikow()
            OdswiezListeOdcinkowWLicznikach()
        End If

        If tabUstawienia.SelectedTab Is tbpLampy Then
            plpPulpit.projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy
        End If
    End Sub

    Private Sub DodajKostkeDoListy(pulpit As PulpitSterowniczy, kostka As Zaleznosci.Kostka, nazwa As String)
        pulpit.Pulpit.Kostki(0, 0) = kostka
        Dim bm As New Bitmap(ROZMIAR_KOSTKI_LISTA, ROZMIAR_KOSTKI_LISTA)
        pulpit.DrawToBitmap(bm, New Rectangle(0, 0, ROZMIAR_KOSTKI_LISTA, ROZMIAR_KOSTKI_LISTA))
        imlKostki.Images.Add(bm)
        lvPulpitKostki.Items.Add(New ListViewItem(nazwa, imlKostki.Images.Count - 1) With {
            .Tag = kostka.GetType()
        })
    End Sub

    Private Sub UtworzListeKostek()
        Dim p As New PulpitSterowniczy With {.Skalowanie = ROZMIAR_KOSTKI_LISTA - 1, .RysujKrawedzieKostek = False, .TypRysownika = TypRysownika.KlasycznyGDI, .TrybProjektowy = True}

        DodajKostkeDoListy(p, New Zaleznosci.Tor(), "Tor")
        DodajKostkeDoListy(p, New Zaleznosci.TorKoniec(), "Koniec toru")
        DodajKostkeDoListy(p, New Zaleznosci.Zakret(), "Zakręt")
        DodajKostkeDoListy(p, New Zaleznosci.RozjazdLewo(), "Rozjazd lewy")
        DodajKostkeDoListy(p, New Zaleznosci.RozjazdPrawo(), "Rozjazd prawy")
        DodajKostkeDoListy(p, New Zaleznosci.SygnalizatorSamoczynny(), "Sygnalizator samoczynny")
        DodajKostkeDoListy(p, New Zaleznosci.SygnalizatorManewrowy(), "Sygnalizator manewrowy")
        DodajKostkeDoListy(p, New Zaleznosci.SygnalizatorPowtarzajacy(), "Sygnalizator powtarzający")
        DodajKostkeDoListy(p, New Zaleznosci.SygnalizatorPolsamoczynny(), "Sygnalizator półsamoczynny")
        DodajKostkeDoListy(p, New Zaleznosci.Przycisk(), "Przycisk")
        DodajKostkeDoListy(p, New Zaleznosci.PrzyciskTor(), "Przycisk z torem")
        DodajKostkeDoListy(p, New Zaleznosci.Kierunek(), "Wjazd/wyjazd ze stacji")
        DodajKostkeDoListy(p, New Zaleznosci.Napis(), "Napis")
    End Sub

    Private Sub UstawTytulOkna()
        If plpPulpit.Pulpit.Nazwa = "" Then
            Text = NAZWA_OKNA
        Else
            Text = NAZWA_OKNA & " - " & plpPulpit.Pulpit.Nazwa
        End If
    End Sub

    Private Sub CzyscDane(Optional nowyPulpit As Zaleznosci.Pulpit = Nothing)
        plpPulpit.Czysc(nowyPulpit)
        OdswiezListeLamp()
        tabUstawienia_Selected()
    End Sub

    Private Sub OdswiezPoZmianieRozmiaruPulpitu()
        plpPulpit.Invalidate()
        If tabUstawienia.SelectedTab Is tbpLiczniki Then OdswiezListeLicznikow()
        If tabUstawienia.SelectedTab Is tbpLampy Then OdswiezListeLamp()
    End Sub

    Private Function Zapisz(nowyPlik As Boolean) As Boolean
        Dim nowaSciezka As String = Nothing
        Dim wynik As Boolean

        If plpPulpit.Pulpit.SciezkaPliku = "" Or nowyPlik Then
            Dim dlg As New SaveFileDialog With {
                .Filter = FILTR_PLIKU
            }
            If dlg.ShowDialog = DialogResult.OK Then
                nowaSciezka = dlg.FileName
            Else
                Return False
            End If
        End If

        If nowaSciezka IsNot Nothing Then
            wynik = plpPulpit.Pulpit.Zapisz(nowaSciezka)
        Else
            wynik = plpPulpit.Pulpit.Zapisz()
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
            UstawTytulOkna()
        End If
    End Sub

    Private Sub mnuOtworz_Click() Handles mnuOtworz.Click
        If PrzetworzPorzucaniePliku() Then
            Dim dlg As New OpenFileDialog With {
                .Filter = FILTR_PLIKU
            }
            If dlg.ShowDialog = DialogResult.OK Then
                Dim pulpitNowy As Zaleznosci.Pulpit = Zaleznosci.Pulpit.Otworz(dlg.FileName)
                If pulpitNowy IsNot Nothing Then
                    CzyscDane(pulpitNowy)
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
        Dim wnd As New wndEdytorPowierzchni(wndEdytorPowierzchni.TypEdycji.Dodaj, plpPulpit.Pulpit.Szerokosc, plpPulpit.Pulpit.Wysokosc)
        If wnd.ShowDialog = DialogResult.OK Then
            plpPulpit.Pulpit.PowiekszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek)
            OdswiezPoZmianieRozmiaruPulpitu()
        End If
    End Sub

    Private Sub mnuUsunKostki_Click() Handles mnuUsunKostki.Click
        Dim wnd As New wndEdytorPowierzchni(wndEdytorPowierzchni.TypEdycji.Usun, plpPulpit.Pulpit.Szerokosc, plpPulpit.Pulpit.Wysokosc)
        If wnd.ShowDialog = DialogResult.OK Then
            Try
                Dim wynik As Zaleznosci.ObiektBlokujacyZmniejszaniePulpitu = plpPulpit.Pulpit.PomniejszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek)

                If wynik = 0 Then
                    OdswiezPoZmianieRozmiaruPulpitu()
                Else
                    Dim obiekty As New List(Of String)
                    If (wynik And Zaleznosci.ObiektBlokujacyZmniejszaniePulpitu.Kostka) <> 0 Then obiekty.Add("kostka")
                    If (wynik And Zaleznosci.ObiektBlokujacyZmniejszaniePulpitu.LicznikOsi) <> 0 Then obiekty.Add("licznik osi")
                    If (wynik And Zaleznosci.ObiektBlokujacyZmniejszaniePulpitu.Lampa) <> 0 Then obiekty.Add("lampa")

                    PokazBlad($"Nie udało się usunąć kostek - w wybranym zakresie usuwania pulpit nie jest pusty. Znajdują się tam następujące rodzaje obiektów: {String.Join(", ", obiekty)}.")
                End If

            Catch ex As Exception
                PokazBlad("Wystąpił błąd podczas usuwania kostek:" & vbCrLf & ex.Message)
            End Try
        End If
    End Sub

    Private Sub mnuNazwa_Click() Handles mnuNazwa.Click
        Dim wnd As New wndNazwaStacji(plpPulpit.Pulpit.Adres, plpPulpit.Pulpit.Nazwa, plpPulpit.Pulpit.DataUtworzenia)
        If wnd.ShowDialog = DialogResult.OK Then
            plpPulpit.Pulpit.Adres = wnd.Adres
            plpPulpit.Pulpit.Nazwa = wnd.Nazwa
            UstawTytulOkna()
        End If
    End Sub

    Private Sub ctxSortuj_Click() Handles ctxSortuj.Click
        If tabUstawienia.SelectedTab Is tbpOdcinki Then
            plpPulpit.Pulpit.SortujOdcinkiNazwaRosnaco()
            OdswiezListeOdcinkow()

        ElseIf tabUstawienia.SelectedTab Is tbpLiczniki Then
            plpPulpit.Pulpit.SortujLicznikiAdres1Rosnaco()
            OdswiezListeLicznikow()

        ElseIf tabUstawienia.SelectedTab Is tbpLampy Then
            plpPulpit.Pulpit.SortujLampyAdresRosnaco()
            OdswiezListeLamp()

        End If
    End Sub

#End Region 'Menu

#Region "Zakładka Pulpit"

    Private Sub lvKostki_MouseDown(sender As Object, e As MouseEventArgs) Handles lvPulpitKostki.MouseDown
        Dim lvi As ListViewItem = lvPulpitKostki.GetItemAt(e.X, e.Y)
        If lvi IsNot Nothing Then
            lvi.Selected = True
            DoDragDrop(Activator.CreateInstance(DirectCast(lvi.Tag, Type)), DragDropEffects.Copy)
        End If
    End Sub


    'Tor
    Private Sub txtKonfTorPredkosc_TextChanged() Handles txtKonfTorPredkosc.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor).PredkoscZasadnicza = PobierzKrotkaLiczbeNieujemna(txtKonfTorPredkosc)
    End Sub


    'Rozjazd
    Private Sub txtKonfRozjazdAdres_TextChanged() Handles txtKonfRozjazdAdres.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).Adres = PobierzKrotkaLiczbeNieujemna(txtKonfRozjazdAdres)
    End Sub

    Private Sub txtKonfRozjazdNazwa_TextChanged() Handles txtKonfRozjazdNazwa.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).Nazwa = txtKonfRozjazdNazwa.Text
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtKonfRozjazdPredkZasad_TextChanged() Handles txtKonfRozjazdPredkZasad.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).PredkoscZasadnicza = PobierzKrotkaLiczbeNieujemna(txtKonfRozjazdPredkZasad)
    End Sub

    Private Sub txtKonfRozjazdPredkBoczna_TextChanged() Handles txtKonfRozjazdPredkBoczna.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).PredkoscBoczna = PobierzKrotkaLiczbeNieujemna(txtKonfRozjazdPredkBoczna)
    End Sub

    Private Sub cboKonfRozjazdWprost1_SelectedIndexChanged() Handles cboKonfRozjazdWprost1.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdWprost1, rbKonfRozjazdWprost1Plus, rbKonfRozjazdWprost1Minus, roz) Then
            Dim rozZazn As Zaleznosci.Rozjazd = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd)
            rozZazn.ZaleznosciJesliWprost(0).RozjazdZalezny = roz

            ZdarzeniaWlaczone = False
            PokazDostepneRozjazdyZalezne(cboKonfRozjazdWprost2, rozZazn.ZaleznosciJesliWprost(1).RozjazdZalezny, roz)
            ZdarzeniaWlaczone = True
        End If
    End Sub

    Private Sub cboKonfRozjazdWprost2_SelectedIndexChanged() Handles cboKonfRozjazdWprost2.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdWprost2, rbKonfRozjazdWprost2Plus, rbKonfRozjazdWprost2Minus, roz) Then
            Dim rozZazn As Zaleznosci.Rozjazd = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd)
            rozZazn.ZaleznosciJesliWprost(1).RozjazdZalezny = roz

            ZdarzeniaWlaczone = False
            PokazDostepneRozjazdyZalezne(cboKonfRozjazdWprost1, rozZazn.ZaleznosciJesliWprost(0).RozjazdZalezny, roz)
            ZdarzeniaWlaczone = True
        End If
    End Sub

    Private Sub cboKonfRozjazdBok1_SelectedIndexChanged() Handles cboKonfRozjazdBok1.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdBok1, rbKonfRozjazdBok1Plus, rbKonfRozjazdBok1Minus, roz) Then
            Dim rozZazn As Zaleznosci.Rozjazd = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd)
            rozZazn.ZaleznosciJesliBok(0).RozjazdZalezny = roz

            ZdarzeniaWlaczone = False
            PokazDostepneRozjazdyZalezne(cboKonfRozjazdBok2, rozZazn.ZaleznosciJesliBok(1).RozjazdZalezny, roz)
            ZdarzeniaWlaczone = True
        End If
    End Sub

    Private Sub cboKonfRozjazdBok2_SelectedIndexChanged() Handles cboKonfRozjazdBok2.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdBok2, rbKonfRozjazdBok2Plus, rbKonfRozjazdBok2Minus, roz) Then
            Dim rozZazn As Zaleznosci.Rozjazd = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd)
            rozZazn.ZaleznosciJesliBok(1).RozjazdZalezny = roz

            ZdarzeniaWlaczone = False
            PokazDostepneRozjazdyZalezne(cboKonfRozjazdBok1, rozZazn.ZaleznosciJesliBok(0).RozjazdZalezny, roz)
            ZdarzeniaWlaczone = True
        End If
    End Sub

    Private Sub rbKonfRozjazdWprost1Plus_CheckedChanged() Handles rbKonfRozjazdWprost1Plus.CheckedChanged
        If rbKonfRozjazdWprost1Plus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdWprost1Minus_CheckedChanged() Handles rbKonfRozjazdWprost1Minus.CheckedChanged
        If rbKonfRozjazdWprost1Minus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdWprost2Plus_CheckedChanged() Handles rbKonfRozjazdWprost2Plus.CheckedChanged
        If rbKonfRozjazdWprost2Plus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdWprost2Minus_CheckedChanged() Handles rbKonfRozjazdWprost2Minus.CheckedChanged
        If rbKonfRozjazdWprost2Minus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdBok1Plus_CheckedChanged() Handles rbKonfRozjazdBok1Plus.CheckedChanged
        If rbKonfRozjazdBok1Plus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdBok1Minus_CheckedChanged() Handles rbKonfRozjazdBok1Minus.CheckedChanged
        If rbKonfRozjazdBok1Minus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdBok2Plus_CheckedChanged() Handles rbKonfRozjazdBok2Plus.CheckedChanged
        If rbKonfRozjazdBok2Plus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdBok2Minus_CheckedChanged() Handles rbKonfRozjazdBok2Minus.CheckedChanged
        If rbKonfRozjazdBok2Minus.Checked Then DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Bok
    End Sub


    'Sygnalizacja
    Private Sub txtKonfSygnAdres_TextChanged() Handles txtKonfSygnAdres.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator).Adres = PobierzKrotkaLiczbeNieujemna(txtKonfSygnAdres)
    End Sub

    Private Sub txtKonfSygnNazwa_TextChanged() Handles txtKonfSygnNazwa.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator).Nazwa = txtKonfSygnNazwa.Text
        plpPulpit.Invalidate()
    End Sub

    Private Sub cboKonfSygnOdcinekNast_SelectedIndexChanged() Handles cboKonfSygnOdcinekNast.SelectedIndexChanged
        If cboKonfSygnOdcinekNast.SelectedItem Is Nothing Then Exit Sub
        Dim el As ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboKonfSygnOdcinekNast.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru))
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator).OdcinekNastepujacy = el.Wartosc
    End Sub

    Private Sub cboKonfSygnSygnNast_SelectedIndexChanged() Handles cboKonfSygnSygnNast.SelectedIndexChanged
        If cboKonfSygnSygnNast.SelectedItem Is Nothing Then Exit Sub
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cboKonfSygnSygnNast.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka))
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorUzalezniony).SygnalizatorNastepny = DirectCast(el.Wartosc, Zaleznosci.Sygnalizator)
    End Sub

    Private Sub txtKonfSygnPredkosc_TextChanged() Handles txtKonfSygnPredkosc.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator).PredkoscZasadnicza = PobierzKrotkaLiczbeNieujemna(txtKonfSygnPredkosc)
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


    'Sygnalizator powtarzający
    Private Sub txtKonfSygnPowtAdres_TextChanged() Handles txtKonfSygnPowtAdres.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy).Adres = PobierzKrotkaLiczbeNieujemna(txtKonfSygnPowtAdres)
    End Sub

    Private Sub rbKonfSygnPowtKolejnoscI_CheckedChanged() Handles rbKonfSygnPowtKolejnoscI.CheckedChanged
        UstawKolejnoscSygnalizatoraPowtarzajacego(rbKonfSygnPowtKolejnoscI, Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Pierwszy)
    End Sub

    Private Sub rbKonfSygnPowtKolejnoscII_CheckedChanged() Handles rbKonfSygnPowtKolejnoscII.CheckedChanged
        UstawKolejnoscSygnalizatoraPowtarzajacego(rbKonfSygnPowtKolejnoscII, Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Drugi)
    End Sub

    Private Sub rbKonfSygnPowtKolejnoscIII_CheckedChanged() Handles rbKonfSygnPowtKolejnoscIII.CheckedChanged
        UstawKolejnoscSygnalizatoraPowtarzajacego(rbKonfSygnPowtKolejnoscIII, Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Trzeci)
    End Sub

    Private Sub cboKonfSygnPowtSygnObslugiwany_SelectedIndexChanged() Handles cboKonfSygnPowtSygnObslugiwany.SelectedIndexChanged
        If cboKonfSygnPowtSygnObslugiwany.SelectedItem Is Nothing Then Exit Sub
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cboKonfSygnPowtSygnObslugiwany.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka))
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy).SygnalizatorPowtarzany = DirectCast(el.Wartosc, Zaleznosci.SygnalizatorPolsamoczynny)
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtKonfSygnPowtPredkosc_TextChanged() Handles txtKonfSygnPowtPredkosc.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy).PredkoscZasadnicza = PobierzKrotkaLiczbeNieujemna(txtKonfSygnPowtPredkosc)
    End Sub


    'Przycisk
    Private Sub cboKonfPrzyciskTyp_SelectedIndexChanged() Handles cboKonfPrzyciskTyp.SelectedIndexChanged
        Dim aktywny As Boolean = False
        Dim el As Object = cboKonfPrzyciskTyp.SelectedItem

        If el Is Nothing Then
            aktywny = False
        Else
            aktywny = True

            Select Case plpPulpit.ZaznaczonaKostka.Typ
                Case Zaleznosci.TypKostki.Przycisk
                    Dim typ As Zaleznosci.TypPrzyciskuEnum = DirectCast(el, ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)).Wartosc
                    Dim prz As Zaleznosci.Przycisk = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Przycisk)
                    prz.TypPrzycisku = typ
                    If typ = Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow Then aktywny = False
                    cboKonfPrzyciskSygnalizator.Items.Clear()
                    cboKonfPrzyciskSygnalizator.Items.AddRange(PobierzElementyDoComboBox(AddressOf CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweSygnalizatora))
                    ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskSygnalizator, prz.ObslugiwanySygnalizator)

                Case Zaleznosci.TypKostki.PrzyciskTor
                    Dim typ As Zaleznosci.TypPrzyciskuTorEnum = DirectCast(el, ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)).Wartosc
                    Dim prz As Zaleznosci.PrzyciskTor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
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
        plpPulpit.Invalidate()
    End Sub

    Private Sub cboKonfPrzyciskSygnalizator_SelectedIndexChanged() Handles cboKonfPrzyciskSygnalizator.SelectedIndexChanged
        If cboKonfPrzyciskSygnalizator.SelectedItem Is Nothing Then Exit Sub

        Dim sygn As Zaleznosci.Kostka = DirectCast(cboKonfPrzyciskSygnalizator.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc
        Select Case plpPulpit.ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Przycisk)
                prz.ObslugiwanySygnalizator = DirectCast(sygn, Zaleznosci.SygnalizatorPolsamoczynny)

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                prz.ObslugiwanySygnalizator = DirectCast(sygn, Zaleznosci.Sygnalizator)

        End Select

        plpPulpit.Invalidate()
    End Sub

    Private Sub txtKonfPrzyciskPredkosc_TextChanged() Handles txtKonfPrzyciskPredkosc.TextChanged
        If plpPulpit.ZaznaczonaKostka.Typ = Zaleznosci.TypKostki.PrzyciskTor Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor).PredkoscZasadnicza = PobierzKrotkaLiczbeNieujemna(txtKonfPrzyciskPredkosc)
        End If
    End Sub


    'Napis
    Private Sub txtKonfNapisTekst_TextChanged() Handles txtKonfNapisTekst.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Napis).Tekst = txtKonfNapisTekst.Text
        plpPulpit.Invalidate()
    End Sub


    'Kierunek
    Private Sub txtKonfKierPredkosc_TextChanged() Handles txtKonfKierPredkosc.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek).PredkoscZasadnicza = PobierzKrotkaLiczbeNieujemna(txtKonfKierPredkosc)
    End Sub

    Private Sub rbKonfKierZasadniczy_CheckedChanged() Handles rbKonfKierZasadniczy.CheckedChanged
        If rbKonfKierZasadniczy.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek).KierunekWlaczany = Zaleznosci.KierunekWlaczanyEnum.Zasadniczy
        End If
    End Sub

    Private Sub rbKonfKierPrzeciwny_CheckedChanged() Handles rbKonfKierPrzeciwny.CheckedChanged
        If rbKonfKierPrzeciwny.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek).KierunekWlaczany = Zaleznosci.KierunekWlaczanyEnum.Przeciwny
        End If
    End Sub


    'Wyświetlanie paneli
    Private Sub PokazPanelKonf()
        Dim nowyPanel As Panel = Nothing

        If plpPulpit.ZaznaczonaKostka IsNot Nothing Then

            Select Case plpPulpit.ZaznaczonaKostka.Typ
                Case Zaleznosci.TypKostki.Tor, Zaleznosci.TypKostki.Zakret
                    nowyPanel = PokazKonfTor()
                Case Zaleznosci.TypKostki.RozjazdLewo, Zaleznosci.TypKostki.RozjazdPrawo
                    nowyPanel = PokazKonfRozjazd()
                Case Zaleznosci.TypKostki.SygnalizatorManewrowy, Zaleznosci.TypKostki.SygnalizatorSamoczynny, Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                    nowyPanel = PokazKonfSygn()
                Case Zaleznosci.TypKostki.SygnalizatorPowtarzajacy
                    nowyPanel = PokazKonfSygnPowt()
                Case Zaleznosci.TypKostki.Przycisk, Zaleznosci.TypKostki.PrzyciskTor
                    nowyPanel = PokazKonfPrzycisk()
                Case Zaleznosci.TypKostki.Napis
                    nowyPanel = PokazKonfNapis()
                Case Zaleznosci.TypKostki.Kierunek
                    nowyPanel = PokazKonfKier()
            End Select

        End If

        If nowyPanel IsNot WyswietlonyPanelKonf Then
            If WyswietlonyPanelKonf IsNot Nothing Then WyswietlonyPanelKonf.Visible = False
            If nowyPanel IsNot Nothing Then nowyPanel.Visible = True
            WyswietlonyPanelKonf = nowyPanel
        End If
    End Sub

    Private Function PokazKonfTor() As Panel
        Dim tor As Zaleznosci.Tor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor)

        txtKonfTorPredkosc.Text = tor.PredkoscZasadnicza.ToString()
        Return pnlKonfTor
    End Function

    Private Function PokazKonfRozjazd() As Panel
        Dim roz As Zaleznosci.Rozjazd = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd)
        txtKonfRozjazdAdres.Text = roz.Adres.ToString
        txtKonfRozjazdNazwa.Text = roz.Nazwa
        txtKonfRozjazdPredkZasad.Text = roz.PredkoscZasadnicza.ToString
        txtKonfRozjazdPredkBoczna.Text = roz.PredkoscBoczna.ToString

        PokazDostepneRozjazdyZalezne(cboKonfRozjazdWprost1, roz.ZaleznosciJesliWprost(0).RozjazdZalezny, roz.ZaleznosciJesliWprost(1).RozjazdZalezny)
        PokazDostepneRozjazdyZalezne(cboKonfRozjazdWprost2, roz.ZaleznosciJesliWprost(1).RozjazdZalezny, roz.ZaleznosciJesliWprost(0).RozjazdZalezny)
        PokazDostepneRozjazdyZalezne(cboKonfRozjazdBok1, roz.ZaleznosciJesliBok(0).RozjazdZalezny, roz.ZaleznosciJesliBok(1).RozjazdZalezny)
        PokazDostepneRozjazdyZalezne(cboKonfRozjazdBok2, roz.ZaleznosciJesliBok(1).RozjazdZalezny, roz.ZaleznosciJesliBok(0).RozjazdZalezny)

        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliWprost(0).RozjazdZalezny, rbKonfRozjazdWprost1Plus, rbKonfRozjazdWprost1Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliWprost(1).RozjazdZalezny, rbKonfRozjazdWprost2Plus, rbKonfRozjazdWprost2Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliBok(0).RozjazdZalezny, rbKonfRozjazdBok1Plus, rbKonfRozjazdBok1Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliBok(1).RozjazdZalezny, rbKonfRozjazdBok2Plus, rbKonfRozjazdBok2Minus)

        If roz.ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost Then rbKonfRozjazdWprost1Plus.Checked = True Else rbKonfRozjazdWprost1Minus.Checked = True
        If roz.ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost Then rbKonfRozjazdWprost2Plus.Checked = True Else rbKonfRozjazdWprost2Minus.Checked = True
        If roz.ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost Then rbKonfRozjazdBok1Plus.Checked = True Else rbKonfRozjazdBok1Minus.Checked = True
        If roz.ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduEnum.Wprost Then rbKonfRozjazdBok2Plus.Checked = True Else rbKonfRozjazdBok2Minus.Checked = True

        Return pnlKonfRozjazd
    End Function

    Private Sub PokazKonfSygnOdcinki()
        Dim sygn As Zaleznosci.Sygnalizator = TryCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator)
        If sygn Is Nothing Then Exit Sub

        cboKonfSygnOdcinekNast.Items.Clear()
        Dim odcinki As IEnumerable(Of Zaleznosci.OdcinekToru) = plpPulpit.Pulpit.OdcinkiTorow.OrderBy(Function(t As Zaleznosci.OdcinekToru) t.Nazwa)
        For Each odc As Zaleznosci.OdcinekToru In odcinki
            cboKonfSygnOdcinekNast.Items.Add(New ObiektComboBox(Of Zaleznosci.OdcinekToru)(odc, odc.Nazwa))
        Next

        ZaznaczElement(cboKonfSygnOdcinekNast, sygn.OdcinekNastepujacy)
    End Sub

    Private Function PokazKonfSygn() As Panel
        Dim sygn As Zaleznosci.Sygnalizator = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator)
        txtKonfSygnAdres.Text = sygn.Adres.ToString
        txtKonfSygnNazwa.Text = sygn.Nazwa.ToString
        cboKonfSygnSygnNast.Enabled = sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorManewrowy
        pnlKonfSygnSwiatla.Visible = sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny

        PokazKonfSygnOdcinki()

        cboKonfSygnSygnNast.Items.Clear()
        Dim sygn_nast As Zaleznosci.Sygnalizator = Nothing
        If sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorManewrowy Then
            Dim sygnalizatory As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzySygnalizatorUzalezniony, AddressOf PobierzNazweSygnalizatora)
            cboKonfSygnSygnNast.Items.Add(PUSTY_CBO_KOSTKA)
            cboKonfSygnSygnNast.Items.AddRange(sygnalizatory)
            sygn_nast = DirectCast(sygn, Zaleznosci.SygnalizatorUzalezniony).SygnalizatorNastepny
            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfSygnSygnNast, sygn_nast)
        End If

        txtKonfSygnPredkosc.Text = sygn.PredkoscZasadnicza.ToString()

        If sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Then
            Dim sw As Zaleznosci.DostepneSwiatlaEnum = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny).DostepneSwiatla
            cbKonfSygnZiel.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Zielone) <> 0
            cbKonfSygnPomGor.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweGora) <> 0
            cbKonfSygnCzer.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Czerwone) <> 0
            cbKonfSygnPomDol.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweDol) <> 0
            cbKonfSygnBiale.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Biale) <> 0
            cbKonfSygnZielPas.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.ZielonyPas) <> 0
            cbKonfSygnPomPas.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas) <> 0
        End If

        Return pnlKonfSygn
    End Function

    Private Function PokazKonfSygnPowt() As Panel
        Dim sygn As Zaleznosci.SygnalizatorPowtarzajacy = CType(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy)

        txtKonfSygnPowtAdres.Text = sygn.Adres.ToString

        Select Case sygn.Kolejnosc
            Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Pierwszy
                rbKonfSygnPowtKolejnoscI.Checked = True
            Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Drugi
                rbKonfSygnPowtKolejnoscII.Checked = True
            Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Trzeci
                rbKonfSygnPowtKolejnoscIII.Checked = True
        End Select

        Dim sygnalizatory As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweSygnalizatora)
        cboKonfSygnPowtSygnObslugiwany.Items.Clear()
        cboKonfSygnPowtSygnObslugiwany.Items.AddRange(sygnalizatory)
        ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfSygnPowtSygnObslugiwany, sygn.SygnalizatorPowtarzany)

        txtKonfSygnPowtPredkosc.Text = sygn.PredkoscZasadnicza.ToString

        Return pnlKonfSygnPowt
    End Function

    Private Function PokazKonfPrzycisk() As Panel
        cboKonfPrzyciskTyp.Items.Clear()

        Select Case plpPulpit.ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Przycisk)
                cboKonfPrzyciskTyp.Items.AddRange({
                    New ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)(Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy, "Sygnał zastępczy"),
                    New ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)(Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow, "Zwolnienie przebiegów")
                })
                cboKonfPrzyciskTyp.SelectedIndex = prz.TypPrzycisku
                pnlKonfPrzyciskPredkosc.Visible = False

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                cboKonfPrzyciskTyp.Items.AddRange({
                    New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny, "Sygnalizator półsamoczynny"),
                    New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy, "Sygnalizator manewrowy"),
                    New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy, "Sygnał manewrowy na sygnalizatorze półsamoczynnym")
                })
                cboKonfPrzyciskTyp.SelectedIndex = prz.TypPrzycisku
                pnlKonfPrzyciskPredkosc.Visible = True
                txtKonfPrzyciskPredkosc.Text = prz.PredkoscZasadnicza.ToString()

        End Select

        Return pnlKonfPrzycisk
    End Function

    Private Function PokazKonfNapis() As Panel
        Dim napis As Zaleznosci.Napis = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Napis)

        txtKonfNapisTekst.Text = napis.Tekst
        Return pnlKonfNapis
    End Function

    Private Function PokazKonfKier() As Panel
        Dim kierunek As Zaleznosci.Kierunek = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek)

        txtKonfKierPredkosc.Text = kierunek.PredkoscZasadnicza.ToString
        If kierunek.KierunekWlaczany = Zaleznosci.KierunekWlaczanyEnum.Zasadniczy Then
            rbKonfKierZasadniczy.Checked = True
        Else
            rbKonfKierPrzeciwny.Checked = True
        End If

        Return pnlKonfKier
    End Function

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

    Private Sub PokazDostepneRozjazdyZalezne(cbo As ComboBox, rozjazdWybrany As Zaleznosci.Rozjazd, rozjazdUkryty As Zaleznosci.Rozjazd)
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzyRozjazd, AddressOf PobierzNazweRozjazdu, rozjazdUkryty)

        cbo.Items.Clear()
        cbo.Items.Add(PUSTY_CBO_KOSTKA)
        cbo.Items.AddRange(el)

        ZaznaczElement(Of Zaleznosci.Kostka)(cbo, rozjazdWybrany)
    End Sub

    Private Function CzyRozjazd(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.RozjazdLewo Or kostka.Typ = Zaleznosci.TypKostki.RozjazdPrawo
    End Function

    Private Function PobierzNazweRozjazdu(kostka As Zaleznosci.Kostka) As String
        Return DirectCast(kostka, Zaleznosci.Rozjazd).Nazwa
    End Function

    Private Sub UstawDostepneSwiatlo(cb As CheckBox, kolor As Zaleznosci.DostepneSwiatlaEnum)
        Dim sygn As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny)
        If cb.Checked Then
            sygn.DostepneSwiatla = sygn.DostepneSwiatla Or kolor
        Else
            sygn.DostepneSwiatla = sygn.DostepneSwiatla And (Not kolor)
        End If
    End Sub

    Private Sub UstawKolejnoscSygnalizatoraPowtarzajacego(rb As RadioButton, kolejnosc As Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego)
        If rb.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy).Kolejnosc = kolejnosc
            plpPulpit.Invalidate()
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

    Private Function PobierzElementyDoComboBox(sprawdzanie As SprawdzTypKostki, nazwa As PobierzNazweKostki, Optional obiektUnikany As Zaleznosci.Kostka = Nothing) As ObiektComboBox(Of Zaleznosci.Kostka)()
        Dim kostki As New List(Of ObiektComboBox(Of Zaleznosci.Kostka))

        plpPulpit.Pulpit.PrzeiterujKostki(Sub(x, y, k)
                                              If sprawdzanie(k) AndAlso k IsNot plpPulpit.ZaznaczonaKostka AndAlso k IsNot obiektUnikany Then
                                                  kostki.Add(New ObiektComboBox(Of Zaleznosci.Kostka)(k, nazwa(k)))
                                              End If
                                          End Sub)

        Return kostki.OrderBy(Function(k As ObiektComboBox(Of Zaleznosci.Kostka)) nazwa(k.Wartosc)).ToArray()
    End Function

#End Region 'Zakładka Pulpit

#Region "Zakładka Odcinki torów"

    Private Sub lvOdcinki_SelectedIndexChanged() Handles lvOdcinki.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyOdcinekNaLiscie = PobierzZaznaczonyElementNaLiscie(lvOdcinki)
        Dim odcinek As Zaleznosci.OdcinekToru = PobierzZaznaczonyElement(Of Zaleznosci.OdcinekToru)(lvOdcinki)
        If odcinek Is Nothing Then
            txtOdcinekAdres.Text = ""
            txtOdcinekNazwa.Text = ""
            txtOdcinekOpis.Text = ""
            UstawAktywnoscPolOdcinkow(False)
        Else
            txtOdcinekAdres.Text = odcinek.Adres.ToString
            txtOdcinekNazwa.Text = odcinek.Nazwa.ToString
            txtOdcinekOpis.Text = odcinek.Opis.ToString
            UstawAktywnoscPolOdcinkow(True)
        End If
        plpPulpit.projZaznaczonyOdcinek = odcinek
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnOdcinekDodaj_Click() Handles btnOdcinekDodaj.Click
        plpPulpit.Pulpit.OdcinkiTorow.Add(New Zaleznosci.OdcinekToru)
        OdswiezListeOdcinkow()
    End Sub

    Private Sub btnOdcinekUsun_Click() Handles btnOdcinekUsun.Click
        Dim odcinek As Zaleznosci.OdcinekToru = plpPulpit.projZaznaczonyOdcinek
        If odcinek Is Nothing Then Exit Sub

        If ZadajPytanie("Czy usunąć odcinek torów o nazwie " & odcinek.Nazwa & "?") = DialogResult.Yes Then
            plpPulpit.Pulpit.UsunOdcinekToru(odcinek)
            OdswiezListeOdcinkow()
        End If
    End Sub

    Private Sub txtOdcinekAdres_TextChanged() Handles txtOdcinekAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim odc As Zaleznosci.OdcinekToru = plpPulpit.projZaznaczonyOdcinek
        If odc IsNot Nothing Then
            odc.Adres = PobierzKrotkaLiczbeNieujemna(txtOdcinekAdres)
            ZaznaczonyOdcinekNaLiscie.SubItems(0).Text = odc.Adres.ToString
        End If
    End Sub

    Private Sub txtOdcinekNazwa_TextChanged() Handles txtOdcinekNazwa.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim odc As Zaleznosci.OdcinekToru = plpPulpit.projZaznaczonyOdcinek
        If odc IsNot Nothing Then
            odc.Nazwa = txtOdcinekNazwa.Text
            ZaznaczonyOdcinekNaLiscie.SubItems(1).Text = odc.Nazwa
        End If
    End Sub

    Private Sub txtOdcinekOpis_TextChanged() Handles txtOdcinekOpis.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim odc As Zaleznosci.OdcinekToru = plpPulpit.projZaznaczonyOdcinek
        If odc IsNot Nothing Then
            odc.Opis = txtOdcinekOpis.Text
        End If
    End Sub

    Private Sub OdswiezListeOdcinkow()
        Dim odcinek As Zaleznosci.OdcinekToru = plpPulpit.projZaznaczonyOdcinek
        lvOdcinki.Items.Clear()
        ZaznaczonyOdcinekNaLiscie = Nothing

        For Each o As Zaleznosci.OdcinekToru In plpPulpit.Pulpit.OdcinkiTorow
            Dim lvi As New ListViewItem(New String() {o.Adres.ToString, o.Nazwa.ToString, o.KostkiTory.Count.ToString()}) With {
                .Tag = o
            }
            If o Is odcinek Then
                lvi.Selected = True
                ZaznaczonyOdcinekNaLiscie = lvi
            End If
            lvOdcinki.Items.Add(lvi)
        Next

        If ZaznaczonyOdcinekNaLiscie Is Nothing Then lvOdcinki_SelectedIndexChanged()
    End Sub

    Private Sub UstawAktywnoscPolOdcinkow(wlaczony As Boolean)
        btnOdcinekUsun.Enabled = wlaczony
        txtOdcinekAdres.Enabled = wlaczony
        txtOdcinekNazwa.Enabled = wlaczony
        txtOdcinekOpis.Enabled = wlaczony
    End Sub

    Private Sub OdswiezLiczbePrzypisanychKostekTorow()
        If lvOdcinki.Items Is Nothing Then Exit Sub

        For i As Integer = 0 To lvOdcinki.Items.Count - 1
            Dim o As Zaleznosci.OdcinekToru = DirectCast(lvOdcinki.Items(i).Tag, Zaleznosci.OdcinekToru)
            lvOdcinki.Items(i).SubItems(2).Text = o.KostkiTory.Count.ToString()
        Next
    End Sub

#End Region 'Zakładka Odcinki torów

#Region "Zakładka Liczniki osi"

    Private Sub lvLiczniki_SelectedIndexChanged() Handles lvLiczniki.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyLicznikNaLiscie = PobierzZaznaczonyElementNaLiscie(lvLiczniki)
        Dim licznik As Zaleznosci.ParaLicznikowOsi = PobierzZaznaczonyElement(Of Zaleznosci.ParaLicznikowOsi)(lvLiczniki)
        plpPulpit.projZaznaczonyLicznik = licznik
        If licznik Is Nothing Then
            txtLicznik1Adres.Text = ""
            txtLicznik1X.Text = ""
            txtLicznik1Y.Text = ""
            txtLicznik2Adres.Text = ""
            txtLicznik2X.Text = ""
            txtLicznik2Y.Text = ""
            cboLicznikOdcinek1.SelectedItem = Nothing
            cboLicznikOdcinek2.SelectedItem = Nothing
            UstawAktywnoscPolLicznikow(False)
        Else
            txtLicznik1Adres.Text = licznik.Adres1.ToString
            txtLicznik1X.Text = licznik.X1.ToString
            txtLicznik1Y.Text = licznik.Y1.ToString
            txtLicznik2Adres.Text = licznik.Adres2.ToString
            txtLicznik2X.Text = licznik.X2.ToString
            txtLicznik2Y.Text = licznik.Y2.ToString
            OdswiezListeOdcinkowWLicznikach()
            UstawAktywnoscPolLicznikow(True)
        End If
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnLicznikDodaj_Click() Handles btnLicznikDodaj.Click
        plpPulpit.Pulpit.LicznikiOsi.Add(New Zaleznosci.ParaLicznikowOsi())
        OdswiezListeLicznikow()
    End Sub

    Private Sub btnLicznikUsun_Click() Handles btnLicznikUsun.Click
        Const BRAK_ODCINKA As String = "(brak odcinka)"

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik Is Nothing Then Exit Sub

        Dim odc1 As String = If(licznik.Odcinek1?.Nazwa IsNot Nothing, $"""{licznik.Odcinek1?.Nazwa}""", BRAK_ODCINKA)
        Dim odc2 As String = If(licznik.Odcinek2?.Nazwa IsNot Nothing, $"""{licznik.Odcinek2?.Nazwa}""", BRAK_ODCINKA)

        If ZadajPytanie($"Czy usunąć parę liczników osi dla odcinków torów {odc1} oraz {odc2}?") = DialogResult.Yes Then
            plpPulpit.Pulpit.LicznikiOsi.Remove(licznik)
            OdswiezListeLicznikow()
        End If
    End Sub

    Private Sub txtLicznik1Adres_TextChanged() Handles txtLicznik1Adres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Adres1 = PobierzKrotkaLiczbeNieujemna(txtLicznik1Adres)
            ZaznaczonyLicznikNaLiscie.SubItems(0).Text = licznik.Adres1.ToString
        End If
    End Sub

    Private Sub txtLicznik1X_TextChanged() Handles txtLicznik1X.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.X1 = PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(txtLicznik1X, plpPulpit.Pulpit.Szerokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub txtLicznik1Y_TextChanged() Handles txtLicznik1Y.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Y1 = PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(txtLicznik1Y, plpPulpit.Pulpit.Wysokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub txtLicznik2Adres_TextChanged() Handles txtLicznik2Adres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Adres2 = PobierzKrotkaLiczbeNieujemna(txtLicznik2Adres)
            ZaznaczonyLicznikNaLiscie.SubItems(1).Text = licznik.Adres2.ToString
        End If
    End Sub

    Private Sub txtLicznik2X_TextChanged() Handles txtLicznik2X.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.X2 = PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(txtLicznik2X, plpPulpit.Pulpit.Szerokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub txtLicznik2Y_TextChanged() Handles txtLicznik2Y.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Y2 = PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(txtLicznik2Y, plpPulpit.Pulpit.Wysokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub cboLicznikOdcinek1_SelectedIndexChanged() Handles cboLicznikOdcinek1.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboLicznikOdcinek1.SelectedItem Is Nothing Then Exit Sub

        Dim odcinek As ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboLicznikOdcinek1.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Odcinek1 = odcinek.Wartosc
            ZaznaczonyLicznikNaLiscie.SubItems(2).Text = licznik.Odcinek1?.Nazwa
        End If
        plpPulpit.Invalidate()

        ZdarzeniaWlaczone = False
        OdswiezListeOdcinkowWLicznikach(cboLicznikOdcinek2, licznik?.Odcinek2, licznik?.Odcinek1)
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub cboLicznikOdcinek2_SelectedIndexChanged() Handles cboLicznikOdcinek2.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboLicznikOdcinek2.SelectedItem Is Nothing Then Exit Sub

        Dim odcinek As ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboLicznikOdcinek2.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Odcinek2 = odcinek.Wartosc
            ZaznaczonyLicznikNaLiscie.SubItems(3).Text = licznik.Odcinek2?.Nazwa
        End If
        plpPulpit.Invalidate()

        ZdarzeniaWlaczone = False
        OdswiezListeOdcinkowWLicznikach(cboLicznikOdcinek1, licznik?.Odcinek1, licznik?.Odcinek2)
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub UstawAktywnoscPolLicznikow(wlaczony As Boolean)
        btnLicznikUsun.Enabled = wlaczony
        txtLicznik1Adres.Enabled = wlaczony
        txtLicznik1X.Enabled = wlaczony
        txtLicznik1Y.Enabled = wlaczony
        txtLicznik2Adres.Enabled = wlaczony
        txtLicznik2X.Enabled = wlaczony
        txtLicznik2Y.Enabled = wlaczony
        cboLicznikOdcinek1.Enabled = wlaczony
        cboLicznikOdcinek2.Enabled = wlaczony
    End Sub

    Private Sub OdswiezListeLicznikow()
        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        lvLiczniki.Items.Clear()
        ZaznaczonyLicznikNaLiscie = Nothing

        For Each l As Zaleznosci.ParaLicznikowOsi In plpPulpit.Pulpit.LicznikiOsi
            Dim lvi As New ListViewItem(New String() {l.Adres1.ToString, l.Adres2.ToString, l.Odcinek1?.Nazwa, l.Odcinek2?.Nazwa}) With {
                .Tag = l
            }
            If l Is licznik Then
                lvi.Selected = True
                ZaznaczonyLicznikNaLiscie = lvi
            End If
            lvLiczniki.Items.Add(lvi)
        Next

        If ZaznaczonyLicznikNaLiscie Is Nothing Then lvLiczniki_SelectedIndexChanged()
    End Sub

    Private Sub OdswiezListeOdcinkowWLicznikach()
        OdswiezListeOdcinkowWLicznikach(cboLicznikOdcinek1, plpPulpit.projZaznaczonyLicznik?.Odcinek1, plpPulpit.projZaznaczonyLicznik?.Odcinek2)
        OdswiezListeOdcinkowWLicznikach(cboLicznikOdcinek2, plpPulpit.projZaznaczonyLicznik?.Odcinek2, plpPulpit.projZaznaczonyLicznik?.Odcinek1)
    End Sub

    Private Sub OdswiezListeOdcinkowWLicznikach(cbo As ComboBox, zaznaczony As Zaleznosci.OdcinekToru, ukryty As Zaleznosci.OdcinekToru)
        cbo.Items.Clear()
        cbo.Items.Add(PUSTY_CBO_ODCINEK_TORU)
        Dim odcinki As IEnumerable(Of Zaleznosci.OdcinekToru) = plpPulpit.Pulpit.OdcinkiTorow.OrderBy(Function(f As Zaleznosci.OdcinekToru) f.Nazwa)

        For Each odc As Zaleznosci.OdcinekToru In odcinki
            If odc IsNot ukryty Then
                cbo.Items.Add(New ObiektComboBox(Of Zaleznosci.OdcinekToru)(odc, odc.Nazwa))
            End If
        Next

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
        plpPulpit.projZaznaczonaLampa = lampa
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnLampaDodaj_Click() Handles btnLampaDodaj.Click
        plpPulpit.Pulpit.Lampy.Add(New Zaleznosci.Lampa())
        OdswiezListeLamp()
    End Sub

    Private Sub btnLampaUsun_Click() Handles btnLampaUsun.Click
        Dim lampa As Zaleznosci.Lampa = plpPulpit.projZaznaczonaLampa
        If lampa Is Nothing Then Exit Sub

        If ZadajPytanie("Czy usunąć lampę o adresie " & lampa.Adres.ToString & "?") = DialogResult.Yes Then
            plpPulpit.Pulpit.Lampy.Remove(lampa)
            OdswiezListeLamp()
        End If
    End Sub

    Private Sub txtLampaAdres_TextChanged() Handles txtLampaAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = plpPulpit.projZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.Adres = PobierzKrotkaLiczbeNieujemna(txtLampaAdres)
            ZaznaczonaLampaNaLiscie.SubItems(0).Text = lampa.Adres.ToString
        End If
    End Sub

    Private Sub txtLampaX_TextChanged() Handles txtLampaX.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = plpPulpit.projZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.X = PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(txtLampaX, plpPulpit.Pulpit.Szerokosc)
            ZaznaczonaLampaNaLiscie.SubItems(1).Text = lampa.X.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtLampaY_TextChanged() Handles txtLampaY.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = plpPulpit.projZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.Y = PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(txtLampaY, plpPulpit.Pulpit.Wysokosc)
            ZaznaczonaLampaNaLiscie.SubItems(2).Text = lampa.Y.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub OdswiezListeLamp()
        Dim lampa As Zaleznosci.Lampa = plpPulpit.projZaznaczonaLampa
        lvLampy.Items.Clear()
        ZaznaczonaLampaNaLiscie = Nothing

        For Each l As Zaleznosci.Lampa In plpPulpit.Pulpit.Lampy
            Dim lvi As New ListViewItem(New String() {l.Adres.ToString, l.X.ToString, l.Y.ToString}) With {
                .Tag = l
            }

            If l Is lampa Then
                lvi.Selected = True
                ZaznaczonaLampaNaLiscie = lvi
            End If

            lvLampy.Items.Add(lvi)
        Next

        If ZaznaczonaLampaNaLiscie Is Nothing Then lvLampy_SelectedIndexChanged()
    End Sub

    Private Sub UstawAktywnoscPolLamp(wlaczony As Boolean)
        btnLampaUsun.Enabled = wlaczony
        txtLampaAdres.Enabled = wlaczony
        txtLampaX.Enabled = wlaczony
        txtLampaY.Enabled = wlaczony
    End Sub

#End Region 'Zakładka Lampy

#Region "Pulpit"

    Private Sub plpPulpit_ZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka) Handles plpPulpit.ZmianaZaznaczeniaKostki
        PokazPanelKonf()
    End Sub

    Private Sub plpPulpit_projZmianaZaznaczeniaLampy(lampa As Zaleznosci.Lampa) Handles plpPulpit.projZmianaZaznaczeniaLampy
        If Not ZdarzeniaWlaczone Then Exit Sub

        lvLampy.SelectedItems.Clear()

        If lampa IsNot Nothing Then
            For i As Integer = 0 To lvLampy.Items.Count - 1
                Dim lvi As ListViewItem = lvLampy.Items(i)
                If DirectCast(lvi.Tag, Zaleznosci.Lampa) Is lampa Then
                    lvi.Selected = True
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub plpPulpit_projZmianaPrzypisaniaToruDoOdcinka() Handles plpPulpit.projZmianaPrzypisaniaToruDoOdcinka
        OdswiezLiczbePrzypisanychKostekTorow()
    End Sub

#End Region 'Pulpit

#Region "Reszta"

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

        cbo.SelectedItem = Nothing
    End Sub

    Private Function PobierzZaznaczonyElementNaLiscie(lv As ListView) As ListViewItem
        If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count = 0 Then
            Return Nothing
        Else
            Return lv.SelectedItems(0)
        End If
    End Function

    Private Function PobierzKrotkaLiczbeNieujemna(pole As TextBox) As UShort
        Dim liczba As UShort = 0
        UShort.TryParse(pole.Text, liczba)
        Return liczba
    End Function

    Private Function PobierzLiczbeNieujemnaRzeczywistaWZakresiePulpitu(pole As TextBox, zakresMax As Single) As Single
        Dim liczba As Single = 0.0F
        If Single.TryParse(pole.Text, liczba) Then
            If liczba < 0.0F Then liczba = 0.0F
            If liczba > zakresMax Then liczba = zakresMax
        End If

        Return liczba
    End Function

#End Region 'Reszta

End Class