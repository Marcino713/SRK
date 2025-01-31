﻿Imports System.Net
Imports System.Net.Sockets
Imports System.Numerics
Imports System.Threading

Public Class KlientTCP
    Inherits ZarzadzanieTCP

    Private _Uruchomiony As Boolean = False
    Public ReadOnly Property Uruchomiony As Boolean
        Get
            Return _Uruchomiony
        End Get
    End Property

    Private Klient As PolaczenieTCP
    Private AdresIP As String
    Private Port As UShort
    Private Haslo As String
    Private NumerKomunikatu As Integer
    Private DaneDH As KlientDaneDH
    Private WatekNawiazywaniaPolaczenia As Thread

    Public Event BladNawiazywaniaPolaczenia()
    Public Event ZakonczonoPolaczenie()
    Public Event OdebranoInformacje(kom As Informacja)
    Public Event OdebranoZakonczonoDzialanieSerwera(kom As ZakonczonoDzialanieSerwera)
    Public Event OdebranoDodanoPociag(kom As DodanoPociag)
    Public Event OdebranoNieuwierzytelniono(kom As Nieuwierzytelniono)
    Public Event OdebranoUwierzytelnionoPoprawnie(kom As UwierzytelnionoPoprawnie)
    Public Event OdebranoWybranoPosterunek(kom As WybranoPosterunek)
    Public Event OdebranoZakonczonoSesjeKlienta(kom As ZakonczonoSesjeKlienta)
    Public Event OdebranoZmienionoJasnoscLamp(kom As ZmienionoJasnoscLamp)
    Public Event OdebranoZmienionoKierunek(kom As ZmienionoKierunek)
    Public Event OdebranoZmienionoPredkoscDozwolona(kom As ZmienionoPredkoscDozwolona)
    Public Event OdebranoZmienionoPredkoscPociagu(kom As ZmienionoPredkoscPociagu)
    Public Event OdebranoZmienionoStanSygnalizatora(kom As ZmienionoStanSygnalizatora)
    Public Event OdebranoZmienionoStanToru(kom As ZmienionoStanToru)
    Public Event OdebranoZmienionoStanToruNrPociagow(kom As ZmienionoStanToruNrPociagow)
    Public Event OdebranoZmienionoStanZwrotnicy(kom As ZmienionoStanZwrotnicy)
    Public Event OdebranoPobranoPociagi(kom As PobranoPociagi)
    Public Event OdebranoWybranoPociag(kom As WybranoPociag)
    Public Event OdebranoWysiadznietoZPociagu(kom As WysiadznietoZPociagu)
    Public Event OdebranoUsunietoPociag(kom As UsunietoPociag)
    Public Event OdebranoZmienionoNazwePociagu(kom As ZmienionoNazwePociagu)
    Public Event OdebranoZmienionoStanPrzejazdu(kom As ZmienionoStanPrzejazdu)
    Public Event OdebranoZmienionoBlokadeZwrotnicy(kom As ZmienionoBlokadeZwrotnicy)
    Public Event OdebranoZmienionoBlokadeSygnalizatora(kom As ZmienionoBlokadeSygnalizatora)
    Public Event OdebranoZmienionoZamkniecieOdcinka(kom As ZmienionoZamkniecieOdcinka)
    Public Event OdebranoWyzerowanoLicznikOsi(kom As WyzerowanoLicznikOsi)
    Public Event OdebranoUstawionoTrybSamoczynnySygnalizatora(kom As UstawionoTrybSamoczynnySygnalizatora)

    Public Delegate Sub PrzetworzNumerKomunikatu(numer As Integer)

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
            AddressOf ZakonczonoDzialSerwera
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.DODANO_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf DodanoPociag.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoDodanoPociag(CType(kom, DodanoPociag))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.NIEUWIERZYTELNIONO, New PrzetwOdebrKomunikatu(
            AddressOf Nieuwierzytelniono.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoNieuwierzytelniono(CType(kom, Nieuwierzytelniono))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.UWIERZYTELNIONO_POPRAWNIE, New PrzetwOdebrKomunikatu(
            AddressOf UwierzytelnionoPoprawnie.Otworz,
            AddressOf PoprUwierzytelniono
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYBRANO_POSTERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf WybranoPosterunek.Otworz,
            AddressOf WybrPosterunek
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZONO_SESJE_KLIENTA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczonoSesjeKlienta.Otworz,
            AddressOf ZakSesjeKlienta
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_JASNOSC_LAMP, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoJasnoscLamp.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoJasnoscLamp(CType(kom, ZmienionoJasnoscLamp))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoKierunek.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoKierunek(CType(kom, ZmienionoKierunek))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_PREDKOSC_DOZWOLONA, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoPredkoscDozwolona.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoPredkoscDozwolona(CType(kom, ZmienionoPredkoscDozwolona))
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

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_STAN_TORU_NR_POCIAGOW, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoStanToruNrPociagow.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoStanToruNrPociagow(CType(kom, ZmienionoStanToruNrPociagow))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_STAN_ZWROTNICY, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoStanZwrotnicy.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoStanZwrotnicy(CType(kom, ZmienionoStanZwrotnicy))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.POBRANO_POCIAGI, New PrzetwOdebrKomunikatu(
            AddressOf PobranoPociagi.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoPobranoPociagi(CType(kom, PobranoPociagi))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYBRANO_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf WybranoPociag.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoWybranoPociag(CType(kom, WybranoPociag))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYSIADZNIETO_Z_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf WysiadznietoZPociagu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoWysiadznietoZPociagu(CType(kom, WysiadznietoZPociagu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USUNIETO_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf UsunietoPociag.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUsunietoPociag(CType(kom, UsunietoPociag))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_NAZWE_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoNazwePociagu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoNazwePociagu(CType(kom, ZmienionoNazwePociagu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_STAN_PRZEJAZDU, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoStanPrzejazdu.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoStanPrzejazdu(CType(kom, ZmienionoStanPrzejazdu))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_BLOKADE_ZWROTNICY, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoBlokadeZwrotnicy.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoBlokadeZwrotnicy(CType(kom, ZmienionoBlokadeZwrotnicy))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_BLOKADE_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoBlokadeSygnalizatora.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoBlokadeSygnalizatora(CType(kom, ZmienionoBlokadeSygnalizatora))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZMIENIONO_ZAMKNIECIE_ODCINKA, New PrzetwOdebrKomunikatu(
            AddressOf ZmienionoZamkniecieOdcinka.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoZmienionoZamkniecieOdcinka(CType(kom, ZmienionoZamkniecieOdcinka))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYZEROWANO_LICZNIK_OSI, New PrzetwOdebrKomunikatu(
            AddressOf WyzerowanoLicznikOsi.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoWyzerowanoLicznikOsi(CType(kom, WyzerowanoLicznikOsi))
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAWIONO_TRYB_SAMOCZYNNY_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf UstawionoTrybSamoczynnySygnalizatora.Otworz,
            Sub(pol, kom) RaiseEvent OdebranoUstawionoTrybSamoczynnySygnalizatora(CType(kom, UstawionoTrybSamoczynnySygnalizatora))
        ))
    End Sub

    Public Sub WyslijZakonczDzialanieKlienta(kom As ZakonczDzialanieKlienta)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawJasnoscLamp(kom As UstawJasnoscLamp)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawKierunek(kom As UstawKierunek)
        Wyslij(kom)
    End Sub

    Public Sub WyslijPotwierdzKierunek(kom As PotwierdzKierunek)
        Wyslij(kom)
    End Sub

    Public Sub WyslijDodajPociag(kom As DodajPociag)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawPredkoscPociagu(kom As UstawPredkoscPociagu)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawStanSygnalizatora(kom As UstawStanSygnalizatora)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawZwrotnice(kom As UstawZwrotnice)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUwierzytelnijSie(kom As UwierzytelnijSie)
        Wyslij(kom)
    End Sub

    Public Sub WyslijWybierzPosterunek(kom As WybierzPosterunek)
        Wyslij(kom)
    End Sub

    Public Sub WyslijZwolnijPrzebieg(kom As ZwolnijPrzebieg)
        Wyslij(kom)
    End Sub

    Public Sub WyslijPobierzPociagi(kom As PobierzPociagi)
        Wyslij(kom)
    End Sub

    Public Sub WyslijWybierzPociag(kom As WybierzPociag)
        Wyslij(kom)
    End Sub

    Public Sub WyslijWysiadzZPociagu(kom As WysiadzZPociagu)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUsunPociag(kom As UsunPociag)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawStanPrzejazdu(kom As UstawStanPrzejazdu)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawBlokadeZwrotnicy(kom As UstawBlokadeZwrotnicy)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawBlokadeSygnalizatora(kom As UstawBlokadeSygnalizatora)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawZamkniecieOdcinka(kom As UstawZamkniecieOdcinka, Optional przetwNumer As PrzetworzNumerKomunikatu = Nothing)
        Wyslij(kom, przetwNumer)
    End Sub

    Public Sub WyslijZerujLicznikOsi(kom As ZerujLicznikOsi)
        Wyslij(kom)
    End Sub

    Public Sub WyslijUstawTrybSamoczynnySygnalizatora(kom As UstawTrybSamoczynnySygnalizatora)
        Wyslij(kom)
    End Sub

    Public Sub Polacz(AdresIp As String, Port As UShort, Haslo As String, Obserwator As Boolean)
        Me.AdresIP = AdresIp
        Me.Port = Port
        Me.Haslo = Haslo

        WatekNawiazywaniaPolaczenia = New Thread(AddressOf PolaczZSerwerem)
        WatekNawiazywaniaPolaczenia.Start(Obserwator)
    End Sub

    Public Sub Zakoncz(czekaj As Boolean)
        If Not _Uruchomiony Then Exit Sub

        Dim k As PolaczenieTCP = Klient
        Klient = Nothing
        k?.Zakoncz(czekaj)

        Try
            WatekNawiazywaniaPolaczenia?.Abort()
        Catch
        End Try

        _Uruchomiony = False
    End Sub

    Friend Overrides Sub PrzetworzZakonczeniePolaczenia(pol As PolaczenieTCP)
        Klient = Nothing
        _Uruchomiony = False
        RaiseEvent ZakonczonoPolaczenie()
    End Sub

    Private Sub Wyslij(kom As Komunikat, Optional przetwNumer As PrzetworzNumerKomunikatu = Nothing)
        Dim nr As Integer = 0

        SyncLock Me
            If NumerKomunikatu = Integer.MaxValue Then
                NumerKomunikatu = 1
            Else
                NumerKomunikatu += 1
            End If

            nr = NumerKomunikatu
        End SyncLock

        kom.Numer = nr
        If przetwNumer IsNot Nothing Then przetwNumer(nr)
        Klient?.WyslijKomunikat(kom)
    End Sub

    Private Sub PolaczZSerwerem(obserwator As Object)
        Try
            Dim tcp As New TcpClient()
            tcp.Connect(New IPEndPoint(IPAddress.Parse(AdresIP), Port))
            Klient = New PolaczenieTCP(Me, tcp) With {
                .TrybObserwatora = CBool(obserwator)
            }
            _Uruchomiony = True

            DaneDH = New KlientDaneDH()
            Dim kom As DHInicjalizuj = DaneDH
            Wyslij(kom)
        Catch
            _Uruchomiony = False
            RaiseEvent BladNawiazywaniaPolaczenia()
        End Try

        WatekNawiazywaniaPolaczenia = Nothing
    End Sub

    Private Sub PrzetworzDH(pol As PolaczenieTCP, kom As Komunikat)
        Dim dh As DHZainicjalizowano = CType(kom, DHZainicjalizowano)
        Dim klucz As BigInteger = BigInteger.ModPow(dh.LiczbaB, DaneDH.LiczbaPrywA, DaneDH.LiczbaP)
        DaneDH = Nothing
        Klient.InicjujAes(klucz.ToByteArray)
        Wyslij(New UwierzytelnijSie() With {.Haslo = Haslo, .TrybObserwatora = pol.TrybObserwatora})
    End Sub

    Private Sub PoprUwierzytelniono(pol As PolaczenieTCP, kom As Komunikat)
        pol.UstawStanWyborPosterunku()
        RaiseEvent OdebranoUwierzytelnionoPoprawnie(CType(kom, UwierzytelnionoPoprawnie))
    End Sub

    Private Sub ZakonczonoDzialSerwera(pol As PolaczenieTCP, kom As Komunikat)
        Klient = Nothing
        pol.Zakoncz(False)
        _Uruchomiony = False
        RaiseEvent OdebranoZakonczonoDzialanieSerwera(CType(kom, ZakonczonoDzialanieSerwera))
    End Sub

    Private Sub WybrPosterunek(pol As PolaczenieTCP, kom As Komunikat)
        Dim wybrPost As WybranoPosterunek = CType(kom, WybranoPosterunek)
        If wybrPost.Stan = StanUstawianegoPosterunku.WybranoPoprawnie Then
            If pol.TrybObserwatora Then
                pol.UstawStanTrybObserwatora()
            Else
                pol.UstawStanSterowanieRuchem()
            End If
        End If
        RaiseEvent OdebranoWybranoPosterunek(wybrPost)
    End Sub

    Private Sub ZakSesjeKlienta(pol As PolaczenieTCP, kom As Komunikat)
        Klient = Nothing
        pol.Zakoncz(False)
        _Uruchomiony = False
        RaiseEvent OdebranoZakonczonoSesjeKlienta(CType(kom, ZakonczonoSesjeKlienta))
    End Sub
End Class