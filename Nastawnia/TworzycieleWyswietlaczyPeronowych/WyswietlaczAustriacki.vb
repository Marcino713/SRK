Friend Class WyswietlaczAustriacki
    Inherits WyswietlaczPeronowy

    Private Const MNOZNIK_WYSOKOSCI As Single = 1.27F
    Private Const WYS_KATEGORIA As Single = 16.0F
    Private Const WYS_STACJA_ODJAZD As Single = 22.0F
    Private Const WYS_STACJA_OD As Single = 14.0F
    Private Const WYS_KIERUNEK As Single = 13.0F
    Private Const WYS_CZAS As Single = 17.0F
    Private Const WYS_PRZEZ As Single = 11.0F
    Private Const WYS_KONIEC As Single = 17.0F
    Private Const POCZATEK_LEWO As Single = 2.0F
    Private Const PRZEZ_Y As Single = 63.0F
    Private Const STACJA_SZER As Single = 115.0F
    Private Const CZAS_POCZ As Single = 110.0F
    Private Const CZAS_SZER As Single = SZEROKOSC - CZAS_POCZ
    Private Const CZAS_PRZYJAZD_Y As Single = 30.0F
    Private Const STRZALKA_Y As Single = 18.0F
    Private Const STRZALKA_WYS_POL As Single = 5.0F
    Private Const STRZALKA_POCZ_X As Single = 118.0F
    Private Const STRZALKA_KONC_X As Single = 131.0F
    Private Const STRZALK_TROJKAT_X As Single = 126.0F
    Private Const KRESKA_X As Single = 115.0F
    Private Const KRESKA_WYS_POL As Single = 6.0F
    Private Const KONIEC_POCZ As Single = 33.0F
    Private Const TEKST_AB As String = "ab"
    Private Const TEKST_UBER As String = "über "
    Private Const TEKST_ZUG_ENDET As String = "Zug endet!"
    Private Const TEKST_VON As String = "von/from"
    Private Const FORMAT_CZASU As String = "h\:mm"
    Private Const NAZWA_CZCIONKI As String = "Arial"
    Private ReadOnly CZ_KATEGORIA As New Font(NAZWA_CZCIONKI, WYS_KATEGORIA, GraphicsUnit.Pixel)
    Private ReadOnly CZ_STACJA_ODJAZD As New Font(NAZWA_CZCIONKI, WYS_STACJA_ODJAZD, GraphicsUnit.Pixel)
    Private ReadOnly CZ_STACJA_OD As New Font(NAZWA_CZCIONKI, WYS_STACJA_OD, GraphicsUnit.Pixel)
    Private ReadOnly CZ_KIERUNEK As New Font(NAZWA_CZCIONKI, WYS_KIERUNEK, GraphicsUnit.Pixel)
    Private ReadOnly CZ_CZAS As New Font(NAZWA_CZCIONKI, WYS_CZAS, GraphicsUnit.Pixel)
    Private ReadOnly CZ_PRZEZ As New Font(NAZWA_CZCIONKI, WYS_PRZEZ, GraphicsUnit.Pixel)
    Private ReadOnly CZ_KONIEC As New Font(NAZWA_CZCIONKI, WYS_KONIEC, FontStyle.Bold, GraphicsUnit.Pixel)

    Private pedzelBialy As New SolidBrush(Color.White)
    Private pedzelNiebieski As New SolidBrush(Color.Blue)
    Private olowekBialy As New Pen(Color.White, 2.0F)

    Friend Overrides Function Rysuj(dane As DaneWyswietlaczaPeronowego) As Bitmap
        Dim bm As New Bitmap(SZEROKOSC, WYSOKOSC)
        Dim gr As Graphics = Graphics.FromImage(bm)
        gr.TextRenderingHint = Text.TextRenderingHint.SingleBitPerPixel
        gr.Clear(Color.Blue)

        Dim pociag As String = $"{dane.Kategoria}{If(String.IsNullOrEmpty(dane.Kategoria) Or String.IsNullOrEmpty(dane.Numer), "", " ")}{dane.Numer}"
        If dane.Typ = TypPostojuPociagu.Odjazd Then
            RysujOdjazd(dane, pociag, gr)
        Else
            RysujPrzyjazd(dane, pociag, gr)
        End If

        If dane.Opoznienie > 0 Then
            gr.FillRectangle(pedzelBialy, New RectangleF(0, WYSOKOSC * 0.75F, SZEROKOSC, WYSOKOSC * 0.25F))
            gr.DrawString($"Zug ca. {dane.Opoznienie} min. verspätet", CZ_PRZEZ, pedzelNiebieski, New RectangleF(POCZATEK_LEWO, PRZEZ_Y, SZEROKOSC, WYS_PRZEZ * MNOZNIK_WYSOKOSCI))
        End If

        Return bm
    End Function

    Private Sub RysujOdjazd(dane As DaneWyswietlaczaPeronowego, pociag As String, gr As Graphics)
        gr.DrawString(pociag, CZ_KATEGORIA, pedzelBialy, New RectangleF(POCZATEK_LEWO, 4.0F, SZEROKOSC * 0.75F, WYS_KATEGORIA * MNOZNIK_WYSOKOSCI))
        gr.DrawString(dane.Stacja, CZ_STACJA_ODJAZD, pedzelBialy, New RectangleF(0.0F, 26.0F, STACJA_SZER, WYS_STACJA_ODJAZD * MNOZNIK_WYSOKOSCI))
        gr.DrawString(TEKST_AB, CZ_KIERUNEK, pedzelBialy, New RectangleF(140.0F, 11.0F, 20.0F, WYS_KIERUNEK * MNOZNIK_WYSOKOSCI))
        gr.DrawString(dane.Czas.ToString(FORMAT_CZASU), CZ_CZAS, pedzelBialy, New RectangleF(CZAS_POCZ, 31.0F, CZAS_SZER, WYS_CZAS * MNOZNIK_WYSOKOSCI))

        'Strzałka
        gr.DrawLine(olowekBialy, KRESKA_X, STRZALKA_Y - KRESKA_WYS_POL, KRESKA_X, STRZALKA_Y + KRESKA_WYS_POL)
        gr.DrawLine(olowekBialy, STRZALKA_POCZ_X, STRZALKA_Y, STRZALKA_KONC_X + 1.0F, STRZALKA_Y)
        gr.DrawLine(olowekBialy, STRZALKA_KONC_X, STRZALKA_Y, STRZALK_TROJKAT_X, STRZALKA_Y - STRZALKA_WYS_POL)
        gr.DrawLine(olowekBialy, STRZALKA_KONC_X, STRZALKA_Y, STRZALK_TROJKAT_X, STRZALKA_Y + STRZALKA_WYS_POL)

        If dane.Opoznienie = 0 Then
            Dim przez As New List(Of String)

            If dane.Przez IsNot Nothing AndAlso dane.Przez.Count > 0 Then
                For i As Integer = 0 To dane.Przez.Length - 1
                    Dim s As String = dane.Przez(i).Trim()
                    If Not String.IsNullOrEmpty(s) Then przez.Add(s)
                Next
            End If

            If przez.Count > 0 Then
                gr.DrawString(TEKST_UBER & String.Join("~", przez), CZ_PRZEZ, pedzelBialy, New RectangleF(POCZATEK_LEWO, PRZEZ_Y, SZEROKOSC, WYS_PRZEZ * MNOZNIK_WYSOKOSCI))
            End If
        End If
    End Sub

    Private Sub RysujPrzyjazd(dane As DaneWyswietlaczaPeronowego, pociag As String, gr As Graphics)
        gr.DrawString(TEKST_ZUG_ENDET, CZ_KONIEC, pedzelBialy, New RectangleF(KONIEC_POCZ, 0.0F, SZEROKOSC, WYS_KONIEC * MNOZNIK_WYSOKOSCI))

        Dim pociagOd As String = pociag
        If Not String.IsNullOrEmpty(pociag) Then pociagOd &= " "
        pociagOd &= TEKST_VON

        gr.DrawString(pociagOd, CZ_STACJA_OD, pedzelBialy, New RectangleF(0.0F, 26.0F, STACJA_SZER, WYS_STACJA_OD * MNOZNIK_WYSOKOSCI))
        gr.DrawString(dane.Stacja, CZ_STACJA_OD, pedzelBialy, New RectangleF(0.0F, 44.0F, STACJA_SZER, WYS_STACJA_OD * MNOZNIK_WYSOKOSCI))
        gr.DrawString(dane.Czas.ToString(FORMAT_CZASU), CZ_CZAS, pedzelBialy, New RectangleF(CZAS_POCZ, CZAS_PRZYJAZD_Y, CZAS_SZER, WYS_CZAS * MNOZNIK_WYSOKOSCI))
    End Sub
End Class