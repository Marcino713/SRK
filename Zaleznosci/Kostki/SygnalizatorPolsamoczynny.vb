Public Class SygnalizatorPolsamoczynny
    Inherits SygnalizatorUzalezniony

    Public Property DostepneSwiatla As DostepneSwiatlaEnum

    Private _Stan As StanSygnalizatora = StanSygnalizatora.BrakWyjazdu
    Public Property Stan As StanSygnalizatora
        Get
            Return _Stan
        End Get
        Set(value As StanSygnalizatora)
            _Stan = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorPolsamoczynny)
    End Sub

    Public Overrides Function CzyMiga() As Boolean
        Return MyBase.CzyMiga() OrElse _Stan = StanSygnalizatora.Zastepczy
    End Function

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CUShort(DostepneSwiatla))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        DostepneSwiatla = CType(br.ReadUInt16, DostepneSwiatlaEnum)
    End Sub
End Class

<Flags>
Public Enum DostepneSwiatlaEnum
    Zielone = 1 << 0
    PomaranczoweGora = 1 << 1
    Czerwone = 1 << 2
    PomaranczoweDol = 1 << 3
    Biale = 1 << 4
    ZielonyPas = 1 << 5
    PomaranczowyPas = 1 << 6
    WskaznikKierunkuPrzeciwnego = 1 << 7
End Enum

Public Enum StanSygnalizatora As Byte
    BrakWyjazdu = 1
    Zezwalajacy = 2
    Manewrowy = 3
    Zastepczy = 4
End Enum