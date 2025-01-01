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

    Private _Port As UShort
    Public ReadOnly Property Port As UShort
        Get
            Return _Port
        End Get
    End Property

    Public ReadOnly Property CzyWczytanoPosterunki As Boolean
        Get
            Return Posterunki IsNot Nothing AndAlso Posterunki.Length > 0
        End Get
    End Property

    Private _AkceptacjaAdresow As New AkceptowaneAdresy
    Public Property AkceptacjaAdresow As AkceptowaneAdresy
        Get
            SyncLock slockAkcAdresy
                Return New AkceptowaneAdresy(_AkceptacjaAdresow)
            End SyncLock
        End Get
        Set(value As AkceptowaneAdresy)
            SyncLock slockAkcAdresy
                _AkceptacjaAdresow = If(value Is Nothing, New AkceptowaneAdresy(), New AkceptowaneAdresy(value))
            End SyncLock
        End Set
    End Property

    Public Property DostepnyTrybObserwatora As Boolean

    Private WithEvents PolaczenieUart As KomunikacjaZUrzadzeniami
    Private Posterunki As ObslugiwanyPosterunek()
    Private KlienciPoAdresiePosterunku As New Dictionary(Of UShort, ObslugiwanyPosterunek)
    Private PociagiPoNumerze As New Dictionary(Of UInteger, Pociag)
    Private WszyscyKlienci As New List(Of PolaczenieTCP)
    Private SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego As New Dictionary(Of UShort, List(Of SygnalizatorPowtarzajacy))
    Private DodatkoweDane As New Dictionary(Of UShort, DaneDodatkowePosterunku)
    Private Przejazdy As New Dictionary(Of UShort, PrzejazdKolejowoDrogowy)
    Private Serwer As TcpListener
    Private WatekSerwera As Thread
    Private WatekZamykaniaPolaczen As Thread
    Private Koniec As Boolean = False
    Private Haslo As String
    Private HasloObserwatora As String
    Private MaksymalnaPredkoscSieci As UShort
    Private slockRezerwacjaPosterunku As New Object
    Private slockListaPolaczen As New Object
    Private slockPociagi As New Object
    Private slockAkcAdresy As New Object

    Public Event UniewaznionoListePosterunkow()
    Public Event ZmianaCzasuPodlaczenia(post As String, dataPodlaczenia As String, adresIp As String)
    Public Event ZmienionoLiczbeObserwatorow(post As String, liczba As Integer)
    Public Event OdebranoUstawJasnoscLamp(post As UShort, kom As UstawJasnoscLamp)
    Public Event OdebranoUstawKierunek(post As UShort, kom As UstawKierunek)
    Public Event OdebranoPotwierdzKierunek(post As UShort, kom As PotwierdzKierunek)
    Public Event OdebranoDodajPociag(post As UShort, kom As DodajPociag)
    Public Event OdebranoUstawPredkoscPociagu(post As UShort, kom As UstawPredkoscPociagu)
    Public Event OdebranoUstawStanSygnalizatora(post As UShort, kom As UstawStanSygnalizatora)
    Public Event OdebranoUstawZwrotnice(post As UShort, kom As UstawZwrotnice)
    Public Event OdebranoUwierzytelnijSie(post As UShort, kom As UwierzytelnijSie)
    Public Event OdebranoWybierzPosterunek(post As UShort, kom As WybierzPosterunek)
    Public Event OdebranoZwolnijPrzebieg(post As UShort, kom As ZwolnijPrzebieg)
    Public Event OdebranoUstawStanPrzejazdu(post As UShort, kom As UstawStanPrzejazdu)
    Public Event OdebranoUstawBlokadeZwrotnicy(post As UShort, kom As UstawBlokadeZwrotnicy)
    Public Event OdebranoUstawBlokadeSygnalizatora(post As UShort, kom As UstawBlokadeSygnalizatora)
    Public Event OdebranoUstawZamkniecieOdcinka(post As UShort, kom As UstawZamkniecieOdcinka)
    Public Event OdebranoZerujLicznikOsi(post As UShort, kom As ZerujLicznikOsi)
    Public Event OdebranoUstawTrybSamoczynnySygnalizatora(post As UShort, kom As UstawTrybSamoczynnySygnalizatora)
    Public Event DodanoPociag(pociag As StanPociagu)
    Public Event ZmienionNazwePociagu(nrPociagu As UInteger, nazwa As String)
    Public Event ZmienionoLiczbeOsiPociagu(nrPociagu As UInteger, liczbaOsi As UShort)
    Public Event ZmienionoPosterunekSterujacyPociagiem(nrPociagu As UInteger, posterunek As String)
    Public Event ZmienionoLokalizacjePociagu(nrPociagu As UInteger, lokalizacja As String)
    Public Event UsunietoPociag(nrPociagu As UInteger)

    Public Sub New()
        DaneFabrykiObiektow.Add(TypKomunikatu.DH_INICJALIZUJ, New PrzetwOdebrKomunikatu(
            AddressOf DHInicjalizuj.Otworz,
            AddressOf PrzetworzDH
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZAKONCZ_DZIALANIE_KLIENTA, New PrzetwOdebrKomunikatu(
            AddressOf ZakonczDzialanieKlienta.Otworz,
            AddressOf PrzetworzZakonczDzialanieKlienta
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_JASNOSC_LAMP, New PrzetwOdebrKomunikatu(
            AddressOf UstawJasnoscLamp.Otworz,
            Sub(pol, kom)
                Dim k As UstawJasnoscLamp = CType(kom, UstawJasnoscLamp)
                Dim adr(k.Jasnosci.Length - 1) As UShort
                Dim jasnosc As JasnoscLampy

                For i As Integer = 0 To k.Jasnosci.Length - 1
                    jasnosc = k.Jasnosci(i)
                    adr(i) = jasnosc.Adres

                    PolaczenieUart?.UstawJasnoscLampy(New UstawJasnoscLampyUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = jasnosc.Adres, .Jasnosc = jasnosc.Jasnosc})
                Next

                pol.WyslijKomunikat(New ZmienionoJasnoscLamp() With {.Adresy = adr})
                'RaiseEvent OdebranoUstawJasnoscLamp(pol.AdresStacji, CType(kom, UstawJasnoscLamp))
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf UstawKierunek.Otworz,
            Sub(pol, kom)
                Dim k As UstawKierunek = CType(kom, UstawKierunek)
                Dim nowyStan As ObecnyStanKierunku
                Dim zajetoscToru As ZajetoscToru
                If k.Stan = UstawianyStanKierunku.Wlacz Then
                    nowyStan = ObecnyStanKierunku.Wyjazd
                    zajetoscToru = ZajetoscToru.Wolny
                Else
                    nowyStan = ObecnyStanKierunku.Neutralny
                    zajetoscToru = ZajetoscToru.BlokadaNieustawiona
                End If
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoKierunek() With {.Adres = k.Adres, .Stan = nowyStan})

                Dim dod As DaneDodatkowePosterunku = Nothing

                If DodatkoweDane.TryGetValue(pol.AdresPosterunku, dod) Then
                    Dim odc As OdcinekToru = Nothing

                    If dod.OdcinkiTorow.TryGetValue(k.Adres, odc) AndAlso odc.LiczbaTorow > 0 Then
                        Dim akt As New List(Of AktualizowanyKawalekToru)(odc.LiczbaTorow)

                        odc.PrzeiterujTory(Sub(kostka, polozenie)
                                               Dim p As Punkt = Nothing
                                               If dod.Kostki.TryGetValue(kostka, p) Then
                                                   If (polozenie And PrzynaleznoscToruDoOdcinka.Pierwszy) <> 0 Then
                                                       akt.Add(New AktualizowanyKawalekToru() With {
                                                               .Polozenie = PolozenieToru.TorPierwszy,
                                                               .Zajetosc = zajetoscToru,
                                                               .WspolrzedneKostki = p})
                                                   End If

                                                   If (polozenie And PrzynaleznoscToruDoOdcinka.Drugi) <> 0 Then
                                                       akt.Add(New AktualizowanyKawalekToru() With {
                                                               .Polozenie = PolozenieToru.TorDrugi,
                                                               .Zajetosc = zajetoscToru,
                                                               .WspolrzedneKostki = p})
                                                   End If
                                               End If
                                           End Sub)

                        If akt.Count > 0 Then
                            WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoStanToru() With {.Tory = akt.ToArray})
                        End If

                    End If
                End If

                RaiseEvent OdebranoUstawKierunek(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.POTWIERDZ_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf PotwierdzKierunek.Otworz,
            Sub(pol, kom)
                Dim k As PotwierdzKierunek = CType(kom, PotwierdzKierunek)
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoKierunek() With {.Adres = k.Adres, .Stan = ObecnyStanKierunku.Przyjazd})
                RaiseEvent OdebranoPotwierdzKierunek(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.DODAJ_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf DodajPociag.Otworz,
            Sub(pol, kom)
                Dim k As DodajPociag = CType(kom, DodajPociag)
                Dim obs As ObslugiwanyPosterunek = Nothing
                Dim stan As StanDodaniaPociagu = StanDodaniaPociagu.NrZajety

                If k.NrPociagu = 0 Then
                    stan = StanDodaniaPociagu.NieprawidlowyNumer

                ElseIf k.LiczbaOsi = 0 Then
                    stan = StanDodaniaPociagu.NieprawidlowaLiczbaOsi

                Else

                    SyncLock slockRezerwacjaPosterunku
                        KlienciPoAdresiePosterunku.TryGetValue(pol.AdresPosterunku, obs)
                    End SyncLock

                    If obs IsNot Nothing Then
                        Dim dod As DaneDodatkowePosterunku = Nothing
                        Dim odc As OdcinekToru = Nothing

                        If DodatkoweDane.TryGetValue(pol.AdresPosterunku, dod) AndAlso dod.OdcinkiTorow.TryGetValue(k.AdresOdcinka, odc) Then
                            SyncLock slockPociagi
                                If Not PociagiPoNumerze.ContainsKey(k.NrPociagu) Then
                                    stan = StanDodaniaPociagu.Dobrze

                                    PociagiPoNumerze.Add(k.NrPociagu, New Pociag() With {
                                        .DodajacyPosterunek = obs,
                                        .LiczbaOsi = k.LiczbaOsi,
                                        .PredkoscMaksymalna = k.PredkoscMaksymalna,
                                        .Lokalizacja = obs,
                                        .Nazwa = k.Nazwa,
                                        .Numer = k.NrPociagu,
                                        .Sterowalny = k.PojazdSterowalny
                                     })

                                End If
                            End SyncLock

                        Else
                            stan = StanDodaniaPociagu.BlednyAdresOdcinka
                        End If
                    End If

                End If

                pol.WyslijKomunikat(New DodanoPociag() With {.NrPociagu = k.NrPociagu, .Stan = stan})

                If stan = StanDodaniaPociagu.Dobrze Then
                    'RaiseEvent OdebranoDodajPociag(pol.AdresStacji, k)
                    RaiseEvent DodanoPociag(New StanPociagu With {
                        .DodajacyPosterunek = obs.NazwaPosterunku,
                        .LiczbaOsi = k.LiczbaOsi,
                        .PredkoscMaksymalna = k.PredkoscMaksymalna,
                        .Lokalizacja = obs.NazwaPosterunku,
                        .Nazwa = k.Nazwa,
                        .Numer = k.NrPociagu,
                        .Sterowalny = k.PojazdSterowalny
                    })
                End If
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_PREDKOSC_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf UstawPredkoscPociagu.Otworz,
            Sub(pol, kom)
                Dim k As UstawPredkoscPociagu = CType(kom, UstawPredkoscPociagu)
                Dim stan As StanZmianyPredkosci = StanZmianyPredkosci.BlednyNumer

                SyncLock slockPociagi
                    Dim poc As Pociag = Nothing

                    If PociagiPoNumerze.TryGetValue(k.NrPociagu, poc) Then
                        If poc.SterujacyPosterunek IsNot Nothing AndAlso poc.SterujacyPosterunek.Adres = pol.AdresPosterunku Then
                            poc.Predkosc = k.Predkosc
                            stan = StanZmianyPredkosci.Zmieniono
                        Else
                            stan = StanZmianyPredkosci.PociagNiesterowanyPrzezPosterunek
                        End If
                    End If
                End SyncLock

                pol.WyslijKomunikat(New ZmienionoPredkoscPociagu() With {
                                    .NrPociagu = k.NrPociagu,
                                    .Predkosc = If(stan = StanZmianyPredkosci.Zmieniono, k.Predkosc, 0S),
                                    .Stan = stan,
                                    .Zrodlo = ZrodloZmianyPredkosci.Program}
                                    )
                RaiseEvent OdebranoUstawPredkoscPociagu(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_STAN_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf UstawStanSygnalizatora.Otworz,
            Sub(pol, kom)
                Dim k As UstawStanSygnalizatora = CType(kom, UstawStanSygnalizatora)
                Dim st As StanSygnalizatora


                Dim p As PredkoscSygnalizatora


                Select Case k.Stan
                    Case UstawianyStanSygnalizatora.Manewrowy
                        st = StanSygnalizatora.Manewrowy
                        p = PredkoscSygnalizatora.Manewrowy
                    Case UstawianyStanSygnalizatora.Zastepczy
                        st = StanSygnalizatora.Zastepczy
                        p = PredkoscSygnalizatora.Zastepczy
                    Case UstawianyStanSygnalizatora.Zezwalajacy
                        st = StanSygnalizatora.Zezwalajacy
                        p = PredkoscSygnalizatora.VMax
                End Select
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoStanSygnalizatora() With {.Adres = k.Adres, .Stan = st})

                Dim lista As List(Of SygnalizatorPowtarzajacy) = Nothing
                If SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego.TryGetValue(k.Adres, lista) Then
                    Dim stanPowt As StanSygnalizatora = StanSygnalizatora.BrakWyjazdu
                    If k.Stan = UstawianyStanSygnalizatora.Zezwalajacy Then stanPowt = StanSygnalizatora.Zezwalajacy

                    For Each powt As SygnalizatorPowtarzajacy In lista
                        WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoStanSygnalizatora With {.Adres = powt.Adres, .Stan = stanPowt})
                    Next
                End If


                PolaczenieUart?.UstawStanSygnalizatoraPolsamoczynnego(New UstawStanSygnalizatoraPolsamoczynnegoUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = k.Adres, .Predkosc = p, .PredkoscPowtarzana = PredkoscPowtarzanaSygnalizatora.VMax})


                RaiseEvent OdebranoUstawStanSygnalizatora(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_ZWROTNICE, New PrzetwOdebrKomunikatu(
            AddressOf UstawZwrotnice.Otworz,
            Sub(pol, kom)
                Dim k As UstawZwrotnice = CType(kom, UstawZwrotnice)
                PolaczenieUart?.UstawZwrotnice(New UstawZwrotniceUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = k.Adres, .Ustawienie = If(k.Ustawienie = UstawianyStanRozjazdu.Wprost, UstawienieRozjazduEnum.Wprost, UstawienieRozjazduEnum.Bok)})
                RaiseEvent OdebranoUstawZwrotnice(pol.AdresPosterunku, k)
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

        DaneFabrykiObiektow.Add(TypKomunikatu.ZWOLNIJ_PRZEBIEG, New PrzetwOdebrKomunikatu(
            AddressOf ZwolnijPrzebieg.Otworz,
            Sub(pol, kom)
                Dim k As ZwolnijPrzebieg = CType(kom, ZwolnijPrzebieg)
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoStanSygnalizatora() With {.Adres = k.Adres, .Stan = StanSygnalizatora.BrakWyjazdu})
                RaiseEvent OdebranoZwolnijPrzebieg(pol.AdresPosterunku, k)


                PolaczenieUart?.UstawStanSygnalizatoraPolsamoczynnego(New UstawStanSygnalizatoraPolsamoczynnegoUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = k.Adres, .Predkosc = PredkoscSygnalizatora.V0})


            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.POBIERZ_POCIAGI, New PrzetwOdebrKomunikatu(
            AddressOf PobierzPociagi.Otworz,
            Sub(pol, kom)
                Dim lista As New List(Of DaneWybieralnegoPociagu)

                SyncLock slockPociagi
                    For Each kv As KeyValuePair(Of UInteger, Pociag) In PociagiPoNumerze
                        Dim p As Pociag = kv.Value

                        If p.Sterowalny Then
                            lista.Add(New DaneWybieralnegoPociagu() With {
                                .Numer = p.Numer,
                                .Nazwa = p.Nazwa,
                                .PredkoscMaksymalna = p.PredkoscMaksymalna,
                                .Stan = If(p.SterujacyPosterunek Is Nothing, StanWybieralnegoPociagu.Wolny, StanWybieralnegoPociagu.Zajety),
                                .DodajacyPosterunek = p.DodajacyPosterunek.NazwaPosterunku,
                                .Lokalizacja = p.Lokalizacja.NazwaPosterunku
                            })
                        End If

                    Next
                End SyncLock

                pol.WyslijKomunikat(New PobranoPociagi() With {.Pociagi = lista.ToArray()})
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYBIERZ_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf WybierzPociag.Otworz,
            Sub(pol, kom)
                Dim k As WybierzPociag = CType(kom, WybierzPociag)
                Dim stan As StanWybranegoPociagu = StanWybranegoPociagu.BlednyNumer
                Dim post As ObslugiwanyPosterunek = Nothing

                SyncLock slockRezerwacjaPosterunku
                    KlienciPoAdresiePosterunku.TryGetValue(pol.AdresPosterunku, post)
                End SyncLock

                If post IsNot Nothing Then
                    SyncLock slockPociagi
                        Dim poc As Pociag = Nothing

                        If PociagiPoNumerze.TryGetValue(k.NrPociagu, poc) Then
                            If poc.Sterowalny Then
                                If poc.SterujacyPosterunek Is Nothing Then
                                    poc.SterujacyPosterunek = post
                                    stan = StanWybranegoPociagu.Wybrany
                                Else
                                    stan = StanWybranegoPociagu.Zajety
                                End If
                            Else
                                stan = StanWybranegoPociagu.Niesterowalny
                            End If
                        End If
                    End SyncLock
                End If

                pol.WyslijKomunikat(New WybranoPociag() With {.NrPociagu = k.NrPociagu, .Stan = stan})

                If stan = StanWybranegoPociagu.Wybrany Then
                    RaiseEvent ZmienionoPosterunekSterujacyPociagiem(k.NrPociagu, post.NazwaPosterunku)
                End If
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.WYSIADZ_Z_POCIAGU, New PrzetwOdebrKomunikatu(
            AddressOf WysiadzZPociagu.Otworz,
            Sub(pol, kom)
                Dim k As WysiadzZPociagu = CType(kom, WysiadzZPociagu)
                WyrzucMaszyniste(k.NrPociagu, pol)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USUN_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf UsunPociag.Otworz,
            Sub(pol, kom)
                Dim k As UsunPociag = CType(kom, UsunPociag)
                UsunPociagZSieci(k.NrPociagu, pol)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_STAN_PRZEJAZDU, New PrzetwOdebrKomunikatu(
            AddressOf UstawStanPrzejazdu.Otworz,
            Sub(pol, kom)
                Dim k As UstawStanPrzejazdu = CType(kom, UstawStanPrzejazdu)
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoStanPrzejazdu() With {
                                    .NumerPrzejazdu = k.NumerPrzejazdu,
                                    .Stan = If(k.Stan = UstawianyStanPrzejazdu.Zamkniety, StanPrzejazduKolejowego.Zamkniety, StanPrzejazduKolejowego.Otwarty)})
                RaiseEvent OdebranoUstawStanPrzejazdu(pol.AdresPosterunku, k)

                Dim prz As PrzejazdKolejowoDrogowy = Nothing
                If Przejazdy.TryGetValue(k.NumerPrzejazdu, prz) Then
                    Dim wlaczony As Boolean = k.Stan = UstawianyStanPrzejazdu.Zamkniety

                    For Each r As PrzejazdRogatka In prz.Rogatki
                        If k.Stan = UstawianyStanPrzejazdu.Zamkniety Then
                            PolaczenieUart?.ZamknijRogatke(New ZamknijRogatkeUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = r.Adres, .CzasZamykaniaMs = prz.CzasOpuszczania})
                        Else
                            PolaczenieUart?.OtworzRogatke(New OtworzRogatkeUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = r.Adres, .CzasOtwieraniaMs = prz.CzasPodnoszenia})
                        End If
                    Next

                    For Each s As PrzejazdElementWykonawczy In prz.SygnalizatoryDrogowe
                        PolaczenieUart?.UstawStanSygnalizatoraDrogowego(New UstawStanSygnalizatoraDrogowegoUrz() With {.AdresPosterunku = pol.AdresPosterunku, .AdresUrzadzenia = s.Adres, .Wlaczony = wlaczony})
                    Next
                End If

            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_BLOKADE_ZWROTNICY, New PrzetwOdebrKomunikatu(
            AddressOf UstawBlokadeZwrotnicy.Otworz,
            Sub(pol, kom)
                Dim k As UstawBlokadeZwrotnicy = CType(kom, UstawBlokadeZwrotnicy)
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoBlokadeZwrotnicy() With {.Adres = k.Adres, .Stan = If(k.Zablokowana, StanZmienionejBlokadyZwrotnicy.Zablokowana, StanZmienionejBlokadyZwrotnicy.Odblokowana)})
                RaiseEvent OdebranoUstawBlokadeZwrotnicy(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_BLOKADE_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf UstawBlokadeSygnalizatora.Otworz,
            Sub(pol, kom)
                Dim k As UstawBlokadeSygnalizatora = CType(kom, UstawBlokadeSygnalizatora)
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoBlokadeSygnalizatora() With {.Adres = k.Adres, .Stan = If(k.Zablokowany, StanZmienionejBlokadySygnalizatora.Zablokowany, StanZmienionejBlokadySygnalizatora.Odblokowany)})
                RaiseEvent OdebranoUstawBlokadeSygnalizatora(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_ZAMKNIECIE_ODCINKA, New PrzetwOdebrKomunikatu(
            AddressOf UstawZamkniecieOdcinka.Otworz,
            Sub(pol, kom)
                Dim k As UstawZamkniecieOdcinka = CType(kom, UstawZamkniecieOdcinka)
                WyslijKomunikatDoWszystkichPolaczen(pol, New ZmienionoZamkniecieOdcinka() With {.Adres = k.Adres, .Stan = If(k.Zamkniety, StanZamykanegoOdcinka.Zamkniety, StanZamykanegoOdcinka.Otwarty), .Numer = k.Numer})
                RaiseEvent OdebranoUstawZamkniecieOdcinka(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.ZERUJ_LICZNIK_OSI, New PrzetwOdebrKomunikatu(
            AddressOf ZerujLicznikOsi.Otworz,
            Sub(pol, kom)
                Dim k As ZerujLicznikOsi = CType(kom, ZerujLicznikOsi)
                WyslijKomunikatDoWszystkichPolaczen(pol, New WyzerowanoLicznikOsi() With {.AdresOdcinka = k.AdresOdcinka, .Stan = StanZerowanegoLicznikaOsi.Wyzerowano})
                RaiseEvent OdebranoZerujLicznikOsi(pol.AdresPosterunku, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_TRYB_SAMOCZYNNY_SYGNALIZATORA, New PrzetwOdebrKomunikatu(
            AddressOf UstawTrybSamoczynnySygnalizatora.Otworz,
            Sub(pol, kom)
                Dim k As UstawTrybSamoczynnySygnalizatora = CType(kom, UstawTrybSamoczynnySygnalizatora)
                pol.WyslijKomunikat(New UstawionoTrybSamoczynnySygnalizatora() With {.Adres = k.Adres, .Stan = If(k.TrybSamoczynny, StanTrybuSamoczynnegoSygnalizatora.TrybSamoczynny, StanTrybuSamoczynnegoSygnalizatora.TrybPolsamoczynny)})
                RaiseEvent OdebranoUstawTrybSamoczynnySygnalizatora(pol.AdresPosterunku, k)
            End Sub
        ))
    End Sub

    Public Sub WyslijInformacje(post As UShort, kom As Informacja)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijDodanoPociag(post As UShort, kom As DodanoPociag)
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

    Public Sub WyslijZmienionoJasnoscLamp(post As UShort, kom As ZmienionoJasnoscLamp)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoKierunek(post As UShort, kom As ZmienionoKierunek)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WyslijZmienionoPredkoscDozwolona(post As UShort, kom As ZmienionoPredkoscDozwolona)
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

    Public Sub WysliZmienionoStanPrzejazdu(post As UShort, kom As ZmienionoStanPrzejazdu)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WysliZmienionoBlokadeZwrotnicy(post As UShort, kom As ZmienionoBlokadeZwrotnicy)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WysliZmienionoBlokadeSygnalizatora(post As UShort, kom As ZmienionoBlokadeSygnalizatora)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WysliZmienionoZamkniecieOdcinka(post As UShort, kom As ZmienionoZamkniecieOdcinka)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WysliWyzerowanoLicznikOsi(post As UShort, kom As WyzerowanoLicznikOsi)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub WysliUstawionoTrybSamoczynnySygnalizatora(post As UShort, kom As UstawionoTrybSamoczynnySygnalizatora)
        WyslijDoKlienta(post, kom)
    End Sub

    Public Sub UstawZwrotniceSerwisowo(kom As UstawZwrotniceSerwisowoUrz)
        PolaczenieUart?.UstawZwrotniceSerwisowo(kom)
    End Sub

    Public Sub ZmienNazwePociagu(NrPociagu As UInteger, Nazwa As String)
        Dim poc As Pociag = Nothing
        Dim obs As ObslugiwanyPosterunek = Nothing

        SyncLock slockPociagi
            If PociagiPoNumerze.TryGetValue(NrPociagu, poc) Then
                poc.Nazwa = Nazwa
                obs = poc.SterujacyPosterunek
            End If
        End SyncLock

        obs?.Polaczenie?.WyslijKomunikat(New ZmienionoNazwePociagu() With {.NrPociagu = NrPociagu, .Nazwa = Nazwa})

        If poc IsNot Nothing Then
            RaiseEvent ZmienionNazwePociagu(NrPociagu, Nazwa)
        End If
    End Sub

    Public Function UsunPociagZSieci(NrPociagu As UInteger) As StanUsuwaniaPociagu
        Return UsunPociagZSieci(NrPociagu, Nothing)
    End Function

    Private Function UsunPociagZSieci(NrPociagu As UInteger, pol As PolaczenieTCP) As StanUsuwaniaPociagu
        Dim stan As StanUsuwaniaPociagu = StanUsuwaniaPociagu.BlednyNumer
        Dim poc As Pociag = Nothing
        Dim post As UShort? = Nothing

        SyncLock slockPociagi
            If PociagiPoNumerze.TryGetValue(NrPociagu, poc) Then

                If pol Is Nothing Then
                    stan = StanUsuwaniaPociagu.Usunieto
                Else
                    If poc.SterujacyPosterunek Is Nothing OrElse poc.SterujacyPosterunek.Adres = pol.AdresPosterunku Then
                        stan = StanUsuwaniaPociagu.Usunieto
                    Else
                        stan = StanUsuwaniaPociagu.PociagZajety
                    End If
                End If

                If stan = StanUsuwaniaPociagu.Usunieto Then PociagiPoNumerze.Remove(NrPociagu)
                post = poc.SterujacyPosterunek?.Adres

            End If
        End SyncLock

        If pol Is Nothing And post.HasValue Then
            Dim obs As ObslugiwanyPosterunek = Nothing

            SyncLock slockRezerwacjaPosterunku
                KlienciPoAdresiePosterunku.TryGetValue(post.Value, obs)
                pol = obs?.Polaczenie
            End SyncLock
        End If

        pol?.WyslijKomunikat(New UsunietoPociag() With {.NrPociagu = NrPociagu, .Stan = stan})
        If stan = StanUsuwaniaPociagu.Usunieto Then RaiseEvent UsunietoPociag(NrPociagu)

        Return stan
    End Function

    Public Function WyrzucMaszyniste(nrPociagu As UInteger) As StanWysiadzniecia
        Return WyrzucMaszyniste(nrPociagu, Nothing)
    End Function

    Private Function WyrzucMaszyniste(nrPociagu As UInteger, pol As PolaczenieTCP) As StanWysiadzniecia
        Dim stan As StanWysiadzniecia = StanWysiadzniecia.BlednyNumer

        SyncLock slockPociagi
            Dim poc As Pociag = Nothing

            If PociagiPoNumerze.TryGetValue(nrPociagu, poc) Then
                If pol Is Nothing Then

                    pol = poc.SterujacyPosterunek?.Polaczenie
                    poc.SterujacyPosterunek = Nothing
                    stan = StanWysiadzniecia.Wyrzucono

                Else

                    If poc.SterujacyPosterunek IsNot Nothing AndAlso poc.SterujacyPosterunek.Adres = pol.AdresPosterunku Then
                        poc.SterujacyPosterunek = Nothing
                        stan = StanWysiadzniecia.Wysiadznieto
                    Else
                        stan = StanWysiadzniecia.PociagNiesterowanyPrzezPosterunek
                    End If

                End If
            End If
        End SyncLock

        pol?.WyslijKomunikat(New WysiadznietoZPociagu() With {.NrPociagu = nrPociagu, .Stan = stan})

        If stan = StanWysiadzniecia.Wysiadznieto Then
            RaiseEvent ZmienionoPosterunekSterujacyPociagiem(nrPociagu, Nothing)
        End If

        Return stan
    End Function

    Public Function Uruchom(port As UShort, haslo As String, hasloObserwatora As String) As Boolean
        If Serwer IsNot Nothing Or _Uruchomiony Or Not CzyWczytanoPosterunki Then Return False

        Koniec = False
        _Port = port
        Me.Haslo = haslo
        Me.HasloObserwatora = hasloObserwatora

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

        For Each obs As ObslugiwanyPosterunek In KlienciPoAdresiePosterunku.Values
            obs.Polaczenie = Nothing
            obs.Obserwatorzy.Clear()
        Next
        RaiseEvent UniewaznionoListePosterunkow()

        _Uruchomiony = False
    End Sub

    Public Sub UstawUrzadzenie(urz As KomunikacjaZUrzadzeniami)
        PolaczenieUart = urz
    End Sub

    Public Function WczytajPolaczenie(SciezkaPliku As String) As Boolean
        If Uruchomiony Then Return False

        KlienciPoAdresiePosterunku = New Dictionary(Of UShort, ObslugiwanyPosterunek)
        SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego = New Dictionary(Of UShort, List(Of SygnalizatorPowtarzajacy))
        DodatkoweDane = New Dictionary(Of UShort, DaneDodatkowePosterunku)
        Dim PosterunkiLista As New List(Of ObslugiwanyPosterunek)

        MaksymalnaPredkoscSieci = 0
        Dim polaczenia As PolaczeniaPosterunkow = PolaczeniaPosterunkow.OtworzPlik(SciezkaPliku, False)

        If polaczenia IsNot Nothing Then
            For Each pol As LaczonyPlikPosterunku In polaczenia.LaczanePliki
                Dim obs As New ObslugiwanyPosterunek() With {
                                       .NazwaPosterunku = pol.NazwaPosterunku,
                                       .NazwaPliku = pol.NazwaPliku,
                                       .Adres = pol.Adres,
                                       .Zawartosc = pol.ZawartoscPosterunku,
                                       .DanePulpitu = pol.DanePulpitu}

                If Not KlienciPoAdresiePosterunku.ContainsKey(pol.Adres) Then
                    KlienciPoAdresiePosterunku.Add(pol.Adres, obs)
                End If

                PosterunkiLista.Add(obs)
                ZnajdzMaksymalnaPredkoscSieci(pol.DanePulpitu)
                ZnajdzSygnalizatoryPowtarzajace(pol.DanePulpitu)
                Dim prz As Dictionary(Of UShort, PrzejazdKolejowoDrogowy) = pol.DanePulpitu.PobierzPrzejazdyKolejowoDrogowe
                If prz.Count > 0 Then Przejazdy = prz

                If Not DodatkoweDane.ContainsKey(pol.Adres) Then
                    DodatkoweDane.Add(pol.Adres, New DaneDodatkowePosterunku() With {
                                      .Kostki = pol.DanePulpitu.PobierzKostkiZeWspolrzednymi(),
                                      .OdcinkiTorow = pol.DanePulpitu.PobierzOdcinkiTorow()})
                End If

            Next
        End If

        Posterunki = PosterunkiLista.ToArray

        RaiseEvent UniewaznionoListePosterunkow()
        Return CzyWczytanoPosterunki
    End Function

    Public Sub ZakonczPolaczenie(adresPosterunku As UShort)
        WyslijZakonczenieSesjiKlienta(adresPosterunku, Function(o) o.Polaczenie)
    End Sub

    Public Sub ZakonczPolaczenieObserwatora(adresPosterunku As UShort, adresIp As String)
        WyslijZakonczenieSesjiKlienta(adresPosterunku, Function(o)
                                                           Dim pol As PolaczenieTCP = Nothing
                                                           o.Obserwatorzy.TryGetValue(adresIp, pol)
                                                           Return pol
                                                       End Function)
    End Sub

    Public Function PobierzStanPolaczen() As StanObslugiwanegoPosterunku()
        If Posterunki Is Nothing Then Return Nothing

        Dim lista As New List(Of StanObslugiwanegoPosterunku)

        For Each obs As ObslugiwanyPosterunek In Posterunki
            Dim dataPodlaczenia As String = ""
            Dim ostatnieZapytanie As String = ""
            Dim adresIp As String = ""

            If obs.Polaczenie IsNot Nothing Then
                dataPodlaczenia = obs.Polaczenie.CzasNawiazaniaPolaczenia.ToString(FORMAT_DATY)
                ostatnieZapytanie = obs.Polaczenie.OstatnieZapytanie.ToString(FORMAT_DATY)
                adresIp = obs.Polaczenie.AdresIp
            End If

            lista.Add(New StanObslugiwanegoPosterunku With {
                      .NazwaPosterunku = obs.NazwaPosterunku,
                      .NazwaPliku = obs.NazwaPliku,
                      .Adres = obs.Adres.ToString,
                      .DataPodlaczenia = dataPodlaczenia,
                      .OstatnieZapytanie = ostatnieZapytanie,
                      .AdresIp = adresIp,
                      .Obserwatorzy = obs.Obserwatorzy.Count
            })
        Next

        Return lista.ToArray()
    End Function

    Public Function PobierzStanPociagow() As IEnumerable(Of StanPociagu)
        Dim lista As New List(Of StanPociagu)

        SyncLock slockPociagi
            For Each poc As KeyValuePair(Of UInteger, Pociag) In PociagiPoNumerze
                lista.Add(New StanPociagu() With {
                    .DodajacyPosterunek = poc.Value.DodajacyPosterunek.NazwaPosterunku,
                    .LiczbaOsi = poc.Value.LiczbaOsi,
                    .PredkoscMaksymalna = poc.Value.PredkoscMaksymalna,
                    .Lokalizacja = poc.Value.Lokalizacja.NazwaPosterunku,
                    .Nazwa = poc.Value.Nazwa,
                    .Numer = poc.Value.Numer,
                    .Sterowalny = poc.Value.Sterowalny,
                    .SterujacyPosterunek = poc.Value.SterujacyPosterunek?.NazwaPosterunku
                })
            Next
        End SyncLock

        Return lista
    End Function

    Public Function PobierzZwrotnice() As IEnumerable(Of DaneZwrotnicy)
        If _Uruchomiony Or Not CzyWczytanoPosterunki Then Return Nothing

        Dim zwrotnice As New List(Of DaneZwrotnicy)

        For Each p As ObslugiwanyPosterunek In Posterunki
            p.DanePulpitu.PrzeiterujKostki(Sub(x, y, k)
                                               Dim z As Rozjazd = TryCast(k, Rozjazd)
                                               If z IsNot Nothing Then
                                                   zwrotnice.Add(New DaneZwrotnicy() With {
                                                                 .AdresPosterunku = p.Adres,
                                                                 .NazwaPosterunku = p.NazwaPosterunku,
                                                                 .AdresZwrotnicy = z.Adres,
                                                                 .NazwaZwrotnicy = z.Nazwa})
                                               End If
                                           End Sub)
        Next

        Return zwrotnice
    End Function

    Friend Overrides Sub PrzetworzZakonczeniePolaczenia(pol As PolaczenieTCP)
        If Koniec Then Exit Sub

        SyncLock slockListaPolaczen
            WszyscyKlienci.Remove(pol)
        End SyncLock

        RozlaczPosterunek(pol)
    End Sub

    Private Sub WyslijZakonczenieSesjiKlienta(adresPosterunku As UShort, metoda As Func(Of ObslugiwanyPosterunek, PolaczenieTCP))
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim pol As PolaczenieTCP = Nothing

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresiePosterunku.TryGetValue(adresPosterunku, obs) Then
                pol = metoda(obs)
            End If
        End SyncLock

        If pol IsNot Nothing Then
            Dim kom As New ZakonczonoSesjeKlienta() With {.Przyczyna = PrzyczynaZakonczeniaSesjiKlienta.RozlaczeniePrzezSerwer}
            pol.WyslijKomunikat(kom)
            pol.Zakoncz(True)
        End If
    End Sub

    Private Function CzyAdresAkceptowany(adres As EndPoint) As Boolean
        Dim adr As IPAddress = TryCast(adres, IPEndPoint)?.Address
        If adr Is Nothing Then Return False

        SyncLock slockAkcAdresy
            If _AkceptacjaAdresow.Typ = TypAkceptowaniaAdresow.Wszyscy Then Return True

            Dim zawiera As Boolean = _AkceptacjaAdresow.Zbior.Contains(adr)
            Return _
                (_AkceptacjaAdresow.Typ = TypAkceptowaniaAdresow.OproczWybranych AndAlso Not zawiera) OrElse
                (_AkceptacjaAdresow.Typ = TypAkceptowaniaAdresow.TylkoWybrani AndAlso zawiera)
        End SyncLock

        Return False
    End Function

    Private Sub AkceptujPolaczenia()
        Do Until Koniec
            Try
                Dim tcp As TcpClient = Serwer.AcceptTcpClient()

                If Not CzyAdresAkceptowany(tcp.Client.RemoteEndPoint) Then
                    tcp.Close()
                    Continue Do
                End If

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
        Dim polaczeniaDoZamkniecia As List(Of PolaczenieTCP)
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
            KlienciPoAdresiePosterunku.TryGetValue(post, pol)
        End SyncLock

        pol?.Polaczenie?.WyslijKomunikat(kom)
    End Sub

    Private Sub PrzetworzDH(pol As PolaczenieTCP, kom As Komunikat)
        Dim dh As DHInicjalizuj = CType(kom, DHInicjalizuj)
        If dh.LiczbaP < MIN_LICZBA_PIERWSZA Then Exit Sub

        Dim liczbaPrywB As BigInteger = PobierzLosowaDuzaLiczbe()
        Dim dhOdp As New DHZainicjalizowano
        dhOdp.LiczbaB = BigInteger.ModPow(New BigInteger(dh.LiczbaG), liczbaPrywB, dh.LiczbaP)

        Dim klucz As BigInteger = BigInteger.ModPow(dh.LiczbaA, liczbaPrywB, dh.LiczbaP)
        pol.InicjujAes(klucz.ToByteArray)

        pol.WyslijKomunikat(dhOdp)
    End Sub

    Private Sub UwierzytelnijIWyslijPosterunki(pol As PolaczenieTCP, kom As Komunikat)
        Dim uw As UwierzytelnijSie = CType(kom, UwierzytelnijSie)

        If ((Not uw.TrybObserwatora) AndAlso uw.Haslo = Haslo) OrElse (DostepnyTrybObserwatora AndAlso uw.TrybObserwatora AndAlso uw.Haslo = HasloObserwatora) Then
            pol.TrybObserwatora = uw.TrybObserwatora
            pol.UstawStanWyborPosterunku()

            Dim postLista As New List(Of DanePosterunku)
            For Each op As ObslugiwanyPosterunek In Posterunki
                postLista.Add(New DanePosterunku() With {
                              .Adres = op.Adres,
                              .Nazwa = op.NazwaPosterunku,
                              .Stan = If(op.Polaczenie Is Nothing, StanPosterunku.Wolny, StanPosterunku.Zajety)})
            Next
            pol.WyslijKomunikat(New UwierzytelnionoPoprawnie() With {.PredkoscMaksymalna = MaksymalnaPredkoscSieci, .Posterunki = postLista.ToArray})

        ElseIf (Not DostepnyTrybObserwatora) AndAlso uw.TrybObserwatora Then
            pol.WyslijKomunikat(New Nieuwierzytelniono() With {.Przyczyna = PrzyczynaNieuwierzytelnienia.BrakTrybuObserwatora})
        Else
            pol.WyslijKomunikat(New Nieuwierzytelniono() With {.Przyczyna = PrzyczynaNieuwierzytelnienia.BledneHaslo})
        End If
    End Sub

    Private Sub ZerezerwujPosterunek(pol As PolaczenieTCP, kom As Komunikat)
        Dim k As WybierzPosterunek = CType(kom, WybierzPosterunek)
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim odp As Komunikat = Nothing
        Dim dataPodl As String = Nothing
        Dim adresIp As String = Nothing
        Dim wybrano As Boolean = False
        Dim liczbaObserwatorow As Integer = -1

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresiePosterunku.TryGetValue(k.Adres, obs) Then

                If pol.TrybObserwatora Then
                    If Not obs.Obserwatorzy.ContainsKey(pol.AdresIp) Then
                        obs.Obserwatorzy.Add(pol.AdresIp, pol)
                        liczbaObserwatorow = obs.Obserwatorzy.Count
                        pol.UstawStanTrybObserwatora()
                        wybrano = True
                    End If

                ElseIf obs.Polaczenie Is Nothing Then
                    obs.Polaczenie = pol
                    dataPodl = pol.CzasNawiazaniaPolaczenia.ToString(FORMAT_DATY)
                    adresIp = pol.AdresIp
                    pol.UstawStanSterowanieRuchem()
                    wybrano = True

                End If

                If wybrano Then
                    pol.AdresPosterunku = k.Adres
                    odp = New WybranoPosterunek() With {.Stan = StanUstawianegoPosterunku.WybranoPoprawnie, .ZawartoscPliku = obs.Zawartosc}
                End If
            End If
        End SyncLock

        If Not wybrano Then
            odp = New WybranoPosterunek() With {.Stan = StanUstawianegoPosterunku.PosterunekZajety}
        End If

        pol.WyslijKomunikat(odp)

        If dataPodl IsNot Nothing And adresIp IsNot Nothing Then
            RaiseEvent ZmianaCzasuPodlaczenia(obs.Adres.ToString, dataPodl, adresIp)
        ElseIf liczbaObserwatorow >= 0 Then
            RaiseEvent ZmienionoLiczbeObserwatorow(obs.Adres.ToString, liczbaObserwatorow)
        End If
    End Sub

    Private Sub RozlaczPosterunek(polaczenie As PolaczenieTCP)
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim adr As String = Nothing
        Dim pol As PolaczenieTCP = Nothing
        Dim liczbaObs As Integer = -1

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresiePosterunku.TryGetValue(polaczenie.AdresPosterunku, obs) Then

                If polaczenie.TrybObserwatora AndAlso obs.Obserwatorzy.TryGetValue(polaczenie.AdresIp, pol) Then
                    obs.Obserwatorzy.Remove(polaczenie.AdresIp)
                    liczbaObs = obs.Obserwatorzy.Count
                ElseIf (Not polaczenie.TrybObserwatora) AndAlso obs.Polaczenie IsNot Nothing Then
                    obs.Polaczenie = Nothing
                    adr = polaczenie.AdresPosterunku.ToString
                End If

            End If
        End SyncLock

        If adr IsNot Nothing Then
            RaiseEvent ZmianaCzasuPodlaczenia(adr, "", "")
        ElseIf liczbaObs >= 0 Then
            RaiseEvent ZmienionoLiczbeObserwatorow(polaczenie.AdresPosterunku.ToString, liczbaObs)
        End If
    End Sub

    Private Sub PrzetworzZakonczDzialanieKlienta(pol As PolaczenieTCP, kom As Komunikat)
        RozlaczPosterunek(pol)
        pol.WyslijKomunikat(New ZakonczonoSesjeKlienta() With {.Przyczyna = PrzyczynaZakonczeniaSesjiKlienta.RozlaczenieKlienta})
        pol.Zakoncz(True)
    End Sub

    Private Sub WyslijKomunikatDoWszystkichPolaczen(pol As PolaczenieTCP, kom As Komunikat)
        WyslijKomunikatDoWszystkichPolaczen(pol, pol.AdresPosterunku, kom)
    End Sub

    Private Sub WyslijKomunikatDoWszystkichPolaczen(pol As PolaczenieTCP, adresPosterunku As UShort, kom As Komunikat)
        Dim obs As ObslugiwanyPosterunek = Nothing

        pol?.WyslijKomunikat(kom)

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresiePosterunku.TryGetValue(adresPosterunku, obs) Then
                For Each kv As KeyValuePair(Of String, PolaczenieTCP) In obs.Obserwatorzy
                    kv.Value.WyslijKomunikat(kom)
                Next
            End If
        End SyncLock
    End Sub

    Private Sub ZnajdzMaksymalnaPredkoscSieci(p As Pulpit)
        p.PrzeiterujKostki(Sub(x, y, k)
                               Dim t As Tor = TryCast(k, Tor)
                               Dim r As Rozjazd = TryCast(k, Rozjazd)

                               If t IsNot Nothing AndAlso t.Predkosc > MaksymalnaPredkoscSieci Then
                                   MaksymalnaPredkoscSieci = t.Predkosc
                               End If

                               If r IsNot Nothing AndAlso r.PredkoscDrugi > MaksymalnaPredkoscSieci Then
                                   MaksymalnaPredkoscSieci = r.PredkoscDrugi
                               End If
                           End Sub)
    End Sub

    Private Sub ZnajdzSygnalizatoryPowtarzajace(p As Pulpit)
        p.PrzeiterujKostki(Sub(x, y, k)
                               If k.Typ = TypKostki.SygnalizatorPowtarzajacy Then
                                   Dim powt As SygnalizatorPowtarzajacy = CType(k, SygnalizatorPowtarzajacy)

                                   If powt.SygnalizatorPowtarzany IsNot Nothing Then
                                       Dim lista As List(Of SygnalizatorPowtarzajacy) = Nothing

                                       If Not SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego.TryGetValue(powt.SygnalizatorPowtarzany.Adres, lista) Then
                                           lista = New List(Of SygnalizatorPowtarzajacy)()
                                           SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego.Add(powt.SygnalizatorPowtarzany.Adres, lista)
                                       End If

                                       lista.Add(powt)
                                   End If
                               End If
                           End Sub)
    End Sub

    Public Function PobierzPosterunki() As Pulpit()
        Dim lista As New List(Of Pulpit)(Posterunki.Length)

        For i As Integer = 0 To Posterunki.Length - 1
            lista.Add(Posterunki(i).DanePulpitu)
        Next

        Return lista.OrderBy(Function(p) p.Nazwa).ToArray()
    End Function

    Public Function PobierzObserwatorow(adres As UShort) As ObserwatorPosterunku()
        Dim obs As ObslugiwanyPosterunek = Nothing
        Dim wynik As New List(Of ObserwatorPosterunku)

        SyncLock slockRezerwacjaPosterunku
            If KlienciPoAdresiePosterunku.TryGetValue(adres, obs) Then
                For Each kv As KeyValuePair(Of String, PolaczenieTCP) In obs.Obserwatorzy
                    wynik.Add(New ObserwatorPosterunku() With {.AdresIP = kv.Value.AdresIp, .CzasPodlaczenia = kv.Value.CzasNawiazaniaPolaczenia.ToString(FORMAT_DATY)})
                Next
            End If
        End SyncLock

        Return wynik.ToArray()
    End Function

    Private Sub PolaczenieUart_OdebranoUstawionoJasnoscLampy(kom As UstawionoJasnoscLampyUrz) Handles PolaczenieUart.OdebranoUstawionoJasnoscLampy
        Debug.Print("Jasność lampy " & kom.AdresUrzadzenia)
    End Sub

    Private Sub PolaczenieUart_OdebranoUstawionoStanSygnalizatora(kom As UstawionoStanSygnalizatoraUrz) Handles PolaczenieUart.OdebranoUstawionoStanSygnalizatora
        Debug.Print("Ustawienie sygnalizatora " & kom.AdresUrzadzenia)
    End Sub

    Private Sub PolaczenieUart_OdebranoUstawionoStanSygnalizatoraDrogowego(kom As UstawionoStanSygnalizatoraDrogowegoUrz) Handles PolaczenieUart.OdebranoUstawionoStanSygnalizatoraDrogowego
        Debug.Print("Ustawienie sygnalizatora drogowego " & kom.AdresUrzadzenia)
    End Sub

    Private Sub PolaczenieUart_OdebranoWykrytoOs(kom As WykrytoOsUrz) Handles PolaczenieUart.OdebranoWykrytoOs
        Debug.Print("Wykryto oś " & kom.AdresUrzadzenia)
    End Sub

    Private Sub PolaczenieUart_OdebranoZmienionoStanRogatki(kom As ZmienionoStanRogatkiUrz) Handles PolaczenieUart.OdebranoZmienionoStanRogatki
        Debug.Print("Zmieniono stan rogatki " & kom.AdresUrzadzenia & ": " & kom.Stan.ToString)
    End Sub

    Private Sub PolaczenieUart_OdebranoZmienionoStanZwrotnicy(kom As ZmienionoStanZwrotnicyUrz) Handles PolaczenieUart.OdebranoZmienionoStanZwrotnicy
        Debug.Print("Zmieniono stan zwrotnicy " & kom.AdresUrzadzenia & ": " & kom.Stan.ToString)

        Dim obs As ObslugiwanyPosterunek = Nothing
        SyncLock slockRezerwacjaPosterunku
            KlienciPoAdresiePosterunku.TryGetValue(kom.AdresPosterunku, obs)
        End SyncLock

        If obs IsNot Nothing Then
            WyslijKomunikatDoWszystkichPolaczen(obs.Polaczenie, kom.AdresPosterunku, New ZmienionoStanZwrotnicy() With {.Adres = kom.AdresUrzadzenia, .Stan = kom.Stan, .Rozprucie = kom.Stan = StanRozjazdu.Oba})
        End If
    End Sub

    Private Class DaneDodatkowePosterunku
        Friend OdcinkiTorow As Dictionary(Of UShort, OdcinekToru)
        Friend Kostki As Dictionary(Of Kostka, Punkt)
    End Class
End Class