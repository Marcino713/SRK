Public Class PrzyciskTor
    Inherits Kostka
    Public Property TypPrzycisku As TypPrzyciskuTorEnum
    Public Property ObslugiwanySygnalizator As Sygnalizator
    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    SygnalizatorPolsamoczynny
    SygnalizatorManewrowy
    SygnalManewrowy     'sygnał manewrowy na sygnalizatorze półsamoczynnym
End Enum