Imports System.Drawing.Drawing2D

Friend Class UrzadzenieRysujaceGDI
    Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font)

    Private Const KAT_PROSTY As Single = 90.0F

    Private gr As Graphics

    Public Sub Inicjalizuj(uchwyt As IntPtr, szer As UInteger, wys As UInteger) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).Inicjalizuj
    End Sub

    Public Sub RozpocznijRysunek(grp As Graphics) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).RozpocznijRysunek
        gr = grp
    End Sub

    Public Sub ZakonczRysunek() Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).ZakonczRysunek
    End Sub

    Public Sub ZmienRozmiar(szer As UInteger, wys As UInteger) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).ZmienRozmiar
    End Sub

    Public Function UtworzOlowek(kolor As Color) As Pen Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).UtworzOlowek
        Return New Pen(kolor)
    End Function

    Public Function UtworzPedzel(kolor As Color) As Brush Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).UtworzPedzel
        Return New SolidBrush(kolor)
    End Function

    Public Sub TransformacjaResetuj() Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).TransformacjaResetuj
        gr.ResetTransform()
    End Sub

    Public Sub TransformacjaDolacz(tr As Matrix) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).TransformacjaDolacz
        gr.MultiplyTransform(tr, MatrixOrder.Append)
    End Sub

    Public Function TransformacjaPobierz() As Matrix Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).TransformacjaPobierz
        Return gr.Transform
    End Function

    Public Sub TransformacjaPrzesun(x As Single, y As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).TransformacjaPrzesun
        gr.TranslateTransform(x, y, MatrixOrder.Append)
    End Sub

    Public Sub TransformacjaObroc(kat As Single, srodekX As Single, srodekY As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).TransformacjaObroc
        gr.TranslateTransform(-srodekX, -srodekY, MatrixOrder.Append)
        gr.RotateTransform(kat, MatrixOrder.Append)
        gr.TranslateTransform(srodekX, srodekY, MatrixOrder.Append)
    End Sub

    Public Sub TransformacjaSkaluj(skalowanie As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).TransformacjaSkaluj
        gr.ScaleTransform(skalowanie, skalowanie, MatrixOrder.Append)
    End Sub

    Public Sub RysujLinie(pedzel As Pen, grubosc As Single, x1 As Single, y1 As Single, x2 As Single, y2 As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).RysujLinie
        pedzel.Width = grubosc
        gr.DrawLine(pedzel, x1, y1, x2, y2)
    End Sub

    Public Sub RysujProstokat(pedzel As Pen, grubosc As Single, x As Single, y As Single, szer As Single, wys As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).RysujProstokat
        pedzel.Width = grubosc
        gr.DrawRectangle(pedzel, x, y, szer, wys)
    End Sub

    Public Sub Czysc(kolor As Color) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).Czysc
        gr.Clear(kolor)
    End Sub

    Public Sub WypelnijProstokat(pedzel As Brush, x As Single, y As Single, szer As Single, wys As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).WypelnijProstokat
        gr.FillRectangle(pedzel, x, y, szer, wys)
    End Sub

    Public Sub WypelnijFigure(pedzel As Brush, punkty() As PointF) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).WypelnijFigure
        gr.FillPolygon(pedzel, punkty)
    End Sub

    Public Sub WypelnijKolo(pedzel As Brush, srodekX As Single, srodekY As Single, r As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).WypelnijKolo
        gr.FillEllipse(pedzel, srodekX - r, srodekY - r, r * 2.0F, r * 2.0F)
    End Sub

    Public Sub WypelnijTloSygnalizatora(pedzel As Brush, poczX As Single, koncX As Single, y As Single, r As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).WypelnijTloSygnalizatora
        Dim r2 As Single = 2 * r
        gr.FillPie(pedzel, poczX - r, y - r, r2, r2, KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillPie(pedzel, koncX - r, y - r, r2, r2, 3 * KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillRectangle(pedzel, poczX, y - r, koncX - poczX, r2)
    End Sub

    Public Function UtworzCzcionke(nazwa As String, rozmiar As Single) As Font Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).UtworzCzcionke
        Return New Font(nazwa, rozmiar)
    End Function

    Public Function ZmierzTekst(czcionka As Font, tekst As String, szer As Single, wys As Single) As SizeF Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).ZmierzTekst
        Return gr.MeasureString(tekst, czcionka, New SizeF(szer, wys))
    End Function

    Public Sub RysujTekst(pedzel As Brush, czcionka As Font, tekst As String, x As Single, y As Single, szer As Single, wys As Single) Implements IUrzadzenieRysujace(Of Pen, Brush, Matrix, Font).RysujTekst
        gr.DrawString(tekst, czcionka, pedzel, New RectangleF(x, y, szer, wys))
    End Sub
End Class