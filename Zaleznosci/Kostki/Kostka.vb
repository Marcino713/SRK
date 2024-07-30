Imports Zaleznosci.PlikiPulpitu
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPulpitu.KonfiguracjaZapisu, Zaleznosci.PlikiPulpitu.KonfiguracjaOdczytu)

Public MustInherit Class Kostka
    Implements IObiektPlikuTyp

    Private Delegate Function UtworzKostke() As Kostka

    Private Shared ReadOnly TworzycieleKostek As New Dictionary(Of TypKostki, UtworzKostke) From {
        {TypKostki.Tor, Function() New Tor},
        {TypKostki.TorKoniec, Function() New TorKoniec},
        {TypKostki.Zakret, Function() New Zakret},
        {TypKostki.RozjazdLewo, Function() New RozjazdLewo},
        {TypKostki.RozjazdPrawo, Function() New RozjazdPrawo},
        {TypKostki.SygnalizatorManewrowy, Function() New SygnalizatorManewrowy},
        {TypKostki.SygnalizatorPolsamoczynny, Function() New SygnalizatorPolsamoczynny},
        {TypKostki.SygnalizatorSamoczynny, Function() New SygnalizatorSamoczynny},
        {TypKostki.Przycisk, Function() New Przycisk},
        {TypKostki.PrzyciskTor, Function() New PrzyciskTor},
        {TypKostki.Kierunek, Function() New Kierunek},
        {TypKostki.Napis, Function() New Napis},
        {TypKostki.SygnalizatorPowtarzajacy, Function() New SygnalizatorPowtarzajacy},
        {TypKostki.SygnalizatorOstrzegawczyPrzejazdowy, Function() New SygnalizatorOstrzegawczyPrzejazdowy},
        {TypKostki.PrzejazdKolejowy, Function() New PrzejazdKolejowoDrogowyKostka}
    }

    Public ReadOnly Property Typ As TypKostki
    Public Property Obrot As Integer
    Public Property Migacz As IMigacz

    Public Shared Function CzyRozjazd(k As Kostka) As Boolean
        Return TypeOf k Is Rozjazd
    End Function

    Public Shared Function CzySygnalizator(k As Kostka) As Boolean
        Return TypeOf k Is Sygnalizator
    End Function

    Public Shared Function CzySygnalizatorUzalezniony(k As Kostka) As Boolean
        Return TypeOf k Is SygnalizatorUzalezniony
    End Function

    Public Shared Function CzySygnalizatorPolsamoczynny(k As Kostka) As Boolean
        Return TypeOf k Is SygnalizatorPolsamoczynny
    End Function

    Public Shared Function CzySygnalizatorManewrowy(k As Kostka) As Boolean
        Return TypeOf k Is SygnalizatorManewrowy
    End Function

    Public Shared Function CzySygnalizatorTOP(k As Kostka) As Boolean
        Return TypeOf k Is SygnalizatorOstrzegawczyPrzejazdowy
    End Function

    Public Shared Function CzyKierunek(k As Kostka) As Boolean
        Return TypeOf k Is Kierunek
    End Function

    Public Shared Function CzyTorBezRozjazdu(k As Kostka) As Boolean
        Return TypeOf k Is Tor AndAlso TypeOf k IsNot Rozjazd
    End Function

    Public Shared Function CzyTor(k As Kostka) As Boolean
        Return TypeOf k Is Tor OrElse TypeOf k Is TorKoniec
    End Function

    Public Sub New(typ As TypKostki)
        Me.Typ = typ
    End Sub

    Public Overridable Function CzyMiga() As Boolean
        Return False
    End Function

    Protected Friend Overridable Sub UsunPowiazanie(kostka As Kostka)
    End Sub

    Protected Friend Overridable Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
    End Sub

    Protected Friend Overridable Sub UsunPrzejazdZPowiazan(przejazd As PrzejazdKolejowoDrogowy)
    End Sub

    Friend MustOverride Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(CUShort(Typ))
                bw.Write(konf.Kostki(Me))
                bw.Write(konf.X)
                bw.Write(konf.Y)
                bw.Write(CUShort(Obrot))
                ZapiszKostke(bw, konf)

                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPlikuTyp
        Dim typ As TypKostki = CType(PobierzInt32(dane, 0, 2), TypKostki)
        Dim id As Integer = PobierzInt32(dane, 2, 4)
        Dim k As Kostka = Nothing
        Dim metodaTworzaca As UtworzKostke = Nothing

        If TworzycieleKostek.TryGetValue(typ, metodaTworzaca) Then
            k = metodaTworzaca()
            konf.Kostki.Add(id, k)
            Return k
        End If

        Return Nothing
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
    SygnalizatorPowtarzajacy = 13
    SygnalizatorOstrzegawczyPrzejazdowy = 14
    PrzejazdKolejowy = 15
End Enum