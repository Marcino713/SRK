Public Class SygnalizatorSamoczynny
    Inherits SygnalizatorWylaczanyPoPrzejechaniu

    Public Property Stan As StanSygnalizatoraSamoczynnego = StanSygnalizatoraSamoczynnego.BrakWyjazdu

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorSamoczynny)
    End Sub

End Class

Public Enum StanSygnalizatoraSamoczynnego
    BrakWyjazdu = 1
    Zezwalajacy = 2
End Enum