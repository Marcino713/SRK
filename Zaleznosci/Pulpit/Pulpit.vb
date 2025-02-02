Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPulpitu, Zaleznosci.KonfiguracjaOdczytuPulpitu)
Imports SegmPliku = Zaleznosci.SegmentPliku(Of Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPulpitu, Zaleznosci.KonfiguracjaOdczytuPulpitu))

Public Class Pulpit
    Public Delegate Sub PrzetworzKostkeZObiektem(Of T)(x As Integer, y As Integer, k As Kostka, o As T)
    Public Delegate Sub PrzetworzKostke(x As Integer, y As Integer, k As Kostka)
    Private Delegate Function CzyZgodnyTypKostki(k As Kostka) As Boolean
    Private Delegate Function UtworzObiektPulpitu(dane As Byte(), konf As KonfiguracjaOdczytuPulpitu) As IObiektPlikuTyp

    Public Const ROZSZERZENIE_PLIKU As String = ".stacja"
    Public Const ROZMIAR_DOMYSLNY As Integer = 10
    Public Shared ReadOnly ObslugiwaneWersje As WersjaPliku() = {New WersjaPliku(0, 1)}
    Private Const NAGLOWEK As String = "STAC"

    Private Shared ReadOnly TWORZYCIELE_OBIEKTOW As New Dictionary(Of UShort, UtworzObiektPulpitu) From {
        {TypObiektuPlikuPulpitu.KOSTKA, AddressOf Kostka.UtworzObiekt},
        {TypObiektuPlikuPulpitu.ODCINEK_TORU, AddressOf OdcinekToru.UtworzObiekt},
        {TypObiektuPlikuPulpitu.LICZNIK_OSI, AddressOf ParaLicznikowOsi.UtworzObiekt},
        {TypObiektuPlikuPulpitu.LAMPA, AddressOf Lampa.UtworzObiekt},
        {TypObiektuPlikuPulpitu.PRZEJAZD_KOLEJOWO_DROGOWY, AddressOf PrzejazdKolejowoDrogowy.UtworzObiekt}
    }

    Public ReadOnly Property Wersja As New WersjaPliku(0, 1)

    Private _SciezkaPliku As String = ""
    Public ReadOnly Property SciezkaPliku As String
        Get
            Return _SciezkaPliku
        End Get
    End Property

    Public Property Adres As UShort = 0
    Public Property Nazwa As String = ""
    Public Property SkrotTelegraficzny As String = ""
    Public Property Typ As TypPosterunku = TypPosterunku.Inny

    Private _DataUtworzenia As Date
    Public ReadOnly Property DataUtworzenia As Date
        Get
            Return _DataUtworzenia
        End Get
    End Property

    Private _Szerokosc As Integer
    Public ReadOnly Property Szerokosc As Integer
        Get
            Return _Szerokosc
        End Get
    End Property

    Private _Wysokosc As Integer
    Public ReadOnly Property Wysokosc As Integer
        Get
            Return _Wysokosc
        End Get
    End Property

    Private _Kostki As Kostka(,)
    Public ReadOnly Property Kostki As Kostka(,)
        Get
            Return _Kostki
        End Get
    End Property

    Private _Lampy As New List(Of Lampa)
    Public ReadOnly Property Lampy As List(Of Lampa)
        Get
            Return _Lampy
        End Get
    End Property

    Private _Odcinki As New List(Of OdcinekToru)
    Public ReadOnly Property OdcinkiTorow As List(Of OdcinekToru)
        Get
            Return _Odcinki
        End Get
    End Property

    Private _LicznikiOsi As New List(Of ParaLicznikowOsi)
    Public ReadOnly Property LicznikiOsi As List(Of ParaLicznikowOsi)
        Get
            Return _LicznikiOsi
        End Get
    End Property

    Private _Przejazdy As New List(Of PrzejazdKolejowoDrogowy)
    Public ReadOnly Property Przejazdy As List(Of PrzejazdKolejowoDrogowy)
        Get
            Return _Przejazdy
        End Get
    End Property

    Public Sub New()
        Me.New(ROZMIAR_DOMYSLNY, ROZMIAR_DOMYSLNY)
    End Sub

    Public Sub New(szer As Integer, wys As Integer)
        _Szerokosc = szer
        _Wysokosc = wys
        ReDim _Kostki(_Szerokosc - 1, _Wysokosc - 1)
        _DataUtworzenia = Now
    End Sub

    Public Sub New(wersja As WersjaPliku, szer As Integer, wys As Integer)
        Me.New(szer, wys)

        If Not wersja.CzyObslugiwana(ObslugiwaneWersje) Then Throw New OtwieraniePlikuException("Wersja pliku jest nieobsługiwana.")

        Me.Wersja = wersja
    End Sub

    Public Shared Function Otworz(sciezka As String) As Pulpit
        Try
            Using fs As New FileStream(sciezka, FileMode.Open, FileAccess.Read)
                Dim p As Pulpit = _Otworz(fs)
                p._SciezkaPliku = sciezka
                Return p
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Public Shared Function Otworz(zawartosc As Byte()) As Pulpit
        Try
            Using ms As New MemoryStream(zawartosc)
                Return _Otworz(ms)
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Public Function Zapisz() As Boolean
        Return Zapisz(SciezkaPliku)
    End Function

    Public Function Zapisz(sciezka As String) As Boolean
        _SciezkaPliku = sciezka
        If sciezka Is Nothing Then Return False

        Try
            Return _Zapisz()
        Catch
            Return False
        End Try
    End Function

    Public Sub SortujLampyAdresRosnaco()
        _Lampy = _Lampy.OrderBy(Function(l) l.Adres).ToList()
    End Sub

    Public Sub SortujOdcinkiNazwaRosnaco()
        _Odcinki = _Odcinki.OrderBy(Function(o) o.Nazwa).ToList
    End Sub

    Public Sub SortujLicznikiAdres1Rosnaco()
        _LicznikiOsi = _LicznikiOsi.OrderBy(Function(l) l.Adres1).ToList
    End Sub

    Public Sub SortujPrzejazdyNazwaRosnaco()
        _Przejazdy = _Przejazdy.OrderBy(Function(p) p.Nazwa).ToList
    End Sub

    Public Function PobierzSygnalizatory() As Dictionary(Of UShort, Kostka)
        Return PobierzKostki(Of Kostka)(AddressOf Kostka.CzySygnalizator)
    End Function

    Public Function PobierzRozjazdy() As Dictionary(Of UShort, Rozjazd)
        Return PobierzKostki(Of Rozjazd)(AddressOf Kostka.CzyRozjazd)
    End Function

    Public Function PobierzKierunkiPoAdresieOdcinka() As Dictionary(Of UShort, Kierunek)
        Dim slownik As New Dictionary(Of UShort, Kierunek)

        PrzeiterujKostki(Sub(x, y, k)
                             If k.Typ = TypKostki.Kierunek Then
                                 Dim kier As Kierunek = CType(k, Kierunek)

                                 If kier.NalezyDoOdcinka IsNot Nothing Then
                                     Dim adr As UShort = kier.NalezyDoOdcinka.Adres
                                     If Not slownik.ContainsKey(adr) Then
                                         slownik.Add(adr, kier)
                                     End If
                                 End If
                             End If
                         End Sub)

        Return slownik
    End Function

    Public Function PobierzPrzyciskiSygnalizatorowIZwrotnic() As Dictionary(Of UShort, HashSet(Of IPrzycisk))
        Dim slownik As New Dictionary(Of UShort, HashSet(Of IPrzycisk))

        PrzeiterujKostki(Sub(x, y, k)
                             If k.Typ = TypKostki.Przycisk Then
                                 Dim p As Przycisk = CType(k, Przycisk)

                                 If p.TypPrzycisku = TypPrzyciskuEnum.SygnalZastepczy Or p.TypPrzycisku = TypPrzyciskuEnum.ZwolnieniePrzebiegu Or p.TypPrzycisku = TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego Then
                                     If p.SygnalizatorPolsamoczynny IsNot Nothing Then DodajPrzyciskDoSlownika(slownik, p.SygnalizatorPolsamoczynny.Adres, p)
                                 ElseIf p.TypPrzycisku = TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnManewrowego Then
                                     If p.SygnalizatorManewrowy IsNot Nothing Then DodajPrzyciskDoSlownika(slownik, p.SygnalizatorManewrowy.Adres, p)
                                 ElseIf p.TypPrzycisku = TypPrzyciskuEnum.KasowanieRozprucia Then
                                     If p.Rozjazd IsNot Nothing Then DodajPrzyciskDoSlownika(slownik, p.Rozjazd.Adres, p)
                                 End If

                             ElseIf k.Typ = TypKostki.PrzyciskTor Then
                                 Dim p As PrzyciskTor = CType(k, PrzyciskTor)

                                 If p.TypPrzycisku = TypPrzyciskuTorEnum.ManewrySygnalizatorManewrowy Then
                                     If p.SygnalizatorManewrowy IsNot Nothing Then DodajPrzyciskDoSlownika(slownik, p.SygnalizatorManewrowy.Adres, p)
                                 Else
                                     If p.SygnalizatorPolsamoczynny IsNot Nothing Then DodajPrzyciskDoSlownika(slownik, p.SygnalizatorPolsamoczynny.Adres, p)
                                 End If

                             End If
                         End Sub)
        Return slownik
    End Function

    Public Function PobierzKostkiZeWspolrzednymi() As Dictionary(Of Kostka, Punkt)
        Dim slownik As New Dictionary(Of Kostka, Punkt)
        PrzeiterujKostki(Sub(x, y, k) slownik.Add(k, New Punkt(x, y)))
        Return slownik
    End Function

    Public Function PobierzLampy() As Dictionary(Of UShort, Lampa)
        Dim dict As New Dictionary(Of UShort, Lampa)

        For Each l As Lampa In Lampy
            If Not dict.ContainsKey(l.Adres) Then
                dict.Add(l.Adres, l)
            End If
        Next

        Return dict
    End Function

    Public Function PobierzOdcinkiTorow() As Dictionary(Of UShort, OdcinekToru)
        Dim slownik As New Dictionary(Of UShort, OdcinekToru)

        For Each o As OdcinekToru In _Odcinki
            If Not slownik.ContainsKey(o.Adres) Then
                slownik.Add(o.Adres, o)
            End If
        Next

        Return slownik
    End Function

    Public Function PobierzPrzejazdyKolejowoDrogowe() As Dictionary(Of UShort, PrzejazdKolejowoDrogowy)
        Dim slownik As New Dictionary(Of UShort, PrzejazdKolejowoDrogowy)

        For Each p As PrzejazdKolejowoDrogowy In _Przejazdy
            If Not slownik.ContainsKey(p.Numer) Then
                slownik.Add(p.Numer, p)
            End If
        Next

        Return slownik
    End Function

    Public Sub PrzeiterujKostki(Of T)(metoda As PrzetworzKostkeZObiektem(Of T), obiekt As T)
        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                Dim k As Kostka = _Kostki(x, y)
                If k IsNot Nothing Then metoda(x, y, k, obiekt)
            Next
        Next
    End Sub

    Public Sub PrzeiterujKostki(metoda As PrzetworzKostke)
        PrzeiterujKostki(Of Object)(Sub(x, y, k, o) metoda(x, y, k), Nothing)
    End Sub

    Public Function ZnajdzKostke(kostka As Kostka) As Punkt
        If kostka Is Nothing Then Return Nothing

        Dim p As Punkt = Nothing
        PrzeiterujKostki(Sub(x, y, k)
                             If k Is kostka Then
                                 p = New Punkt(x, y)
                             End If
                         End Sub)
        Return p
    End Function

    Public Function CzyKostkaWZakresiePulpitu(wspolrzedne As PunktCalkowity) As Boolean
        Return wspolrzedne.X >= 0 And wspolrzedne.X < _Szerokosc And wspolrzedne.Y >= 0 And wspolrzedne.Y < _Wysokosc
    End Function

    Public Function CzyKostkaWZakresiePulpitu(wspolrzedne As Punkt) As Boolean
        Return wspolrzedne.X < _Szerokosc And wspolrzedne.Y < _Wysokosc
    End Function

    Public Function CzyKostkaNiepusta(wspolrzedne As PunktCalkowity) As Boolean
        Return CzyKostkaWZakresiePulpitu(wspolrzedne) AndAlso _Kostki(wspolrzedne.X, wspolrzedne.Y) IsNot Nothing
    End Function

    Public Function CzyKostkaNiepusta(wspolrzedne As Punkt) As Boolean
        Return CzyKostkaWZakresiePulpitu(wspolrzedne) AndAlso _Kostki(wspolrzedne.X, wspolrzedne.Y) IsNot Nothing
    End Function

    Public Sub UsunKostke(kostka As Kostka)
        Dim tor As Tor = TryCast(kostka, Tor)
        tor?.NalezyDoOdcinka?.UsunTor(tor, PrzynaleznoscToruDoOdcinka.Oba)

        Dim podw As TorPodwojnyNiezalezny = TryCast(kostka, TorPodwojnyNiezalezny)
        podw?.NalezyDoOdcinkaDrugi?.UsunTor(podw, PrzynaleznoscToruDoOdcinka.Oba)

        Dim przejazd As PrzejazdKolejowoDrogowyKostka = TryCast(kostka, PrzejazdKolejowoDrogowyKostka)
        przejazd?.NalezyDoPrzejazdu?.KostkiPrzejazdy.Remove(przejazd)

        Dim sygnTop As SygnalizatorOstrzegawczyPrzejazdowy = TryCast(kostka, SygnalizatorOstrzegawczyPrzejazdowy)
        If sygnTop IsNot Nothing Then
            For Each p As PrzejazdKolejowoDrogowy In _Przejazdy
                p.UsunSygnalizatorZPowiazan(sygnTop)
            Next
        End If

        PrzeiterujKostki(Sub(x, y, k)
                             k.UsunPowiazanie(kostka)
                             If k Is kostka Then _Kostki(x, y) = Nothing
                         End Sub)
    End Sub

    Public Sub UsunOdcinekToru(odcinek As OdcinekToru)
        _Odcinki.Remove(odcinek)

        PrzeiterujKostki(Sub(x, y, k) k.UsunOdcinekToruZPowiazan(odcinek))

        For Each p As ParaLicznikowOsi In _LicznikiOsi
            p.UsunOdcinekToruZPowiazan(odcinek)
        Next

        For Each p As PrzejazdKolejowoDrogowy In _Przejazdy
            p.UsunOdcinekToruZPowiazan(odcinek)
        Next
    End Sub

    Public Sub UsunPrzejazdKolejowoDrogowy(przejazd As PrzejazdKolejowoDrogowy)
        _Przejazdy.Remove(przejazd)

        PrzeiterujKostki(Sub(x, y, k) k.UsunPrzejazdZPowiazan(przejazd))
    End Sub

    Public Function ZnajdzMaksymalnaPredkoscSieci() As UShort
        Dim maks As UShort

        PrzeiterujKostki(Sub(x, y, k)
                             Dim t As Tor = TryCast(k, Tor)
                             Dim p As TorPodwojny = TryCast(k, TorPodwojny)

                             If t IsNot Nothing AndAlso t.Predkosc > maks Then
                                 maks = t.Predkosc
                             End If

                             If p IsNot Nothing AndAlso p.PredkoscDrugi > maks Then
                                 maks = p.PredkoscDrugi
                             End If
                         End Sub)

        Return maks
    End Function

    Public Sub PowiekszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer)
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        Dim szernowa As Integer = _Szerokosc
        Dim wysnowa As Integer = _Wysokosc
        Dim przesx As Integer = 0
        Dim przesy As Integer = 0

        If kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo Then
            szernowa += rozmiar
            If kierunek = KierunekEdycjiPulpitu.Lewo Then przesx = rozmiar
        Else
            wysnowa += rozmiar
            If kierunek = KierunekEdycjiPulpitu.Gora Then przesy = rozmiar
        End If

        'Przesuń kostki
        Dim tab(szernowa - 1, wysnowa - 1) As Kostka
        PrzeiterujKostki(Sub(x, y, k) tab(x + przesx, y + przesy) = k)
        _Kostki = tab
        _Szerokosc = szernowa
        _Wysokosc = wysnowa

        'Przesuń obiekty punktowe
        PrzesunObiektyPunktowe(kierunek, rozmiar)
    End Sub

    Public Function PomniejszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer) As List(Of ObiektBlokujacyZmniejszaniePulpitu)
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        If (kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo) And rozmiar >= _Szerokosc Then
            Throw New ArgumentException("Rozmiar musi być mniejszy niż szerokość pulpitu.")
        End If

        If (kierunek = KierunekEdycjiPulpitu.Gora Or kierunek = KierunekEdycjiPulpitu.Dol) And rozmiar >= _Wysokosc Then
            Throw New ArgumentException("Rozmiar musi być mniejszy niż wysokość pulpitu.")
        End If

        'Oblicz parametry zmniejszania pulpitu
        Dim poczx As Integer = 0
        Dim koncx As Integer = _Szerokosc - 1
        Dim poczy As Integer = 0
        Dim koncy As Integer = _Wysokosc - 1
        Dim przesx As Integer = 0
        Dim przesy As Integer = 0
        Dim wysnowa As Integer = _Wysokosc
        Dim szernowa As Integer = _Szerokosc
        Dim wspolrzednaGraniczna As Single = rozmiar
        Dim wynik As New List(Of ObiektBlokujacyZmniejszaniePulpitu)

        Select Case kierunek
            Case KierunekEdycjiPulpitu.Gora
                wysnowa -= rozmiar
                koncy = rozmiar - 1
                przesy = rozmiar

            Case KierunekEdycjiPulpitu.Prawo
                szernowa -= rozmiar
                poczx = szernowa
                wspolrzednaGraniczna = _Szerokosc - rozmiar

            Case KierunekEdycjiPulpitu.Dol
                wysnowa -= rozmiar
                poczy = wysnowa
                wspolrzednaGraniczna = _Wysokosc - rozmiar

            Case KierunekEdycjiPulpitu.Lewo
                szernowa -= rozmiar
                koncx = rozmiar - 1
                przesx = rozmiar

        End Select

        'Sprawdź, czy są jakieś kostki
        For x As Integer = poczx To koncx
            For y As Integer = poczy To koncy
                If _Kostki(x, y) IsNot Nothing Then
                    wynik.Add(New ObiektBlokujacyZmniejszaniePulpitu(RodzajObiektuBlokujacegoZmniejszaniePulpitu.Kostka, x, y))
                End If
            Next
        Next

        'Sprawdź, czy są jakieś liczniki
        For Each p As ParaLicznikowOsi In _LicznikiOsi
            If CzyPunktWUsuwanymZakresie(p.X1, p.Y1, kierunek, wspolrzednaGraniczna) Or
               CzyPunktWUsuwanymZakresie(p.X2, p.Y2, kierunek, wspolrzednaGraniczna) Then
                wynik.Add(New ObiektBlokujacyZmniejszaniePulpitu(RodzajObiektuBlokujacegoZmniejszaniePulpitu.LicznikOsi, p.Adres1, p.Adres2))
            End If
        Next

        'Sprawdź, czy są jakieś lampy
        For Each l As Lampa In _Lampy
            If CzyPunktWUsuwanymZakresie(l.X, l.Y, kierunek, wspolrzednaGraniczna) Then
                wynik.Add(New ObiektBlokujacyZmniejszaniePulpitu(RodzajObiektuBlokujacegoZmniejszaniePulpitu.Lampa, l.Adres, 0))
            End If
        Next

        'Sprawdź, czy są elementy przejazdów
        For Each p As PrzejazdKolejowoDrogowy In _Przejazdy
            For Each r As PrzejazdElementWykonawczy In p.Rogatki
                If CzyPunktWUsuwanymZakresie(r.X, r.Y, kierunek, wspolrzednaGraniczna) Then
                    wynik.Add(New ObiektBlokujacyZmniejszaniePulpitu(RodzajObiektuBlokujacegoZmniejszaniePulpitu.PrzejazdRogatka, p.Numer, r.Adres))
                End If
            Next

            For Each s As PrzejazdElementWykonawczy In p.SygnalizatoryDrogowe
                If CzyPunktWUsuwanymZakresie(s.X, s.Y, kierunek, wspolrzednaGraniczna) Then
                    wynik.Add(New ObiektBlokujacyZmniejszaniePulpitu(RodzajObiektuBlokujacegoZmniejszaniePulpitu.PrzejazdSygnalizatorDrogowy, p.Numer, s.Adres))
                End If
            Next
        Next

        'Usuwany zakres jest pusty, można usunąć
        If wynik.Count = 0 Then
            Dim tab(szernowa - 1, wysnowa - 1) As Kostka
            PrzeiterujKostki(Sub(x, y, k) tab(x - przesx, y - przesy) = k)
            _Kostki = tab
            _Szerokosc = szernowa
            _Wysokosc = wysnowa

            PrzesunObiektyPunktowe(kierunek, -rozmiar)

            Return Nothing
        Else
            Return wynik
        End If
    End Function

    Private Sub PrzesunObiektyPunktowe(kierunek As KierunekEdycjiPulpitu, rozm As Single)
        Dim x As Single = 0.0F
        Dim y As Single = 0.0F

        Select Case kierunek
            Case KierunekEdycjiPulpitu.Lewo
                x = rozm
            Case KierunekEdycjiPulpitu.Gora
                y = rozm
            Case Else
                Exit Sub
        End Select

        'Przesuń liczniki
        For Each p As ParaLicznikowOsi In _LicznikiOsi
            p.X1 += x
            p.X2 += x
            p.Y1 += y
            p.Y2 += y
        Next

        'Przesuń lampy
        For Each l As Lampa In _Lampy
            l.X += x
            l.Y += y
        Next

        'Przejazdy
        For Each p As PrzejazdKolejowoDrogowy In _Przejazdy

            'Przesuń rogatki
            For Each r As PrzejazdElementWykonawczy In p.Rogatki
                r.X += x
                r.Y += y
            Next

            'Przesuń sygnalizatory drogowe
            For Each s As PrzejazdElementWykonawczy In p.SygnalizatoryDrogowe
                s.X += x
                s.Y += y
            Next
        Next
    End Sub

    Private Function _Zapisz() As Boolean
        Dim konf As New KonfiguracjaZapisuPulpitu
        Dim ix As Integer = 1

        PrzeiterujKostki(Sub(x, y, k)
                             konf.Kostki.Add(k, ix)
                             ix += 1
                         End Sub)

        ix = 1
        For Each o As OdcinekToru In _Odcinki
            konf.OdcinkiTorow.Add(o, ix)
            ix += 1
        Next

        ix = 1
        For Each p As PrzejazdKolejowoDrogowy In _Przejazdy
            konf.Przejazdy.Add(p, ix)
            ix += 1
        Next

        Using fs As New FileStream(_SciezkaPliku, FileMode.Create, FileAccess.Write)
            Using bw As New BinaryWriter(fs)

                'Nagłówek
                bw.Write(PobierzBajty(NAGLOWEK))
                bw.Write(Wersja.WersjaGlowna)
                bw.Write(Wersja.WersjaBoczna)
                bw.Write(CUShort(_Szerokosc))
                bw.Write(CUShort(_Wysokosc))

                'Informacje o posterunku
                bw.Write(DataUtworzenia.ToBinary)
                bw.Write(Adres)
                ZapiszTekst(bw, Nazwa)
                ZapiszTekst(bw, SkrotTelegraficzny)
                bw.Write(Typ)

                'Pulpit
                PrzeiterujKostki(Sub(x, y, k)
                                     konf.X = CUShort(x)
                                     konf.Y = CUShort(y)
                                     ZapiszObiekt(bw, k, TypObiektuPlikuPulpitu.KOSTKA, konf)
                                 End Sub)

                'Inne obiekty
                ZapiszObiekty(bw, _Odcinki, TypObiektuPlikuPulpitu.ODCINEK_TORU, konf)
                ZapiszObiekty(bw, _LicznikiOsi, TypObiektuPlikuPulpitu.LICZNIK_OSI, konf)
                ZapiszObiekty(bw, _Lampy, TypObiektuPlikuPulpitu.LAMPA, konf)
                ZapiszObiekty(bw, _Przejazdy, TypObiektuPlikuPulpitu.PRZEJAZD_KOLEJOWO_DROGOWY, konf)

            End Using
        End Using

        Return True
    End Function

    Private Sub ZapiszObiekt(bw As BinaryWriter, obiekt As IObiektPlikuTyp, typ As UShort, konf As KonfiguracjaZapisuPulpitu)
        Dim dane As Byte() = obiekt.Zapisz(konf)
        bw.Write(typ)
        bw.Write(CUShort(dane.Length))
        bw.Write(dane)
    End Sub

    Private Sub ZapiszObiekty(bw As BinaryWriter, obiekty As IEnumerable(Of IObiektPlikuTyp), typ As UShort, konf As KonfiguracjaZapisuPulpitu)
        For Each o As IObiektPlikuTyp In obiekty
            ZapiszObiekt(bw, o, typ, konf)
        Next
    End Sub

    Private Shared Function _Otworz(str As Stream) As Pulpit
        Dim p As Pulpit
        Dim konf As New KonfiguracjaOdczytuPulpitu
        Dim segmenty As New List(Of SegmPliku)
        konf.Kostki.Add(PUSTE_ODWOLANIE, Nothing)
        konf.OdcinkiTorow.Add(PUSTE_ODWOLANIE, Nothing)
        konf.Przejazdy.Add(PUSTE_ODWOLANIE, Nothing)

        Using br As New BinaryReader(str)
            Dim b As Byte()

            'Nagłówek
            b = br.ReadBytes(NAGLOWEK.Length)
            If PobierzTekst(b) <> NAGLOWEK Then
                Throw New OtwieraniePlikuException("Niepoprawny typ pliku.")
            End If

            Dim wersja_glowna As UShort = br.ReadUInt16
            Dim wersja_boczna As UShort = br.ReadUInt16
            Dim szer As UShort = br.ReadUInt16
            Dim wys As UShort = br.ReadUInt16
            p = New Pulpit(New WersjaPliku(wersja_glowna, wersja_boczna), szer, wys)
            konf.Pulpit = p

            'Informacje o posterunku
            Dim data As Long = br.ReadInt64
            p._DataUtworzenia = Date.FromBinary(data)
            p.Adres = br.ReadUInt16
            p.Nazwa = OdczytajTekst(br)
            p.SkrotTelegraficzny = OdczytajTekst(br)
            p.Typ = CType(br.ReadInt32(), TypPosterunku)

            'Pulpit
            Do Until str.Position >= str.Length
                Dim seg As SegmPliku = UtworzObiekt(br, konf)
                If seg IsNot Nothing Then segmenty.Add(seg)
            Loop

        End Using

        For Each s As SegmPliku In segmenty
            s.Obiekt.Otworz(s.Dane, konf)
        Next

        Return p
    End Function

    Private Shared Function UtworzObiekt(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu) As SegmPliku
        Dim typ As UShort = br.ReadUInt16
        Dim ile As UShort = br.ReadUInt16
        Dim b As Byte() = br.ReadBytes(ile)
        Dim ob As IObiektPlikuTyp = Nothing
        Dim metodaTworzaca As UtworzObiektPulpitu = Nothing

        If TWORZYCIELE_OBIEKTOW.TryGetValue(typ, metodaTworzaca) Then
            ob = metodaTworzaca(b, konf)
            If ob IsNot Nothing Then Return New SegmPliku() With {.Dane = b, .Obiekt = ob}
        End If

        Return Nothing
    End Function

    Private Function PobierzKostki(Of T As Kostka)(sprTypu As CzyZgodnyTypKostki) As Dictionary(Of UShort, T)
        Dim slownik As New Dictionary(Of UShort, T)

        PrzeiterujKostki(Sub(x, y, k)
                             If sprTypu(k) Then
                                 Dim adr As UShort = CType(k, IAdres).Adres
                                 If Not slownik.ContainsKey(adr) Then
                                     slownik.Add(adr, CType(k, T))
                                 End If
                             End If
                         End Sub)

        Return slownik
    End Function

    Private Function CzyPunktWUsuwanymZakresie(x As Single, y As Single, kierunek As KierunekEdycjiPulpitu, wspolrzednaGraniczna As Single) As Boolean
        Return _
            (kierunek = KierunekEdycjiPulpitu.Gora And y < wspolrzednaGraniczna) Or
            (kierunek = KierunekEdycjiPulpitu.Prawo And x > wspolrzednaGraniczna) Or
            (kierunek = KierunekEdycjiPulpitu.Dol And y > wspolrzednaGraniczna) Or
            (kierunek = KierunekEdycjiPulpitu.Lewo And x < wspolrzednaGraniczna)
    End Function

    Private Sub DodajPrzyciskDoSlownika(slownik As Dictionary(Of UShort, HashSet(Of IPrzycisk)), adres As UShort, przycisk As IPrzycisk)
        Dim przyciski As HashSet(Of IPrzycisk) = Nothing

        If Not slownik.TryGetValue(adres, przyciski) Then
            przyciski = New HashSet(Of IPrzycisk)
            slownik.Add(adres, przyciski)
        End If

        przyciski.Add(przycisk)
    End Sub
