Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPulpitu, Zaleznosci.KonfiguracjaOdczytuPulpitu)

Public Class OdcinekToru
    Implements IObiektPlikuTyp

    Public Delegate Sub PrzetworzOdcinekToru(kostka As Tor, przynaleznosc As PrzynaleznoscToruDoOdcinka)

    Private _kostkiTory As New Dictionary(Of Tor, PrzynaleznoscToruDoOdcinka)

    Public Property Adres As UShort = 0
    Public Property Nazwa As String = ""
    Public Property Opis As String = ""

    Private _liczbaTorow As Integer
    Public ReadOnly Property LiczbaTorow As Integer
        Get
            Return _liczbaTorow
        End Get
    End Property

    Public Sub DodajTor(kostka As Tor, element As PrzynaleznoscToruDoOdcinka)
        Dim stary As PrzynaleznoscToruDoOdcinka
        Dim nowy As PrzynaleznoscToruDoOdcinka
        element = PobierzWartoscWZakresie(kostka, element)

        If _kostkiTory.TryGetValue(kostka, stary) Then
            nowy = stary Or element
            _kostkiTory(kostka) = nowy
        ElseIf element > 0 Then
            nowy = element
            _kostkiTory.Add(kostka, nowy)
        End If

        If CzyJest(nowy, stary, PrzynaleznoscToruDoOdcinka.Pierwszy) Then _liczbaTorow += 1
        If CzyJest(nowy, stary, PrzynaleznoscToruDoOdcinka.Drugi) Then _liczbaTorow += 1
    End Sub

    Public Sub UsunTor(kostka As Tor, element As PrzynaleznoscToruDoOdcinka)
        Dim stary As PrzynaleznoscToruDoOdcinka

        If _kostkiTory.TryGetValue(kostka, stary) Then
            Dim nowy As PrzynaleznoscToruDoOdcinka

            nowy = stary And (Not PobierzWartoscWZakresie(kostka, element))
            If nowy = 0 Then
                _kostkiTory.Remove(kostka)
            Else
                _kostkiTory(kostka) = nowy
            End If

            If CzyJest(stary, nowy, PrzynaleznoscToruDoOdcinka.Pierwszy) Then _liczbaTorow -= 1
            If CzyJest(stary, nowy, PrzynaleznoscToruDoOdcinka.Drugi) Then _liczbaTorow -= 1
        End If
    End Sub

    Public Sub PrzeiterujTory(metoda As PrzetworzOdcinekToru)
        For Each kv As KeyValuePair(Of Tor, PrzynaleznoscToruDoOdcinka) In _kostkiTory
            metoda(kv.Key, kv.Value)
        Next
    End Sub

    Friend Function Zapisz(konf As KonfiguracjaZapisuPulpitu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(konf.OdcinkiTorow(Me))
                bw.Write(Adres)
                ZapiszTekst(bw, Nazwa)
                ZapiszTekst(bw, Opis)

                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytuPulpitu) As IObiektPlikuTyp
        Dim id As Integer = PobierzInt32(dane, 0, 4)
        Dim odc As New OdcinekToru
        konf.OdcinkiTorow.Add(id, odc)

        Return odc
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytuPulpitu) Implements IObiektPlikuTyp.Otworz
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                ms.Seek(4, SeekOrigin.Begin)
                Adres = br.ReadUInt16()
                Nazwa = OdczytajTekst(br)
                Opis = OdczytajTekst(br)
            End Using
        End Using

        konf.Pulpit.OdcinkiTorow.Add(Me)
    End Sub

    Private Function PobierzWartoscWZakresie(kostka As Tor, el As PrzynaleznoscToruDoOdcinka) As PrzynaleznoscToruDoOdcinka
        Dim wartosc As PrzynaleznoscToruDoOdcinka

        If TypeOf kostka Is TorPodwojny Then
            wartosc = PrzynaleznoscToruDoOdcinka.Oba
        Else
            wartosc = PrzynaleznoscToruDoOdcinka.Pierwszy
        End If

        Return el And wartosc
    End Function

    Private Function CzyJest(jest As PrzynaleznoscToruDoOdcinka, nieMa As PrzynaleznoscToruDoOdcinka, sprawdzane As PrzynaleznoscToruDoOdcinka) As Boolean
        Return ((jest And sprawdzane) = sprawdzane) And ((nieMa And sprawdzane) = 0)
    End Function
End Class

Public Enum PrzynaleznoscToruDoOdcinka
    Zaden = 0
    Pierwszy = 1
    Drugi = 2
    Oba = Pierwszy Or Drugi
End Enum