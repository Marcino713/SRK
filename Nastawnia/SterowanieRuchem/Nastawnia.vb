Imports System.IO
Imports System.Threading

Public Class wndNastawnia
    Private Const CZEKANIE_NA_ZAMKNIECIE As Integer = 2000
    Private ReadOnly NAZWA_OKNA As String

    Private WithEvents Klient As New Zaleznosci.KlientTCP
    Private WithEvents OknoDodawaniaPociagu As wndDodawaniePociagu = Nothing
    Private WithEvents OknoWybieraniaPociagu As wndWyborPociagu = Nothing
    Private WithEvents OknoOswietlenia As wndOswietlenie = Nothing
    Private WlaczoneOknoWyboruPost As Boolean = False
    Private Sygnalizatory As New Dictionary(Of UShort, Zaleznosci.Kostka)
    Private Rozjazdy As New Dictionary(Of UShort, Zaleznosci.Rozjazd)
    Private Kierunki As New Dictionary(Of UShort, Zaleznosci.Kierunek)
    Private Lampy As New Dictionary(Of UShort, Zaleznosci.Lampa)
    Private Przejazdy As New Dictionary(Of UShort, Zaleznosci.PrzejazdKolejowoDrogowy)
    Private PredkoscMaksymalna As UShort
    Private KursorDomyslny As Cursor
    Private OknaSterowaniaPociagami As New HashSet(Of wndSterowaniePociagiem)
    Private CzyUsuwacOknaSterowaniaPociagami As Boolean = True

    Private actPokazStatus As Action(Of String, Color, Boolean) = AddressOf PokazStatusPolaczenia
    Private actPokazNazweOkna As Action(Of String) = AddressOf PokazNazweOkna
    Private actZamknijOkna As Action = Sub() ZamknijOkna()
    Private actPokazPulpit As Action(Of Zaleznosci.Pulpit) = AddressOf PokazPulpit
    Private actUsunMigacz As Action = AddressOf UsunMigacz
    Private actPokazBlad As Action(Of String) = AddressOf Wspolne.PokazBlad
    Private actPokazKomunikat As Action(Of String) = AddressOf Wspolne.PokazKomunikat

    Public Sub New()
        InitializeComponent()
        Dim argumenty As String() = Environment.GetCommandLineArgs()

        UstawTypRysownika(argumenty)
        plpPulpit.TypRysownika = WybranyTypRysownika
        plpPulpit.Wysrodkuj()

        NAZWA_OKNA = Text
        KursorDomyslny = plpPulpit.Cursor

        WczytajPlikiPosterunkow(argumenty)
    End Sub

    Friend Sub UsunOknoSterowaniaPociagiem(okno As wndSterowaniePociagiem)
        If CzyUsuwacOknaSterowaniaPociagami Then OknaSterowaniaPociagami.Remove(okno)
    End Sub

    Private Sub wndNastawnia_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not Klient.Uruchomiony Then Exit Sub

        If Wspolne.ZadajPytanie("Zamknąć okno? Spowoduje to rozłączenie z serwerem.") = DialogResult.Yes Then
            actZamknijOkna()
            Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
            Dim t As New Thread(AddressOf ZamknijPolaczenie)
            t.Start(Klient)
            Klient = Nothing
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub mnuPolaczZSerwerem_Click() Handles mnuPolaczZSerwerem.Click
        Dim wnd As New wndWyborPosterunku(Klient)
        WlaczoneOknoWyboruPost = True
        wnd.ShowDialog()
        WlaczoneOknoWyboruPost = False
        If wnd.Pulpit IsNot Nothing Then
            PredkoscMaksymalna = wnd.PredkoscMaksymalnaSieci
            actPokazPulpit(wnd.Pulpit)
            plpPulpit.InicjalizujMigacz()
            actPokazStatus("Połączono", Color.Green, True)
            actPokazNazweOkna(wnd.Pulpit.Nazwa)
        End If
    End Sub

    Private Sub mnuRozlaczZSerwerem_Click() Handles mnuRozlaczZSerwerem.Click
        If Wspolne.ZadajPytanie("Czy rozłączyć z serwerem?") = DialogResult.Yes Then
            actZamknijOkna()
            Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
            actPokazStatus("Rozłączanie...", Color.Blue, True)
        End If
    End Sub

    Private Sub mnuZarzadzajSerwerem_Click() Handles mnuZarzadzajSerwerem.Click
        Dim t As New Thread(AddressOf PokazOknoSerwera)
        t.SetApartmentState(ApartmentState.STA)
        t.Start()
    End Sub

    Private Sub mnuDodajPociag_Click() Handles mnuDodajPociag.Click
        If OknoDodawaniaPociagu Is Nothing Then
            OknoDodawaniaPociagu = New wndDodawaniePociagu(Klient, plpPulpit)
            OknoDodawaniaPociagu.Show()
            plpPulpit.MozliwoscWcisnieciaPrzycisku = False
            plpPulpit.MozliwoscZaznaczeniaToru = True
        Else
            OknoDodawaniaPociagu.Focus()
        End If
    End Sub

    Private Sub mnuSterujPociagiem_Click() Handles mnuSterujPociagiem.Click
        If OknoWybieraniaPociagu Is Nothing Then
            OknoWybieraniaPociagu = New wndWyborPociagu(Klient)
            OknoWybieraniaPociagu.Show()
        Else
            OknoWybieraniaPociagu.Focus()
        End If
    End Sub

    Private Sub mnuOswietlenie_Click() Handles mnuOswietlenie.Click
        If OknoOswietlenia Is Nothing Then
            OknoOswietlenia = New wndOswietlenie(Klient, plpPulpit, Lampy)
            OknoOswietlenia.Show()
            plpPulpit.MozliwoscZaznaczeniaLamp = True
            plpPulpit.Cursor = Cursors.Default
        Else
            OknoOswietlenia.Focus()
        End If
    End Sub

    Private Sub mnuEdytorWyswietlaczaPeronowego_Click() Handles mnuEdytorWyswietlaczaPeronowego.Click
        Dim wnd As New wndEdytorWyswietlaczaPeronowego
        wnd.Show()
    End Sub

    Private Sub mnuProjektantPosterunku_Click() Handles mnuProjektantPosterunku.Click
        Dim wnd As New wndProjektantPosterunku()
        wnd.Show()
    End Sub

    Private Sub mnuNowePolaczenia_Click() Handles mnuNowePolaczenia.Click
        Wspolne.PokazKomunikat("Plik połączeń należy zapisać w tym samym folderze, w którym znajdują się pliki konfiguracji posterunków ruchu.")
        WczytajPolaczenia(New SaveFileDialog, AddressOf Zaleznosci.PolaczeniaPosterunkow.OtworzFolder, "Nie udało się zapisać pliku.")
    End Sub

    Private Sub mnuOtworzPolaczenia_Click() Handles mnuOtworzPolaczenia.Click
        WczytajPolaczenia(New OpenFileDialog, AddressOf Zaleznosci.PolaczeniaPosterunkow.OtworzPlik, "Nie udało się otworzyć pliku.")
    End Sub

    Private Sub OknoDodawaniaPociagu_FormClosed() Handles OknoDodawaniaPociagu.FormClosed
        OknoDodawaniaPociagu = Nothing
        plpPulpit.MozliwoscZaznaczeniaToru = False
        plpPulpit.MozliwoscWcisnieciaPrzycisku = True
    End Sub

    Private Sub OknoWybieraniaPociagu_FormClosed() Handles OknoWybieraniaPociagu.FormClosed
        If OknoWybieraniaPociagu.WybranyPociagNr.HasValue Then
            Dim predkoscMaks As UShort
            If OknoWybieraniaPociagu.WybranyPociagPredkoscMaksymalna = 0 Then
                predkoscMaks = PredkoscMaksymalna
            Else
                predkoscMaks = Math.Min(OknoWybieraniaPociagu.WybranyPociagPredkoscMaksymalna, PredkoscMaksymalna)
            End If
            Dim okno As New wndSterowaniePociagiem(Klient, Me, OknoWybieraniaPociagu.WybranyPociagNr.Value, OknoWybieraniaPociagu.WybranyPociagNazwa, predkoscMaks)
            OknaSterowaniaPociagami.Add(okno)
            okno.Show()
        End If

        OknoWybieraniaPociagu = Nothing
    End Sub

    Private Sub OknoOswietlenia_FormClosed() Handles OknoOswietlenia.FormClosed
        OknoOswietlenia = Nothing
        plpPulpit.MozliwoscZaznaczeniaLamp = False
        plpPulpit.Cursor = KursorDomyslny
    End Sub

    Private Sub plpPulpit_WcisnietoPrzycisk(kostka As Zaleznosci.Kostka) Handles plpPulpit.WcisnietoPrzycisk
        Select Case kostka.Typ

            Case Zaleznosci.TypKostki.RozjazdLewo, Zaleznosci.TypKostki.RozjazdPrawo
                Dim roz As Zaleznosci.Rozjazd = DirectCast(kostka, Zaleznosci.Rozjazd)
                Dim stan As Zaleznosci.UstawianyStanRozjazdu
                If roz.Stan = Zaleznosci.StanRozjazdu.Wprost Then
                    stan = Zaleznosci.UstawianyStanRozjazdu.Bok
                Else
                    stan = Zaleznosci.UstawianyStanRozjazdu.Wprost
                End If
                Klient.WyslijUstawZwrotnice(New Zaleznosci.UstawZwrotnice() With {.Adres = roz.Adres, .Ustawienie = stan})

            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(kostka, Zaleznosci.Przycisk)

                Select Case prz.TypPrzycisku
                    Case Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy
                        If prz.SygnalizatorPolsamoczynny IsNot Nothing Then
                            Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {
                                                                .Adres = prz.SygnalizatorPolsamoczynny.Adres,
                                                                .Stan = Zaleznosci.UstawianyStanSygnalizatora.Zastepczy})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegu
                        If prz.SygnalizatorPolsamoczynny IsNot Nothing Then
                            Klient.WyslijZwolnijPrzebieg(New Zaleznosci.ZwolnijPrzebieg() With {
                                                          .Adres = prz.SygnalizatorPolsamoczynny.Adres,
                                                          .Przebieg = Zaleznosci.ZwalnianyPrzebieg.Zezwalajacy})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego
                        If prz.SygnalizatorPolsamoczynny IsNot Nothing Then
                            Klient.WyslijZwolnijPrzebieg(New Zaleznosci.ZwolnijPrzebieg() With {
                                                          .Adres = prz.SygnalizatorPolsamoczynny.Adres,
                                                          .Przebieg = Zaleznosci.ZwalnianyPrzebieg.Manewrowy})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnManewrowego
                        If prz.SygnalizatorManewrowy IsNot Nothing Then
                            Klient.WyslijZwolnijPrzebieg(New Zaleznosci.ZwolnijPrzebieg() With {
                                                          .Adres = prz.SygnalizatorManewrowy.Adres,
                                                          .Przebieg = Zaleznosci.ZwalnianyPrzebieg.Manewrowy})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.WlaczenieSBL
                        If prz.Kierunek?.NalezyDoOdcinka IsNot Nothing Then
                            Klient.WyslijUstawKierunek(New Zaleznosci.UstawKierunek() With {
                                                       .Adres = prz.Kierunek.NalezyDoOdcinka.Adres,
                                                       .Stan = Zaleznosci.UstawianyStanKierunku.Wlacz})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.PotwierdzenieSBL
                        If prz.Kierunek?.NalezyDoOdcinka IsNot Nothing Then
                            Klient.WyslijPotwierdzKierunek(New Zaleznosci.PotwierdzKierunek() With {
                                                           .Adres = prz.Kierunek.NalezyDoOdcinka.Adres})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.ZwolnienieSBL
                        If prz.Kierunek?.NalezyDoOdcinka IsNot Nothing Then
                            Klient.WyslijUstawKierunek(New Zaleznosci.UstawKierunek() With {
                                                       .Adres = prz.Kierunek.NalezyDoOdcinka.Adres,
                                                       .Stan = Zaleznosci.UstawianyStanKierunku.Wylacz})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.KasowanieRozprucia
                        If prz.Rozjazd IsNot Nothing Then
                            Klient.WyslijUstawZwrotnice(New Zaleznosci.UstawZwrotnice() With {
                                                        .Adres = prz.Rozjazd.Adres,
                                                        .Ustawienie = Zaleznosci.UstawianyStanRozjazdu.KasujRozprucie})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.ZamknieciePrzejazdu
                        If prz.Przejazd IsNot Nothing Then
                            Klient.WyslijUstawStanPrzejazdu(New Zaleznosci.UstawStanPrzejazdu() With {
                                                            .Numer = prz.Przejazd.Numer,
                                                            .Stan = Zaleznosci.UstawianyStanPrzejazdu.Zamkniety})
                        End If

                    Case Zaleznosci.TypPrzyciskuEnum.OtwarciePrzejazdu
                        If prz.Przejazd IsNot Nothing Then
                            Klient.WyslijUstawStanPrzejazdu(New Zaleznosci.UstawStanPrzejazdu() With {
                                                            .Numer = prz.Przejazd.Numer,
                                                            .Stan = Zaleznosci.UstawianyStanPrzejazdu.Otwarty})
                        End If

                End Select

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(kostka, Zaleznosci.PrzyciskTor)

                Dim stan As Zaleznosci.UstawianyStanSygnalizatora
                Dim adres As UShort?

                Select Case prz.TypPrzycisku
                    Case Zaleznosci.TypPrzyciskuTorEnum.JazdaSygnalizatorPolsamoczynny
                        stan = Zaleznosci.UstawianyStanSygnalizatora.Zezwalajacy
                        adres = prz.SygnalizatorPolsamoczynny?.Adres

                    Case Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorPolsamoczynny
                        stan = Zaleznosci.UstawianyStanSygnalizatora.Manewrowy
                        adres = prz.SygnalizatorPolsamoczynny?.Adres

                    Case Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorManewrowy
                        stan = Zaleznosci.UstawianyStanSygnalizatora.Manewrowy
                        adres = prz.SygnalizatorManewrowy?.Adres

                End Select

                If adres.HasValue Then
                    Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {.Adres = adres.Value, .Stan = stan})
                End If

            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                Dim tm As Zaleznosci.SygnalizatorManewrowy = DirectCast(kostka, Zaleznosci.SygnalizatorManewrowy)
                Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {.Adres = tm.Adres, .Stan = Zaleznosci.UstawianyStanSygnalizatora.Manewrowy})

        End Select
    End Sub

    Private Sub Klient_ZakonczonoPolaczenie() Handles Klient.ZakonczonoPolaczenie
        CzyscOkno()
    End Sub

    Private Sub Klient_OdebranoZakonczonoSesjeKlienta(kom As Zaleznosci.ZakonczonoSesjeKlienta) Handles Klient.OdebranoZakonczonoSesjeKlienta
        CzyscOkno()

        If Not WlaczoneOknoWyboruPost Then
            Dim tresc As String = PrzyczynaZakonczeniaSesjiKlientaToString(kom.Przyczyna)
            If kom.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaSesjiKlienta.RozlaczenieKlienta Then
                Invoke(actPokazKomunikat, tresc)
            Else
                Invoke(actPokazBlad, tresc)
            End If
        End If
    End Sub

    Private Sub Klient_OdebranoZakonczonoDzialanieSerwera(kom As Zaleznosci.ZakonczonoDzialanieSerwera) Handles Klient.OdebranoZakonczonoDzialanieSerwera
        CzyscOkno()
        If Not WlaczoneOknoWyboruPost Then Invoke(actPokazBlad, "Serwer został zatrzymany.")
    End Sub

    Private Sub Klient_OdebranoZmienionoStanSygnalizatora(kom As Zaleznosci.ZmienionoStanSygnalizatora) Handles Klient.OdebranoZmienionoStanSygnalizatora
        Dim sygn As Zaleznosci.Kostka = Nothing
        If Not Sygnalizatory.TryGetValue(kom.Adres, sygn) Then Exit Sub

        Select Case sygn.Typ
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                Dim s As Zaleznosci.SygnalizatorManewrowy = DirectCast(sygn, Zaleznosci.SygnalizatorManewrowy)
                If kom.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Then
                    s.Stan = Zaleznosci.StanSygnalizatoraManewrowego.BrakWyjazdu
                Else
                    s.Stan = Zaleznosci.StanSygnalizatoraManewrowego.Manewrowy
                End If

            Case Zaleznosci.TypKostki.SygnalizatorSamoczynny
                Dim s As Zaleznosci.SygnalizatorSamoczynny = DirectCast(sygn, Zaleznosci.SygnalizatorSamoczynny)
                If kom.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Then
                    s.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.BrakWyjazdu
                Else
                    s.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.Zezwalajacy
                End If

            Case Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                Dim s As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(sygn, Zaleznosci.SygnalizatorPolsamoczynny)
                s.Stan = kom.Stan

            Case Zaleznosci.TypKostki.SygnalizatorPowtarzajacy
                Dim s As Zaleznosci.SygnalizatorPowtarzajacy = DirectCast(sygn, Zaleznosci.SygnalizatorPowtarzajacy)
                If kom.Stan = Zaleznosci.StanSygnalizatora.Zezwalajacy Then
                    s.Stan = Zaleznosci.StanSygnalizatoraPowtarzajacego.Zezwalajacy
                Else
                    s.Stan = Zaleznosci.StanSygnalizatoraPowtarzajacego.BrakWyjazdu
                End If
        End Select

        plpPulpit.Invalidate()
    End Sub

    Private Sub Klient_OdebranoZmienionoStanToru(kom As Zaleznosci.ZmienionoStanToru) Handles Klient.OdebranoZmienionoStanToru
        For i As Integer = 0 To kom.Tory.Length - 1
            Dim akt As Zaleznosci.AktualizowanyKawalekToru = kom.Tory(i)
            If Not plpPulpit.Pulpit.CzyKostkaNiepusta(akt.WspolrzedneKostki) Then Continue For

            Dim k As Zaleznosci.Kostka = plpPulpit.Pulpit.Kostki(akt.WspolrzedneKostki.X, akt.WspolrzedneKostki.Y)
            If akt.Polozenie = Zaleznosci.PolozenieToru.RozjazdWBok Then
                Dim rozjazd As Zaleznosci.Rozjazd = TryCast(k, Zaleznosci.Rozjazd)
                If rozjazd IsNot Nothing Then rozjazd.ZajetoscBok = akt.Zajetosc
            Else
                Dim tor As Zaleznosci.Tor = TryCast(k, Zaleznosci.Tor)
                If tor IsNot Nothing Then tor.Zajetosc = akt.Zajetosc
            End If
        Next

        plpPulpit.Invalidate()
    End Sub

    Private Sub Klient_OdebranoZmienionoStanZwrotnicy(kom As Zaleznosci.ZmienionoStanZwrotnicy) Handles Klient.OdebranoZmienionoStanZwrotnicy
        Dim rozj As Zaleznosci.Rozjazd = Nothing
        If Not Rozjazdy.TryGetValue(kom.Adres, rozj) Then Exit Sub

        rozj.Rozprucie = kom.Rozprucie
        rozj.Stan = kom.Stan
        plpPulpit.Invalidate()
    End Sub

    Private Sub Klient_OdebranoZmienionoKierunek(kom As Zaleznosci.ZmienionoKierunek) Handles Klient.OdebranoZmienionoKierunek
        Dim kier As Zaleznosci.Kierunek = Nothing

        If kom.Blad = Zaleznosci.BladZmianyKierunku.Brak AndAlso Kierunki.TryGetValue(kom.Adres, kier) Then
            Select Case kom.Stan
                Case Zaleznosci.ObecnyStanKierunku.Neutralny
                    kier.UstawionyKierunek = Zaleznosci.UstawionyKierunekSBL.Zaden

                Case Zaleznosci.ObecnyStanKierunku.Wyjazd
                    kier.UstawionyKierunek = If(kier.KierunekWyjazdu = Zaleznosci.KierunekWyjazduSBL.Lewo, Zaleznosci.UstawionyKierunekSBL.Lewo, Zaleznosci.UstawionyKierunekSBL.Prawo)

                Case Zaleznosci.ObecnyStanKierunku.Przyjazd
                    kier.UstawionyKierunek = If(kier.KierunekWyjazdu = Zaleznosci.KierunekWyjazduSBL.Lewo, Zaleznosci.UstawionyKierunekSBL.Prawo, Zaleznosci.UstawionyKierunekSBL.Lewo)

            End Select

            Select Case kom.StanZmiany
                Case Zaleznosci.StanZmianyKierunku.Brak
                    kier.UstawionyStanZmiany = Zaleznosci.UstawionyStanZmianyKierunkuSBL.Zaden

                Case Zaleznosci.StanZmianyKierunku.ZadanieWlaczenia
                    kier.UstawionyStanZmiany = Zaleznosci.UstawionyStanZmianyKierunkuSBL.OczekiwanieNaPotwierdzenie

                Case Zaleznosci.StanZmianyKierunku.ZadanieWlaczenia
                Case Zaleznosci.StanZmianyKierunku.AnulowanieWlaczenia
                    kier.UstawionyStanZmiany = Zaleznosci.UstawionyStanZmianyKierunkuSBL.Wlaczanie

                Case Zaleznosci.StanZmianyKierunku.Zwalnianie
                    kier.UstawionyStanZmiany = Zaleznosci.UstawionyStanZmianyKierunkuSBL.Wylaczanie

            End Select

            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub Klient_OdebranoZmienionoJasnoscLamp(kom As Zaleznosci.ZmienionoJasnoscLamp) Handles Klient.OdebranoZmienionoJasnoscLamp
        Dim l As Zaleznosci.Lampa = Nothing
        Dim zaznaczona As Integer = -1
        If OknoOswietlenia IsNot Nothing Then zaznaczona = OknoOswietlenia.ZaznaczonaLampa

        For i As Integer = 0 To kom.Adresy.Length - 1
            If Lampy.TryGetValue(kom.Adresy(i), l) Then
                l.OdkolejkujZmianeJasnosci()
                If l.Adres = zaznaczona Then
                    OknoOswietlenia?.OdswiezJasnoscZaznaczonejLampy(CUShort(zaznaczona))
                End If
            End If
        Next
    End Sub

    Private Sub Klient_OdebranoZmienionoStanPrzejazdu(kom As Zaleznosci.ZmienionoStanPrzejazdu) Handles Klient.OdebranoZmienionoStanPrzejazdu
        Dim prz As Zaleznosci.PrzejazdKolejowoDrogowy = Nothing

        If kom.Blad = Zaleznosci.BladZmianyStanuPrzejazdu.Brak AndAlso Przejazdy.TryGetValue(kom.Numer, prz) Then
            For Each k As Zaleznosci.PrzejazdKolejowoDrogowyKostka In prz.KostkiPrzejazdy
                k.Stan = kom.Stan
                k.Awaria = kom.Awaria
            Next

            plpPulpit.Invalidate()
        End If
    End Sub

    Private Sub WczytajPolaczenia(Okno As FileDialog, MetodaOtwierajaca As Func(Of String, Zaleznosci.PolaczeniaPosterunkow), KomunikatBledu As String)
        Okno.Filter = Wspolne.FILTR_PLIKU_POLACZEN

        If Okno.ShowDialog = DialogResult.OK Then
            Dim polaczenia As Zaleznosci.PolaczeniaPosterunkow = MetodaOtwierajaca(Okno.FileName)
            If polaczenia Is Nothing Then
                Wspolne.PokazBlad(KomunikatBledu)
            Else
                Dim wnd As New wndKonfiguratorPolaczen(polaczenia)
                wnd.Show()
            End If
        End If
    End Sub

    Private Sub CzyscOkno()
        Invoke(actPokazStatus, "Rozłączono", Color.Red, False)
        Invoke(actPokazNazweOkna, String.Empty)
        Invoke(actPokazPulpit, New Zaleznosci.Pulpit)
        Invoke(actUsunMigacz)
        Invoke(actZamknijOkna)
    End Sub

    Private Sub ZamknijPolaczenie(klient As Object)
        Thread.Sleep(CZEKANIE_NA_ZAMKNIECIE)
        CType(klient, Zaleznosci.KlientTCP).Zakoncz(False)
    End Sub

    Private Sub PokazOknoSerwera()
        Dim wnd As New SerwerSterujacy.wndOknoSerwera(False)
        wnd.ShowDialog()
    End Sub

    Private Sub PokazStatusPolaczenia(tekst As String, kolor As Color, polaczony As Boolean)
        tslStanPolaczenia.Text = tekst
        tslStanPolaczenia.ForeColor = kolor
        mnuPolaczZSerwerem.Enabled = Not polaczony
        mnuRozlaczZSerwerem.Enabled = polaczony
        mnuDodajPociag.Enabled = polaczony
        mnuSterujPociagiem.Enabled = polaczony
        mnuOswietlenie.Enabled = polaczony
    End Sub

    Private Sub PokazNazweOkna(nazwaPosterunku As String)
        If String.IsNullOrEmpty(nazwaPosterunku) Then
            Text = NAZWA_OKNA
        Else
            Text = $"{NAZWA_OKNA} - {nazwaPosterunku}"
        End If
    End Sub

    Private Sub PokazPulpit(pulpit As Zaleznosci.Pulpit)
        plpPulpit.Pulpit = pulpit

        Sygnalizatory = pulpit.PobierzSygnalizatory()
        Rozjazdy = pulpit.PobierzRozjazdy()
        Kierunki = pulpit.PobierzKierunkiPoAdresieOdcinka()
        Lampy = pulpit.PobierzLampy()
        Przejazdy = pulpit.PobierzPrzejazdyKolejowoDrogowe()
    End Sub

    Private Sub UsunMigacz()
        plpPulpit.UsunMigacz()
    End Sub

    Private Sub ZamknijOkna()
        OknoDodawaniaPociagu?.Close()
        OknoWybieraniaPociagu?.Close()
        OknoOswietlenia?.Close()

        CzyUsuwacOknaSterowaniaPociagami = False
        For Each okno As wndSterowaniePociagiem In OknaSterowaniaPociagami
            okno.Zamknij()
        Next
        CzyUsuwacOknaSterowaniaPociagami = True

        OknaSterowaniaPociagami.Clear()
    End Sub

    Private Sub WczytajPlikiPosterunkow(argumenty As String())
        For Each arg As String In argumenty
            If arg.EndsWith(Zaleznosci.Pulpit.ROZSZERZENIE_PLIKU) AndAlso File.Exists(arg) Then
                Dim wnd As New wndProjektantPosterunku(arg)
                wnd.Show()
            End If
        Next
    End Sub
End Class