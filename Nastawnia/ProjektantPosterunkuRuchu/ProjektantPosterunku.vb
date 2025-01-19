Public Class wndProjektantPosterunku
    Private ReadOnly NAZWA_OKNA As String
    Private Const ROZMIAR_KOSTKI_LISTA As Integer = 48
    Private Const ROZMIAR_CZCIONKI_MIN As Single = 0.05F
    Private Const ROZMIAR_CZCIONKI_MAX As Single = 0.5F
    Private Const PRZEJAZD_AUTOMATYCZNY As String = "A"
    Private Const PRZEJAZD_RECZNY As String = "R"
    Private Const BRAK As String = "(brak)"
    Private Const BRAK_ODCINKA As String = "(brak odcinka)"
    Private Const BRAK_NAZWY As String = "(brak nazwy)"
    Private Shared ReadOnly PUSTY_CBO_KOSTKA As New Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)(Nothing, "")
    Private Shared ReadOnly PUSTY_CBO_ODCINEK_TORU As New Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru)(Nothing, "")
    Private ReadOnly STAWNOSC_SBL As New Dictionary(Of Zaleznosci.StawnoscSBL, OpakowywaczEnum(Of Zaleznosci.StawnoscSBL))
    Private ReadOnly TYP_PRZYCISKU As New Dictionary(Of Zaleznosci.TypPrzyciskuEnum, OpakowywaczEnum(Of Zaleznosci.TypPrzyciskuEnum))
    Private ReadOnly TYP_PRZYCISKU_TOR As New Dictionary(Of Zaleznosci.TypPrzyciskuTorEnum, OpakowywaczEnum(Of Zaleznosci.TypPrzyciskuTorEnum))
    Private ReadOnly LISTA_TYP_PRZYCISKU As Object()
    Private ReadOnly LISTA_TYP_PRZYCISKU_TOR As Object()

    Private PaneleKonfKostek As Panel()
    Private ZnacznikPaneluWyswietlonego As New Object
    Private ZdarzeniaWlaczone As Boolean = True
    Private ZaznaczonaLampaNaLiscie As ListViewItem
    Private ZaznaczonyOdcinekNaLiscie As ListViewItem
    Private ZaznaczonyLicznikNaLiscie As ListViewItem
    Private ZaznaczonyPrzejazdNaLiscie As ListViewItem
    Private ZaznaczonyPrzejazdAutomatyzacjaNaLiscie As ListViewItem
    Private ZaznaczonyPrzejazdRogatkaNaLiscie As ListViewItem
    Private ZaznaczonyPrzejazdSygnDrogNaLiscie As ListViewItem

    Private Delegate Function SprawdzTypKostki(kostka As Zaleznosci.Kostka) As Boolean
    Private Delegate Function PobierzNazweKostki(kostka As Zaleznosci.Kostka) As String

