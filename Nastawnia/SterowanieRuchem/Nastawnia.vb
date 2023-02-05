﻿Imports System.Threading

Public Class wndNastawnia
    Private Const FILTR_PLIKU As String = Zaleznosci.PolaczeniaStacji.OPIS_PLIKU & "|*" & Zaleznosci.PolaczeniaStacji.ROZSZERZENIE_PLIKU
    Private Const CZEKANIE_NA_ZAMKNIECIE As Integer = 2000

    Private WithEvents Klient As New Zaleznosci.KlientTCP
    Private WithEvents OknoDodawaniaPociagu As wndDodawaniePociagu = Nothing
    Private WithEvents OknoWybieraniaPociagu As wndWyborPociagu = Nothing
    Private WithEvents OknoOswietlenia As wndOswietlenie = Nothing
    Private WlaczoneOknoWyboruPost As Boolean = False
    Private Sygnalizatory As New Dictionary(Of UShort, Zaleznosci.Kostka)
    Private Rozjazdy As New Dictionary(Of UShort, Zaleznosci.Rozjazd)
    Private Kierunki As New Dictionary(Of UShort, Zaleznosci.Kierunek)
    Private Lampy As New Dictionary(Of UShort, Zaleznosci.Lampa)
    Private PredkoscMaksymalna As UShort
    Private KursorDomyslny As Cursor
    Private OknaSterowaniaPociagami As New HashSet(Of wndSterowaniePociagiem)
    Private CzyUsuwacOknaSterowaniaPociagami As Boolean = True

    Private actPokazStatus As Action(Of String, Color, Boolean) = AddressOf PokazStatusPolaczenia
    Private actUkryjDodawaniePociagu As Action = Sub() OknoDodawaniaPociagu?.Close()
    Private actUkryjWybieraniePociagu As Action = Sub() OknoWybieraniaPociagu?.Close()
    Private actUkryjSterowaniePociagami As Action = AddressOf ZamknijOknaSterowaniaPociagami
    Private actUkryjOswietlenie As Action = Sub() OknoOswietlenia?.Close()
    Private actPokazPulpit As Action(Of Zaleznosci.Pulpit) = AddressOf PokazPulpit
    Private actPokazBlad As Action(Of String) = AddressOf PokazBlad
    Private actPokazKomunikat As Action(Of String) = AddressOf PokazKomunikat

    Public Sub New()
        InitializeComponent()
        plpPulpit.TypRysownika = TypRysownika.KlasycznyDirect2D
        plpPulpit.Wysrodkuj()

        KursorDomyslny = plpPulpit.Cursor
    End Sub

    Friend Sub UsunOknoSterowaniaPociagiem(okno As wndSterowaniePociagiem)
        If CzyUsuwacOknaSterowaniaPociagami Then OknaSterowaniaPociagami.Remove(okno)
    End Sub

    Private Sub wndNastawnia_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not Klient.Uruchomiony Then Exit Sub

        If ZadajPytanie("Zamknąć okno? Spowoduje to rozłączenie z serwerem.") = DialogResult.Yes Then
            actUkryjDodawaniePociagu()
            actUkryjWybieraniePociagu()
            actUkryjSterowaniePociagami()
            actUkryjOswietlenie()
            Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
            Dim t As New Thread(AddressOf ZamknijPolaczenie)
            t.Start(Klient)
            Klient = Nothing
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub mnuPolaczZSerwerem_Click() Handles mnuPolaczZSerwerem.Click
        Dim wnd As New wndWyborStacji(Klient)
        WlaczoneOknoWyboruPost = True
        wnd.ShowDialog()
        WlaczoneOknoWyboruPost = False
        If wnd.Pulpit IsNot Nothing Then
            PredkoscMaksymalna = wnd.PredkoscMaksymalnaSieci
            actPokazPulpit(wnd.Pulpit)
            actPokazStatus("Połączono", Color.Green, True)
        End If
    End Sub

    Private Sub mnuRozlaczZSerwerem_Click() Handles mnuRozlaczZSerwerem.Click
        If ZadajPytanie("Czy rozłączyć z serwerem?") = DialogResult.Yes Then
            actUkryjDodawaniePociagu()
            actUkryjWybieraniePociagu()
            actUkryjSterowaniePociagami()
            actUkryjOswietlenie()
            Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
            actPokazStatus("Rozłączanie...", Color.Blue, True)
        End If
    End Sub

    Private Sub mnuZarzadzajSerwerem_Click() Handles mnuZarzadzajSerwerem.Click
        Dim t As New Thread(AddressOf PokazOknoSerwera)
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

    Private Sub mnuKonfiguratorStacji_Click() Handles mnuKonfiguratorStacji.Click
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub

    Private Sub mnuNowePolaczenia_Click() Handles mnuNowePolaczenia.Click
        PokazKomunikat("Plik połączeń należy zapisać w tym samym folderze, w którym znajdują się pliki konfiguracji posterunków ruchu.")
        WczytajPolaczenia(New SaveFileDialog, AddressOf Zaleznosci.PolaczeniaStacji.OtworzFolder, "Nie udało się zapisać pliku.")
    End Sub

    Private Sub mnuOtworzPolaczenia_Click() Handles mnuOtworzPolaczenia.Click
        WczytajPolaczenia(New OpenFileDialog, AddressOf Zaleznosci.PolaczeniaStacji.OtworzPlik, "Nie udało się otworzyć pliku.")
    End Sub

    Private Sub OknoDodawaniaPociagu_FormClosed() Handles OknoDodawaniaPociagu.FormClosed
        OknoDodawaniaPociagu = Nothing
        plpPulpit.MozliwoscZaznaczeniaToru = False
        plpPulpit.MozliwoscWcisnieciaPrzycisku = True
    End Sub

    Private Sub OknoWybieraniaPociagu_FormClosed() Handles OknoWybieraniaPociagu.FormClosed
        If OknoWybieraniaPociagu.WybranyPociagNr.HasValue Then
            Dim okno As New wndSterowaniePociagiem(Klient, Me, OknoWybieraniaPociagu.WybranyPociagNr.Value, OknoWybieraniaPociagu.WybranyPociagNazwa, PredkoscMaksymalna)
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
                Dim stan As Zaleznosci.UstawienieRozjazduEnum
                If roz.Stan = Zaleznosci.UstawienieZwrotnicy.Wprost Then
                    stan = Zaleznosci.UstawienieRozjazduEnum.Bok
                Else
                    stan = Zaleznosci.UstawienieRozjazduEnum.Wprost
                End If
                Klient.WyslijUstawZwrotnice(New Zaleznosci.UstawZwrotnice() With {.Adres = roz.Adres, .Ustawienie = stan})

            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(kostka, Zaleznosci.Przycisk)
                If prz.TypPrzycisku = Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow Then
                    Klient.WyslijZwolnijPrzebiegi(New Zaleznosci.ZwolnijPrzebiegi())
                ElseIf prz.TypPrzycisku = Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy Then

                    If prz.ObslugiwanySygnalizator IsNot Nothing Then
                        Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {
                                                            .Adres = prz.ObslugiwanySygnalizator.Adres,
                                                            .Stan = Zaleznosci.UstawianyStanSygnalizatora.Zastepczy})
                    End If
                End If

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(kostka, Zaleznosci.PrzyciskTor)
                If prz.ObslugiwanySygnalizator IsNot Nothing Then
                    Dim stan As Zaleznosci.UstawianyStanSygnalizatora
                    Select Case prz.TypPrzycisku
                        Case Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny
                            stan = Zaleznosci.UstawianyStanSygnalizatora.Zezwalajacy
                        Case Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy, Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy
                            stan = Zaleznosci.UstawianyStanSygnalizatora.Manewrowy
                    End Select
                    Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {.Adres = prz.ObslugiwanySygnalizator.Adres, .Stan = stan})
                End If

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
            If Not plpPulpit.Pulpit.CzyKostkaNiepusta(akt.WspolrzedneKostki.Konwertuj) Then Continue For

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

    Private Sub WczytajPolaczenia(Okno As FileDialog, MetodaOtwierajaca As Func(Of String, Zaleznosci.PolaczeniaStacji), KomunikatBledu As String)
        Okno.Filter = FILTR_PLIKU

        If Okno.ShowDialog = DialogResult.OK Then
            Dim polaczenia As Zaleznosci.PolaczeniaStacji = MetodaOtwierajaca(Okno.FileName)
            If polaczenia Is Nothing Then
                PokazBlad(KomunikatBledu)
            Else
                Dim wnd As New wndKonfiguratorPolaczen(polaczenia)
                wnd.Show()
            End If
        End If
    End Sub

    Private Sub CzyscOkno()
        Invoke(actPokazStatus, "Rozłączono", Color.Red, False)
        Invoke(actPokazPulpit, New Zaleznosci.Pulpit)
        Invoke(actUkryjDodawaniePociagu)
        Invoke(actUkryjWybieraniePociagu)
        Invoke(actUkryjSterowaniePociagami)
        Invoke(actUkryjOswietlenie)
    End Sub

    Private Sub ZamknijPolaczenie(klient As Object)
        Thread.Sleep(CZEKANIE_NA_ZAMKNIECIE)
        CType(klient, Zaleznosci.KlientTCP).Zakoncz(False)
    End Sub

    Private Sub PokazOknoSerwera()
        Dim wnd As New SerwerSterujacy.wndOknoSerwera
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

    Private Sub PokazPulpit(pulpit As Zaleznosci.Pulpit)
        plpPulpit.Pulpit = pulpit

        Sygnalizatory = pulpit.PobierzSygnalizatory()
        Rozjazdy = pulpit.PobierzRozjazdy()
        Kierunki = pulpit.PobierzKierunkiPoAdresieOdcinka()
        Lampy = pulpit.PobierzLampy()
    End Sub

    Private Sub ZamknijOknaSterowaniaPociagami()
        CzyUsuwacOknaSterowaniaPociagami = False
        For Each okno As wndSterowaniePociagiem In OknaSterowaniaPociagami
            okno.Zamknij()
        Next
        CzyUsuwacOknaSterowaniaPociagami = True

        OknaSterowaniaPociagami.Clear()
    End Sub
End Class