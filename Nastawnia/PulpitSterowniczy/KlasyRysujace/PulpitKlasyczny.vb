Friend MustInherit Class PulpitKlasyczny(Of TOlowek, TPedzel, TMacierz, TCzcionka)
    Implements IRysownik

    Private Const COS45 As Single = 0.707
    Private Const KAT_PROSTY As Single = 90.0F
    Private Const POL As Single = 0.5F
    Private Const KRAWEDZ_SZER As Single = 0.02         'grubość krawędzi między kostkami
    Private Const KOLKO_PROMIEN As Single = 0.125       'promień kółka (lampy/licznika osi)
    Private Const KOLKO_TEKST_PROMIEN As Single = 0.06  'promień kółka obok tekstu
    Private Const KOLKO_TEKST_POZ As Single = 0.1       'położenie kółka obok tekstu
    Private Const TOR_SZEROKOSC As Single = 0.16        'szerokość toru na kostce
    Private Const TOR_SZER_ZAKRET As Single = TOR_SZEROKOSC / COS45
    Private Const TOR_KONC_DLUGOSC As Single = 0.27     'długość toru na kostce z końcem
    Private Const TOR_KONC_SZEROKOSC As Single = 0.5    'szerokość odcinka prostopadłego na kostce z końcem
    Private Const SZCZELINA_MARGINES_POZIOM As Single = 0.1     'margines szczeliny w poziomie
    Private Const SZCZEL_MARG_POZIOM_ZAKRET As Single = 0.05    'margines szczeliny w poziomie toru ukośnego
    Private Const SZCZEL_MARG_POZIOM_ROZJ As Single = 0.08      'margines szczeliny w poziomie toru bocznego rozjazdu, na końcu przylegającym do toru prostego
    Private Const SZCZELINA_MARGINES_PION As Single = 0.04      'margines szczeliny w pionie
    Private Const SZCZELINA_MARGINES_KIER As Single = 0.06      'margines szczeliny na kostce z kierunkiem
    Private Const SYGN_POZ As Single = 0.25             'wielokorotność stałej oznacza położenie środków kolejnych świateł sygnałów na osi X
    Private Const SYGN_PROMIEN As Single = 0.09         'promień sygnału
    Private Const SYGN_TLO_SZER As Single = 0.28        'średnica okręgu stanowiącego tło sygnału
    Private Const SYGN_SLUP_SZER_DUZA As Single = 0.15  'szerokość słupa sygnalizatora w szerszym miejscu
    Private Const SYGN_SLUP_SZER_MALA As Single = 0.05  'szerokosć słupa sygnalizatora w węższym miejscu
    Private Const SYGN_SLUP_DLUG As Single = 0.04       'długość poszczególnych segmentów słupa
    Private Const SYGN_KRAWEDZ As Single = 0.01         'grubość krawędzi słupa sygnalizatora
    Private Const KIER_SZER As Single = 0.4             'rozmiar strzałki na przycisku kierunku
    Private Const TEKST_POZ_X_PRZYCISK As Single = 0.17 'dodatkowy margines dla tekstu obok przycisku
    Private Const TEKST_POZ_X As Single = 0.1           'dodatkowy margines dla tekstu
    Private Const TEKST_POZ_Y As Single = 0.12          'dodatkowy margines dla tekstu
    Private Const TEKST_NAPIS_POZ As Single = 0.12      'pozycja tekstu w kostce z napisem
    Private Const TEKST_WYS As Single = 0.78            'wysokość tekstu w kostce z napisem
    Private Const KRAWEDZ_RAMKA_ZAZN As Single = 0.04   'grubość krawędzi ramki zaznaczenia lamp
    Private Const DODATKOWY_MARGINS As Single = 0.01    'margines uwzględniany w elementach, aby te lekko nachodziły na siebie i nie rysowały się przerwy między nimi

    Private ReadOnly KOLOR_TOR_PRZYPISANY As Color = KolorRGB("#8C8C8C")          'tor przypisany do innego odcinka
    Private ReadOnly KOLOR_TOR_TEN_ODCINEK As Color = KolorRGB("#25FF1A")         'tor przypisany do zaznaczonego odcinka
    Private ReadOnly KOLOR_TOR_NIEPRZYPISANY As Color = KolorRGB("#FF1A1A")       'tor nieprzypisany do żadnego odcinka
    Private ReadOnly KOLOR_TOR_LICZNIK_ODCINEK_2 As Color = KolorRGB("#D11AFF")   'drugi odcinek obsługiwany przez parę liczników osi 1A29FF
    Private PEDZEL_TLO_KOSTKI As TPedzel
    Private PEDZEL_KRAWEDZIE As TOlowek
    Private PEDZEL_TOR_WOLNY As TPedzel
    Private PEDZEL_TOR_TEN_ODCINEK As TPedzel
    Private PEDZEL_TOR_NIEPRZYPISANY As TPedzel
    Private PEDZEL_TOR_LICZNIK_ODCINEK_2 As TPedzel
    Private PEDZEL_SZCZELINA_WOLNY As TPedzel
    Private PEDZEL_SZCZELINA_UTWIERDZONY As TPedzel
    Private PEDZEL_SZCZELINA_ZAJETY As TPedzel
    Private PEDZEL_SZCZELINA_ZWROTNICA As TPedzel
    Private PEDZEL_SZCZELINA_ROZPRUCIE As TPedzel
    Private PEDZEL_SYGN_CZER As TPedzel
    Private PEDZEL_SYGN_CZER_JASNY As TPedzel
    Private PEDZEL_SYGN_ZIEL As TPedzel
    Private PEDZEL_SYGN_ZIEL_JASNY As TPedzel
    Private PEDZEL_SYGN_NIEB As TPedzel
    Private PEDZEL_SYGN_NIEB_JASNY As TPedzel
    Private PEDZEL_SYGN_BIAL As TPedzel
    Private PEDZEL_SYGN_BIAL_JASNY As TPedzel
    Private PEDZEL_SYGN_TLO As TPedzel
    Private PEDZEL_SYGN_KRAWEDZ As TOlowek
    Private PEDZEL_PRZYCISK As TPedzel
    Private PEDZEL_PRZYCISK_WCISNIETY As TPedzel
    Private PEDZEL_TEKST As TPedzel
    Private PEDZEL_ZAZN_KOSTKA As TPedzel
    Private PEDZEL_LAMPA_TLO As TPedzel
    Private PEDZEL_LAMPA_ZAZN As TPedzel
    Private PEDZEL_KOLKO_TEKST As TPedzel
    Private PEDZEL_OBSZAR_ZAZN_RAMKA As TOlowek
    Private PEDZEL_OBSZAR_ZAZN_TLO As TPedzel

    Private ReadOnly PUNKTY_KIERUNKU As PointF()
    Private ReadOnly PUNKTY_SZCZELINY_KIERUNKU As PointF()

    Private CZCIONKA As TCzcionka

    Private Const NAZWA_SZ As String = "Sz"     'Sygnał zastępczy
    Private Const NAZWA_ZW As String = "Zw"     'Zwolnienie przebiegów
    Private Const NAZWA_M As String = "m"       'Sygnał manewrowy

    Protected urz As IUrzadzenieRysujace(Of TOlowek, TPedzel, TMacierz, TCzcionka)
    Private pedzelToru As TPedzel
    Private pedzelSzczelinyWprost As TPedzel
    Private pedzelSzczelinyBok As TPedzel
    Private obrot As Single
    Private glownaTransformacja As TMacierz
    Private trybProjektowy As Boolean
    Private Zainicjalizowano As Boolean = False

    Private ReadOnly Property IRysownik_KOLKO_SZER As Single Implements IRysownik.KOLKO_SZER
        Get
            Return KOLKO_PROMIEN
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

    Protected Sub New(urzadzenie As IUrzadzenieRysujace(Of TOlowek, TPedzel, TMacierz, TCzcionka))
        urz = urzadzenie

        PUNKTY_KIERUNKU = ObliczWspolrzedneKierunku()
        PUNKTY_SZCZELINY_KIERUNKU = ObliczWspolrzedneSzczelinyKierunku(PUNKTY_KIERUNKU)
    End Sub

    Public Sub Inicjalizuj(uchwyt As IntPtr, szer As UInteger, wys As UInteger) Implements IRysownik.Inicjalizuj
        If Zainicjalizowano Then Exit Sub

        urz.Inicjalizuj(uchwyt, szer, wys)

        PEDZEL_TLO_KOSTKI = urz.UtworzPedzel(KolorRGB("#99FFCC"))
        PEDZEL_KRAWEDZIE = urz.UtworzOlowek(Color.White)
        PEDZEL_TOR_WOLNY = urz.UtworzPedzel(KOLOR_TOR_PRZYPISANY)
        PEDZEL_TOR_TEN_ODCINEK = urz.UtworzPedzel(KOLOR_TOR_TEN_ODCINEK)
        PEDZEL_TOR_NIEPRZYPISANY = urz.UtworzPedzel(KOLOR_TOR_NIEPRZYPISANY)
        PEDZEL_TOR_LICZNIK_ODCINEK_2 = urz.UtworzPedzel(KOLOR_TOR_LICZNIK_ODCINEK_2)
        PEDZEL_SZCZELINA_WOLNY = urz.UtworzPedzel(KolorRGB("#A8A8A8"))
        PEDZEL_SZCZELINA_UTWIERDZONY = urz.UtworzPedzel(KolorRGB("#FFFFFF"))
        PEDZEL_SZCZELINA_ZAJETY = urz.UtworzPedzel(KolorRGB("#FF3030"))
        PEDZEL_SZCZELINA_ZWROTNICA = urz.UtworzPedzel(KolorRGB("#FFFF00"))
        PEDZEL_SZCZELINA_ROZPRUCIE = urz.UtworzPedzel(KolorRGB("#C00000"))
        PEDZEL_SYGN_CZER = urz.UtworzPedzel(KolorRGB("#520000"))
        PEDZEL_SYGN_CZER_JASNY = urz.UtworzPedzel(KolorRGB("#FF3838"))
        PEDZEL_SYGN_ZIEL = urz.UtworzPedzel(KolorRGB("#004700"))
        PEDZEL_SYGN_ZIEL_JASNY = urz.UtworzPedzel(KolorRGB("#33FF33"))
        PEDZEL_SYGN_NIEB = urz.UtworzPedzel(KolorRGB("#000661"))
        PEDZEL_SYGN_NIEB_JASNY = urz.UtworzPedzel(KolorRGB("#14EBFF"))
        PEDZEL_SYGN_BIAL = urz.UtworzPedzel(KolorRGB("#909090"))
        PEDZEL_SYGN_BIAL_JASNY = urz.UtworzPedzel(KolorRGB("#FFFFFF"))
        PEDZEL_SYGN_TLO = urz.UtworzPedzel(KolorRGB("#808080"))
        PEDZEL_SYGN_KRAWEDZ = urz.UtworzOlowek(KolorRGB("#000000"))
        PEDZEL_PRZYCISK = urz.UtworzPedzel(KolorRGB("#000000"))
        PEDZEL_PRZYCISK_WCISNIETY = urz.UtworzPedzel(KolorRGB("#EDEDED"))
        PEDZEL_TEKST = urz.UtworzPedzel(KolorRGB("#000000"))
        PEDZEL_ZAZN_KOSTKA = urz.UtworzPedzel(KolorRGB("#009DFF"))
        PEDZEL_LAMPA_TLO = urz.UtworzPedzel(KolorRGB("#FFEA00"))
        PEDZEL_LAMPA_ZAZN = urz.UtworzPedzel(KolorRGB("#FF9500"))
        PEDZEL_KOLKO_TEKST = urz.UtworzPedzel(KolorRGB("#FF1A71"))
        PEDZEL_OBSZAR_ZAZN_RAMKA = urz.UtworzOlowek(KolorRGB("#00D8DB"))
        PEDZEL_OBSZAR_ZAZN_TLO = urz.UtworzPedzel(KolorRGB("#00D8DB", 70))
        CZCIONKA = urz.UtworzCzcionke("Arial", 0.17)

        Zainicjalizowano = True
    End Sub

    Public Sub ZmienRozmiar(szer As UInteger, wys As UInteger) Implements IRysownik.ZmienRozmiar
        urz.ZmienRozmiar(szer, wys)
    End Sub

    Friend Overridable Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics) Implements IRysownik.Rysuj
        If Not Zainicjalizowano Then Exit Sub

        urz.RozpocznijRysunek(grp)
        urz.Czysc(ps.BackColor)

        If ps.Pulpit Is Nothing Then
            urz.ZakonczRysunek()
            Exit Sub
        End If

        urz.TransformacjaResetuj()
        urz.TransformacjaSkaluj(ps.Skalowanie)
        urz.TransformacjaPrzesun(ps.Przesuniecie.X, ps.Przesuniecie.Y)
        glownaTransformacja = urz.TransformacjaPobierz

        urz.WypelnijProstokat(PEDZEL_TLO_KOSTKI, 0, 0, ps.Pulpit.Szerokosc, ps.Pulpit.Wysokosc)

        pedzelToru = PEDZEL_TOR_WOLNY
        trybProjektowy = ps.TrybProjektowy

        If ps.RysujKrawedzieKostek Then
            For x As Integer = 0 To ps.Pulpit.Szerokosc
                urz.RysujLinie(PEDZEL_KRAWEDZIE, KRAWEDZ_SZER, x, 0, x, ps.Pulpit.Wysokosc)
            Next

            For y As Integer = 0 To ps.Pulpit.Wysokosc
                urz.RysujLinie(PEDZEL_KRAWEDZIE, KRAWEDZ_SZER, 0, y, ps.Pulpit.Szerokosc, y)
            Next
        End If

        'Rysuj kostki
        For x As Integer = 0 To ps.Pulpit.Szerokosc - 1
            For y As Integer = 0 To ps.Pulpit.Wysokosc - 1
                Dim k As Zaleznosci.Kostka = ps.Pulpit.Kostki(x, y)
                If k Is Nothing Then Continue For

                UstawKolorSzczeliny(k)
                If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Tory Then UstawKolorToru(k, ps.projZaznaczonyOdcinek)
                If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then UstawKolorToruDlaLicznika(k, ps.projZaznaczonyLicznik)
                Dim zazn As Boolean = ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Nic AndAlso (ps.TrybProjektowy Or ps.MozliwoscZaznaczeniaToru) AndAlso k Is ps.ZaznaczonaKostka
                RysujKostke(x, y, k, zazn)
            Next
        Next

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Or (Not ps.TrybProjektowy And ps.MozliwoscZaznaczeniaLamp) Then
            Dim zaznLampy As HashSet(Of Zaleznosci.Lampa) = ps.ZaznaczoneLampy
            Dim en As List(Of Zaleznosci.Lampa).Enumerator = ps.Pulpit.Lampy.GetEnumerator
            While en.MoveNext
                Dim l As Zaleznosci.Lampa = en.Current
                Dim zazn As Boolean = (ps.TrybProjektowy And l Is ps.projZaznaczonaLampa) Or (Not ps.TrybProjektowy And zaznLampy.Contains(l))
                RysujKolko(If(zazn, PEDZEL_LAMPA_ZAZN, PEDZEL_LAMPA_TLO), l.X, l.Y)
            End While
        End If

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then
            Dim l As Zaleznosci.ParaLicznikowOsi = ps.projZaznaczonyLicznik
            If l IsNot Nothing Then
                RysujKolko(PEDZEL_TOR_TEN_ODCINEK, l.X1, l.Y1)
                RysujKolko(PEDZEL_TOR_LICZNIK_ODCINEK_2, l.X2, l.Y2)
            End If
        End If

        If Not ps.TrybProjektowy AndAlso ps.MozliwoscZaznaczeniaLamp AndAlso Not ps.PoczatekZaznaczeniaLamp.IsEmpty AndAlso Not ps.KoniecZaznaczeniaLamp.IsEmpty Then
            RysujZaznaczenieLamp(ps.PoczatekZaznaczeniaLamp, ps.KoniecZaznaczeniaLamp)
        End If

        urz.ZakonczRysunek()
    End Sub

    Private Sub RysujKostke(x As Integer, y As Integer, kostka As Zaleznosci.Kostka, zaznaczona As Boolean)
        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(kostka.Obrot, POL, POL)
        urz.TransformacjaPrzesun(x, y)
        urz.TransformacjaDolacz(glownaTransformacja)

        obrot = kostka.Obrot

        If zaznaczona Then urz.WypelnijProstokat(PEDZEL_ZAZN_KOSTKA, 0, 0, 1, 1)

        Select Case kostka.Typ
            Case Zaleznosci.TypKostki.Tor
                RysujTorProsty()
            Case Zaleznosci.TypKostki.TorKoniec
                RysujKoniecToru()
            Case Zaleznosci.TypKostki.Zakret
                RysujTorUkosny()
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

    Private Sub RysujTorProsty(Optional dlugosc As Single = 1.0F)
        RysujTor(dlugosc)
        RysujSzczelineToru(dlugosc)
    End Sub

    Private Sub RysujTor(dlugosc As Single)
        urz.WypelnijProstokat(pedzelToru, 0, POL - TOR_SZEROKOSC / 2, dlugosc, TOR_SZEROKOSC)
    End Sub

    Private Sub RysujSzczelineToru(dlugosc As Single)
        urz.WypelnijProstokat(pedzelSzczelinyWprost, SZCZELINA_MARGINES_POZIOM, POL - TOR_SZEROKOSC / 2 + SZCZELINA_MARGINES_PION, dlugosc - 2 * SZCZELINA_MARGINES_POZIOM, TOR_SZEROKOSC - 2 * SZCZELINA_MARGINES_PION)
    End Sub

    Private Sub RysujKoniecToru()
        RysujTor(TOR_KONC_DLUGOSC)
        urz.WypelnijProstokat(pedzelToru, TOR_KONC_DLUGOSC - DODATKOWY_MARGINS, POL - TOR_KONC_SZEROKOSC / 2, TOR_SZEROKOSC, TOR_KONC_SZEROKOSC)
    End Sub

    Private Sub RysujTorUkosny()
        RysujZakret()
        RysujSzczelineZakretu(pedzelSzczelinyWprost)
    End Sub

    Private Sub RysujZakret()
        urz.WypelnijFigure(pedzelToru, {
            New PointF(1, POL - TOR_SZER_ZAKRET / 2),
            New PointF(1, POL + TOR_SZER_ZAKRET / 2),
            New PointF(POL + TOR_SZER_ZAKRET / 2, 1),
            New PointF(POL - TOR_SZER_ZAKRET / 2, 1)
        })
    End Sub

    Private Sub RysujSzczelineZakretu(pedzel As TPedzel, Optional margines_lewy As Single = SZCZEL_MARG_POZIOM_ZAKRET, Optional margines_prawy As Single = SZCZEL_MARG_POZIOM_ZAKRET)
        Dim marg_pion As Single = SZCZELINA_MARGINES_PION / COS45
        Dim szczel As Single = TOR_SZER_ZAKRET - 2 * marg_pion

        Dim A As New PointF(1, POL - TOR_SZER_ZAKRET / 2 + marg_pion)
        Dim B As New PointF(1, POL + TOR_SZER_ZAKRET / 2 - marg_pion)
        Dim C As New PointF(POL + TOR_SZER_ZAKRET / 2 - marg_pion, 1)
        Dim D As New PointF(POL - TOR_SZER_ZAKRET / 2 + marg_pion, 1)

        Dim Aprim As New PointF(A.X - margines_prawy - szczel / 2, A.Y + margines_prawy + szczel / 2)
        Dim Bprim As New PointF(B.X - margines_prawy, B.Y + margines_prawy)
        Dim Cprim As New PointF(C.X + margines_lewy, C.Y - margines_lewy)
        Dim Dprim As New PointF(D.X + margines_lewy + szczel / 2, D.Y - margines_lewy - szczel / 2)

        urz.WypelnijFigure(pedzel, {Aprim, Bprim, Cprim, Dprim})
    End Sub

    Private Sub RysujNazwe(nazwa As String, x As Single, y As Single, Optional wys As Single = SYGN_POZ)
        Dim rect As New RectangleF(x, y, 1 - x, wys)

        If obrot >= 2 * KAT_PROSTY And obrot < 4 * KAT_PROSTY Then
            Dim transformacja As TMacierz = urz.TransformacjaPobierz
            Dim rozm As SizeF = urz.ZmierzTekst(CZCIONKA, nazwa, rect.Width, rect.Height)
            Dim x1 As Single = rect.X + rozm.Width / 2
            Dim y1 As Single = rect.Y + rozm.Height / 2

            urz.TransformacjaResetuj()
            urz.TransformacjaObroc(2 * KAT_PROSTY, x1, y1)
            urz.TransformacjaDolacz(transformacja)
        End If

        urz.RysujTekst(PEDZEL_TEKST, CZCIONKA, nazwa, rect.X, rect.Y, rect.Width, rect.Height)
    End Sub

    Private Sub RysujRozjazdLewo(rozjazd As Zaleznosci.RozjazdLewo)
        RysujZakret()
        RysujTor(1)
        RysujSzczelineZakretu(pedzelSzczelinyBok, margines_prawy:=SZCZEL_MARG_POZIOM_ROZJ)
        RysujSzczelineToru(1)
        RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety)
        RysujNazwe(rozjazd.Nazwa, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
    End Sub

    Private Sub RysujRozjazdPrawo(rozjazd As Zaleznosci.RozjazdPrawo)
        RysujTor(1)
        Dim transformacja As TMacierz = urz.TransformacjaPobierz
        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(-KAT_PROSTY, POL, POL)
        urz.TransformacjaDolacz(transformacja)
        RysujZakret()
        RysujSzczelineZakretu(pedzelSzczelinyBok, margines_lewy:=SZCZEL_MARG_POZIOM_ROZJ)
        urz.TransformacjaResetuj()
        urz.TransformacjaDolacz(transformacja)
        RysujSzczelineToru(1)
        RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety, 2)
        RysujNazwe(rozjazd.Nazwa, SYGN_POZ + TEKST_POZ_X_PRZYCISK, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujNazweSygnalizatora(nazwa As String)
        RysujNazwe(nazwa, TEKST_POZ_X, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujSygnalizatorManewrowy(sygnalizator As Zaleznosci.SygnalizatorManewrowy)
        Dim pedzNiebieski As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.BrakWyjazdu, PEDZEL_SYGN_NIEB_JASNY, PEDZEL_SYGN_NIEB)
        Dim pedzBialy As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.Manewrowy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        RysujTorProsty()
        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_SZER / 2)
        urz.WypelnijKolo(pedzNiebieski, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzBialy, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(2)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorPolsamoczynny(sygnalizator As Zaleznosci.SygnalizatorPolsamoczynny)
        Dim pedzCzer As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)
        Dim pedzZiel As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zezwalajacy, PEDZEL_SYGN_ZIEL_JASNY, PEDZEL_SYGN_ZIEL)
        Dim pedzBial As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Manewrowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        RysujTorProsty()
        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 3 * SYGN_POZ, SYGN_POZ, SYGN_TLO_SZER / 2)
        urz.WypelnijKolo(pedzCzer, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzZiel, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzBial, 3 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(3)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorSamoczynny(sygnalizator As Zaleznosci.SygnalizatorSamoczynny)
        Dim pedz As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.BrakWyjazdu, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)

        RysujTorProsty()
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ, SYGN_TLO_SZER / 2)
        urz.WypelnijKolo(pedz, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(1)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujPrzycisk(wcisniety As Boolean, Optional poczy As Single = 0)
        Dim pedzel As TPedzel = If(wcisniety, PEDZEL_PRZYCISK_WCISNIETY, PEDZEL_PRZYCISK)

        urz.WypelnijKolo(PEDZEL_SYGN_TLO, SYGN_POZ, poczy * SYGN_POZ + SYGN_POZ, SYGN_TLO_SZER / 2)
        urz.WypelnijKolo(pedzel, SYGN_POZ, poczy * SYGN_POZ + SYGN_POZ, SYGN_PROMIEN)
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
        RysujTorProsty()
        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety)

        If przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy Or przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy Then
            RysujNazwe(NAZWA_M, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
        End If

        RysujNazweSygnalizatora(przycisk.ObslugiwanySygnalizator?.Nazwa)
    End Sub

    Private Sub RysujKierunek(kier As Zaleznosci.Kierunek)
        urz.WypelnijFigure(pedzelToru, PUNKTY_KIERUNKU)
        urz.WypelnijFigure(PEDZEL_SZCZELINA_ZWROTNICA, PUNKTY_SZCZELINY_KIERUNKU)

        RysujPrzycisk((Not trybProjektowy) And kier.Wcisniety)
    End Sub

    Private Sub RysujKostkeNapis(napis As Zaleznosci.Napis)
        urz.WypelnijKolo(PEDZEL_KOLKO_TEKST, KOLKO_TEKST_POZ, KOLKO_TEKST_POZ, KOLKO_TEKST_PROMIEN)
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

        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x1, y2, x2, y2)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x2, y2, x2, y1)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x2, y1, x3, y1)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x3, y1, x3, y4)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x3, y4, x2, y4)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x2, y4, x2, y3)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x2, y3, x1, y3)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, SYGN_KRAWEDZ, x1, y3, x1, y2)
    End Sub

    Private Sub RysujKolko(pedzel As TPedzel, x As Single, y As Single)
        urz.TransformacjaResetuj()
        urz.TransformacjaPrzesun(x, y)
        urz.TransformacjaDolacz(glownaTransformacja)

        urz.WypelnijKolo(pedzel, 0, 0, KOLKO_PROMIEN)
    End Sub

    Private Sub RysujZaznaczenieLamp(pocz As PointF, konc As PointF)
        urz.TransformacjaResetuj()
        urz.TransformacjaDolacz(glownaTransformacja)

        Dim rect As New RectangleF(
            Math.Min(pocz.X, konc.X),
            Math.Min(pocz.Y, konc.Y),
            Math.Abs(pocz.X - konc.X),
            Math.Abs(pocz.Y - konc.Y))
        urz.WypelnijProstokat(PEDZEL_OBSZAR_ZAZN_TLO, rect.X, rect.Y, rect.Width, rect.Height)
        urz.RysujProstokat(PEDZEL_OBSZAR_ZAZN_RAMKA, KRAWEDZ_RAMKA_ZAZN, rect.X, rect.Y, rect.Width, rect.Height)
    End Sub

    Private Sub UstawKolorToru(k As Zaleznosci.Kostka, zazn As Zaleznosci.OdcinekToru)
        If TypeOf k Is Zaleznosci.Tor Then
            Dim t As Zaleznosci.Tor = DirectCast(k, Zaleznosci.Tor)

            If t.NalezyDoOdcinka Is zazn And zazn IsNot Nothing Then
                pedzelToru = PEDZEL_TOR_TEN_ODCINEK
            ElseIf t.NalezyDoOdcinka IsNot Nothing Then
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
                ElseIf t.NalezyDoOdcinka Is zazn.Odcinek2 Then
                    pedzelToru = PEDZEL_TOR_LICZNIK_ODCINEK_2
                End If
            End If
        End If
    End Sub

    Private Sub UstawKolorSzczeliny(k As Zaleznosci.Kostka)
        pedzelSzczelinyWprost = PEDZEL_SZCZELINA_WOLNY
        pedzelSzczelinyBok = PEDZEL_SZCZELINA_WOLNY

        Dim pedzelUstawiony As Boolean = False
        If TypeOf k Is Zaleznosci.Tor Then
            Dim tor As Zaleznosci.Tor = DirectCast(k, Zaleznosci.Tor)
            pedzelSzczelinyWprost = PobierzPedzelToruNiezwolnionego(tor.Zajetosc, pedzelUstawiony)

            If TypeOf k Is Zaleznosci.Rozjazd Then
                Dim roz As Zaleznosci.Rozjazd = DirectCast(k, Zaleznosci.Rozjazd)

                If Not pedzelUstawiony Then
                    If roz.Rozprucie Then
                        pedzelSzczelinyWprost = PEDZEL_SZCZELINA_ROZPRUCIE
                    ElseIf roz.Stan = Zaleznosci.UstawienieZwrotnicy.Wprost Then
                        pedzelSzczelinyWprost = PEDZEL_SZCZELINA_ZWROTNICA
                    End If
                End If

                pedzelSzczelinyBok = PobierzPedzelToruNiezwolnionego(roz.ZajetoscBok, pedzelUstawiony)
                If Not pedzelUstawiony Then
                    If roz.Rozprucie Then
                        pedzelSzczelinyBok = PEDZEL_SZCZELINA_ROZPRUCIE
                    ElseIf roz.Stan = Zaleznosci.UstawienieZwrotnicy.Bok Then
                        pedzelSzczelinyBok = PEDZEL_SZCZELINA_ZWROTNICA
                    End If
                End If
            End If
        End If
    End Sub

    Private Function PobierzPedzelToruNiezwolnionego(zajetosc As Zaleznosci.ZajetoscToru, ByRef pedzelUstawiony As Boolean) As TPedzel
        pedzelUstawiony = True
        Select Case zajetosc
            Case Zaleznosci.ZajetoscToru.Zajety
                Return PEDZEL_SZCZELINA_ZAJETY
            Case Zaleznosci.ZajetoscToru.PrzebiegUtwierdzony
                Return PEDZEL_SZCZELINA_UTWIERDZONY
        End Select

        pedzelUstawiony = False
        Return PEDZEL_SZCZELINA_WOLNY
    End Function

    Private Function ObliczWspolrzedneKierunku() As PointF()
        Dim A As New PointF(POL + KIER_SZER / 2, POL - KIER_SZER / 2)
        Dim B As New PointF(POL - KIER_SZER / 2, POL)
        Dim C As New PointF(POL + KIER_SZER / 2, POL + KIER_SZER / 2)

        Return New PointF() {A, B, C}
    End Function

    Private Function ObliczWspolrzedneSzczelinyKierunku(punktyDuzegoTrojkata As PointF()) As PointF()
        Dim A As PointF = punktyDuzegoTrojkata(0)
        Dim B As PointF = punktyDuzegoTrojkata(1)
        Dim C As PointF = punktyDuzegoTrojkata(2)

        Dim dl As Single

        Dim ca As New PointF(A.Y - C.Y, -A.X + C.X)
        dl = CSng(Math.Sqrt(ca.X * ca.X + ca.Y * ca.Y))
        ca.X = ca.X / dl * SZCZELINA_MARGINES_KIER
        ca.Y = ca.Y / dl * SZCZELINA_MARGINES_KIER
        Dim PA0 As New PointF(A.X + ca.X, A.Y + ca.Y)

        Dim ab As New PointF(B.Y - A.Y, -B.X + A.X)
        dl = CSng(Math.Sqrt(ab.X * ab.X + ab.Y * ab.Y))
        ab.X = ab.X / dl * SZCZELINA_MARGINES_KIER
        ab.Y = ab.Y / dl * SZCZELINA_MARGINES_KIER
        Dim PB0 As New PointF(B.X + ab.X, B.Y + ab.Y)

        Dim bc As New PointF(C.Y - B.Y, -C.X + B.X)
        dl = CSng(Math.Sqrt(bc.X * bc.X + bc.Y * bc.Y))
        bc.X = bc.X / dl * SZCZELINA_MARGINES_KIER
        bc.Y = bc.Y / dl * SZCZELINA_MARGINES_KIER
        Dim PC0 As New PointF(C.X + bc.X, C.Y + bc.Y)

        Dim a1 As Single = ca.X
        Dim a2 As Single = ab.X
        Dim b1 As Single = ca.Y
        Dim b2 As Single = ab.Y
        Dim c1 As Single = -ca.X * PA0.X - ca.Y * PA0.Y
        Dim c2 As Single = -ab.X * PB0.X - ab.Y * PB0.Y
        Dim y As Single = (a2 * c1 - c2 * a1) / (-a2 * b1 + b2 * a1)
        Dim x As Single = (-b1 * y - c1) / a1
        Dim Aprim As New PointF(x, y)

        a1 = ab.X
        a2 = bc.X
        b1 = ab.Y
        b2 = bc.Y
        c1 = -ab.X * PB0.X - ab.Y * PB0.Y
        c2 = -bc.X * PC0.X - bc.Y * PC0.Y
        y = (a2 * c1 - c2 * a1) / (-a2 * b1 + b2 * a1)
        x = (-b1 * y - c1) / a1
        Dim Bprim As New PointF(x, y)

        a1 = bc.X
        a2 = ca.X
        b1 = bc.Y
        b2 = ca.Y
        c1 = -bc.X * PC0.X - bc.Y * PC0.Y
        c2 = -ca.X * PA0.X - ca.Y * PA0.Y
        y = (a2 * c1 - c2 * a1) / (-a2 * b1 + b2 * a1)
        x = (-b1 * y - c1) / a1
        Dim Cprim As New PointF(x, y)

        Return New PointF() {Aprim, Bprim, Cprim}
    End Function
End Class