Friend MustInherit Class PulpitKlasyczny(Of TOlowek, TPedzel, TMacierz, TCzcionka)
    Implements IRysownik

    Private Const COS45 As Single = 0.707F
    Private Const KAT_PROSTY As Integer = 90
    Private Const POL As Single = 0.5F
    Private Const KRAWEDZ_SZER As Single = 0.02F           'grubość krawędzi między kostkami
    Private Const KOLKO_PROMIEN As Single = 0.125F         'promień kółka (lampy/licznika osi)
    Private Const KOLKO_TEKST_PROMIEN As Single = 0.06F    'promień kółka obok tekstu
    Private Const KOLKO_TEKST_POZ As Single = 0.1F         'położenie kółka obok tekstu
    Private Const TOR_SZEROKOSC As Single = 0.16F          'szerokość toru na kostce
    Private Const TOR_SZER_ZAKRET As Single = TOR_SZEROKOSC / COS45
    Private Const TOR_TROJKAT As Single = (TOR_SZER_ZAKRET - TOR_SZEROKOSC) / 2.0F  'długość przyprostokątnej trójkąta do ścięcia/narysowania przy granicy toru ukośnego i prostego
    Private Const TOR_ELEKTR As Single = POL - TOR_SZEROKOSC / 2.0F - 0.02F         'współrzędna linii elektryfikacji toru
    Private Const TOR_ELEKTR_ROZJAZD As Single = 2.0F * TOR_ELEKTR                  'skrócona współrzędna końca linii elektryfikacji na rozjeździe
    Private Const TOR_KONC_DLUGOSC As Single = 0.27F       'długość toru na kostce z końcem
    Private Const TOR_KONC_SZEROKOSC As Single = 0.5F      'szerokość odcinka prostopadłego na kostce z końcem
    Private Const SZCZELINA_MARGINES_POZIOM As Single = 0.1F    'margines szczeliny w poziomie
    Private Const SZCZEL_MARG_POZIOM_ZAKRET As Single = 0.05F   'margines szczeliny w poziomie toru ukośnego
    Private Const SZCZEL_MARG_POZIOM_ROZJ As Single = 0.08F     'margines szczeliny w poziomie toru bocznego rozjazdu, na końcu przylegającym do toru prostego
    Private Const SZCZELINA_MARGINES_PION As Single = 0.04F     'margines szczeliny w pionie
    Private Const SZCZELINA_MARGINES_KIER As Single = 0.04F     'margines szczeliny na kostce z kierunkiem
    Private Const ROZJAZD_ZNAK_SZER As Single = 0.01F      'szerokość linii plusa/minusa obok rozjazdu
    Private Const ROZJAZD_ZNAK_POL_DL As Single = 0.05F    'połowa długości linii plusa/minusa obok rozjazdu
    Private Const ROZJAZD_ZNAK_WPROST_X As Single = 0.12F  'współrzędna X środka plusa/minusa dla jazdy wprost
    Private Const ROZJAZD_ZNAK_WPROST_Y As Single = 0.7F   'współrzędna Y środka plusa/minusa dla jazdy wprost
    Private Const ROZJAZD_ZNAK_BOK_X As Single = 0.85F     'współrzędna X środka plusa/minusa dla jazdy na bok
    Private Const ROZJAZD_ZNAK_BOK_Y As Single = 0.9F      'współrzędna Y środka plusa/minusa dla jazdy na bok
    Private Const SYGN_POZ As Single = 0.25F               'wielokorotność stałej oznacza położenie środków kolejnych świateł sygnałów na osi X
    Private Const SYGN_PROMIEN As Single = 0.09F           'promień sygnału
    Private Const SYGN_TLO_PROMIEN As Single = 0.14F       'promień okręgu stanowiącego tło sygnału
    Private Const SYGN_SLUP_SZER_DUZA As Single = 0.15F    'szerokość słupa sygnalizatora w szerszym miejscu
    Private Const SYGN_SLUP_SZER_MALA As Single = 0.05F    'szerokosć słupa sygnalizatora w węższym miejscu
    Private Const SYGN_SLUP_DLUG As Single = 0.04F         'długość poszczególnych segmentów słupa
    Private Const SYGN_KRAWEDZ As Single = 0.01F           'grubość krawędzi słupa sygnalizatora
    Private Const PRZYCISK_TLO_PROMIEN As Single = 0.115F  'promień tła przycisku
    Private Const PRZYCISK_RAMKA_PROMIEN As Single = 0.08F 'promień obramowania wciskanej części przycisku
    Private Const PRZYCISK_PROMIEN As Single = 0.055F      'promień wciskanej części przycisku
    Private Const PRZYCISK_TM_DODATEK As Single = 0.09F    'dodatkowe przesunięcie przycisku na kostce sygnalizatora manewrowego
    Private Const KIER_SZER As Single = 0.3F               'rozmiar trójkąta na kostce kierunku
    Private Const KIER_POZ_X As Single = 0.25F             'pozycja trójkąta kierunku na osi X
    Private Const KIER_POZ_Y As Single = 0.78F             'pozycja trójkąta kierunku na osi Y
    Private Const PRZEJAZD_POZ As Single = 0.35F           'odległość linii przejazdu kolejowego od krawędzi bocznych
    Private Const PRZEJAZD_SZER_LINII As Single = 0.02F    'szerokość linii przejazdu kolejowego
    Private Const PRZEJAZD_KONTR_POZ As Single = 0.17F     'pozycja na osi X kontrolek przejazdu kolejowego
    Private Const TEKST_POZ_X_PRZYCISK As Single = 0.17F   'dodatkowy margines dla tekstu obok przycisku
    Private Const TEKST_POZ_X As Single = 0.1F             'dodatkowy margines dla tekstu
    Private Const TEKST_POZ_Y As Single = 0.12F            'dodatkowy margines dla tekstu
    Private Const TEKST_NAPIS_POZ As Single = 0.12F        'pozycja tekstu w kostce z napisem
    Private Const TEKST_WYS_LINIA As Single = 0.27F        'wysokość tekstu jednoliniowego
    Private Const TEKST_WYS_KOSTKA_NAPIS As Single = 0.78F 'wysokość tekstu w kostce z napisem
    Private Const WSPOLRZEDNE_ROZM As Single = 0.33F       'długość linii na skali
    Private Const KRAWEDZ_RAMKA_ZAZN As Single = 0.04F     'grubość krawędzi ramki zaznaczenia lamp
    Private Const DODATKOWY_MARGINES As Single = 0.01F     'margines uwzględniany w elementach, aby te lekko nachodziły na siebie i nie rysowały się przerwy między nimi

    Private ReadOnly KOLOR_TOR_PRZYPISANY As Color = KolorRGB("#8C8C8C")          'tor przypisany do innego odcinka
    Private ReadOnly KOLOR_TOR_TEN_ODCINEK As Color = KolorRGB("#25FF1A")         'tor przypisany do zaznaczonego odcinka
    Private ReadOnly KOLOR_TOR_NIEPRZYPISANY As Color = KolorRGB("#FF1A1A")       'tor nieprzypisany do żadnego odcinka
    Private ReadOnly KOLOR_TOR_LICZNIK_ODCINEK_2 As Color = KolorRGB("#D11AFF")   'drugi odcinek obsługiwany przez parę liczników osi
    Private ReadOnly KOLOR_TLO_SYGNALIZATOR_TOP As Color = KolorRGB("#FF9900")    'tło sygnalizatora ostrzegawczego przejazdowego, który jest przypisany do zaznaczonego obiektu automatyzacji przejazdu
    Private PEDZEL_TLO_KOSTKI As TPedzel
    Private PEDZEL_KRAWEDZIE_KOSTEK As TOlowek
    Private PEDZEL_KRAWEDZ As TOlowek
    Private PEDZEL_TOR_WOLNY As TPedzel
    Private PEDZEL_TOR_TEN_ODCINEK As TPedzel
    Private PEDZEL_TOR_NIEPRZYPISANY As TPedzel
    Private PEDZEL_TOR_LICZNIK_ODCINEK_2 As TPedzel
    Private PEDZEL_SZCZELINA_WOLNY As TPedzel
    Private PEDZEL_SZCZELINA_UTWIERDZONY As TPedzel
    Private PEDZEL_SZCZELINA_ZAJETY As TPedzel
    Private PEDZEL_SZCZELINA_ZWROTNICA As TPedzel
    Private PEDZEL_SZCZELINA_ROZPRUCIE As TPedzel
    Private PEDZEL_ROZJAZD_ZNAK As TOlowek
    Private PEDZEL_SYGN_CZER As TPedzel
    Private PEDZEL_SYGN_CZER_JASNY As TPedzel
    Private PEDZEL_SYGN_ZIEL As TPedzel
    Private PEDZEL_SYGN_ZIEL_JASNY As TPedzel
    Private PEDZEL_SYGN_NIEB As TPedzel
    Private PEDZEL_SYGN_NIEB_JASNY As TPedzel
    Private PEDZEL_SYGN_BIAL As TPedzel
    Private PEDZEL_SYGN_BIAL_JASNY As TPedzel
    Private PEDZEL_SYGN_POMC As TPedzel
    Private PEDZEL_SYGN_POMC_JASNY As TPedzel
    Private PEDZEL_SYGN_TLO As TPedzel
    Private PEDZEL_PRZYCISK_CZERWONY As TPedzel
    Private PEDZEL_PRZYCISK_ZIELONY As TPedzel
    Private PEDZEL_PRZYCISK_CZARNY As TPedzel
    Private PEDZEL_PRZYCISK_BIALY As TPedzel
    Private PEDZEL_PRZYCISK_ZOLTY As TPedzel
    Private PEDZEL_PRZYCISK_RAMKA As TPedzel
    Private PEDZEL_PRZYCISK_WCISNIETY As TPedzel
    Private PEDZEL_TEKST As TPedzel
    Private PEDZEL_ZAZN_KOSTKA As TPedzel
    Private PEDZEL_LAMPA_TLO As TPedzel
    Private PEDZEL_LAMPA_ZAZN As TPedzel
    Private PEDZEL_KOLKO_TEKST As TPedzel
    Private PEDZEL_OBSZAR_ZAZN_RAMKA As TOlowek
    Private PEDZEL_OBSZAR_ZAZN_TLO As TPedzel
    Private PEDZEL_PRZEJAZD_SYGN_TOP As TPedzel
    Private PEDZEL_PRZEJAZD_ROGATKA As TPedzel
    Private PEDZEL_PRZEJAZD_ROGATKA_ZAZN As TPedzel
    Private PEDZEL_PRZEJAZD_SYGN_DROG As TPedzel
    Private PEDZEL_PRZEJAZD_SYGN_DROG_ZAZN As TPedzel

    Private ReadOnly PUNKTY_KIERUNKU As PointF()
    Private ReadOnly PUNKTY_SZCZELINY_KIERUNKU As PointF()

    Private Const DOMYSLNY_ROZMIAR_CZCIONKI As Single = 0.17F
    Private Const NAZWA_CZCIONKI As String = "Arial"
    Private CZCIONKA As TCzcionka

    Private Const NAZWA_SZ As String = "Sz"     'Sygnał zastępczy
    Private Const NAZWA_Z As String = "z"       'Zwolnienie przebiegu
    Private Const NAZWA_M As String = "m"       'Sygnał manewrowy
    Private Const NAZWA_KR As String = "Kr"     'Kasowanie rozprucia
    Private Const NAZWA_WBL As String = "Wbl"   'Włączenie blokady
    Private Const NAZWA_PZK As String = "Pzk"   'Potwierdzenie zmiany kierunku blokady
    Private Const NAZWA_ZWBL As String = "Zwbl" 'Zwolnienie blokady
    Private Const NAZWA_OTW As String = "Otw"   'Otwarcie przejazdu
    Private Const NAZWA_ZAM As String = "Zam"   'Zamknięcie przejazdu

    Protected urz As IUrzadzenieRysujace(Of TOlowek, TPedzel, TMacierz, TCzcionka)
    Private pedzelToru As TPedzel
    Private pedzelSzczelinyWprost As TPedzel
    Private pedzelSzczelinyBok As TPedzel
    Private obrot As Integer
    Private glownaTransformacja As TMacierz
    Private trybProjektowy As Boolean
    Private zainicjalizowano As Boolean = False
    Private pulpit As Zaleznosci.Pulpit
    Private rysujSzczeliny As Boolean
    Private sygnTopWyrozniony As Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy
    Private wysokiStanMigania As Boolean

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

    Private ReadOnly Property IRysownik_KOLOR_TLO_SYGNALIZATOR_PRZEJAZDOWY As Color Implements IRysownik.KOLOR_TLO_SYGNALIZATOR_PRZEJAZDOWY
        Get
            Return KOLOR_TLO_SYGNALIZATOR_TOP
        End Get
    End Property

    Private Property UniewaznioneSasiedztwoTorow As Boolean = True Implements IRysownik.UniewaznioneSasiedztwoTorow

    Protected Sub New(urzadzenie As IUrzadzenieRysujace(Of TOlowek, TPedzel, TMacierz, TCzcionka))
        urz = urzadzenie

        PUNKTY_KIERUNKU = ObliczWspolrzedneKierunku()
        PUNKTY_SZCZELINY_KIERUNKU = ObliczWspolrzedneSzczelinyKierunku(PUNKTY_KIERUNKU)
    End Sub

    Public Sub Inicjalizuj(uchwyt As IntPtr, szer As UInteger, wys As UInteger) Implements IRysownik.Inicjalizuj
        If zainicjalizowano Then Exit Sub

        urz.Inicjalizuj(uchwyt, szer, wys)

        PEDZEL_TLO_KOSTKI = urz.UtworzPedzel(KolorRGB("#99FFCC"))
        PEDZEL_KRAWEDZIE_KOSTEK = urz.UtworzOlowek(Color.White)
        PEDZEL_KRAWEDZ = urz.UtworzOlowek(KolorRGB("#000000"))
        PEDZEL_TOR_WOLNY = urz.UtworzPedzel(KOLOR_TOR_PRZYPISANY)
        PEDZEL_TOR_TEN_ODCINEK = urz.UtworzPedzel(KOLOR_TOR_TEN_ODCINEK)
        PEDZEL_TOR_NIEPRZYPISANY = urz.UtworzPedzel(KOLOR_TOR_NIEPRZYPISANY)
        PEDZEL_TOR_LICZNIK_ODCINEK_2 = urz.UtworzPedzel(KOLOR_TOR_LICZNIK_ODCINEK_2)
        PEDZEL_SZCZELINA_WOLNY = urz.UtworzPedzel(KolorRGB("#404040"))
        PEDZEL_SZCZELINA_UTWIERDZONY = urz.UtworzPedzel(KolorRGB("#FFFFFF"))
        PEDZEL_SZCZELINA_ZAJETY = urz.UtworzPedzel(KolorRGB("#FF3030"))
        PEDZEL_SZCZELINA_ZWROTNICA = urz.UtworzPedzel(KolorRGB("#FFFF00"))
        PEDZEL_SZCZELINA_ROZPRUCIE = urz.UtworzPedzel(KolorRGB("#C00000"))
        PEDZEL_ROZJAZD_ZNAK = urz.UtworzOlowek(KolorRGB("#000000"))
        PEDZEL_SYGN_CZER = urz.UtworzPedzel(KolorRGB("#520000"))
        PEDZEL_SYGN_CZER_JASNY = urz.UtworzPedzel(KolorRGB("#FF3838"))
        PEDZEL_SYGN_ZIEL = urz.UtworzPedzel(KolorRGB("#004700"))
        PEDZEL_SYGN_ZIEL_JASNY = urz.UtworzPedzel(KolorRGB("#33FF33"))
        PEDZEL_SYGN_NIEB = urz.UtworzPedzel(KolorRGB("#000661"))
        PEDZEL_SYGN_NIEB_JASNY = urz.UtworzPedzel(KolorRGB("#14EBFF"))
        PEDZEL_SYGN_BIAL = urz.UtworzPedzel(KolorRGB("#909090"))
        PEDZEL_SYGN_BIAL_JASNY = urz.UtworzPedzel(KolorRGB("#FFFFFF"))
        PEDZEL_SYGN_POMC = urz.UtworzPedzel(KolorRGB("#663D00"))
        PEDZEL_SYGN_POMC_JASNY = urz.UtworzPedzel(KolorRGB("#FF9900"))
        PEDZEL_SYGN_TLO = urz.UtworzPedzel(KolorRGB("#808080"))
        PEDZEL_PRZYCISK_CZERWONY = urz.UtworzPedzel(KolorRGB("#CA3E43"))
        PEDZEL_PRZYCISK_ZIELONY = urz.UtworzPedzel(KolorRGB("#62965E"))
        PEDZEL_PRZYCISK_CZARNY = urz.UtworzPedzel(KolorRGB("#1D1D1D"))
        PEDZEL_PRZYCISK_BIALY = urz.UtworzPedzel(KolorRGB("#FFFFFF"))
        PEDZEL_PRZYCISK_ZOLTY = urz.UtworzPedzel(KolorRGB("#CABF3F"))
        PEDZEL_PRZYCISK_RAMKA = urz.UtworzPedzel(KolorRGB("#555555", 120))
        PEDZEL_PRZYCISK_WCISNIETY = urz.UtworzPedzel(KolorRGB("#42FFFC"))
        PEDZEL_TEKST = urz.UtworzPedzel(KolorRGB("#000000"))
        PEDZEL_ZAZN_KOSTKA = urz.UtworzPedzel(KolorRGB("#009DFF"))
        PEDZEL_LAMPA_TLO = urz.UtworzPedzel(KolorRGB("#FFEA00"))
        PEDZEL_LAMPA_ZAZN = urz.UtworzPedzel(KolorRGB("#FF9500"))
        PEDZEL_KOLKO_TEKST = urz.UtworzPedzel(KolorRGB("#FF1A71"))
        PEDZEL_OBSZAR_ZAZN_RAMKA = urz.UtworzOlowek(KolorRGB("#00D8DB"))
        PEDZEL_OBSZAR_ZAZN_TLO = urz.UtworzPedzel(KolorRGB("#00D8DB", 70))
        PEDZEL_PRZEJAZD_SYGN_TOP = urz.UtworzPedzel(KOLOR_TLO_SYGNALIZATOR_TOP)
        PEDZEL_PRZEJAZD_ROGATKA = urz.UtworzPedzel(KolorRGB("#00BBFF"))
        PEDZEL_PRZEJAZD_ROGATKA_ZAZN = urz.UtworzPedzel(KolorRGB("#000DFF"))
        PEDZEL_PRZEJAZD_SYGN_DROG = urz.UtworzPedzel(KolorRGB("#FF66D1"))
        PEDZEL_PRZEJAZD_SYGN_DROG_ZAZN = urz.UtworzPedzel(KolorRGB("#CC0047"))
        CZCIONKA = urz.UtworzCzcionke(NAZWA_CZCIONKI, DOMYSLNY_ROZMIAR_CZCIONKI, False)

        zainicjalizowano = True
    End Sub

    Public Sub ZmienRozmiar(szer As UInteger, wys As UInteger) Implements IRysownik.ZmienRozmiar
        urz.ZmienRozmiar(szer, wys)
    End Sub

    Friend Overridable Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics) Implements IRysownik.Rysuj
        If Not zainicjalizowano Then Exit Sub

        pulpit = ps.Pulpit
        sygnTopWyrozniony = If(ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyAutomatyzacja, ps.projZaznaczonyPrzejazdAutomatyzacja?.Sygnalizator, Nothing)
        wysokiStanMigania = ps.WysokiStanMigania
        rysujSzczeliny = Not (
            ps.TrybProjektowy And (
                ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Or
                ps.projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow Or
                ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Przejazdy Or
                ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyAutomatyzacja)
            )

        urz.RozpocznijRysunek(grp, ps.BackColor)

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
                urz.RysujLinie(PEDZEL_KRAWEDZIE_KOSTEK, KRAWEDZ_SZER, x, 0, x, ps.Pulpit.Wysokosc)
            Next

            For y As Integer = 0 To ps.Pulpit.Wysokosc
                urz.RysujLinie(PEDZEL_KRAWEDZIE_KOSTEK, KRAWEDZ_SZER, 0, y, ps.Pulpit.Szerokosc, y)
            Next
        End If

        'Rysuj kostki
        If UniewaznioneSasiedztwoTorow Then
            WyznaczWygladzanieZakretow(pulpit)
            UniewaznioneSasiedztwoTorow = False
        End If

        ps.Pulpit.PrzeiterujKostki(AddressOf RysujKostke, ps)

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Lampy Or (Not ps.TrybProjektowy And ps.MozliwoscZaznaczeniaLamp) Then
            Dim zaznLampy As HashSet(Of Zaleznosci.Lampa) = ps.ZaznaczoneLampy
            For Each l As Zaleznosci.Lampa In ps.Pulpit.Lampy
                Dim zazn As Boolean = (ps.TrybProjektowy And l Is ps.projZaznaczonaLampa) Or (Not ps.TrybProjektowy And zaznLampy.Contains(l))
                RysujKolko(If(zazn, PEDZEL_LAMPA_ZAZN, PEDZEL_LAMPA_TLO), l.X, l.Y)
            Next
        End If

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then
            Dim l As Zaleznosci.ParaLicznikowOsi = ps.projZaznaczonyLicznik
            If l IsNot Nothing Then
                RysujKolko(PEDZEL_TOR_TEN_ODCINEK, l.X1, l.Y1)
                RysujKolko(PEDZEL_TOR_LICZNIK_ODCINEK_2, l.X2, l.Y2)
            End If
        End If

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyRogatki AndAlso ps.projZaznaczonyPrzejazd IsNot Nothing Then
            Dim pedzel As TPedzel
            For Each rogatka As Zaleznosci.PrzejazdElementWykonawczy In ps.projZaznaczonyPrzejazd.Rogatki
                pedzel = If(rogatka Is ps.projZaznaczonyPrzejazdRogatka, PEDZEL_PRZEJAZD_ROGATKA_ZAZN, PEDZEL_PRZEJAZD_ROGATKA)
                RysujKolko(pedzel, rogatka.X, rogatka.Y)
            Next
        End If

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdySygnDrog AndAlso ps.projZaznaczonyPrzejazd IsNot Nothing Then
            Dim pedzel As TPedzel
            For Each sygnDrog As Zaleznosci.PrzejazdElementWykonawczy In ps.projZaznaczonyPrzejazd.SygnalizatoryDrogowe
                pedzel = If(sygnDrog Is ps.projZaznaczonyPrzejazdSygnDrog, PEDZEL_PRZEJAZD_SYGN_DROG_ZAZN, PEDZEL_PRZEJAZD_SYGN_DROG)
                RysujKolko(pedzel, sygnDrog.X, sygnDrog.Y)
            Next
        End If

        If Not ps.TrybProjektowy AndAlso ps.MozliwoscZaznaczeniaLamp AndAlso Not ps.PoczatekZaznaczeniaLamp.IsEmpty AndAlso Not ps.KoniecZaznaczeniaLamp.IsEmpty Then
            RysujZaznaczenieLamp(ps.PoczatekZaznaczeniaLamp, ps.KoniecZaznaczeniaLamp)
        End If

        'Rysuj współrzędne
        If ps.RysujWspolrzedne Then
            urz.TransformacjaResetuj()
            urz.TransformacjaSkaluj(ps.Skalowanie)
            urz.TransformacjaPrzesun(ps.Przesuniecie.X, 0.0F)

            Dim xkonc As Integer = ps.Pulpit.Szerokosc
            For x As Integer = 0 To xkonc
                urz.RysujLinie(PEDZEL_KRAWEDZ, KRAWEDZ_SZER, x, 0.0F, x, WSPOLRZEDNE_ROZM)

                If x <> xkonc Then
                    Dim tekst As String = x.ToString
                    Dim rozm As SizeF = urz.ZmierzTekst(CZCIONKA, tekst, 1.0F, WSPOLRZEDNE_ROZM)
                    urz.RysujTekst(PEDZEL_TEKST, CZCIONKA, tekst, x + (1.0F - rozm.Width) / 2.0F, (WSPOLRZEDNE_ROZM - rozm.Height) / 2.0F, 1.0F, WSPOLRZEDNE_ROZM)
                End If
            Next

            urz.TransformacjaResetuj()
            urz.TransformacjaSkaluj(ps.Skalowanie)
            urz.TransformacjaPrzesun(0.0F, ps.Przesuniecie.Y)

            Dim ykonc As Integer = ps.Pulpit.Wysokosc
            For y As Integer = 0 To ykonc
                urz.RysujLinie(PEDZEL_KRAWEDZ, KRAWEDZ_SZER, 0.0F, y, WSPOLRZEDNE_ROZM, y)

                If y <> ykonc Then
                    Dim tekst As String = y.ToString
                    Dim rozm As SizeF = urz.ZmierzTekst(CZCIONKA, tekst, WSPOLRZEDNE_ROZM, 1.0F)
                    urz.RysujTekst(PEDZEL_TEKST, CZCIONKA, tekst, (WSPOLRZEDNE_ROZM - rozm.Width) / 2.0F, y + (1.0F - rozm.Height) / 2.0F, WSPOLRZEDNE_ROZM, 1.0F)
                End If
            Next
        End If

        urz.ZakonczRysunek()
    End Sub

    Private Sub RysujKostke(x As Integer, y As Integer, kostka As Zaleznosci.Kostka, ps As PulpitSterowniczy)
        UstawKolorSzczeliny(kostka)

        Select Case ps.projDodatkoweObiekty
            Case RysujDodatkoweObiekty.OdcinkiTorow
                UstawKolorToru(kostka, ps.projZaznaczonyOdcinek)
            Case RysujDodatkoweObiekty.Liczniki
                UstawKolorToruDlaLicznika(kostka, ps.projZaznaczonyLicznik)
            Case RysujDodatkoweObiekty.Przejazdy
                UstawKolorToruDlaPrzejazdu(kostka, ps.projZaznaczonyPrzejazd)
            Case RysujDodatkoweObiekty.PrzejazdyAutomatyzacja
                UstawKolorToruDlaPrzejazduAutomatyzacja(kostka, ps.projZaznaczonyPrzejazdAutomatyzacja)
        End Select

        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(kostka.Obrot, POL, POL)
        urz.TransformacjaPrzesun(x, y)
        urz.TransformacjaDolacz(glownaTransformacja)
        obrot = kostka.Obrot

        Dim zaznaczona As Boolean = kostka Is ps.ZaznaczonaKostka AndAlso ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Nic AndAlso (ps.TrybProjektowy Or ps.MozliwoscZaznaczeniaToru)
        If zaznaczona Then
            Dim polKrawedzi As Single = KRAWEDZ_SZER * POL
            Dim rozm As Single = 1.0F - KRAWEDZ_SZER
            urz.WypelnijProstokat(PEDZEL_ZAZN_KOSTKA, polKrawedzi, polKrawedzi, rozm, rozm)
        End If

        Select Case kostka.Typ
            Case Zaleznosci.TypKostki.Tor
                RysujTorProsty(CType(kostka, Zaleznosci.Tor), True)
            Case Zaleznosci.TypKostki.TorKoniec
                RysujKoniecToru(CType(kostka, Zaleznosci.TorKoniec).RysowanieDodatkowychTrojkatow)
            Case Zaleznosci.TypKostki.Zakret
                RysujTorUkosny(CType(kostka, Zaleznosci.Zakret))
            Case Zaleznosci.TypKostki.RozjazdLewo
                RysujRozjazdLewo(CType(kostka, Zaleznosci.RozjazdLewo))
            Case Zaleznosci.TypKostki.RozjazdPrawo
                RysujRozjazdPrawo(CType(kostka, Zaleznosci.RozjazdPrawo))
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                RysujSygnalizatorManewrowy(CType(kostka, Zaleznosci.SygnalizatorManewrowy))
            Case Zaleznosci.TypKostki.SygnalizatorPowtarzajacy
                RysujSygnalizatorPowtarzajacy(CType(kostka, Zaleznosci.SygnalizatorPowtarzajacy))
            Case Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                RysujSygnalizatorPolsamoczynny(CType(kostka, Zaleznosci.SygnalizatorPolsamoczynny))
            Case Zaleznosci.TypKostki.SygnalizatorSamoczynny
                RysujSygnalizatorSamoczynny(CType(kostka, Zaleznosci.SygnalizatorSamoczynny))
            Case Zaleznosci.TypKostki.SygnalizatorOstrzegawczyPrzejazdowy
                RysujSygnalizatorTOP(CType(kostka, Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy))
            Case Zaleznosci.TypKostki.PrzejazdKolejowy
                RysujPrzejazd(CType(kostka, Zaleznosci.PrzejazdKolejowoDrogowyKostka))
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

    Private Sub RysujTorProsty(tor As Zaleznosci.Tor, rysujNazwe As Boolean)
        RysujTor(tor.RysowanieDodatkowychTrojkatow, 1.0F)
        If tor.KontrolaNiezajetosci Then RysujSzczelineToru()
        If tor.Zelektryfikowany Then RysujProstaLinieElektryfikacji()
        If rysujNazwe Then RysujNazweDolKostki(tor.Nazwa)
    End Sub

    Private Sub RysujProstaLinieElektryfikacji(Optional dlugosc As Single = 1.0F)
        Dim y As Single = If(CzyObrocicObiekty(), 1.0F - TOR_ELEKTR, TOR_ELEKTR)
        urz.RysujLinie(PEDZEL_KRAWEDZ, KRAWEDZ_SZER, 0.0F, y, dlugosc, y)
    End Sub

    Private Sub RysujTor(dodatkoweTrojkaty As Zaleznosci.DodatkoweTrojkatyTor, dlugosc As Single)
        Dim punkty As New List(Of PointF)

        If (dodatkoweTrojkaty And Zaleznosci.DodatkoweTrojkatyTor.LewoGora) <> 0 Then
            punkty.Add(New PointF(TOR_TROJKAT, POL - TOR_SZEROKOSC / 2))
            punkty.Add(New PointF(0, POL - TOR_SZER_ZAKRET / 2))
        Else
            punkty.Add(New PointF(0, POL - TOR_SZEROKOSC / 2))
        End If

        If (dodatkoweTrojkaty And Zaleznosci.DodatkoweTrojkatyTor.LewoDol) <> 0 Then
            punkty.Add(New PointF(0, POL + TOR_SZER_ZAKRET / 2))
            punkty.Add(New PointF(TOR_TROJKAT, POL + TOR_SZEROKOSC / 2))
        Else
            punkty.Add(New PointF(0, POL + TOR_SZEROKOSC / 2))
        End If

        If (dodatkoweTrojkaty And Zaleznosci.DodatkoweTrojkatyTor.PrawoDol) <> 0 Then
            punkty.Add(New PointF(dlugosc - TOR_TROJKAT, POL + TOR_SZEROKOSC / 2))
            punkty.Add(New PointF(dlugosc, POL + TOR_SZER_ZAKRET / 2))
        Else
            punkty.Add(New PointF(dlugosc, POL + TOR_SZEROKOSC / 2))
        End If

        If (dodatkoweTrojkaty And Zaleznosci.DodatkoweTrojkatyTor.PrawoGora) <> 0 Then
            punkty.Add(New PointF(dlugosc, POL - TOR_SZER_ZAKRET / 2))
            punkty.Add(New PointF(dlugosc - TOR_TROJKAT, POL - TOR_SZEROKOSC / 2))
        Else
            punkty.Add(New PointF(dlugosc, POL - TOR_SZEROKOSC / 2))
        End If

        urz.WypelnijFigure(pedzelToru, punkty.ToArray)
    End Sub

    Private Sub RysujSzczelineToru()
        If Not rysujSzczeliny Then Exit Sub

        urz.WypelnijProstokat(pedzelSzczelinyWprost, SZCZELINA_MARGINES_POZIOM, POL - TOR_SZEROKOSC / 2.0F + SZCZELINA_MARGINES_PION, 1.0F - 2.0F * SZCZELINA_MARGINES_POZIOM, TOR_SZEROKOSC - 2.0F * SZCZELINA_MARGINES_PION)
    End Sub

    Private Sub RysujKoniecToru(dodatkoweTrojkaty As Zaleznosci.DodatkoweTrojkatyTorKoniec)
        RysujTor(CType(dodatkoweTrojkaty, Zaleznosci.DodatkoweTrojkatyTor), TOR_KONC_DLUGOSC)
        urz.WypelnijProstokat(pedzelToru, TOR_KONC_DLUGOSC - DODATKOWY_MARGINES, POL - TOR_KONC_SZEROKOSC / 2, TOR_SZEROKOSC, TOR_KONC_SZEROKOSC)
    End Sub

    Private Sub RysujTorUkosny(zakret As Zaleznosci.Zakret)
        RysujZakret(zakret)
        If zakret.KontrolaNiezajetosci Then RysujSzczelineZakretu(pedzelSzczelinyWprost)
        If zakret.Zelektryfikowany Then RysujUkosnaLinieElektryfikacji(False, False)
        RysujNazwe(zakret.Nazwa, TEKST_POZ_X, TEKST_POZ_Y)
    End Sub

    Private Sub RysujUkosnaLinieElektryfikacji(rozjLewo As Boolean, rozjPrawo As Boolean)
        Dim x1, y1, x2, y2 As Single

        If rozjLewo Then
            x1 = If(CzyObrocicObiekty(), 1.0F, TOR_ELEKTR_ROZJAZD)
            y1 = 1.0F - TOR_ELEKTR
            x2 = If(CzyObrocicObiekty(), 1.0F - TOR_ELEKTR, TOR_ELEKTR)
            y2 = 1.0F

        ElseIf rozjPrawo Then
            Dim przesun As Boolean = obrot = 0 Or obrot = 3 * KAT_PROSTY
            x1 = 1.0F
            y1 = If(przesun, 1.0F - TOR_ELEKTR, TOR_ELEKTR)
            x2 = 1.0F - TOR_ELEKTR
            y2 = If(przesun, 1.0F, TOR_ELEKTR_ROZJAZD)

        Else
            x1 = 1.0F
            y1 = If(CzyObrocicObiekty(), 1.0F - TOR_ELEKTR, TOR_ELEKTR)
            x2 = y1
            y2 = 1.0F

        End If

        urz.RysujLinie(PEDZEL_KRAWEDZ, KRAWEDZ_SZER, x1, y1, x2, y2)
    End Sub

    Private Sub RysujZakret(zakret As Zaleznosci.IZakret)
        Dim punkty As New List(Of PointF)
        Dim dodatkowePrzesuniecie As Single

        If (zakret.PrzytnijZakret And Zaleznosci.PrzycinanieZakretu.Prawo) <> 0 Then
            dodatkowePrzesuniecie = 0.0F
            If (zakret.PrzytnijZakret And Zaleznosci.PrzycinanieZakretu.UmniejszPrawo) <> 0 Then dodatkowePrzesuniecie = DODATKOWY_MARGINES

            punkty.Add(New PointF(1 - TOR_TROJKAT - dodatkowePrzesuniecie, POL - TOR_SZEROKOSC / 2.0F + dodatkowePrzesuniecie))
            punkty.Add(New PointF(1, POL - TOR_SZEROKOSC / 2.0F + dodatkowePrzesuniecie))
        Else
            punkty.Add(New PointF(1, POL - TOR_SZER_ZAKRET / 2.0F))
        End If

        punkty.Add(New PointF(1, POL + TOR_SZER_ZAKRET / 2.0F))
        punkty.Add(New PointF(POL + TOR_SZER_ZAKRET / 2.0F, 1))

        If (zakret.PrzytnijZakret And Zaleznosci.PrzycinanieZakretu.Dol) <> 0 Then
            dodatkowePrzesuniecie = 0.0F
            If (zakret.PrzytnijZakret And Zaleznosci.PrzycinanieZakretu.UmniejszDol) <> 0 Then dodatkowePrzesuniecie = DODATKOWY_MARGINES

            punkty.Add(New PointF(POL - TOR_SZEROKOSC / 2.0F + dodatkowePrzesuniecie, 1))
            punkty.Add(New PointF(POL - TOR_SZEROKOSC / 2.0F + dodatkowePrzesuniecie, 1 - TOR_TROJKAT - dodatkowePrzesuniecie))
        Else
            punkty.Add(New PointF(POL - TOR_SZER_ZAKRET / 2.0F, 1))
        End If

        urz.WypelnijFigure(pedzelToru, punkty.ToArray)
    End Sub

    Private Sub RysujSzczelineZakretu(pedzel As TPedzel, Optional margines_lewy As Single = SZCZEL_MARG_POZIOM_ZAKRET, Optional margines_prawy As Single = SZCZEL_MARG_POZIOM_ZAKRET)
        If Not rysujSzczeliny Then Exit Sub

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

    Private Sub RysujNazwe(nazwa As String, x As Single, y As Single, Optional wys As Single = TEKST_WYS_LINIA, Optional dodatkowyMarginesSzer As Single = TEKST_POZ_X, Optional przywrocTransformacje As Boolean = False)
        RysujNazwe(nazwa, x, y, wys, dodatkowyMarginesSzer, przywrocTransformacje, CZCIONKA)
    End Sub

    Private Sub RysujNazwe(nazwa As String, x As Single, y As Single, wys As Single, dodatkowyMarginesSzer As Single, przywrocTransformacje As Boolean, uzywanaCzcionka As TCzcionka)
        If String.IsNullOrEmpty(nazwa) Then Exit Sub

        Dim rect As New RectangleF(x, y, 1.0F - x - dodatkowyMarginesSzer, wys)
        Dim rozm As SizeF = urz.ZmierzTekst(uzywanaCzcionka, nazwa, rect.Width, rect.Height)
        Dim zmienionoTransformacje As Boolean = False
        Dim transformacja As TMacierz
        rect.X += (rect.Width - rozm.Width) / 2.0F

        If CzyObrocicObiekty() Then
            Dim x1 As Single = rect.X + rozm.Width / 2.0F
            Dim y1 As Single = rect.Y + rozm.Height / 2.0F
            zmienionoTransformacje = True
            transformacja = urz.TransformacjaPobierz

            urz.TransformacjaResetuj()
            urz.TransformacjaObroc(2 * KAT_PROSTY, x1, y1)
            urz.TransformacjaDolacz(transformacja)
        End If

        urz.RysujTekst(PEDZEL_TEKST, uzywanaCzcionka, nazwa, rect.X, rect.Y, rect.Width, rect.Height)

        If zmienionoTransformacje AndAlso przywrocTransformacje Then
            urz.TransformacjaResetuj()
            urz.TransformacjaDolacz(transformacja)
        End If
    End Sub

    Private Sub RysujNazweDolKostki(nazwa As String)
        RysujNazwe(nazwa, TEKST_POZ_X, 2.0F * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujRozjazdLewo(rozjazd As Zaleznosci.RozjazdLewo)
        Dim dodMargines As Single = TEKST_POZ_X
        RysujZakret(rozjazd)
        RysujTor(rozjazd.RysowanieDodatkowychTrojkatow, 1)
        If rozjazd.KontrolaNiezajetosci Then RysujSzczelineToru()
        If rozjazd.KontrolaNiezajetosciBok Then RysujSzczelineZakretu(pedzelSzczelinyBok, margines_prawy:=SZCZEL_MARG_POZIOM_ROZJ)
        If rozjazd.Zelektryfikowany Then RysujProstaLinieElektryfikacji(If(CzyObrocicObiekty(), TOR_ELEKTR_ROZJAZD, 1.0F))
        If rozjazd.ZelektryfikowanyBok Then RysujUkosnaLinieElektryfikacji(True, False)
        If rozjazd.PosiadaPrzycisk Then
            RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety, PEDZEL_PRZYCISK_CZARNY, 2)
            dodMargines += 2.0F * SYGN_TLO_PROMIEN
        End If
        If rozjazd.KierunekZasadniczy = Zaleznosci.UstawienieRozjazduEnum.Wprost Then
            RysujPlus(ROZJAZD_ZNAK_WPROST_X, ROZJAZD_ZNAK_WPROST_Y)
            RysujMinus(ROZJAZD_ZNAK_BOK_X, ROZJAZD_ZNAK_BOK_Y)
        Else
            RysujPlus(ROZJAZD_ZNAK_BOK_X, ROZJAZD_ZNAK_BOK_Y)
            RysujMinus(ROZJAZD_ZNAK_WPROST_X, ROZJAZD_ZNAK_WPROST_Y)
        End If
        RysujNazwe(rozjazd.Nazwa, TEKST_POZ_X, TEKST_POZ_Y, dodatkowyMarginesSzer:=dodMargines)
    End Sub

    Private Sub RysujRozjazdPrawo(rozjazd As Zaleznosci.RozjazdPrawo)
        Dim dodMargines As Single = TEKST_POZ_X
        RysujTor(rozjazd.RysowanieDodatkowychTrojkatow, 1)
        Dim transformacja As TMacierz = urz.TransformacjaPobierz
        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(3 * KAT_PROSTY, POL, POL)
        urz.TransformacjaDolacz(transformacja)
        RysujZakret(rozjazd)
        If rozjazd.KontrolaNiezajetosciBok Then RysujSzczelineZakretu(pedzelSzczelinyBok, margines_lewy:=SZCZEL_MARG_POZIOM_ROZJ)
        If rozjazd.ZelektryfikowanyBok Then RysujUkosnaLinieElektryfikacji(False, True)
        urz.TransformacjaResetuj()
        urz.TransformacjaDolacz(transformacja)
        If rozjazd.KontrolaNiezajetosci Then RysujSzczelineToru()
        If rozjazd.Zelektryfikowany Then RysujProstaLinieElektryfikacji(If(Not CzyObrocicObiekty(), TOR_ELEKTR_ROZJAZD, 1.0F))
        If rozjazd.PosiadaPrzycisk Then
            RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety, PEDZEL_PRZYCISK_CZARNY, 2, 2)
            dodMargines += 2.0F * SYGN_TLO_PROMIEN
        End If
        If rozjazd.KierunekZasadniczy = Zaleznosci.UstawienieRozjazduEnum.Wprost Then
            RysujPlus(ROZJAZD_ZNAK_WPROST_X, 1.0F - ROZJAZD_ZNAK_WPROST_Y)
            RysujMinus(ROZJAZD_ZNAK_BOK_X, 1.0F - ROZJAZD_ZNAK_BOK_Y)
        Else
            RysujPlus(ROZJAZD_ZNAK_BOK_X, 1.0F - ROZJAZD_ZNAK_BOK_Y)
            RysujMinus(ROZJAZD_ZNAK_WPROST_X, 1.0F - ROZJAZD_ZNAK_WPROST_Y)
        End If
        RysujNazwe(rozjazd.Nazwa, TEKST_POZ_X, 2.0F * SYGN_POZ + TEKST_POZ_Y, dodatkowyMarginesSzer:=dodMargines)
    End Sub

    Private Sub RysujPlus(x As Single, y As Single)
        urz.RysujLinie(PEDZEL_ROZJAZD_ZNAK, ROZJAZD_ZNAK_SZER, x - ROZJAZD_ZNAK_POL_DL, y, x + ROZJAZD_ZNAK_POL_DL, y)
        urz.RysujLinie(PEDZEL_ROZJAZD_ZNAK, ROZJAZD_ZNAK_SZER, x, y - ROZJAZD_ZNAK_POL_DL, x, y + ROZJAZD_ZNAK_POL_DL)
    End Sub

    Private Sub RysujMinus(x As Single, y As Single)
        urz.RysujLinie(PEDZEL_ROZJAZD_ZNAK, ROZJAZD_ZNAK_SZER, x - ROZJAZD_ZNAK_POL_DL, y, x + ROZJAZD_ZNAK_POL_DL, y)
    End Sub

    Private Sub RysujSygnalizatorManewrowy(sygnalizator As Zaleznosci.SygnalizatorManewrowy)
        Dim pedzNiebieski As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.BrakWyjazdu, PEDZEL_SYGN_NIEB_JASNY, PEDZEL_SYGN_NIEB)
        Dim pedzBialy As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.Manewrowy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzNiebieski, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzBialy, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(2)
        If sygnalizator.PosiadaPrzycisk Then RysujPrzycisk((Not trybProjektowy) And sygnalizator.Wcisniety, PEDZEL_PRZYCISK_BIALY, 2.0F, dodatekX:=PRZYCISK_TM_DODATEK)
        RysujTorProsty(sygnalizator, True)
    End Sub

    Private Sub RysujSygnalizatorPowtarzajacy(sygnalizator As Zaleznosci.SygnalizatorPowtarzajacy)
        Dim pedzPomc As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraPowtarzajacego.BrakWyjazdu, PEDZEL_SYGN_POMC_JASNY, PEDZEL_SYGN_POMC)
        Dim pedzZiel As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraPowtarzajacego.Zezwalajacy, PEDZEL_SYGN_ZIEL_JASNY, PEDZEL_SYGN_ZIEL)

        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzPomc, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzZiel, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(2)
        RysujTorProsty(sygnalizator, False)
        RysujNazweDolKostki(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorPolsamoczynny(sygnalizator As Zaleznosci.SygnalizatorPolsamoczynny)
        Dim pedzCzer As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)
        Dim pedzZiel As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zezwalajacy, PEDZEL_SYGN_ZIEL_JASNY, PEDZEL_SYGN_ZIEL)
        Dim pedzBial As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Manewrowy Or (sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy And wysokiStanMigania), PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 3 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzCzer, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzZiel, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzBial, 3 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(3)
        RysujTorProsty(sygnalizator, True)
    End Sub

    Private Sub RysujSygnalizatorSamoczynny(sygnalizator As Zaleznosci.SygnalizatorSamoczynny)
        Dim pedz As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.BrakWyjazdu, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)

        urz.WypelnijKolo(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedz, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(1)
        RysujTorProsty(sygnalizator, True)
    End Sub

    Private Sub RysujSygnalizatorTOP(sygnalizator As Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy)
        Dim wyrozniony As Boolean = sygnalizator Is sygnTopWyrozniony
        Dim pedzTlo As TPedzel = If(wyrozniony, PEDZEL_PRZEJAZD_SYGN_TOP, PEDZEL_SYGN_TLO)

        urz.WypelnijTloSygnalizatora(pedzTlo, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)

        If Not wyrozniony Then
            Dim pedzBial As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraOstrzegawczegoPrzejazdowego.PrzejazdZamkniety, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)
            Dim pedzPomc As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraOstrzegawczegoPrzejazdowego.PrzejazdUszkodzony, PEDZEL_SYGN_POMC_JASNY, PEDZEL_SYGN_POMC)

            urz.WypelnijKolo(pedzBial, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
            urz.WypelnijKolo(pedzPomc, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        End If

        RysujSlupSygnalizatora(2)
        RysujTorProsty(sygnalizator, True)
    End Sub

    Private Sub RysujPrzejazd(przejazd As Zaleznosci.PrzejazdKolejowoDrogowyKostka)
        Dim pedzAwaria As TPedzel = If(trybProjektowy Or przejazd.Awaria, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)
        Dim pedzStan As TPedzel = If(
                trybProjektowy Or
                przejazd.Stan = Zaleznosci.StanPrzejazduKolejowego.Zamkniety Or
                ((przejazd.Stan = Zaleznosci.StanPrzejazduKolejowego.Otwierany Or przejazd.Stan = Zaleznosci.StanPrzejazduKolejowego.Zamykany) And wysokiStanMigania),
            PEDZEL_SYGN_BIAL_JASNY,
            PEDZEL_SYGN_BIAL)

        urz.RysujLinie(PEDZEL_KRAWEDZ, PRZEJAZD_SZER_LINII, PRZEJAZD_POZ, 0.0F, PRZEJAZD_POZ, 1.0F)
        urz.RysujLinie(PEDZEL_KRAWEDZ, PRZEJAZD_SZER_LINII, 1.0F - PRZEJAZD_POZ, 0.0F, 1.0F - PRZEJAZD_POZ, 1.0F)
        RysujTorProsty(przejazd, False)
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, PRZEJAZD_KONTR_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzAwaria, PRZEJAZD_KONTR_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, PRZEJAZD_KONTR_POZ, 3 * SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzStan, PRZEJAZD_KONTR_POZ, 3 * SYGN_POZ, SYGN_PROMIEN)
    End Sub

    Private Sub RysujPrzycisk(wcisniety As Boolean, pedzel As TPedzel, Optional poczx As Single = 0.0F, Optional poczy As Single = 0.0F, Optional dodatekX As Single = 0.0F)
        Dim x As Single = (poczx + 1.0F) * SYGN_POZ + dodatekX
        Dim y As Single = (poczy + 1.0F) * SYGN_POZ

        urz.WypelnijKolo(pedzel, x, y, PRZYCISK_TLO_PROMIEN)
        urz.WypelnijKolo(PEDZEL_PRZYCISK_RAMKA, x, y, PRZYCISK_RAMKA_PROMIEN)
        urz.WypelnijKolo(If(wcisniety, PEDZEL_PRZYCISK_WCISNIETY, pedzel), x, y, PRZYCISK_PROMIEN)
    End Sub

    Private Sub RysujPrzyciskZwykly(przycisk As Zaleznosci.Przycisk)
        Dim pedzel As TPedzel = PEDZEL_PRZYCISK_CZARNY
        Dim nazwaPrzycisku As String = Nothing
        Dim nazwaElementu As String = Nothing

        Select Case przycisk.TypPrzycisku
            Case Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy
                nazwaPrzycisku = NAZWA_SZ
                nazwaElementu = przycisk.SygnalizatorPolsamoczynny?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegu
                nazwaPrzycisku = NAZWA_Z
                nazwaElementu = przycisk.SygnalizatorPolsamoczynny?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego
                nazwaPrzycisku = NAZWA_Z & NAZWA_M
                nazwaElementu = przycisk.SygnalizatorPolsamoczynny?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebieguManewrowegoZSygnManewrowego
                nazwaPrzycisku = NAZWA_Z & NAZWA_M
                nazwaElementu = przycisk.SygnalizatorManewrowy?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.WlaczenieSBL
                pedzel = PEDZEL_PRZYCISK_CZERWONY
                nazwaPrzycisku = NAZWA_WBL
                nazwaElementu = przycisk.Kierunek?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.PotwierdzenieSBL
                pedzel = PEDZEL_PRZYCISK_CZERWONY
                nazwaPrzycisku = NAZWA_PZK
                nazwaElementu = przycisk.Kierunek?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.ZwolnienieSBL
                pedzel = PEDZEL_PRZYCISK_CZERWONY
                nazwaPrzycisku = NAZWA_ZWBL
                nazwaElementu = przycisk.Kierunek?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.KasowanieRozprucia
                nazwaPrzycisku = NAZWA_KR
                nazwaElementu = przycisk.Rozjazd?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.ZamknieciePrzejazdu
                pedzel = PEDZEL_PRZYCISK_ZOLTY
                nazwaPrzycisku = NAZWA_ZAM
                nazwaElementu = przycisk.Przejazd?.Nazwa

            Case Zaleznosci.TypPrzyciskuEnum.OtwarciePrzejazdu
                pedzel = PEDZEL_PRZYCISK_ZOLTY
                nazwaPrzycisku = NAZWA_OTW
                nazwaElementu = przycisk.Przejazd?.Nazwa

        End Select

        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety, pedzel)
        RysujNazwe(nazwaPrzycisku, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y, przywrocTransformacje:=True)
        RysujNazweDolKostki(nazwaElementu)
    End Sub

    Private Sub RysujPrzyciskTor(przycisk As Zaleznosci.PrzyciskTor)
        Dim pedzel As TPedzel

        If przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.JazdaSygnalizatorPolsamoczynny Then
            pedzel = PEDZEL_PRZYCISK_ZIELONY
        Else
            RysujNazwe(NAZWA_M, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y, przywrocTransformacje:=True)
            pedzel = PEDZEL_PRZYCISK_BIALY
        End If

        RysujTorProsty(przycisk, False)
        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety, pedzel)

        Dim nazwa As String
        If przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.ManewrySygnalizatorManewrowy Then
            nazwa = przycisk.SygnalizatorManewrowy?.Nazwa
        Else
            nazwa = przycisk.SygnalizatorPolsamoczynny?.Nazwa
        End If

        RysujNazweDolKostki(nazwa)
    End Sub

    Private Sub RysujKierunek(kier As Zaleznosci.Kierunek)
        RysujNazwe(kier.Nazwa, TEKST_POZ_X, TEKST_POZ_Y, przywrocTransformacje:=True)
        RysujTorProsty(kier, False)
        RysujTrojkatKierunku(kier, Zaleznosci.KierunekWyjazduSBL.Lewo, Zaleznosci.UstawionyKierunekSBL.Lewo)

        Dim tr As TMacierz = urz.TransformacjaPobierz
        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(2 * KAT_PROSTY, POL, KIER_POZ_Y)
        urz.TransformacjaDolacz(tr)

        RysujTrojkatKierunku(kier, Zaleznosci.KierunekWyjazduSBL.Prawo, Zaleznosci.UstawionyKierunekSBL.Prawo)
    End Sub

    Private Sub RysujKostkeNapis(napis As Zaleznosci.Napis)
        If String.IsNullOrWhiteSpace(napis.Tekst) Then urz.WypelnijKolo(PEDZEL_KOLKO_TEKST, KOLKO_TEKST_POZ, KOLKO_TEKST_POZ, KOLKO_TEKST_PROMIEN)
        Dim czcionka As TCzcionka = urz.UtworzCzcionke(NAZWA_CZCIONKI, napis.Rozmiar, True)
        RysujNazwe(napis.Tekst, TEKST_NAPIS_POZ, TEKST_NAPIS_POZ, TEKST_WYS_KOSTKA_NAPIS, TEKST_POZ_X, False, czcionka)
    End Sub

    Private Sub RysujSlupSygnalizatora(poczx As Single)
        Dim x1 As Single = poczx * SYGN_POZ + SYGN_TLO_PROMIEN
        Dim x2 As Single = poczx * SYGN_POZ + SYGN_TLO_PROMIEN + SYGN_SLUP_DLUG
        Dim x3 As Single = poczx * SYGN_POZ + SYGN_TLO_PROMIEN + 2 * SYGN_SLUP_DLUG

        Dim y1 As Single = SYGN_POZ - SYGN_SLUP_SZER_DUZA / 2
        Dim y2 As Single = SYGN_POZ - SYGN_SLUP_SZER_MALA / 2
        Dim y3 As Single = SYGN_POZ + SYGN_SLUP_SZER_MALA / 2
        Dim y4 As Single = SYGN_POZ + SYGN_SLUP_SZER_DUZA / 2

        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x1, y2, x2, y2)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x2, y2, x2, y1)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x2, y1, x3, y1)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x3, y1, x3, y4)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x3, y4, x2, y4)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x2, y4, x2, y3)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x2, y3, x1, y3)
        urz.RysujLinie(PEDZEL_KRAWEDZ, SYGN_KRAWEDZ, x1, y3, x1, y2)
    End Sub

    Private Sub RysujTrojkatKierunku(kier As Zaleznosci.Kierunek, kierProj As Zaleznosci.KierunekWyjazduSBL, kierDzialanie As Zaleznosci.UstawionyKierunekSBL)
        urz.WypelnijFigure(PEDZEL_TOR_WOLNY, PUNKTY_KIERUNKU)

        Dim pedzel As TPedzel
        If trybProjektowy Then
            pedzel = If(kier.KierunekWyjazdu = kierProj, PEDZEL_SZCZELINA_UTWIERDZONY, PEDZEL_SZCZELINA_WOLNY)
        Else
            pedzel = If(kier.UstawionyKierunek = kierDzialanie Or (kier.UstawionyStanZmiany <> Zaleznosci.UstawionyStanZmianyKierunkuSBL.Zaden And wysokiStanMigania), PEDZEL_SZCZELINA_UTWIERDZONY, PEDZEL_SZCZELINA_WOLNY)
        End If

        urz.WypelnijFigure(pedzel, PUNKTY_SZCZELINY_KIERUNKU)
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

    Private Function CzyObrocicObiekty() As Boolean
        Return obrot >= 2 * KAT_PROSTY And obrot < 4 * KAT_PROSTY
    End Function

    Private Sub UstawKolorToru(k As Zaleznosci.Kostka, zazn As Zaleznosci.OdcinekToru)
        pedzelToru = PEDZEL_TOR_WOLNY
        Dim t As Zaleznosci.Tor = TryCast(k, Zaleznosci.Tor)

        If t IsNot Nothing Then
            If zazn IsNot Nothing AndAlso t.NalezyDoOdcinka Is zazn Then
                pedzelToru = PEDZEL_TOR_TEN_ODCINEK
            ElseIf t.NalezyDoOdcinka Is Nothing Then
                pedzelToru = PEDZEL_TOR_NIEPRZYPISANY
            End If
        End If
    End Sub

    Private Sub UstawKolorToruDlaLicznika(k As Zaleznosci.Kostka, zazn As Zaleznosci.ParaLicznikowOsi)
        pedzelToru = PEDZEL_TOR_WOLNY
        Dim t As Zaleznosci.Tor = TryCast(k, Zaleznosci.Tor)

        If zazn IsNot Nothing AndAlso t IsNot Nothing Then
            If t.NalezyDoOdcinka IsNot Nothing Then
                If t.NalezyDoOdcinka Is zazn.Odcinek1 Then
                    pedzelToru = PEDZEL_TOR_TEN_ODCINEK
                ElseIf t.NalezyDoOdcinka Is zazn.Odcinek2 Then
                    pedzelToru = PEDZEL_TOR_LICZNIK_ODCINEK_2
                End If
            End If
        End If
    End Sub

    Private Sub UstawKolorToruDlaPrzejazdu(k As Zaleznosci.Kostka, zazn As Zaleznosci.PrzejazdKolejowoDrogowy)
        pedzelToru = PEDZEL_TOR_WOLNY
        Dim p As Zaleznosci.PrzejazdKolejowoDrogowyKostka = TryCast(k, Zaleznosci.PrzejazdKolejowoDrogowyKostka)

        If p IsNot Nothing Then
            If zazn IsNot Nothing AndAlso p.NalezyDoPrzejazdu Is zazn Then
                pedzelToru = PEDZEL_TOR_TEN_ODCINEK
            ElseIf p.NalezyDoPrzejazdu Is Nothing Then
                pedzelToru = PEDZEL_TOR_NIEPRZYPISANY
            End If
        End If
    End Sub

    Private Sub UstawKolorToruDlaPrzejazduAutomatyzacja(k As Zaleznosci.Kostka, zazn As Zaleznosci.PrzejazdAutomatyczneZamykanie)
        pedzelToru = PEDZEL_TOR_WOLNY
        Dim t As Zaleznosci.Tor = TryCast(k, Zaleznosci.Tor)

        If zazn IsNot Nothing AndAlso t IsNot Nothing Then
            If t.NalezyDoOdcinka IsNot Nothing Then
                If t.NalezyDoOdcinka Is zazn.OdcinekWyjazd Then
                    pedzelToru = PEDZEL_TOR_TEN_ODCINEK
                ElseIf t.NalezyDoOdcinka Is zazn.OdcinekPrzyjazd Then
                    pedzelToru = PEDZEL_TOR_LICZNIK_ODCINEK_2
                End If
            End If
        End If
    End Sub

    Private Sub UstawKolorSzczeliny(k As Zaleznosci.Kostka)
        If Not rysujSzczeliny Then Exit Sub

        pedzelSzczelinyWprost = PEDZEL_SZCZELINA_WOLNY
        pedzelSzczelinyBok = PEDZEL_SZCZELINA_WOLNY

        Dim pedzelUstawiony As Boolean = False
        Dim tor As Zaleznosci.Tor = TryCast(k, Zaleznosci.Tor)

        If tor IsNot Nothing Then
            Dim roz As Zaleznosci.Rozjazd = TryCast(k, Zaleznosci.Rozjazd)
            pedzelSzczelinyWprost = PobierzPedzelToruNiezwolnionego(tor.Zajetosc, pedzelUstawiony)

            If roz IsNot Nothing Then
                If Not pedzelUstawiony Then
                    If roz.Rozprucie And wysokiStanMigania Then
                        pedzelSzczelinyWprost = PEDZEL_SZCZELINA_ROZPRUCIE
                    ElseIf roz.Stan = Zaleznosci.StanRozjazdu.Wprost Then
                        pedzelSzczelinyWprost = PEDZEL_SZCZELINA_ZWROTNICA
                    End If
                End If

                pedzelSzczelinyBok = PobierzPedzelToruNiezwolnionego(roz.ZajetoscBok, pedzelUstawiony)
                If Not pedzelUstawiony Then
                    If roz.Rozprucie And wysokiStanMigania Then
                        pedzelSzczelinyBok = PEDZEL_SZCZELINA_ROZPRUCIE
                    ElseIf roz.Stan = Zaleznosci.StanRozjazdu.Bok Then
                        pedzelSzczelinyBok = PEDZEL_SZCZELINA_ZWROTNICA
                    End If
                End If
            End If
        End If
    End Sub

    Private Function PobierzPedzelToruNiezwolnionego(zajetosc As Zaleznosci.ZajetoscToru, ByRef pedzelUstawiony As Boolean) As TPedzel
        pedzelUstawiony = True

        If zajetosc = Zaleznosci.ZajetoscToru.Zajety Then
            Return PEDZEL_SZCZELINA_ZAJETY
        ElseIf zajetosc = Zaleznosci.ZajetoscToru.PrzebiegUtwierdzony Or (zajetosc = Zaleznosci.ZajetoscToru.BlokadaNieustawiona And wysokiStanMigania) Then
            Return PEDZEL_SZCZELINA_UTWIERDZONY
        End If

        pedzelUstawiony = False
        Return PEDZEL_SZCZELINA_WOLNY
    End Function

    Private Function ObliczWspolrzedneKierunku() As PointF()
        Dim A As New PointF(KIER_POZ_X + KIER_SZER / 2, KIER_POZ_Y - KIER_SZER / 2)
        Dim B As New PointF(KIER_POZ_X - KIER_SZER / 2, KIER_POZ_Y)
        Dim C As New PointF(KIER_POZ_X + KIER_SZER / 2, KIER_POZ_Y + KIER_SZER / 2)

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