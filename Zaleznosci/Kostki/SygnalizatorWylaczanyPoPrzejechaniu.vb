Public MustInherit Class SygnalizatorWylaczanyPoPrzejechaniu
    Inherits Sygnalizator

    Public Property OdcinekNastepujacy As OdcinekToru

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        MyBase.UsunOdcinekToruZPowiazan(odcinek)
        If OdcinekNastepujacy Is odcinek Then OdcinekNastepujacy = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(If(OdcinekNastepujacy Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(OdcinekNastepujacy)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Dim id As Integer = br.ReadInt32
        OdcinekNastepujacy = konf.OdcinkiTorow(id)
    End Sub
End Class