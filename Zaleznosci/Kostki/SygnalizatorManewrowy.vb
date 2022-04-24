Public Class SygnalizatorManewrowy
    Inherits Sygnalizator

    Public Property Stan As StanSygnalizatoraManewrowego = StanSygnalizatoraManewrowego.BrakWyjazdu

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorManewrowy)
    End Sub

End Class

Public Enum StanSygnalizatoraManewrowego
    BrakWyjazdu = 1
    Manewrowy = 3
End Enum