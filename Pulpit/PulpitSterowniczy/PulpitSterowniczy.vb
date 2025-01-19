Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports IPunktSingle = Zaleznosci.IObiektPunktowy(Of Single)

Public Class PulpitSterowniczy
    Private Const PUSTA_WSPOLRZEDNA As Integer = -1
    Private Const SKALOWANIE_DOMYSLNE As Single = 50.0F
    Private Const SKALOWANIE_ZMIANA As Single = 0.05F
    Private Const SKALOWANIE_MIN As Single = 30.0F
    Private Const SKALOWANIE_MAX As Single = 200.0F
    Private Const POL As Single = 0.5F
    Private Const JEDNA_TRZECIA As Single = 1.0F / 3.0F
    Private Const ZWIEKSZ_OBROT As Integer = 90
    Private Const KAT_PELNY As Integer = 360
    Private Const CZAS_WCISKANIA_PRZYCISKU_MS As Integer = 300
    Private Const KATEG_KONF As String = "Konfiguracja"
    Private Const KATEG_ZDARZ As String = "Zdarzenia trybu działania"
    Private Const KATEG_ZDARZ_PROJ As String = "Zdarzenia trybu projektowego"
    Private ReadOnly KLAWISZE_STRZALEK As New HashSet(Of Keys) From {Keys.Up, Keys.Down, Keys.Left, Keys.Right}

    Public PoczatekZaznaczeniaLamp As PointF
    Public KoniecZaznaczeniaLamp As PointF
    Public ZaznaczonyLicznikOsiAdres As UShort?
    Private PoprzZaznLampyWObszarze As HashSet(Of Zaleznosci.Lampa)     'Nothing - tryb zaznaczania pojedynczej lampy; niepusty obiekt - tryb zaznaczania obszarem
    Private KolejnoscZaznaczeniaLamp As New List(Of Zaleznosci.Lampa)
    Private PoprzedniPunkt As New Point(PUSTA_WSPOLRZEDNA, PUSTA_WSPOLRZEDNA)
    Private PoprzLokalizacjaKostki As New Zaleznosci.PunktCalkowity(PUSTA_WSPOLRZEDNA, PUSTA_WSPOLRZEDNA)
    Private WcisnietyPrzycisk As Zaleznosci.IPrzycisk = Nothing
    Private RysowanieWlaczone As Boolean = True
    Private Migacz As Zaleznosci.IMigacz
    Private TabliczkiZamkniecOdcinkow As New Dictionary(Of UShort, Zaleznosci.PunktCalkowity)
    Private MenuZwrotnica As Zaleznosci.Rozjazd
    Private MenuSygnalizator As Zaleznosci.Sygnalizator
    Private MenuOdcinek As Zaleznosci.OdcinekToru
    Private MenuOdcinekPunkt As Zaleznosci.PunktCalkowity

    Private actWlaczZegarMigania As Action = Sub() tmrMiganie.Start()
    Private actWylaczZegarMigania As Action = Sub() tmrMiganie.Stop()

    <Browsable(False)>
    Public ReadOnly Property SzerokoscPulpitu As Integer
        Get
            Return CInt(Pulpit.Szerokosc * Skalowanie) + 1
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property WysokoscPulpitu As Integer
        Get
            Return CInt(Pulpit.Wysokosc * Skalowanie) + 1
        End Get
    End Property

    Private _Rysownik As IRysownik = New PulpitKlasycznyGDI
    <Browsable(False)>
    Public ReadOnly Property Rysownik As IRysownik
        Get
            Return _Rysownik
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property WysokiStanMigania As Boolean
        Get
            If Migacz Is Nothing Then Return True
            Return Migacz.WysokiStanMigania
        End Get
    End Property

    <Description("Czy kontrolka działa w trybie projektowania posterunku ruchu"), Category(KATEG_KONF), DefaultValue(False)>
    Public Property TrybProjektowy As Boolean = False

    Private _RysujKrawedzieKostek As Boolean = True
    <Description("Czy rysować krawędzie kostek"), Category(KATEG_KONF), DefaultValue(True)>
    Public Property RysujKrawedzieKostek As Boolean
        Get
            Return _RysujKrawedzieKostek
        End Get
        Set(value As Boolean)
            _RysujKrawedzieKostek = value
            Invalidate()
        End Set
    End Property

    Private _RysujWspolrzedne As Boolean = True
    <Description("Czy rysować współrzędne pulpitu"), Category(KATEG_KONF), DefaultValue(True)>
    Public Property RysujWspolrzedne As Boolean
        Get
            Return _RysujWspolrzedne
        End Get
        Set(value As Boolean)
            _RysujWspolrzedne = value
            Invalidate()
        End Set
    End Property

    Private _Skalowanie As Single = SKALOWANIE_DOMYSLNE
    <Description("Skalowanie pulpitu"), Category(KATEG_KONF), DefaultValue(SKALOWANIE_DOMYSLNE)>
    Public Property Skalowanie As Single
        Get
            Return _Skalowanie
        End Get
        Set(value As Single)
            If value < SKALOWANIE_MIN Then value = SKALOWANIE_MIN
            If value > SKALOWANIE_MAX Then value = SKALOWANIE_MAX

            If value <> _Skalowanie Then
                _Skalowanie = value
                Invalidate()
            End If
        End Set
    End Property

    Private _Przesuniecie As New Point(0, 0)
    <Description("Przesunięcie pulpitu względem początku kontrolki"), Category(KATEG_KONF)>
    Public Property Przesuniecie As Point
        Get
            Return _Przesuniecie
        End Get
        Set(value As Point)
            _Przesuniecie = value
            Invalidate()
        End Set
    End Property

    Private _TypRysownika As TypRysownika = TypRysownika.KlasycznyGDI
    <Description("Rodzaj obiektu rysującego pulpit"), Category(KATEG_KONF), DefaultValue(TypRysownika.KlasycznyGDI)>
    Public Property TypRysownika As TypRysownika
        Get
            Return _TypRysownika
        End Get
        Set(value As TypRysownika)
            If value = _TypRysownika Then Exit Property

            Select Case value
                Case TypRysownika.KlasycznyGDI
                    _Rysownik = New PulpitKlasycznyGDI()
                    DoubleBuffered = True
                Case TypRysownika.KlasycznyDirect2D
                    _Rysownik = New PulpitKlasycznyDirect2D()
                    DoubleBuffered = False
            End Select

            _Rysownik.Inicjalizuj(Handle, CUInt(Width), CUInt(Height))
            _TypRysownika = value
            Invalidate()
        End Set
    End Property

    Private _MozliwoscWcisnieciaPrzycisku As Boolean = True
    <Description("Możliwość wciśnięcia przycisku w trybie działania"), Category(KATEG_KONF), DefaultValue(True)>
    Public Property MozliwoscWcisnieciaPrzycisku As Boolean
        Get
            Return _MozliwoscWcisnieciaPrzycisku
        End Get
        Set(value As Boolean)
            If value <> _MozliwoscWcisnieciaPrzycisku Then
                If Not value Then WylaczWcisnieciePrzycisku()
                _MozliwoscWcisnieciaPrzycisku = value
            End If
        End Set
    End Property

    <Description("Czas od kliknięcia przycisku do reakcji pulpitu"), Category(KATEG_KONF), DefaultValue(CZAS_WCISKANIA_PRZYCISKU_MS)>
    Public Property CzasWciskaniaPrzyciskuMS As Integer = CZAS_WCISKANIA_PRZYCISKU_MS

    Private _DodatkoweObiekty As DodatkoweObiektyTrybDzialania = DodatkoweObiektyTrybDzialania.Nic
    <Description("Dodatkowe obiekty używane w trybie działania"), Category(KATEG_KONF), DefaultValue(DodatkoweObiektyTrybDzialania.Nic)>
    Public Property DodatkoweObiekty As DodatkoweObiektyTrybDzialania
        Get
            Return _DodatkoweObiekty
        End Get
        Set(value As DodatkoweObiektyTrybDzialania)
            If value <> _DodatkoweObiekty Then
                _DodatkoweObiekty = value
                Invalidate()
            End If
        End Set
    End Property

    Private _Pulpit As New Zaleznosci.Pulpit
    <Browsable(False)>
    Public Property Pulpit As Zaleznosci.Pulpit
        Get
            Return _Pulpit
        End Get
        Set(value As Zaleznosci.Pulpit)
            _Pulpit = value
            _Rysownik.UniewaznioneSasiedztwoTorow = True
            Invalidate()
        End Set
    End Property

    Private _MozliwoscZaznaczeniaKostki As Boolean = False
    <Description("Możliwość zaznaczenia kostki w trybie działania"), Category(KATEG_KONF), DefaultValue(False)>
    Public Property MozliwoscZaznaczeniaKostki As Boolean
        Get
            Return _MozliwoscZaznaczeniaKostki
        End Get
        Set(value As Boolean)
            If value <> _MozliwoscZaznaczeniaKostki Then
                _MozliwoscZaznaczeniaKostki = value
                If _ZaznaczonaKostka IsNot Nothing Then Invalidate()
            End If
        End Set
    End Property

    <Browsable(False)>
    Public Property WarunekZaznaczeniaKostki As ZaznaczalnoscKostki

    Private _ZaznaczonaKostka As Zaleznosci.Kostka
    <Browsable(False)>
    Public Property ZaznaczonaKostka As Zaleznosci.Kostka
        Get
            Return _ZaznaczonaKostka
        End Get
        Set(value As Zaleznosci.Kostka)
            If value IsNot _ZaznaczonaKostka And (_TrybProjektowy Or _MozliwoscZaznaczeniaKostki) Then
                _ZaznaczonaKostka = value
                RaiseEvent ZmianaZaznaczeniaKostki(value)
                If Not TrybProjektowy Or _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic Then Invalidate()
            End If
        End Set
    End Property

    Private _ZaznaczoneLampy As New HashSet(Of Zaleznosci.Lampa)
    <Browsable(False)>
    Public ReadOnly Property ZaznaczoneLampy As HashSet(Of Zaleznosci.Lampa)
        Get
            Return New HashSet(Of Zaleznosci.Lampa)(_ZaznaczoneLampy)
        End Get
    End Property

    Private _MozliwoscZaznaczeniaOdcinka As Boolean = False
    <Browsable(False)>
    Public Property MozliwoscZaznaczeniaOdcinka As Boolean
        Get
            Return _MozliwoscZaznaczeniaOdcinka
        End Get
        Set(value As Boolean)
            If (Not TrybProjektowy) And value <> _MozliwoscZaznaczeniaOdcinka Then
                _MozliwoscZaznaczeniaOdcinka = value
                Invalidate()
            End If
        End Set
    End Property

    Private _MozliwoscZaznaczeniaLamp As Boolean = False
    <Browsable(False)>
    Public Property MozliwoscZaznaczeniaLamp As Boolean
        Get
            Return _MozliwoscZaznaczeniaLamp
        End Get
        Set(value As Boolean)
            _MozliwoscZaznaczeniaLamp = value
            Invalidate()
        End Set
    End Property

    Private _ZaznaczonyOdcinek As Zaleznosci.OdcinekToru
    <Browsable(False)>
    Public Property ZaznaczonyOdcinek As Zaleznosci.OdcinekToru
        Get
            Return _ZaznaczonyOdcinek
        End Get
        Set(value As Zaleznosci.OdcinekToru)
            If value IsNot _ZaznaczonyOdcinek Then
                Dim zaznTrybDzialania As Boolean = (Not TrybProjektowy) And _MozliwoscZaznaczeniaOdcinka

                If (TrybProjektowy And _projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow) Or zaznTrybDzialania Then
                    _ZaznaczonyOdcinek = value
                    If zaznTrybDzialania Then RaiseEvent ZaznaczonoOdcinek(_ZaznaczonyOdcinek)
                    Invalidate()
                End If
            End If
        End Set
    End Property

    Private _projDodatkoweObiekty As RysujDodatkoweObiekty = RysujDodatkoweObiekty.Nic
    <Browsable(False)>
    Public Property projDodatkoweObiekty As RysujDodatkoweObiekty
        Get
            Return _projDodatkoweObiekty
        End Get
        Set(value As RysujDodatkoweObiekty)
            _projDodatkoweObiekty = value
            Invalidate()
        End Set
    End Property

    Private _projZaznaczonaLampa As Zaleznosci.Lampa
    <Browsable(False)>
    Public Property projZaznaczonaLampa As Zaleznosci.Lampa
        Get
            Return _projZaznaczonaLampa
        End Get
        Set(value As Zaleznosci.Lampa)
            If _projZaznaczonaLampa Is value Then Exit Property

            _projZaznaczonaLampa = value
            RaiseEvent projZmianaZaznaczeniaLampy(value)
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Then Invalidate()
        End Set
    End Property

    Private _projZaznaczonyLicznik As Zaleznosci.ParaLicznikowOsi
    <Browsable(False)>
    Public Property projZaznaczonyLicznik As Zaleznosci.ParaLicznikowOsi
        Get
            Return _projZaznaczonyLicznik
        End Get
        Set(value As Zaleznosci.ParaLicznikowOsi)
            _projZaznaczonyLicznik = value
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then Invalidate()
        End Set
    End Property

    Private _projZaznaczonyPrzejazd As Zaleznosci.PrzejazdKolejowoDrogowy
    <Browsable(False)>
    Public Property projZaznaczonyPrzejazd As Zaleznosci.PrzejazdKolejowoDrogowy
        Get
            Return _projZaznaczonyPrzejazd
        End Get
        Set(value As Zaleznosci.PrzejazdKolejowoDrogowy)
            If value IsNot _projZaznaczonyPrzejazd Then
                _projZaznaczonyPrzejazd = value
                projZaznaczonyPrzejazdAutomatyzacja = Nothing
                projZaznaczonyPrzejazdRogatka = Nothing
                projZaznaczonyPrzejazdSygnDrog = Nothing
                If _projDodatkoweObiekty = RysujDodatkoweObiekty.Przejazdy Then Invalidate()
            End If
        End Set
    End Property

    Private _projZaznaczonyPrzejazdAutomatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie
    <Browsable(False)>
    Public Property projZaznaczonyPrzejazdAutomatyzacja As Zaleznosci.PrzejazdAutomatyczneZamykanie
        Get
            Return _projZaznaczonyPrzejazdAutomatyzacja
        End Get
        Set(value As Zaleznosci.PrzejazdAutomatyczneZamykanie)
            _projZaznaczonyPrzejazdAutomatyzacja = value
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyAutomatyzacja Then Invalidate()
        End Set
    End Property

    Private _projZaznaczonyPrzejazdRogatka As Zaleznosci.PrzejazdRogatka
    <Browsable(False)>
    Public Property projZaznaczonyPrzejazdRogatka As Zaleznosci.PrzejazdRogatka
        Get
            Return _projZaznaczonyPrzejazdRogatka
        End Get
        Set(value As Zaleznosci.PrzejazdRogatka)
            If _projZaznaczonyPrzejazdRogatka Is value Then Exit Property

            _projZaznaczonyPrzejazdRogatka = value
            RaiseEvent projZmianaZaznaczeniaRogatki(value)
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyRogatki Then Invalidate()
        End Set
    End Property

    Private _projZaznaczonyPrzejazdSygnDrog As Zaleznosci.PrzejazdElementWykonawczy
    <Browsable(False)>
    Public Property projZaznaczonyPrzejazdSygnDrog As Zaleznosci.PrzejazdElementWykonawczy
        Get
            Return _projZaznaczonyPrzejazdSygnDrog
        End Get
        Set(value As Zaleznosci.PrzejazdElementWykonawczy)
            If _projZaznaczonyPrzejazdSygnDrog Is value Then Exit Property

            _projZaznaczonyPrzejazdSygnDrog = value
            RaiseEvent projZmianaZaznaczeniaSygnalizatoraDrogowego(value)
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdySygnDrog Then Invalidate()
        End Set
    End Property

    <Description("Wciśnięto przycisk na pulpicie"), Category(KATEG_ZDARZ)>
    Public Event WcisnietoPrzycisk(kostka As Zaleznosci.Kostka)

    <Description("Zarejestrowano oś w liczniku osi"), Category(KATEG_ZDARZ)>
    Public Event ZarejestrowanoOs(adres As UShort)

    <Description("Zmiana zaznaczonej kostki"), Category(KATEG_ZDARZ)>
    Public Event ZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka)

    <Description("Zaznaczono odcinek toru w trybie działania"), Category(KATEG_ZDARZ)>
    Public Event ZaznaczonoOdcinek(odcinek As Zaleznosci.OdcinekToru)

    <Description("Zmiana blokady zwrotnicy"), Category(KATEG_ZDARZ)>
    Public Event BlokadaZwrotnicy(zwrotnica As Zaleznosci.Rozjazd)

    <Description("Zmiana blokady sygnalizatora"), Category(KATEG_ZDARZ)>
    Public Event BlokadaSygnalizatora(sygnalizator As Zaleznosci.Sygnalizator)

    <Description("Zmiana trybu działania sygnalizatora półsamoczynnego"), Category(KATEG_ZDARZ)>
    Public Event TrybSamoczynnySygnalizatora(sygnalizator As Zaleznosci.SygnalizatorPolsamoczynny)

    <Description("Zmiana zamknięcia odcinka toru"), Category(KATEG_ZDARZ)>
    Public Event ZamkniecieOdcinka(odcinek As Zaleznosci.OdcinekToru, wspolrzedne As Zaleznosci.PunktCalkowity)

    <Description("Zerowanie licznika osi"), Category(KATEG_ZDARZ)>
    Public Event ZerowanieLicznikaOsi(odcinek As Zaleznosci.OdcinekToru)

    <Description("Zmiana ostatnio zaznaczonej lampy"), Category(KATEG_ZDARZ)>
    Public Event ZmianaZaznaczeniaOstatniejLampy(lampa As Zaleznosci.Lampa)

    <Description("Zmiana zbioru zaznaczonych lamp"), Category(KATEG_ZDARZ)>
    Public Event ZmianaZaznaczeniaLamp(lampy As HashSet(Of Zaleznosci.Lampa))

    <Description("Zmiana zaznaczonej lampy"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaZaznaczeniaLampy(lampa As Zaleznosci.Lampa)

    <Description("Zmiana przypisania toru do odcinka"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaPrzypisaniaToruDoOdcinka()

    <Description("Zmiana przypisania kostki do przejazdu kolejowo-drogowego"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaPrzypisaniaKostkiDoPrzejazdu()

    <Description("Zmiana zaznaczonej rogatki przejazdu kolejowo-drogowego"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaZaznaczeniaRogatki(rogatka As Zaleznosci.PrzejazdRogatka)

    <Description("Zmiana zaznaczonego sygnalizatora drogowego przejazdu kolejowo-drogowego"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaZaznaczeniaSygnalizatoraDrogowego(sygnalizator As Zaleznosci.PrzejazdElementWykonawczy)

    Public Delegate Function ZaznaczalnoscKostki(k As Zaleznosci.Kostka) As Boolean
    Public Delegate Sub PrzetwarzanieTabliczkiZamkniecia(punkt As Zaleznosci.PunktCalkowity)

    Public Sub New()
        InitializeComponent()
        DoubleBuffered = True
    End Sub

    Public Sub Czysc(Optional nowyPulpit As Zaleznosci.Pulpit = Nothing)
        _Pulpit = If(nowyPulpit, New Zaleznosci.Pulpit)
        _Skalowanie = SKALOWANIE_DOMYSLNE
        _Przesuniecie = PobierzPozycjeDlaWysrodkowania()
        _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic
        _ZaznaczonyOdcinek = Nothing
        _projZaznaczonyLicznik = Nothing
        _projZaznaczonyPrzejazd = Nothing
        _projZaznaczonyPrzejazdAutomatyzacja = Nothing
        _projZaznaczonyPrzejazdRogatka = Nothing
        _projZaznaczonyPrzejazdSygnDrog = Nothing
        _Rysownik.UniewaznioneSasiedztwoTorow = True
        projZaznaczonaLampa = Nothing
        ZaznaczonyLicznikOsiAdres = Nothing

        SyncLock Me
            TabliczkiZamkniecOdcinkow.Clear()
        End SyncLock

        ZaznaczonaKostka = Nothing  'przypisanie do własności zamiast zmiennej, żeby wywołały się zdarzenia
        Invalidate()
    End Sub

    Public Sub Wysrodkuj()
        Przesuniecie = PobierzPozycjeDlaWysrodkowania()
    End Sub

    Public Overloads Sub Invalidate()
        If Not RysowanieWlaczone Then Exit Sub

        If _TypRysownika = TypRysownika.KlasycznyGDI Then
            MyBase.Invalidate()
        Else
            _Rysownik.Rysuj(Me, Nothing)
        End If
    End Sub

    Public Sub WlaczZegarMigania()
        Invoke(actWlaczZegarMigania)
    End Sub

    Public Sub WylaczZegarMigania()
        Invoke(actWylaczZegarMigania)
    End Sub

    Public Sub InicjalizujMigacz()
        UstawMigacz(New Migacz(Me))
    End Sub

    Public Sub UsunMigacz()
        Migacz?.Wylacz()
        UstawMigacz(Nothing)
        actWylaczZegarMigania()
    End Sub

    Public Sub DodajTabliczkeZamkniecia(adresOdcinka As UShort, wspolrzedne As Zaleznosci.PunktCalkowity)
        SyncLock Me
            If Not TabliczkiZamkniecOdcinkow.ContainsKey(adresOdcinka) Then
                TabliczkiZamkniecOdcinkow.Add(adresOdcinka, wspolrzedne)
            End If
        End SyncLock
    End Sub

    Public Sub UsunTabliczkeZamkniecia(adresOdcinka As UShort)
        SyncLock Me
            TabliczkiZamkniecOdcinkow.Remove(adresOdcinka)
        End SyncLock
    End Sub

    Public Sub PrzetworzTabliczkiZamkniec(metoda As PrzetwarzanieTabliczkiZamkniecia)
        SyncLock Me
            For Each kv As KeyValuePair(Of UShort, Zaleznosci.PunktCalkowity) In TabliczkiZamkniecOdcinkow
                metoda(kv.Value)
            Next
        End SyncLock
    End Sub

    Private Sub PulpitSterowniczy_Load() Handles Me.Load
        _Rysownik.Inicjalizuj(Handle, CUInt(Width), CUInt(Height))
    End Sub

    Private Sub PulpitSterowniczy_Resize() Handles Me.Resize
        _Rysownik.ZmienRozmiar(CUInt(Width), CUInt(Height))
        Invalidate()
    End Sub

    Private Sub PulpitSterowniczy_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        _Rysownik.Rysuj(Me, e.Graphics)
    End Sub

    Private Sub PulpitSterowniczy_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If TrybProjektowy And _ZaznaczonaKostka IsNot Nothing Then

            If e.KeyData = Keys.R Then
                Dim obrot As Integer = _ZaznaczonaKostka.Obrot
                obrot = (obrot + ZWIEKSZ_OBROT) Mod KAT_PELNY
                _ZaznaczonaKostka.Obrot = obrot
                _Rysownik.UniewaznioneSasiedztwoTorow = True
                Invalidate()

            ElseIf e.KeyData = Keys.Delete Then
                Pulpit.UsunKostke(_ZaznaczonaKostka)
                _Rysownik.UniewaznioneSasiedztwoTorow = True
                _ZaznaczonaKostka = Nothing
            End If

        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Dim p As Zaleznosci.PunktCalkowity = PobierzKliknieteWspolrzedneKostki(e.Location)
        Dim zaznaczycKostke As Boolean = False

        If TrybProjektowy Then

            If _projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow Then
                If _Pulpit.CzyKostkaWZakresiePulpitu(p) AndAlso _ZaznaczonyOdcinek IsNot Nothing Then
                    Dim tor As Zaleznosci.Tor = TryCast(Pulpit.Kostki(p.X, p.Y), Zaleznosci.Tor)

                    If tor IsNot Nothing Then
                        Dim pozycja As Zaleznosci.PrzynaleznoscToruDoOdcinka = Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy

                        Dim podw As Zaleznosci.TorPodwojnyNiezalezny = TryCast(tor, Zaleznosci.TorPodwojnyNiezalezny)
                        If podw IsNot Nothing Then
                            pozycja = PobierzFragmentToruPodwojnego(e.Location, podw)
                        End If

                        If pozycja <> Zaleznosci.PrzynaleznoscToruDoOdcinka.Zaden Then

                            Dim nalezyDoTegoOdcinka As Boolean

                            If pozycja = Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy Then
                                nalezyDoTegoOdcinka = tor.NalezyDoOdcinka Is _ZaznaczonyOdcinek
                                tor.NalezyDoOdcinka?.UsunTor(tor, Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy)
                                If nalezyDoTegoOdcinka Then
                                    tor.NalezyDoOdcinka = Nothing
                                Else
                                    tor.NalezyDoOdcinka = _ZaznaczonyOdcinek
                                    _ZaznaczonyOdcinek.DodajTor(tor, Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy)
                                End If

                            Else

                                nalezyDoTegoOdcinka = podw.NalezyDoOdcinkaDrugi Is _ZaznaczonyOdcinek
                                podw.NalezyDoOdcinkaDrugi?.UsunTor(podw, Zaleznosci.PrzynaleznoscToruDoOdcinka.Drugi)
                                If nalezyDoTegoOdcinka Then
                                    podw.NalezyDoOdcinkaDrugi = Nothing
                                Else
                                    podw.NalezyDoOdcinkaDrugi = _ZaznaczonyOdcinek
                                    _ZaznaczonyOdcinek.DodajTor(podw, Zaleznosci.PrzynaleznoscToruDoOdcinka.Drugi)
                                End If

                            End If

                            RaiseEvent projZmianaPrzypisaniaToruDoOdcinka()
                            Invalidate()
                        End If

                    End If
                End If

            ElseIf _projDodatkoweObiekty = RysujDodatkoweObiekty.Przejazdy Then
                If _Pulpit.CzyKostkaWZakresiePulpitu(p) Then
                    Dim kostka As Zaleznosci.Kostka = Pulpit.Kostki(p.X, p.Y)
                    Dim prz As Zaleznosci.PrzejazdKolejowoDrogowyKostka = TryCast(kostka, Zaleznosci.PrzejazdKolejowoDrogowyKostka)

                    If prz IsNot Nothing AndAlso _projZaznaczonyPrzejazd IsNot Nothing Then
                        Dim nalezyDoTegoPrzejazdu As Boolean = prz.NalezyDoPrzejazdu Is _projZaznaczonyPrzejazd
                        prz.NalezyDoPrzejazdu?.KostkiPrzejazdy.Remove(prz)
                        If nalezyDoTegoPrzejazdu Then
                            prz.NalezyDoPrzejazdu = Nothing
                        Else
                            prz.NalezyDoPrzejazdu = _projZaznaczonyPrzejazd
                            _projZaznaczonyPrzejazd.KostkiPrzejazdy.Add(prz)
                        End If
                        RaiseEvent projZmianaPrzypisaniaKostkiDoPrzejazdu()
                        Invalidate()

                    End If
                End If

            ElseIf _projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyRogatki Then
                projZaznaczonyPrzejazdRogatka = PobierzKliknietaRogatke(e.Location)

            ElseIf _projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdySygnDrog Then
                projZaznaczonyPrzejazdSygnDrog = PobierzKliknietySygnDrog(e.Location)

            ElseIf _projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Then
                projZaznaczonaLampa = PobierzKliknietaLampe(e.Location)

            ElseIf _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic Then
                zaznaczycKostke = _Pulpit.CzyKostkaWZakresiePulpitu(p)

            End If

        Else

            If _MozliwoscZaznaczeniaOdcinka Then
                Dim odc As Zaleznosci.OdcinekToru = PobierzKliknietyOdcinek(p, e.Location)
                If odc IsNot Nothing AndAlso odc.Zamkniety Then odc = Nothing
                ZaznaczonyOdcinek = odc
            End If

            If _MozliwoscZaznaczeniaLamp And PoprzZaznLampyWObszarze Is Nothing Then     'zaznaczenie pojedynczej lampy kliknięciem
                Dim lampa As Zaleznosci.Lampa = PobierzKliknietaLampe(e.Location)
                Dim zmiana As Boolean = True
                Dim pusteZazn As Boolean = _ZaznaczoneLampy.Count = 0
                Dim czyNieCtrl As Boolean = (ModifierKeys And Keys.Control) = 0

                If czyNieCtrl Then
                    If lampa IsNot Nothing And _ZaznaczoneLampy.Count = 1 And _ZaznaczoneLampy.Contains(lampa) Then
                        zmiana = False
                    Else
                        _ZaznaczoneLampy.Clear()
                        KolejnoscZaznaczeniaLamp.Clear()
                    End If
                End If

                If lampa IsNot Nothing And zmiana Then
                    'nie udało się usunąć, więc nie było lampy w zaznaczeniu - dodaj ją
                    If Not _ZaznaczoneLampy.Remove(lampa) Then
                        _ZaznaczoneLampy.Add(lampa)
                        KolejnoscZaznaczeniaLamp.Add(lampa)
                        RaiseEvent ZmianaZaznaczeniaOstatniejLampy(lampa)
                    Else
                        Dim ostLampa As Zaleznosci.Lampa = If(KolejnoscZaznaczeniaLamp.Count = 0, Nothing, KolejnoscZaznaczeniaLamp.Last)
                        KolejnoscZaznaczeniaLamp.Remove(lampa)
                        Dim nowaOstLampa As Zaleznosci.Lampa = If(KolejnoscZaznaczeniaLamp.Count = 0, Nothing, KolejnoscZaznaczeniaLamp.Last)
                        If nowaOstLampa IsNot ostLampa Then
                            RaiseEvent ZmianaZaznaczeniaOstatniejLampy(nowaOstLampa)
                        End If
                    End If

                    RaiseEvent ZmianaZaznaczeniaLamp(ZaznaczoneLampy)

                ElseIf czyNieCtrl And lampa Is Nothing And Not pusteZazn Then
                    RaiseEvent ZmianaZaznaczeniaOstatniejLampy(Nothing)
                    RaiseEvent ZmianaZaznaczeniaLamp(ZaznaczoneLampy)

                End If

                Invalidate()
            End If

            If _MozliwoscZaznaczeniaKostki AndAlso _Pulpit.CzyKostkaWZakresiePulpitu(p) Then
                zaznaczycKostke = WarunekZaznaczeniaKostki Is Nothing OrElse WarunekZaznaczeniaKostki(_Pulpit.Kostki(p.X, p.Y))
            End If

        End If

        If zaznaczycKostke Then ZaznaczonaKostka = _Pulpit.Kostki(p.X, p.Y)
    End Sub

    Private Sub PulpitSterowniczy_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Dim wspx As Double = (e.X - Przesuniecie.X) / SzerokoscPulpitu
        Dim wspy As Double = (e.Y - Przesuniecie.Y) / WysokoscPulpitu
        Dim sk As Single = Skalowanie

        RysowanieWlaczone = False
        Skalowanie += e.Delta * SKALOWANIE_ZMIANA
        RysowanieWlaczone = True

        If sk = Skalowanie Then Exit Sub

        Dim nowy As New Point(CInt(wspx * SzerokoscPulpitu), CInt(wspy * WysokoscPulpitu))
        Przesuniecie = New Point(e.X - nowy.X, e.Y - nowy.Y)
    End Sub

    Private Sub PulpitSterowniczy_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then
            If PoprzedniPunkt.X <> PUSTA_WSPOLRZEDNA Then    'przesuń
                Dim zmX As Integer = e.X - PoprzedniPunkt.X
                Dim zmY As Integer = e.Y - PoprzedniPunkt.Y
                Przesuniecie = New Point(Przesuniecie.X + zmX, Przesuniecie.Y + zmY)
                PoprzedniPunkt = e.Location

            ElseIf MozliwoscZaznaczeniaLamp And PoprzZaznLampyWObszarze IsNot Nothing Then      'zaznacz lampy obszrem
                KoniecZaznaczeniaLamp = WspolrzedneEkranuDoPulpitu(e.Location)
                Dim zazn As HashSet(Of Zaleznosci.Lampa) = PobierzLampyWObszarze()
                Dim ostLampa As Zaleznosci.Lampa = If(KolejnoscZaznaczeniaLamp.Count = 0, Nothing, KolejnoscZaznaczeniaLamp.Last)
                Dim zmiana As Boolean = PorwonajLampyWObszarze(PoprzZaznLampyWObszarze, zazn)

                If zmiana Then
                    Dim nowaOstLampa As Zaleznosci.Lampa = If(KolejnoscZaznaczeniaLamp.Count = 0, Nothing, KolejnoscZaznaczeniaLamp.Last)

                    If ostLampa IsNot nowaOstLampa Then
                        RaiseEvent ZmianaZaznaczeniaOstatniejLampy(nowaOstLampa)
                    End If

                    RaiseEvent ZmianaZaznaczeniaLamp(ZaznaczoneLampy)
                End If

                PoprzZaznLampyWObszarze = zazn
                Invalidate()

            End If
        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If Not e.Button = MouseButtons.Left Then Exit Sub

        PoprzedniPunkt.X = PUSTA_WSPOLRZEDNA
        PoprzedniPunkt.Y = PUSTA_WSPOLRZEDNA

        If Not _TrybProjektowy And ((_MozliwoscZaznaczeniaLamp And PoprzZaznLampyWObszarze IsNot Nothing) Or ZaznaczonyLicznikOsiAdres.HasValue) Then
            PoczatekZaznaczeniaLamp = New PointF
            KoniecZaznaczeniaLamp = New PointF
            ZaznaczonyLicznikOsiAdres = Nothing
            Invalidate()
        End If

        WylaczWcisnieciePrzycisku()
    End Sub

    Private Sub PulpitSterowniczy_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If Not e.Button = MouseButtons.Left Then Exit Sub

        PoprzZaznLampyWObszarze = Nothing

        If Not TrybProjektowy AndAlso _MozliwoscZaznaczeniaLamp Then
            Dim lampa As Zaleznosci.Lampa = PobierzKliknietaLampe(e.Location)

            If lampa Is Nothing Then    'zaznaczanie obszarem
                PoczatekZaznaczeniaLamp = WspolrzedneEkranuDoPulpitu(e.Location)
                PoprzZaznLampyWObszarze = New HashSet(Of Zaleznosci.Lampa)

                If (ModifierKeys And Keys.Control) = 0 And _ZaznaczoneLampy.Count > 0 And KolejnoscZaznaczeniaLamp.Count > 0 Then
                    _ZaznaczoneLampy.Clear()
                    KolejnoscZaznaczeniaLamp.Clear()
                    Invalidate()

                    RaiseEvent ZmianaZaznaczeniaOstatniejLampy(Nothing)
                    RaiseEvent ZmianaZaznaczeniaLamp(ZaznaczoneLampy)
                End If
            End If

            Exit Sub    'przy sterowaniu oświetleniem (możliwość zaznaczenia obszaru), wyłącz możliwość przesuwania pulpitu/wciskania przycisków
        End If

        Dim p As Zaleznosci.PunktCalkowity = PobierzKliknieteWspolrzedneKostki(e.Location)

        If TrybProjektowy And projDodatkoweObiekty = RysujDodatkoweObiekty.Nic And ((ModifierKeys And Keys.Shift) <> 0) Then      'Przesuń kostkę
            If _Pulpit.CzyKostkaNiepusta(p) Then
                PoprzLokalizacjaKostki = p
                ZaznaczonaKostka = Pulpit.Kostki(p.X, p.Y)
                DoDragDrop(New PrzeciaganaKostka(_ZaznaczonaKostka, Handle), DragDropEffects.Move)
            End If
        ElseIf (ModifierKeys And Keys.Control) = 0 Then     'Przesuń cały pulpit
            PoprzedniPunkt = e.Location
        End If

        If Not TrybProjektowy Then

            If _MozliwoscWcisnieciaPrzycisku AndAlso _Pulpit.CzyKostkaNiepusta(p) Then

                Dim prz As Zaleznosci.IPrzycisk = TryCast(Pulpit.Kostki(p.X, p.Y), Zaleznosci.IPrzycisk)
                If prz IsNot Nothing AndAlso prz.PosiadaPrzycisk AndAlso (Not prz.Zablokowany) Then
                    WcisnietyPrzycisk = prz
                    WcisnietyPrzycisk.Wcisniety = True
                    Invalidate()
                    tmrPrzycisk.Interval = CzasWciskaniaPrzyciskuMS
                    tmrPrzycisk.Start()
                End If

            ElseIf _DodatkoweObiekty = DodatkoweObiektyTrybDzialania.LicznikiOsi Then

                Dim adresLicznika As UShort? = PobierzKliknietyLicznikOsi(_Pulpit.LicznikiOsi, e.Location)
                If adresLicznika.HasValue Then
                    RaiseEvent ZarejestrowanoOs(adresLicznika.Value)
                    ZaznaczonyLicznikOsiAdres = adresLicznika
                    Invalidate()
                End If

            End If

        End If
    End Sub

    Private Sub PulpitSterowniczy_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        e.Effect = DragDropEffects.None
        If Not TrybProjektowy Then Exit Sub

        Dim przeciaganaKostka As Zaleznosci.Kostka = PobierzPrzeciaganaKostke(e)
        If przeciaganaKostka Is Nothing Then Exit Sub

        Dim p As Zaleznosci.PunktCalkowity = PobierzKliknieteWspolrzedneKostki(PointToClient(New Point(e.X, e.Y)))

        If _Pulpit.CzyKostkaWZakresiePulpitu(p) Then
            Dim polozonaKostka As Zaleznosci.Kostka = _Pulpit.Kostki(p.X, p.Y)
            If polozonaKostka IsNot Nothing AndAlso polozonaKostka IsNot przeciaganaKostka Then Exit Sub

            Dim dodajKostke As Boolean = _ZaznaczonaKostka IsNot przeciaganaKostka
            Dim przesunKostke As Boolean = PoprzLokalizacjaKostki <> p

            If dodajKostke Or przesunKostke Then
                If dodajKostke Then
                    ZaznaczonaKostka = przeciaganaKostka
                Else
                    _Pulpit.Kostki(PoprzLokalizacjaKostki.X, PoprzLokalizacjaKostki.Y) = Nothing
                End If

                _Pulpit.Kostki(p.X, p.Y) = _ZaznaczonaKostka
                PoprzLokalizacjaKostki = p
                _Rysownik.UniewaznioneSasiedztwoTorow = True
                Invalidate()
                Focus()
            End If

            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub ctxMenu_Opening() Handles ctxMenu.Opening
        Dim pokazZwrotn As Boolean = False
        Dim pokazSygnManewr As Boolean = False
        Dim pokazSygnPolsam As Boolean = False
        Dim pokazOdcinek As Boolean = False

        MenuZwrotnica = Nothing
        MenuSygnalizator = Nothing
        MenuOdcinek = Nothing
        MenuOdcinekPunkt = Nothing

        If (Not TrybProjektowy) And _MozliwoscWcisnieciaPrzycisku Then
            Dim klik As Point = PointToClient(MousePosition)
            Dim p As Zaleznosci.PunktCalkowity = PobierzKliknieteWspolrzedneKostki(klik)

            If _Pulpit.CzyKostkaNiepusta(p) Then
                Dim k As Zaleznosci.Kostka = _Pulpit.Kostki(p.X, p.Y)
                Dim z As Zaleznosci.Rozjazd = TryCast(k, Zaleznosci.Rozjazd)
                Dim s As Zaleznosci.Sygnalizator = TryCast(k, Zaleznosci.Sygnalizator)

                'kostka
                If z IsNot Nothing Then
                    pokazZwrotn = True
                    MenuZwrotnica = z

                ElseIf s IsNot Nothing Then
                    If s.Typ = Zaleznosci.TypKostki.SygnalizatorManewrowy Then
                        pokazSygnManewr = True
                        MenuSygnalizator = s
                    ElseIf s.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Then
                        pokazSygnPolsam = True
                        MenuSygnalizator = s
                    End If

                End If

                'odcinek toru
                Dim odc As Zaleznosci.OdcinekToru = PobierzKliknietyOdcinek(p, klik)
                If odc IsNot Nothing Then
                    pokazOdcinek = True
                    MenuOdcinek = odc
                    MenuOdcinekPunkt = p
                End If
            End If
        End If

        ctmBlokadaZwrotnicy.Visible = pokazZwrotn
        ctmBlokadaSygnalizatora.Visible = pokazSygnManewr Or pokazSygnPolsam
        ctmTrybSamoczynny.Visible = pokazSygnPolsam
        tspUrzadzenia.Visible = pokazZwrotn Or pokazSygnManewr Or pokazSygnPolsam
        ctmZamkniecieOdcinka.Visible = pokazOdcinek
        ctmZerujLicznikOsi.Visible = pokazOdcinek
        tspOdcinekToru.Visible = pokazOdcinek
    End Sub

    Private Sub ctmBlokadaZwrotnicy_Click() Handles ctmBlokadaZwrotnicy.Click
        If MenuZwrotnica Is Nothing Then Exit Sub
        RaiseEvent BlokadaZwrotnicy(MenuZwrotnica)
    End Sub

    Private Sub ctmBlokadaSygnalizatora_Click() Handles ctmBlokadaSygnalizatora.Click
        If MenuSygnalizator Is Nothing Then Exit Sub
        RaiseEvent BlokadaSygnalizatora(MenuSygnalizator)
    End Sub

    Private Sub ctmTrybSamoczynny_Click() Handles ctmTrybSamoczynny.Click
        Dim sygnPolsam As Zaleznosci.SygnalizatorPolsamoczynny = TryCast(MenuSygnalizator, Zaleznosci.SygnalizatorPolsamoczynny)
        If sygnPolsam Is Nothing Then Exit Sub
        RaiseEvent TrybSamoczynnySygnalizatora(sygnPolsam)
    End Sub

    Private Sub ctmZamkniecieOdcinka_Click() Handles ctmZamkniecieOdcinka.Click
        If MenuOdcinek Is Nothing Or MenuOdcinekPunkt Is Nothing Then Exit Sub
        RaiseEvent ZamkniecieOdcinka(MenuOdcinek, MenuOdcinekPunkt)
    End Sub

    Private Sub ctmZerujLicznikOsi_Click() Handles ctmZerujLicznikOsi.Click
        If MenuOdcinek Is Nothing Then Exit Sub
        RaiseEvent ZerowanieLicznikaOsi(MenuOdcinek)
    End Sub

    Private Sub ctmWysrodkuj_Click() Handles ctmWysrodkuj.Click
        Wysrodkuj()
    End Sub

    Private Sub tmrPrzycisk_Tick() Handles tmrPrzycisk.Tick
        tmrPrzycisk.Stop()
        If WcisnietyPrzycisk IsNot Nothing Then RaiseEvent WcisnietoPrzycisk(DirectCast(WcisnietyPrzycisk, Zaleznosci.Kostka))
    End Sub

    Private Sub tmrMiganie_Tick() Handles tmrMiganie.Tick
        If Migacz IsNot Nothing Then
            Migacz.PrzelaczStan()
            Invalidate()
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Dim wynik As Boolean = False

        If TrybProjektowy AndAlso _ZaznaczonaKostka IsNot Nothing AndAlso KLAWISZE_STRZALEK.Contains(keyData) Then
            Dim p As Zaleznosci.Punkt = _Pulpit.ZnajdzKostke(_ZaznaczonaKostka)
            Dim przesX As Integer
            Dim przesY As Integer
            Dim maxX As Integer
            Dim maxY As Integer

            If keyData = Keys.Up AndAlso p.Y <> 0 Then
                przesY = -1
                maxY = -1

            ElseIf keyData = Keys.Down AndAlso p.Y <> (Pulpit.Wysokosc - 1) Then
                przesY = 1
                maxY = Pulpit.Wysokosc

            ElseIf keyData = Keys.Left AndAlso p.X <> 0 Then
                przesX = -1
                maxX = -1

            ElseIf keyData = Keys.Right AndAlso p.X <> (Pulpit.Szerokosc - 1) Then
                przesX = 1
                maxX = Pulpit.Szerokosc
            End If

            If przesX <> 0 Or przesY <> 0 Then
                Dim nast As Zaleznosci.Kostka = ZnajdzNajblizszaKostke(p.X + przesX, p.Y + przesY, przesX, przesY, maxX, maxY)
                If nast IsNot Nothing Then ZaznaczonaKostka = nast
                wynik = True
            End If
        End If

        Return wynik
    End Function

    Private Function PobierzKliknietyOdcinek(p As Zaleznosci.PunktCalkowity, klik As Point) As Zaleznosci.OdcinekToru
        If _Pulpit.CzyKostkaWZakresiePulpitu(p) Then
            Dim tor As Zaleznosci.Tor = TryCast(Pulpit.Kostki(p.X, p.Y), Zaleznosci.Tor)

            If tor IsNot Nothing Then
                Dim pozycja As Zaleznosci.PrzynaleznoscToruDoOdcinka = Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy

                Dim podw As Zaleznosci.TorPodwojnyNiezalezny = TryCast(tor, Zaleznosci.TorPodwojnyNiezalezny)
                If podw IsNot Nothing Then
                    pozycja = PobierzFragmentToruPodwojnego(klik, podw)
                End If

                If pozycja = Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy Then
                    Return tor.NalezyDoOdcinka
                ElseIf pozycja = Zaleznosci.PrzynaleznoscToruDoOdcinka.Drugi Then
                    Return podw.NalezyDoOdcinkaDrugi
                End If
            End If
        End If

        Return Nothing
    End Function

    Private Function PobierzFragmentToruPodwojnego(klik As Point, tor As Zaleznosci.TorPodwojnyNiezalezny) As Zaleznosci.PrzynaleznoscToruDoOdcinka
        Dim p As PointF = WspolrzedneEkranuDoWspolrzednychKostki(klik)

        If tor.Obrot <> 0 Then
            Dim punkty As PointF() = {p}
            Dim m As New Matrix()

            m.Translate(POL, POL)
            m.Rotate(-tor.Obrot)
            m.Translate(-POL, -POL)
            m.TransformPoints(punkty)
            p = punkty(0)
        End If

        If TypeOf tor Is Zaleznosci.ZakretPodwojny Then

            If (1.0F - p.Y) < p.X Then
                Return Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy
            Else
                Return Zaleznosci.PrzynaleznoscToruDoOdcinka.Drugi
            End If

        ElseIf TypeOf tor Is Zaleznosci.Most Then

            If p.Y >= JEDNA_TRZECIA AndAlso p.Y <= 2.0F * JEDNA_TRZECIA Then
                Return Zaleznosci.PrzynaleznoscToruDoOdcinka.Pierwszy
            ElseIf p.X >= JEDNA_TRZECIA AndAlso p.X <= 2.0F * JEDNA_TRZECIA Then
                Return Zaleznosci.PrzynaleznoscToruDoOdcinka.Drugi
            End If

        End If

        Return Zaleznosci.PrzynaleznoscToruDoOdcinka.Zaden
    End Function

    Private Function ZnajdzNajblizszaKostke(x As Integer, y As Integer, przesX As Integer, przesY As Integer, maxX As Integer, maxY As Integer) As Zaleznosci.Kostka
        While x <> maxX And y <> maxY
            Dim k As Zaleznosci.Kostka = Pulpit.Kostki(x, y)
            If k IsNot Nothing Then Return k

            x += przesX
            y += przesY
        End While

        Return Nothing
    End Function

    Private Function PobierzPozycjeDlaWysrodkowania() As Point
        Return New Point(
            (Width - SzerokoscPulpitu) \ 2,
            (Height - WysokoscPulpitu) \ 2)
    End Function

    Private Function PobierzPrzeciaganaKostke(e As DragEventArgs) As Zaleznosci.Kostka
        Dim dane As PrzeciaganaKostka = TryCast(e.Data.GetData(GetType(PrzeciaganaKostka)), PrzeciaganaKostka)
        If dane IsNot Nothing AndAlso dane.Zrodlo = Handle Then
            Return dane.Kostka
        Else
            Return Nothing
        End If
    End Function

    Private Function WspolrzedneEkranuDoPulpitu(klik As Point) As PointF
        Return New PointF((klik.X - Przesuniecie.X) / Skalowanie, (klik.Y - Przesuniecie.Y) / Skalowanie)
    End Function

    Private Function WspolrzedneEkranuDoWspolrzednychKostki(klik As Point) As PointF
        Dim p As PointF = WspolrzedneEkranuDoPulpitu(klik)
        p.X -= CSng(Math.Floor(p.X))
        p.Y -= CSng(Math.Floor(p.Y))
        Return p
    End Function

    Private Function PobierzKliknieteWspolrzedneKostki(klik As Point) As Zaleznosci.PunktCalkowity
        Dim p As PointF = WspolrzedneEkranuDoPulpitu(klik)
        Return New Zaleznosci.PunktCalkowity(p.X - POL, p.Y - POL)
    End Function

    Private Function PobierzKliknietyLicznikOsi(liczniki As List(Of Zaleznosci.ParaLicznikowOsi), klik As Point) As UShort?
        Dim s As PointF = WspolrzedneEkranuDoPulpitu(klik)
        Dim pol As Single = _Rysownik.KOLKO_SZER / 2.0F

        For Each l As Zaleznosci.ParaLicznikowOsi In liczniki
            If (s.X <= l.X1 + pol) And (s.X >= l.X1 - pol) And (s.Y <= l.Y1 + pol) And (s.Y >= l.Y1 - pol) Then
                Return l.Adres1
            End If

            If (s.X <= l.X2 + pol) And (s.X >= l.X2 - pol) And (s.Y <= l.Y2 + pol) And (s.Y >= l.Y2 - pol) Then
                Return l.Adres2
            End If
        Next

        Return Nothing
    End Function

    Private Function PobierzKliknietyObiektPunktowy(elementy As IEnumerable(Of IPunktSingle), klik As Point) As IPunktSingle
        Dim s As PointF = WspolrzedneEkranuDoPulpitu(klik)
        Dim pol As Single = _Rysownik.KOLKO_SZER / 2

        For Each el As IPunktSingle In elementy
            If (s.X <= el.X + pol) And (s.X >= el.X - pol) And (s.Y <= el.Y + pol) And (s.Y >= el.Y - pol) Then
                Return el
            End If
        Next

        Return Nothing
    End Function

    Private Function PobierzKliknietaRogatke(klik As Point) As Zaleznosci.PrzejazdRogatka
        If _projZaznaczonyPrzejazd IsNot Nothing Then
            Return CType(PobierzKliknietyObiektPunktowy(_projZaznaczonyPrzejazd.Rogatki, klik), Zaleznosci.PrzejazdRogatka)
        Else
            Return Nothing
        End If
    End Function

    Private Function PobierzKliknietySygnDrog(klik As Point) As Zaleznosci.PrzejazdElementWykonawczy
        If _projZaznaczonyPrzejazd IsNot Nothing Then
            Return CType(PobierzKliknietyObiektPunktowy(_projZaznaczonyPrzejazd.SygnalizatoryDrogowe, klik), Zaleznosci.PrzejazdElementWykonawczy)
        Else
            Return Nothing
        End If
    End Function

    Private Function PobierzKliknietaLampe(klik As Point) As Zaleznosci.Lampa
        Return CType(PobierzKliknietyObiektPunktowy(Pulpit.Lampy, klik), Zaleznosci.Lampa)
    End Function

    Private Function PobierzLampyWObszarze() As HashSet(Of Zaleznosci.Lampa)
        Dim lampy As New HashSet(Of Zaleznosci.Lampa)
        If PoczatekZaznaczeniaLamp.IsEmpty Or KoniecZaznaczeniaLamp.IsEmpty Then Return lampy

        Dim pocz As New PointF(Math.Min(PoczatekZaznaczeniaLamp.X, KoniecZaznaczeniaLamp.X), Math.Min(PoczatekZaznaczeniaLamp.Y, KoniecZaznaczeniaLamp.Y))
        Dim konc As New PointF(Math.Max(PoczatekZaznaczeniaLamp.X, KoniecZaznaczeniaLamp.X), Math.Max(PoczatekZaznaczeniaLamp.Y, KoniecZaznaczeniaLamp.Y))

        For Each l As Zaleznosci.Lampa In Pulpit.Lampy
            If l.X >= pocz.X AndAlso l.X <= konc.X AndAlso l.Y >= pocz.Y AndAlso l.Y <= konc.Y Then
                lampy.Add(l)
            End If
        Next

        Return lampy
    End Function

    Private Function PorwonajLampyWObszarze(poprz As HashSet(Of Zaleznosci.Lampa), nowe As HashSet(Of Zaleznosci.Lampa)) As Boolean
        Dim zmiana As Boolean = False

        For Each l As Zaleznosci.Lampa In poprz
            If Not nowe.Contains(l) Then
                _ZaznaczoneLampy.Remove(l)
                KolejnoscZaznaczeniaLamp.Remove(l)
                zmiana = True
            End If
        Next

        For Each l As Zaleznosci.Lampa In nowe
            If Not poprz.Contains(l) And Not _ZaznaczoneLampy.Contains(l) Then
                _ZaznaczoneLampy.Add(l)
                KolejnoscZaznaczeniaLamp.Add(l)
                zmiana = True
            End If
        Next

        Return zmiana
    End Function

    Private Sub WylaczWcisnieciePrzycisku()
        If WcisnietyPrzycisk IsNot Nothing Then
            tmrPrzycisk.Stop()
            WcisnietyPrzycisk.Wcisniety = False
            WcisnietyPrzycisk = Nothing
            Invalidate()
        End If
    End Sub

    Private Sub UstawMigacz(migacz As Zaleznosci.IMigacz)
        Me.Migacz = migacz
        Pulpit.PrzeiterujKostki(Sub(x, y, k) k.Migacz = migacz)
    End Sub
End Class