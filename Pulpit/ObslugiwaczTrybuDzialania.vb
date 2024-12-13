Public Class ObslugiwaczTrybuDzialania
    Private plpPulpit As PulpitSterowniczy
    Private CzyscKoniec As Boolean
    Private Sygnalizatory As New Dictionary(Of UShort, Zaleznosci.Kostka)
    Private Rozjazdy As New Dictionary(Of UShort, Zaleznosci.Rozjazd)
    Private Kierunki As New Dictionary(Of UShort, Zaleznosci.Kierunek)
    Private Przejazdy As New Dictionary(Of UShort, Zaleznosci.PrzejazdKolejowoDrogowy)
    Private WithEvents Klient As Zaleznosci.KlientTCP

    Private actCzyscPulpit As Action = AddressOf CzyscPulpit

    Public Sub New(plp As PulpitSterowniczy, klientTcp As Zaleznosci.KlientTCP, czyscPoZakonczeniuPolaczenia As Boolean)
        plpPulpit = plp
        Klient = klientTcp
        CzyscKoniec = czyscPoZakonczeniuPolaczenia
    End Sub

    Public Sub PokazPulpit(pulpit As Zaleznosci.Pulpit)
        plpPulpit.Pulpit = pulpit

        Sygnalizatory = pulpit.PobierzSygnalizatory()
        Rozjazdy = pulpit.PobierzRozjazdy()
        Kierunki = pulpit.PobierzKierunkiPoAdresieOdcinka()
        Przejazdy = pulpit.PobierzPrzejazdyKolejowoDrogowe()

        plpPulpit.InicjalizujMigacz()
    End Sub

    Public Sub Czysc()
        Klient = Nothing
    End Sub

    Private Sub Klient_Koniec() Handles Klient.ZakonczonoPolaczenie, Klient.OdebranoZakonczonoSesjeKlienta, Klient.OdebranoZakonczonoDzialanieSerwera
        plpPulpit.Invoke(actCzyscPulpit)
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

            Case Zaleznosci.TypKostki.SygnalizatorPowtarzajacy, Zaleznosci.TypKostki.SygnalizatorOstrzegawczy
                Dim s As Zaleznosci.SygnalizatorInformujacy = DirectCast(sygn, Zaleznosci.SygnalizatorInformujacy)
                If kom.Stan = Zaleznosci.StanSygnalizatora.Zezwalajacy Then
                    s.Stan = Zaleznosci.StanSygnalizatoraInformujacego.Zezwalajacy
                Else
                    s.Stan = Zaleznosci.StanSygnalizatoraInformujacego.BrakWyjazdu
                End If
        End Select

        plpPulpit.Invalidate()
    End Sub

    Private Sub Klient_OdebranoZmienionoStanToru(kom As Zaleznosci.ZmienionoStanToru) Handles Klient.OdebranoZmienionoStanToru
        For i As Integer = 0 To kom.Tory.Length - 1
            Dim akt As Zaleznosci.AktualizowanyKawalekToru = kom.Tory(i)
            If Not plpPulpit.Pulpit.CzyKostkaNiepusta(akt.WspolrzedneKostki) Then Continue For

            Dim k As Zaleznosci.Kostka = plpPulpit.Pulpit.Kostki(akt.WspolrzedneKostki.X, akt.WspolrzedneKostki.Y)
            If akt.Polozenie = Zaleznosci.PolozenieToru.TorDrugi Then
                Dim torPodw As Zaleznosci.TorPodwojny = TryCast(k, Zaleznosci.TorPodwojny)
                If torPodw IsNot Nothing Then torPodw.ZajetoscDrugi = akt.Zajetosc
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

    Private Sub CzyscPulpit()
        If Not CzyscKoniec Then Exit Sub

        plpPulpit.UsunMigacz()
        PokazPulpit(New Zaleznosci.Pulpit)
    End Sub
End Class