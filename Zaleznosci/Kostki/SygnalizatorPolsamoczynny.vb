Public Class SygnalizatorPolsamoczynny
    Inherits SygnalizatorWylaczanyPoPrzejechaniu

    Private Const USTAWIENIA_DOMYSLNE As UstawieniaSygnalizatoraPolsamoczynnego = UstawieniaSygnalizatoraPolsamoczynnego.DostepneManewry
    Private Const DOSTEPNE_SWIATLA_DOMYSLNE As DostepneSwiatlaSygnPolsamoczynny = DostepneSwiatlaSygnPolsamoczynny.Zielone Or DostepneSwiatlaSygnPolsamoczynny.Czerwone Or DostepneSwiatlaSygnPolsamoczynny.PomaranczoweDol Or DostepneSwiatlaSygnPolsamoczynny.Biale

    Public Property Ustawienia As UstawieniaSygnalizatoraPolsamoczynnego = USTAWIENIA_DOMYSLNE
    Public Property DostepneSwiatla As DostepneSwiatlaSygnPolsamoczynny = DOSTEPNE_SWIATLA_DOMYSLNE

    Public Property Zablokowany As Boolean
    Public Property TrybSamoczynny As Boolean

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
        bw.Write(CUShort(Ustawienia))
        bw.Write(CUShort(DostepneSwiatla))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Ustawienia = CType(br.ReadUInt16, UstawieniaSygnalizatoraPolsamoczynnego)
        DostepneSwiatla = CType(br.ReadUInt16, DostepneSwiatlaSygnPolsamoczynny)
    End Sub
End Class

<Flags>
Public Enum UstawieniaSygnalizatoraPolsamoczynnego
    DostepneManewry = 1 << 0
    BrakDrogiHamowania = 1 << 1
End Enum

<Flags>
Public Enum DostepneSwiatlaSygnPolsamoczynny
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