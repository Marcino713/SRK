Imports Zaleznosci.PlikiPulpitu

Public Class PrzyciskTor
    Inherits Tor
    Implements IPrzycisk

    Public Property TypPrzycisku As TypPrzyciskuTorEnum
    Public Property ObslugiwanySygnalizator As Sygnalizator

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety

    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If ObslugiwanySygnalizator Is kostka Then ObslugiwanySygnalizator = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CType(TypPrzycisku, Byte))
        bw.Write(If(ObslugiwanySygnalizator Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(ObslugiwanySygnalizator)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        TypPrzycisku = CType(br.ReadByte, TypPrzyciskuTorEnum)
        Dim id As Integer = br.ReadInt32
        ObslugiwanySygnalizator = CType(konf.Kostki(id), Sygnalizator)
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    SygnalizatorPolsamoczynny
    SygnalizatorManewrowy
    SygnalManewrowy     'sygnał manewrowy na sygnalizatorze półsamoczynnym
End Enum