Imports System.Net
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Threading

Public Class KlientTCP
    Inherits ZarzadzanieTCP

    Private Klient As PolaczenieTCP
    Private AdresIP As String
    Private Port As UShort
    Private Haslo As String
    Private DaneDH As KlientDaneDH

    Public Event BladPolaczenia()
    Public Event OdebranoInformacje(kom As Informacja)
    Public Event OdebranoZakonczonoDzialanieSerwera(kom As ZakonczonoDzialanieSerwera)
    Public Event OdebranoNadanoNumerPociagu(kom As NadanoNumerPociagu)
    Public Event OdebranoNieuwierzytelniono(kom As Nieuwierzytelniono)
    Public Event OdebranoUwierzytelnionoPoprawnie(kom As UwierzytelnionoPoprawnie)
    Public Event OdebranoWybranoPosterunek(kom As WybranoPosterunek)
    Public Event OdebranoZakonczonoSesjeKlienta(kom As ZakonczonoSesjeKlienta)
    Public Event OdebranoZazadanoUstawieniaKierunku(kom As ZazadanoUstawieniaKierunku)
    Public Event OdebranoZmienionoJasnoscLamp(kom As ZmienionoJasnoscLamp)
    Public Event OdebranoZmienionoKierunek(kom As ZmienionoKierunek)
    Public Event OdebranoZmienionoPredkoscMaksymalna(kom As ZmienionoPredkoscMaksymalna)
    Public Event OdebranoZmienionoPredkoscPociagu(kom As ZmienionoPredkoscPociagu)
    Public Event OdebranoZmienionoStanSygnalizatora(kom As ZmienionoStanSygnalizatora)
    Public Event OdebranoZmienionoStanToru(kom As ZmienionoStanToru)
    Public Event OdebranoZmienionoStanZwrotnicy(kom As ZmienionoStanZwrotnicy)

    Public Sub New()
        DaneFabrykiObiektow.Add(TypKomunikatu.DH_ZAINICJALIZOWANO, New PrzetwOdebrKomunikatu(
            AddressOf DHZainicjalizowano.Otworz,
            AddressOf PrzetworzDH
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.INFORMACJA, New PrzetwOdebrKomunikatu(
            AddressOf Informacja.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoInformacje(CType(kom, Informacja))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZONO_DZIALANIE_SERWERA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczonoDzialanieSerwera.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZakonczonoDzialanieSerwera(CType(kom, ZakonczonoDzialanieSerwera))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.NADANO_NUMER_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf NadanoNumerPociagu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoNadanoNumerPociagu(CType(kom, NadanoNumerPociagu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.NIEUWIERZYTELNIONO, New PrzetwOdebrKomunikatu(
            AddressOf Nieuwierzytelniono.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoNieuwierzytelniono(CType(kom, Nieuwierzytelniono))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.UWIERZYTELNIONO_POPRAWNIE, New PrzetwOdebrKomunikatu(
            AddressOf UwierzytelnionoPoprawnie.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUwierzytelnionoPoprawnie(CType(kom, UwierzytelnionoPoprawnie))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYBRANO_POSTERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf WybranoPosterunek.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoWybranoPosterunek(CType(kom, WybranoPosterunek))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZONO_SESJE_KLIENTA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczonoSesjeKlienta.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZakonczonoSesjeKlienta(CType(kom, ZakonczonoSesjeKlienta))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAZADANO_USTAWIENIA_KIERUNKU, New PrzetwOdebrKomunikatu(
            AddressOf ZazadanoUstawieniaKierunku.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZazadanoUstawieniaKierunku(CType(kom, ZazadanoUstawieniaKierunku))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_JASNOSC_LAMP, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoJasnoscLamp.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoJasnoscLamp(CType(kom, ZmienionoJasnoscLamp))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoKierunek.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoKierunek(CType(kom, ZmienionoKierunek))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_PREDKOSC_MAKSYMALNA, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoPredkoscMaksymalna.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoPredkoscMaksymalna(CType(kom, ZmienionoPredkoscMaksymalna))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_PREDKOSC_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoPredkoscPociagu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoPredkoscPociagu(CType(kom, ZmienionoPredkoscPociagu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_STAN_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoStanSygnalizatora.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoStanSygnalizatora(CType(kom, ZmienionoStanSygnalizatora))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_STAN_TORU, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoStanToru.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoStanToru(CType(kom, ZmienionoStanToru))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_STAN_ZWROTNICY, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoStanZwrotnicy.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoStanZwrotnicy(CType(kom, ZmienionoStanZwrotnicy))
        ))
    End Sub

    Public Sub WyslijZakonczDzialanieKlienta(kom As ZakonczDzialanieKlienta)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUstawJasnoscLamp(kom As UstawJasnoscLamp)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUstawKierunek(kom As UstawKierunek)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUstawPoczatkowaZajetoscToru(kom As UstawPoczatkowaZajetoscToru)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUstawPredkoscPociagu(kom As UstawPredkoscPociagu)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUstawStanSygnalizatora(kom As UstawStanSygnalizatora)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUstawZwrotnice(kom As UstawZwrotnice)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijUwierzytelnijSie(kom As UwierzytelnijSie)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijWybierzPosterunek(kom As WybierzPosterunek)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub WyslijZwolnijPrzebiegi(kom As ZwolnijPrzebiegi)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Public Sub Polacz(AdresIp As String, Port As UShort, Haslo As String)
        Me.AdresIP = AdresIp
        Me.Port = Port
        Me.Haslo = Haslo

        Dim t As New Thread(AddressOf PolaczZSerwerem)
        t.Start()
    End Sub

    Public Sub Zakoncz()
        Dim k As PolaczenieTCP = Klient
        Klient = Nothing
        k?.Zakoncz()
    End Sub

    Private Sub PolaczZSerwerem()
        Try
            Dim tcp As New TcpClient()
            tcp.Connect(New IPEndPoint(IPAddress.Parse(AdresIP), Port))
            Klient = New PolaczenieTCP(Me, tcp)

            DaneDH = New KlientDaneDH()
            Dim kom As DHInicjalizuj = DaneDH
            Klient.WyslijKomunikat(kom)
        Catch
            RaiseEvent BladPolaczenia()
        End Try
    End Sub

    Private Sub PrzetworzDH(pol As PolaczenieTCP, kom As Komunikat)
        Dim dh As DHZainicjalizowano = CType(kom, DHZainicjalizowano)
        Dim klucz As BigInteger = BigInteger.ModPow(dh.LiczbaB, DaneDH.LiczbaPrywA, DaneDH.LiczbaP)
        DaneDH = Nothing
        Klient.InicjujAes(klucz.ToByteArray)
    End Sub

End Class