#Region "Okno"

    Public Sub New()
        InitializeComponent()
        NAZWA_OKNA = Text

        LISTA_TYP_PRZYCISKU = DodajRodzajePrzyciskow()
        LISTA_TYP_PRZYCISKU_TOR = DodajRodzajePrzyciskowToru()
    End Sub

    Public Sub New(sciezka As String)
        Me.New()
        OtworzPlik(sciezka)
    End Sub

    Private Sub wndProjektantPosterunku_Load() Handles Me.Load
        plpPulpit.TypRysownika = WybranyTypRysownika
        plpPulpit.Wysrodkuj()

        PaneleKonfKostek = {pnlKonfPrzycisk, pnlKonfRozjazd, pnlKonfSygnOdcNast, pnlKonfPosiadaPrzycisk, pnlKonfSygnPolsamSwiatla, pnlKonfSygnPowtKolejnosc, pnlKonfTor, pnlKonfNapis,
            pnlKonfKier, pnlKonfAdres, pnlKonfNazwa, pnlKonfTorPodwojny, pnlKonfSygnPolsamUstawienia, pnlKonfSygnInfSwiatla, pnlKonfSygnInfSygnPowtarzany}
        For i As Integer = 0 To PaneleKonfKostek.Length - 1
            PaneleKonfKostek(i).Width = splKartaPulpit.Panel2.Width
        Next

        UtworzListeKostek()

        UstawAktywnoscPolLamp(False)
        UstawAktywnoscPolLicznikow(False)
        UstawAktywnoscPolOdcinkow(False)
        UstawAktywnoscPolPrzejazdu(False)
        UstawAktywnoscPolPrzejazdAutomatyzacja(False)
        UstawAktywnoscPolPrzejazdRogatka(False)
        UstawAktywnoscPolPrzejazdSygnDrog(False)

        PokazStawnoscSBL()

        pnlTorKolorTenOdcinek.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlTorKolorInnyOdcinek.BackColor = plpPulpit.Rysownik.KOLOR_TOR_PRZYPISANY
        pnlTorKolorNieprzypisany.BackColor = plpPulpit.Rysownik.KOLOR_TOR_NIEPRZYPISANY

        pnlLicznik1.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlLicznik2.BackColor = plpPulpit.Rysownik.KOLOR_TOR_LICZNIK_ODCINEK_2

        pnlLicznikOdcinek1.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlLicznikOdcinek2.BackColor = plpPulpit.Rysownik.KOLOR_TOR_LICZNIK_ODCINEK_2

        pnlPrzejazdKolorPrzypisany.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlPrzejazdKolorInny.BackColor = plpPulpit.Rysownik.KOLOR_TOR_PRZYPISANY
        pnlPrzejazdKolorNieprzypisany.BackColor = plpPulpit.Rysownik.KOLOR_TOR_NIEPRZYPISANY

        pnlPrzejazdAutomatyzacjaKolorWyjazd.BackColor = plpPulpit.Rysownik.KOLOR_TOR_TEN_ODCINEK
        pnlPrzejazdAutomatyzacjaKolorPrzyjazd.BackColor = plpPulpit.Rysownik.KOLOR_TOR_LICZNIK_ODCINEK_2
        pnlPrzejazdAutomatyzacjaKolorSygnalizator.BackColor = plpPulpit.Rysownik.KOLOR_TLO_SYGNALIZATOR_WYROZNIONY
    End Sub

    Private Sub wndProjektantPosterunku_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not PrzetworzPorzucaniePliku()
    End Sub

    Private Sub tabUstawienia_Selected() Handles tabUstawienia.Selected
        If tabUstawienia.SelectedTab Is tbpPulpit Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.Nic
            Dim zazn As Zaleznosci.Kostka = plpPulpit.ZaznaczonaKostka
            If zazn IsNot Nothing Then
                If TypeOf zazn Is Zaleznosci.SygnalizatorWylaczanyPoPrzejechaniu Then
                    PokazKonfSygnOdcinki()
                ElseIf TypeOf zazn Is Zaleznosci.Przycisk Then
                    cboKonfPrzyciskTyp_SelectedIndexChanged()
                End If
            End If

        ElseIf tabUstawienia.SelectedTab Is tbpOdcinki Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.OdcinkiTorow
            OdswiezListeOdcinkowTorow()

        ElseIf tabUstawienia.SelectedTab Is tbpLiczniki Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.Liczniki
            OdswiezListeLicznikow()
            OdswiezListeOdcinkowWLicznikach()

        ElseIf tabUstawienia.SelectedTab Is tbpLampy Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.Lampy

        ElseIf tabUstawienia.SelectedTab Is tbpPrzejazdy Then
            tabPrzejazd_Selected()
            OdswiezListePrzejazdow()
            OdswiezListePrzejazdAutomatyzacja()
            OdswiezListeOdcinkowPrzejazdAutomatyzacja()
            OdswiezListeSygnalizatorowPrzejazdAutomatyzacja()
        End If
    End Sub

    Private Function UtworzKostkeDoListy(pulpit As Pulpit.PulpitSterowniczy, kostka As Zaleznosci.Kostka, nazwa As String) As ListViewItem
        pulpit.Pulpit.Kostki(0, 0) = kostka
        Dim bm As New Bitmap(ROZMIAR_KOSTKI_LISTA, ROZMIAR_KOSTKI_LISTA)
        pulpit.DrawToBitmap(bm, New Rectangle(0, 0, ROZMIAR_KOSTKI_LISTA, ROZMIAR_KOSTKI_LISTA))
        imlKostki.Images.Add(bm)
        Return New ListViewItem(nazwa, imlKostki.Images.Count - 1) With {
            .Tag = kostka.GetType()
        }
    End Function

    Private Sub UtworzListeKostek()
        Dim p As New Pulpit.PulpitSterowniczy With {
            .Skalowanie = ROZMIAR_KOSTKI_LISTA - 1,
            .RysujKrawedzieKostek = False,
            .RysujWspolrzedne = False,
            .TypRysownika = Pulpit.TypRysownika.KlasycznyGDI,
            .TrybProjektowy = True}

        Dim sygnPolsam As New Zaleznosci.SygnalizatorPolsamoczynny With {.Nazwa = "A1/2m"}
        Dim sygnPolsamPowt As New Zaleznosci.SygnalizatorPolsamoczynny With {.Nazwa = "B"}
        Dim numerPoc As New Zaleznosci.NumerPociagu
        numerPoc.UstawNumery({101})

        Dim kostkiLista As New List(Of ListViewItem) From {
            UtworzKostkeDoListy(p, New Zaleznosci.Tor() With {.Nazwa = "Tor 1"}, "Tor"),
            UtworzKostkeDoListy(p, New Zaleznosci.TorKoniec(), "Koniec toru"),
            UtworzKostkeDoListy(p, New Zaleznosci.Zakret(), "Zakręt"),
            UtworzKostkeDoListy(p, New Zaleznosci.RozjazdLewo() With {.Nazwa = "101"}, "Rozjazd lewy"),
            UtworzKostkeDoListy(p, New Zaleznosci.RozjazdPrawo() With {.Nazwa = "103"}, "Rozjazd prawy"),
            UtworzKostkeDoListy(p, New Zaleznosci.SygnalizatorSamoczynny() With {.Nazwa = "105"}, "Sygnalizator samoczynny"),
            UtworzKostkeDoListy(p, New Zaleznosci.SygnalizatorManewrowy() With {.Nazwa = "Tm5"}, "Sygnalizator manewrowy"),
            UtworzKostkeDoListy(p, New Zaleznosci.SygnalizatorPowtarzajacy() With {.SygnalizatorPowtarzany = sygnPolsamPowt}, "Sygnalizator powtarzający"),
            UtworzKostkeDoListy(p, New Zaleznosci.SygnalizatorOstrzegawczy() With {.SygnalizatorPowtarzany = sygnPolsamPowt}, "Sygnalizator ostrzegawczy"),
            UtworzKostkeDoListy(p, sygnPolsam, "Sygnalizator półsamoczynny"),
            UtworzKostkeDoListy(p, New Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy() With {.Nazwa = "107"}, "Sygnalizator przejazdowy"),
            UtworzKostkeDoListy(p, New Zaleznosci.PrzejazdKolejowoDrogowyKostka(), "Przejazd kolejowy"),
            UtworzKostkeDoListy(p, New Zaleznosci.Przycisk() With {.SygnalizatorPolsamoczynny = sygnPolsam}, "Przycisk"),
            UtworzKostkeDoListy(p, New Zaleznosci.PrzyciskTor() With {.SygnalizatorPolsamoczynny = sygnPolsam}, "Przycisk z torem"),
            UtworzKostkeDoListy(p, New Zaleznosci.Kierunek() With {.Nazwa = "Tor 9"}, "Wjazd/wyjazd ze stacji"),
            UtworzKostkeDoListy(p, New Zaleznosci.Napis() With {.Tekst = "Magazyn"}, "Napis"),
            UtworzKostkeDoListy(p, New Zaleznosci.ZakretPodwojny(), "Zakręt podwójny"),
            UtworzKostkeDoListy(p, New Zaleznosci.Most(), "Most"),
            UtworzKostkeDoListy(p, numerPoc, "Numer pociągu")
        }

        lvPulpitKostki.Items.AddRange(kostkiLista.OrderBy(Function(k) k.Text).ToArray())
    End Sub

    Private Sub UstawTytulOkna()
        If plpPulpit.Pulpit.Nazwa = "" Then
            Text = NAZWA_OKNA
        Else
            Text = $"{NAZWA_OKNA} - {plpPulpit.Pulpit.Nazwa}"
        End If
    End Sub

    Private Sub CzyscDane(Optional nowyPulpit As Zaleznosci.Pulpit = Nothing)
        plpPulpit.Czysc(nowyPulpit)
        OdswiezListeLamp()
        tabUstawienia_Selected()
    End Sub

    Private Sub OtworzPlik(sciezka As String)
        Dim pulpitNowy As Zaleznosci.Pulpit = Zaleznosci.Pulpit.Otworz(sciezka)
        If pulpitNowy IsNot Nothing Then
            CzyscDane(pulpitNowy)
            UstawTytulOkna()
        Else
            Wspolne.PokazBlad($"Nie udało się otworzyć pliku {sciezka}.")
        End If
    End Sub

    Private Sub OdswiezPoZmianieRozmiaruPulpitu()
        plpPulpit.Invalidate()
        If tabUstawienia.SelectedTab Is tbpLiczniki Then OdswiezListeLicznikow()
        OdswiezListeLamp()
        If tabUstawienia.SelectedTab Is tbpPrzejazdy Then
            OdswiezListePrzejazdRogatki()
            OdswiezListePrzejazdSygnDrog()
        End If
    End Sub

    Private Function Zapisz(nowyPlik As Boolean) As Boolean
        Dim nowaSciezka As String = Nothing
        Dim wynik As Boolean

        If plpPulpit.Pulpit.SciezkaPliku = "" Or nowyPlik Then
            Dim dlg As New SaveFileDialog With {
                .Filter = Wspolne.FILTR_PLIKU_PULPITU
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
            Wspolne.PokazBlad("Nie udało się zapisać pliku.")
            Return False
        Else
            Wspolne.PokazKomunikat("Plik został zapisany.")
            Return True
        End If
    End Function

    ''' <summary>
    ''' Pyta użytkownika o zapisanie pliku, ewentualnie zapisuje i zwraca wartość określającą, czy można przejść do następnego kroku (np. wczytania pliku)
    ''' </summary>
    Private Function PrzetworzPorzucaniePliku() As Boolean
        Dim wynik As DialogResult = Wspolne.ZadajPytanieTrzyodpowiedziowe("Zapisać plik?")

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
                .Filter = Wspolne.FILTR_PLIKU_PULPITU
            }
            If dlg.ShowDialog = DialogResult.OK Then OtworzPlik(dlg.FileName)
        End If
    End Sub

    Private Sub mnuZapisz_Click() Handles mnuZapisz.Click
        Zapisz(False)
    End Sub

    Private Sub mnuZapiszJako_Click() Handles mnuZapiszJako.Click
        Zapisz(True)
    End Sub

    Private Sub mnuDodajKostki_Click() Handles mnuDodajKostki.Click
        Dim wnd As New wndEdytorRozmiaruPowierzchni(wndEdytorRozmiaruPowierzchni.TypEdycji.Dodaj, plpPulpit.Pulpit.Szerokosc, plpPulpit.Pulpit.Wysokosc)
        If wnd.ShowDialog = DialogResult.OK Then
            plpPulpit.Pulpit.PowiekszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek)
            OdswiezPoZmianieRozmiaruPulpitu()
        End If
    End Sub

    Private Sub mnuUsunKostki_Click() Handles mnuUsunKostki.Click
        Dim wnd As New wndEdytorRozmiaruPowierzchni(wndEdytorRozmiaruPowierzchni.TypEdycji.Usun, plpPulpit.Pulpit.Szerokosc, plpPulpit.Pulpit.Wysokosc)
        If wnd.ShowDialog = DialogResult.OK Then
            Try
                Dim wynik As List(Of Zaleznosci.ObiektBlokujacyZmniejszaniePulpitu) = plpPulpit.Pulpit.PomniejszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek)

                If wynik Is Nothing Then
                    OdswiezPoZmianieRozmiaruPulpitu()
                Else
                    Dim obiekty As New List(Of Wspolne.Komunikat)

                    For Each obj As Zaleznosci.ObiektBlokujacyZmniejszaniePulpitu In wynik
                        Select Case obj.Typ
                            Case Zaleznosci.RodzajObiektuBlokujacegoZmniejszaniePulpitu.Kostka
                                obiekty.Add(New Wspolne.Komunikat($"kostka o współrzędnych ({obj.Liczba1}, {obj.Liczba2})"))

                            Case Zaleznosci.RodzajObiektuBlokujacegoZmniejszaniePulpitu.LicznikOsi
                                obiekty.Add(New Wspolne.Komunikat($"para liczników osi o adresach {obj.Liczba1}, {obj.Liczba2}"))

                            Case Zaleznosci.RodzajObiektuBlokujacegoZmniejszaniePulpitu.Lampa
                                obiekty.Add(New Wspolne.Komunikat($"lampa o adresie {obj.Liczba1}"))

                            Case Zaleznosci.RodzajObiektuBlokujacegoZmniejszaniePulpitu.PrzejazdRogatka
                                obiekty.Add(New Wspolne.Komunikat($"rogatka o adresie {obj.Liczba2} z przejazdu numer {obj.Liczba1}"))

                            Case Zaleznosci.RodzajObiektuBlokujacegoZmniejszaniePulpitu.PrzejazdSygnalizatorDrogowy
                                obiekty.Add(New Wspolne.Komunikat($"sygnalizator drogowy o adresie {obj.Liczba2} z przejazdu numer {obj.Liczba1}"))

                            Case Else
                                obiekty.Add(New Wspolne.Komunikat("nieznany obiekt"))

                        End Select
                    Next

                    Dim wndKom As New Wspolne.wndKomunikatZLista($"W wybranym zakresie usuwania pulpit nie jest pusty.{vbCrLf}Znajdują się tam następujące rodzaje obiektów:", obiekty)
                    wndKom.ShowDialog()
                End If

            Catch ex As Exception
                Wspolne.PokazBlad("Wystąpił błąd podczas usuwania kostek:" & vbCrLf & ex.Message)
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

    Private Sub ctmSortuj_Click() Handles ctmSortuj.Click
        If tabUstawienia.SelectedTab Is tbpOdcinki Then
            plpPulpit.Pulpit.SortujOdcinkiNazwaRosnaco()
            OdswiezListeOdcinkowTorow()

        ElseIf tabUstawienia.SelectedTab Is tbpLiczniki Then
            plpPulpit.Pulpit.SortujLicznikiAdres1Rosnaco()
            OdswiezListeLicznikow()

        ElseIf tabUstawienia.SelectedTab Is tbpLampy Then
            plpPulpit.Pulpit.SortujLampyAdresRosnaco()
            OdswiezListeLamp()

        ElseIf tabUstawienia.SelectedTab Is tbpPrzejazdy Then
            plpPulpit.Pulpit.SortujPrzejazdyNazwaRosnaco()
            OdswiezListePrzejazdow()

        End If
    End Sub

    Private Sub ctmSortujPrzejazdy_Click() Handles ctmSortujPrzejazdy.Click
        If plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            Dim prz As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd

            If tabPrzejazd.SelectedTab Is tbpPrzejazdAutomatyzacja Then
                prz.SortujAutomatyzacjaWyjazdNazwaRosnaco()
                OdswiezListePrzejazdAutomatyzacja()

            ElseIf tabPrzejazd.SelectedTab Is tbpPrzejazdRogatki Then
                prz.SortujRogatkiAdresRosnaco()
                OdswiezListePrzejazdRogatki()

            ElseIf tabPrzejazd.SelectedTab Is tbpPrzejazdSygnDrog Then
                prz.SortujSygnalizatoryDrogoweAdresRosnaco()
                OdswiezListePrzejazdSygnDrog()

            End If
        End If
    End Sub

#End Region 'Menu

#Region "Zakładka Pulpit"

    Private Sub lvKostki_MouseDown(sender As Object, e As MouseEventArgs) Handles lvPulpitKostki.MouseDown
        Dim lvi As ListViewItem = lvPulpitKostki.GetItemAt(e.X, e.Y)
        If lvi IsNot Nothing Then
            lvi.Selected = True
            DoDragDrop(
                New Pulpit.PrzeciaganaKostka(
                    CType(Activator.CreateInstance(DirectCast(lvi.Tag, Type)), Zaleznosci.Kostka),
                    plpPulpit.Handle),
                DragDropEffects.Copy)
        End If
    End Sub


    'Adres
    Private Sub txtKonfAdres_TextChanged() Handles txtKonfAdres.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.IAdres).Adres = PobierzKrotkaLiczbeNieujemna(txtKonfAdres)
    End Sub

    'Nazwa
    Private Sub txtKonfNazwa_TextChanged() Handles txtKonfNazwa.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor).Nazwa = txtKonfNazwa.Text
        plpPulpit.Invalidate()
    End Sub

    'Tor
    Private Sub txtKonfTorPredkosc_TextChanged() Handles txtKonfTorPredkosc.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor).Predkosc = PobierzKrotkaLiczbeNieujemna(txtKonfTorPredkosc)
    End Sub

    Private Sub txtKonfTorDlugosc_TextChanged() Handles txtKonfTorDlugosc.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor).Dlugosc = PobierzLiczbeRzeczywistaNieujemna(txtKonfTorDlugosc)
    End Sub

    Private Sub cbKonfTorZelektryfikowany_CheckedChanged() Handles cbKonfTorZelektryfikowany.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor).Zelektryfikowany = cbKonfTorZelektryfikowany.Checked
        plpPulpit.Invalidate()
    End Sub

    Private Sub cbKonfTorNiezajetosc_CheckedChanged() Handles cbKonfTorNiezajetosc.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor).KontrolaNiezajetosci = cbKonfTorNiezajetosc.Checked
        plpPulpit.Invalidate()
    End Sub

    'Tor podwójny
    Private Sub txtKonfTorPodwPredk1_TextChanged() Handles txtKonfTorPodwPredk1.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).Predkosc = PobierzKrotkaLiczbeNieujemna(txtKonfTorPodwPredk1)
    End Sub

    Private Sub txtKonfTorPodwPredk2_TextChanged() Handles txtKonfTorPodwPredk2.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).PredkoscDrugi = PobierzKrotkaLiczbeNieujemna(txtKonfTorPodwPredk2)
    End Sub

    Private Sub txtKonfTorPodwDlugosc1_TextChanged() Handles txtKonfTorPodwDlugosc1.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).Dlugosc = PobierzLiczbeRzeczywistaNieujemna(txtKonfTorPodwDlugosc1)
    End Sub

    Private Sub txtKonfTorPodwDlugosc2_TextChanged() Handles txtKonfTorPodwDlugosc2.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).DlugoscDrugi = PobierzLiczbeRzeczywistaNieujemna(txtKonfTorPodwDlugosc2)
    End Sub

    Private Sub cbKonfTorPodwElektr1_CheckedChanged() Handles cbKonfTorPodwElektr1.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).Zelektryfikowany = cbKonfTorPodwElektr1.Checked
        plpPulpit.Invalidate()
    End Sub

    Private Sub cbKonfTorPodwElektr2_CheckedChanged() Handles cbKonfTorPodwElektr2.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).ZelektryfikowanyDrugi = cbKonfTorPodwElektr2.Checked
        plpPulpit.Invalidate()
    End Sub

    Private Sub cbKonfTorPodwNiezajetosc1_CheckedChanged() Handles cbKonfTorPodwNiezajetosc1.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).KontrolaNiezajetosci = cbKonfTorPodwNiezajetosc1.Checked
        plpPulpit.Invalidate()
    End Sub

    Private Sub cbKonfTorPodwNiezajetosc2_CheckedChanged() Handles cbKonfTorPodwNiezajetosc2.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny).KontrolaNiezajetosciDrugi = cbKonfTorPodwNiezajetosc2.Checked
        plpPulpit.Invalidate()
    End Sub

    'Rozjazd
    Private Sub rbKonfRozjazdZasadniczyPlus_CheckedChanged() Handles rbKonfRozjazdZasadniczyPlus.CheckedChanged
        If rbKonfRozjazdZasadniczyPlus.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).KierunekZasadniczy = Zaleznosci.UstawienieRozjazduEnum.Wprost
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub rbKonfRozjazdZasadniczyMinus_CheckedChanged() Handles rbKonfRozjazdZasadniczyMinus.CheckedChanged
        If rbKonfRozjazdZasadniczyMinus.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd).KierunekZasadniczy = Zaleznosci.UstawienieRozjazduEnum.Bok
        End If
        plpPulpit.Invalidate()
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
    Private Sub cboKonfSygnOdcinekNast_SelectedIndexChanged() Handles cboKonfSygnOdcinekNast.SelectedIndexChanged
        If cboKonfSygnOdcinekNast.SelectedItem Is Nothing Then Exit Sub
        Dim sygn As Zaleznosci.SygnalizatorWylaczanyPoPrzejechaniu = TryCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorWylaczanyPoPrzejechaniu)

        If sygn IsNot Nothing Then
            Dim el As Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboKonfSygnOdcinekNast.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru))
            sygn.OdcinekNastepujacy = el.Wartosc
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub cbKonfSygnPolsamManewry_CheckedChanged() Handles cbKonfSygnPolsamManewry.CheckedChanged
        UstawUstawienieSygnPolsam(cbKonfSygnPolsamManewry, Zaleznosci.UstawieniaSygnalizatoraPolsamoczynnego.DostepneManewry)
    End Sub

    Private Sub cbKonfSygnPolsamBrakDrogiHamowania_CheckedChanged() Handles cbKonfSygnPolsamBrakDrogiHamowania.CheckedChanged
        UstawUstawienieSygnPolsam(cbKonfSygnPolsamBrakDrogiHamowania, Zaleznosci.UstawieniaSygnalizatoraPolsamoczynnego.BrakDrogiHamowania)
    End Sub

    Private Sub cbKonfSygnPolsamZiel_CheckedChanged() Handles cbKonfSygnPolsamZiel.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamZiel, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.Zielone)
    End Sub

    Private Sub cbKonfSygnPolsamPomGor_CheckedChanged() Handles cbKonfSygnPolsamPomGor.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamPomGor, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.PomaranczoweGora)
    End Sub

    Private Sub cbKonfSygnPolsamCzer_CheckedChanged() Handles cbKonfSygnPolsamCzer.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamCzer, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.Czerwone)
    End Sub

    Private Sub cbKonfSygnPolsamPomDol_CheckedChanged() Handles cbKonfSygnPolsamPomDol.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamPomDol, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.PomaranczoweDol)
    End Sub

    Private Sub cbKonfSygnPolsamBiale_CheckedChanged() Handles cbKonfSygnPolsamBiale.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamBiale, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.Biale)
    End Sub

    Private Sub cbKonfSygnPolsamZielPas_CheckedChanged() Handles cbKonfSygnPolsamZielPas.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamZielPas, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.ZielonyPas)
    End Sub

    Private Sub cbKonfSygnPolsamPomPas_CheckedChanged() Handles cbKonfSygnPolsamPomPas.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamPomPas, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.PomaranczowyPas)
    End Sub

    Private Sub cbKonfSygnPolsamKierPrzeciwny_CheckedChanged() Handles cbKonfSygnPolsamKierPrzeciwny.CheckedChanged
        UstawDostepneSwiatloSygnPolsam(cbKonfSygnPolsamKierPrzeciwny, Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.WskaznikKierunkuPrzeciwnego)
    End Sub


    'Sygnalizator informujący
    Private Sub rbKonfSygnPowtKolejnoscI_CheckedChanged() Handles rbKonfSygnPowtKolejnoscI.CheckedChanged
        UstawKolejnoscSygnalizatoraPowtarzajacego(rbKonfSygnPowtKolejnoscI, Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Pierwszy)
    End Sub

    Private Sub rbKonfSygnPowtKolejnoscII_CheckedChanged() Handles rbKonfSygnPowtKolejnoscII.CheckedChanged
        UstawKolejnoscSygnalizatoraPowtarzajacego(rbKonfSygnPowtKolejnoscII, Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Drugi)
    End Sub

    Private Sub rbKonfSygnPowtKolejnoscIII_CheckedChanged() Handles rbKonfSygnPowtKolejnoscIII.CheckedChanged
        UstawKolejnoscSygnalizatoraPowtarzajacego(rbKonfSygnPowtKolejnoscIII, Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Trzeci)
    End Sub

    Private Sub cboKonfSygnInfSygnPowtarzany_SelectedIndexChanged() Handles cboKonfSygnInfSygnPowtarzany.SelectedIndexChanged
        If cboKonfSygnInfSygnPowtarzany.SelectedItem Is Nothing Then Exit Sub
        Dim el As Wspolne.ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cboKonfSygnInfSygnPowtarzany.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka))
        Dim sygnInf As Zaleznosci.SygnalizatorInformujacy = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorInformujacy)
        sygnInf.SygnalizatorPowtarzany = DirectCast(el.Wartosc, Zaleznosci.SygnalizatorPolsamoczynny)
        txtKonfNazwa.Text = sygnInf.Nazwa
        plpPulpit.Invalidate()
    End Sub

    Private Sub cbKonfSygnInfZiel_CheckedChanged() Handles cbKonfSygnInfZiel.CheckedChanged
        UstawDostepneSwiatloSygnInf(cbKonfSygnInfZiel, Zaleznosci.DostepneSwiatlaSygnInformujacy.Zielone)
    End Sub

    Private Sub cbKonfSygnInfPom_CheckedChanged() Handles cbKonfSygnInfPom.CheckedChanged
        UstawDostepneSwiatloSygnInf(cbKonfSygnInfPom, Zaleznosci.DostepneSwiatlaSygnInformujacy.Pomaranczowe)
    End Sub


    'Posiadanie przycisku
    Private Sub cbKonfPrzycisk_CheckedChanged() Handles cbKonfPrzycisk.CheckedChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.IPrzycisk).PosiadaPrzycisk = cbKonfPrzycisk.Checked
        plpPulpit.Invalidate()
    End Sub


    'Przycisk
    Private Sub cboKonfPrzyciskTyp_SelectedIndexChanged() Handles cboKonfPrzyciskTyp.SelectedIndexChanged
        Dim el As Object = cboKonfPrzyciskTyp.SelectedItem

        If el IsNot Nothing Then
            cboKonfPrzyciskObiekt.Items.Clear()

            Select Case plpPulpit.ZaznaczonaKostka.Typ
                Case Zaleznosci.TypKostki.Przycisk
                    Dim typ As Zaleznosci.TypPrzyciskuEnum = DirectCast(el, Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of Zaleznosci.TypPrzyciskuEnum))).Wartosc.Element
                    Dim prz As Zaleznosci.Przycisk = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Przycisk)
                    prz.TypPrzycisku = typ

                    Select Case typ
                        Case Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegu, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweToru))
                            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskObiekt, prz.SygnalizatorPolsamoczynny)

                        Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnManewrowego
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzySygnalizatorManewrowy, AddressOf PobierzNazweToru))
                            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskObiekt, prz.SygnalizatorManewrowy)

                        Case Zaleznosci.TypPrzyciskuEnum.WlaczenieSBL, Zaleznosci.TypPrzyciskuEnum.PotwierdzenieSBL, Zaleznosci.TypPrzyciskuEnum.ZwolnienieSBL
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzyKierunek, AddressOf PobierzNazweToru))
                            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskObiekt, prz.Kierunek)

                        Case Zaleznosci.TypPrzyciskuEnum.KasowanieRozprucia
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzyRozjazd, AddressOf PobierzNazweToru))
                            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskObiekt, prz.Rozjazd)

                        Case Zaleznosci.TypPrzyciskuEnum.ZamknieciePrzejazdu, Zaleznosci.TypPrzyciskuEnum.OtwarciePrzejazdu
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzPrzejazdyDoComboBox())
                            ZaznaczElement(cboKonfPrzyciskObiekt, prz.Przejazd)

                    End Select

                Case Zaleznosci.TypKostki.PrzyciskTor
                    Dim typ As Zaleznosci.TypPrzyciskuTorEnum = DirectCast(el, Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of Zaleznosci.TypPrzyciskuTorEnum))).Wartosc.Element
                    Dim prz As Zaleznosci.PrzyciskTor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                    prz.TypPrzycisku = typ

                    Select Case typ
                        Case Zaleznosci.TypPrzyciskuTorEnum.JazdaSygnalizatorPolsamoczynny, Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorPolsamoczynny
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweToru))
                            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskObiekt, prz.SygnalizatorPolsamoczynny)

                        Case Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorManewrowy
                            cboKonfPrzyciskObiekt.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzySygnalizatorManewrowy, AddressOf PobierzNazweToru))
                            ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfPrzyciskObiekt, prz.SygnalizatorManewrowy)

                    End Select

            End Select
        End If

        plpPulpit.Invalidate()
    End Sub

    Private Sub cboKonfPrzyciskObiekt_SelectedIndexChanged() Handles cboKonfPrzyciskObiekt.SelectedIndexChanged
        If cboKonfPrzyciskObiekt.SelectedItem Is Nothing Then Exit Sub

        Dim zazn As Object = cboKonfPrzyciskObiekt.SelectedItem
        Select Case plpPulpit.ZaznaczonaKostka.Typ

            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Przycisk)

                Select Case prz.TypPrzycisku
                    Case Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegu, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego
                        prz.SygnalizatorPolsamoczynny = CType(CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc, Zaleznosci.SygnalizatorPolsamoczynny)

                    Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnManewrowego
                        prz.SygnalizatorManewrowy = CType(CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc, Zaleznosci.SygnalizatorManewrowy)

                    Case Zaleznosci.TypPrzyciskuEnum.WlaczenieSBL, Zaleznosci.TypPrzyciskuEnum.PotwierdzenieSBL, Zaleznosci.TypPrzyciskuEnum.ZwolnienieSBL
                        prz.Kierunek = CType(CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc, Zaleznosci.Kierunek)

                    Case Zaleznosci.TypPrzyciskuEnum.KasowanieRozprucia
                        prz.Rozjazd = CType(CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc, Zaleznosci.Rozjazd)

                    Case Zaleznosci.TypPrzyciskuEnum.ZamknieciePrzejazdu, Zaleznosci.TypPrzyciskuEnum.OtwarciePrzejazdu
                        prz.Przejazd = CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.PrzejazdKolejowoDrogowy)).Wartosc

                End Select

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)

                Select Case prz.TypPrzycisku
                    Case Zaleznosci.TypPrzyciskuTorEnum.JazdaSygnalizatorPolsamoczynny, Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorPolsamoczynny
                        prz.SygnalizatorPolsamoczynny = CType(CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc, Zaleznosci.SygnalizatorPolsamoczynny)

                    Case Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorManewrowy
                        prz.SygnalizatorManewrowy = CType(CType(zazn, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc, Zaleznosci.SygnalizatorManewrowy)

                End Select

        End Select

        plpPulpit.Invalidate()
    End Sub


    'Napis
    Private Sub txtKonfNapisTekst_TextChanged() Handles txtKonfNapisTekst.TextChanged
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Napis).Tekst = txtKonfNapisTekst.Text
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtKonfNapisRozmiar_TextChanged() Handles txtKonfNapisRozmiar.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Napis).Rozmiar = PobierzLiczbeRzeczywistaWZakresie(txtKonfNapisRozmiar, ROZMIAR_CZCIONKI_MIN, ROZMIAR_CZCIONKI_MAX)
        plpPulpit.Invalidate()
    End Sub


    'Kierunek
    Private Sub rbKonfKierWyjazdLewo_CheckedChanged() Handles rbKonfKierWyjazdLewo.CheckedChanged
        If rbKonfKierWyjazdLewo.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek).KierunekWyjazdu = Zaleznosci.KierunekWyjazduSBL.Lewo
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub rbKonfKierWyjazdPrawo_CheckedChanged() Handles rbKonfKierWyjazdPrawo.CheckedChanged
        If rbKonfKierWyjazdPrawo.Checked Then
            DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek).KierunekWyjazdu = Zaleznosci.KierunekWyjazduSBL.Prawo
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub cboKonfKierStawnosc_SelectedIndexChanged() Handles cboKonfKierStawnosc.SelectedIndexChanged
        If cboKonfKierStawnosc.SelectedItem Is Nothing Then Exit Sub
        DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek).Stawnosc =
            DirectCast(cboKonfKierStawnosc.SelectedItem, Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of Zaleznosci.StawnoscSBL))).Wartosc.Element
    End Sub


    'Wyświetlanie paneli
    Private Sub PokazPanelKonf()
        Dim nowePanele As List(Of Panel) = Nothing

        If plpPulpit.ZaznaczonaKostka IsNot Nothing Then
            ZdarzeniaWlaczone = False

            Select Case plpPulpit.ZaznaczonaKostka.Typ
                Case Zaleznosci.TypKostki.Tor, Zaleznosci.TypKostki.Zakret, Zaleznosci.TypKostki.NumerPociagu
                    nowePanele = PokazKonfTor()
                Case Zaleznosci.TypKostki.ZakretPodwojny, Zaleznosci.TypKostki.Most
                    nowePanele = PokazKonfTorPodwojny()
                Case Zaleznosci.TypKostki.PrzejazdKolejowy
                    nowePanele = PokazKonfPrzejazd()
                Case Zaleznosci.TypKostki.RozjazdLewo, Zaleznosci.TypKostki.RozjazdPrawo
                    nowePanele = PokazKonfRozjazd()
                Case Zaleznosci.TypKostki.SygnalizatorManewrowy, Zaleznosci.TypKostki.SygnalizatorSamoczynny, Zaleznosci.TypKostki.SygnalizatorPolsamoczynny, Zaleznosci.TypKostki.SygnalizatorOstrzegawczyPrzejazdowy
                    nowePanele = PokazKonfSygn()
                Case Zaleznosci.TypKostki.SygnalizatorPowtarzajacy, Zaleznosci.TypKostki.SygnalizatorOstrzegawczy
                    nowePanele = PokazKonfSygnInf()
                Case Zaleznosci.TypKostki.Przycisk, Zaleznosci.TypKostki.PrzyciskTor
                    nowePanele = PokazKonfPrzycisk()
                Case Zaleznosci.TypKostki.Napis
                    nowePanele = PokazKonfNapis()
                Case Zaleznosci.TypKostki.Kierunek
                    nowePanele = PokazKonfKier()
            End Select

            ZdarzeniaWlaczone = True
        End If

        If nowePanele IsNot Nothing Then
            Dim y As Integer = 0

            For Each p As Panel In nowePanele
                p.Location = New Point(0, y)
                p.Visible = True
                p.Tag = ZnacznikPaneluWyswietlonego
                y += p.Size.Height
            Next
        End If

        For Each p As Panel In PaneleKonfKostek
            If p.Visible And p.Tag Is Nothing Then p.Visible = False
            p.Tag = Nothing
        Next
    End Sub

    Private Sub PokazKonfAdres()
        Dim adr As Zaleznosci.IAdres = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.IAdres)
        txtKonfAdres.Text = adr.Adres.ToString
    End Sub

    Private Sub PokazKonfNazwa(Optional wlaczony As Boolean = True)
        Dim tor As Zaleznosci.Tor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor)
        txtKonfNazwa.Text = tor.Nazwa
        txtKonfNazwa.Enabled = wlaczony
    End Sub

    Private Sub PokazKonfTorPodwojny(tor As Zaleznosci.TorPodwojny)
        txtKonfTorPodwPredk1.Text = tor.Predkosc.ToString
        txtKonfTorPodwPredk2.Text = tor.PredkoscDrugi.ToString
        txtKonfTorPodwDlugosc1.Text = tor.Dlugosc.ToString
        txtKonfTorPodwDlugosc2.Text = tor.DlugoscDrugi.ToString
        cbKonfTorPodwElektr1.Checked = tor.Zelektryfikowany
        cbKonfTorPodwElektr2.Checked = tor.ZelektryfikowanyDrugi
        cbKonfTorPodwNiezajetosc1.Checked = tor.KontrolaNiezajetosci
        cbKonfTorPodwNiezajetosc2.Checked = tor.KontrolaNiezajetosciDrugi
    End Sub

    Private Sub PokazKonfTor(tor As Zaleznosci.Tor)
        txtKonfTorPredkosc.Text = tor.Predkosc.ToString()
        txtKonfTorDlugosc.Text = tor.Dlugosc.ToString()
        cbKonfTorZelektryfikowany.Checked = tor.Zelektryfikowany
        cbKonfTorNiezajetosc.Checked = tor.KontrolaNiezajetosci
    End Sub

    Private Function PokazKonfTor() As List(Of Panel)
        Dim tor As Zaleznosci.Tor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Tor)
        PokazKonfNazwa()
        PokazKonfTor(tor)
        Return New List(Of Panel) From {pnlKonfNazwa, pnlKonfTor}
    End Function

    Private Function PokazKonfTorPodwojny() As List(Of Panel)
        Dim podw As Zaleznosci.TorPodwojny = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.TorPodwojny)
        PokazKonfTorPodwojny(podw)
        Return New List(Of Panel) From {pnlKonfTorPodwojny}
    End Function

    Private Function PokazKonfPrzejazd() As List(Of Panel)
        Dim prz As Zaleznosci.PrzejazdKolejowoDrogowyKostka = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzejazdKolejowoDrogowyKostka)
        PokazKonfTor(prz)
        Return New List(Of Panel) From {pnlKonfTor}
    End Function

    Private Function PokazKonfRozjazd() As List(Of Panel)
        Dim roz As Zaleznosci.Rozjazd = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Rozjazd)
        PokazKonfAdres()
        PokazKonfNazwa()
        PokazKonfTorPodwojny(roz)
        PokazKonfPosiadaPrzycisk(roz)

        If roz.KierunekZasadniczy = Zaleznosci.UstawienieRozjazduEnum.Wprost Then
            rbKonfRozjazdZasadniczyPlus.Checked = True
        Else
            rbKonfRozjazdZasadniczyMinus.Checked = True
        End If

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

        Return New List(Of Panel) From {pnlKonfAdres, pnlKonfNazwa, pnlKonfTorPodwojny, pnlKonfPosiadaPrzycisk, pnlKonfRozjazd}
    End Function

    Private Sub PokazKonfPosiadaPrzycisk(przycisk As Zaleznosci.IPrzycisk)
        cbKonfPrzycisk.Checked = przycisk.PosiadaPrzycisk
    End Sub

    Private Sub PokazKonfSygnOdcinki()
        cboKonfSygnOdcinekNast.Items.Clear()

        Dim sygn As Zaleznosci.SygnalizatorWylaczanyPoPrzejechaniu = TryCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorWylaczanyPoPrzejechaniu)
        If sygn Is Nothing Then Exit Sub

        Dim odcinki As IEnumerable(Of Zaleznosci.OdcinekToru) = plpPulpit.Pulpit.OdcinkiTorow.OrderBy(Function(t) t.Nazwa)
        For Each odc As Zaleznosci.OdcinekToru In odcinki
            cboKonfSygnOdcinekNast.Items.Add(New Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru)(odc, odc.Nazwa))
        Next

        ZaznaczElement(cboKonfSygnOdcinekNast, sygn.OdcinekNastepujacy)
    End Sub

    Private Function PokazKonfSygn() As List(Of Panel)
        Dim sygn As Zaleznosci.Sygnalizator = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Sygnalizator)
        Dim panele As New List(Of Panel) From {pnlKonfAdres, pnlKonfNazwa, pnlKonfTor}

        PokazKonfAdres()
        PokazKonfNazwa()
        PokazKonfTor(sygn)

        If sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorOstrzegawczyPrzejazdowy Then
            panele.Add(pnlKonfSygnOdcNast)
            PokazKonfSygnOdcinki()
        End If

        If sygn.Typ = Zaleznosci.TypKostki.SygnalizatorManewrowy Then
            panele.Add(pnlKonfPosiadaPrzycisk)
            Dim tm As Zaleznosci.SygnalizatorManewrowy = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorManewrowy)
            PokazKonfPosiadaPrzycisk(tm)
        End If

        If sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Then
            Dim sygnPolsam As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny)

            panele.Add(pnlKonfSygnPolsamUstawienia)
            Dim ust As Zaleznosci.UstawieniaSygnalizatoraPolsamoczynnego = sygnPolsam.Ustawienia
            cbKonfSygnPolsamManewry.Checked = (ust And Zaleznosci.UstawieniaSygnalizatoraPolsamoczynnego.DostepneManewry) <> 0
            cbKonfSygnPolsamBrakDrogiHamowania.Checked = (ust And Zaleznosci.UstawieniaSygnalizatoraPolsamoczynnego.BrakDrogiHamowania) <> 0

            panele.Add(pnlKonfSygnPolsamSwiatla)
            Dim sw As Zaleznosci.DostepneSwiatlaSygnPolsamoczynny = sygnPolsam.DostepneSwiatla
            cbKonfSygnPolsamZiel.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.Zielone) <> 0
            cbKonfSygnPolsamPomGor.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.PomaranczoweGora) <> 0
            cbKonfSygnPolsamCzer.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.Czerwone) <> 0
            cbKonfSygnPolsamPomDol.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.PomaranczoweDol) <> 0
            cbKonfSygnPolsamBiale.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.Biale) <> 0
            cbKonfSygnPolsamZielPas.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.ZielonyPas) <> 0
            cbKonfSygnPolsamPomPas.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.PomaranczowyPas) <> 0
            cbKonfSygnPolsamKierPrzeciwny.Checked = (sw And Zaleznosci.DostepneSwiatlaSygnPolsamoczynny.WskaznikKierunkuPrzeciwnego) <> 0
        End If

        Return panele
    End Function

    Private Function PokazKonfSygnInf() As List(Of Panel)
        Dim sygn As Zaleznosci.SygnalizatorInformujacy = CType(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorInformujacy)
        Dim sygnPowt As Zaleznosci.SygnalizatorPowtarzajacy = TryCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy)
        Dim panele As New List(Of Panel) From {pnlKonfAdres, pnlKonfNazwa, pnlKonfTor}

        PokazKonfAdres()
        PokazKonfNazwa(False)
        PokazKonfTor(sygn)

        If sygnPowt IsNot Nothing Then
            Select Case sygnPowt.Kolejnosc
                Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Pierwszy
                    rbKonfSygnPowtKolejnoscI.Checked = True
                Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Drugi
                    rbKonfSygnPowtKolejnoscII.Checked = True
                Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Trzeci
                    rbKonfSygnPowtKolejnoscIII.Checked = True
            End Select

            panele.Add(pnlKonfSygnPowtKolejnosc)
        End If

        Dim sygnalizatory As Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweToru)
        cboKonfSygnInfSygnPowtarzany.Items.Clear()
        cboKonfSygnInfSygnPowtarzany.Items.AddRange(sygnalizatory)
        ZaznaczElement(Of Zaleznosci.Kostka)(cboKonfSygnInfSygnPowtarzany, sygn.SygnalizatorPowtarzany)
        panele.Add(pnlKonfSygnInfSygnPowtarzany)

        cbKonfSygnInfZiel.Checked = (sygn.DostepneSwiatla And Zaleznosci.DostepneSwiatlaSygnInformujacy.Zielone) <> 0
        cbKonfSygnInfPom.Checked = (sygn.DostepneSwiatla And Zaleznosci.DostepneSwiatlaSygnInformujacy.Pomaranczowe) <> 0
        panele.Add(pnlKonfSygnInfSwiatla)

        Return panele
    End Function

    Private Function PokazKonfPrzycisk() As List(Of Panel)
        cboKonfPrzyciskTyp.Items.Clear()
        Dim panele As New List(Of Panel)

        Select Case plpPulpit.ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Przycisk)
                cboKonfPrzyciskTyp.Items.AddRange(LISTA_TYP_PRZYCISKU)
                ZaznaczElement(cboKonfPrzyciskTyp, TYP_PRZYCISKU(prz.TypPrzycisku))

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                cboKonfPrzyciskTyp.Items.AddRange(LISTA_TYP_PRZYCISKU_TOR)
                ZaznaczElement(cboKonfPrzyciskTyp, TYP_PRZYCISKU_TOR(prz.TypPrzycisku))

                PokazKonfTor(prz)
                panele.Add(pnlKonfTor)
        End Select

        panele.Add(pnlKonfPrzycisk)
        Return panele
    End Function

    Private Function PokazKonfNapis() As List(Of Panel)
        Dim napis As Zaleznosci.Napis = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Napis)

        txtKonfNapisTekst.Text = napis.Tekst
        txtKonfNapisRozmiar.Text = napis.Rozmiar.ToString
        Return New List(Of Panel) From {pnlKonfNapis}
    End Function

    Private Function PokazKonfKier() As List(Of Panel)
        Dim kierunek As Zaleznosci.Kierunek = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.Kierunek)

        PokazKonfNazwa()
        PokazKonfTor(kierunek)

        If kierunek.KierunekWyjazdu = Zaleznosci.KierunekWyjazduSBL.Lewo Then
            rbKonfKierWyjazdLewo.Checked = True
        Else
            rbKonfKierWyjazdPrawo.Checked = True
        End If
        ZaznaczElement(cboKonfKierStawnosc, STAWNOSC_SBL(kierunek.Stawnosc))

        Return New List(Of Panel) From {pnlKonfNazwa, pnlKonfTor, pnlKonfKier}
    End Function

    'Inne
    Private Sub PokazStawnoscSBL()
        cboKonfKierStawnosc.Items.Clear()
        DodajRodzajStawnosciSBL(Zaleznosci.StawnoscSBL.Dwustawna, "Dwustawna")
        DodajRodzajStawnosciSBL(Zaleznosci.StawnoscSBL.Trzystawna, "Trzystawna")
        DodajRodzajStawnosciSBL(Zaleznosci.StawnoscSBL.Czterostawna, "Czterostawna")
    End Sub

    Private Sub DodajRodzajStawnosciSBL(stawnosc As Zaleznosci.StawnoscSBL, opis As String)
        cboKonfKierStawnosc.Items.Add(New Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of Zaleznosci.StawnoscSBL))(DodajElementOpakowywanyDoSlownika(STAWNOSC_SBL, stawnosc), opis))
    End Sub

    Private Function DodajRodzajePrzyciskow() As Object()
        Dim lista As New List(Of Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of Zaleznosci.TypPrzyciskuEnum)))

        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy, "Sygnał zastępczy")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegu, "Zwolnienie przebiegu pociągowego na sygnalizatorze półsamoczynnym")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego, "Zwolnienie przebiegu manewrowego na sygnalizatorze półsamoczynnym")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnManewrowego, "Zwolnienie przebiegu manewrowego na sygnalizatorze manewrowym")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.WlaczenieSBL, "Włączenie blokady")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.PotwierdzenieSBL, "Potwierdzenie kierunku blokady")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.ZwolnienieSBL, "Zwolnienie blokady")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.KasowanieRozprucia, "Kasowanie rozprucia")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.ZamknieciePrzejazdu, "Zamknięcie przejazdu kolejowo-drogowego")
        DodajElementOpakowywany(TYP_PRZYCISKU, lista, Zaleznosci.TypPrzyciskuEnum.OtwarciePrzejazdu, "Otwarcie przejazdu kolejowo-drogowego")

        Return lista.OrderBy(Function(t) t.Tekst).ToArray
    End Function

    Private Function DodajRodzajePrzyciskowToru() As Object()
        Dim lista As New List(Of Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of Zaleznosci.TypPrzyciskuTorEnum)))

        DodajElementOpakowywany(TYP_PRZYCISKU_TOR, lista, Zaleznosci.TypPrzyciskuTorEnum.JazdaSygnalizatorPolsamoczynny, "Wyjazd na sygnalizatorze półsamoczynnym")
        DodajElementOpakowywany(TYP_PRZYCISKU_TOR, lista, Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorPolsamoczynny, "Sygnał manewrowy na sygnalizatorze półsamoczynnym")
        DodajElementOpakowywany(TYP_PRZYCISKU_TOR, lista, Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorManewrowy, "Sygnał manewrowy na sygnalizatorze manewrowym")

        Return lista.OrderBy(Function(t) t.Tekst).ToArray
    End Function

    Private Sub DodajElementOpakowywany(Of T)(slownik As Dictionary(Of T, OpakowywaczEnum(Of T)), lista As List(Of Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of T))), element As T, nazwa As String)
        lista.Add(New Wspolne.ObiektComboBox(Of OpakowywaczEnum(Of T))(DodajElementOpakowywanyDoSlownika(slownik, element), nazwa))
    End Sub

    Private Function DodajElementOpakowywanyDoSlownika(Of T)(slownik As Dictionary(Of T, OpakowywaczEnum(Of T)), element As T) As OpakowywaczEnum(Of T)
        Dim el As New OpakowywaczEnum(Of T)(element)
        slownik.Add(element, el)
        Return el
    End Function

    Private Function PrzetworzZaznaczenieRozjazduZaleznego(cbo As ComboBox, rbPlus As RadioButton, rbMinus As RadioButton, ByRef rozjazd As Zaleznosci.Rozjazd) As Boolean
        If cbo.SelectedItem Is Nothing Then Return False

        Dim el As Wspolne.ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cbo.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka))
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
        Dim el As Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzyRozjazd, AddressOf PobierzNazweToru, rozjazdUkryty)

        cbo.Items.Clear()
        cbo.Items.Add(PUSTY_CBO_KOSTKA)
        cbo.Items.AddRange(el)

        ZaznaczElement(Of Zaleznosci.Kostka)(cbo, rozjazdWybrany)
    End Sub

    Private Sub UstawUstawienieSygnPolsam(cb As CheckBox, ustawienie As Zaleznosci.UstawieniaSygnalizatoraPolsamoczynnego)
        Dim sygn As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny)
        If cb.Checked Then
            sygn.Ustawienia = sygn.Ustawienia Or ustawienie
        Else
            sygn.Ustawienia = sygn.Ustawienia And (Not ustawienie)
        End If
    End Sub

    Private Sub UstawDostepneSwiatloSygnPolsam(cb As CheckBox, kolor As Zaleznosci.DostepneSwiatlaSygnPolsamoczynny)
        Dim sygn As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny)
        If cb.Checked Then
            sygn.DostepneSwiatla = sygn.DostepneSwiatla Or kolor
        Else
            sygn.DostepneSwiatla = sygn.DostepneSwiatla And (Not kolor)
        End If
    End Sub

    Private Sub UstawKolejnoscSygnalizatoraPowtarzajacego(rb As RadioButton, kolejnosc As Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego)
        If rb.Checked Then
            Dim sygnPowt As Zaleznosci.SygnalizatorPowtarzajacy = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorPowtarzajacy)
            sygnPowt.Kolejnosc = kolejnosc
            txtKonfNazwa.Text = sygnPowt.Nazwa
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub UstawDostepneSwiatloSygnInf(cb As CheckBox, kolor As Zaleznosci.DostepneSwiatlaSygnInformujacy)
        Dim sygn As Zaleznosci.SygnalizatorInformujacy = DirectCast(plpPulpit.ZaznaczonaKostka, Zaleznosci.SygnalizatorInformujacy)
        If cb.Checked Then
            sygn.DostepneSwiatla = sygn.DostepneSwiatla Or kolor
        Else
            sygn.DostepneSwiatla = sygn.DostepneSwiatla And (Not kolor)
        End If
    End Sub

    Private Function PobierzPrzejazdyDoComboBox() As Wspolne.ObiektComboBox(Of Zaleznosci.PrzejazdKolejowoDrogowy)()
        Dim przejazdy As New List(Of Wspolne.ObiektComboBox(Of Zaleznosci.PrzejazdKolejowoDrogowy))

        For Each p As Zaleznosci.PrzejazdKolejowoDrogowy In plpPulpit.Pulpit.Przejazdy
            przejazdy.Add(New Wspolne.ObiektComboBox(Of Zaleznosci.PrzejazdKolejowoDrogowy)(p, p.Nazwa))
        Next

        Return przejazdy.OrderBy(Function(p) p.Wartosc.Nazwa).ToArray
    End Function

#End Region 'Zakładka Pulpit

#Region "Zakładka Odcinki torów"

    Private Sub lvOdcinki_SelectedIndexChanged() Handles lvOdcinki.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyOdcinekNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvOdcinki)
        Dim odcinek As Zaleznosci.OdcinekToru = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.OdcinekToru)(ZaznaczonyOdcinekNaLiscie)
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
        plpPulpit.ZaznaczonyOdcinek = odcinek
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnOdcinekDodaj_Click() Handles btnOdcinekDodaj.Click
        plpPulpit.Pulpit.OdcinkiTorow.Add(New Zaleznosci.OdcinekToru)
        OdswiezListeOdcinkowTorow()
    End Sub

    Private Sub btnOdcinekUsun_Click() Handles btnOdcinekUsun.Click
        Dim odcinek As Zaleznosci.OdcinekToru = plpPulpit.ZaznaczonyOdcinek
        If odcinek Is Nothing Then Exit Sub

        Dim pyt As String = "Czy usunąć odcinek torów"
        If odcinek.Nazwa <> "" Then pyt &= " o nazwie " & odcinek.Nazwa

        If Wspolne.ZadajPytanie(pyt & "?") = DialogResult.Yes Then
            plpPulpit.Pulpit.UsunOdcinekToru(odcinek)
            OdswiezListeOdcinkowTorow()
        End If
    End Sub

    Private Sub txtOdcinekAdres_TextChanged() Handles txtOdcinekAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim odc As Zaleznosci.OdcinekToru = plpPulpit.ZaznaczonyOdcinek
        If odc IsNot Nothing Then
            odc.Adres = PobierzKrotkaLiczbeNieujemna(txtOdcinekAdres)
            ZaznaczonyOdcinekNaLiscie.SubItems(0).Text = odc.Adres.ToString
        End If
    End Sub

    Private Sub txtOdcinekNazwa_TextChanged() Handles txtOdcinekNazwa.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim odc As Zaleznosci.OdcinekToru = plpPulpit.ZaznaczonyOdcinek
        If odc IsNot Nothing Then
            odc.Nazwa = txtOdcinekNazwa.Text
            ZaznaczonyOdcinekNaLiscie.SubItems(1).Text = odc.Nazwa
        End If
    End Sub

    Private Sub txtOdcinekOpis_TextChanged() Handles txtOdcinekOpis.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim odc As Zaleznosci.OdcinekToru = plpPulpit.ZaznaczonyOdcinek
        If odc IsNot Nothing Then
            odc.Opis = txtOdcinekOpis.Text
        End If
    End Sub

    Private Sub OdswiezListeOdcinkowTorow()
        Dim odcinek As Zaleznosci.OdcinekToru = plpPulpit.ZaznaczonyOdcinek
        lvOdcinki.Items.Clear()
        ZaznaczonyOdcinekNaLiscie = Nothing

        For Each o As Zaleznosci.OdcinekToru In plpPulpit.Pulpit.OdcinkiTorow
            Dim lvi As New ListViewItem(New String() {o.Adres.ToString, o.Nazwa.ToString, o.LiczbaTorow.ToString()}) With {
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
        Dim lvi As ListViewItem

        For i As Integer = 0 To lvOdcinki.Items.Count - 1
            lvi = lvOdcinki.Items(i)
            Dim o As Zaleznosci.OdcinekToru = DirectCast(lvi.Tag, Zaleznosci.OdcinekToru)
            lvi.SubItems(2).Text = o.LiczbaTorow.ToString()
        Next
    End Sub

#End Region 'Zakładka Odcinki torów

#Region "Zakładka Liczniki osi"

    Private Sub lvLiczniki_SelectedIndexChanged() Handles lvLiczniki.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyLicznikNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvLiczniki)
        Dim licznik As Zaleznosci.ParaLicznikowOsi = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.ParaLicznikowOsi)(ZaznaczonyLicznikNaLiscie)
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
        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik Is Nothing Then Exit Sub

        Dim odc1 As String = PobierzNazweOdcinka(licznik.Odcinek1?.Nazwa)
        Dim odc2 As String = PobierzNazweOdcinka(licznik.Odcinek2?.Nazwa)

        If Wspolne.ZadajPytanie($"Czy usunąć parę liczników osi dla odcinków torów {odc1} oraz {odc2}?") = DialogResult.Yes Then
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
            licznik.X1 = PobierzLiczbeRzeczywistaWZakresie(txtLicznik1X, plpPulpit.Pulpit.Szerokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub txtLicznik1Y_TextChanged() Handles txtLicznik1Y.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Y1 = PobierzLiczbeRzeczywistaWZakresie(txtLicznik1Y, plpPulpit.Pulpit.Wysokosc)
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
            licznik.X2 = PobierzLiczbeRzeczywistaWZakresie(txtLicznik2X, plpPulpit.Pulpit.Szerokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub txtLicznik2Y_TextChanged() Handles txtLicznik2Y.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Y2 = PobierzLiczbeRzeczywistaWZakresie(txtLicznik2Y, plpPulpit.Pulpit.Wysokosc)
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub cboLicznikOdcinek1_SelectedIndexChanged() Handles cboLicznikOdcinek1.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboLicznikOdcinek1.SelectedItem Is Nothing Then Exit Sub

        Dim odcinek As Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboLicznikOdcinek1.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Odcinek1 = odcinek.Wartosc
            ZaznaczonyLicznikNaLiscie.SubItems(2).Text = licznik.Odcinek1?.Nazwa
        End If
        plpPulpit.Invalidate()

        ZdarzeniaWlaczone = False
        OdswiezListeOdcinkowWComboBox(cboLicznikOdcinek2, True, licznik?.Odcinek2, licznik?.Odcinek1)
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub cboLicznikOdcinek2_SelectedIndexChanged() Handles cboLicznikOdcinek2.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboLicznikOdcinek2.SelectedItem Is Nothing Then Exit Sub

        Dim odcinek As Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru) = DirectCast(cboLicznikOdcinek2.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim licznik As Zaleznosci.ParaLicznikowOsi = plpPulpit.projZaznaczonyLicznik
        If licznik IsNot Nothing Then
            licznik.Odcinek2 = odcinek.Wartosc
            ZaznaczonyLicznikNaLiscie.SubItems(3).Text = licznik.Odcinek2?.Nazwa
        End If
        plpPulpit.Invalidate()

        ZdarzeniaWlaczone = False
        OdswiezListeOdcinkowWComboBox(cboLicznikOdcinek1, True, licznik?.Odcinek1, licznik?.Odcinek2)
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
        OdswiezListeOdcinkowWComboBox(cboLicznikOdcinek1, True, plpPulpit.projZaznaczonyLicznik?.Odcinek1, plpPulpit.projZaznaczonyLicznik?.Odcinek2)
        OdswiezListeOdcinkowWComboBox(cboLicznikOdcinek2, True, plpPulpit.projZaznaczonyLicznik?.Odcinek2, plpPulpit.projZaznaczonyLicznik?.Odcinek1)
    End Sub

    Private Function PobierzNazweOdcinka(nazwa As String) As String
        If nazwa Is Nothing Then
            Return BRAK_ODCINKA
        ElseIf nazwa = "" Then
            Return BRAK_NAZWY
        Else
            Return $"""{nazwa}"""
        End If
    End Function

#End Region 'Zakładka Liczniki osi

#Region "Zakładka Lampy"

    Private Sub lvLampy_SelectedIndexChanged() Handles lvLampy.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonaLampaNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvLampy)
        Dim lampa As Zaleznosci.Lampa = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.Lampa)(ZaznaczonaLampaNaLiscie)
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

        If Wspolne.ZadajPytanie("Czy usunąć lampę o adresie " & lampa.Adres.ToString & "?") = DialogResult.Yes Then
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
            lampa.X = PobierzLiczbeRzeczywistaWZakresie(txtLampaX, plpPulpit.Pulpit.Szerokosc)
            ZaznaczonaLampaNaLiscie.SubItems(1).Text = lampa.X.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtLampaY_TextChanged() Handles txtLampaY.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim lampa As Zaleznosci.Lampa = plpPulpit.projZaznaczonaLampa
        If lampa IsNot Nothing Then
            lampa.Y = PobierzLiczbeRzeczywistaWZakresie(txtLampaY, plpPulpit.Pulpit.Wysokosc)
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

#Region "Zakładka Przejazdy kolejowo-drogowe"

    Private Sub lvPrzejazdy_SelectedIndexChanged() Handles lvPrzejazdy.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyPrzejazdNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvPrzejazdy)
        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.PrzejazdKolejowoDrogowy)(ZaznaczonyPrzejazdNaLiscie)
        plpPulpit.projZaznaczonyPrzejazd = przejazd
        If przejazd Is Nothing Then
            txtPrzejazdNumer.Text = ""
            txtPrzejazdNazwa.Text = ""
            cbPrzejazdTrybAutomatyczny.Checked = False
            cbPrzejazdTrybReczny.Checked = False
            txtPrzejazdCzasSwiatla.Text = ""
            txtPrzejazdCzasOpuszczanie.Text = ""
            txtPrzejazdCzasPodnoszenie.Text = ""
            UstawAktywnoscPolPrzejazdu(False)
        Else
            txtPrzejazdNumer.Text = przejazd.Numer.ToString
            txtPrzejazdNazwa.Text = przejazd.Nazwa
            cbPrzejazdTrybAutomatyczny.Checked = (przejazd.Tryb And Zaleznosci.TrybPrzejazduKolejowego.Automatyczny) <> 0
            cbPrzejazdTrybReczny.Checked = (przejazd.Tryb And Zaleznosci.TrybPrzejazduKolejowego.Reczny) <> 0
            txtPrzejazdCzasSwiatla.Text = przejazd.CzasSwiatel.ToString
            txtPrzejazdCzasOpuszczanie.Text = przejazd.CzasOpuszczania.ToString
            txtPrzejazdCzasPodnoszenie.Text = przejazd.CzasPodnoszenia.ToString
            tbpPrzejazdAutomatyzacja.Enabled = (przejazd.Tryb And Zaleznosci.TrybPrzejazduKolejowego.Automatyczny) <> 0
            UstawAktywnoscPolPrzejazdu(True)
        End If
        OdswiezListePrzejazdAutomatyzacja()
        OdswiezListePrzejazdRogatki()
        OdswiezListePrzejazdSygnDrog()
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnPrzejazdDodaj_Click() Handles btnPrzejazdDodaj.Click
        plpPulpit.Pulpit.Przejazdy.Add(New Zaleznosci.PrzejazdKolejowoDrogowy)
        OdswiezListePrzejazdow()
    End Sub

    Private Sub btnPrzejazdUsun_Click() Handles btnPrzejazdUsun.Click
        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd Is Nothing Then Exit Sub

        Dim pyt As String = "Czy usunąć przejazd kolejowo-drogowy"
        If przejazd.Nazwa <> "" Then pyt &= " o nazwie " & przejazd.Nazwa

        If Wspolne.ZadajPytanie(pyt & "?") = DialogResult.Yes Then
            plpPulpit.Pulpit.UsunPrzejazdKolejowoDrogowy(przejazd)
            OdswiezListePrzejazdow()
        End If
    End Sub

    Private Sub txtPrzejazdNumer_TextChanged() Handles txtPrzejazdNumer.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd IsNot Nothing Then
            przejazd.Numer = PobierzKrotkaLiczbeNieujemna(txtPrzejazdNumer)
            ZaznaczonyPrzejazdNaLiscie.SubItems(0).Text = przejazd.Numer.ToString
        End If
    End Sub

    Private Sub txtPrzejazdNazwa_TextChanged() Handles txtPrzejazdNazwa.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd IsNot Nothing Then
            przejazd.Nazwa = txtPrzejazdNazwa.Text
            ZaznaczonyPrzejazdNaLiscie.SubItems(1).Text = przejazd.Nazwa
            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub cbPrzejazdTrybAutomatyczny_CheckedChanged() Handles cbPrzejazdTrybAutomatyczny.CheckedChanged
        UstawTrybZaznaczonegoPrzejazdu(Zaleznosci.TrybPrzejazduKolejowego.Automatyczny, cbPrzejazdTrybAutomatyczny.Checked)
    End Sub

    Private Sub cbPrzejazdTrybReczny_CheckedChanged() Handles cbPrzejazdTrybReczny.CheckedChanged
        UstawTrybZaznaczonegoPrzejazdu(Zaleznosci.TrybPrzejazduKolejowego.Reczny, cbPrzejazdTrybReczny.Checked)
    End Sub

    Private Sub txtPrzejazdCzasSwiatla_TextChanged() Handles txtPrzejazdCzasSwiatla.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd IsNot Nothing Then
            przejazd.CzasSwiatel = PobierzKrotkaLiczbeNieujemna(txtPrzejazdCzasSwiatla)
        End If
    End Sub

    Private Sub txtPrzejazdCzasOpuszczanie_TextChanged() Handles txtPrzejazdCzasOpuszczanie.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd IsNot Nothing Then
            przejazd.CzasOpuszczania = PobierzKrotkaLiczbeNieujemna(txtPrzejazdCzasOpuszczanie)
        End If
    End Sub

    Private Sub txtPrzejazdCzasPodnoszenie_TextChanged() Handles txtPrzejazdCzasPodnoszenie.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd IsNot Nothing Then
            przejazd.CzasPodnoszenia = PobierzKrotkaLiczbeNieujemna(txtPrzejazdCzasPodnoszenie)
        End If
    End Sub

    Private Sub tabPrzejazd_Selected() Handles tabPrzejazd.Selected
        If tabPrzejazd.SelectedTab Is tbpPrzejazdOgolne Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.Przejazdy

        ElseIf tabPrzejazd.SelectedTab Is tbpPrzejazdAutomatyzacja Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.PrzejazdyAutomatyzacja

        ElseIf tabPrzejazd.SelectedTab Is tbpPrzejazdRogatki Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.PrzejazdyRogatki

        ElseIf tabPrzejazd.SelectedTab Is tbpPrzejazdSygnDrog Then
            plpPulpit.projDodatkoweObiekty = Pulpit.RysujDodatkoweObiekty.PrzejazdySygnDrog

        End If
    End Sub

    Private Sub lvPrzejazdAutomatyzacja_SelectedIndexChanged() Handles lvPrzejazdAutomatyzacja.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyPrzejazdAutomatyzacjaNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvPrzejazdAutomatyzacja)
        Dim automatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.PrzejazdAutomatyczneZamykanie)(ZaznaczonyPrzejazdAutomatyzacjaNaLiscie)
        plpPulpit.projZaznaczonyPrzejazdAutomatyzacja = automatyzacja
        If automatyzacja Is Nothing Then
            cboPrzejazdAutomatyzacjaOdcinekWyjazd.SelectedItem = Nothing
            cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.SelectedItem = Nothing
            cboPrzejazdAutomatyzacjaSygnalizator.SelectedItem = Nothing
            UstawAktywnoscPolPrzejazdAutomatyzacja(False)
        Else
            OdswiezListeOdcinkowPrzejazdAutomatyzacja()
            ZaznaczElement(Of Zaleznosci.Kostka)(cboPrzejazdAutomatyzacjaSygnalizator, automatyzacja.Sygnalizator)
            UstawAktywnoscPolPrzejazdAutomatyzacja(True)
        End If
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnPrzejazdAutomatyzacjaDodaj_Click() Handles btnPrzejazdAutomatyzacjaDodaj.Click
        If plpPulpit.projZaznaczonyPrzejazd Is Nothing Then Exit Sub

        plpPulpit.projZaznaczonyPrzejazd.AutomatyczneZamykanie.Add(New Zaleznosci.PrzejazdAutomatyczneZamykanie())
        OdswiezListePrzejazdAutomatyzacja()
        OdswiezLiczbeObiektowAutomatyzacji()
    End Sub

    Private Sub btnPrzejazdAutomatyzacjaUsun_Click() Handles btnPrzejazdAutomatyzacjaUsun.Click
        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        Dim automatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie = plpPulpit.projZaznaczonyPrzejazdAutomatyzacja
        If przejazd Is Nothing OrElse automatyzacja Is Nothing Then Exit Sub

        Dim pyt As String = "Czy usunąć obiekt automatyzacji"
        If przejazd.Nazwa <> "" Then pyt &= " z przejazdu " & przejazd.Nazwa

        If Wspolne.ZadajPytanie(pyt & "?") = DialogResult.Yes Then
            przejazd.AutomatyczneZamykanie.Remove(automatyzacja)
            OdswiezListePrzejazdAutomatyzacja()
            OdswiezLiczbeObiektowAutomatyzacji()
        End If
    End Sub

    Private Sub cboPrzejazdAutomatyzacjaOdcinekWyjazd_SelectedIndexChanged() Handles cboPrzejazdAutomatyzacjaOdcinekWyjazd.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboPrzejazdAutomatyzacjaOdcinekWyjazd.SelectedItem Is Nothing Then Exit Sub
        If plpPulpit.projZaznaczonyPrzejazdAutomatyzacja Is Nothing Then Exit Sub

        Dim odcinek As Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru) = CType(cboPrzejazdAutomatyzacjaOdcinekWyjazd.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim automatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie = plpPulpit.projZaznaczonyPrzejazdAutomatyzacja
        automatyzacja.OdcinekWyjazd = odcinek.Wartosc
        ZaznaczonyPrzejazdAutomatyzacjaNaLiscie.SubItems(0).Text = odcinek.Wartosc.Nazwa
        plpPulpit.Invalidate()

        ZdarzeniaWlaczone = False
        OdswiezListeOdcinkowWComboBox(cboPrzejazdAutomatyzacjaOdcinekPrzyjazd, False, automatyzacja.OdcinekPrzyjazd, odcinek.Wartosc)
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub cboPrzejazdAutomatyzacjaOdcinekPrzyjazd_SelectedIndexChanged() Handles cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.SelectedItem Is Nothing Then Exit Sub
        If plpPulpit.projZaznaczonyPrzejazdAutomatyzacja Is Nothing Then Exit Sub

        Dim odcinek As Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru) = CType(cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru))
        Dim automatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie = plpPulpit.projZaznaczonyPrzejazdAutomatyzacja
        automatyzacja.OdcinekPrzyjazd = odcinek.Wartosc
        ZaznaczonyPrzejazdAutomatyzacjaNaLiscie.SubItems(1).Text = odcinek.Wartosc.Nazwa
        plpPulpit.Invalidate()

        ZdarzeniaWlaczone = False
        OdswiezListeOdcinkowWComboBox(cboPrzejazdAutomatyzacjaOdcinekWyjazd, False, automatyzacja.OdcinekWyjazd, odcinek.Wartosc)
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub cboPrzejazdAutomatyzacjaSygnalizator_SelectedIndexChanged() Handles cboPrzejazdAutomatyzacjaSygnalizator.SelectedIndexChanged
        If Not ZdarzeniaWlaczone Then Exit Sub
        If cboPrzejazdAutomatyzacjaSygnalizator.SelectedItem Is Nothing Then Exit Sub
        If plpPulpit.projZaznaczonyPrzejazdAutomatyzacja Is Nothing Then Exit Sub

        Dim sygn As Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy = CType(
            CType(cboPrzejazdAutomatyzacjaSygnalizator.SelectedItem, Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc,
            Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy)
        plpPulpit.projZaznaczonyPrzejazdAutomatyzacja.Sygnalizator = sygn
        ZaznaczonyPrzejazdAutomatyzacjaNaLiscie.SubItems(2).Text = sygn.Nazwa
        plpPulpit.Invalidate()
    End Sub

    Private Sub lvPrzejazdRogatki_SelectedIndexChanged() Handles lvPrzejazdRogatki.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyPrzejazdRogatkaNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvPrzejazdRogatki)
        Dim rogatka As Zaleznosci.PrzejazdRogatka = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.PrzejazdRogatka)(ZaznaczonyPrzejazdRogatkaNaLiscie)
        If rogatka Is Nothing Then
            txtPrzejazdRogatkaAdres.Text = ""
            txtPrzejazdRogatkaX.Text = ""
            txtPrzejazdRogatkaY.Text = ""
            txtPrzejazdRogatkaCzasDoZamkniecia.Text = ""
            UstawAktywnoscPolPrzejazdRogatka(False)
        Else
            txtPrzejazdRogatkaAdres.Text = rogatka.Adres.ToString
            txtPrzejazdRogatkaX.Text = rogatka.X.ToString
            txtPrzejazdRogatkaY.Text = rogatka.Y.ToString
            txtPrzejazdRogatkaCzasDoZamkniecia.Text = rogatka.CzasDoZamkniecia.ToString
            UstawAktywnoscPolPrzejazdRogatka(True)
        End If
        plpPulpit.projZaznaczonyPrzejazdRogatka = rogatka
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnPrzejazdRogatkaDodaj_Click() Handles btnPrzejazdRogatkaDodaj.Click
        If plpPulpit.projZaznaczonyPrzejazd Is Nothing Then Exit Sub

        plpPulpit.projZaznaczonyPrzejazd.Rogatki.Add(New Zaleznosci.PrzejazdRogatka())
        OdswiezListePrzejazdRogatki()
        OdswiezLiczbeRogatek()
    End Sub

    Private Sub btnPrzejazdRogatkaUsun_Click() Handles btnPrzejazdRogatkaUsun.Click
        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        Dim rogatka As Zaleznosci.PrzejazdRogatka = plpPulpit.projZaznaczonyPrzejazdRogatka
        If przejazd Is Nothing OrElse rogatka Is Nothing Then Exit Sub

        Dim pyt As String = "Czy usunąć rogatkę o adresie " & rogatka.Adres.ToString
        If przejazd.Nazwa <> "" Then pyt &= " z przejazdu " & przejazd.Nazwa

        If Wspolne.ZadajPytanie(pyt & "?") = DialogResult.Yes Then
            przejazd.Rogatki.Remove(rogatka)
            OdswiezListePrzejazdRogatki()
            OdswiezLiczbeRogatek()
        End If
    End Sub

    Private Sub txtPrzejazdRogatkaAdres_TextChanged() Handles txtPrzejazdRogatkaAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim rogatka As Zaleznosci.PrzejazdRogatka = plpPulpit.projZaznaczonyPrzejazdRogatka
        If rogatka IsNot Nothing Then
            rogatka.Adres = PobierzKrotkaLiczbeNieujemna(txtPrzejazdRogatkaAdres)
            ZaznaczonyPrzejazdRogatkaNaLiscie.SubItems(0).Text = rogatka.Adres.ToString
        End If
    End Sub

    Private Sub txtPrzejazdRogatkaX_TextChanged() Handles txtPrzejazdRogatkaX.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim rogatka As Zaleznosci.PrzejazdRogatka = plpPulpit.projZaznaczonyPrzejazdRogatka
        If rogatka IsNot Nothing Then
            rogatka.X = PobierzLiczbeRzeczywistaWZakresie(txtPrzejazdRogatkaX, plpPulpit.Pulpit.Szerokosc)
            ZaznaczonyPrzejazdRogatkaNaLiscie.SubItems(1).Text = rogatka.X.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtPrzejazdRogatkaY_TextChanged() Handles txtPrzejazdRogatkaY.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim rogatka As Zaleznosci.PrzejazdRogatka = plpPulpit.projZaznaczonyPrzejazdRogatka
        If rogatka IsNot Nothing Then
            rogatka.Y = PobierzLiczbeRzeczywistaWZakresie(txtPrzejazdRogatkaY, plpPulpit.Pulpit.Wysokosc)
            ZaznaczonyPrzejazdRogatkaNaLiscie.SubItems(2).Text = rogatka.Y.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtPrzejazdRogatkaCzasDoZamkniecia_TextChanged() Handles txtPrzejazdRogatkaCzasDoZamkniecia.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim rogatka As Zaleznosci.PrzejazdRogatka = plpPulpit.projZaznaczonyPrzejazdRogatka
        If rogatka IsNot Nothing Then
            rogatka.CzasDoZamkniecia = PobierzKrotkaLiczbeNieujemna(txtPrzejazdRogatkaCzasDoZamkniecia)
        End If
    End Sub

    Private Sub lvPrzejazdSygnDrog_SelectedIndexChanged() Handles lvPrzejazdSygnDrog.SelectedIndexChanged
        ZdarzeniaWlaczone = False
        ZaznaczonyPrzejazdSygnDrogNaLiscie = Wspolne.PobierzZaznaczonyElementNaLiscie(lvPrzejazdSygnDrog)
        Dim sygn As Zaleznosci.PrzejazdElementWykonawczy = Wspolne.PobierzTagZElementuListy(Of Zaleznosci.PrzejazdElementWykonawczy)(ZaznaczonyPrzejazdSygnDrogNaLiscie)
        If sygn Is Nothing Then
            txtPrzejazdSygnDrogAdres.Text = ""
            txtPrzejazdSygnDrogX.Text = ""
            txtPrzejazdSygnDrogY.Text = ""
            UstawAktywnoscPolPrzejazdSygnDrog(False)
        Else
            txtPrzejazdSygnDrogAdres.Text = sygn.Adres.ToString
            txtPrzejazdSygnDrogX.Text = sygn.X.ToString
            txtPrzejazdSygnDrogY.Text = sygn.Y.ToString
            UstawAktywnoscPolPrzejazdSygnDrog(True)
        End If
        plpPulpit.projZaznaczonyPrzejazdSygnDrog = sygn
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub btnPrzejazdSygnDrogDodaj_Click() Handles btnPrzejazdSygnDrogDodaj.Click
        If plpPulpit.projZaznaczonyPrzejazd Is Nothing Then Exit Sub

        plpPulpit.projZaznaczonyPrzejazd.SygnalizatoryDrogowe.Add(New Zaleznosci.PrzejazdElementWykonawczy)
        OdswiezListePrzejazdSygnDrog()
        OdswiezLiczbeSygnDrog()
    End Sub

    Private Sub btnPrzejazdSygnDrogUsun_Click() Handles btnPrzejazdSygnDrogUsun.Click
        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        Dim sygnDrog As Zaleznosci.PrzejazdElementWykonawczy = plpPulpit.projZaznaczonyPrzejazdSygnDrog
        If przejazd Is Nothing OrElse sygnDrog Is Nothing Then Exit Sub

        Dim pyt As String = "Czy usunąć sygnalizator drogowy o adresie " & sygnDrog.Adres
        If przejazd.Nazwa <> "" Then pyt &= " z przejazdu " & przejazd.Nazwa

        If Wspolne.ZadajPytanie(pyt & "?") = DialogResult.Yes Then
            przejazd.SygnalizatoryDrogowe.Remove(sygnDrog)
            OdswiezListePrzejazdSygnDrog()
            OdswiezLiczbeSygnDrog()
        End If
    End Sub

    Private Sub txtPrzejazdSygnDrogAdres_TextChanged() Handles txtPrzejazdSygnDrogAdres.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim sygn As Zaleznosci.PrzejazdElementWykonawczy = plpPulpit.projZaznaczonyPrzejazdSygnDrog
        If sygn IsNot Nothing Then
            sygn.Adres = PobierzKrotkaLiczbeNieujemna(txtPrzejazdSygnDrogAdres)
            ZaznaczonyPrzejazdSygnDrogNaLiscie.SubItems(0).Text = sygn.Adres.ToString
        End If
    End Sub

    Private Sub txtPrzejazdSygnDrogX_TextChanged() Handles txtPrzejazdSygnDrogX.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim sygn As Zaleznosci.PrzejazdElementWykonawczy = plpPulpit.projZaznaczonyPrzejazdSygnDrog
        If sygn IsNot Nothing Then
            sygn.X = PobierzLiczbeRzeczywistaWZakresie(txtPrzejazdSygnDrogX, plpPulpit.Pulpit.Szerokosc)
            ZaznaczonyPrzejazdSygnDrogNaLiscie.SubItems(1).Text = sygn.X.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub txtPrzejazdSygnDrogY_TextChanged() Handles txtPrzejazdSygnDrogY.TextChanged
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim sygn As Zaleznosci.PrzejazdElementWykonawczy = plpPulpit.projZaznaczonyPrzejazdSygnDrog
        If sygn IsNot Nothing Then
            sygn.Y = PobierzLiczbeRzeczywistaWZakresie(txtPrzejazdSygnDrogY, plpPulpit.Pulpit.Wysokosc)
            ZaznaczonyPrzejazdSygnDrogNaLiscie.SubItems(2).Text = sygn.Y.ToString
        End If
        plpPulpit.Invalidate()
    End Sub

    Private Sub OdswiezListePrzejazdow()
        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        lvPrzejazdy.Items.Clear()
        ZaznaczonyPrzejazdNaLiscie = Nothing

        For Each p As Zaleznosci.PrzejazdKolejowoDrogowy In plpPulpit.Pulpit.Przejazdy
            Dim lvi As New ListViewItem(New String() {
                p.Numer.ToString,
                p.Nazwa,
                PobierzTrybPrzejazdu(p),
                p.KostkiPrzejazdy.Count.ToString,
                p.AutomatyczneZamykanie.Count.ToString,
                p.Rogatki.Count.ToString,
                p.SygnalizatoryDrogowe.Count.ToString
            }) With {.Tag = p}

            If p Is przejazd Then
                lvi.Selected = True
                ZaznaczonyPrzejazdNaLiscie = lvi
            End If

            lvPrzejazdy.Items.Add(lvi)
        Next

        If ZaznaczonyPrzejazdNaLiscie Is Nothing Then lvPrzejazdy_SelectedIndexChanged()
    End Sub

    Private Sub OdswiezLiczbePrzypisanychKostekPrzejazdow()
        If lvPrzejazdy.Items Is Nothing Then Exit Sub

        For Each lvi As ListViewItem In lvPrzejazdy.Items
            Dim prz As Zaleznosci.PrzejazdKolejowoDrogowy = DirectCast(lvi.Tag, Zaleznosci.PrzejazdKolejowoDrogowy)
            lvi.SubItems(3).Text = prz.KostkiPrzejazdy.Count.ToString()
        Next
    End Sub

    Private Sub OdswiezLiczbeObiektowAutomatyzacji()
        If ZaznaczonyPrzejazdNaLiscie IsNot Nothing AndAlso plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            ZaznaczonyPrzejazdNaLiscie.SubItems(4).Text = plpPulpit.projZaznaczonyPrzejazd.AutomatyczneZamykanie.Count.ToString
        End If
    End Sub

    Private Sub OdswiezLiczbeRogatek()
        If ZaznaczonyPrzejazdNaLiscie IsNot Nothing AndAlso plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            ZaznaczonyPrzejazdNaLiscie.SubItems(5).Text = plpPulpit.projZaznaczonyPrzejazd.Rogatki.Count.ToString
        End If
    End Sub

    Private Sub OdswiezLiczbeSygnDrog()
        If ZaznaczonyPrzejazdNaLiscie IsNot Nothing AndAlso plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            ZaznaczonyPrzejazdNaLiscie.SubItems(6).Text = plpPulpit.projZaznaczonyPrzejazd.SygnalizatoryDrogowe.Count.ToString
        End If
    End Sub

    Private Sub UstawAktywnoscPolPrzejazdu(wlaczony As Boolean)
        btnPrzejazdUsun.Enabled = wlaczony
        tbpPrzejazdOgolne.Enabled = wlaczony
        If Not wlaczony Then tbpPrzejazdAutomatyzacja.Enabled = wlaczony
        tbpPrzejazdRogatki.Enabled = wlaczony
        tbpPrzejazdSygnDrog.Enabled = wlaczony
    End Sub

    Private Sub UstawTrybZaznaczonegoPrzejazdu(tryb As Zaleznosci.TrybPrzejazduKolejowego, ustawiony As Boolean)
        If Not ZdarzeniaWlaczone Then Exit Sub

        Dim przejazd As Zaleznosci.PrzejazdKolejowoDrogowy = plpPulpit.projZaznaczonyPrzejazd
        If przejazd IsNot Nothing Then
            If ustawiony Then
                przejazd.Tryb = przejazd.Tryb Or tryb
            Else
                przejazd.Tryb = przejazd.Tryb And (Not tryb)
            End If
            ZaznaczonyPrzejazdNaLiscie.SubItems(2).Text = PobierzTrybPrzejazdu(przejazd)
            tbpPrzejazdAutomatyzacja.Enabled = (przejazd.Tryb And Zaleznosci.TrybPrzejazduKolejowego.Automatyczny) <> 0
        End If
    End Sub

    Private Function PobierzTrybPrzejazdu(przejazd As Zaleznosci.PrzejazdKolejowoDrogowy) As String
        Dim wynik As String = ""
        If (przejazd.Tryb And Zaleznosci.TrybPrzejazduKolejowego.Automatyczny) <> 0 Then wynik &= PRZEJAZD_AUTOMATYCZNY
        If (przejazd.Tryb And Zaleznosci.TrybPrzejazduKolejowego.Reczny) <> 0 Then wynik &= PRZEJAZD_RECZNY
        Return wynik
    End Function

    Private Sub OdswiezListePrzejazdAutomatyzacja()
        Dim automatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie = plpPulpit.projZaznaczonyPrzejazdAutomatyzacja
        lvPrzejazdAutomatyzacja.Items.Clear()
        ZaznaczonyPrzejazdAutomatyzacjaNaLiscie = Nothing

        If plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            For Each a As Zaleznosci.PrzejazdAutomatyczneZamykanie In plpPulpit.projZaznaczonyPrzejazd.AutomatyczneZamykanie
                Dim lvi As New ListViewItem(New String() {
                    If(a.OdcinekWyjazd?.Nazwa, BRAK),
                    If(a.OdcinekPrzyjazd?.Nazwa, BRAK),
                    If(a.Sygnalizator?.Nazwa, BRAK)
                }) With {.Tag = a}

                If a Is automatyzacja Then
                    lvi.Selected = True
                    ZaznaczonyPrzejazdAutomatyzacjaNaLiscie = lvi
                End If

                lvPrzejazdAutomatyzacja.Items.Add(lvi)
            Next
        End If

        If ZaznaczonyPrzejazdAutomatyzacjaNaLiscie Is Nothing Then lvPrzejazdAutomatyzacja_SelectedIndexChanged()
    End Sub

    Private Sub OdswiezListeOdcinkowPrzejazdAutomatyzacja()
        OdswiezListeOdcinkowWComboBox(cboPrzejazdAutomatyzacjaOdcinekWyjazd, False, plpPulpit.projZaznaczonyPrzejazdAutomatyzacja?.OdcinekWyjazd, plpPulpit.projZaznaczonyPrzejazdAutomatyzacja?.OdcinekPrzyjazd)
        OdswiezListeOdcinkowWComboBox(cboPrzejazdAutomatyzacjaOdcinekPrzyjazd, False, plpPulpit.projZaznaczonyPrzejazdAutomatyzacja?.OdcinekPrzyjazd, plpPulpit.projZaznaczonyPrzejazdAutomatyzacja?.OdcinekWyjazd)
    End Sub

    Private Sub OdswiezListeSygnalizatorowPrzejazdAutomatyzacja()
        cboPrzejazdAutomatyzacjaSygnalizator.Items.Clear()
        cboPrzejazdAutomatyzacjaSygnalizator.Items.AddRange(PobierzKostkiDoComboBox(AddressOf Zaleznosci.Kostka.CzySygnalizatorTOP, AddressOf PobierzNazweToru, pominZaznaczony:=False))
        If plpPulpit.projZaznaczonyPrzejazdAutomatyzacja IsNot Nothing Then
            ZaznaczElement(Of Zaleznosci.Kostka)(cboPrzejazdAutomatyzacjaSygnalizator, plpPulpit.projZaznaczonyPrzejazdAutomatyzacja.Sygnalizator)
        End If
    End Sub

    Private Sub UstawAktywnoscPolPrzejazdAutomatyzacja(wlaczony As Boolean)
        btnPrzejazdAutomatyzacjaUsun.Enabled = wlaczony
        cboPrzejazdAutomatyzacjaOdcinekWyjazd.Enabled = wlaczony
        cboPrzejazdAutomatyzacjaOdcinekPrzyjazd.Enabled = wlaczony
        cboPrzejazdAutomatyzacjaSygnalizator.Enabled = wlaczony
    End Sub

    Private Sub OdswiezListePrzejazdRogatki()
        Dim rogatka As Zaleznosci.PrzejazdElementWykonawczy = plpPulpit.projZaznaczonyPrzejazdRogatka
        lvPrzejazdRogatki.Items.Clear()
        ZaznaczonyPrzejazdRogatkaNaLiscie = Nothing

        If plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            For Each r As Zaleznosci.PrzejazdElementWykonawczy In plpPulpit.projZaznaczonyPrzejazd.Rogatki
                Dim lvi As New ListViewItem(New String() {
                    r.Adres.ToString,
                    r.X.ToString,
                    r.Y.ToString
                }) With {.Tag = r}

                If r Is rogatka Then
                    lvi.Selected = True
                    ZaznaczonyPrzejazdRogatkaNaLiscie = lvi
                End If

                lvPrzejazdRogatki.Items.Add(lvi)
            Next
        End If

        If ZaznaczonyPrzejazdRogatkaNaLiscie Is Nothing Then lvPrzejazdRogatki_SelectedIndexChanged()
    End Sub

    Private Sub UstawAktywnoscPolPrzejazdRogatka(wlaczony As Boolean)
        btnPrzejazdRogatkaUsun.Enabled = wlaczony
        txtPrzejazdRogatkaAdres.Enabled = wlaczony
        txtPrzejazdRogatkaX.Enabled = wlaczony
        txtPrzejazdRogatkaY.Enabled = wlaczony
        txtPrzejazdRogatkaCzasDoZamkniecia.Enabled = wlaczony
    End Sub

    Private Sub OdswiezListePrzejazdSygnDrog()
        Dim sygn As Zaleznosci.PrzejazdElementWykonawczy = plpPulpit.projZaznaczonyPrzejazdSygnDrog
        lvPrzejazdSygnDrog.Items.Clear()
        ZaznaczonyPrzejazdSygnDrogNaLiscie = Nothing

        If plpPulpit.projZaznaczonyPrzejazd IsNot Nothing Then
            For Each s As Zaleznosci.PrzejazdElementWykonawczy In plpPulpit.projZaznaczonyPrzejazd.SygnalizatoryDrogowe
                Dim lvi As New ListViewItem(New String() {
                    s.Adres.ToString,
                    s.X.ToString,
                    s.Y.ToString
                }) With {.Tag = s}

                If s Is sygn Then
                    lvi.Selected = True
                    ZaznaczonyPrzejazdSygnDrogNaLiscie = lvi
                End If

                lvPrzejazdSygnDrog.Items.Add(lvi)
            Next
        End If

        If ZaznaczonyPrzejazdSygnDrogNaLiscie Is Nothing Then lvPrzejazdSygnDrog_SelectedIndexChanged()
    End Sub

    Private Sub UstawAktywnoscPolPrzejazdSygnDrog(wlaczony As Boolean)
        btnPrzejazdSygnDrogUsun.Enabled = wlaczony
        txtPrzejazdSygnDrogAdres.Enabled = wlaczony
        txtPrzejazdSygnDrogX.Enabled = wlaczony
        txtPrzejazdSygnDrogY.Enabled = wlaczony
    End Sub

#End Region 'Zakładka Przejazdy kolejowo-drogowe

#Region "Pulpit"

    Private Sub plpPulpit_ZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka) Handles plpPulpit.ZmianaZaznaczeniaKostki
        PokazPanelKonf()
    End Sub

    Private Sub plpPulpit_projZmianaZaznaczeniaLampy(lampa As Zaleznosci.Lampa) Handles plpPulpit.projZmianaZaznaczeniaLampy
        If Not ZdarzeniaWlaczone Then Exit Sub
        lvLampy.SelectedItems.Clear()
        If lampa IsNot Nothing Then ZaznaczElementNaLiscie(lvLampy, lampa)
    End Sub

    Private Sub plpPulpit_projZmianaPrzypisaniaToruDoOdcinka() Handles plpPulpit.projZmianaPrzypisaniaToruDoOdcinka
        OdswiezLiczbePrzypisanychKostekTorow()
    End Sub

    Private Sub plpPulpit_projZmianaPrzypisaniaKostkiDoPrzejazdu() Handles plpPulpit.projZmianaPrzypisaniaKostkiDoPrzejazdu
        OdswiezLiczbePrzypisanychKostekPrzejazdow()
    End Sub

    Private Sub plpPulpit_projZmianaZaznaczeniaRogatki(rogatka As Zaleznosci.PrzejazdRogatka) Handles plpPulpit.projZmianaZaznaczeniaRogatki
        If Not ZdarzeniaWlaczone Then Exit Sub
        lvPrzejazdRogatki.SelectedItems.Clear()
        If rogatka IsNot Nothing Then ZaznaczElementNaLiscie(lvPrzejazdRogatki, rogatka)
    End Sub

    Private Sub plpPulpit_projZmianaZaznaczeniaSygnalizatoraDrogowego(sygnalizator As Zaleznosci.PrzejazdElementWykonawczy) Handles plpPulpit.projZmianaZaznaczeniaSygnalizatoraDrogowego
        If Not ZdarzeniaWlaczone Then Exit Sub
        lvPrzejazdSygnDrog.SelectedItems.Clear()
        If sygnalizator IsNot Nothing Then ZaznaczElementNaLiscie(lvPrzejazdSygnDrog, sygnalizator)
    End Sub

#End Region 'Pulpit

#Region "Reszta"

    Private Sub OdswiezListeOdcinkowWComboBox(cbo As ComboBox, dodajPusty As Boolean, zaznaczony As Zaleznosci.OdcinekToru, ukryty As Zaleznosci.OdcinekToru)
        cbo.Items.Clear()
        If dodajPusty Then cbo.Items.Add(PUSTY_CBO_ODCINEK_TORU)
        Dim odcinki As IEnumerable(Of Zaleznosci.OdcinekToru) = plpPulpit.Pulpit.OdcinkiTorow.OrderBy(Function(o) o.Nazwa)

        For Each odc As Zaleznosci.OdcinekToru In odcinki
            If odc IsNot ukryty Then
                cbo.Items.Add(New Wspolne.ObiektComboBox(Of Zaleznosci.OdcinekToru)(odc, odc.Nazwa))
            End If
        Next

        ZaznaczElement(cbo, zaznaczony)
    End Sub

    Private Function PobierzKostkiDoComboBox(sprawdzanie As SprawdzTypKostki, nazwa As PobierzNazweKostki, Optional obiektUnikany As Zaleznosci.Kostka = Nothing, Optional pominZaznaczony As Boolean = True) As Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)()
        Dim kostki As New List(Of Wspolne.ObiektComboBox(Of Zaleznosci.Kostka))

        plpPulpit.Pulpit.PrzeiterujKostki(Sub(x, y, k)
                                              If sprawdzanie(k) AndAlso (Not pominZaznaczony Or k IsNot plpPulpit.ZaznaczonaKostka) AndAlso k IsNot obiektUnikany Then
                                                  kostki.Add(New Wspolne.ObiektComboBox(Of Zaleznosci.Kostka)(k, nazwa(k)))
                                              End If
                                          End Sub)

        Return kostki.OrderBy(Function(k) k.Tekst).ToArray()
    End Function

    Private Function PobierzNazweToru(kostka As Zaleznosci.Kostka) As String
        Return DirectCast(kostka, Zaleznosci.Tor).Nazwa
    End Function

    Private Sub ZaznaczElement(Of T As Class)(cbo As ComboBox, el As T)
        If el Is Nothing Then
            cbo.SelectedItem = Nothing
            Exit Sub
        End If

        For i As Integer = 0 To cbo.Items.Count - 1
            If DirectCast(cbo.Items(i), Wspolne.ObiektComboBox(Of T)).Wartosc Is el Then
                cbo.SelectedIndex = i
                Exit Sub
            End If
        Next

        cbo.SelectedItem = Nothing
    End Sub

    Private Sub ZaznaczElementNaLiscie(Of T As Class)(lista As ListView, szukany As T)
        For Each lvi As ListViewItem In lista.Items
            If TryCast(lvi.Tag, T) Is szukany Then
                lvi.Selected = True
                Exit For
            End If
        Next
    End Sub

    Private Function PobierzKrotkaLiczbeNieujemna(pole As TextBox) As UShort
        Dim liczba As UShort = 0
        UShort.TryParse(pole.Text, liczba)
        Return liczba
    End Function

    Private Function PobierzLiczbeRzeczywistaNieujemna(pole As TextBox) As Single
        Dim liczba As Single = 0.0F
        If Single.TryParse(pole.Text, liczba) Then
            If liczba < 0.0F Then liczba = 0.0F
        End If

        Return liczba
    End Function

    Private Function PobierzLiczbeRzeczywistaWZakresie(pole As TextBox, zakresMax As Single) As Single
        Dim liczba As Single = PobierzLiczbeRzeczywistaNieujemna(pole)
        If liczba > zakresMax Then liczba = zakresMax

        Return liczba
    End Function

    Private Function PobierzLiczbeRzeczywistaWZakresie(pole As TextBox, zakresMin As Single, zakresMax As Single) As Single
        Dim liczba As Single = PobierzLiczbeRzeczywistaWZakresie(pole, zakresMax)
        If liczba < zakresMin Then liczba = zakresMin

        Return liczba
    End Function

    Private Class OpakowywaczEnum(Of T)
        Public Element As T

        Public Sub New(el As T)
            Element = el
        End Sub
    End Class
#End Region 'Reszta

End Class