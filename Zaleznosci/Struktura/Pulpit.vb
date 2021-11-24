Public Class Pulpit
    Public Shared ReadOnly ObslugiwaneWersje As WersjaPliku() = {New WersjaPliku(0, 1)}
    Public Const ROZSZERZENIE_PLIKU As String = ".stacja"
    Public Const OPIS_PLIKU As String = "Schemat posterunku ruchu"
    Public Const ROZMIAR_DOMYSLNY As Integer = 10
    Private Const NAGLOWEK As String = "STAC"

    Public ReadOnly Property Wersja As New WersjaPliku(0, 1)

    Private _SciezkaPliku As String = ""
    Public ReadOnly Property SciezkaPliku As String
        Get
            Return _SciezkaPliku
        End Get
    End Property

    Public Property Nazwa As String = ""
    Public Property Adres As UShort = 0

    Private _DataUtworzenia As Date
    Public ReadOnly Property DataUtworzenia As Date
        Get
            Return _DataUtworzenia
        End Get
    End Property

    Private _Szerokosc As Integer
    Public ReadOnly Property Szerokosc As Integer
        Get
            Return _Szerokosc
        End Get
    End Property

    Private _Wysokosc As Integer
    Public ReadOnly Property Wysokosc As Integer
        Get
            Return _Wysokosc
        End Get
    End Property

    Private _Kostki As Kostka(,)
    Public ReadOnly Property Kostki As Kostka(,)
        Get
            Return _Kostki
        End Get
    End Property

    Private _Lampy As New List(Of Lampa)
    Public ReadOnly Property Lampy As List(Of Lampa)
        Get
            Return _Lampy
        End Get
    End Property
    Public Sub SortujLampyAdresRosnaco()
        _Lampy = _Lampy.OrderBy(Function(l As Lampa) l.Adres).ToList()
    End Sub

    Private _Odcinki As New List(Of OdcinekToru)
    Public ReadOnly Property OdcinkiTorow As List(Of OdcinekToru)
        Get
            Return _Odcinki
        End Get
    End Property
    Public Sub SortujOdcinkiNazwaRosnaco()
        _Odcinki = _Odcinki.OrderBy(Function(o As OdcinekToru) o.Nazwa).ToList
    End Sub

    Private _LicznikiOsi As New List(Of ParaLicznikowOsi)
    Public ReadOnly Property LicznikiOsi As List(Of ParaLicznikowOsi)
        Get
            Return _LicznikiOsi
        End Get
    End Property
    Public Sub SortujLicznikiAdres1Rosnaco()
        _LicznikiOsi = _LicznikiOsi.OrderBy(Function(l As ParaLicznikowOsi) l.Adres1).ToList
    End Sub

    Public Sub New()
        Me.New(ROZMIAR_DOMYSLNY, ROZMIAR_DOMYSLNY)
    End Sub

    Public Sub New(szer As Integer, wys As Integer)
        _Szerokosc = szer
        _Wysokosc = wys
        ReDim _Kostki(_Szerokosc - 1, _Wysokosc - 1)
        _DataUtworzenia = Now
    End Sub

    Public Sub New(wersja As WersjaPliku, szer As Integer, wys As Integer)
        Me.New(szer, wys)

        Dim znaleziono As Boolean = False
        For i As Integer = 0 To ObslugiwaneWersje.Length - 1
            If ObslugiwaneWersje(i) = wersja Then
                znaleziono = True
                Exit For
            End If
        Next

        If Not znaleziono Then Throw New OtwieraniePlikuException("Wersja pliku jest nieobsługiwana.")

        Me.Wersja = wersja
    End Sub

    Public Function Zapisz() As Boolean
        Return Zapisz(SciezkaPliku)
    End Function

    Public Function Zapisz(sciezka As String) As Boolean
        _SciezkaPliku = sciezka

        Try
            Return _Zapisz()
        Catch
            Return False
        End Try
    End Function

    Private Function _Zapisz() As Boolean
        Dim konf As New KonfiguracjaZapisu
        Dim ix As Integer = 1

        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                If _Kostki(x, y) IsNot Nothing Then
                    konf.Kostki.Add(_Kostki(x, y), ix)
                    ix += 1
                End If
            Next
        Next

        ix = 1
        For Each o As OdcinekToru In _Odcinki
            konf.OdcinkiTorow.Add(o, ix)
            ix += 1
        Next

        Using fs As New FileStream(_SciezkaPliku, FileMode.OpenOrCreate, FileAccess.Write)
            Using bw As New BinaryWriter(fs)

                'Nagłówek
                bw.Write(PobierzBajty(NAGLOWEK))
                bw.Write(Wersja.WersjaGlowna)
                bw.Write(Wersja.WersjaBoczna)
                bw.Write(CType(_Szerokosc, UShort))
                bw.Write(CType(_Wysokosc, UShort))

                'Informacje o posterunku
                bw.Write(DataUtworzenia.ToBinary)
                bw.Write(Adres)
                ZapiszTekst(bw, Nazwa)

                'Pulpit
                For x As Integer = 0 To _Szerokosc - 1
                    For y As Integer = 0 To _Wysokosc - 1
                        If _Kostki(x, y) IsNot Nothing Then
                            konf.X = CType(x, UShort)
                            konf.Y = CType(y, UShort)
                            ZapiszObiekt(bw, _Kostki(x, y), TypObiektuPliku.KOSTKA, konf)
                        End If
                    Next
                Next

                'Inne obiekty
                ZapiszObiekty(bw, _Odcinki, TypObiektuPliku.ODCINEK_TORU, konf)
                ZapiszObiekty(bw, _LicznikiOsi, TypObiektuPliku.LICZNIK_OSI, konf)
                ZapiszObiekty(bw, _Lampy, TypObiektuPliku.LAMPA, konf)

            End Using
        End Using
        Return True
    End Function

    Private Sub ZapiszObiekt(bw As BinaryWriter, obiekt As IObiektPliku, typ As UShort, konf As KonfiguracjaZapisu)
        Dim dane As Byte() = obiekt.Zapisz(konf)
        bw.Write(typ)
        bw.Write(CType(dane.Length, UShort))
        bw.Write(dane)
    End Sub

    Private Sub ZapiszObiekty(bw As BinaryWriter, obiekty As IEnumerable(Of IObiektPliku), typ As UShort, konf As KonfiguracjaZapisu)
        For Each o As IObiektPliku In obiekty
            ZapiszObiekt(bw, o, typ, konf)
        Next
    End Sub


    Public Shared Function Otworz(sciezka As String) As Pulpit
        Try
            Return _Otworz(sciezka)
        Catch
            Return Nothing
        End Try
    End Function

    Private Shared Function _Otworz(sciezka As String) As Pulpit
        Dim p As Pulpit
        Dim konf As New KonfiguracjaOdczytu
        Dim segmenty As New List(Of SegmentPliku)
        konf.Kostki.Add(PUSTE_ODWOLANIE, Nothing)
        konf.OdcinkiTorow.Add(PUSTE_ODWOLANIE, Nothing)

        Using fs As New FileStream(sciezka, FileMode.Open, FileAccess.Read)
            Using br As New BinaryReader(fs)
                Dim b As Byte()

                'Nagłówek
                b = br.ReadBytes(NAGLOWEK.Length)
                If PobierzTekst(b) <> NAGLOWEK Then
                    Throw New OtwieraniePlikuException("Niepoprawny typ pliku.")
                End If

                Dim wersja_glowna As UShort = br.ReadUInt16
                Dim wersja_boczna As UShort = br.ReadUInt16
                Dim szer As UShort = br.ReadUInt16
                Dim wys As UShort = br.ReadUInt16
                p = New Pulpit(New WersjaPliku(wersja_glowna, wersja_boczna), szer, wys)
                p._SciezkaPliku = sciezka

                'Informacje o posterunku
                Dim data As Long = br.ReadInt64
                p._DataUtworzenia = Date.FromBinary(data)
                p.Adres = br.ReadUInt16
                p.Nazwa = OdczytajTekst(br)

                'Pulpit
                Do Until fs.Position >= fs.Length
                    Dim seg As SegmentPliku = UtworzObiekt(br, konf)
                    If seg.Obiekt IsNot Nothing Then segmenty.Add(seg)
                Loop

            End Using
        End Using

        For Each s As SegmentPliku In segmenty
            s.Obiekt.Otworz(s.Dane, konf, p)
        Next

        Return p
    End Function

    Private Shared Function UtworzObiekt(br As BinaryReader, konf As KonfiguracjaOdczytu) As SegmentPliku
        Dim typ As UShort = br.ReadUInt16
        Dim ile As UShort = br.ReadUInt16
        Dim b As Byte() = br.ReadBytes(ile)
        Dim ob As IObiektPliku = Nothing

        Select Case typ
            Case TypObiektuPliku.KOSTKA
                ob = Kostka.UtworzObiekt(b, konf)
            Case TypObiektuPliku.ODCINEK_TORU
                ob = OdcinekToru.UtworzObiekt(b, konf)
            Case TypObiektuPliku.LICZNIK_OSI
                ob = ParaLicznikowOsi.UtworzObiekt(b, konf)
            Case TypObiektuPliku.LAMPA
                ob = Lampa.UtworzObiekt(b, konf)
        End Select

        Return New SegmentPliku() With {.Dane = b, .Obiekt = ob}
    End Function

    Public Sub UsunKostkeZPowiazan(kostka As Kostka)
        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                If _Kostki(x, y) IsNot Nothing Then _Kostki(x, y).UsunPowiazanie(kostka)
            Next
        Next
    End Sub

    Public Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                If _Kostki(x, y) IsNot Nothing Then _Kostki(x, y).UsunOdcinekToruZPowiazan(odcinek)
            Next
        Next

        Dim en As List(Of ParaLicznikowOsi).Enumerator = _LicznikiOsi.GetEnumerator
        While en.MoveNext
            en.Current.UsunOdcinekToruZPowiazan(odcinek)
        End While
    End Sub

    Public Sub PowiekszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer)
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        Dim staraszer As Integer = _Szerokosc
        Dim starawys As Integer = _Wysokosc
        Dim przesx As Integer = 0
        Dim przesy As Integer = 0

        If kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo Then
            _Szerokosc += rozmiar
            If kierunek = KierunekEdycjiPulpitu.Lewo Then przesx = rozmiar
        Else
            _Wysokosc += rozmiar
            If kierunek = KierunekEdycjiPulpitu.Gora Then przesy = rozmiar
        End If

        Dim tab(_Szerokosc - 1, _Wysokosc - 1) As Kostka
        For x As Integer = 0 To staraszer - 1
            For y As Integer = 0 To starawys - 1
                tab(x + przesx, y + przesy) = _Kostki(x, y)
            Next
        Next

        _Kostki = tab
    End Sub

    Public Function PomniejszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer) As Boolean
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        If (kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo) And rozmiar >= _Szerokosc Then
            Throw New ArgumentException("Rozmiar musi być mniejszy niż szerokość pulpitu.")
        End If

        If (kierunek = KierunekEdycjiPulpitu.Gora Or kierunek = KierunekEdycjiPulpitu.Dol) And rozmiar >= _Wysokosc Then
            Throw New ArgumentException("Rozmiar musi być mniejszy niż wysokość pulpitu.")
        End If

        'Sprawdź, czy w usuwanym zakresie nie ma żadnych kostek
        Dim poczx As Integer = 0
        Dim koncx As Integer = _Szerokosc - 1
        Dim poczy As Integer = 0
        Dim koncy As Integer = _Wysokosc - 1
        Dim przesx As Integer = 0
        Dim przesy As Integer = 0
        Dim wysnowa As Integer = _Wysokosc
        Dim szernowa As Integer = _Szerokosc

        Select Case kierunek
            Case KierunekEdycjiPulpitu.Gora
                wysnowa -= rozmiar
                koncy = rozmiar - 1
                przesy = rozmiar
            Case KierunekEdycjiPulpitu.Prawo
                szernowa -= rozmiar
                poczx = szernowa
            Case KierunekEdycjiPulpitu.Dol
                wysnowa -= rozmiar
                poczy = wysnowa
            Case KierunekEdycjiPulpitu.Lewo
                szernowa -= rozmiar
                koncx = rozmiar - 1
                przesx = rozmiar
        End Select

        Dim puste As Boolean = True
        For x As Integer = poczx To koncx
            For y As Integer = poczy To koncy
                If _Kostki(x, y) IsNot Nothing Then
                    puste = False
                    Exit For
                End If
            Next
            If Not puste Then Exit For
        Next

        If Not puste Then Return False

        'Usuń komórki
        _Szerokosc = szernowa
        _Wysokosc = wysnowa

        Dim tab(_Szerokosc - 1, _Wysokosc - 1) As Kostka

        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                tab(x, y) = _Kostki(x + przesx, y + przesy)
            Next
        Next

        _Kostki = tab
        Return True
    End Function

End Class

Public Enum KierunekEdycjiPulpitu
    Gora
    Prawo
    Dol
    Lewo
End Enum