Imports System.ComponentModel

Friend Class PredkoscPociagu
    Private Const ZMIANA_PREDKOSCI_KLAWISZ As UShort = 2
    Private Const PASEK_MARGINES_PION As Integer = 10
    Private Const PASEK_SZEROKOSC_POL As Integer = 3
    Private Const GRANICA_SZEROKOSC_POL As Integer = 15
    Private Const GRANICA_WYSOKOSC_POL As Integer = 2
    Private Const SUWAK_ROZMIAR_POL As Integer = 9
    Private Const TEKST_MARGINES_SRODEK As Integer = 23
    Private Const TEKST_KRESKA_MARGINES_SRODEK As Integer = 16
    Private Const TEKST_KRESKA_SZEROKOSC As Integer = 5
    Private Const KATEG_PREDKOSC As String = "Prędkość"
    Private ReadOnly PEDZEL_PASEK As Brush = New SolidBrush(KolorRGB("#10F055"))
    Private ReadOnly PEDZEL_PASEK_NIEAKTYWNY As Brush = New SolidBrush(KolorRGB("#C0C0C0"))
    Private ReadOnly PEDZEL_SUWAK As Brush = New SolidBrush(KolorRGB("#24AF4D"))
    Private ReadOnly PEDZEL_SUWAK_NIEAKTYWNY As Brush = New SolidBrush(KolorRGB("#7A7A7A"))
    Private ReadOnly PEDZEL_GRANICA As Brush = New SolidBrush(KolorRGB("#FF0000"))
    Private ReadOnly PEDZEL_TEKST As Brush = New SolidBrush(KolorRGB("#000000"))
    Private ReadOnly OLOWEK_TEKST_KRESKA As New Pen(KolorRGB("#000000"))

    Private PolozenieSuwaka As Rectangle
    Private WspolrzednaPrzeciaganiaY As Integer = 0
    Private PrzeciaganieAktywne As Boolean = False

    Private _PredkoscMaksymalna As UShort = 300
    <Description("Maksymalna prędkość, jaką może rozwinąć pociąg"), Category(KATEG_PREDKOSC), DefaultValue(300US)>
    Public Property PredkoscMaksymalna As UShort
        Get
            Return _PredkoscMaksymalna
        End Get
        Set(value As UShort)
            If value <> _PredkoscMaksymalna Then
                _PredkoscMaksymalna = value
                If _PredkoscDozwolona > value Then
                    PredkoscDozwolona = value
                Else
                    Invalidate()
                End If
            End If
        End Set
    End Property

    Private _PredkoscDozwolona As UShort = 100
    <Description("Maksymalna obecnie dozwolona prędkość, wynikająca np. z ograniczeń prędkości"), Category(KATEG_PREDKOSC), DefaultValue(100US)>
    Public Property PredkoscDozwolona As UShort
        Get
            Return _PredkoscDozwolona
        End Get
        Set(value As UShort)
            If value > _PredkoscMaksymalna Then value = _PredkoscMaksymalna
            If value <> _PredkoscDozwolona Then
                _PredkoscDozwolona = value
                If _PredkoscBiezaca > value Then
                    PredkoscBiezaca = value
                Else
                    Invalidate()
                End If
            End If
        End Set
    End Property

    Private _PredkoscBiezaca As UShort = 0
    <Description("Aktualna prędkość zadana dla pociągu"), Category(KATEG_PREDKOSC), DefaultValue(0US)>
    Public Property PredkoscBiezaca As UShort
        Get
            Return _PredkoscBiezaca
        End Get
        Set(value As UShort)
            If value > _PredkoscDozwolona Then value = _PredkoscDozwolona
            If value <> _PredkoscBiezaca Then
                _PredkoscBiezaca = value
                Invalidate()
                RaiseEvent ZmienionoPredkoscBiezaca(value)
            End If
        End Set
    End Property


    <Description("Występuje po zmianie zadanej prędkości bieżącej"), Category(KATEG_PREDKOSC)>
    Public Event ZmienionoPredkoscBiezaca(predkosc As UShort)


    Private Sub PredkoscPociagu_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Rysuj(e.Graphics)
    End Sub

    Private Sub PredkoscPociagu_ZmianaWygladu() Handles Me.Resize, Me.EnabledChanged, Me.FontChanged
        Invalidate()
    End Sub

    Private Sub PredkoscPociagu_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If Not Enabled Then Exit Sub

        If e.X >= PolozenieSuwaka.X And e.X < (PolozenieSuwaka.X + PolozenieSuwaka.Width) And e.Y >= PolozenieSuwaka.Y And e.Y < (PolozenieSuwaka.Y + PolozenieSuwaka.Height) Then
            WspolrzednaPrzeciaganiaY = e.Y - PolozenieSuwaka.Y - SUWAK_ROZMIAR_POL
            PrzeciaganieAktywne = True
        End If
    End Sub

    Private Sub PredkoscPociagu_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Enabled And PrzeciaganieAktywne Then
            PredkoscBiezaca = PobierzPredkosc(e.Y - WspolrzednaPrzeciaganiaY)
        End If
    End Sub

    Private Sub PredkoscPociagu_MouseUp() Handles Me.MouseUp
        PrzeciaganieAktywne = False
    End Sub

    Private Sub PredkoscPociagu_Click() Handles Me.Click
        Focus()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Up Or keyData = Keys.Down Then

            If keyData = Keys.Up Then
                PredkoscBiezaca += ZMIANA_PREDKOSCI_KLAWISZ
            Else
                If PredkoscBiezaca >= ZMIANA_PREDKOSCI_KLAWISZ Then
                    PredkoscBiezaca -= ZMIANA_PREDKOSCI_KLAWISZ
                Else
                    PredkoscBiezaca = 0
                End If
            End If

            Return True
        Else
            Return False
        End If
    End Function

    Private Sub Rysuj(gr As Graphics)
        gr.Clear(BackColor)

        Dim wspDozwolona As Integer = PobierzWspolrzedna(_PredkoscDozwolona)
        Dim pasek As New Rectangle(Width \ 2 - PASEK_SZEROKOSC_POL, PASEK_MARGINES_PION, 2 * PASEK_SZEROKOSC_POL, wspDozwolona - PASEK_MARGINES_PION)
        If pasek.Height > 0 Then gr.FillRectangle(PEDZEL_PASEK_NIEAKTYWNY, pasek)

        pasek.Y = wspDozwolona
        pasek.Height = Height - PASEK_MARGINES_PION - pasek.Y
        gr.FillRectangle(If(Enabled, PEDZEL_PASEK, PEDZEL_PASEK_NIEAKTYWNY), pasek)

        Dim granica As New Rectangle(Width \ 2 - GRANICA_SZEROKOSC_POL, wspDozwolona - GRANICA_WYSOKOSC_POL, 2 * GRANICA_SZEROKOSC_POL, 2 * GRANICA_WYSOKOSC_POL)
        gr.FillRectangle(If(Enabled, PEDZEL_GRANICA, PEDZEL_PASEK_NIEAKTYWNY), granica)

        PolozenieSuwaka = New Rectangle(Width \ 2 - SUWAK_ROZMIAR_POL, PobierzWspolrzedna(_PredkoscBiezaca) - SUWAK_ROZMIAR_POL, SUWAK_ROZMIAR_POL * 2, SUWAK_ROZMIAR_POL * 2)
        gr.FillRectangle(If(Enabled, PEDZEL_SUWAK, PEDZEL_SUWAK_NIEAKTYWNY), PolozenieSuwaka)

        Dim lista As New List(Of DaneTekstu)
        DopiszWartoscPredkosci(gr, lista, _PredkoscMaksymalna)
        DopiszWartoscPredkosci(gr, lista, _PredkoscDozwolona)
        DopiszWartoscPredkosci(gr, lista, 0)

        RysujListeTekstow(gr, lista)
    End Sub

    Private Sub DopiszWartoscPredkosci(gr As Graphics, lista As List(Of DaneTekstu), predkosc As UShort)
        Dim t As String = predkosc.ToString
        Dim rozm As SizeF = gr.MeasureString(t, Font)

        lista.Add(New DaneTekstu With {
            .Tekst = t,
            .PozycjaY = PobierzWspolrzedna(predkosc),
            .WysokoscTekstu = CInt(rozm.Height)
        })
    End Sub

    Private Sub RysujListeTekstow(gr As Graphics, lista As List(Of DaneTekstu))
        Dim xLinia As Integer = Width \ 2 + TEKST_KRESKA_MARGINES_SRODEK
        Dim szerLinia As Integer = xLinia + TEKST_KRESKA_SZEROKOSC
        Dim x As Integer = Width \ 2 + TEKST_MARGINES_SRODEK
        Dim y As Integer
        Dim poprzY As Integer = 0

        For Each dt As DaneTekstu In lista
            y = dt.PozycjaY - dt.WysokoscTekstu \ 2
            If y < poprzY Then y = poprzY

            gr.DrawString(dt.Tekst, Font, PEDZEL_TEKST, x, y)
            gr.DrawLine(OLOWEK_TEKST_KRESKA, xLinia, dt.PozycjaY, szerLinia, dt.PozycjaY)

            poprzY = y + dt.WysokoscTekstu
        Next
    End Sub

    Private Function PobierzPredkosc(wspY As Integer) As UShort
        Dim predkosc As Short = CShort(PredkoscMaksymalna * (1.0 - (wspY - PASEK_MARGINES_PION) / (Height - 2.0 * PASEK_MARGINES_PION)))
        If predkosc < 0 Then predkosc = 0
        Return CUShort(predkosc)
    End Function

    Private Function PobierzWspolrzedna(predkosc As UShort) As Integer
        Return CInt((1.0 - predkosc / _PredkoscMaksymalna) * (Height - 2.0 * PASEK_MARGINES_PION) + PASEK_MARGINES_PION)
    End Function

    Private Class DaneTekstu
        Friend Tekst As String
        Friend PozycjaY As Integer
        Friend WysokoscTekstu As Integer
    End Class
End Class