Imports System.Net
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Threading

Public Class SerwerTCP
    Inherits ZarzadzanieTCP

    Private ReadOnly MIN_LICZBA_PIERWSZA As New BigInteger(10000)

    Private KlienciPoAdresieStacji As New Dictionary(Of UShort, PolaczenieTCP)
    Private WszyscyKlienci As New List(Of PolaczenieTCP)
    Private Serwer As TcpListener
    Private WatekSerwera As Thread
    Private Koniec As Boolean = False
    Private Haslo As String

    Public Event OdebranoZakonczDzialanieKlienta(post As UShort, kom As ZakonczDzialanieKlienta)
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
            Sub(pol, kom) RaiseEvent OdebranoZakonczDzialanieKlienta(pol.AdresStacji, CType(kom, ZakonczDzialanieKlienta))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_JASNOSC_LAMP, New PrzetwOdebrKomunikatu(
            AddressOf UstawJasnoscLamp.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawJasnoscLamp(pol.AdresStacji, CType(kom, UstawJasnoscLamp))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf UstawKierunek.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawKierunek(pol.AdresStacji, CType(kom, UstawKierunek))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_POCZATKOWA_ZAJETOSC_TORU, New PrzetwOdebrKomunikatu(
            AddressOf UstawPoczatkowaZajetoscToru.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawPoczatkowaZajetoscToru(pol.AdresStacji, CType(kom, UstawPoczatkowaZajetoscToru))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_PREDKOSC_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf UstawPredkoscPociagu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawPredkoscPociagu(pol.AdresStacji, CType(kom, UstawPredkoscPociagu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_STAN_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf UstawStanSygnalizatora.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawStanSygnalizatora(pol.AdresStacji, CType(kom, UstawStanSygnalizatora))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_ZWROTNICE, New PrzetwOdebrKomunikatu(
            AddressOf UstawZwrotnice.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawZwrotnice(pol.AdresStacji, CType(kom, UstawZwrotnice))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.UWIERZYTELNIJ_SIE, New PrzetwOdebrKomunikatu(
            AddressOf UwierzytelnijSie.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUwierzytelnijSie(pol.AdresStacji, CType(kom, UwierzytelnijSie))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYBIERZ_POSTERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf WybierzPosterunek.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoWybierzPosterunek(pol.AdresStacji, CType(kom, WybierzPosterunek))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZWOLNIJ_PRZEBIEGI, New PrzetwOdebrKomunikatu(
            AddressOf ZwolnijPrzebiegi.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZwolnijPrzebiegi(pol.AdresStacji, CType(kom, ZwolnijPrzebiegi))
        ))
    End Sub

    Public Sub WyslijInformacje(post As UShort, kom As Informacja)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZakonczonoDzialanieSerwera(kom As ZakonczonoDzialanieSerwera)
        For Each k As PolaczenieTCP In WszyscyKlienci
            k.WyslijKomunikat(kom)
        Next
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

    Public Function UruchomSerwer(port As UShort, haslo As String) As Boolean
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

        Return True
    End Function

    Public Sub Zakoncz()
        Koniec = True
        Serwer?.Stop()
        Serwer = Nothing
    End Sub

    Private Sub AkceptujPolaczenia()
        Do Until Koniec
            Try
                Dim tcp As TcpClient = Serwer.AcceptTcpClient()
                Dim klient As New PolaczenieTCP(Me, tcp)
                WszyscyKlienci.Add(klient)
            Catch
            End Try
        Loop
    End Sub

    Private Sub WyslijDoKlienta(post As UShort, kom As Komunikat)
        Dim pol As PolaczenieTCP = Nothing
        If KlienciPoAdresieStacji.TryGetValue(post, pol) Then
            pol.WyslijKomunikat(kom)
        End If
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

End Class