Friend Module WygladzanieTorowUkosnych
    Private Const KAT_PROSTY As Integer = 90
    Private Const KAT_PELNY As Integer = 360
    Private Const BOKI As Integer = 4

    'Z której strony, względem kostki o danych współrzędnych, znajduje się kostka sąsiednia
    Private ReadOnly PRAWO As New Point(1, 0)
    Private ReadOnly DOL As New Point(0, 1)
    Private ReadOnly LEWO As New Point(-1, 0)
    Private ReadOnly GORA As New Point(0, -1)

    'Położenia kostki sąsiedniej, zgodnie z rosnącym obrotem
    Private ReadOnly KOSTKI_SASIEDNIE As Point() = {PRAWO, DOL, LEWO, GORA}

    Friend Sub WyznaczWygladzanieZakretow(pulpit As Zaleznosci.Pulpit)
        pulpit.PrzeiterujKostki(AddressOf CzyscParametryWygladzaniaDlaKostki)
        pulpit.PrzeiterujKostki(AddressOf PrzetworzKostke, pulpit)
    End Sub

    Private Sub CzyscParametryWygladzaniaDlaKostki(x As Integer, y As Integer, k As Zaleznosci.Kostka)
        If TypeOf k Is Zaleznosci.Tor And TypeOf k IsNot Zaleznosci.Zakret Then
            DirectCast(k, Zaleznosci.Tor).RysowanieDodatkowychTrojkatow = 0
        End If

        If TypeOf k Is Zaleznosci.TorKoniec Then
            DirectCast(k, Zaleznosci.TorKoniec).RysowanieDodatkowychTrojkatow = 0
        End If

        If TypeOf k Is Zaleznosci.IZakret Then
            DirectCast(k, Zaleznosci.IZakret).PrzytnijZakret = 0
        End If
    End Sub

    Private Sub PrzetworzKostke(x As Integer, y As Integer, k As Zaleznosci.Kostka, pulpit As Zaleznosci.Pulpit)
        If TypeOf k Is Zaleznosci.IZakret Then
            Dim zakret As Zaleznosci.IZakret = DirectCast(k, Zaleznosci.IZakret)
            Dim rozjLewo As Boolean = k.Typ = Zaleznosci.TypKostki.RozjazdLewo
            Dim rozjPrawo As Boolean = k.Typ = Zaleznosci.TypKostki.RozjazdPrawo
            Dim wsp As New Point(x, y)
            Dim granicaPrawo As GranicaToru
            Dim granicaDol As GranicaToru
            Dim sasiedztwoPrawo As Point
            Dim sasiedztwoDol As Point
            Dim obrotKostki As Integer = ObliczObrot(k.Obrot, rozjPrawo)

            'sprawdź, z czym graniczy kostka
            sasiedztwoPrawo = KOSTKI_SASIEDNIE(ObliczStopienObrotu(obrotKostki))
            sasiedztwoDol = KOSTKI_SASIEDNIE(ObliczStopienObrotu(obrotKostki + KAT_PROSTY))

            granicaPrawo = CzyGraniczyZTorem(pulpit, wsp, sasiedztwoPrawo, KrawedzZakretu.Prawo, rozjPrawo)
            granicaDol = CzyGraniczyZTorem(pulpit, wsp, sasiedztwoDol, KrawedzZakretu.Dol, rozjPrawo)

            'wyznacz przycięcie trójkątów na torach ukośnych
            If ((granicaPrawo And GranicaToru.PrzedluzonyZakret) = 0) And (((granicaPrawo And GranicaToru.Prosty) <> 0) Or rozjLewo) Then
                zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.Prawo
                If rozjLewo Then zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.UmniejszPrawo
            End If

            If ((granicaDol And GranicaToru.PrzedluzonyZakret) = 0) And (((granicaDol And GranicaToru.Prosty) <> 0) Or rozjPrawo) Then
                zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.Dol
                If rozjPrawo Then zakret.PrzytnijZakret = zakret.PrzytnijZakret Or Zaleznosci.PrzycinanieZakretu.UmniejszDol
            End If

            'wyznacz dokładanie trójkątów na torach prostych
            WyznaczDodatkoweTrojkaty(pulpit, x, y, obrotKostki, granicaPrawo, sasiedztwoPrawo, 0 * KAT_PROSTY,
                                     Zaleznosci.DodatkoweTrojkatyTor.LewoDol, Zaleznosci.DodatkoweTrojkatyTorKoniec.LewoDol, Zaleznosci.DodatkoweTrojkatyTor.PrawoGora)

            WyznaczDodatkoweTrojkaty(pulpit, x, y, obrotKostki, granicaDol, sasiedztwoDol, 1 * KAT_PROSTY,
                                     Zaleznosci.DodatkoweTrojkatyTor.LewoGora, Zaleznosci.DodatkoweTrojkatyTorKoniec.LewoGora, Zaleznosci.DodatkoweTrojkatyTor.PrawoDol)

        End If
    End Sub

    Private Sub WyznaczDodatkoweTrojkaty(pulpit As Zaleznosci.Pulpit, x As Integer, y As Integer, obrot As Integer, granica As GranicaToru, sasiedztwo As Point, poczatkowyObrot As Integer,
                                         trojLewo As Zaleznosci.DodatkoweTrojkatyTor, trojLewoKoniec As Zaleznosci.DodatkoweTrojkatyTorKoniec, trojPrawo As Zaleznosci.DodatkoweTrojkatyTor)

        If (granica And GranicaToru.Prosty) <> 0 And (granica And GranicaToru.Zakret) = 0 Then
            Dim sasiedniaKostka As Zaleznosci.Kostka = pulpit.Kostki(x + sasiedztwo.X, y + sasiedztwo.Y)
            Dim sasiedniTor As Zaleznosci.Tor = TryCast(sasiedniaKostka, Zaleznosci.Tor)
            Dim sasiedniKoniec As Zaleznosci.TorKoniec = TryCast(sasiedniaKostka, Zaleznosci.TorKoniec)

            If CzyObrotRowny(sasiedniaKostka.Obrot, obrot + poczatkowyObrot) Then
                If sasiedniTor IsNot Nothing Then
                    sasiedniTor.RysowanieDodatkowychTrojkatow = sasiedniTor.RysowanieDodatkowychTrojkatow Or trojLewo
                ElseIf sasiedniKoniec IsNot Nothing Then
                    sasiedniKoniec.RysowanieDodatkowychTrojkatow = sasiedniKoniec.RysowanieDodatkowychTrojkatow Or trojLewoKoniec
                End If

            ElseIf CzyObrotRowny(sasiedniaKostka.Obrot, obrot + poczatkowyObrot + 2 * KAT_PROSTY) Then
                If sasiedniTor IsNot Nothing Then
                    sasiedniTor.RysowanieDodatkowychTrojkatow = sasiedniTor.RysowanieDodatkowychTrojkatow Or trojPrawo
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
    ''' <returns></returns>
    Private Function CzyGraniczyZTorem(pulpit As Zaleznosci.Pulpit, wspolrzedne As Point, roznica As Point, krawedz As KrawedzZakretu, rozjazdPrawy As Boolean) As GranicaToru
        Dim kostkaNowa As New Point(wspolrzedne.X + roznica.X, wspolrzedne.Y + roznica.Y)

        If kostkaNowa.X < 0 OrElse kostkaNowa.Y < 0 OrElse kostkaNowa.X >= pulpit.Szerokosc OrElse kostkaNowa.Y >= pulpit.Wysokosc Then
            Return GranicaToru.Brak
        End If

        Dim kostka As Zaleznosci.Kostka = pulpit.Kostki(kostkaNowa.X, kostkaNowa.Y)
        If kostka Is Nothing Then Return GranicaToru.Brak

        If Zaleznosci.Kostka.CzyTor(kostka.Typ) Then
            If kostka.Typ = Zaleznosci.TypKostki.Zakret Then
                Return CzyGraniczyZZakretem(kostka.Obrot, roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.RozjazdLewo Then
                Return CzyGraniczyZToremProstym(kostka.Obrot, roznica) Or CzyGraniczyZZakretem(kostka.Obrot, roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.RozjazdPrawo Then
                Return CzyGraniczyZToremProstym(kostka.Obrot, roznica) Or CzyGraniczyZZakretem(ObliczObrot(kostka.Obrot, True), roznica, krawedz)

            ElseIf kostka.Typ = Zaleznosci.TypKostki.TorKoniec Then
                Return CzyGraniczyZKoncemToru(kostka.Obrot, roznica)

            Else    'tor prosty
                Return CzyGraniczyZToremProstym(kostka.Obrot, roznica)
            End If
        End If

        Return GranicaToru.Brak
    End Function

    Private Function CzyGraniczyZKoncemToru(obrot As Integer, roznica As Point) As GranicaToru
        Dim ix As Integer = ObliczStopienObrotu(obrot)

        If KOSTKI_SASIEDNIE(ix) = roznica Then
            Return GranicaToru.Prosty
        End If

        Return GranicaToru.Brak
    End Function

    Private Function CzyGraniczyZToremProstym(obrot As Integer, roznica As Point) As GranicaToru
        Dim reszta As Integer = ObliczStopienObrotu(obrot) And 1

        If _
            (reszta = 0 And roznica.X <> 0) Or
            (reszta = 1 And roznica.Y <> 0) Then
            Return GranicaToru.Prosty
        End If

        Return GranicaToru.Brak
    End Function

    Private Function CzyGraniczyZZakretem(obrot As Integer, roznica As Point, krawedz As KrawedzZakretu) As GranicaToru
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

    Private Function CzyObrotRowny(obrSprawdzany As Integer, obrDrugi As Integer) As Boolean
        obrSprawdzany = obrSprawdzany Mod KAT_PELNY
        obrDrugi = obrDrugi Mod KAT_PELNY
        Return obrSprawdzany = obrDrugi
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
    Friend Enum GranicaToru
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