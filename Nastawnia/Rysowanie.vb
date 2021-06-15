Public Module Rysowanie
    Public Const LAMPA_SZER As Single = 0.25     'średnica lampy
    Private Const TOR_SZEROKOSC As Single = 0.1 'szerokość toru na kostce
    Private Const SYGN_POZ As Single = 0.25     'wielokorotność stałej oznacza położenie środków kolejnych świateł sygnałów na osi X
    Private Const SYGN_SZER As Single = 0.18    'średnica sygnału
    Private Const SYGN_TLO_SZER As Single = 0.28        'średnica okręgu stanowiącego tło sygnału
    Private Const SYGN_SLUP_SZER_DUZA As Single = 0.15  'szerokość słupa sygnalizatora w szerszym miejscu
    Private Const SYGN_SLUP_SZER_MALA As Single = 0.05  'szerokosć słupa sygnalizatora w węższym miejscu
    Private Const SYGN_SLUP_DLUG As Single = 0.04       'długość poszczególnych segmentów słupa
    Private Const KIER_SZER As Single = 0.4
    Private Const TEKST_POZ_X As Single = 0.4
    Private Const TEKST_POZ_Y As Single = 0.1
    Private Const COS45 As Single = 0.707

    Private ReadOnly PEDZEL_KRAWEDZIE As New Pen(Color.White, 0)
    Private ReadOnly PEDZEL_TOR_WOLNY As New SolidBrush(KolorRGB("#8C8C8C"))
    Private ReadOnly PEDZEL_SYGN_CZER As New SolidBrush(KolorRGB("#800000"))
    Private ReadOnly PEDZEL_SYGN_CZER_JASNY As New SolidBrush(KolorRGB("#FF0000"))
    Private ReadOnly PEDZEL_SYGN_ZIEL As New SolidBrush(KolorRGB("#008000"))
    Private ReadOnly PEDZEL_SYGN_ZIEL_JASNY As New SolidBrush(KolorRGB("#00FF00"))
    Private ReadOnly PEDZEL_SYGN_NIEB As New SolidBrush(KolorRGB("#000080"))
    Private ReadOnly PEDZEL_SYGN_NIEB_JASNY As New SolidBrush(KolorRGB("#0000FF"))
    Private ReadOnly PEDZEL_SYGN_BIAL As New SolidBrush(KolorRGB("#CCCCCC"))
    Private ReadOnly PEDZEL_SYGN_BIAL_JASNY As New SolidBrush(KolorRGB("#FFFFFF"))
    Private ReadOnly PEDZEL_SYGN_TLO As New SolidBrush(KolorRGB("#808080"))
    Private ReadOnly PEDZEL_SYGN_KRAWEDZ As New Pen(KolorRGB("#000000"), 0)
    Private ReadOnly PEDZEL_PRZYCISK As New SolidBrush(KolorRGB("#000000"))
    Private ReadOnly PEDZEL_TEKST As New SolidBrush(KolorRGB("#000000"))
    Private ReadOnly PEDZEL_ZAZN_KOSTKA As New SolidBrush(KolorRGB("#009DFF"))
    Private ReadOnly PEDZEL_LAMPA_TLO As New SolidBrush(KolorRGB("#FFEA00"))
    Private ReadOnly PEDZEL_LAMPA_ZAZN As New SolidBrush(KolorRGB("#FF9500"))

    Private ReadOnly CZCIONKA As New Font("Arial", 0.2)

    Private gr As Graphics

    Public Function Rysuj(pulpit As Zaleznosci.Pulpit, konfiguracja As KonfiguracjaRysowania) As Bitmap
        Dim poczx As Integer = 0
        Dim poczy As Integer = 0

        Dim img As New Bitmap(CInt(pulpit.Szerokosc * konfiguracja.Skalowanie) + 1, CInt(pulpit.Wysokosc * konfiguracja.Skalowanie) + 1)
        gr = Graphics.FromImage(img)
        gr.Clear(konfiguracja.KolorKostki)

        If konfiguracja.ZaznaczX >= 0 And konfiguracja.ZaznaczX < pulpit.Szerokosc And konfiguracja.ZaznaczY >= 0 And konfiguracja.ZaznaczY < pulpit.Wysokosc Then
            gr.ScaleTransform(konfiguracja.Skalowanie, konfiguracja.Skalowanie)
            gr.FillRectangle(PEDZEL_ZAZN_KOSTKA, konfiguracja.ZaznaczX, konfiguracja.ZaznaczY, 1, 1)
            If konfiguracja.PrzesuwanaKostka IsNot Nothing Then RysujKostke(konfiguracja.ZaznaczX, konfiguracja.ZaznaczY, konfiguracja.Skalowanie, konfiguracja.PrzesuwanaKostka)
            gr.ResetTransform()
        End If

        If konfiguracja.RysujKrawedzieKostek Then
            gr.ScaleTransform(konfiguracja.Skalowanie, konfiguracja.Skalowanie)

            For x As Integer = 0 To pulpit.Szerokosc
                gr.DrawLine(PEDZEL_KRAWEDZIE, x, 0, x, pulpit.Wysokosc)
            Next
            For y As Integer = 0 To pulpit.Wysokosc
                gr.DrawLine(PEDZEL_KRAWEDZIE, 0, y, pulpit.Szerokosc, y)
            Next
        End If

        For x As Integer = 0 To pulpit.Szerokosc - 1
            For y As Integer = 0 To pulpit.Wysokosc - 1
                Dim k As Zaleznosci.Kostka = pulpit.Kostki(x, y)
                If k Is Nothing Then Continue For

                RysujKostke(x, y, konfiguracja.Skalowanie, k)
            Next
        Next

        If konfiguracja.RysujLampy Then
            Dim en As List(Of Zaleznosci.Lampa).Enumerator = pulpit.Lampy.GetEnumerator
            While en.MoveNext
                Dim l As Zaleznosci.Lampa = en.Current
                gr.ResetTransform()
                gr.ScaleTransform(konfiguracja.Skalowanie, konfiguracja.Skalowanie)
                gr.TranslateTransform(CSng(l.X - LAMPA_SZER / 2), CSng(l.Y - LAMPA_SZER / 2))

                Dim pedzel As SolidBrush
                If l Is konfiguracja.ZaznaczonaLampa Then
                    pedzel = PEDZEL_LAMPA_ZAZN
                Else
                    pedzel = PEDZEL_LAMPA_TLO
                End If
                gr.FillEllipse(pedzel, 0, 0, LAMPA_SZER, LAMPA_SZER)
            End While
        End If

        Return img
    End Function

    Private Sub RysujKostke(x As Integer, y As Integer, skalowanie As Single, kostka As Zaleznosci.Kostka)
        gr.ResetTransform()
        gr.ScaleTransform(skalowanie, skalowanie)
        gr.TranslateTransform(x + 0.5F, y + 0.5F)
        gr.RotateTransform(kostka.Obrot)
        gr.TranslateTransform(-0.5F, -0.5F)

        Select Case kostka.Typ
            Case Zaleznosci.TypKostki.Tor
                RysujTor()
            Case Zaleznosci.TypKostki.TorKoniec
                RysujTorKoniec()
            Case Zaleznosci.TypKostki.Zakret
                RysujZakret()
            Case Zaleznosci.TypKostki.RozjazdLewo
                RysujRozjazdLewo(CType(kostka, Zaleznosci.RozjazdLewo))
            Case Zaleznosci.TypKostki.RozjazdPrawo
                RysujRozjazdPrawo(CType(kostka, Zaleznosci.RozjazdPrawo))
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                RysujSygnalizatorManewrowy()
            Case Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                RysujSygnalizatorPolsamoczynny()
            Case Zaleznosci.TypKostki.SygnalizatorSamoczynny
                RysujSygnalizatorSamoczynny()
            Case Zaleznosci.TypKostki.Przycisk
                RysujPrzycisk()
            Case Zaleznosci.TypKostki.PrzyciskTor
                RysujPrzyciskTor()
            Case Zaleznosci.TypKostki.Kierunek
                RysujKierunek()
        End Select
    End Sub

    Private Sub RysujTor()
        gr.FillRectangle(PEDZEL_TOR_WOLNY, 0, 0.5 - TOR_SZEROKOSC / 2, 1, TOR_SZEROKOSC)
    End Sub

    Private Sub RysujTorKoniec()
        gr.FillRectangle(PEDZEL_TOR_WOLNY, 0, 0.5 - TOR_SZEROKOSC / 2, 0.5, TOR_SZEROKOSC)
    End Sub

    Private Sub RysujZakret()
        Dim szer As Single = TOR_SZEROKOSC / COS45
        gr.FillPolygon(PEDZEL_TOR_WOLNY, {
        New PointF(1, 0.5F - szer / 2),
        New PointF(1, 0.5F + szer / 2),
        New PointF(0.5F + szer / 2, 1),
        New PointF(0.5F - szer / 2, 1)
        })
    End Sub

    Private Sub RysujZakretPrawo()
        Dim szer As Single = TOR_SZEROKOSC / COS45
        gr.FillPolygon(PEDZEL_TOR_WOLNY, {
        New PointF(1, 0.5F - szer / 2),
        New PointF(0.5F + szer / 2, 0),
        New PointF(0.5F - szer / 2, 0),
        New PointF(1, 0.5F + szer / 2)
        })
    End Sub

    Private Sub RysujRozjazdLewo(rozjazd As Zaleznosci.RozjazdLewo)
        RysujTor()
        RysujZakret()
        RysujPrzycisk()
        gr.DrawString(rozjazd.Nazwa, CZCIONKA, PEDZEL_TEKST, TEKST_POZ_X, TEKST_POZ_Y)
    End Sub

    Private Sub RysujRozjazdPrawo(rozjazd As Zaleznosci.RozjazdPrawo)
        RysujTor()
        RysujZakretPrawo()
        RysujPrzycisk(2)
        gr.DrawString(rozjazd.Nazwa, CZCIONKA, PEDZEL_TEKST, TEKST_POZ_X, TEKST_POZ_Y + 2 * SYGN_POZ)
    End Sub

    Private Sub RysujSygnalizatorManewrowy()
        RysujTor()
        gr.FillPie(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 90, 180)
        gr.FillPie(PEDZEL_SYGN_TLO, 2 * SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 270, 180)
        gr.FillRectangle(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_SYGN_NIEB_JASNY, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(PEDZEL_SYGN_BIAL_JASNY, 2 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(2)
    End Sub

    Private Sub RysujSygnalizatorPolsamoczynny()
        RysujTor()
        gr.FillPie(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 90, 180)
        gr.FillPie(PEDZEL_SYGN_TLO, 3 * SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 270, 180)
        gr.FillRectangle(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ - SYGN_TLO_SZER / 2, 2 * SYGN_POZ, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_SYGN_CZER_JASNY, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(PEDZEL_SYGN_ZIEL_JASNY, 2 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(PEDZEL_SYGN_BIAL_JASNY, 3 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(3)
    End Sub

    Private Sub RysujSygnalizatorSamoczynny()
        RysujTor()
        gr.FillEllipse(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_SYGN_CZER_JASNY, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(1)
    End Sub

    Private Sub RysujPrzycisk(Optional poczy As Single = 0)
        gr.FillEllipse(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, poczy * SYGN_POZ + SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_PRZYCISK, SYGN_POZ - SYGN_SZER / 2, poczy * SYGN_POZ + SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
    End Sub

    Private Sub RysujPrzyciskTor()
        RysujTor()
        RysujPrzycisk()
    End Sub

    Private Sub RysujKierunek()
        gr.FillPolygon(PEDZEL_TOR_WOLNY, New PointF() {
        New PointF(0.5 + KIER_SZER / 2, 0.5 - KIER_SZER / 2),
        New PointF(0.5 - KIER_SZER / 2, 0.5),
        New PointF(0.5 + KIER_SZER / 2, 0.5 + KIER_SZER / 2)
        })
        RysujPrzycisk()
    End Sub

    Private Sub RysujSlupSygnalizatora(poczx As Single)
        Dim x1 As Single = poczx * SYGN_POZ + SYGN_TLO_SZER / 2
        Dim x2 As Single = poczx * SYGN_POZ + SYGN_TLO_SZER / 2 + SYGN_SLUP_DLUG
        Dim x3 As Single = poczx * SYGN_POZ + SYGN_TLO_SZER / 2 + 2 * SYGN_SLUP_DLUG

        Dim y1 As Single = SYGN_POZ - SYGN_SLUP_SZER_DUZA / 2
        Dim y2 As Single = SYGN_POZ - SYGN_SLUP_SZER_MALA / 2
        Dim y3 As Single = SYGN_POZ + SYGN_SLUP_SZER_MALA / 2
        Dim y4 As Single = SYGN_POZ + SYGN_SLUP_SZER_DUZA / 2

        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x1, y2, x2, y2)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x2, y2, x2, y1)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x2, y1, x3, y1)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x3, y1, x3, y4)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x3, y4, x2, y4)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x2, y4, x2, y3)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x2, y3, x1, y3)
        gr.DrawLine(PEDZEL_SYGN_KRAWEDZ, x1, y3, x1, y2)
    End Sub

End Module