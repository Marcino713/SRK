Public MustInherit Class SygnalizatorInformujacy
    Inherits Sygnalizator

    Private Const DOSTEPNE_SWIATLA_DOMYSLNE As DostepneSwiatlaSygnInformujacy = DostepneSwiatlaSygnInformujacy.Zielone Or DostepneSwiatlaSygnInformujacy.Pomaranczowe

    Public Property SygnalizatorPowtarzany As SygnalizatorPolsamoczynny
    Public Property DostepneSwiatla As DostepneSwiatlaSygnInformujacy = DOSTEPNE_SWIATLA_DOMYSLNE

    Public Property Stan As StanSygnalizatoraInformujacego = StanSygnalizatoraInformujacego.BrakWyjazdu

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorPowtarzany Is kostka Then SygnalizatorPowtarzany = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(If(SygnalizatorPowtarzany Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorPowtarzany)))
        bw.Write(CByte(DostepneSwiatla))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Dim id As Integer = br.ReadInt32
        SygnalizatorPowtarzany = CType(konf.Kostki(id), SygnalizatorPolsamoczynny)
        DostepneSwiatla = CType(br.ReadByte, DostepneSwiatlaSygnInformujacy)
    End Sub
End Class

<Flags>
Public Enum DostepneSwiatlaSygnInformujacy
    Zielone = 1 << 0
    Pomaranczowe = 1 << 1
End Enum

Public Enum StanSygnalizatoraInformujacego
    BrakWyjazdu = 1
    Zezwalajacy = 2
End Enum