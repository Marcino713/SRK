Imports Zaleznosci.PlikiPolaczen
Imports SegmPliku = Zaleznosci.SegmentPliku(Of Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPolaczen.KonfiguracjaZapisu, Zaleznosci.PlikiPolaczen.KonfiguracjaOdczytu))
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPolaczen.KonfiguracjaZapisu, Zaleznosci.PlikiPolaczen.KonfiguracjaOdczytu)

Public Class PolaczeniaStacji
    Public Shared ReadOnly ObslugiwaneWersje As WersjaPliku() = {New WersjaPliku(0, 1)}
    Public Const ROZSZERZENIE_PLIKU As String = ".pol"
    Public Const OPIS_PLIKU As String = "Połączenia posterunków ruchu"
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

    Private _LaczanePliki As New List(Of LaczonyPlikStacji)
    Public ReadOnly Property LaczanePliki As List(Of LaczonyPlikStacji)
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

    Public Shared Function OtworzPlik(sciezkaPliku As String, Optional dodajNowePliki As Boolean = True) As PolaczeniaStacji
        Try
            Dim pol As PolaczeniaStacji = _Otworz(sciezkaPliku)
            PorownajStacjeIPolacz(pol, pol.OtworzStacjeZFolderu(Path.GetDirectoryName(sciezkaPliku)), dodajNowePliki)
            pol.SortujPosterunki()
            Return pol
        Catch
            Return Nothing
        End Try
    End Function

    Public Shared Function OtworzFolder(sciezkaPlikuDoZapisu As String) As PolaczeniaStacji
        Dim polaczenia As New PolaczeniaStacji(sciezkaPlikuDoZapisu)
        If Not polaczenia.Zapisz Then Return Nothing
        polaczenia._LaczanePliki = polaczenia.OtworzStacjeZFolderu(Path.GetDirectoryName(sciezkaPlikuDoZapisu))
        polaczenia.SortujPosterunki()
        Return polaczenia
    End Function

    Private Function OtworzStacjeZFolderu(sciezka As String) As List(Of LaczonyPlikStacji)
        Dim pliki As New List(Of LaczonyPlikStacji)
        Dim sciezki As String() = Directory.GetFiles(sciezka)

        For i As Integer = 0 To sciezki.Length - 1
            If sciezki(i).EndsWith(Pulpit.ROZSZERZENIE_PLIKU) Then
                Dim plik As LaczonyPlikStacji = LaczonyPlikStacji.WczytajPulpit(sciezki(i))
                If plik IsNot Nothing Then pliki.Add(plik)
            End If
        Next

        Return pliki
    End Function

    Private Shared Sub PorownajStacjeIPolacz(plikPol As PolaczeniaStacji, plikiStacji As List(Of LaczonyPlikStacji), dodajNowePliki As Boolean)
        'Sprawdź, czy pliki stacji się zmieniły
        For Each pol As LaczonyPlikStacji In plikPol.LaczanePliki
            Dim dopasowanaStacja As LaczonyPlikStacji = ZnajdzIUsun(pol.NazwaPliku, plikiStacji)

            If dopasowanaStacja Is Nothing Then
                pol.Uwagi = UwagiLaczanegoPlikuStacji.BrakPliku
            ElseIf Not CzyRowne(pol.Skrot, dopasowanaStacja.Skrot) Then
                pol.Uwagi = UwagiLaczanegoPlikuStacji.Zmodyfikowany
                pol.Skrot = dopasowanaStacja.Skrot
            ElseIf CzyBrakujePolaczen(pol, plikPol.LaczaneTory)
                pol.Uwagi = UwagiLaczanegoPlikuStacji.BrakiPolaczen
            End If

        Next

        'Dodaj nowe pliki z folderu do połączeń
        If dodajNowePliki Then
            For Each pol As LaczonyPlikStacji In plikiStacji
                plikPol.LaczanePliki.Add(pol)
            Next
        End If
    End Sub

    Private Shared Function ZnajdzIUsun(nazwa As String, pliki As List(Of LaczonyPlikStacji)) As LaczonyPlikStacji
        Dim znaleziony As LaczonyPlikStacji = Nothing

        For Each stacja As LaczonyPlikStacji In pliki
            If stacja.NazwaPliku = nazwa Then
                znaleziony = stacja
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
        Dim konf As New KonfiguracjaZapisu

        Using fs As New FileStream(_SciezkaPliku, FileMode.Create, FileAccess.Write)
            Using bw As New BinaryWriter(fs)
                bw.Write(PobierzBajty(NAGLOWEK))
                bw.Write(Wersja.WersjaGlowna)
                bw.Write(Wersja.WersjaBoczna)
                bw.Write(DataUtworzenia.ToBinary)

                ZapiszObiekty(bw, _LaczaneTory, TypObiektuPliku.LACZONE_TORY, konf)
                ZapiszObiekty(bw, _LaczanePliki, TypObiektuPliku.LACZONE_PLIKI, konf)
            End Using
        End Using

        Return True
    End Function

    Private Sub ZapiszObiekty(bw As BinaryWriter, obiekty As IEnumerable(Of IObiektPlikuTyp), typ As UShort, konf As KonfiguracjaZapisu)
        For Each o As IObiektPlikuTyp In obiekty
            Dim b As Byte() = o.Zapisz(konf)
            If b IsNot Nothing Then
                bw.Write(typ)
                bw.Write(CType(b.Length, UShort))
                bw.Write(b)
            End If
        Next
    End Sub

    Private Shared Function _Otworz(sciezka As String) As PolaczeniaStacji
        Dim p As PolaczeniaStacji
        Dim segmenty As New List(Of SegmPliku)
        Dim konf As New KonfiguracjaOdczytu
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
                p = New PolaczeniaStacji(New WersjaPliku(wersja_glowna, wersja_boczna))
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

    Private Shared Function UtworzObiekt(br As BinaryReader, konf As KonfiguracjaOdczytu) As SegmPliku
        Dim typ As UShort = br.ReadUInt16
        Dim ile As UShort = br.ReadUInt16
        Dim b As Byte() = br.ReadBytes(ile)
        Dim ob As IObiektPlikuTyp = Nothing

        Select Case typ
            Case TypObiektuPliku.LACZONE_TORY
                ob = LaczoneOdcinkiTorow.UtworzObiekt(b, konf)
            Case TypObiektuPliku.LACZONE_PLIKI
                ob = LaczonyPlikStacji.UtworzObiekt(b, konf)
        End Select

        Return New SegmPliku() With {.Dane = b, .Obiekt = ob}
    End Function

    Private Sub SortujPosterunki()
        _LaczanePliki = _LaczanePliki.OrderBy(Function(p As LaczonyPlikStacji) p.NazwaPosterunku).ToList
    End Sub

    Private Shared Function CzyBrakujePolaczen(stacja As LaczonyPlikStacji, polaczoneTory As List(Of LaczoneOdcinkiTorow)) As Boolean
        For Each tor As LaczoneOdcinkiTorow In polaczoneTory
            If (tor.Posterunek1 Is stacja Or tor.Posterunek2 Is stacja) And tor.Uwagi <> UwagiLaczonegoOdcinkaTorow.OK Then
                Return True
            End If
        Next

        Return False
    End Function

End Class