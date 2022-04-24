Imports Zaleznosci.PlikiPulpitu

Public Class SygnalizatorPolsamoczynny
    Inherits SygnalizatorUzalezniony

    Public Property DostepneSwiatla As DostepneSwiatlaEnum

    Public Property Stan As StanSygnalizatora = StanSygnalizatora.BrakWyjazdu

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorPolsamoczynny)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CType(DostepneSwiatla, UShort))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        DostepneSwiatla = CType(br.ReadUInt16, DostepneSwiatlaEnum)
    End Sub
End Class

Public Enum DostepneSwiatlaEnum
    Zielone = 1 << 1
    PomaranczoweGora = 1 << 2
    Czerwone = 1 << 3
    PomaranczoweDol = 1 << 4
    Biale = 1 << 5
    ZielonyPas = 1 << 6
    PomaranczowyPas = 1 << 7
End Enum

Public Enum StanSygnalizatora As Byte
    BrakWyjazdu = 1
    Zezwalajacy = 2
    Manewrowy = 3
    Zastepczy = 4
End Enum