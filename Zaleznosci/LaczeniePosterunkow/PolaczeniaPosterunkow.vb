Imports SegmPliku = Zaleznosci.SegmentPliku(Of Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPolaczen, Zaleznosci.KonfiguracjaOdczytuPolaczen))
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPolaczen, Zaleznosci.KonfiguracjaOdczytuPolaczen)

Public Class PolaczeniaPosterunkow
    Public Shared ReadOnly ObslugiwaneWersje As WersjaPliku() = {New WersjaPliku(0, 1)}
    Public Const ROZSZERZENIE_PLIKU As String = ".pol"
    Private Const NAGLOWEK As String = "POLC"

    Public ReadOnly Property Wersja As New WersjaPliku(0, 1)

    Private _SciezkaPliku As String = ""
    Public ReadOnly Property SciezkaPliku As String
        Get
            Return _SciezkaPliku
        End Get
    End Property

    Private _DataUtworzenia As Date = Now
    Public ReadOnly Property DataUtworzenia As Date
        Get
            Return _DataUtworzenia
        End Get
    End Property

    Private _LaczanePliki As New List(Of LaczonyPlikPosterunku)
    Public ReadOnly Property LaczanePliki As List(Of LaczonyPlikPosterunku)
        Get
            Return _LaczanePliki
        End Get
    End Property

    Private _LaczaneTory As New List(Of LaczoneOdcinkiTorow)
    Public ReadOnly Property LaczaneTory As List(Of LaczoneOdcinkiTorow)
        Get
            Return _LaczaneTory
        End Get
    End Property

    Private Sub New(sciezka As String)
        _SciezkaPliku = sciezka
    End Sub

    Private Sub New(wersja As WersjaPliku)
        If Not wersja.CzyObslugiwana(ObslugiwaneWersje) Then Throw New OtwieraniePlikuException("Wersja pliku jest nieobsługiwana.")

        Me.Wersja = wersja
    End Sub

    Public Shared Function OtworzPlik(sciezkaPliku As String, Optional dodajNowePliki As Boolean = True) As PolaczeniaPosterunkow
        Try
            Dim pol As PolaczeniaPosterunkow = _Otworz(sciezkaPliku)
            PorownajPosterunkiIPolacz(pol, pol.OtworzPosterunkiZFolderu(Path.GetDirectoryName(sciezkaPliku)), dodajNowePliki)
            pol.SortujPosterunki()
            Return pol
        Catch
            Return Nothing
        End Try
    End Function

    Public Shared Function OtworzFolder(sciezkaPlikuDoZapisu As String) As PolaczeniaPosterunkow
        Dim polaczenia As New PolaczeniaPosterunkow(sciezkaPlikuDoZapisu)
        If Not polaczenia.Zapisz Then Return Nothing
        polaczenia._LaczanePliki = polaczenia.OtworzPosterunkiZFolderu(Path.GetDirectoryName(sciezkaPlikuDoZapisu))
        polaczenia.SortujPosterunki()
        Return polaczenia
    End Function

    Private Function OtworzPosterunkiZFolderu(sciezka As String) As List(Of LaczonyPlikPosterunku)
        Dim pliki As New List(Of LaczonyPlikPosterunku)
        Dim sciezki As String() = Directory.GetFiles(sciezka)

        For i As Integer = 0 To sciezki.Length - 1
            If sciezki(i).EndsWith(Pulpit.ROZSZERZENIE_PLIKU) Then
                Dim plik As LaczonyPlikPosterunku = LaczonyPlikPosterunku.WczytajPulpit(sciezki(i))
                If plik IsNot Nothing Then pliki.Add(plik)
            End If
        Next

        Return pliki
    End Function

    Private Shared Sub PorownajPosterunkiIPolacz(plikPol As PolaczeniaPosterunkow, plikiPosterunkow As List(Of LaczonyPlikPosterunku), dodajNowePliki As Boolean)
        'Sprawdź, czy pliki posterunkow się zmieniły
        For Each pol As LaczonyPlikPosterunku In plikPol.LaczanePliki
            Dim dopasowanyPosterunek As LaczonyPlikPosterunku = ZnajdzIUsun(pol.NazwaPliku, plikiPosterunkow)

            If dopasowanyPosterunek Is Nothing Then
                pol.Uwagi = UwagiLaczanegoPlikuPosterunku.BrakPliku
            ElseIf Not CzyRowne(pol.Skrot, dopasowanyPosterunek.Skrot) Then
                pol.Uwagi = UwagiLaczanegoPlikuPosterunku.Zmodyfikowany
                pol.Skrot = dopasowanyPosterunek.Skrot
            ElseIf CzyBrakujePolaczen(pol, plikPol.LaczaneTory) Then
                pol.Uwagi = UwagiLaczanegoPlikuPosterunku.BrakiPolaczen
            End If

        Next

        'Dodaj nowe pliki z folderu do połączeń
        If dodajNowePliki Then
            For Each pol As LaczonyPlikPosterunku In plikiPosterunkow
                plikPol.LaczanePliki.Add(pol)
            Next
        End If
    End Sub

    Private Shared Function ZnajdzIUsun(nazwa As String, pliki As List(Of LaczonyPlikPosterunku)) As LaczonyPlikPosterunku
        Dim znaleziony As LaczonyPlikPosterunku = Nothing

        For Each posterunek As LaczonyPlikPosterunku In pliki
            If posterunek.NazwaPliku = nazwa Then
                znaleziony = posterunek
                Exit For
            End If
        Next

        If znaleziony IsNot Nothing Then pliki.Remove(znaleziony)

        Return znaleziony
    End Function

    Public Function Zapisz() As Boolean
        Try
            Return _Zapisz()
        Catch
            Return False
        End Try
    End Function

    Private Function _Zapisz() As Boolean
        Dim konf As New KonfiguracjaZapisuPolaczen

        Using fs As New FileStream(_SciezkaPliku, FileMode.Create, FileAccess.Write)
            Using bw As New BinaryWriter(fs)
                bw.Write(PobierzBajty(NAGLOWEK))
                bw.Write(Wersja.WersjaGlowna)
                bw.Write(Wersja.WersjaBoczna)
                bw.Write(DataUtworzenia.ToBinary)

                ZapiszObiekty(bw, _LaczaneTory, TypObiektuPlikuPolaczen.LACZONE_TORY, konf)
                ZapiszObiekty(bw, _LaczanePliki, TypObiektuPlikuPolaczen.LACZONE_PLIKI, konf)
            End Using
        End Using

        Return True
    End Function

    Private Sub ZapiszObiekty(bw As BinaryWriter, obiekty As IEnumerable(Of IObiektPlikuTyp), typ As UShort, konf As KonfiguracjaZapisuPolaczen)
        For Each o As IObiektPlikuTyp In obiekty
            Dim b As Byte() = o.Zapisz(konf)
            If b IsNot Nothing Then
                bw.Write(typ)
                bw.Write(CUShort(b.Length))
                bw.Write(b)
            End If
        Next
    End Sub

    Private Shared Function _Otworz(sciezka As String) As PolaczeniaPosterunkow
        Dim p As PolaczeniaPosterunkow
        Dim segmenty As New List(Of SegmPliku)
        Dim konf As New KonfiguracjaOdczytuPolaczen
        konf.SciezkaFolderu = Path.GetDirectoryName(sciezka)
        If Not konf.SciezkaFolderu.EndsWith(Path.DirectorySeparatorChar) Then konf.SciezkaFolderu &= Path.DirectorySeparatorChar

        Using fs As New FileStream(sciezka, FileMode.Open, FileAccess.Read)
            Using br As New BinaryReader(fs)
                Dim b As Byte()

                b = br.ReadBytes(NAGLOWEK.Length)
                If PobierzTekst(b) <> NAGLOWEK Then
                    Throw New OtwieraniePlikuException("Niepoprawny typ pliku.")
                End If

                Dim wersja_glowna As UShort = br.ReadUInt16
                Dim wersja_boczna As UShort = br.ReadUInt16
                p = New PolaczeniaPosterunkow(New WersjaPliku(wersja_glowna, wersja_boczna))
                p._SciezkaPliku = sciezka
                p._DataUtworzenia = Date.FromBinary(br.ReadInt64)
                konf.Polaczenia = p

                Do Until fs.Position >= fs.Length
                    Dim seg As SegmPliku = UtworzObiekt(br, konf)
                    If seg.Obiekt IsNot Nothing Then segmenty.Add(seg)
                Loop

            End Using
        End Using

        For Each s As SegmPliku In segmenty
            s.Obiekt.Otworz(s.Dane, konf)
        Next

        Return p
    End Function

    Private Shared Function UtworzObiekt(br As BinaryReader, konf As KonfiguracjaOdczytuPolaczen) As SegmPliku
        Dim typ As UShort = br.ReadUInt16
        Dim ile As UShort = br.ReadUInt16
        Dim b As Byte() = br.ReadBytes(ile)
        Dim ob As IObiektPlikuTyp = Nothing

        Select Case typ
            Case TypObiektuPlikuPolaczen.LACZONE_TORY
                ob = LaczoneOdcinkiTorow.UtworzObiekt(b, konf)
            Case TypObiektuPlikuPolaczen.LACZONE_PLIKI
                ob = LaczonyPlikPosterunku.UtworzObiekt(b, konf)
        End Select

        Return New SegmPliku() With {.Dane = b, .Obiekt = ob}
    End Function

    Private Sub SortujPosterunki()
        _LaczanePliki = _LaczanePliki.OrderBy(Function(p) p.NazwaPosterunku).ToList
    End Sub

    Private Shared Function CzyBrakujePolaczen(posterunek As LaczonyPlikPosterunku, polaczoneTory As List(Of LaczoneOdcinkiTorow)) As Boolean
        For Each tor As LaczoneOdcinkiTorow In polaczoneTory
            If (tor.Posterunek1 Is posterunek Or tor.Posterunek2 Is posterunek) And tor.Uwagi <> UwagiLaczonegoOdcinkaTorow.OK Then
                Return True
            End If
        Next

        Return False
    End Function

End Class

Friend Class TypObiektuPlikuPolaczen
    Friend Const LACZONE_TORY As UShort = 1
    Friend Const LACZONE_PLIKI As UShort = 2
End Class