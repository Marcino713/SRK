Imports Zaleznosci.PlikiPulpitu

Public Class PrzyciskTor
    Inherits Tor
    Implements IPrzycisk

    Public Property TypPrzycisku As TypPrzyciskuTorEnum

    Public Property SygnalizatorPolsamoczynny As SygnalizatorPolsamoczynny
    Public Property SygnalizatorManewrowy As SygnalizatorManewrowy

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety

    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorPolsamoczynny Is kostka Then SygnalizatorPolsamoczynny = Nothing
        If SygnalizatorManewrowy Is kostka Then SygnalizatorManewrowy = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CByte(TypPrzycisku))
        bw.Write(If(SygnalizatorPolsamoczynny Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorPolsamoczynny)))
        bw.Write(If(SygnalizatorManewrowy Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorManewrowy)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        TypPrzycisku = CType(br.ReadByte, TypPrzyciskuTorEnum)
        SygnalizatorPolsamoczynny = CType(konf.Kostki(br.ReadInt32), SygnalizatorPolsamoczynny)
        SygnalizatorManewrowy = CType(konf.Kostki(br.ReadInt32), SygnalizatorManewrowy)
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    JazdaSygnalizatorPolsamoczynny
    ManewrySygnalizatorPolsamoczynny
    ManewrySygnalizatorManewrowy
End Enum