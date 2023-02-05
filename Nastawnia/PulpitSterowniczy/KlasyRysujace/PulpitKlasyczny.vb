Friend MustInherit Class PulpitKlasyczny(Of TOlowek, TPedzel, TMacierz, TCzcionka)
    Implements IRysownik

    Private Const COS45 As Single = 0.707F
    Private Const KAT_PROSTY As Integer = 90
    Private Const POL As Single = 0.5F
    Private Const KRAWEDZ_SZER As Single = 0.02F         'grubość krawędzi między kostkami
    Private Const KOLKO_PROMIEN As Single = 0.125F       'promień kółka (lampy/licznika osi)
    Private Const KOLKO_TEKST_PROMIEN As Single = 0.06F  'promień kółka obok tekstu
    Private Const KOLKO_TEKST_POZ As Single = 0.1F       'położenie kółka obok tekstu
    Private Const TOR_SZEROKOSC As Single = 0.16F        'szerokość toru na kostce
    Private Const TOR_SZER_ZAKRET As Single = TOR_SZEROKOSC / COS45
    Private Const TOR_TROJKAT As Single = (TOR_SZER_ZAKRET - TOR_SZEROKOSC) / 2.0F  'długość przyprostokątnej trójkąta do ścięcia/narysowania przy granicy toru ukośnego i prostego
    Private Const TOR_KONC_DLUGOSC As Single = 0.27F     'długość toru na kostce z końcem
    Private Const TOR_KONC_SZEROKOSC As Single = 0.5F    'szerokość odcinka prostopadłego na kostce z końcem
    Private Const SZCZELINA_MARGINES_POZIOM As Single = 0.1F    'margines szczeliny w poziomie
    Private Const SZCZEL_MARG_POZIOM_ZAKRET As Single = 0.05F   'margines szczeliny w poziomie toru ukośnego
    Private Const SZCZEL_MARG_POZIOM_ROZJ As Single = 0.08F     'margines szczeliny w poziomie toru bocznego rozjazdu, na końcu przylegającym do toru prostego
    Private Const SZCZELINA_MARGINES_PION As Single = 0.04F     'margines szczeliny w pionie
    Private Const SZCZELINA_MARGINES_KIER As Single = 0.04F     'margines szczeliny na kostce z kierunkiem
    Private Const ROZJAZD_ZNAK_SZER As Single = 0.01F    'szerokość linii plusa/minusa obok rozjazdu
    Private Const ROZJAZD_ZNAK_POL_DL As Single = 0.05F  'połowa długości linii plusa/minusa obok rozjazdu
    Private Const ROZJAZD_PLUS_X As Single = 0.12F       'współrzędna X środka plusa obok rozjazdu
    Private Const ROZJAZD_PLUS_Y As Single = 0.7F        'współrzędna Y środka plusa obok rozjazdu
    Private Const ROZJAZD_MINUS_X As Single = 0.85F      'współrzędna X środka minusa obok rozjazdu
    Private Const ROZJAZD_MINUS_Y As Single = 0.9F       'współrzędna Y środka minusa obok rozjazdu
    Private Const SYGN_POZ As Single = 0.25F             'wielokorotność stałej oznacza położenie środków kolejnych świateł sygnałów na osi X
    Private Const SYGN_PROMIEN As Single = 0.09F         'promień sygnału
    Private Const SYGN_TLO_PROMIEN As Single = 0.14F     'promień okręgu stanowiącego tło sygnału
    Private Const SYGN_SLUP_SZER_DUZA As Single = 0.15F  'szerokość słupa sygnalizatora w szerszym miejscu
    Private Const SYGN_SLUP_SZER_MALA As Single = 0.05F  'szerokosć słupa sygnalizatora w węższym miejscu
    Private Const SYGN_SLUP_DLUG As Single = 0.04F       'długość poszczególnych segmentów słupa
    Private Const SYGN_KRAWEDZ As Single = 0.01F         'grubość krawędzi słupa sygnalizatora
    Private Const KIER_SZER As Single = 0.3F             'rozmiar trójkąta na kostce kierunku
    Private Const KIER_POZ_X As Single = 0.25F           'pozycja trójkąta kierunku na osi X
    Private Const KIER_POZ_Y As Single = 0.78F           'pozycja trójkąta kierunku na osi Y
    Private Const PRZEJAZD_POZ As Single = 0.35F         'odległość linii przejazdu kolejowego od krawędzi bocznych
    Private Const PRZEJAZD_SZER_LINII As Single = 0.02F  'szerokość linii przejazdu kolejowego
    Private Const PRZEJAZD_KONTR_POZ As Single = 0.17F   'pozycja na osi X kontrolek przejazdu kolejowego
    Private Const TEKST_POZ_X_PRZYCISK As Single = 0.17F 'dodatkowy margines dla tekstu obok przycisku
    Private Const TEKST_POZ_X As Single = 0.1F           'dodatkowy margines dla tekstu
    Private Const TEKST_POZ_Y As Single = 0.12F          'dodatkowy margines dla tekstu
    Private Const TEKST_NAPIS_POZ As Single = 0.12F      'pozycja tekstu w kostce z napisem
    Private Const TEKST_WYS As Single = 0.78F            'wysokość tekstu w kostce z napisem
    Private Const KRAWEDZ_RAMKA_ZAZN As Single = 0.04F   'grubość krawędzi ramki zaznaczenia lamp
    Private Const DODATKOWY_MARGINES As Single = 0.01F   'margines uwzględniany w elementach, aby te lekko nachodziły na siebie i nie rysowały się przerwy między nimi

    Private ReadOnly KOLOR_TOR_PRZYPISANY As Color = KolorRGB("#8C8C8C")          'tor przypisany do innego odcinka
    Private ReadOnly KOLOR_TOR_TEN_ODCINEK As Color = KolorRGB("#25FF1A")         'tor przypisany do zaznaczonego odcinka
    Private ReadOnly KOLOR_TOR_NIEPRZYPISANY As Color = KolorRGB("#FF1A1A")       'tor nieprzypisany do żadnego odcinka
    Private ReadOnly KOLOR_TOR_LICZNIK_ODCINEK_2 As Color = KolorRGB("#D11AFF")   'drugi odcinek obsługiwany przez parę liczników osi
    Private ReadOnly KOLOR_TLO_SYGNALIZATOR_TOP As Color = KolorRGB("#FF9900")    'tło sygnalizatora ostrzegawczego przejazdowego, który jest przypisany do zaznaczonego obiektu automatyzacji przejazdu
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
    Private PEDZEL_PRZEJAZD_SYGN_TOP As TPedzel
    Private PEDZEL_PRZEJAZD_ROGATKA As TPedzel
    Private PEDZEL_PRZEJAZD_ROGATKA_ZAZN As TPedzel
    Private PEDZEL_PRZEJAZD_SYGN_DROG As TPedzel
    Private PEDZEL_PRZEJAZD_SYGN_DROG_ZAZN As TPedzel

    Private ReadOnly PUNKTY_KIERUNKU As PointF()
    Private ReadOnly PUNKTY_SZCZELINY_KIERUNKU As PointF()

    Private CZCIONKA As TCzcionka

    Private Const NAZWA_SP As String = "Sp"     'Sygnalizator powtarzający
    Private Const NAZWA_SZ As String = "Sz"     'Sygnał zastępczy
    Private Const NAZWA_ZW As String = "Zw"     'Zwolnienie przebiegów
    Private Const NAZWA_M As String = "m"       'Sygnał manewrowy
    Private Const NAZWA_WBL As String = "Wbl"   'Włączenie blokady

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
        PEDZEL_PRZEJAZD_SYGN_TOP = urz.UtworzPedzel(KOLOR_TLO_SYGNALIZATOR_TOP)
        PEDZEL_PRZEJAZD_ROGATKA = urz.UtworzPedzel(KolorRGB("#00BBFF"))
        PEDZEL_PRZEJAZD_ROGATKA_ZAZN = urz.UtworzPedzel(KolorRGB("#000DFF"))
        PEDZEL_PRZEJAZD_SYGN_DROG = urz.UtworzPedzel(KolorRGB("#FF66D1"))
        PEDZEL_PRZEJAZD_SYGN_DROG_ZAZN = urz.UtworzPedzel(KolorRGB("#CC0047"))
        CZCIONKA = urz.UtworzCzcionke("Arial", 0.17)

        zainicjalizowano = True
    End Sub

    Public Sub ZmienRozmiar(szer As UInteger, wys As UInteger) Implements IRysownik.ZmienRozmiar
        urz.ZmienRozmiar(szer, wys)
    End Sub

    Friend Overridable Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics) Implements IRysownik.Rysuj
        If Not zainicjalizowano Then Exit Sub

        pulpit = ps.Pulpit
        sygnTopWyrozniony = If(ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyAutomatyzacja, ps.projZaznaczonyPrzejazdAutomatyzacja?.Sygnalizator, Nothing)
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
                urz.RysujLinie(PEDZEL_KRAWEDZIE, KRAWEDZ_SZER, x, 0, x, ps.Pulpit.Wysokosc)
            Next

            For y As Integer = 0 To ps.Pulpit.Wysokosc
                urz.RysujLinie(PEDZEL_KRAWEDZIE, KRAWEDZ_SZER, 0, y, ps.Pulpit.Szerokosc, y)
            Next
        End If

        'Rysuj kostki
        If UniewaznioneSasiedztwoTorow Then
            WyznaczWygladzanieZakretow(pulpit)
            UniewaznioneSasiedztwoTorow = False
        End If

        ps.Pulpit.PrzeiterujKostki(AddressOf PrzetworzKostke, ps)

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
            For Each rogatka As Zaleznosci.ElementWykonaczyPrzejazduKolejowego In ps.projZaznaczonyPrzejazd.Rogatki
                pedzel = If(rogatka Is ps.projZaznaczonyPrzejazdRogatka, PEDZEL_PRZEJAZD_ROGATKA_ZAZN, PEDZEL_PRZEJAZD_ROGATKA)
                RysujKolko(pedzel, rogatka.X, rogatka.Y)
            Next
        End If

        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdySygnDrog AndAlso ps.projZaznaczonyPrzejazd IsNot Nothing Then
            Dim pedzel As TPedzel
            For Each sygnDrog As Zaleznosci.ElementWykonaczyPrzejazduKolejowego In ps.projZaznaczonyPrzejazd.SygnalizatoryDrogowe
                pedzel = If(sygnDrog Is ps.projZaznaczonyPrzejazdSygnDrog, PEDZEL_PRZEJAZD_SYGN_DROG_ZAZN, PEDZEL_PRZEJAZD_SYGN_DROG)
                RysujKolko(pedzel, sygnDrog.X, sygnDrog.Y)
            Next
        End If

        If Not ps.TrybProjektowy AndAlso ps.MozliwoscZaznaczeniaLamp AndAlso Not ps.PoczatekZaznaczeniaLamp.IsEmpty AndAlso Not ps.KoniecZaznaczeniaLamp.IsEmpty Then
            RysujZaznaczenieLamp(ps.PoczatekZaznaczeniaLamp, ps.KoniecZaznaczeniaLamp)
        End If

        urz.ZakonczRysunek()
    End Sub

    Private Sub PrzetworzKostke(x As Integer, y As Integer, k As Zaleznosci.Kostka, ps As PulpitSterowniczy)
        UstawKolorSzczeliny(k)
        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.OdcinkiTorow Then UstawKolorToru(k, ps.projZaznaczonyOdcinek)
        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Liczniki Then UstawKolorToruDlaLicznika(k, ps.projZaznaczonyLicznik)
        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Przejazdy Then UstawKolorToruDlaPrzejazdu(k, ps.projZaznaczonyPrzejazd)
        If ps.projDodatkoweObiekty = RysujDodatkoweObiekty.PrzejazdyAutomatyzacja Then UstawKolorToruDlaPrzejazduAutomatyzacja(k, ps.projZaznaczonyPrzejazdAutomatyzacja)
        Dim zazn As Boolean = k Is ps.ZaznaczonaKostka AndAlso ps.projDodatkoweObiekty = RysujDodatkoweObiekty.Nic AndAlso (ps.TrybProjektowy Or ps.MozliwoscZaznaczeniaToru)
        RysujKostke(x, y, k, zazn)
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
                RysujTorProsty(CType(kostka, Zaleznosci.Tor).RysowanieDodatkowychTrojkatow)
            Case Zaleznosci.TypKostki.TorKoniec
                RysujKoniecToru(CType(kostka, Zaleznosci.TorKoniec).RysowanieDodatkowychTrojkatow)
            Case Zaleznosci.TypKostki.Zakret
                RysujTorUkosny(CType(kostka, Zaleznosci.IZakret))
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
                RysujPrzejazd(CType(kostka, Zaleznosci.PrzejazdKolejowy))
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

    Private Sub RysujTorProsty(dodatkoweTrojkaty As Zaleznosci.DodatkoweTrojkatyTor, Optional dlugosc As Single = 1.0F)
        RysujTor(dodatkoweTrojkaty, dlugosc)
        RysujSzczelineToru(dlugosc)
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

    Private Sub RysujSzczelineToru(dlugosc As Single)
        If Not rysujSzczeliny Then Exit Sub

        urz.WypelnijProstokat(pedzelSzczelinyWprost, SZCZELINA_MARGINES_POZIOM, POL - TOR_SZEROKOSC / 2 + SZCZELINA_MARGINES_PION, dlugosc - 2 * SZCZELINA_MARGINES_POZIOM, TOR_SZEROKOSC - 2 * SZCZELINA_MARGINES_PION)
    End Sub

    Private Sub RysujKoniecToru(dodatkoweTrojkaty As Zaleznosci.DodatkoweTrojkatyTorKoniec)
        RysujTor(CType(dodatkoweTrojkaty, Zaleznosci.DodatkoweTrojkatyTor), TOR_KONC_DLUGOSC)
        urz.WypelnijProstokat(pedzelToru, TOR_KONC_DLUGOSC - DODATKOWY_MARGINES, POL - TOR_KONC_SZEROKOSC / 2, TOR_SZEROKOSC, TOR_KONC_SZEROKOSC)
    End Sub

    Private Sub RysujTorUkosny(zakret As Zaleznosci.IZakret)
        RysujZakret(zakret)
        RysujSzczelineZakretu(pedzelSzczelinyWprost)
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

    Private Sub RysujNazwe(nazwa As String, x As Single, y As Single, Optional wys As Single = SYGN_POZ, Optional dodatkowyMarginesSzer As Single = TEKST_POZ_X, Optional przywrocTransformacje As Boolean = False)
        Dim rect As New RectangleF(x, y, 1 - x - dodatkowyMarginesSzer, wys)
        Dim zmienionoTransformacje As Boolean = False
        Dim transformacja As TMacierz

        If obrot >= 2 * KAT_PROSTY And obrot < 4 * KAT_PROSTY Then
            Dim rozm As SizeF = urz.ZmierzTekst(CZCIONKA, nazwa, rect.Width, rect.Height)
            Dim x1 As Single = rect.X + rozm.Width / 2
            Dim y1 As Single = rect.Y + rozm.Height / 2
            zmienionoTransformacje = True
            transformacja = urz.TransformacjaPobierz

            urz.TransformacjaResetuj()
            urz.TransformacjaObroc(2 * KAT_PROSTY, x1, y1)
            urz.TransformacjaDolacz(transformacja)
        End If

        urz.RysujTekst(PEDZEL_TEKST, CZCIONKA, nazwa, rect.X, rect.Y, rect.Width, rect.Height)

        If zmienionoTransformacje AndAlso przywrocTransformacje Then
            urz.TransformacjaResetuj()
            urz.TransformacjaDolacz(transformacja)
        End If
    End Sub

    Private Sub RysujRozjazdLewo(rozjazd As Zaleznosci.RozjazdLewo)
        RysujZakret(rozjazd)
        RysujTor(rozjazd.RysowanieDodatkowychTrojkatow, 1)
        RysujSzczelineZakretu(pedzelSzczelinyBok, margines_prawy:=SZCZEL_MARG_POZIOM_ROZJ)
        RysujSzczelineToru(1)
        RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety, 2)
        RysujPlus(ROZJAZD_PLUS_Y)
        RysujMinus(ROZJAZD_MINUS_Y)
        RysujNazwe(rozjazd.Nazwa, TEKST_POZ_X, TEKST_POZ_Y, dodatkowyMarginesSzer:=2.0F * SYGN_TLO_PROMIEN + TEKST_POZ_X)
    End Sub

    Private Sub RysujRozjazdPrawo(rozjazd As Zaleznosci.RozjazdPrawo)
        RysujTor(rozjazd.RysowanieDodatkowychTrojkatow, 1)
        Dim transformacja As TMacierz = urz.TransformacjaPobierz
        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(3 * KAT_PROSTY, POL, POL)
        urz.TransformacjaDolacz(transformacja)
        RysujZakret(rozjazd)
        RysujSzczelineZakretu(pedzelSzczelinyBok, margines_lewy:=SZCZEL_MARG_POZIOM_ROZJ)
        urz.TransformacjaResetuj()
        urz.TransformacjaDolacz(transformacja)
        RysujSzczelineToru(1)
        RysujPrzycisk((Not trybProjektowy) And rozjazd.Wcisniety, 2, 2)
        RysujPlus(1.0F - ROZJAZD_PLUS_Y)
        RysujMinus(1.0F - ROZJAZD_MINUS_Y)
        RysujNazwe(rozjazd.Nazwa, TEKST_POZ_X, 2.0F * SYGN_POZ + TEKST_POZ_Y, dodatkowyMarginesSzer:=2.0F * SYGN_TLO_PROMIEN + TEKST_POZ_X)
    End Sub

    Private Sub RysujPlus(y As Single)
        urz.RysujLinie(PEDZEL_ROZJAZD_ZNAK, ROZJAZD_ZNAK_SZER, ROZJAZD_PLUS_X - ROZJAZD_ZNAK_POL_DL, y, ROZJAZD_PLUS_X + ROZJAZD_ZNAK_POL_DL, y)
        urz.RysujLinie(PEDZEL_ROZJAZD_ZNAK, ROZJAZD_ZNAK_SZER, ROZJAZD_PLUS_X, y - ROZJAZD_ZNAK_POL_DL, ROZJAZD_PLUS_X, y + ROZJAZD_ZNAK_POL_DL)
    End Sub

    Private Sub RysujMinus(y As Single)
        urz.RysujLinie(PEDZEL_ROZJAZD_ZNAK, ROZJAZD_ZNAK_SZER, ROZJAZD_MINUS_X - ROZJAZD_ZNAK_POL_DL, y, ROZJAZD_MINUS_X + ROZJAZD_ZNAK_POL_DL, y)
    End Sub

    Private Sub RysujNazweSygnalizatora(nazwa As String)
        RysujNazwe(nazwa, TEKST_POZ_X, 2 * SYGN_POZ + TEKST_POZ_Y)
    End Sub

    Private Sub RysujSygnalizatorManewrowy(sygnalizator As Zaleznosci.SygnalizatorManewrowy)
        Dim pedzNiebieski As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.BrakWyjazdu, PEDZEL_SYGN_NIEB_JASNY, PEDZEL_SYGN_NIEB)
        Dim pedzBialy As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraManewrowego.Manewrowy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        RysujTorProsty(sygnalizator.RysowanieDodatkowychTrojkatow)
        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzNiebieski, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzBialy, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(2)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorPowtarzajacy(sygnalizator As Zaleznosci.SygnalizatorPowtarzajacy)
        Dim pedzPomc As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraPowtarzajacego.BrakWyjazdu, PEDZEL_SYGN_POMC_JASNY, PEDZEL_SYGN_POMC)
        Dim pedzZiel As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraPowtarzajacego.Zezwalajacy, PEDZEL_SYGN_ZIEL_JASNY, PEDZEL_SYGN_ZIEL)

        Dim nazwa As String = KolejnoscSygnPowtToString(sygnalizator.Kolejnosc) & NAZWA_SP
        If sygnalizator.SygnalizatorPowtarzany IsNot Nothing Then nazwa &= sygnalizator.SygnalizatorPowtarzany.Nazwa

        RysujTorProsty(sygnalizator.RysowanieDodatkowychTrojkatow)
        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzPomc, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzZiel, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(2)
        RysujNazweSygnalizatora(nazwa)
    End Sub

    Private Sub RysujSygnalizatorPolsamoczynny(sygnalizator As Zaleznosci.SygnalizatorPolsamoczynny)
        Dim pedzCzer As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)
        Dim pedzZiel As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zezwalajacy, PEDZEL_SYGN_ZIEL_JASNY, PEDZEL_SYGN_ZIEL)
        Dim pedzBial As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Manewrowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatora.Zastepczy, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        RysujTorProsty(sygnalizator.RysowanieDodatkowychTrojkatow)
        urz.WypelnijTloSygnalizatora(PEDZEL_SYGN_TLO, SYGN_POZ, 3 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzCzer, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzZiel, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(pedzBial, 3 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(3)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorSamoczynny(sygnalizator As Zaleznosci.SygnalizatorSamoczynny)
        Dim pedz As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.BrakWyjazdu, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)

        RysujTorProsty(sygnalizator.RysowanieDodatkowychTrojkatow)
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedz, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        RysujSlupSygnalizatora(1)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujSygnalizatorTOP(sygnalizator As Zaleznosci.SygnalizatorOstrzegawczyPrzejazdowy)
        Dim wyrozniony As Boolean = sygnalizator Is sygnTopWyrozniony
        Dim pedzTlo As TPedzel = If(wyrozniony, PEDZEL_PRZEJAZD_SYGN_TOP, PEDZEL_SYGN_TLO)

        RysujTorProsty(sygnalizator.RysowanieDodatkowychTrojkatow)
        urz.WypelnijTloSygnalizatora(pedzTlo, SYGN_POZ, 2 * SYGN_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)

        If Not wyrozniony Then
            Dim pedzBial As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraOstrzegawczegoPrzejazdowego.PrzejazdZamkniety, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)
            Dim pedzPomc As TPedzel = If(trybProjektowy Or sygnalizator.Stan = Zaleznosci.StanSygnalizatoraOstrzegawczegoPrzejazdowego.PrzejazdUszkodzony, PEDZEL_SYGN_POMC_JASNY, PEDZEL_SYGN_POMC)

            urz.WypelnijKolo(pedzBial, SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
            urz.WypelnijKolo(pedzPomc, 2 * SYGN_POZ, SYGN_POZ, SYGN_PROMIEN)
        End If

        RysujSlupSygnalizatora(2)
        RysujNazweSygnalizatora(sygnalizator.Nazwa)
    End Sub

    Private Sub RysujPrzejazd(przejazd As Zaleznosci.PrzejazdKolejowy)
        Dim pedzAwaria As TPedzel = If(trybProjektowy Or przejazd.Awaria, PEDZEL_SYGN_CZER_JASNY, PEDZEL_SYGN_CZER)
        Dim pedzStan As TPedzel = If(trybProjektowy Or przejazd.Stan <> Zaleznosci.StanPrzejazduKolejowego.Otwarty, PEDZEL_SYGN_BIAL_JASNY, PEDZEL_SYGN_BIAL)

        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, PRZEJAZD_SZER_LINII, PRZEJAZD_POZ, 0.0, PRZEJAZD_POZ, 1.0)
        urz.RysujLinie(PEDZEL_SYGN_KRAWEDZ, PRZEJAZD_SZER_LINII, 1.0 - PRZEJAZD_POZ, 0.0, 1.0 - PRZEJAZD_POZ, 1.0)
        RysujTorProsty(przejazd.RysowanieDodatkowychTrojkatow)
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, PRZEJAZD_KONTR_POZ, SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzAwaria, PRZEJAZD_KONTR_POZ, SYGN_POZ, SYGN_PROMIEN)
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, PRZEJAZD_KONTR_POZ, 3 * SYGN_POZ, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzStan, PRZEJAZD_KONTR_POZ, 3 * SYGN_POZ, SYGN_PROMIEN)
    End Sub

    Private Sub RysujPrzycisk(wcisniety As Boolean, Optional poczx As Single = 0.0F, Optional poczy As Single = 0.0F)
        Dim pedzel As TPedzel = If(wcisniety, PEDZEL_PRZYCISK_WCISNIETY, PEDZEL_PRZYCISK)

        Dim x As Single = (poczx + 1.0F) * SYGN_POZ
        Dim y As Single = (poczy + 1.0F) * SYGN_POZ
        urz.WypelnijKolo(PEDZEL_SYGN_TLO, x, y, SYGN_TLO_PROMIEN)
        urz.WypelnijKolo(pedzel, x, y, SYGN_PROMIEN)
    End Sub

    Private Sub RysujPrzyciskZwykly(przycisk As Zaleznosci.Przycisk)
        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety)

        Select Case przycisk.TypPrzycisku
            Case Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy
                RysujNazwe(NAZWA_SZ, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y, przywrocTransformacje:=True)
                RysujNazweSygnalizatora(przycisk.ObslugiwanySygnalizator?.Nazwa)

            Case Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow
                RysujNazwe(NAZWA_ZW, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y)
        End Select
    End Sub

    Private Sub RysujPrzyciskTor(przycisk As Zaleznosci.PrzyciskTor)
        RysujTorProsty(przycisk.RysowanieDodatkowychTrojkatow)
        RysujPrzycisk((Not trybProjektowy) And przycisk.Wcisniety)

        If przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy Or przycisk.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy Then
            RysujNazwe(NAZWA_M, SYGN_POZ + TEKST_POZ_X_PRZYCISK, TEKST_POZ_Y, przywrocTransformacje:=True)
        End If

        RysujNazweSygnalizatora(przycisk.ObslugiwanySygnalizator?.Nazwa)
    End Sub

    Private Sub RysujKierunek(kier As Zaleznosci.Kierunek)
        RysujNazwe(kier.Nazwa, TEKST_POZ_X, TEKST_POZ_Y, przywrocTransformacje:=True)
        RysujTorProsty(kier.RysowanieDodatkowychTrojkatow)
        RysujTrojkatKierunku(kier, Zaleznosci.KierunekWyjazduSBL.Lewo, Zaleznosci.UstawionyKierunekSBL.Lewo)

        Dim tr As TMacierz = urz.TransformacjaPobierz
        urz.TransformacjaResetuj()
        urz.TransformacjaObroc(2 * KAT_PROSTY, POL, KIER_POZ_Y)
        urz.TransformacjaDolacz(tr)

        RysujTrojkatKierunku(kier, Zaleznosci.KierunekWyjazduSBL.Prawo, Zaleznosci.UstawionyKierunekSBL.Prawo)
    End Sub

    Private Sub RysujKostkeNapis(napis As Zaleznosci.Napis)
        urz.WypelnijKolo(PEDZEL_KOLKO_TEKST, KOLKO_TEKST_POZ, KOLKO_TEKST_POZ, KOLKO_TEKST_PROMIEN)
        RysujNazwe(napis.Tekst, TEKST_NAPIS_POZ, TEKST_NAPIS_POZ, TEKST_WYS, TEKST_POZ_X)
    End Sub

    Private Sub RysujSlupSygnalizatora(poczx As Single)
        Dim x1 As Single = poczx * SYGN_POZ + SYGN_TLO_PROMIEN
        Dim x2 As Single = poczx * SYGN_POZ + SYGN_TLO_PROMIEN + SYGN_SLUP_DLUG
        Dim x3 As Single = poczx * SYGN_POZ + SYGN_TLO_PROMIEN + 2 * SYGN_SLUP_DLUG

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

    Private Sub RysujTrojkatKierunku(kier As Zaleznosci.Kierunek, kierProj As Zaleznosci.KierunekWyjazduSBL, kierDzialanie As Zaleznosci.UstawionyKierunekSBL)
        urz.WypelnijFigure(PEDZEL_TOR_WOLNY, PUNKTY_KIERUNKU)

        Dim pedzel As TPedzel
        If trybProjektowy Then
            pedzel = If(kier.KierunekWyjazdu = kierProj, PEDZEL_SZCZELINA_UTWIERDZONY, PEDZEL_SZCZELINA_WOLNY)
        Else
            pedzel = If(kier.UstawionyKierunek = kierDzialanie, PEDZEL_SZCZELINA_UTWIERDZONY, PEDZEL_SZCZELINA_WOLNY)
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

    Private Sub UstawKolorToru(k As Zaleznosci.Kostka, zazn As Zaleznosci.OdcinekToru)
        pedzelToru = PEDZEL_TOR_WOLNY

        If TypeOf k Is Zaleznosci.Tor Then
            Dim t As Zaleznosci.Tor = DirectCast(k, Zaleznosci.Tor)

            If zazn IsNot Nothing AndAlso t.NalezyDoOdcinka Is zazn Then
                pedzelToru = PEDZEL_TOR_TEN_ODCINEK
            ElseIf t.NalezyDoOdcinka Is Nothing Then
                pedzelToru = PEDZEL_TOR_NIEPRZYPISANY
            End If
        End If
    End Sub

    Private Sub UstawKolorToruDlaLicznika(k As Zaleznosci.Kostka, zazn As Zaleznosci.ParaLicznikowOsi)
        pedzelToru = PEDZEL_TOR_WOLNY

        If zazn IsNot Nothing AndAlso TypeOf k Is Zaleznosci.Tor Then
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

    Private Sub UstawKolorToruDlaPrzejazdu(k As Zaleznosci.Kostka, zazn As Zaleznosci.PrzejazdKolejowoDrogowy)
        pedzelToru = PEDZEL_TOR_WOLNY

        If TypeOf k Is Zaleznosci.PrzejazdKolejowy Then
            Dim p As Zaleznosci.PrzejazdKolejowy = CType(k, Zaleznosci.PrzejazdKolejowy)

            If zazn IsNot Nothing AndAlso p.NalezyDoPrzejazdu Is zazn Then
                pedzelToru = PEDZEL_TOR_TEN_ODCINEK
            ElseIf p.NalezyDoPrzejazdu Is Nothing Then
                pedzelToru = PEDZEL_TOR_NIEPRZYPISANY
            End If
        End If
    End Sub

    Private Sub UstawKolorToruDlaPrzejazduAutomatyzacja(k As Zaleznosci.Kostka, zazn As Zaleznosci.AutomatyczneZamykaniePrzejazduKolejowego)
        pedzelToru = PEDZEL_TOR_WOLNY

        If zazn IsNot Nothing AndAlso TypeOf k Is Zaleznosci.Tor Then
            Dim t As Zaleznosci.Tor = DirectCast(k, Zaleznosci.Tor)

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
            Case Zaleznosci.ZajetoscToru.PrzebiegUtwierdzony,
                 Zaleznosci.ZajetoscToru.BlokadaNieustawiona
                Return PEDZEL_SZCZELINA_UTWIERDZONY
        End Select

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

    Private Function KolejnoscSygnPowtToString(kolejnosc As Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego) As String
        Select Case kolejnosc
            Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Pierwszy
                Return "I"
            Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Drugi
                Return "II"
            Case Zaleznosci.KolejnoscSygnalizatoraPowtarzajacego.Trzeci
                Return "III"
            Case Else
                Return ""
        End Select
    End Function
End Class