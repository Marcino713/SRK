Public Class Przycisk
    Inherits Kostka
    Implements IPrzycisk

    Private Const BLAD As String = "Nie można zmienić własności posiadania przycisku przez kostkę przycisku."

    Public Property TypPrzycisku As TypPrzyciskuEnum
    Public Property SygnalizatorPolsamoczynny As SygnalizatorPolsamoczynny
    Public Property SygnalizatorManewrowy As SygnalizatorManewrowy
    Public Property Kierunek As Kierunek
    Public Property Rozjazd As Rozjazd
    Public Property Przejazd As PrzejazdKolejowoDrogowy
    Public Property PosiadaPrzycisk As Boolean Implements IPrzycisk.PosiadaPrzycisk
        Get
            Return True
        End Get
        Set(value As Boolean)
            Throw New NotSupportedException(BLAD)
        End Set
    End Property

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety

    Public Sub New()
        MyBase.New(TypKostki.Przycisk)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorPolsamoczynny Is kostka Then SygnalizatorPolsamoczynny = Nothing
        If SygnalizatorManewrowy Is kostka Then SygnalizatorManewrowy = Nothing
        If Kierunek Is kostka Then Kierunek = Nothing
        If Rozjazd Is kostka Then Rozjazd = Nothing
    End Sub

    Protected Friend Overrides Sub UsunPrzejazdZPowiazan(przejazd As PrzejazdKolejowoDrogowy)
        If Me.Przejazd Is przejazd Then Me.Przejazd = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        bw.Write(CByte(TypPrzycisku))
        bw.Write(If(SygnalizatorPolsamoczynny Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorPolsamoczynny)))
        bw.Write(If(SygnalizatorManewrowy Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorManewrowy)))
        bw.Write(If(Kierunek Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(Kierunek)))
        bw.Write(If(Rozjazd Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(Rozjazd)))
        bw.Write(If(Przejazd Is Nothing, PUSTE_ODWOLANIE, konf.Przejazdy(Przejazd)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        TypPrzycisku = CType(br.ReadByte, TypPrzyciskuEnum)
        SygnalizatorPolsamoczynny = CType(konf.Kostki(br.ReadInt32), SygnalizatorPolsamoczynny)
        SygnalizatorManewrowy = CType(konf.Kostki(br.ReadInt32), SygnalizatorManewrowy)
        Kierunek = CType(konf.Kostki(br.ReadInt32), Kierunek)
        Rozjazd = CType(konf.Kostki(br.ReadInt32), Rozjazd)
        Przejazd = konf.Przejazdy(br.ReadInt32)
    End Sub
End Class

Public Enum TypPrzyciskuEnum
    SygnalZastepczy
    ZwolnieniePrzebiegu
    ZwolnieniePrzebieguManewrowegoZSygnPolsamoczynnego
    ZwolnieniePrzebieguManewrowegoZSygnManewrowego
    WlaczenieSBL
    PotwierdzenieSBL
    ZwolnienieSBL
    KasowanieRozprucia
    ZamknieciePrzejazdu
    OtwarciePrzejazdu
End Enum