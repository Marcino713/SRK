Public Class PrzyciskTor
    Inherits Kostka
    Implements ITor

    Public Property TypPrzycisku As TypPrzyciskuTorEnum
    Public Property ObslugiwanySygnalizator As Sygnalizator
    Public Property PredkoscZasadnicza As Integer Implements ITor.PredkoscZasadnicza
    Public Property NalezyDoOdcinka As OdcinekToru Implements ITor.NalezyDoOdcinka

    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub
    Public Overrides Sub UsunPowiazanie(kostka As Kostka)
        If ObslugiwanySygnalizator Is kostka Then ObslugiwanySygnalizator = Nothing
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    SygnalizatorPolsamoczynny
    SygnalizatorManewrowy
    SygnalManewrowy     'sygnał manewrowy na sygnalizatorze półsamoczynnym
End Enum