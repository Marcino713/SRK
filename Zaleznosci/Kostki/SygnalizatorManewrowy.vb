Public Class SygnalizatorManewrowy
    Inherits SygnalizatorWylaczanyPoPrzejechaniu
    Implements IPrzycisk

    Public Property PosiadaPrzycisk As Boolean = True Implements IPrzycisk.PosiadaPrzycisk

    Public Property Stan As StanSygnalizatoraManewrowego = StanSygnalizatoraManewrowego.BrakWyjazdu
    Public Property Wcisniety As Boolean Implements IPrzycisk.Wcisniety
    Public Property Zablokowany As Boolean Implements IPrzycisk.Zablokowany

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorManewrowy)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(PosiadaPrzycisk)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        PosiadaPrzycisk = br.ReadBoolean
    End Sub

End Class

Public Enum StanSygnalizatoraManewrowego
    BrakWyjazdu = 1
    Manewrowy = 3
End Enum