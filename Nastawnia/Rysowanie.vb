Public Module Rysowanie
    Public Const KOLKO_SZER As Single = 0.25            'średnica kółka (lampy/licznika osi)
    Private Const KOLKO_TEKST_SZER As Single = 0.12     'średnica kółka obok tekstu
    Private Const KOLKO_TEKST_POZ As Single = 0.1       'położenie kółka obok tekstu
    Private Const TOR_SZEROKOSC As Single = 0.1         'szerokość toru na kostce
    Private Const SYGN_POZ As Single = 0.25             'wielokorotność stałej oznacza położenie środków kolejnych świateł sygnałów na osi X
    Private Const SYGN_SZER As Single = 0.18            'średnica sygnału
    Private Const SYGN_TLO_SZER As Single = 0.28        'średnica okręgu stanowiącego tło sygnału
    Private Const SYGN_SLUP_SZER_DUZA As Single = 0.15  'szerokość słupa sygnalizatora w szerszym miejscu
    Private Const SYGN_SLUP_SZER_MALA As Single = 0.05  'szerokosć słupa sygnalizatora w węższym miejscu
    Private Const SYGN_SLUP_DLUG As Single = 0.04       'długość poszczególnych segmentów słupa
    Private Const KIER_SZER As Single = 0.4
    Private Const TEKST_POZ_X_PRZYCISK As Single = 0.17 'dodatkowy margines dla tekstu obok przycisku
    Private Const TEKST_POZ_X As Single = 0.1           'dodatkowy margines dla tekstu
    Private Const TEKST_POZ_Y As Single = 0.12          'dodatkowy margines dla tekstu
    Private Const TEKST_NAPIS_POZ As Single = 0.12      'pozycja tekstu w kostce z napisem
    Private Const TEKST_WYS As Single = 0.8             'wysokość tekstu w kostce z napisem
    Private Const COS45 As Single = 0.707
    Private Const KAT_PROSTY As Single = 90.0

    Public ReadOnly KOLOR_TOR_PRZYPISANY As Color = KolorRGB("#8C8C8C")          'tor przypisany do innego odcinka
    Public ReadOnly KOLOR_TOR_TEN_ODCINEK As Color = KolorRGB("#25FF1A")         'tor przypisany do zaznaczonego odcinka
    Public ReadOnly KOLOR_TOR_NIEPRZYPISANY As Color = KolorRGB("#FF1A1A")       'tor nieprzypisany do żadnego odcinka
    Public ReadOnly KOLOR_TOR_LICZNIK_ODCINEK_2 As Color = KolorRGB("#D11AFF")   'drugi odcinek obsługiwany przez parę liczników osi 1A29FF

    Private ReadOnly PEDZEL_KRAWEDZIE As New Pen(Color.White, 0)
    Private ReadOnly PEDZEL_TOR_WOLNY As New SolidBrush(KOLOR_TOR_PRZYPISANY)
    Private ReadOnly PEDZEL_TOR_TEN_ODCINEK As New SolidBrush(KOLOR_TOR_TEN_ODCINEK)
    Private ReadOnly PEDZEL_TOR_NIEPRZYPISANY As New SolidBrush(KOLOR_TOR_NIEPRZYPISANY)
    Private ReadOnly PEDZEL_TOR_LICZNIK_ODCINEK_2 As New SolidBrush(KOLOR_TOR_LICZNIK_ODCINEK_2)
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
    Private ReadOnly PEDZEL_KOLKO_TEKST As New SolidBrush(KolorRGB("#FF1A71"))

    Private ReadOnly CZCIONKA As New Font("Arial", 0.17)

    Private Const NAZWA_SZ As String = "Sz"     'Sygnał zastępczy
    Private Const NAZWA_ZW As String = "Zw"     'Zwolnienie przebiegów
    Private Const NAZWA_M As String = "m"       'Sygnał manewrowy

    Private gr As Graphics
    Private pedzelToru As SolidBrush
    Private obrot As Single

    Public Function Rysuj(pulpit As Zaleznosci.Pulpit, konfiguracja As KonfiguracjaRysowania) As Bitmap
        Dim poczx As Integer = 0
        Dim poczy As Integer = 0

        Dim img As New Bitmap(CInt(pulpit.Szerokosc * konfiguracja.Skalowanie) + 1, CInt(pulpit.Wysokosc * konfiguracja.Skalowanie) + 1)
        gr = Graphics.FromImage(img)
        gr.Clear(konfiguracja.KolorKostki)
        pedzelToru = PEDZEL_TOR_WOLNY

        'Rysuj zaznaczenie kostki i kostkę przeciąganą
        If (Not konfiguracja.RysujOdcinki) And (Not konfiguracja.RysujLiczniki) And (Not konfiguracja.RysujLampy) And konfiguracja.ZaznaczX >= 0 And konfiguracja.ZaznaczX < pulpit.Szerokosc And konfiguracja.ZaznaczY >= 0 And konfiguracja.ZaznaczY < pulpit.Wysokosc Then
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

        'Rysuj kostki
        For x As Integer = 0 To pulpit.Szerokosc - 1
            For y As Integer = 0 To pulpit.Wysokosc - 1
                Dim k As Zaleznosci.Kostka = pulpit.Kostki(x, y)
                If k Is Nothing Then Continue For

                If konfiguracja.RysujOdcinki Then UstawKolorToru(k, konfiguracja.ZaznaczonyOdcinek)
                If konfiguracja.RysujLiczniki Then UstawKolorToruDlaLicznika(k, konfiguracja.ZaznaczonyLicznik)
                RysujKostke(x, y, konfiguracja.Skalowanie, k)
            Next
        Next

        If konfiguracja.RysujLampy Then
            Dim en As List(Of Zaleznosci.Lampa).Enumerator = pulpit.Lampy.GetEnumerator
            While en.MoveNext
                Dim l As Zaleznosci.Lampa = en.Current
                RysujKolko(If(l Is konfiguracja.ZaznaczonaLampa, PEDZEL_LAMPA_ZAZN, PEDZEL_LAMPA_TLO), konfiguracja.Skalowanie, l.X, l.Y)
            End While
        End If

        If konfiguracja.RysujLiczniki Then
            Dim l As Zaleznosci.ParaLicznikowOsi = konfiguracja.ZaznaczonyLicznik
            If l IsNot Nothing Then
                RysujKolko(PEDZEL_TOR_TEN_ODCINEK, konfiguracja.Skalowanie, l.X1, l.Y1)
                RysujKolko(PEDZEL_TOR_LICZNIK_ODCINEK_2, konfiguracja.Skalowanie, l.X2, l.Y2)
            End If
        End If

        Return img
    End Function

    Private Sub RysujKostke(x As Integer, y As Integer, skalowanie As Single, kostka As Zaleznosci.Kostka)
        gr.ResetTransform()
        gr.ScaleTransform(skalowanie, skalowanie)
        gr.TranslateTransform(x + 0.5F, y + 0.5F)
        gr.RotateTransform(kostka.Obrot)
        gr.TranslateTransform(-0.5F, -0.5F)
        obrot = kostka.Obrot

        Select Case kostka.Typ
            Case Zaleznosci.TypKostki.Tor
                RysujTor()
            Case Zaleznosci.TypKostki.TorKoniec
                RysujTor(0.5)
            Case Zaleznosci.TypKostki.Zakret
                RysujZakret()
            Case Zaleznosci.TypKostki.RozjazdLewo
                RysujRozjazdLewo(CType(kostka, Zaleznosci.RozjazdLewo))
            Case Zaleznosci.TypKostki.RozjazdPrawo
                RysujRozjazdPrawo(CType(kostka, Zaleznosci.RozjazdPrawo))
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                RysujSygnalizatorManewrowy(CType(kostka, Zaleznosci.SygnalizatorManewrowy))
            Case Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                RysujSygnalizatorPolsamoczynny(CType(kostka, Zaleznosci.SygnalizatorPolsamoczynny))
            Case Zaleznosci.TypKostki.SygnalizatorSamoczynny
                RysujSygnalizatorSamoczynny(CType(kostka, Zaleznosci.SygnalizatorSamoczynny))
            Case Zaleznosci.TypKostki.Przycisk
                RysujPrzyciskZwykly(CType(kostka, Zaleznosci.Przycisk))
            Case Zaleznosci.TypKostki.PrzyciskTor
                RysujPrzyciskTor(CType(kostka, Zaleznosci.PrzyciskTor))
            Case Zaleznosci.TypKostki.Kierunek
                RysujKierunek()
            Case Zaleznosci.TypKostki.Napis
                RysujKostkeNapis(CType(kostka, Zaleznosci.Napis))
        End Select
    End Sub

    Private Sub RysujTor(Optional Dlugosc As Single = 1)
        gr.FillRectangle(pedzelToru, 0, 0.5 - TOR_SZEROKOSC / 2, Dlugosc, TOR_SZEROKOSC)
    End Sub

    Private Sub RysujZakret()
        Dim szer As Single = TOR_SZEROKOSC / COS45
        gr.FillPolygon(pedzelToru, {
        New PointF(1, 0.5F - szer / 2),
        New PointF(1, 0.5F + szer / 2),
        New PointF(0.5F + szer / 2, 1),
        New PointF(0.5F - szer / 2, 1)
        })
    End Sub

    Private Sub RysujZakretPrawo()
        Dim szer As Single = TOR_SZEROKOSC / COS45
        gr.FillPolygon(pedzelToru, {
        New PointF(1, 0.5F - szer / 2),
        New PointF(0.5F + szer / 2, 0),
        New PointF(0.5F - szer / 2, 0),
        New PointF(1, 0.5F + szer / 2)
        })
    End Sub

    Private Sub RysujNazwe(nazwa As String, x As Single, y As Single, Optional wys As Single = SYGN_POZ)
        Dim rect As New RectangleF(x, y, 1 - x, wys)
        Dim transformacja As Drawing2D.Matrix = gr.Transform

        If obrot >= 2 * KAT_PROSTY And obrot < 4 * KAT_PROSTY Then
            Dim rozm As SizeF = gr.MeasureString(nazwa, CZCIONKA, New SizeF(rect.Width, rect.Height))
            Dim x1 As Single = rect.X + rozm.Width / 2
            Dim y1 As Single = rect.Y + rozm.Height / 2
            gr.TranslateTransform(x1, y1)
            gr.RotateTransform(2 * KAT_PROSTY)
            gr.TranslateTransform(-x1, -y1)
        End If

        gr.DrawString(nazwa, CZCIONKA, PEDZEL_TEKST, rect)
        gr.Transform = transformacja
    End Sub

    Private Sub RysujRozjazdLewo(rozjazd As Zaleznosci.RozjazdLewo)
        RysujTor()
        RysujZakret()
        RysujPrzycisk()
        RysujNazwe(rozjazd.Nazwa, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
    End Sub

    Private Sub RysujRozjazdPrawo(rozjazd As Zaleznosci.RozjazdPrawo)
        RysujTor()
        RysujZakretPrawo()
        RysujPrzycisk(2)
        RysujNazwe(rozjazd.Nazwa, SYGN_POZ + TEKST_POZ_X_PRZYCISK, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujNazweSygnalizatora(nazwa As String)
        RysujNazwe(nazwa, TEKST_POZ_X, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujSygnalizatorManewrowy(sygnalizator As Zaleznosci.SygnalizatorManewrowy)
        RysujTor()
        gr.FillPie(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillPie(PEDZEL_SYGN_TLO, 2 * SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 3 * KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillRectangle(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_SYGN_NIEB_JASNY, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(PEDZEL_SYGN_BIAL_JASNY, 2 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(2)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorPolsamoczynny(sygnalizator As Zaleznosci.SygnalizatorPolsamoczynny)
        RysujTor()
        gr.FillPie(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillPie(PEDZEL_SYGN_TLO, 3 * SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 3 * KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillRectangle(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ - SYGN_TLO_SZER / 2, 2 * SYGN_POZ, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_SYGN_CZER_JASNY, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(PEDZEL_SYGN_ZIEL_JASNY, 2 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(PEDZEL_SYGN_BIAL_JASNY, 3 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(3)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorSamoczynny(sygnalizator As Zaleznosci.SygnalizatorSamoczynny)
        RysujTor()
        gr.FillEllipse(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_SYGN_CZER_JASNY, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(1)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujPrzycisk(Optional poczy As Single = 0)
        gr.FillEllipse(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, poczy * SYGN_POZ + SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER)
        gr.FillEllipse(PEDZEL_PRZYCISK, SYGN_POZ - SYGN_SZER / 2, poczy * SYGN_POZ + SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
    End Sub

    Private Sub RysujPrzyciskZwykly(przycisk As Zaleznosci.Przycisk)
        RysujPrzycisk()
        Select Case przycisk.TypPrzycisku
            Case Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy
                RysujNazwe(NAZWA_SZ, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
                RysujNazweSygnalizatora(przycisk.ObslugiwanySygnalizator?.Nazwa)
            Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow
                RysujNazwe(NAZWA_ZW, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
        End Select
    End Sub

    Private Sub RysujPrzyciskTor(przycisk As Zaleznosci.PrzyciskTor)
        RysujTor()
        RysujPrzycisk()
        If przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy Or przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy Then
            RysujNazwe(NAZWA_M, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
        End If
        RysujNazweSygnalizatora(przycisk.ObslugiwanySygnalizator?.Nazwa)
    End Sub

    Private Sub RysujKierunek()
        gr.FillPolygon(pedzelToru, New PointF() {
        New PointF(0.5 + KIER_SZER / 2, 0.5 - KIER_SZER / 2),
        New PointF(0.5 - KIER_SZER / 2, 0.5),
        New PointF(0.5 + KIER_SZER / 2, 0.5 + KIER_SZER / 2)
        })
        RysujPrzycisk()
    End Sub

    Private Sub RysujKostkeNapis(napis As Zaleznosci.Napis)
        Dim poz As Single = KOLKO_TEKST_POZ - KOLKO_TEKST_SZER / 2.0F
        gr.FillEllipse(PEDZEL_KOLKO_TEKST, poz, poz, KOLKO_TEKST_SZER, KOLKO_TEKST_SZER)
        RysujNazwe(napis.Tekst, TEKST_NAPIS_POZ, TEKST_NAPIS_POZ, TEKST_WYS)
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

    Private Sub RysujKolko(pedzel As Brush, skalowanie As Single, x As Single, y As Single)
        gr.ResetTransform()
        gr.ScaleTransform(skalowanie, skalowanie)
        gr.TranslateTransform(x - KOLKO_SZER / 2.0F, y - KOLKO_SZER / 2.0F)
        gr.FillEllipse(pedzel, 0, 0, KOLKO_SZER, KOLKO_SZER)
    End Sub

    Private Sub UstawKolorToru(k As Zaleznosci.Kostka, zazn As Zaleznosci.OdcinekToru)
        If TypeOf k Is Zaleznosci.ITor Then
            Dim t As Zaleznosci.ITor = DirectCast(k, Zaleznosci.ITor)
            If t.NalezyDoOdcinka Is zazn And zazn IsNot Nothing Then
                pedzelToru = PEDZEL_TOR_TEN_ODCINEK
            ElseIf t.NalezyDoOdcinka IsNot Nothing
                pedzelToru = PEDZEL_TOR_WOLNY
            Else
                pedzelToru = PEDZEL_TOR_NIEPRZYPISANY
            End If
        Else
            pedzelToru = PEDZEL_TOR_WOLNY
        End If
    End Sub

    Private Sub UstawKolorToruDlaLicznika(k As Zaleznosci.Kostka, zazn As Zaleznosci.ParaLicznikowOsi)
        pedzelToru = PEDZEL_TOR_WOLNY
        If zazn Is Nothing Then Exit Sub

        If TypeOf k Is Zaleznosci.ITor Then
            Dim t As Zaleznosci.ITor = DirectCast(k, Zaleznosci.ITor)
            If t.NalezyDoOdcinka IsNot Nothing Then
                If t.NalezyDoOdcinka Is zazn.Odcinek1 Then
                    pedzelToru = PEDZEL_TOR_TEN_ODCINEK
                ElseIf t.NalezyDoOdcinka Is zazn.Odcinek2
                    pedzelToru = PEDZEL_TOR_LICZNIK_ODCINEK_2
                End If
            End If
        End If
    End Sub
End Module