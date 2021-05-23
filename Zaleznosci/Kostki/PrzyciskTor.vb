Public Class PrzyciskTor
    Inherits Kostka
    Public Property TypPrzycisku As TypPrzyciskuTorEnum
    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    SygnalizatorPolsamoczynny
    SygnalizatorManewrowy
    SygnalManewrowy
End Enum