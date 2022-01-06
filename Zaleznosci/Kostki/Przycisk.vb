Imports Zaleznosci.PlikiPulpitu

Public Class Przycisk
    Inherits Kostka

    Public Property TypPrzycisku As TypPrzyciskuEnum
    Public Property ObslugiwanySygnalizator As SygnalizatorPolsamoczynny

    Public Sub New()
        MyBase.New(TypKostki.Przycisk)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If ObslugiwanySygnalizator Is kostka Then ObslugiwanySygnalizator = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        bw.Write(CType(TypPrzycisku, Byte))
        bw.Write(If(ObslugiwanySygnalizator Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(ObslugiwanySygnalizator)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        TypPrzycisku = CType(br.ReadByte, TypPrzyciskuEnum)
        Dim id As Integer = br.ReadInt32
        ObslugiwanySygnalizator = CType(konf.Kostki(id), SygnalizatorPolsamoczynny)
    End Sub
End Class

Public Enum TypPrzyciskuEnum
    SygnalZastepczy
    ZwolnieniePrzebiegow
End Enum