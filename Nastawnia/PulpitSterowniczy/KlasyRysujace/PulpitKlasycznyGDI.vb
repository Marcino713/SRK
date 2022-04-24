Friend Class PulpitKlasycznyGDI
    Implements IRysownik

    Private Const KOLKO_SZER As Single = 0.25            'średnica kółka (lampy/licznika osi)
    Private Const KOLKO_TEKST_SZER As Single = 0.12     'średnica kółka obok tekstu
    Private Const KOLKO_TEKST_POZ As Single = 0.1       'położenie kółka obok tekstu
    Private Const TOR_SZEROKOSC As Single = 0.1         'szerokość toru na kostce
    Private Const TOR_KONC_DLUGOSC As Single = 0.35     'długość toru na kostce z końcem
    Private Const TOR_KONC_KONCOWKI As Single = 0.15    'odelgłość środków końcówek toru od środka toru
    Private Const TOR_KONC_KONCOWKI_DL As Single = 0.1  'długość końcówek toru
    Private Const SYGN_POZ As Single = 0.25             'wielokorotność stałej oznacza położenie środków kolejnych świateł sygnałów na osi X
    Private Const SYGN_SZER As Single = 0.18            'średnica sygnału
    Private Const SYGN_TLO_SZER As Single = 0.28        'średnica okręgu stanowiącego tło sygnału
    Private Const SYGN_SLUP_SZER_DUZA As Single = 0.15  'szerokość słupa sygnalizatora w szerszym miejscu
    Private Const SYGN_SLUP_SZER_MALA As Single = 0.05  'szerokosć słupa sygnalizatora w węższym miejscu
    Private Const SYGN_SLUP_DLUG As Single = 0.04       'długość poszczególnych segmentów słupa
    Private Const KIER_SZER As Single = 0.4             'rozmiar strzałki na przycisku kierunku
    Private Const TEKST_POZ_X_PRZYCISK As Single = 0.17 'dodatkowy margines dla tekstu obok przycisku
    Private Const TEKST_POZ_X As Single = 0.1           'dodatkowy margines dla tekstu
    Private Const TEKST_POZ_Y As Single = 0.12          'dodatkowy margines dla tekstu
    Private Const TEKST_NAPIS_POZ As Single = 0.12      'pozycja tekstu w kostce z napisem
    Private Const TEKST_WYS As Single = 0.8             'wysokość tekstu w kostce z napisem
    Private Const COS45 As Single = 0.707
    Private Const KAT_PROSTY As Single = 90.0
    Private Const POL As Single = 0.5F
    Private Const CWIERC As Single = 0.25F

    Private ReadOnly KOLOR_TOR_PRZYPISANY As Color = KolorRGB("#8C8C8C")          'tor przypisany do innego odcinka
    Private ReadOnly KOLOR_TOR_TEN_ODCINEK As Color = KolorRGB("#25FF1A")         'tor przypisany do zaznaczonego odcinka
    Private ReadOnly KOLOR_TOR_NIEPRZYPISANY As Color = KolorRGB("#FF1A1A")       'tor nieprzypisany do żadnego odcinka
    Private ReadOnly KOLOR_TOR_LICZNIK_ODCINEK_2 As Color = KolorRGB("#D11AFF")   'drugi odcinek obsługiwany przez parę liczników osi 1A29FF

    Private ReadOnly PEDZEL_TLO_KOSTKI As New SolidBrush(KolorRGB("#99FFCC"))
    Private ReadOnly PEDZEL_KRAWEDZIE As New Pen(Color.White, 0.02)
    Private ReadOnly PEDZEL_TOR_WOLNY As New SolidBrush(KOLOR_TOR_PRZYPISANY)
    Private ReadOnly PEDZEL_TOR_TEN_ODCINEK As New SolidBrush(KOLOR_TOR_TEN_ODCINEK)
    Private ReadOnly PEDZEL_TOR_NIEPRZYPISANY As New SolidBrush(KOLOR_TOR_NIEPRZYPISANY)
    Private ReadOnly PEDZEL_TOR_LICZNIK_ODCINEK_2 As New SolidBrush(KOLOR_TOR_LICZNIK_ODCINEK_2)
    Private ReadOnly PEDZEL_SYGN_CZER As New SolidBrush(KolorRGB("#520000"))
    Private ReadOnly PEDZEL_SYGN_CZER_JASNY As New SolidBrush(KolorRGB("#FF3838"))
    Private ReadOnly PEDZEL_SYGN_ZIEL As New SolidBrush(KolorRGB("#004700"))
    Private ReadOnly PEDZEL_SYGN_ZIEL_JASNY As New SolidBrush(KolorRGB("#33FF33"))
    Private ReadOnly PEDZEL_SYGN_NIEB As New SolidBrush(KolorRGB("#000661"))
    Private ReadOnly PEDZEL_SYGN_NIEB_JASNY As New SolidBrush(KolorRGB("#14EBFF"))
    Private ReadOnly PEDZEL_SYGN_BIAL As New SolidBrush(KolorRGB("#909090"))
    Private ReadOnly PEDZEL_SYGN_BIAL_JASNY As New SolidBrush(KolorRGB("#FFFFFF"))
    Private ReadOnly PEDZEL_SYGN_TLO As New SolidBrush(KolorRGB("#808080"))
    Private ReadOnly PEDZEL_SYGN_KRAWEDZ As New Pen(KolorRGB("#000000"), 0.01)
    Private ReadOnly PEDZEL_PRZYCISK As New SolidBrush(KolorRGB("#000000"))
    Private ReadOnly PEDZEL_PRZYCISK_WCISNIETY As New SolidBrush(KolorRGB("#EDEDED"))
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
    Private poczatkowaTransformacja As Drawing2D.Matrix
    Private trybProjektowy As Boolean

    Private ReadOnly Property IRysownik_KOLKO_SZER As Single Implements IRysownik.KOLKO_SZER
        Get
            Return KOLKO_SZER
        End Get
    End Property

    Private ReadOnly Property IRysownik_KOLOR_TOR_TEN_ODCINEK As Color Implements IRysownik.KOLOR_TOR_TEN_ODCINEK
        Get
            Return KOLOR_TOR_TEN_ODCINEK
        End Get
    End Property

    Private ReadOnly Property IRysownik_KOLOR_TOR_PRZYPISANY As Color Implements IRysownik.KOLOR_TOR_PRZYPISANY
        Get
            Return KOLOR_TOR_PRZYPISANY
        End Get
    End Property

    Private ReadOnly Property IRysownik_KOLOR_TOR_NIEPRZYPISANY As Color Implements IRysownik.KOLOR_TOR_NIEPRZYPISANY
        Get
            Return KOLOR_TOR_NIEPRZYPISANY
        End Get
    End Property

    Private ReadOnly Property IRysownik_KOLOR_TOR_LICZNIK_ODCINEK_2 As Color Implements IRysownik.KOLOR_TOR_LICZNIK_ODCINEK_2
        Get
            Return KOLOR_TOR_LICZNIK_ODCINEK_2
        End Get
    End Property

    Friend Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics) Implements IRysownik.Rysuj
        gr = grp
        gr.Clear(ps.BackColor)

        If ps.Pulpit Is Nothing Then Exit Sub

        gr.ResetTransform()
        gr.TranslateTransform(ps.Przesuniecie.X, ps.Przesuniecie.Y)
        poczatkowaTransformacja = gr.Transform
        gr.FillRectangle(PEDZEL_TLO_KOSTKI, New Rectangle(0, 0, ps.SzerokoscPulpitu, ps.WysokoscPulpitu))

        pedzelToru = PEDZEL_TOR_WOLNY
        trybProjektowy = ps.TrybProjektowy

        If ps.RysujKrawedzieKostek Then
            gr.ScaleTransform(ps.Skalowanie, ps.Skalowanie)

            For x As Integer = 0 To ps.Pulpit.Szerokosc
                gr.DrawLine(PEDZEL_KRAWEDZIE, x, 0, x, ps.Pulpit.Wysokosc)
            Next

            For y As Integer = 0 To ps.Pulpit.Wysokosc
                gr.DrawLine(PEDZEL_KRAWEDZIE, 0, y, ps.Pulpit.Szerokosc, y)
            Next
        End If

        'Rysuj kostki
        For x As Integer = 0 To ps.Pulpit.Szerokosc - 1
            For y As Integer = 0 To ps.Pulpit.Wysokosc - 1
                Dim k As Zaleznosci.Kostka = ps.Pulpit.Kostki(x, y)
                If k Is Nothing Then Continue For

                If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Tory Then UstawKolorToru(k, ps.projZaznaczonyOdcinek)
                If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then UstawKolorToruDlaLicznika(k, ps.projZaznaczonyLicznik)
                Dim zazn As Boolean = ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Nic AndAlso k Is ps.projZaznaczonaKostka
                RysujKostke(x, y, ps.Skalowanie, k, zazn)
            Next
        Next

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Then
            Dim en As List(Of Zaleznosci.Lampa).Enumerator = ps.Pulpit.Lampy.GetEnumerator
            While en.MoveNext
                Dim l As Zaleznosci.Lampa = en.Current
                RysujKolko(If(l Is ps.projZaznaczonaLampa, PEDZEL_LAMPA_ZAZN, PEDZEL_LAMPA_TLO), ps.Skalowanie, l.X, l.Y)
            End While
        End If

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then
            Dim l As Zaleznosci.ParaLicznikowOsi = ps.projZaznaczonyLicznik
            If l IsNot Nothing Then
                RysujKolko(PEDZEL_TOR_TEN_ODCINEK, ps.Skalowanie, l.X1, l.Y1)
                RysujKolko(PEDZEL_TOR_LICZNIK_ODCINEK_2, ps.Skalowanie, l.X2, l.Y2)
            End If
        End If
    End Sub

    Private Sub RysujKostke(x As Integer, y As Integer, skalowanie As Single, kostka As Zaleznosci.Kostka, zaznaczona As Boolean)
        gr.Transform = poczatkowaTransformacja
        gr.ScaleTransform(skalowanie, skalowanie)
        gr.TranslateTransform(x, y)
        Obroc(POL, POL, kostka.Obrot)
        obrot = kostka.Obrot

        If zaznaczona Then gr.FillRectangle(PEDZEL_ZAZN_KOSTKA, 0, 0, 1, 1)

        Select Case kostka.Typ
            Case Zaleznosci.TypKostki.Tor
                RysujTor()
            Case Zaleznosci.TypKostki.TorKoniec
                RysujKoniecToru()
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
                RysujKierunek(CType(kostka, Zaleznosci.Kierunek))
            Case Zaleznosci.TypKostki.Napis
                RysujKostkeNapis(CType(kostka, Zaleznosci.Napis))
        End Select
    End Sub

    Private Sub RysujTor(Optional dlugosc As Single = 1.0F)
        gr.FillRectangle(pedzelToru, 0, POL - TOR_SZEROKOSC / 2, dlugosc, TOR_SZEROKOSC)
    End Sub

    Private Sub RysujKoniecToru()
        RysujTor(TOR_KONC_DLUGOSC)
        gr.FillRectangle(pedzelToru, TOR_KONC_DLUGOSC - TOR_KONC_KONCOWKI_DL, POL - TOR_KONC_KONCOWKI - TOR_SZEROKOSC / 2, TOR_KONC_KONCOWKI_DL, TOR_SZEROKOSC)     'góra
        gr.FillRectangle(pedzelToru, TOR_KONC_DLUGOSC - TOR_KONC_KONCOWKI_DL, POL + TOR_KONC_KONCOWKI - TOR_SZEROKOSC / 2, TOR_KONC_KONCOWKI_DL, TOR_SZEROKOSC)     'dół
        gr.FillRectangle(pedzelToru, TOR_KONC_DLUGOSC, POL - TOR_KONC_KONCOWKI - TOR_SZEROKOSC / 2, TOR_SZEROKOSC, 2 * TOR_KONC_KONCOWKI + TOR_SZEROKOSC)           'koniec
    End Sub

    Private Sub RysujZakret()
        Dim szer As Single = TOR_SZEROKOSC / COS45
        gr.FillPolygon(pedzelToru, {
        New PointF(1, POL - szer / 2),
        New PointF(1, POL + szer / 2),
        New PointF(POL + szer / 2, 1),
        New PointF(POL - szer / 2, 1)
        })
    End Sub

    Private Sub RysujNazwe(nazwa As String, x As Single, y As Single, Optional wys As Single = SYGN_POZ)
        Dim rect As New RectangleF(x, y, 1 - x, wys)
        Dim transformacja As Drawing2D.Matrix = gr.Transform

        If obrot >= 2 * KAT_PROSTY And obrot < 4 * KAT_PROSTY Then
            Dim rozm As SizeF = gr.MeasureString(nazwa, CZCIONKA, New SizeF(rect.Width, rect.Height))
            Dim x1 As Single = rect.X + rozm.Width / 2
            Dim y1 As Single = rect.Y + rozm.Height / 2
            Obroc(x1, y1, 2 * KAT_PROSTY)
        End If

        gr.DrawString(nazwa, CZCIONKA, PEDZEL_TEKST, rect)
        gr.Transform = transformacja
    End Sub

    Private Sub RysujRozjazdLewo(rozjazd As Zaleznosci.RozjazdLewo)
        RysujZakret()
        RysujTor()
        RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety)
        RysujNazwe(rozjazd.Nazwa, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
    End Sub

    Private Sub RysujRozjazdPrawo(rozjazd As Zaleznosci.RozjazdPrawo)
        Dim transformacja As Drawing2D.Matrix = gr.Transform
        Obroc(POL, POL, -KAT_PROSTY)
        RysujZakret()
        gr.Transform = transformacja
        RysujTor()
        RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety, 2)
        RysujNazwe(rozjazd.Nazwa, SYGN_POZ + TEKST_POZ_X_PRZYCISK, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujNazweSygnalizatora(nazwa As String)
        RysujNazwe(nazwa, TEKST_POZ_X, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujSygnalizatorManewrowy(sygnalizator As Zaleznosci.SygnalizatorManewrowy)
        Dim pedzNiebieski As Brush = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.BrakWyjazdu, PEDZEL_SYGN_NIEB_JASNY, PEDZEL_SYGN_NIEB)
        Dim pedzBialy As Brush = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.Manewrowy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        RysujTor()
        gr.FillPie(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillPie(PEDZEL_SYGN_TLO, 2 * SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 3 * KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillRectangle(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ, SYGN_TLO_SZER)
        gr.FillEllipse(pedzNiebieski, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(pedzBialy, 2 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(2)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorPolsamoczynny(sygnalizator As Zaleznosci.SygnalizatorPolsamoczynny)
        Dim pedzCzer As Brush = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)
        Dim pedzZiel As Brush = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zezwalajacy, PEDZEL_SYGN_ZIEL_JASNY, PEDZEL_SYGN_ZIEL)
        Dim pedzBial As Brush = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Manewrowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        RysujTor()
        gr.FillPie(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillPie(PEDZEL_SYGN_TLO, 3 * SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER, 3 * KAT_PROSTY, 2 * KAT_PROSTY)
        gr.FillRectangle(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ - SYGN_TLO_SZER / 2, 2 * SYGN_POZ, SYGN_TLO_SZER)
        gr.FillEllipse(pedzCzer, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(pedzZiel, 2 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        gr.FillEllipse(pedzBial, 3 * SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(3)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorSamoczynny(sygnalizator As Zaleznosci.SygnalizatorSamoczynny)
        Dim pedz As Brush = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.BrakWyjazdu, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)

        RysujTor()
        gr.FillEllipse(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER)
        gr.FillEllipse(pedz, SYGN_POZ - SYGN_SZER / 2, SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
        RysujSlupSygnalizatora(1)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujPrzycisk(wcisniety As Boolean, Optional poczy As Single = 0)
        Dim pedzel As Brush = If(wcisniety, PEDZEL_PRZYCISK_WCISNIETY, PEDZEL_PRZYCISK)
        gr.FillEllipse(PEDZEL_SYGN_TLO, SYGN_POZ - SYGN_TLO_SZER / 2, poczy * SYGN_POZ + SYGN_POZ - SYGN_TLO_SZER / 2, SYGN_TLO_SZER, SYGN_TLO_SZER)
        gr.FillEllipse(pedzel, SYGN_POZ - SYGN_SZER / 2, poczy * SYGN_POZ + SYGN_POZ - SYGN_SZER / 2, SYGN_SZER, SYGN_SZER)
    End Sub

    Private Sub RysujPrzyciskZwykly(przycisk As Zaleznosci.Przycisk)
        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety)
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
        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety)
        If przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy Or przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy Then
            RysujNazwe(NAZWA_M, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
        End If
        RysujNazweSygnalizatora(przycisk.ObslugiwanySygnalizator?.Nazwa)
    End Sub

    Private Sub RysujKierunek(kier As Zaleznosci.Kierunek)
        gr.FillPolygon(pedzelToru, New PointF() {
        New PointF(POL + KIER_SZER / 2, POL - KIER_SZER / 2),
        New PointF(POL - KIER_SZER / 2, POL),
        New PointF(POL + KIER_SZER / 2, POL + KIER_SZER / 2)
        })
        RysujPrzycisk((Not trybProjektowy) And kier.Wcisniety)
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
        gr.Transform = poczatkowaTransformacja
        gr.ScaleTransform(skalowanie, skalowanie)
        gr.TranslateTransform(x - KOLKO_SZER / 2.0F, y - KOLKO_SZER / 2.0F)
        gr.FillEllipse(pedzel, 0, 0, KOLKO_SZER, KOLKO_SZER)
    End Sub

    Private Sub Obroc(srodekX As Single, srodekY As Single, kat As Single)
        gr.TranslateTransform(srodekX, srodekY)
        gr.RotateTransform(kat)
        gr.TranslateTransform(-srodekX, -srodekY)
    End Sub

    Private Sub UstawKolorToru(k As Zaleznosci.Kostka, zazn As Zaleznosci.OdcinekToru)
        If TypeOf k Is Zaleznosci.Tor Then
            Dim t As Zaleznosci.Tor = DirectCast(k, Zaleznosci.Tor)
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

        If TypeOf k Is Zaleznosci.Tor Then
            Dim t As Zaleznosci.Tor = DirectCast(k, Zaleznosci.Tor)
            If t.NalezyDoOdcinka IsNot Nothing Then
                If t.NalezyDoOdcinka Is zazn.Odcinek1 Then
                    pedzelToru = PEDZEL_TOR_TEN_ODCINEK
                ElseIf t.NalezyDoOdcinka Is zazn.Odcinek2
                    pedzelToru = PEDZEL_TOR_LICZNIK_ODCINEK_2
                End If
            End If
        End If
    End Sub

End Class