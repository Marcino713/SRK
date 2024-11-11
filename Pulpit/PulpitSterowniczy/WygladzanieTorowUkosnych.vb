Friend Module WygladzanieTorowUkosnych
    Private Const KAT_PROSTY As Integer = 90
    Private Const KAT_PELNY As Integer = 360
    Private Const BOKI As Integer = 4

    'Z której strony, względem kostki o danych współrzędnych, znajduje się kostka sąsiednia
    Private ReadOnly PRAWO As New Zaleznosci.PunktCalkowity(1, 0)
    Private ReadOnly DOL As New Zaleznosci.PunktCalkowity(0, 1)
    Private ReadOnly LEWO As New Zaleznosci.PunktCalkowity(-1, 0)
    Private ReadOnly GORA As New Zaleznosci.PunktCalkowity(0, -1)

    'Położenia kostki sąsiedniej, zgodnie z rosnącym obrotem
    Private ReadOnly KOSTKI_SASIEDNIE As Zaleznosci.PunktCalkowity() = {PRAWO, DOL, LEWO, GORA}

    Friend Sub WyznaczWygladzanieZakretow(pulpit As Zaleznosci.Pulpit)
        pulpit.PrzeiterujKostki(AddressOf CzyscParametryWygladzaniaDlaKostki)
        pulpit.PrzeiterujKostki(AddressOf PrzetworzKostke, pulpit)
    End Sub

    Private Sub CzyscParametryWygladzaniaDlaKostki(x As Integer, y As Integer, k As Zaleznosci.Kostka)
        Dim tor As Zaleznosci.Tor = TryCast(k, Zaleznosci.Tor)
        If tor IsNot Nothing AndAlso TypeOf k IsNot Zaleznosci.Zakret AndAlso TypeOf k IsNot Zaleznosci.ZakretPodwojny Then
            tor.RysowanieDodatkowychTrojkatow = 0

            Dim torPodw As Zaleznosci.TorPodwojny = TryCast(k, Zaleznosci.TorPodwojny)
            If torPodw IsNot Nothing Then torPodw.RysowanieDodatkowychTrojkatowDrugi = 0
        End If

        Dim konc As Zaleznosci.TorKoniec = TryCast(k, Zaleznosci.TorKoniec)
        If konc IsNot Nothing Then konc.RysowanieDodatkowychTrojkatow = 0

        Dim zakr As Zaleznosci.IZakret = TryCast(k, Zaleznosci.IZakret)
        If zakr IsNot Nothing Then
            zakr.PrzytnijZakret = 0

            Dim zakrPodw As Zaleznosci.ZakretPodwojny = TryCast(k, Zaleznosci.ZakretPodwojny)
            If zakrPodw IsNot Nothing Then zakrPodw.PrzytnijZakretDrugi = 0
        End If
    End Sub

    Private Sub PrzetworzKostke(x As Integer, y As Integer, k As Zaleznosci.Kostka, pulpit As Zaleznosci.Pulpit)
        Dim zakret As Zaleznosci.IZakret = TryCast(k, Zaleznosci.IZakret)

        If zakret IsNot Nothing Then
            Dim zakretPodw As Zaleznosci.ZakretPodwojny = TryCast(k, Zaleznosci.ZakretPodwojny)
            Dim rozjLewo As Boolean = k.Typ = Zaleznosci.TypKostki.RozjazdLewo
            Dim rozjPrawo As Boolean = k.Typ = Zaleznosci.TypKostki.RozjazdPrawo
            Dim wsp As New Zaleznosci.PunktCalkowity(x, y)
            Dim obrot As Integer = ObliczObrot(k.Obrot, rozjPrawo)
            Dim granicaPrawo As GranicaToru
            Dim granicaDol As GranicaToru

            WyznaczGraniceITrojkaty(pulpit, wsp, obrot, 0, granicaPrawo, granicaDol)

            'wyznacz przycięcie trójkątów na torach ukośnych
            If ((granicaPrawo And GranicaToru.PrzedluzonyZakret) = 0) And (((granicaPrawo And GranicaToru.Prosty) <> 0) Or rozjLewo) Then
                zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.Prawo
                If rozjLewo Then zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.UmniejszPrawo
            End If

            If ((granicaDol And GranicaToru.PrzedluzonyZakret) = 0) And (((granicaDol And GranicaToru.Prosty) <> 0) Or rozjPrawo) Then
                zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.Dol
                If rozjPrawo Then zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.UmniejszDol
            End If

            If zakretPodw IsNot Nothing Then
                WyznaczGraniceITrojkaty(pulpit, wsp, obrot, 2, granicaPrawo, granicaDol)

                'wyznacz przycięcie trójkątów na drugim torze ukośnym
                If ((granicaPrawo And GranicaToru.PrzedluzonyZakret) = 0) And ((granicaPrawo And GranicaToru.Prosty) <> 0) Then
                    zakretPodw.PrzytnijZakretDrugi = zakretPodw.PrzytnijZakretDrugi Or Zaleznosci.PrzycinanieZakretu.Prawo
                End If

                If ((granicaDol And GranicaToru.PrzedluzonyZakret) = 0) And ((granicaDol And GranicaToru.Prosty) <> 0) Then
                    zakretPodw.PrzytnijZakretDrugi = zakretPodw.PrzytnijZakretDrugi Or Zaleznosci.PrzycinanieZakretu.Dol
                End If
            End If
        End If
    End Sub

    Private Sub WyznaczGraniceITrojkaty(pulpit As Zaleznosci.Pulpit, wspolrzedne As Zaleznosci.PunktCalkowity, obrot As Integer, mnoznik As Integer, ByRef granicaPrawo As GranicaToru, ByRef granicaDol As GranicaToru)
        Dim sasiedztwoPrawo As Zaleznosci.PunktCalkowity = KOSTKI_SASIEDNIE(ObliczStopienObrotu(obrot + (mnoznik + 0) * KAT_PROSTY))
        Dim sasiedztwoDol As Zaleznosci.PunktCalkowity = KOSTKI_SASIEDNIE(ObliczStopienObrotu(obrot + (mnoznik + 1) * KAT_PROSTY))

        granicaPrawo = CzyGraniczyZTorem(pulpit, wspolrzedne, sasiedztwoPrawo, KrawedzZakretu.Prawo)
        granicaDol = CzyGraniczyZTorem(pulpit, wspolrzedne, sasiedztwoDol, KrawedzZakretu.Dol)

        'wyznacz dokładanie trójkątów na torach prostych
        WyznaczDodatkoweTrojkaty(pulpit, wspolrzedne, obrot, granicaPrawo, sasiedztwoPrawo, (mnoznik + 0) * KAT_PROSTY,
                             Zaleznosci.DodatkoweTrojkatyTor.LewoDol, Zaleznosci.DodatkoweTrojkatyTorKoniec.LewoDol, Zaleznosci.DodatkoweTrojkatyTor.PrawoGora)

        WyznaczDodatkoweTrojkaty(pulpit, wspolrzedne, obrot, granicaDol, sasiedztwoDol, (mnoznik + 1) * KAT_PROSTY,
                             Zaleznosci.DodatkoweTrojkatyTor.LewoGora, Zaleznosci.DodatkoweTrojkatyTorKoniec.LewoGora, Zaleznosci.DodatkoweTrojkatyTor.PrawoDol)
    End Sub

    Private Sub WyznaczDodatkoweTrojkaty(pulpit As Zaleznosci.Pulpit, wspolrzedne As Zaleznosci.PunktCalkowity, obrot As Integer, granica As GranicaToru, sasiedztwo As Zaleznosci.PunktCalkowity, poczatkowyObrot As Integer,
                                         trojLewo As Zaleznosci.DodatkoweTrojkatyTor, trojLewoKoniec As Zaleznosci.DodatkoweTrojkatyTorKoniec, trojPrawo As Zaleznosci.DodatkoweTrojkatyTor)

        If (granica And GranicaToru.Prosty) <> 0 And (granica And GranicaToru.Zakret) = 0 Then
            Dim sasiedniaKostka As Zaleznosci.Kostka = pulpit.Kostki(wspolrzedne.X + sasiedztwo.X, wspolrzedne.Y + sasiedztwo.Y)
            Dim sasiedniTor As Zaleznosci.Tor = TryCast(sasiedniaKostka, Zaleznosci.Tor)
            Dim sasiedniKoniec As Zaleznosci.TorKoniec = TryCast(sasiedniaKostka, Zaleznosci.TorKoniec)
            Dim sasiedniMost As Zaleznosci.Most = TryCast(sasiedniaKostka, Zaleznosci.Most)
            Dim sasiedniObrot As Integer = sasiedniaKostka.Obrot

            If CzyObrotRowny(sasiedniObrot, obrot + poczatkowyObrot) Then   'obrót 0
                If sasiedniTor IsNot Nothing Then
                    sasiedniTor.RysowanieDodatkowychTrojkatow = sasiedniTor.RysowanieDodatkowychTrojkatow Or trojLewo
                ElseIf sasiedniKoniec IsNot Nothing Then
                    sasiedniKoniec.RysowanieDodatkowychTrojkatow = sasiedniKoniec.RysowanieDodatkowychTrojkatow Or trojLewoKoniec
                End If

            ElseIf CzyObrotRowny(sasiedniObrot, obrot + poczatkowyObrot + 2 * KAT_PROSTY) Then  'obrót 2
                If sasiedniTor IsNot Nothing Then
                    sasiedniTor.RysowanieDodatkowychTrojkatow = sasiedniTor.RysowanieDodatkowychTrojkatow Or trojPrawo
                End If

            ElseIf sasiedniMost IsNot Nothing Then  'obrót 1, 3 dla mostu
                Dim obr As Integer = obrot + poczatkowyObrot + 1 * KAT_PROSTY

                If CzyObrotRowny(sasiedniObrot, obr) Then
                    sasiedniMost.RysowanieDodatkowychTrojkatowDrugi = sasiedniMost.RysowanieDodatkowychTrojkatowDrugi Or trojLewo
                ElseIf CzyObrotRowny(sasiedniObrot, obr + 2 * KAT_PROSTY) Then
                    sasiedniMost.RysowanieDodatkowychTrojkatowDrugi = sasiedniMost.RysowanieDodatkowychTrojkatowDrugi Or trojPrawo
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sprawdza, czy i ewentualnie z czym graniczy kostka
    ''' </summary>
    ''' <param name="wspolrzedne">Współrzędne kostki</param>
    ''' <param name="roznica">Wartość dodawana do współrzednych kostki, aby uzyskać współrzędne kostki sprawdzanej</param>
    ''' <param name="krawedz">Sprawdzana krawędź toru ukośnego</param>
    ''' <returns>Rodzaj granicy z torem sąsiednim</returns>
    Private Function CzyGraniczyZTorem(pulpit As Zaleznosci.Pulpit, wspolrzedne As Zaleznosci.PunktCalkowity, roznica As Zaleznosci.PunktCalkowity, krawedz As KrawedzZakretu) As GranicaToru
        Dim kostkaNowa As New Zaleznosci.PunktCalkowity(wspolrzedne.X + roznica.X, wspolrzedne.Y + roznica.Y)

        'sąsiednia kostka poza zakresem pulpitu
        If kostkaNowa.X < 0 OrElse kostkaNowa.Y < 0 OrElse kostkaNowa.X >= pulpit.Szerokosc OrElse kostkaNowa.Y >= pulpit.Wysokosc Then
            Return GranicaToru.Brak
        End If

        'brak kostki
        Dim kostka As Zaleznosci.Kostka = pulpit.Kostki(kostkaNowa.X, kostkaNowa.Y)
        If kostka Is Nothing Then Return GranicaToru.Brak

        'sąsiednia kostka istnieje
        If TypeOf kostka Is Zaleznosci.Tor Then
            If kostka.Typ = Zaleznosci.TypKostki.Zakret Then
                Return CzyGraniczyZZakretem(kostka.Obrot, roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.ZakretPodwojny Then
                Return CzyGraniczyZZakretemPodwojnym(kostka.Obrot, roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.RozjazdLewo Then
                Return CzyGraniczyZToremProstym(kostka.Obrot, roznica) Or CzyGraniczyZZakretem(kostka.Obrot, roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.RozjazdPrawo Then
                Return CzyGraniczyZToremProstym(kostka.Obrot, roznica) Or CzyGraniczyZZakretem(ObliczObrot(kostka.Obrot, True), roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.Most Then
                Return GranicaToru.Prosty

            Else    'tor prosty
                Return CzyGraniczyZToremProstym(kostka.Obrot, roznica)
            End If

        ElseIf TypeOf kostka Is Zaleznosci.TorKoniec Then
            Return CzyGraniczyZKoncemToru(kostka.Obrot, roznica)

        End If

        Return GranicaToru.Brak
    End Function

    Private Function CzyGraniczyZKoncemToru(obrot As Integer, roznica As Zaleznosci.PunktCalkowity) As GranicaToru
        Dim ix As Integer = ObliczStopienObrotu(obrot)

        If KOSTKI_SASIEDNIE(ix) = roznica Then
            Return GranicaToru.Prosty
        End If

        Return GranicaToru.Brak
    End Function

    Private Function CzyGraniczyZToremProstym(obrot As Integer, roznica As Zaleznosci.PunktCalkowity) As GranicaToru
        Dim reszta As Integer = ObliczStopienObrotu(obrot) And 1

        If _
            (reszta = 0 And roznica.X <> 0) Or
            (reszta = 1 And roznica.Y <> 0) Then
            Return GranicaToru.Prosty
        End If

        Return GranicaToru.Brak
    End Function

    Private Function CzyGraniczyZZakretem(obrot As Integer, roznica As Zaleznosci.PunktCalkowity, krawedz As KrawedzZakretu) As GranicaToru
        Dim wynik As GranicaToru = GranicaToru.Brak
        Dim ix As Integer = ObliczStopienObrotu(obrot)

        If _
            (CzyObrotRowny(obrot, 0 * KAT_PROSTY) And ((roznica = GORA And krawedz = KrawedzZakretu.Dol) Or (roznica = LEWO And krawedz = KrawedzZakretu.Prawo))) Or
            (CzyObrotRowny(obrot, 1 * KAT_PROSTY) And ((roznica = GORA And krawedz = KrawedzZakretu.Prawo) Or (roznica = PRAWO And krawedz = KrawedzZakretu.Dol))) Or
            (CzyObrotRowny(obrot, 2 * KAT_PROSTY) And ((roznica = DOL And krawedz = KrawedzZakretu.Dol) Or (roznica = PRAWO And krawedz = KrawedzZakretu.Prawo))) Or
            (CzyObrotRowny(obrot, 3 * KAT_PROSTY) And ((roznica = DOL And krawedz = KrawedzZakretu.Prawo) Or (roznica = LEWO And krawedz = KrawedzZakretu.Dol))) Then
            wynik = GranicaToru.PrzedluzonyZakret
        End If

        If roznica = KOSTKI_SASIEDNIE((ix + 2) Mod BOKI) Or roznica = KOSTKI_SASIEDNIE((ix + 3) Mod BOKI) Then
            wynik = wynik Or GranicaToru.Zakret
        End If

        Return wynik
    End Function

    Private Function CzyGraniczyZZakretemPodwojnym(obrot As Integer, roznica As Zaleznosci.PunktCalkowity, krawedz As KrawedzZakretu) As GranicaToru
        Dim wynik As GranicaToru = GranicaToru.Zakret
        Dim reszta As Integer = ObliczStopienObrotu(obrot) And 1

        If _
            (
                (roznica = PRAWO Or roznica = LEWO) And (
                    (krawedz = KrawedzZakretu.Prawo And reszta = 0) Or
                    (krawedz = KrawedzZakretu.Dol And reszta = 1)
                )
            ) Or (
                (roznica = DOL Or roznica = GORA) And (
                    (krawedz = KrawedzZakretu.Dol And reszta = 0) Or
                    (krawedz = KrawedzZakretu.Prawo And reszta = 1)
                )
            ) Then
            wynik = wynik Or GranicaToru.PrzedluzonyZakret
        End If

        Return wynik
    End Function

    Private Function CzyObrotRowny(obrSprawdzany As Integer, obrDrugi As Integer) As Boolean
        Return (obrSprawdzany Mod KAT_PELNY) = (obrDrugi Mod KAT_PELNY)
    End Function

    Private Function ObliczStopienObrotu(obrot As Integer) As Integer
        Return (obrot Mod KAT_PELNY) \ KAT_PROSTY
    End Function

    Private Function ObliczObrot(obrot As Integer, czyRozjazdPrawy As Boolean) As Integer
        If czyRozjazdPrawy Then
            Return (obrot + 3 * KAT_PROSTY) Mod KAT_PELNY
        Else
            Return obrot
        End If
    End Function

    <Flags>
    Private Enum GranicaToru
        Brak = 0
        Prosty = 1
        Zakret = 2
        PrzedluzonyZakret = 4   'stykają się krawędzie prawa z prawą lub dolna z dolną
    End Enum

    Private Enum KrawedzZakretu
        Prawo
        Dol
    End Enum

End Module