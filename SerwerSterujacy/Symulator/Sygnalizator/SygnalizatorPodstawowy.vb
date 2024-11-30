Imports System.Drawing.Drawing2D

Friend Class SygnalizatorPodstawowy
    Inherits Sygnalizator

    Protected SwiatlaOkragle As Integer
    Private sciezkaTlo As GraphicsPath

    Friend Overrides ReadOnly Property Szerokosc As Single
        Get
            Return MODUL_2
        End Get
    End Property

    Friend Sub New(migacz As Migacz, swiatla As List(Of SwiatloSygnalizatora), swiatlaOkragle As Integer)
        MyBase.New(migacz, swiatla)
        Me.SwiatlaOkragle = swiatlaOkragle
    End Sub

    Friend Overrides Sub Rysuj(gr As Graphics)
        'słup
        gr.FillRectangle(Brushes.Gray, (MODUL_2 - SLUP_SZER) / 2.0F, (SwiatlaOkragle + 1.0F) * MODUL - PROMIEN, SLUP_SZER, PROMIEN + MARGINES_Y)

        'tło tarczy
        If sciezkaTlo Is Nothing Then UtworzSciezkeTla()
        gr.FillPath(Brushes.Black, sciezkaTlo)

        'światła okrągłe
        For i As Integer = 0 To SwiatlaOkragle - 1
            gr.FillEllipse(PobierzPedzel(i), MODUL - PROMIEN, (i + 1.0F) * MODUL - PROMIEN, SREDNICA, SREDNICA)
        Next
    End Sub

    Private Sub UtworzSciezkeTla()
        sciezkaTlo = New GraphicsPath
        sciezkaTlo.AddArc(0.0F, 0.0F, MODUL_2, MODUL_2, KAT_POLPELNY, KAT_POLPELNY)
        sciezkaTlo.AddArc(0.0F, (SwiatlaOkragle - 1.0F) * MODUL, MODUL_2, MODUL_2, 0.0F, KAT_POLPELNY)
        sciezkaTlo.CloseFigure()
    End Sub
End Class