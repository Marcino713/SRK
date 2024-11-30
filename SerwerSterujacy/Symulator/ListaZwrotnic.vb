Imports System.ComponentModel

Friend Class ListaZwrotnic
    Private Const KOLKO_PRZEWIN As Integer = 1
    Private Const GRUBOSC_LINII As Single = 1.0F
    Private Const KWADRAT_MARGINES As Single = 4.0F
    Private Const TEKST_NAZWA As String = "Nazwa"
    Private Const TEKST_STAN_USTAWIANY As String = "Stan ustawiany"
    Private Const TEKST_STAN_OBECNY As String = "Stan obecny"
    Private Const TEKST_PLUS As String = "+"
    Private Const TEKST_MINUS As String = "-"
    Private Const MIERZONY_TEKST As String = "Yy"
    Private Const KATEGORIA As String = "Konfiguracja"

    Private ReadOnly KOLOR_KWADRAT_WYLACZONY As Color = Pulpit.KolorRGB("#A0A0A0")
    Private ReadOnly KOLOR_KWADRAT_DOSTEPNY As Color = Pulpit.KolorRGB("#000000")
    Private ReadOnly PEDZEL_NAGLOWEK As Brush = New SolidBrush(Pulpit.KolorRGB("#DCDCDC"))
    Private ReadOnly PEDZEL_STAN_NOWY As Brush = New SolidBrush(Pulpit.KolorRGB("#FF3849"))
    Private ReadOnly PEDZEL_STAN_ROZNY As Brush = New SolidBrush(Pulpit.KolorRGB("#38FFA5"))
    Private ReadOnly PEDZEL_ZAZNACZONY As Brush = New SolidBrush(Pulpit.KolorRGB("#383FFF"))
    Private ReadOnly OLOWEK_LINIE As Pen = New Pen(Pulpit.KolorRGB("#1E1E1E"), GRUBOSC_LINII)
    Private ReadOnly PEDZEL_KWADRAT_PUSTY As Brush = New SolidBrush(Pulpit.KolorRGB("#FFFFFF"))
    Private ReadOnly PEDZEL_KWADRAT_WYLACZONY As Brush = New SolidBrush(KOLOR_KWADRAT_WYLACZONY)
    Private ReadOnly PEDZEL_KWADRAT_ZAZNACZONY As Brush = New SolidBrush(KOLOR_KWADRAT_DOSTEPNY)
    Private ReadOnly OLOWEK_KWADRAT_WYLACZONY As Pen = New Pen(KOLOR_KWADRAT_WYLACZONY, GRUBOSC_LINII)
    Private ReadOnly OLOWEK_KWADRAT_DOSTEPNY As Pen = New Pen(KOLOR_KWADRAT_DOSTEPNY, GRUBOSC_LINII)

    Private _automatyczne As Boolean = False
    <Description("Określa, czy zwrotnica jest automatycznie ustawiana na zadany stan"), Category(KATEGORIA), DefaultValue(False)>
    Public Property Automatyczne As Boolean
        Get
            Return _automatyczne
        End Get
        Set(value As Boolean)
            If value <> _automatyczne Then
                _automatyczne = value
                Invalidate()
            End If
        End Set
    End Property

    Private _SzerokoscKolumnyNazwa As Single = 150.0F
    <Description("Szerokość kolumny z nazwą zwrotnicy"), Category(KATEGORIA), DefaultValue(150.0F)>
    Public Property SzerokoscKolumnyNazwa As Single
        Get
            Return _SzerokoscKolumnyNazwa
        End Get
        Set(value As Single)
            _SzerokoscKolumnyNazwa = value
            ZmierzTekst()
            Invalidate()
        End Set
    End Property

    Private _SzerokoscKolumnyStan As Single = 50.0F
    <Description("Szerokość kolumn z polami stanu zwrotnicy"), Category(KATEGORIA), DefaultValue(50.0F)>
    Public Property SzerokoscKolumnyStan As Single
        Get
            Return _SzerokoscKolumnyStan
        End Get
        Set(value As Single)
            _SzerokoscKolumnyStan = value
            ZmierzTekst()
            Invalidate()
        End Set
    End Property

    Private _MarginesTekstu As Single = 4.0F
    <Description("Pionowy margines wewnętrzny w komórce tabeli"), Category(KATEGORIA), DefaultValue(4.0F)>
    Public Property MarginesTekstu As Single
        Get
            Return _MarginesTekstu
        End Get
        Set(value As Single)
            _MarginesTekstu = value
            ZmierzTekst()
            PrzesunPasekPrzewijania()
            UstawPasekPrzewijania()
            Invalidate()
        End Set
    End Property

    Private _zaznaczonaZwrotnica As UShort?
    <Browsable(False)>
    Public Property ZaznaczonaZwrotnica As UShort?
        Get
            Return _zaznaczonaZwrotnica
        End Get
        Set(value As UShort?)
            If Wspolne.CzyRozne(value, _zaznaczonaZwrotnica) Then
                _zaznaczonaZwrotnica = value
                Invalidate()
            End If
        End Set
    End Property

    <Description("Użytkownik zmienił obecny stan zwrotnicy")>
    Public Event ZmienionoStanZwrotnicy(adres As UShort, stan As Zaleznosci.StanRozjazdu)

    <Description("Użytkownik zmienił zaznaczoną zwrotnicę")>
    Public Event ZmienionoZaznaczenieZwrotnicy(adres As UShort?)

    Private zwrotniceSlownik As Dictionary(Of UShort, DaneZwrotnicy)
    Private zwrotnice As DaneZwrotnicy()
    Private wysLinii As Single = 1.0F
    Private xNazwa As Single
    Private xStanUstawiany As Single
    Private xStanObecny As Single
    Private xPlus As Single
    Private xMinus As Single
    Private szerokosc As Single
    Private pierwszyWiersz As Integer = 0

    Friend Sub New()
        InitializeComponent()
        DoubleBuffered = True
        ZmierzTekst()
        PrzesunPasekPrzewijania()
    End Sub

    Public Sub PokazZwrotnice(zwrotn As Zaleznosci.Rozjazd())
        zwrotniceSlownik = New Dictionary(Of UShort, DaneZwrotnicy)(zwrotn.Length)
        ReDim zwrotnice(zwrotn.Length - 1)

        For i As Integer = 0 To zwrotn.Length - 1
            Dim z As Zaleznosci.Rozjazd = zwrotn(i)

            If Not zwrotniceSlownik.ContainsKey(z.Adres) Then
                Dim dz As New DaneZwrotnicy() With {.Adres = z.Adres, .Nazwa = z.Nazwa}
                zwrotniceSlownik.Add(z.Adres, dz)
                zwrotnice(i) = dz
            End If
        Next
    End Sub

    Public Function UstawZwrotnice(adres As UShort, stan As Zaleznosci.UstawienieRozjazduEnum) As Boolean
        Dim zwrotn As DaneZwrotnicy = Nothing
        Dim wynik As Boolean = False

        If zwrotniceSlownik?.TryGetValue(adres, zwrotn) = True Then
            zwrotn.StanUstawiany = stan

            If _automatyczne Then
                zwrotn.StanObecny = If(stan = Zaleznosci.UstawienieRozjazduEnum.Wprost, Zaleznosci.StanRozjazdu.Wprost, Zaleznosci.StanRozjazdu.Bok)
            Else
                wynik = True
                zwrotn.ZmianaStanu = True
            End If

            Invalidate()
        End If

        Return wynik
    End Function

    Private Sub ListaZwrotnic_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(BackColor)
        RysujNaglowek(e.Graphics)

        Dim x As Integer
        Dim y As Single = 2.0F * wysLinii
        Dim yKwadrat As Integer
        Dim rozmKwadrat As Single = wysLinii - 2.0F * KWADRAT_MARGINES

        If zwrotnice IsNot Nothing Then
            For i As Integer = pierwszyWiersz To zwrotnice.Length - 1
                Dim z As DaneZwrotnicy = zwrotnice(i)

                If z.ZmianaStanu Then
                    RysujZaznaczenie(e.Graphics, PEDZEL_STAN_NOWY, y)
                ElseIf _zaznaczonaZwrotnica.HasValue AndAlso z.Adres = _zaznaczonaZwrotnica.Value Then
                    RysujZaznaczenie(e.Graphics, PEDZEL_ZAZNACZONY, y)
                ElseIf z.StanUstawiany <> z.StanObecny Then
                    RysujZaznaczenie(e.Graphics, PEDZEL_STAN_ROZNY, y)
                End If

                e.Graphics.DrawString(z.Nazwa, Font, Brushes.Black, New PointF(0.0F, _MarginesTekstu + y))
                yKwadrat = CInt(y + KWADRAT_MARGINES)

                x = CInt((_SzerokoscKolumnyStan - rozmKwadrat) / 2.0F + _SzerokoscKolumnyNazwa)
                RysujKwadrat(e.Graphics, x, yKwadrat, False, z.StanUstawiany = Zaleznosci.UstawienieRozjazduEnum.Wprost)

                x += CInt(_SzerokoscKolumnyStan)
                RysujKwadrat(e.Graphics, x, yKwadrat, False, z.StanUstawiany = Zaleznosci.UstawienieRozjazduEnum.Bok)

                x += CInt(_SzerokoscKolumnyStan)
                RysujKwadrat(e.Graphics, x, yKwadrat, Not _automatyczne, (z.StanObecny And Zaleznosci.StanRozjazdu.Wprost) <> 0)

                x += CInt(_SzerokoscKolumnyStan)
                RysujKwadrat(e.Graphics, x, yKwadrat, Not _automatyczne, (z.StanObecny And Zaleznosci.StanRozjazdu.Bok) <> 0)

                y += wysLinii
            Next
        End If
    End Sub

    Private Sub ListaZwrotnic_FontChanged() Handles Me.FontChanged
        ZmierzTekst()
    End Sub

    Private Sub ListaZwrotnic_Resize() Handles Me.Resize
        UstawPasekPrzewijania()
        Invalidate()
    End Sub

    Private Sub ListaZwrotnic_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Dim kliknietoNazwe As Boolean = e.X < _SzerokoscKolumnyNazwa
        Dim kliknietoStanObecny As Boolean = (e.X >= PobierzXKolumny(2)) And (e.X <= PobierzXKolumny(4))

        If kliknietoNazwe Or kliknietoStanObecny Then
            Dim y As Integer = CInt(Math.Floor((e.Y - 2.0F * wysLinii) / wysLinii)) + pierwszyWiersz

            If y >= 0 And y < zwrotnice.Length Then
                Dim z As DaneZwrotnicy = zwrotnice(y)

                If kliknietoNazwe Then
                    _zaznaczonaZwrotnica = z.Adres
                    RaiseEvent ZmienionoZaznaczenieZwrotnicy(_zaznaczonaZwrotnica)

                ElseIf Not _automatyczne Then
                    Dim stan As Zaleznosci.StanRozjazdu = Zaleznosci.StanRozjazdu.Wprost
                    If e.X >= PobierzXKolumny(3) Then stan = Zaleznosci.StanRozjazdu.Bok
                    z.ZmianaStanu = False
                    z.StanObecny = z.StanObecny Xor stan
                    RaiseEvent ZmienionoStanZwrotnicy(z.Adres, z.StanObecny)

                End If

                Invalidate()
            End If
        End If
    End Sub

    Private Sub ListaZwrotnic_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If vscPrzewin.Visible Then
            Dim nowa As Integer = vscPrzewin.Value + If(e.Delta < 0, KOLKO_PRZEWIN, -KOLKO_PRZEWIN)

            If nowa < vscPrzewin.Minimum Then
                nowa = vscPrzewin.Minimum
            ElseIf nowa > vscPrzewin.Maximum Then
                nowa = vscPrzewin.Maximum
            End If
            vscPrzewin.Value = nowa
        End If
    End Sub

    Private Sub vscPrzewin_ValueChanged() Handles vscPrzewin.ValueChanged
        If vscPrzewin.Value <> pierwszyWiersz Then
            pierwszyWiersz = vscPrzewin.Value
            Invalidate()
        End If
    End Sub

    Private Sub RysujNaglowek(gr As Graphics)
        gr.FillRectangle(PEDZEL_NAGLOWEK, 0.0F, 0.0F, Width, 2.0F * wysLinii)
        gr.DrawString(TEKST_NAZWA, Font, Brushes.Black, xNazwa, _MarginesTekstu + wysLinii / 2.0F)
        gr.DrawString(TEKST_STAN_USTAWIANY, Font, Brushes.Black, _SzerokoscKolumnyNazwa + xStanUstawiany, _MarginesTekstu)
        gr.DrawString(TEKST_PLUS, Font, Brushes.Black, _SzerokoscKolumnyNazwa + xPlus, _MarginesTekstu + wysLinii)
        gr.DrawString(TEKST_MINUS, Font, Brushes.Black, _SzerokoscKolumnyNazwa + _SzerokoscKolumnyStan + xMinus, _MarginesTekstu + wysLinii)
        gr.DrawString(TEKST_STAN_OBECNY, Font, Brushes.Black, _SzerokoscKolumnyNazwa + 2.0F * _SzerokoscKolumnyStan + xStanObecny, _MarginesTekstu)
        gr.DrawString(TEKST_PLUS, Font, Brushes.Black, _SzerokoscKolumnyNazwa + 2.0F * _SzerokoscKolumnyStan + xPlus, _MarginesTekstu + wysLinii)
        gr.DrawString(TEKST_MINUS, Font, Brushes.Black, _SzerokoscKolumnyNazwa + 3.0F * _SzerokoscKolumnyStan + xMinus, _MarginesTekstu + wysLinii)

        'linie nagłówka pion
        gr.DrawLine(OLOWEK_LINIE, 0.0F, 0.0F, 0.0F, 2.0F * wysLinii)
        gr.DrawLine(OLOWEK_LINIE, _SzerokoscKolumnyNazwa, 0.0F, _SzerokoscKolumnyNazwa, 2.0F * wysLinii)
        gr.DrawLine(OLOWEK_LINIE, _SzerokoscKolumnyNazwa + _SzerokoscKolumnyStan, wysLinii, _SzerokoscKolumnyNazwa + _SzerokoscKolumnyStan, 2.0F * wysLinii)
        gr.DrawLine(OLOWEK_LINIE, _SzerokoscKolumnyNazwa + 2.0F * _SzerokoscKolumnyStan, 0.0F, _SzerokoscKolumnyNazwa + 2.0F * _SzerokoscKolumnyStan, 2.0F * wysLinii)
        gr.DrawLine(OLOWEK_LINIE, _SzerokoscKolumnyNazwa + 3.0F * _SzerokoscKolumnyStan, wysLinii, _SzerokoscKolumnyNazwa + 3.0F * _SzerokoscKolumnyStan, 2.0F * wysLinii)
        gr.DrawLine(OLOWEK_LINIE, szerokosc, 0.0F, szerokosc, 2.0F * wysLinii)

        'linie nagłówka poziom
        gr.DrawLine(OLOWEK_LINIE, 0.0F, 0.0F, szerokosc, 0.0F)
        gr.DrawLine(OLOWEK_LINIE, _SzerokoscKolumnyNazwa, wysLinii, szerokosc, wysLinii)
        gr.DrawLine(OLOWEK_LINIE, 0.0F, 2.0F * wysLinii, szerokosc, 2.0F * wysLinii)
    End Sub

    Private Sub RysujZaznaczenie(gr As Graphics, pedzel As Brush, y As Single)
        gr.FillRectangle(pedzel, 0.0F, y + GRUBOSC_LINII, szerokosc + GRUBOSC_LINII, wysLinii - GRUBOSC_LINII)
    End Sub

    Private Sub RysujKwadrat(gr As Graphics, x As Integer, y As Integer, dostepny As Boolean, zaznaczony As Boolean)
        Dim rozm As Integer = CInt(wysLinii - 2.0F * KWADRAT_MARGINES - 2.0F * GRUBOSC_LINII)
        Dim rozm2 As Integer = CInt(wysLinii - 2.0F * KWADRAT_MARGINES)

        If zaznaczony Then
            gr.FillRectangle(If(dostepny, PEDZEL_KWADRAT_ZAZNACZONY, PEDZEL_KWADRAT_WYLACZONY), x, y, rozm2 + 1, rozm2 + 1)
        Else
            gr.DrawRectangle(If(dostepny, OLOWEK_KWADRAT_DOSTEPNY, OLOWEK_KWADRAT_WYLACZONY), x, y, rozm2, rozm2)
            gr.FillRectangle(PEDZEL_KWADRAT_PUSTY, CInt(x + GRUBOSC_LINII), CInt(y + GRUBOSC_LINII), rozm + 1, rozm + 1)
        End If
    End Sub

    Private Sub ZmierzTekst()
        Dim gr As Graphics = CreateGraphics()
        wysLinii = CSng(Math.Round(gr.MeasureString(MIERZONY_TEKST, Font).Height + 2.0F * _MarginesTekstu))
        xNazwa = (_SzerokoscKolumnyNazwa - gr.MeasureString(TEKST_NAZWA, Font).Width) / 2.0F
        xStanUstawiany = (2.0F * _SzerokoscKolumnyStan - gr.MeasureString(TEKST_STAN_USTAWIANY, Font).Width) / 2.0F
        xStanObecny = (2.0F * _SzerokoscKolumnyStan - gr.MeasureString(TEKST_STAN_OBECNY, Font).Width) / 2.0F
        xPlus = (_SzerokoscKolumnyStan - gr.MeasureString(TEKST_PLUS, Font).Width) / 2.0F
        xMinus = (_SzerokoscKolumnyStan - gr.MeasureString(TEKST_MINUS, Font).Width) / 2.0F
        szerokosc = _SzerokoscKolumnyNazwa + 4.0F * _SzerokoscKolumnyStan
        gr.Dispose()
    End Sub

    Private Sub PrzesunPasekPrzewijania()
        Dim y As Integer = CInt(2.0F * wysLinii + GRUBOSC_LINII)
        Dim roznica As Integer = vscPrzewin.Location.Y - y
        vscPrzewin.Location = New Point(vscPrzewin.Location.X, y)
        vscPrzewin.Size = New Size(vscPrzewin.Size.Width, vscPrzewin.Size.Height + roznica)
    End Sub

    Private Sub UstawPasekPrzewijania()
        Dim dostepne As Integer = CInt(Math.Floor(Height / wysLinii)) - 2

        If zwrotnice IsNot Nothing AndAlso dostepne < zwrotnice.Length Then
            vscPrzewin.Maximum = zwrotnice.Length - dostepne
            vscPrzewin.Visible = True
            pierwszyWiersz = vscPrzewin.Value
        Else
            pierwszyWiersz = 0
            vscPrzewin.Visible = False
        End If
    End Sub

    Private Function PobierzXKolumny(liczbaKolumnStan As Integer) As Single
        Return _SzerokoscKolumnyNazwa + liczbaKolumnStan * _SzerokoscKolumnyStan
    End Function

    Private Class DaneZwrotnicy
        Public Adres As UShort
        Public Nazwa As String
        Public StanUstawiany As Zaleznosci.UstawienieRozjazduEnum
        Public StanObecny As Zaleznosci.StanRozjazdu
        Public ZmianaStanu As Boolean
    End Class
End Class