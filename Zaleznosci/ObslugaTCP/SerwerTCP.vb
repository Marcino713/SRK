Imports System.Net
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Threading

Public Class SerwerTCP
    Inherits ZarzadzanieTCP

    Private ReadOnly MIN_LICZBA_PIERWSZA As New BigInteger(10000)
    Private ReadOnly ZAMYKANIE_NIEAKTYWNYCH_CZAS_WYBOR_POSTERUNKU As TimeSpan = TimeSpan.FromMilliseconds(300000)
    Private ReadOnly ZAMYKANIE_NIEAKTYWNYCH_CZAS_INICJALIZACJA As TimeSpan = TimeSpan.FromMilliseconds(30000)
    Private ReadOnly ZAMYKANIE_NIEKATYWNYCH_SPANIE As TimeSpan = TimeSpan.FromMilliseconds(1000)
    Private ReadOnly ZAMYKANIE_NIEAKTYWNYCH_LICZBA_OKRAZEN_PETLI As Integer = 30

    Private _Uruchomiony As Boolean = False
    Public ReadOnly Property Uruchomiony As Boolean
        Get
            Return _Uruchomiony
        End Get
    End Property

    Public ReadOnly Property CzyWczytanoPosterunki As Boolean
        Get
            Return Posterunki IsNot Nothing AndAlso Posterunki.Length > 0
        End Get
    End Property

    Private Posterunki As ObslugiwanyPosterunek()
    Private KlienciPoAdresieStacji As New Dictionary(Of UShort, ObslugiwanyPosterunek)
    Private WszyscyKlienci As New List(Of PolaczenieTCP)
    Private Serwer As TcpListener
    Private WatekSerwera As Thread
    Private WatekZamykaniaPolaczen As Thread
    Private Koniec As Boolean = False
    Private Haslo As String
    Private slockRezerwacjaPosterunku As New Object
    Private slockListaPolaczen As New Object

    Public Event UniewaznionoListePosterunkow()
    Public Event ZmianaCzasuPodlaczenia(post As String, dataPodlaczenia As String)
    Public Event OdebranoUstawJasnoscLamp(post As UShort, kom As UstawJasnoscLamp)
    Public Event OdebranoUstawKierunek(post As UShort, kom As UstawKierunek)
    Public Event OdebranoUstawPoczatkowaZajetoscToru(post As UShort, kom As UstawPoczatkowaZajetoscToru)
    Public Event OdebranoUstawPredkoscPociagu(post As UShort, kom As UstawPredkoscPociagu)
    Public Event OdebranoUstawStanSygnalizatora(post As UShort, kom As UstawStanSygnalizatora)
    Public Event OdebranoUstawZwrotnice(post As UShort, kom As UstawZwrotnice)
    Public Event OdebranoUwierzytelnijSie(post As UShort, kom As UwierzytelnijSie)
    Public Event OdebranoWybierzPosterunek(post As UShort, kom As WybierzPosterunek)
    Public Event OdebranoZwolnijPrzebiegi(post As UShort, kom As ZwolnijPrzebiegi)

    Public Sub New()
        DaneFabrykiObiektow.Add(TypKomunikatu.DH_INICJALIZUJ, New PrzetwOdebrKomunikatu(
            AddressOf DHInicjalizuj.Otworz,
            AddressOf PrzetworzDH
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZ_DZIALANIE_KLIENTA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczDzialanieKlienta.Otworz,
            AddressOf ZakDzialanieKlienta
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_JASNOSC_LAMP, New PrzetwOdebrKomunikatu(
            AddressOf UstawJasnoscLamp.Otworz,
            Sub(pol, kom)
                Dim k As UstawJasnoscLamp = CType(kom, UstawJasnoscLamp)
                Dim adr(k.Jasnosci.Length - 1) As UShort
                For i As Integer = 0 To k.Jasnosci.Length - 1
                    adr(i) = k.Jasnosci(i).Adres
                Next
                pol.WyslijKomunikat(New ZmienionoJasnoscLamp() With {.Adresy = adr})
                'RaiseEvent OdebranoUstawJasnoscLamp(pol.AdresStacji, CType(kom, UstawJasnoscLamp))
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf UstawKierunek.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawKierunek(pol.AdresStacji, CType(kom, UstawKierunek))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_POCZATKOWA_ZAJETOSC_TORU, New PrzetwOdebrKomunikatu(
            AddressOf UstawPoczatkowaZajetoscToru.Otworz,
            Sub(pol, kom)
                Dim k As UstawPoczatkowaZajetoscToru = CType(kom, UstawPoczatkowaZajetoscToru)
                Dim akt(0) As AktualizowanyKawalekToru
                akt(0) = New AktualizowanyKawalekToru() With
                    {.Polozenie = PolozenieToru.TorGlowny, .Zajetosc = ZajetoscToru.Zajety, .WspolrzedneKostki = k.WspolrzedneKostki}
                pol.WyslijKomunikat(New ZmienionoStanToru() With {.Tory = akt})
                RaiseEvent OdebranoUstawPoczatkowaZajetoscToru(pol.AdresStacji, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_PREDKOSC_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf UstawPredkoscPociagu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawPredkoscPociagu(pol.AdresStacji, CType(kom, UstawPredkoscPociagu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_STAN_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf UstawStanSygnalizatora.Otworz,
            Sub(pol, kom)
                Dim k As UstawStanSygnalizatora = CType(kom, UstawStanSygnalizatora)
                Dim st As StanSygnalizatora
                Select Case k.Stan
                    Case UstawianyStanSygnalizatora.Manewrowy
                        st = StanSygnalizatora.Manewrowy
                    Case UstawianyStanSygnalizatora.Zastepczy
                        st = StanSygnalizatora.Zastepczy
                    Case UstawianyStanSygnalizatora.Zezwalajacy
                        st = StanSygnalizatora.Zezwalajacy
                End Select
                pol.WyslijKomunikat(New ZmienionoStanSygnalizatora() With {.Adres = k.Adres, .Stan = st})
                RaiseEvent OdebranoUstawStanSygnalizatora(pol.AdresStacji, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_ZWROTNICE, New PrzetwOdebrKomunikatu(
            AddressOf UstawZwrotnice.Otworz,
            Sub(pol, kom)
                Dim k As UstawZwrotnice = CType(kom, UstawZwrotnice)
                pol.WyslijKomunikat(New ZmienionoStanZwrotnicy() With {.Adres = k.Adres, .Stan = If(k.Ustawienie = UstawienieRozjazduEnum.Wprost, UstawienieZwrotnicy.Wprost, UstawienieZwrotnicy.Bok)})
                RaiseEvent OdebranoUstawZwrotnice(pol.AdresStacji, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.UWIERZYTELNIJ_SIE, New PrzetwOdebrKomunikatu(
            AddressOf UwierzytelnijSie.Otworz,
            AddressOf UwierzytelnijIWyslijPosterunki
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYBIERZ_POSTERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf WybierzPosterunek.Otworz,
            AddressOf ZerezerwujPosterunek
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZWOLNIJ_PRZEBIEGI, New PrzetwOdebrKomunikatu(
            AddressOf ZwolnijPrzebiegi.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZwolnijPrzebiegi(pol.AdresStacji, CType(kom, ZwolnijPrzebiegi))
        ))
    End Sub

    Public Sub WyslijInformacje(post As UShort, kom As Informacja)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijNadanoNumerPociagu(post As UShort, kom As NadanoNumerPociagu)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijNieuwierzytelniono(post As UShort, kom As Nieuwierzytelniono)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijUwierzytelnionoPoprawnie(post As UShort, kom As UwierzytelnionoPoprawnie)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijWybranoPosterunek(post As UShort, kom As WybranoPosterunek)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZakonczonoSesjeKlienta(post As UShort, kom As ZakonczonoSesjeKlienta)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZazadanoUstawieniaKierunku(post As UShort, kom As ZazadanoUstawieniaKierunku)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoJasnoscLamp(post As UShort, kom As ZmienionoJasnoscLamp)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoKierunek(post As UShort, kom As ZmienionoKierunek)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoPredkoscMaksymalna(post As UShort, kom As ZmienionoPredkoscMaksymalna)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoPredkoscPociagu(post As UShort, kom As ZmienionoPredkoscPociagu)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoStanSygnalizatora(post As UShort, kom As ZmienionoStanSygnalizatora)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoStanToru(post As UShort, kom As ZmienionoStanToru)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoStanZwrotnicy(post As UShort, kom As ZmienionoStanZwrotnicy)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Function Uruchom(port As UShort, haslo As String) As Boolean
        If Serwer IsNot Nothing Or _Uruchomiony Or Not CzyWczytanoPosterunki Then Return False

        Koniec = False
        Me.Haslo = haslo

        Try
            Serwer = New TcpListener(IPAddress.Any, port)
            Serwer.Start()
        Catch
            Return False
        End Try

        WatekSerwera = New Thread(AddressOf AkceptujPolaczenia)
        WatekSerwera.Start()
        WatekZamykaniaPolaczen = New Thread(AddressOf ZamykajNieaktywnePolaczenia)
        WatekZamykaniaPolaczen.Start()

        _Uruchomiony = True

        Return True
    End Function

    Public Sub Zatrzymaj()
        Koniec = True
        Serwer?.Stop()
        Serwer = Nothing
        WatekSerwera?.Join()

        Dim kom As New ZakonczonoDzialanieSerwera() With {.Przyczyna = PrzyczynaZakonczeniaDzialaniaSerwera.ZatrzymanieSerwera}

        Dim polaczenia As PolaczenieTCP()
        SyncLock slockListaPolaczen
            polaczenia = WszyscyKlienci.ToArray()
            WszyscyKlienci.Clear()
        End SyncLock

        For Each k As PolaczenieTCP In polaczenia
            If k.Stan = StanPolaczenia.UstalanieKluczaSzyfrujacego Or k.Stan = StanPolaczenia.UwierzytelnianieHaslem Then
                k.Zakoncz(False)
            Else
                k.WyslijKomunikat(kom)
                k.Zakoncz(True)
            End If
        Next

        For Each obs As ObslugiwanyPosterunek In KlienciPoAdresieStacji.Values
            obs.Polaczenie = Nothing
        Next
        RaiseEvent UniewaznionoListePosterunkow()

        _Uruchomiony = False
    End Sub

    Public Function WczytajPolaczenie(SciezkaPliku As String) As Boolean
        If Uruchomiony Then Return False

        KlienciPoAdresieStacji = New Dictionary(Of UShort, ObslugiwanyPosterunek)
        Dim PosterunkiLista As New List(Of ObslugiwanyPosterunek)

        Dim polaczenia As PolaczeniaStacji = PolaczeniaStacji.OtworzPlik(SciezkaPliku, False)

        If polaczenia IsNot Nothing Then
            For Each pol As LaczonyPlikStacji In polaczenia.LaczanePliki
                Dim obs As New ObslugiwanyPosterunek() With {
                                       .NazwaPosterunku = pol.NazwaPosterunku,
                                       .NazwaPliku = pol.NazwaPliku,
                                       .Adres = pol.Adres,
                                       .Zawartosc = pol.ZawartoscPosterunku}

                KlienciPoAdresieStacji.Add(pol.Adres, obs)
                PosterunkiLista.Add(obs)
            Next

        End If

        Posterunki = PosterunkiLista.ToArray

        RaiseEvent UniewaznionoListePosterunkow()
        Return CzyWczytanoPosterunki
    End Function

    Public Sub ZakonczPolaczenie(adres As UShort)
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim pol As PolaczenieTCP = Nothing

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresieStacji.TryGetValue(adres, obs) Then
                pol = obs.Polaczenie
            End If
        End SyncLock

        If pol IsNot Nothing Then
            Dim kom As New ZakonczonoSesjeKlienta() With {.Przyczyna = PrzyczynaZakonczeniaSesjiKlienta.RozlaczeniePrzezSerwer}
            pol.WyslijKomunikat(kom)
            pol.Zakoncz(True)
        End If
    End Sub

    Public Function PobierzStanPolaczen() As StanObslugiwanegoPosterunku()
        If Posterunki Is Nothing Then Return Nothing

        Dim lista As New List(Of StanObslugiwanegoPosterunku)

        For Each obs As ObslugiwanyPosterunek In Posterunki
            Dim dataPodlaczenia As String = ""
            Dim ostatnieZapytanie As String = ""

            If obs.Polaczenie IsNot Nothing Then
                dataPodlaczenia = obs.Polaczenie.CzasNawiazaniaPolaczenia.ToString(FORMAT_DATY)
                ostatnieZapytanie = obs.Polaczenie.OstatnieZapytanie.ToString(FORMAT_DATY)
            End If

            lista.Add(New StanObslugiwanegoPosterunku With {
                      .NazwaPosterunku = obs.NazwaPosterunku,
                      .NazwaPliku = obs.NazwaPliku,
                      .Adres = obs.Adres.ToString,
                      .DataPodlaczenia = dataPodlaczenia,
                      .OstatnieZapytanie = ostatnieZapytanie
            })
        Next

        Return lista.ToArray()
    End Function

    Friend Overrides Sub PrzetworzZakonczeniePolaczenia(pol As PolaczenieTCP)
        If Koniec Then Exit Sub

        SyncLock slockListaPolaczen
            WszyscyKlienci.Remove(pol)
        End SyncLock

        RozlaczPosterunek(pol.AdresStacji)
    End Sub

    Private Sub AkceptujPolaczenia()
        Do Until Koniec
            Try
                Dim tcp As TcpClient = Serwer.AcceptTcpClient()
                Dim klient As New PolaczenieTCP(Me, tcp)

                SyncLock slockListaPolaczen
                    WszyscyKlienci.Add(klient)
                End SyncLock
            Catch
            End Try
        Loop
    End Sub

    Private Sub ZamykajNieaktywnePolaczenia()
        Dim kom As New ZakonczonoSesjeKlienta() With {.Przyczyna = PrzyczynaZakonczeniaSesjiKlienta.PrzekroczenieCzasuOczekiwania}
        Dim polaczeniaDoZamkniecia As List(Of PolaczenieTCP) = Nothing
        Dim czas As Date
        Dim ix As Integer

        Do Until Koniec
            Try
                ix = 0
                Do Until Koniec Or ix >= ZAMYKANIE_NIEAKTYWNYCH_LICZBA_OKRAZEN_PETLI
                    Thread.Sleep(ZAMYKANIE_NIEKATYWNYCH_SPANIE)
                    ix += 1
                Loop
                If Koniec Then Exit Do

                polaczeniaDoZamkniecia = Nothing
                czas = Now

                SyncLock slockListaPolaczen
                    For Each pol As PolaczenieTCP In WszyscyKlienci
                        If _
                          ((pol.Stan = StanPolaczenia.UstalanieKluczaSzyfrujacego Or pol.Stan = StanPolaczenia.UwierzytelnianieHaslem) And pol.OstatnieZapytanie + ZAMYKANIE_NIEAKTYWNYCH_CZAS_INICJALIZACJA < czas) Or
                          (pol.Stan = StanPolaczenia.WyborPosterunku And pol.OstatnieZapytanie + ZAMYKANIE_NIEAKTYWNYCH_CZAS_WYBOR_POSTERUNKU < czas) Then
                            If polaczeniaDoZamkniecia Is Nothing Then polaczeniaDoZamkniecia = New List(Of PolaczenieTCP)
                            polaczeniaDoZamkniecia.Add(pol)
                        End If
                    Next
                End SyncLock

                If polaczeniaDoZamkniecia Is Nothing Then Continue Do

                For Each pol As PolaczenieTCP In polaczeniaDoZamkniecia
                    Dim czekaj As Boolean = False
                    If pol.Stan = StanPolaczenia.WyborPosterunku Then
                        pol.WyslijKomunikat(kom)
                        czekaj = True
                    End If
                    pol.Zakoncz(czekaj)
                Next
            Catch
            End Try
        Loop
    End Sub

    Private Sub WyslijDoKlienta(post As UShort, kom As Komunikat)
        Dim pol As ObslugiwanyPosterunek = Nothing

        SyncLock slockRezerwacjaPosterunku
            KlienciPoAdresieStacji.TryGetValue(post, pol)
        End SyncLock

        If pol IsNot Nothing Then pol.Polaczenie?.WyslijKomunikat(kom)
    End Sub

    Private Sub PrzetworzDH(pol As PolaczenieTCP, kom As Komunikat)
        Dim dh As DHInicjalizuj = CType(kom, DHInicjalizuj)
        If dh.LiczbaP < MIN_LICZBA_PIERWSZA Then Exit Sub

        Dim liczbaPrywB As BigInteger = PobierzDuzaLiczbe()
        Dim dhOdp As New DHZainicjalizowano
        dhOdp.LiczbaB = BigInteger.ModPow(New BigInteger(dh.LiczbaG), liczbaPrywB, dh.LiczbaP)

        Dim klucz As BigInteger = BigInteger.ModPow(dh.LiczbaA, liczbaPrywB, dh.LiczbaP)
        pol.InicjujAes(klucz.ToByteArray)

        pol.WyslijKomunikat(dhOdp)
    End Sub

    Private Sub UwierzytelnijIWyslijPosterunki(pol As PolaczenieTCP, kom As Komunikat)
        Dim uw As UwierzytelnijSie = CType(kom, UwierzytelnijSie)
        If uw.Haslo = Haslo Then
            pol.UstawStanWyborPosterunku()

            Dim postLista As New List(Of DanePosterunku)
            For Each op As ObslugiwanyPosterunek In Posterunki
                postLista.Add(New DanePosterunku() With {.Adres = op.Adres, .Nazwa = op.NazwaPosterunku, .Stan = If(op.Polaczenie Is Nothing, StanPosterunku.Wolny, StanPosterunku.Zajety)})
            Next
            pol.WyslijKomunikat(New UwierzytelnionoPoprawnie() With {.Posterunki = postLista.ToArray})

        Else
            pol.WyslijKomunikat(New Nieuwierzytelniono())
        End If
    End Sub

    Private Sub ZerezerwujPosterunek(pol As PolaczenieTCP, kom As Komunikat)
        Dim k As WybierzPosterunek = CType(kom, WybierzPosterunek)
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim odp As Komunikat
        Dim dataPodl As String = Nothing

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresieStacji.TryGetValue(k.Adres, obs) AndAlso obs.Polaczenie Is Nothing Then
                obs.Polaczenie = pol
                pol.AdresStacji = k.Adres
                odp = New WybranoPosterunek() With {.Stan = StanUstawianegoPosterunku.WybranoPoprawnie, .ZawartoscPliku = obs.Zawartosc}
                dataPodl = pol.CzasNawiazaniaPolaczenia.ToString(FORMAT_DATY)
                pol.UstawStanSterowanieRuchem()
            Else
                odp = New WybranoPosterunek() With {.Stan = StanUstawianegoPosterunku.PosterunekZajety}
            End If
        End SyncLock

        pol.WyslijKomunikat(odp)
        If dataPodl IsNot Nothing Then
            RaiseEvent ZmianaCzasuPodlaczenia(obs.Adres.ToString, dataPodl)
        End If
    End Sub

    Private Sub RozlaczPosterunek(adres As UShort)
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim adr As String = Nothing

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresieStacji.TryGetValue(adres, obs) AndAlso obs.Polaczenie IsNot Nothing Then
                obs.Polaczenie = Nothing
                adr = adres.ToString
            End If
        End SyncLock

        If adr IsNot Nothing Then RaiseEvent ZmianaCzasuPodlaczenia(adr, "")
    End Sub

    Private Sub ZakDzialanieKlienta(pol As PolaczenieTCP, kom As Komunikat)
        RozlaczPosterunek(pol.AdresStacji)
        pol.WyslijKomunikat(New ZakonczonoSesjeKlienta() With {.Przyczyna = PrzyczynaZakonczeniaSesjiKlienta.RozlaczenieKlienta})
        pol.Zakoncz(True)
    End Sub

End Class