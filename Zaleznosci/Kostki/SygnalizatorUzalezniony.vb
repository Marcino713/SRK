Public MustInherit Class SygnalizatorUzalezniony
    Inherits Sygnalizator
    Public Property SygnalizatorNastepny As Sygnalizator
    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub
    Public Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorNastepny Is kostka Then SygnalizatorNastepny = Nothing
    End Sub
End Class
