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

    Private _Przesuniecie As Point = New Point(0, 0)
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
            End Select

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
            Invalidate()
        End Set
    End Property

    Private _projDodatkoweObiekty As RysujDodatkoweObiekty
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

    Private _projZaznaczonaKostka As Zaleznosci.Kostka
    <Browsable(False)>
    Public Property projZaznaczonaKostka As Zaleznosci.Kostka
        Get
            Return _projZaznaczonaKostka
        End Get
        Set(value As Zaleznosci.Kostka)
            _projZaznaczonaKostka = value
            RaiseEvent projZmianaZaznaczeniaKostki(value)
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic Then Invalidate()
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
            If _projDodatkoweObiekty = RysujDodatkoweObiekty.Tory Then Invalidate()
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

    Private PoprzedniPunkt As New Point(-1, -1)
    Private AkceptowaniePrzeciagania As DragDropEffects = DragDropEffects.None
    Private PoprzLokalizacjaKostki As New Point(-1, -1)
    Private PierwszaObslugaPrzeciagania As Boolean = True
    Private WcisnietyPrzycisk As Zaleznosci.IPrzycisk = Nothing

    <Description("Wciśnięto przycisk na pulpicie"), Category(KATEG_ZDARZ)>
    Public Event WcisnietoPrzycisk(kostka As Zaleznosci.Kostka)

    <Description("Zmiana zaznaczonej kostki"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka)

    <Description("Zmiana zaznaczonej lampy"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaZaznaczeniaLampy(lampa As Zaleznosci.Lampa)

    <Description("Zmiana przypisania toru do odcinka"), Category(KATEG_ZDARZ_PROJ)>
    Public Event projZmianaPrzypisaniaToruDoOdcinka()

    Public Sub New()
        InitializeComponent()
        DoubleBuffered = True
    End Sub

    Public Sub Czysc(Optional nowyPulpit As Zaleznosci.Pulpit = Nothing)
        _Skalowanie = SKALOWANIE_DOMYSLNE
        _Przesuniecie = New Point(0, 0)
        _Pulpit = If(nowyPulpit Is Nothing, New Zaleznosci.Pulpit, nowyPulpit)
        _projDodatkoweObiekty = RysujDodatkoweObiekty.Nic
        _projZaznaczonyOdcinek = Nothing
        _projZaznaczonyLicznik = Nothing
        projZaznaczonaKostka = Nothing  'przypisanie do własności zamiast zmiennej, żeby wywołały się zdarzenia
        projZaznaczonaLampa = Nothing
    End Sub

    Private Sub PulpitSterowniczy_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        _Rysownik.Rysuj(Me, e.Graphics)
    End Sub

    Private Sub PulpitSterowniczy_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If TrybProjektowy And projZaznaczonaKostka IsNot Nothing Then

            If e.KeyData = Keys.R Then
                Dim obrot As Integer = projZaznaczonaKostka.Obrot
                obrot = (obrot + ZWIEKSZ_OBROT) Mod KAT_PELNY
                projZaznaczonaKostka.Obrot = obrot
                Invalidate()

            ElseIf e.KeyData = Keys.Delete
                Pulpit.UsunKostke(projZaznaczonaKostka)
                projZaznaczonaKostka = Nothing
            End If

        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Dim p As Point = PobierzKliknieteWspolrzedneKostki(e.Location)

        If TrybProjektowy Then

            If projDodatkoweObiekty = RysujDodatkoweObiekty.Tory Then
                If CzyKostkaWZakresiePulpitu(p) Then
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

            ElseIf projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy
                projZaznaczonaLampa = PobierzKliknietaLampe(e.Location)

            ElseIf projDodatkoweObiekty = RysujDodatkoweObiekty.Nic
                If CzyKostkaNiepusta(p) Then
                    projZaznaczonaKostka = Pulpit.Kostki(p.X, p.Y)
                Else
                    projZaznaczonaKostka = Nothing
                End If

            End If
        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Dim wspx As Double = (e.X - Przesuniecie.X) / SzerokoscPulpitu
        Dim wspy As Double = (e.Y - Przesuniecie.Y) / WysokoscPulpitu

        Skalowanie = Skalowanie + e.Delta * SKALOWANIE_ZMIANA

        Dim nowy As New Point(CInt(wspx * SzerokoscPulpitu), CInt(wspy * WysokoscPulpitu))
        Przesuniecie = New Point(e.X - nowy.X, e.Y - nowy.Y)
    End Sub

    Private Sub PulpitSterowniczy_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = MouseButtons.Left AndAlso PoprzedniPunkt.X <> -1 Then
            Dim zmX As Integer = e.X - PoprzedniPunkt.X
            Dim zmY As Integer = e.Y - PoprzedniPunkt.Y
            Przesuniecie = New Point(Przesuniecie.X + zmX, Przesuniecie.Y + zmY)
            PoprzedniPunkt = e.Location
        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseUp() Handles Me.MouseUp
        PoprzedniPunkt.X = -1
        PoprzedniPunkt.Y = -1

        If WcisnietyPrzycisk IsNot Nothing Then
            tmrLicznik.Stop()
            WcisnietyPrzycisk.Wcisniety = False
            WcisnietyPrzycisk = Nothing
            Invalidate()
        End If
    End Sub

    Private Sub PulpitSterowniczy_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Dim p As Point = PobierzKliknieteWspolrzedneKostki(e.Location)

        If TrybProjektowy And projDodatkoweObiekty = RysujDodatkoweObiekty.Nic And ((ModifierKeys And Keys.Shift) <> 0) Then      'Przesuń kostkę
            If CzyKostkaNiepusta(p) Then
                PoprzLokalizacjaKostki = p
                DoDragDrop(Pulpit.Kostki(p.X, p.Y), DragDropEffects.Move)
            End If
        Else        'Przesuń cały pulpit
            PoprzedniPunkt = e.Location
        End If

        If Not TrybProjektowy AndAlso CzyKostkaNiepusta(p) Then
            Dim k As Zaleznosci.Kostka = Pulpit.Kostki(p.X, p.Y)
            If Zaleznosci.CzyPrzycisk(k.Typ) Then
                WcisnietyPrzycisk = DirectCast(k, Zaleznosci.IPrzycisk)
                WcisnietyPrzycisk.Wcisniety = True
                Invalidate()
                tmrLicznik.Interval = CzasWciskaniaPrzyciskuMS
                tmrLicznik.Start()
            End If
        End If
    End Sub

    Private Sub PulpitSterowniczy_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        If Not e.Data.GetFormats()(0).StartsWith("Zaleznosci.") Then
            e.Effect = DragDropEffects.None
            Exit Sub
        End If

        Dim p As Point = PobierzKliknieteWspolrzedneKostki(PointToClient(New Point(e.X, e.Y)))

        If CzyKostkaWZakresiePulpitu(p) AndAlso (
            (PierwszaObslugaPrzeciagania AndAlso (Pulpit.Kostki(p.X, p.Y) Is Nothing Or Pulpit.Kostki(p.X, p.Y) Is PobierzDodawanaKostke(e))) Or
            (p <> PoprzLokalizacjaKostki AndAlso Pulpit.Kostki(p.X, p.Y) Is Nothing)
            ) Then

            If PierwszaObslugaPrzeciagania Then
                AkceptowaniePrzeciagania = DragDropEffects.All
                projZaznaczonaKostka = PobierzDodawanaKostke(e)
                PierwszaObslugaPrzeciagania = False
            End If

            If CzyKostkaWZakresiePulpitu(PoprzLokalizacjaKostki) AndAlso Pulpit.Kostki(PoprzLokalizacjaKostki.X, PoprzLokalizacjaKostki.Y) Is projZaznaczonaKostka Then
                Pulpit.Kostki(PoprzLokalizacjaKostki.X, PoprzLokalizacjaKostki.Y) = Nothing
            End If

            Pulpit.Kostki(p.X, p.Y) = projZaznaczonaKostka
            PoprzLokalizacjaKostki = p
            Invalidate()
        End If

        e.Effect = AkceptowaniePrzeciagania
    End Sub

    Private Sub PulpitSterowniczy_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If _projZaznaczonaKostka IsNot PobierzDodawanaKostke(e) Then
            AkceptowaniePrzeciagania = DragDropEffects.None
            PierwszaObslugaPrzeciagania = True
        End If
    End Sub

    Private Sub tmrLicznik_Tick() Handles tmrLicznik.Tick
        tmrLicznik.Stop()
        If WcisnietyPrzycisk IsNot Nothing Then RaiseEvent WcisnietoPrzycisk(DirectCast(WcisnietyPrzycisk, Zaleznosci.Kostka))
    End Sub

    Private Function PobierzDodawanaKostke(e As DragEventArgs) As Zaleznosci.Kostka
        Return DirectCast(e.Data.GetData(e.Data.GetFormats()(0)), Zaleznosci.Kostka)
    End Function

    Private Function CzyKostkaWZakresiePulpitu(wspolrzedne As Point) As Boolean
        Return wspolrzedne.X >= 0 And wspolrzedne.X < Pulpit.Szerokosc And wspolrzedne.Y >= 0 And wspolrzedne.Y < Pulpit.Wysokosc
    End Function

    Private Function CzyKostkaNiepusta(wspolrzedne As Point) As Boolean
        Return CzyKostkaWZakresiePulpitu(wspolrzedne) AndAlso Pulpit.Kostki(wspolrzedne.X, wspolrzedne.Y) IsNot Nothing
    End Function

    Private Function PobierzKliknieteWspolrzedneKostki(klik As Point) As Point
        Return New Point(
        CInt((klik.X - Przesuniecie.X) / Skalowanie - 0.5),
        CInt((klik.Y - Przesuniecie.Y) / Skalowanie - 0.5)
        )
    End Function

    Private Function PobierzKliknietaLampe(klik As Point) As Zaleznosci.Lampa
        Dim s As PointF = New PointF((klik.X - Przesuniecie.X) / Skalowanie, (klik.Y - Przesuniecie.Y) / Skalowanie)
        Dim pol As Single = _Rysownik.KOLKO_SZER / 2
        Dim en As List(Of Zaleznosci.Lampa).Enumerator = Pulpit.Lampy.GetEnumerator

        While en.MoveNext
            Dim l As Zaleznosci.Lampa = en.Current
            If (s.X <= l.X + pol) And (s.X >= l.X - pol) And (s.Y <= l.Y + pol) And (s.Y >= l.Y - pol) Then
                Return l
            End If
        End While

        Return Nothing
    End Function
End Class