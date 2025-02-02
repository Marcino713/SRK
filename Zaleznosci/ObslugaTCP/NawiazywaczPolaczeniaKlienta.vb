Imports System.Net
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Threading

Friend Class NawiazywaczPolaczeniaKlienta
    Inherits ZarzadzanieTCP

    Private Dane As DanePolaczeniaKlienta
    Private Klient As PolaczenieTCP
    Private DaneDH As KlientDaneDH
    Private WatekNawiazywaniaPolaczenia As Thread
    Private Rozlacz As Boolean

    Public Event BladNawiazywaniaPolaczenia()
    Public Event OdebranoNieuwierzytelniono(kom As Nieuwierzytelniono)
    Public Event OdebranoZakonczonoDzialanieSerwera(pol As PolaczenieTCP, kom As ZakonczonoDzialanieSerwera)
    Public Event OdebranoZakonczonoSesjeKlienta(pol As PolaczenieTCP, kom As ZakonczonoSesjeKlienta)
    Public Event Polaczono(pol As PolaczenieTCP, kom As UwierzytelnionoPoprawnie)

    Friend Sub New(dane As DanePolaczeniaKlienta)
        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZONO_DZIALANIE_SERWERA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczonoDzialanieSerwera.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZakonczonoDzialanieSerwera(pol, CType(kom, ZakonczonoDzialanieSerwera))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZONO_SESJE_KLIENTA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczonoSesjeKlienta.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZakonczonoSesjeKlienta(pol, CType(kom, ZakonczonoSesjeKlienta))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.DH_ZAINICJALIZOWANO, New PrzetwOdebrKomunikatu(
            AddressOf DHZainicjalizowano.Otworz,
            AddressOf PrzetworzDH
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.NIEUWIERZYTELNIONO, New PrzetwOdebrKomunikatu(
            AddressOf Nieuwierzytelniono.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoNieuwierzytelniono(CType(kom, Nieuwierzytelniono))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.UWIERZYTELNIONO_POPRAWNIE, New PrzetwOdebrKomunikatu(
            AddressOf UwierzytelnionoPoprawnie.Otworz,
            AddressOf PoprUwierzytelniono
        ))

        Me.Dane = dane
        WatekNawiazywaniaPolaczenia = New Thread(AddressOf PolaczZSerwerem)
        WatekNawiazywaniaPolaczenia.Start()
    End Sub

    Friend Overrides Sub PrzetworzZakonczeniePolaczenia(pol As PolaczenieTCP)
        RaiseEvent BladNawiazywaniaPolaczenia()
    End Sub

    Friend Sub Zakoncz()
        Rozlacz = True
        Klient?.WyslijKomunikat(New ZakonczDzialanieKlienta() With {.Przyczyna = PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
        Klient?.Zakoncz(True)
    End Sub

    Private Sub PolaczZSerwerem()
        Try
            Dim tcp As New TcpClient()
            tcp.Connect(New IPEndPoint(IPAddress.Parse(Dane.AdresIp), Dane.Port))

            If Rozlacz Then Exit Sub

            Klient = New PolaczenieTCP(Me, tcp) With {
                .TrybObserwatora = Dane.Obserwator
            }

            DaneDH = New KlientDaneDH()
            Dim kom As DHInicjalizuj = DaneDH
            Klient.WyslijKomunikat(kom)
        Catch
            RaiseEvent BladNawiazywaniaPolaczenia()
        End Try
    End Sub

    Private Sub PrzetworzDH(pol As PolaczenieTCP, kom As Komunikat)
        Dim dh As DHZainicjalizowano = CType(kom, DHZainicjalizowano)
        Dim klucz As BigInteger = BigInteger.ModPow(dh.LiczbaB, DaneDH.LiczbaPrywA, DaneDH.LiczbaP)
        DaneDH = Nothing
        Klient.InicjujAes(klucz.ToByteArray)
        Klient.WyslijKomunikat(New UwierzytelnijSie() With {.Haslo = Dane.Haslo, .TrybObserwatora = pol.TrybObserwatora})
    End Sub

    Private Sub PoprUwierzytelniono(pol As PolaczenieTCP, kom As Komunikat)
        pol.UstawStanWyborPosterunku()
        RaiseEvent Polaczono(Klient, CType(kom, UwierzytelnionoPoprawnie))
    End Sub
End Class