End Class

Friend Class TypObiektuPlikuPulpitu
    Friend Const KOSTKA As UShort = 1
    Friend Const ODCINEK_TORU As UShort = 2
    Friend Const LICZNIK_OSI As UShort = 3
    Friend Const LAMPA As UShort = 4
    Friend Const PRZEJAZD_KOLEJOWO_DROGOWY As UShort = 5
End Class

Public Class ObiektBlokujacyZmniejszaniePulpitu
    Public Typ As RodzajObiektuBlokujacegoZmniejszaniePulpitu
    Public Liczba1 As Integer
    Public Liczba2 As Integer

    Public Sub New(typ As RodzajObiektuBlokujacegoZmniejszaniePulpitu, liczba1 As Integer, liczba2 As Integer)
        Me.Typ = typ
        Me.Liczba1 = liczba1
        Me.Liczba2 = liczba2
    End Sub
End Class

Public Enum KierunekEdycjiPulpitu
    Gora
    Prawo
    Dol
    Lewo
End Enum

Public Enum RodzajObiektuBlokujacegoZmniejszaniePulpitu
    Kostka
    LicznikOsi
    Lampa
    PrzejazdRogatka
    PrzejazdSygnalizatorDrogowy
End Enum

<Flags>
Public Enum TypPosterunku
    Inny = &H0
    BocznicaStacyjna = &H1
    BocznicaSzlakowa = &H2
    GrupaTorowTowarowych = &H4
    Ladownia = &H8
    Mijanka = &H10
    PosterunekBocznicowyStacyjny = &H20
    PosterunekBocznicowySzlakowy = &H40
    PosterunekOdgalezny = &H80
    PosterunekOdstepowy = &H100
    PrzejscieGraniczne = &H200
    PrzystanekOsobowy = &H400
    PrzystanekSluzbowy = &H800
    PunktPrzeladunkowy = &H1000
    Stacja = &H2000
    StacjaTechniczna = &H4000
End Enum