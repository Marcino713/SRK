Public MustInherit Class SygnalizatorUzalezniony
    Inherits SygnalizatorWylaczanyPoPrzejechaniu

    Public Property SygnalizatorNastepny As Sygnalizator

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorNastepny Is kostka Then SygnalizatorNastepny = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(If(SygnalizatorNastepny Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorNastepny)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Dim id As Integer = br.ReadInt32
        SygnalizatorNastepny = CType(konf.Kostki(id), Sygnalizator)
    End Sub

End Class