Public Class SygnalizatorSamoczynny
    Inherits Sygnalizator
    Public Property SygnalizatorNastepny As Sygnalizator
    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorSamoczynny)
    End Sub
End Class