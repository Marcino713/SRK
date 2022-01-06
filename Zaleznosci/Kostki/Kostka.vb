Imports Zaleznosci.PlikiPulpitu
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPulpitu.KonfiguracjaZapisu, Zaleznosci.PlikiPulpitu.KonfiguracjaOdczytu)

Public MustInherit Class Kostka
    Implements IObiektPlikuTyp

    Public ReadOnly Property Typ As TypKostki
    Public Property Obrot As Integer

    Public Sub New(typ As TypKostki)
        Me.Typ = typ
    End Sub

    Protected Friend Overridable Sub UsunPowiazanie(kostka As Kostka)
    End Sub

    Protected Friend Overridable Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
    End Sub

    Friend MustOverride Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(CType(Typ, UShort))
                bw.Write(konf.Kostki(Me))
                bw.Write(konf.X)
                bw.Write(konf.Y)
                bw.Write(CType(Obrot, UShort))
                ZapiszKostke(bw, konf)
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPlikuTyp
        Dim typ As TypKostki = CType(PobierzInt32(dane, 0, 2), TypKostki)
        Dim id As Integer = PobierzInt32(dane, 2, 4)
        Dim k As Kostka = Nothing

        Select Case typ
            Case TypKostki.Tor
                k = New Tor
            Case TypKostki.TorKoniec
                k = New TorKoniec
            Case TypKostki.Zakret
                k = New Zakret
            Case TypKostki.RozjazdLewo
                k = New RozjazdLewo
            Case TypKostki.RozjazdPrawo
                k = New RozjazdPrawo
            Case TypKostki.SygnalizatorManewrowy
                k = New SygnalizatorManewrowy
            Case TypKostki.SygnalizatorPolsamoczynny
                k = New SygnalizatorPolsamoczynny
            Case TypKostki.SygnalizatorSamoczynny
                k = New SygnalizatorSamoczynny
            Case TypKostki.Przycisk
                k = New Przycisk
            Case TypKostki.PrzyciskTor
                k = New PrzyciskTor
            Case TypKostki.Kierunek
                k = New Kierunek
            Case TypKostki.Napis
                k = New Napis
        End Select

        konf.Kostki.Add(id, k)
        Return k
    End Function

    Friend MustOverride Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytu) Implements IObiektPlikuTyp.Otworz
        Dim x As UShort
        Dim y As UShort
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                ms.Seek(6, SeekOrigin.Begin)
                x = br.ReadUInt16
                y = br.ReadUInt16
                Obrot = br.ReadUInt16
                OtworzKostke(br, konf)
            End Using
        End Using
        konf.Pulpit.Kostki(x, y) = Me
    End Sub
End Class

Public Enum TypKostki
    Tor = 1
    TorKoniec = 2
    Zakret = 3
    RozjazdLewo = 4
    RozjazdPrawo = 5
    SygnalizatorManewrowy = 6
    SygnalizatorPolsamoczynny = 7
    SygnalizatorSamoczynny = 8
    Przycisk = 9
    PrzyciskTor = 10
    Kierunek = 11
    Napis = 12
End Enum