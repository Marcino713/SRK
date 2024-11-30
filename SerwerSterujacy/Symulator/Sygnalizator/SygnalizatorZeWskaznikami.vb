Imports System.Drawing.Drawing2D

Friend Class SygnalizatorZeWskaznikami
    Inherits SygnalizatorPodstawowy

    Friend Const BRAK_SWIATLA As Integer = -1

    Private Const PAS_SZER As Single = 12.0F
    Private Const PAS_WYS As Single = 6.0F
    Private Const PAS_MARGINES_X As Single = 1.0F
    Private Const PAS_MARGINES_Y As Single = 4.0F
    Private Const PAS_TLO_SZER As Single = 42.0F
    Private Const PAS_TLO_WYS As Single = PAS_WYS + 3.0F * PAS_MARGINES_Y
    Private Const PAS_ZWEZANIE As Single = 10.0F
    Private Const WSKAZNIK_SZER As Single = 20.0F
    Private Const WSKAZNIK_KRESKA_DLUZSZY As Single = 5.0F
    Private Const WSKAZNIK_KRESKA_KROTSZY As Single = 3.0F

    Private dodatkoweSwiatla As DodatkoweSwiatlaSygnalizatora
    Private liczbaPasow As Integer
    Private sciezkaPasyTlo As GraphicsPath
    Private wielokatKierPrzeciwny As PointF()

    Friend Sub New(migacz As Migacz, swiatla As List(Of SwiatloSygnalizatora), swiatlaOkragle As Integer, dodatkoweSwiatla As DodatkoweSwiatlaSygnalizatora)
        MyBase.New(migacz, swiatla, swiatlaOkragle)

        Me.dodatkoweSwiatla = dodatkoweSwiatla
        If dodatkoweSwiatla.ZielonyPas <> BRAK_SWIATLA Then liczbaPasow += 1
        If dodatkoweSwiatla.PomaranczowyPas <> BRAK_SWIATLA Then liczbaPasow += 1
    End Sub

    Friend Overrides Sub Rysuj(gr As Graphics)
        MyBase.Rysuj(gr)
        Dim slupX As Single = (PAS_TLO_SZER - SLUP_SZER) / 2.0F
        Dim y As Single = (SwiatlaOkragle + 1.0F) * MODUL + MARGINES_Y

        If liczbaPasow > 0 Then
            'tło pasów
            Dim pasTloWys As Single = PAS_TLO_WYS
            If liczbaPasow = 2 Then pasTloWys += PAS_WYS + 2.0F * PAS_MARGINES_Y

            If sciezkaPasyTlo Is Nothing Then UtworzSciezkeTlaPasow(y, pasTloWys)
            gr.FillPath(Brushes.Black, sciezkaPasyTlo)

            'pasy
            y += PAS_MARGINES_Y

            If dodatkoweSwiatla.ZielonyPas <> BRAK_SWIATLA Then
                RysujPas(gr, PobierzPedzel(dodatkoweSwiatla.ZielonyPas), y)
            End If

            If liczbaPasow = 2 Then
                y += PAS_WYS + 2.0F * PAS_MARGINES_Y
            End If

            If dodatkoweSwiatla.PomaranczowyPas <> BRAK_SWIATLA Then
                RysujPas(gr, PobierzPedzel(dodatkoweSwiatla.PomaranczowyPas), y)
            End If

            'słup
            y += PAS_WYS + 2.0F * PAS_MARGINES_Y + PAS_ZWEZANIE
            gr.FillRectangle(Brushes.Gray, slupX, y, SLUP_SZER, MARGINES_Y)
            y += MARGINES_Y
        End If

        'wskaźnik kierunku przeciwnego
        If dodatkoweSwiatla.WskaznikKierPrzeciwnego <> BRAK_SWIATLA Then
            Dim wskX As Single = MODUL - WSKAZNIK_SZER / 2.0F

            If wielokatKierPrzeciwny Is Nothing Then UtworzWielokatKierunkuPrzeciwnego(wskX, y)
            gr.FillRectangle(Brushes.Black, wskX, y, WSKAZNIK_SZER, WSKAZNIK_SZER)
            gr.FillPolygon(PobierzPedzel(dodatkoweSwiatla.WskaznikKierPrzeciwnego), wielokatKierPrzeciwny)

            'słup
            y += WSKAZNIK_SZER
            gr.FillRectangle(Brushes.Gray, slupX, y, SLUP_SZER, MARGINES_Y)
        End If
    End Sub

    Private Sub RysujPas(gr As Graphics, pedzel As Brush, y As Single)
        For i As Integer = 0 To 2
            gr.FillRectangle(pedzel, (i + 2.0F) * PAS_MARGINES_X + i * PAS_SZER, y, PAS_SZER, PAS_WYS)
        Next
    End Sub

    Private Sub UtworzSciezkeTlaPasow(y As Single, pasTloWys As Single)
        sciezkaPasyTlo = New GraphicsPath()
        sciezkaPasyTlo.AddPolygon(New PointF() {
                               New PointF(0.0F, y),
                               New PointF(PAS_TLO_SZER, y),
                               New PointF(PAS_TLO_SZER, y + pasTloWys),
                               New PointF(PAS_TLO_SZER - PAS_ZWEZANIE, y + pasTloWys + PAS_ZWEZANIE),
                               New PointF(PAS_ZWEZANIE, y + pasTloWys + PAS_ZWEZANIE),
                               New PointF(0.0F, y + pasTloWys)
                               })
        sciezkaPasyTlo.CloseFigure()
    End Sub

    Private Sub UtworzWielokatKierunkuPrzeciwnego(x As Single, y As Single)
        wielokatKierPrzeciwny = New PointF() {
                New PointF(x + WSKAZNIK_KRESKA_DLUZSZY, y + WSKAZNIK_KRESKA_KROTSZY),
                New PointF(x + WSKAZNIK_SZER - WSKAZNIK_KRESKA_KROTSZY, y + WSKAZNIK_SZER - WSKAZNIK_KRESKA_DLUZSZY),
                New PointF(x + WSKAZNIK_SZER - WSKAZNIK_KRESKA_DLUZSZY, y + WSKAZNIK_SZER - WSKAZNIK_KRESKA_KROTSZY),
                New PointF(x + WSKAZNIK_KRESKA_KROTSZY, y + WSKAZNIK_KRESKA_DLUZSZY)
            }
    End Sub
End Class

Friend Class DodatkoweSwiatlaSygnalizatora
    Public ZielonyPas As Integer = SygnalizatorZeWskaznikami.BRAK_SWIATLA
    Public PomaranczowyPas As Integer = SygnalizatorZeWskaznikami.BRAK_SWIATLA
    Public WskaznikKierPrzeciwnego As Integer = SygnalizatorZeWskaznikami.BRAK_SWIATLA
End Class