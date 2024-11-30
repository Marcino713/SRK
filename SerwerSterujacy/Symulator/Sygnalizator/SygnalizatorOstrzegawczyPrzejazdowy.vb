Imports System.Drawing.Drawing2D

Friend Class SygnalizatorOstrzegawczyPrzejazdowy
    Inherits Sygnalizator

    Private Const TOP_KAT_ZGIECIA As Single = 30.0F
    Private Const MODUL_3 As Single = 3.0F * MODUL

    Private Shared ReadOnly SCIEZKA_KONTUR As GraphicsPath = UtworzSciezkeTOP(0.0F)
    Private Shared ReadOnly SCIEZKA_TLO As GraphicsPath = UtworzSciezkeTOP(3.0F)

    Friend Overrides ReadOnly Property Szerokosc As Single
        Get
            Return MODUL_3
        End Get
    End Property

    Friend Sub New(migacz As Migacz, swiatla As List(Of SwiatloSygnalizatora))
        MyBase.New(migacz, swiatla)
    End Sub

    Friend Overrides Sub Rysuj(gr As Graphics)
        'słup
        gr.FillRectangle(Brushes.Gray, (MODUL_3 - SLUP_SZER) / 2.0F, 4.0F * MODUL, SLUP_SZER, MARGINES_Y)

        'kontur, tło
        gr.FillPath(Brushes.White, SCIEZKA_KONTUR)
        gr.DrawPath(Pens.Black, SCIEZKA_KONTUR)
        gr.FillPath(Brushes.Black, SCIEZKA_TLO)

        'światła
        gr.FillEllipse(PobierzPedzel(0), MODUL - PROMIEN, MODUL - PROMIEN, SREDNICA, SREDNICA)
        gr.FillEllipse(PobierzPedzel(1), MODUL - PROMIEN, MODUL_2 - PROMIEN, SREDNICA, SREDNICA)
        gr.FillEllipse(PobierzPedzel(2), MODUL - PROMIEN, MODUL_3 - PROMIEN, SREDNICA, SREDNICA)
        gr.FillEllipse(PobierzPedzel(3), MODUL_2 - PROMIEN, MODUL_3 - PROMIEN, SREDNICA, SREDNICA)
    End Sub

    Private Shared Function UtworzSciezkeTOP(margines As Single) As GraphicsPath
        Dim sciezka As New GraphicsPath()

        'od prawego dolnego rogu
        sciezka.AddArc(New RectangleF(MODUL, MODUL_2, MODUL_2 - margines, MODUL_2 - margines), -TOP_KAT_ZGIECIA, KAT_PROSTY + TOP_KAT_ZGIECIA)
        sciezka.AddArc(New RectangleF(margines, MODUL_2, MODUL_2 - margines, MODUL_2 - margines), KAT_PROSTY, KAT_PROSTY)
        sciezka.AddArc(New RectangleF(margines, margines, MODUL_2 - 2.0F * margines, MODUL_2 - 2.0F * margines), 2.0F * KAT_PROSTY, KAT_PROSTY + 2.0F * TOP_KAT_ZGIECIA)

        sciezka.CloseFigure()
        Return sciezka
    End Function
End Class
