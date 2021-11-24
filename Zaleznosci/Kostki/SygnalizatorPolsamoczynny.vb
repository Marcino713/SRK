Public Class SygnalizatorPolsamoczynny
    Inherits SygnalizatorUzalezniony

    Public Property DostepneSwiatla As DostepneSwiatlaEnum

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