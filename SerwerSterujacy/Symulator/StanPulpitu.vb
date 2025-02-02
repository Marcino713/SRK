Imports System.Net

Friend Class wndStanPulpitu
    Private Const TEKST_POLACZONO As String = "Połączono w trybie obserwatora"
    Private Const TEKST_LACZENIE As String = "Łączenie w trybie obserwatora..."
    Private Const TEKST_ROZLACZONO As String = "Brak połączenia w trybie obserwatora"
    Private Shared ReadOnly KOLOR_POLACZONO As Color = Color.Green
    Private Shared ReadOnly KOLOR_LACZENIE As Color = Color.Blue
    Private Shared ReadOnly KOLOR_ROZLACZONO As Color = Color.Red

    Private oknoSymulatora As wndSymulator
    Private symulator As UrzadzenieSymulator
    Private obslugiwaczPulpitu As Pulpit.ObslugiwaczTrybuDzialania
    Private WithEvents danePulpitu As Zaleznosci.Pulpit
    Private WithEvents klient As New Zaleznosci.KlientTCP

    Private actPokazPoprawnePolaczenie As Action = AddressOf PokazPoprawnePolaczenie
    Private actKoniecKlienta As Action = AddressOf PokazKoniecDzialaniaKlienta

    Friend Sub New(oknoSymulatora As wndSymulator, danePulpitu As Zaleznosci.Pulpit, symulator As UrzadzenieSymulator, rysownik As Pulpit.TypRysownika, konfiguracja As KonfiguracjaSymulatora)
        InitializeComponent()

        Me.oknoSymulatora = oknoSymulatora
        Me.danePulpitu = danePulpitu
        Me.symulator = symulator

        plpPulpit.TypRysownika = rysownik
        plpPulpit.WarunekZaznaczeniaKostki = AddressOf CzyKostkaZaznaczalna

        obslugiwaczPulpitu = New Pulpit.ObslugiwaczTrybuDzialania(plpPulpit, klient, False)
        obslugiwaczPulpitu.PokazPulpit(danePulpitu)

        plpPulpit.Wysrodkuj()

        Text = $"{Text} - {danePulpitu.Nazwa}"
        PokazLaczenie()
        Dim dane As New Zaleznosci.DanePolaczeniaKlienta With {
            .AdresIp = IPAddress.Loopback.ToString(),
            .Port = konfiguracja.Port,
            .Haslo = konfiguracja.HasloObserwatora,
            .Obserwator = True}
        klient.Polacz(dane)
    End Sub

    Friend Sub ZaznaczKostke(kostka As Zaleznosci.Kostka)
        plpPulpit.ZaznaczonaKostka = kostka
    End Sub

    Private Sub wndPulpit_FormClosing() Handles Me.FormClosing
        oknoSymulatora.UsunOknoPulpitu(danePulpitu.Adres)
        danePulpitu = Nothing
        obslugiwaczPulpitu.Czysc()
        klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
        klient = Nothing
    End Sub

    Private Sub plpPulpit_ZarejestrowanoOs(adres As UShort) Handles plpPulpit.ZarejestrowanoOs
        symulator.ZarejestrowanoOs(New Zaleznosci.WykrytoOsUrz() With {.AdresPosterunku = danePulpitu.Adres, .AdresUrzadzenia = adres})
    End Sub

    Private Sub plpPulpit_ZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka) Handles plpPulpit.ZmianaZaznaczeniaKostki
        oknoSymulatora.ZaznaczonoKostke(danePulpitu.Adres, kostka)
    End Sub

    Private Sub klient_KoniecPolaczenia() Handles klient.ZakonczonoPolaczenie, klient.OdebranoZakonczonoDzialanieSerwera, klient.OdebranoZakonczonoSesjeKlienta
        Invoke(actKoniecKlienta)
    End Sub

    Private Sub klient_OdebranoUwierzytelnionoPoprawnie(kom As Zaleznosci.UwierzytelnionoPoprawnie) Handles klient.OdebranoUwierzytelnionoPoprawnie
        klient.WyslijWybierzPosterunek(New Zaleznosci.WybierzPosterunek() With {.Adres = danePulpitu.Adres})
    End Sub

    Private Sub klient_OdebranoWybranoPosterunek(kom As Zaleznosci.WybranoPosterunek) Handles klient.OdebranoWybranoPosterunek
        Invoke(actPokazPoprawnePolaczenie)
    End Sub

    Private Function CzyKostkaZaznaczalna(k As Zaleznosci.Kostka) As Boolean
        Return k Is Nothing OrElse TypeOf k Is Zaleznosci.Sygnalizator OrElse TypeOf k Is Zaleznosci.Rozjazd
    End Function

    Private Sub PokazLaczenie()
        PokazStatus(TEKST_LACZENIE, KOLOR_LACZENIE)
    End Sub

    Private Sub PokazPoprawnePolaczenie()
        PokazStatus(TEKST_POLACZONO, KOLOR_POLACZONO)
    End Sub

    Private Sub PokazKoniecDzialaniaKlienta()
        PokazStatus(TEKST_ROZLACZONO, KOLOR_ROZLACZONO)
    End Sub

    Private Sub PokazStatus(tekst As String, kolor As Color)
        tslStanPolaczenia.Text = tekst
        tslStanPolaczenia.ForeColor = kolor
    End Sub
End Class