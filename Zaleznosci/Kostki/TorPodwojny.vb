Public MustInherit Class TorPodwojny
    Inherits Tor

    Public Property PredkoscDrugi As UShort = 0
    Public Property DlugoscDrugi As Single = 0.0F
    Public Property ZelektryfikowanyDrugi As Boolean = True
    Public Property KontrolaNiezajetosciDrugi As Boolean = True

    Private _ZajetoscDrugi As ZajetoscToru = ZajetoscToru.Wolny
    Public Property ZajetoscDrugi As ZajetoscToru
        Get
            Return _ZajetoscDrugi
        End Get
        Set(value As ZajetoscToru)
            _ZajetoscDrugi = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property

    Public Overridable Property RysowanieDodatkowychTrojkatowDrugi As DodatkoweTrojkatyTor

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Public Overrides Function CzyMiga() As Boolean
        Return MyBase.CzyMiga OrElse _ZajetoscDrugi = ZajetoscToru.BlokadaNieustawiona
    End Function

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(PredkoscDrugi)
        bw.Write(DlugoscDrugi)
        bw.Write(ZelektryfikowanyDrugi)
        bw.Write(KontrolaNiezajetosciDrugi)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        PredkoscDrugi = br.ReadUInt16
        DlugoscDrugi = br.ReadSingle
        ZelektryfikowanyDrugi = br.ReadBoolean
        KontrolaNiezajetosciDrugi = br.ReadBoolean
    End Sub
End Class