Public Class PrzyciskTor
    Inherits Tor

    Public Property TypPrzycisku As TypPrzyciskuTorEnum
    Public Property ObslugiwanySygnalizator As Sygnalizator

    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub
    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If ObslugiwanySygnalizator Is kostka Then ObslugiwanySygnalizator = Nothing
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    SygnalizatorPolsamoczynny
    SygnalizatorManewrowy
    SygnalManewrowy     'sygnał manewrowy na sygnalizatorze półsamoczynnym
End Enum