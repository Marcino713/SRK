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
    Private PociagiPoNumerze As New Dictionary(Of UInteger, Pociag)
    Private WszyscyKlienci As New List(Of PolaczenieTCP)
    Private SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego As New Dictionary(Of UShort, List(Of SygnalizatorPowtarzajacy))
    Private Serwer As TcpListener
    Private WatekSerwera As Thread
    Private WatekZamykaniaPolaczen As Thread
    Private Koniec As Boolean = False
    Private Haslo As String
    Private MaksymalnaPredkoscSieci As UShort
    Private slockRezerwacjaPosterunku As New Object
    Private slockListaPolaczen As New Object
    Private slockPociagi As New Object

    Public Event UniewaznionoListePosterunkow()
    Public Event ZmianaCzasuPodlaczenia(post As String, dataPodlaczenia As String)
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
            Sub(pol, kom)
                Dim k As UstawKierunek = CType(kom, UstawKierunek)
                pol.WyslijKomunikat(New ZmienionoKierunek() With {.Adres = k.Adres, .Stan = ObecnyStanKierunku.Wyjazd})
                RaiseEvent OdebranoUstawKierunek(pol.AdresStacji, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.POTWIERDZ_KIERUNEK, New PrzetwOdebrKomunikatu(
            AddressOf PotwierdzKierunek.Otworz,
            Sub(pol, kom)
                Dim k As PotwierdzKierunek = CType(kom, PotwierdzKierunek)
                pol.WyslijKomunikat(New ZmienionoKierunek() With {.Adres = k.Adres, .Stan = ObecnyStanKierunku.Przyjazd})
                RaiseEvent OdebranoPotwierdzKierunek(pol.AdresStacji, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.DODAJ_POCIAG, New PrzetwOdebrKomunikatu(
            AddressOf DodajPociag.Otworz,
            Sub(pol, kom)
                Dim k As DodajPociag = CType(kom, DodajPociag)
                Dim obs As ObslugiwanyPosterunek = Nothing
                Dim stan As StanNadaniaNumeruPociagu = StanNadaniaNumeruPociagu.NrZajety
                Dim akt(0) As AktualizowanyKawalekToru

                SyncLock slockRezerwacjaPosterunku
                    KlienciPoAdresieStacji.TryGetValue(pol.AdresStacji, obs)
                End SyncLock

                If obs IsNot Nothing Then
                    Dim p As Drawing.Point = k.WspolrzedneKostki.Konwertuj

                    If obs.DanePulpitu.CzyKostkaNiepusta(p) AndAlso Kostka.CzyTorBezRozjazdu(obs.DanePulpitu.Kostki(p.X, p.Y).Typ) Then

                        SyncLock slockPociagi
                            If Not PociagiPoNumerze.ContainsKey(k.NrPociagu) Then
                                stan = StanNadaniaNumeruPociagu.Dobrze
                                akt(0) = New AktualizowanyKawalekToru() With {
                                    .Polozenie = PolozenieToru.TorGlowny,
                                    .Zajetosc = ZajetoscToru.Zajety,
                                    .WspolrzedneKostki = k.WspolrzedneKostki
                                }

                                PociagiPoNumerze.Add(k.NrPociagu, New Pociag() With {
                                    .DodajacyPosterunek = obs,
                                    .LiczbaOsi = k.LiczbaOsi,
                                    .Lokalizacja = obs,
                                    .Nazwa = k.Nazwa,
                                    .Numer = k.NrPociagu,
                                    .Sterowalny = k.PojazdSterowalny
                                 })

                            End If
                        End SyncLock

                    Else
                        stan = StanNadaniaNumeruPociagu.BledneWspolrzedne
                    End If
                End If

                pol.WyslijKomunikat(New DodanoPociag() With {.NrPociagu = k.NrPociagu, .Stan = stan})

                If stan = StanNadaniaNumeruPociagu.Dobrze Then
                    pol.WyslijKomunikat(New ZmienionoStanToru() With {.Tory = akt})

                    'RaiseEvent OdebranoDodajPociag(pol.AdresStacji, k)
                    RaiseEvent DodanoPociag(New StanPociagu With {
                        .DodajacyPosterunek = obs.NazwaPosterunku,
                        .LiczbaOsi = k.LiczbaOsi,
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
                        If poc.SterujacyPosterunek IsNot Nothing AndAlso poc.SterujacyPosterunek.Adres = pol.AdresStacji Then
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
                RaiseEvent OdebranoUstawPredkoscPociagu(pol.AdresStacji, k)
            End Sub
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

                Dim lista As List(Of SygnalizatorPowtarzajacy) = Nothing
                If SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego.TryGetValue(k.Adres, lista) Then
                    Dim stanPowt As StanSygnalizatora = StanSygnalizatora.BrakWyjazdu
                    If k.Stan = UstawianyStanSygnalizatora.Zezwalajacy Then stanPowt = StanSygnalizatora.Zezwalajacy

                    For Each powt As SygnalizatorPowtarzajacy In lista
                        pol.WyslijKomunikat(New ZmienionoStanSygnalizatora With {.Adres = powt.Adres, .Stan = stanPowt})
                    Next
                End If

                RaiseEvent OdebranoUstawStanSygnalizatora(pol.AdresStacji, k)
            End Sub
        ))

        DaneFabrykiObiektow.Add(TypKomunikatu.USTAW_ZWROTNICE, New PrzetwOdebrKomunikatu(
            AddressOf UstawZwrotnice.Otworz,
            Sub(pol, kom)
                Dim k As UstawZwrotnice = CType(kom, UstawZwrotnice)
                pol.WyslijKomunikat(New ZmienionoStanZwrotnicy() With {.Adres = k.Adres, .Stan = If(k.Ustawienie = UstawienieRozjazduEnum.Wprost, StanRozjazdu.Wprost, StanRozjazdu.Bok)})
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

        DaneFabrykiObiektow.Add(TypKomunikatu.ZWOLNIJ_PRZEBIEG, New PrzetwOdebrKomunikatu(
            AddressOf ZwolnijPrzebieg.Otworz,
            Sub(pol, kom)
                Dim k As ZwolnijPrzebieg = CType(kom, ZwolnijPrzebieg)
                pol.WyslijKomunikat(New ZmienionoStanSygnalizatora() With {.Adres = k.Adres, .Stan = StanSygnalizatora.BrakWyjazdu})
                RaiseEvent OdebranoZwolnijPrzebieg(pol.AdresStacji, k)
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
                    KlienciPoAdresieStacji.TryGetValue(pol.AdresStacji, post)
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
                Dim stan As StanWysiadzniecia = StanWysiadzniecia.BlednyNumer

                SyncLock slockPociagi
                    Dim poc As Pociag = Nothing

                    If PociagiPoNumerze.TryGetValue(k.NrPociagu, poc) Then
                        If poc.SterujacyPosterunek IsNot Nothing AndAlso poc.SterujacyPosterunek.Adres = pol.AdresStacji Then
                            poc.SterujacyPosterunek = Nothing
                            stan = StanWysiadzniecia.Wysiadznieto
                        Else
                            stan = StanWysiadzniecia.PociagNiesterowanyPrzezPosterunek
                        End If
                    End If
                End SyncLock

                pol.WyslijKomunikat(New WysiadznietoZPociagu() With {.NrPociagu = k.NrPociagu, .Stan = stan})

                If stan = StanWysiadzniecia.Wysiadznieto Then
                    RaiseEvent ZmienionoPosterunekSterujacyPociagiem(k.NrPociagu, Nothing)
                End If
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
                pol.WyslijKomunikat(New ZmienionoStanPrzejazdu() With {
                                    .Numer = k.Numer,
                                    .Stan = If(k.Stan = UstawianyStanPrzejazdu.Zamkniety, StanPrzejazduKolejowego.Zamkniety, StanPrzejazduKolejowego.Otwarty)})
                RaiseEvent OdebranoUstawStanPrzejazdu(pol.AdresStacji, k)
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
                    If poc.SterujacyPosterunek Is Nothing OrElse poc.SterujacyPosterunek.Adres = pol.AdresStacji Then
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
                KlienciPoAdresieStacji.TryGetValue(post.Value, obs)
                pol = obs?.Polaczenie
            End SyncLock
        End If

        pol?.WyslijKomunikat(New UsunietoPociag() With {.NrPociagu = NrPociagu, .Stan = stan})
        If stan = StanUsuwaniaPociagu.Usunieto Then RaiseEvent UsunietoPociag(NrPociagu)

        Return stan
    End Function

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
        SygnalizatoryPowtDlaSygnalizatoraPolsamoczynego = New Dictionary(Of UShort, List(Of SygnalizatorPowtarzajacy))
        Dim PosterunkiLista As New List(Of ObslugiwanyPosterunek)

        MaksymalnaPredkoscSieci = 0
        Dim polaczenia As PolaczeniaStacji = PolaczeniaStacji.OtworzPlik(SciezkaPliku, False)

        If polaczenia IsNot Nothing Then
            For Each pol As LaczonyPlikStacji In polaczenia.LaczanePliki
                Dim obs As New ObslugiwanyPosterunek() With {
                                       .NazwaPosterunku = pol.NazwaPosterunku,
                                       .NazwaPliku = pol.NazwaPliku,
                                       .Adres = pol.Adres,
                                       .Zawartosc = pol.ZawartoscPosterunku,
                                       .DanePulpitu = pol.DanePulpitu}

                KlienciPoAdresieStacji.Add(pol.Adres, obs)
                PosterunkiLista.Add(obs)
                ZnajdzMaksymalnaPredkoscSieci(pol.DanePulpitu)
                ZnajdzSygnalizatoryPowtarzajace(pol.DanePulpitu)
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

    Public Function PobierzStanPociagow() As StanPociagu()
        Dim lista As New List(Of StanPociagu)

        SyncLock slockPociagi
            For Each poc As KeyValuePair(Of UInteger, Pociag) In PociagiPoNumerze
                lista.Add(New StanPociagu() With {
                    .DodajacyPosterunek = poc.Value.DodajacyPosterunek.NazwaPosterunku,
                    .LiczbaOsi = poc.Value.LiczbaOsi,
                    .Lokalizacja = poc.Value.Lokalizacja.NazwaPosterunku,
                    .Nazwa = poc.Value.Nazwa,
                    .Numer = poc.Value.Numer,
                    .Sterowalny = poc.Value.Sterowalny,
                    .SterujacyPosterunek = poc.Value.SterujacyPosterunek?.NazwaPosterunku
                })
            Next
        End SyncLock

        Return lista.ToArray
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
            KlienciPoAdresieStacji.TryGetValue(post, pol)
        End SyncLock

        If pol IsNot Nothing Then pol.Polaczenie?.WyslijKomunikat(kom)
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
        If uw.Haslo = Haslo Then
            pol.UstawStanWyborPosterunku()

            Dim postLista As New List(Of DanePosterunku)
            For Each op As ObslugiwanyPosterunek In Posterunki
                postLista.Add(New DanePosterunku() With {
                              .Adres = op.Adres,
                              .Nazwa = op.NazwaPosterunku,
                              .Stan = If(op.Polaczenie Is Nothing, StanPosterunku.Wolny, StanPosterunku.Zajety)})
            Next
            pol.WyslijKomunikat(New UwierzytelnionoPoprawnie() With {.PredkoscMaksymalna = MaksymalnaPredkoscSieci, .Posterunki = postLista.ToArray})

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

    Private Sub ZnajdzMaksymalnaPredkoscSieci(p As Pulpit)
        p.PrzeiterujKostki(Sub(x, y, k)
                               Dim t As Tor = TryCast(k, Tor)
                               Dim r As Rozjazd = TryCast(k, Rozjazd)

                               If t IsNot Nothing AndAlso t.PredkoscZasadnicza > MaksymalnaPredkoscSieci Then
                                   MaksymalnaPredkoscSieci = t.PredkoscZasadnicza
                               End If

                               If r IsNot Nothing AndAlso r.PredkoscBoczna > MaksymalnaPredkoscSieci Then
                                   MaksymalnaPredkoscSieci = r.PredkoscBoczna
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
End Class