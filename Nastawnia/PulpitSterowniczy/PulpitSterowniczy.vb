Imports System.ComponentModel

Friend Class PulpitSterowniczy
    Private Const SKALOWANIE_DOMYSLNE As Single = 50.0F
    Private Const SKALOWANIE_ZMIANA As Single = 0.05F
    Private Const SKALOWANIE_MIN As Single = 30.0F
    Private Const SKALOWANIE_MAX As Single = 200.0F
    Private Const ZWIEKSZ_OBROT As Integer = 90
    Private Const KAT_PELNY As Integer = 360
    Private Const CZAS_WCISKANIA_PRZYCISKU_MS As Integer = 300
    Private Const KATEG_KONF As String = "Konfiguracja"
    Private Const KATEG_ZDARZ As String = "Zdarzenia trybu działania"
    Private Const KATEG_ZDARZ_PROJ As String = "Zdarzenia trybu projektowego"

    Public PoczatekZaznaczeniaLamp As PointF
    Public KoniecZaznaczeniaLamp As PointF
    Private PoprzZaznLampyWObszarze As HashSet(Of Zaleznosci.Lampa)     'Nothing - tryb zaznaczania pojedynczej lampy; niepusty obiekt - tryb zaznaczania obszarem
    Private KolejnoscZaznaczeniaLamp As New List(Of Zaleznosci.Lampa)
    Private PoprzedniPunkt As New Point(-1, -1)
    Private AkceptowaniePrzeciagania As DragDropEffects = DragDropEffects.None
    Private PoprzLokalizacjaKostki As New Point(-1, -1)
    Private PierwszaObslugaPrzeciagania As Boolean = True
    Private WcisnietyPrzycisk As Zaleznosci.IPrzycisk = Nothing
    Private RysowanieWlaczone As Boolean = True

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

    Private _Skalowanie As Single = SKALOWANIE_DOMYSLNE
    <Description("Skalowanie pulpitu"), Category(KATEG_KONF), DefaultValue(SKALOWANIE_DOMYSLNE)>
    Public Property Skalowanie As Single
        Get
            Return _Skalowanie
        End Get
        Set(value As Single)
            If value < SKALOWANIE_MIN Then value = SKALOWANIE_MIN
            If value > SKALOWANIE_MAX Then value = SKALOWANIE_MAX
            _Skalowanie = value
            Invalidate()
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

    <Description("Czas od kliknięcia przycisku do reakcji pulpitu"), Category(KATEG_KONF), DefaultValue(CZAS_WCISKANIA_PRZYCISKU_MS)>
    Public Property CzasWciskaniaPrzyciskuMS As Integer = CZAS_WCISKANIA_PRZYCISKU_MS

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

    Private _ZaznaczonaKostka As Zaleznosci.Kostka
    <Browsable(False)>
    Public Property ZaznaczonaKostka As Zaleznosci.Kostka
        Get
            Return _ZaznaczonaKostka
        End Get
        Set(value As Zaleznosci.Kostka)
            If _ZaznaczonaKostka Is value Then Exit Property

            _ZaznaczonaKostka = value
            RaiseEvent ZmianaZaznaczeniaKostki(value)
            If Not TrybProjektowy Or _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic Then Invalidate()
        End Set
    End Property

    Private _ZaznaczoneLampy As New HashSet(Of Zaleznosci.Lampa)
    <Browsable(False)>
    Public ReadOnly Property ZaznaczoneLampy As HashSet(Of Zaleznosci.Lampa)
        Get
            Return New HashSet(Of Zaleznosci.Lampa)(_ZaznaczoneLampy)
        End Get
    End Property

    Private _MozliwoscZaznaczeniaToru As Boolean = False
    <Browsable(False)>
    Public Property MozliwoscZaznaczeniaToru As Boolean
        Get
            Return _MozliwoscZaznaczeniaToru
        End Get
        Set(value As Boolean)
            _MozliwoscZaznaczeniaToru = value
            If value Then
                Invalidate()
            Else
                ZaznaczonaKostka = Nothing
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

    Private _MozliwoscWcisnieciaPrzycisku As Boolean = True
    <Browsable(False)>
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
            _projZaznaczonaLampa = value
            RaiseEvent projZmianaZaznaczeniaLampy(value)
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Then Invalidate()
        End Set
    End Property

    Private _projZaznaczonyOdcinek As Zaleznosci.OdcinekToru
    <Browsable(False)>
    Public Property projZaznaczonyOdcinek As Zaleznosci.OdcinekToru
        Get
            Return _projZaznaczonyOdcinek
        End Get
        Set(value As Zaleznosci.OdcinekToru)
            _projZaznaczonyOdcinek = value
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow Then Invalidate()
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

    <Description("Wciśnięto przycisk na pulpicie"), Category(KATEG_ZDARZ)>
    Public Event WcisnietoPrzycisk(kostka As Zaleznosci.Kostka)

    <Description("Zmiana zaznaczonej kostki"), Category(KATEG_ZDARZ)>
    Public Event ZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka)

    <Description("Zmiana ostatnio zaznaczonej lampy"), Category(KATEG_ZDARZ)>
    Public Event ZmianaZaznaczeniaOstatniejLampy(lampa As Zaleznosci.Lampa)

    <Description("Zmiana zbioru zaznaczonych lamp"), Category(KATEG_ZDARZ)>
    Public Event ZmianaZaznaczeniaLamp(lampy As HashSet(Of Zaleznosci.Lampa))

    <Description("Zmiana zaznaczonej lampy"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaZaznaczeniaLampy(lampa As Zaleznosci.Lampa)

    <Description("Zmiana przypisania toru do odcinka"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaPrzypisaniaToruDoOdcinka()

    Public Sub New()
        InitializeComponent()
        DoubleBuffered = True
    End Sub

    Public Sub Czysc(Optional nowyPulpit As Zaleznosci.Pulpit = Nothing)
        _Pulpit = If(nowyPulpit, New Zaleznosci.Pulpit)
        _Skalowanie = SKALOWANIE_DOMYSLNE
        _Przesuniecie = PobierzPozycjeDlaWysrodkowania()
        _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic
        _projZaznaczonyOdcinek = Nothing
        _projZaznaczonyLicznik = Nothing
        _Rysownik.UniewaznioneSasiedztwoTorow = True
        ZaznaczonaKostka = Nothing  'przypisanie do własności zamiast zmiennej, żeby wywołały się zdarzenia
        projZaznaczonaLampa = Nothing
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
        If TrybProjektowy And ZaznaczonaKostka IsNot Nothing Then

            If e.KeyData = Keys.R Then
                Dim obrot As Integer = ZaznaczonaKostka.Obrot
                obrot = (obrot + ZWIEKSZ_OBROT) Mod KAT_PELNY
                ZaznaczonaKostka.Obrot = obrot
                _Rysownik.UniewaznioneSasiedztwoTorow = True
                Invalidate()

            ElseIf e.KeyData = Keys.Delete Then
                Pulpit.UsunKostke(ZaznaczonaKostka)
                _Rysownik.UniewaznioneSasiedztwoTorow = True
                ZaznaczonaKostka = Nothing
            End If

        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Dim p As Point = PobierzKliknieteWspolrzedneKostki(e.Location)

        If TrybProjektowy Then

            If projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow Then
                If _Pulpit.CzyKostkaWZakresiePulpitu(p) Then
                    Dim kostka As Zaleznosci.Kostka = Pulpit.Kostki(p.X, p.Y)
                    If kostka IsNot Nothing AndAlso TypeOf kostka Is Zaleznosci.Tor AndAlso projZaznaczonyOdcinek IsNot Nothing Then

                        Dim t As Zaleznosci.Tor = DirectCast(kostka, Zaleznosci.Tor)
                        Dim nalezyDoTegoOdcinka As Boolean = t.NalezyDoOdcinka Is projZaznaczonyOdcinek
                        If t.NalezyDoOdcinka IsNot Nothing Then t.NalezyDoOdcinka.KostkiTory.Remove(t)
                        If nalezyDoTegoOdcinka Then
                            t.NalezyDoOdcinka = Nothing
                        Else
                            t.NalezyDoOdcinka = projZaznaczonyOdcinek
                            projZaznaczonyOdcinek.KostkiTory.Add(t)
                        End If
                        RaiseEvent projZmianaPrzypisaniaToruDoOdcinka()
                        Invalidate()

                    End If
                End If

            ElseIf projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Then
                projZaznaczonaLampa = PobierzKliknietaLampe(e.Location)

            ElseIf projDodatkoweObiekty = RysujDodatkoweObiekty.Nic Then

                If _Pulpit.CzyKostkaNiepusta(p) Then
                    ZaznaczonaKostka = Pulpit.Kostki(p.X, p.Y)
                Else
                    ZaznaczonaKostka = Nothing
                End If

            End If

        Else

            If MozliwoscZaznaczeniaToru Then

                Dim zaznaczono As Boolean = False

                If _Pulpit.CzyKostkaNiepusta(p) Then
                    Dim k As Zaleznosci.Kostka = Pulpit.Kostki(p.X, p.Y)
                    If Zaleznosci.Kostka.CzyTorBezRozjazdu(k.Typ) Then
                        ZaznaczonaKostka = k
                        zaznaczono = True
                    End If
                End If

                If Not zaznaczono Then ZaznaczonaKostka = Nothing

            End If

            If MozliwoscZaznaczeniaLamp And PoprzZaznLampyWObszarze Is Nothing Then     'zaznaczenie pojedynczej lampy kliknięciem
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

        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Dim wspx As Double = (e.X - Przesuniecie.X) / SzerokoscPulpitu
        Dim wspy As Double = (e.Y - Przesuniecie.Y) / WysokoscPulpitu

        RysowanieWlaczone = False
        Skalowanie += e.Delta * SKALOWANIE_ZMIANA
        RysowanieWlaczone = True

        Dim nowy As New Point(CInt(wspx * SzerokoscPulpitu), CInt(wspy * WysokoscPulpitu))
        Przesuniecie = New Point(e.X - nowy.X, e.Y - nowy.Y)
    End Sub

    Private Sub PulpitSterowniczy_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left Then
            If PoprzedniPunkt.X <> -1 Then    'przesuń
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

    Private Sub PulpitSterowniczy_MouseUp() Handles Me.MouseUp
        PoprzedniPunkt.X = -1
        PoprzedniPunkt.Y = -1

        If Not TrybProjektowy And _MozliwoscZaznaczeniaLamp And PoprzZaznLampyWObszarze IsNot Nothing Then
            PoczatekZaznaczeniaLamp = New PointF
            KoniecZaznaczeniaLamp = New PointF
            Invalidate()
        End If

        WylaczWcisnieciePrzycisku()
    End Sub

    Private Sub PulpitSterowniczy_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
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

        Dim p As Point = PobierzKliknieteWspolrzedneKostki(e.Location)

        If TrybProjektowy And projDodatkoweObiekty = RysujDodatkoweObiekty.Nic And ((ModifierKeys And Keys.Shift) <> 0) Then      'Przesuń kostkę
            If _Pulpit.CzyKostkaNiepusta(p) Then
                PoprzLokalizacjaKostki = p
                DoDragDrop(Pulpit.Kostki(p.X, p.Y), DragDropEffects.Move)
            End If
        Else        'Przesuń cały pulpit
            PoprzedniPunkt = e.Location
        End If

        If Not TrybProjektowy AndAlso _MozliwoscWcisnieciaPrzycisku AndAlso _Pulpit.CzyKostkaNiepusta(p) Then
            Dim k As Zaleznosci.Kostka = Pulpit.Kostki(p.X, p.Y)
            If Zaleznosci.Kostka.CzyPrzycisk(k.Typ) Then
                WcisnietyPrzycisk = DirectCast(k, Zaleznosci.IPrzycisk)
                WcisnietyPrzycisk.Wcisniety = True
                Invalidate()
                tmrLicznik.Interval = CzasWciskaniaPrzyciskuMS
                tmrLicznik.Start()
            End If
        End If
    End Sub

    Private Sub PulpitSterowniczy_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        If Not TrybProjektowy Or Not e.Data.GetFormats()(0).StartsWith("Zaleznosci.") Then
            e.Effect = DragDropEffects.None
            Exit Sub
        End If

        Dim p As Point = PobierzKliknieteWspolrzedneKostki(PointToClient(New Point(e.X, e.Y)))

        If _Pulpit.CzyKostkaWZakresiePulpitu(p) AndAlso (
            (PierwszaObslugaPrzeciagania AndAlso (Pulpit.Kostki(p.X, p.Y) Is Nothing Or Pulpit.Kostki(p.X, p.Y) Is PobierzDodawanaKostke(e))) Or
            (p <> PoprzLokalizacjaKostki AndAlso Pulpit.Kostki(p.X, p.Y) Is Nothing)
            ) Then

            If PierwszaObslugaPrzeciagania Then
                AkceptowaniePrzeciagania = DragDropEffects.All
                ZaznaczonaKostka = PobierzDodawanaKostke(e)
                PierwszaObslugaPrzeciagania = False
            End If

            If _Pulpit.CzyKostkaWZakresiePulpitu(PoprzLokalizacjaKostki) AndAlso Pulpit.Kostki(PoprzLokalizacjaKostki.X, PoprzLokalizacjaKostki.Y) Is ZaznaczonaKostka Then
                Pulpit.Kostki(PoprzLokalizacjaKostki.X, PoprzLokalizacjaKostki.Y) = Nothing
            End If

            Pulpit.Kostki(p.X, p.Y) = ZaznaczonaKostka
            PoprzLokalizacjaKostki = p
            _Rysownik.UniewaznioneSasiedztwoTorow = True
            Invalidate()
            Focus()
        End If

        e.Effect = AkceptowaniePrzeciagania
    End Sub

    Private Sub PulpitSterowniczy_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If Not TrybProjektowy Then Exit Sub

        If _ZaznaczonaKostka IsNot PobierzDodawanaKostke(e) Then
            AkceptowaniePrzeciagania = DragDropEffects.None
            PierwszaObslugaPrzeciagania = True
        End If
    End Sub

    Private Sub tmrLicznik_Tick() Handles tmrLicznik.Tick
        tmrLicznik.Stop()
        If WcisnietyPrzycisk IsNot Nothing Then RaiseEvent WcisnietoPrzycisk(DirectCast(WcisnietyPrzycisk, Zaleznosci.Kostka))
    End Sub

    Private Function PobierzPozycjeDlaWysrodkowania() As Point
        Return New Point(
            (Width - SzerokoscPulpitu) \ 2,
            (Height - WysokoscPulpitu) \ 2)
    End Function

    Private Function PobierzDodawanaKostke(e As DragEventArgs) As Zaleznosci.Kostka
        Return DirectCast(e.Data.GetData(e.Data.GetFormats()(0)), Zaleznosci.Kostka)
    End Function

    Private Function WspolrzedneEkranuDoPulpitu(klik As Point) As PointF
        Return New PointF((klik.X - Przesuniecie.X) / Skalowanie, (klik.Y - Przesuniecie.Y) / Skalowanie)
    End Function

    Private Function PobierzKliknieteWspolrzedneKostki(klik As Point) As Point
        Dim p As PointF = WspolrzedneEkranuDoPulpitu(klik)
        Return New Point(CInt(p.X - 0.5), CInt(p.Y - 0.5))
    End Function

    Private Function PobierzKliknietaLampe(klik As Point) As Zaleznosci.Lampa
        Dim s As PointF = WspolrzedneEkranuDoPulpitu(klik)
        Dim pol As Single = _Rysownik.KOLKO_SZER / 2

        For Each l As Zaleznosci.Lampa In Pulpit.Lampy
            If (s.X <= l.X + pol) And (s.X >= l.X - pol) And (s.Y <= l.Y + pol) And (s.Y >= l.Y - pol) Then
                Return l
            End If
        Next

        Return Nothing
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
            tmrLicznik.Stop()
            WcisnietyPrzycisk.Wcisniety = False
            WcisnietyPrzycisk = Nothing
            Invalidate()
        End If
    End Sub
End Class