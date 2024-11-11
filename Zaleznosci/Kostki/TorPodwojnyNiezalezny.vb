Public MustInherit Class TorPodwojnyNiezalezny
    Inherits TorPodwojny

    Public Property NalezyDoOdcinkaDrugi As OdcinekToru = Nothing

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        MyBase.UsunOdcinekToruZPowiazan(odcinek)
        If NalezyDoOdcinkaDrugi Is odcinek Then NalezyDoOdcinkaDrugi = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(If(NalezyDoOdcinkaDrugi Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(NalezyDoOdcinkaDrugi)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Dim id As Integer = br.ReadInt32
        NalezyDoOdcinkaDrugi = konf.OdcinkiTorow(id)
        NalezyDoOdcinkaDrugi?.DodajTor(Me, PrzynaleznoscToruDoOdcinka.Drugi)
    End Sub
End Class