Public Class Przycisk
    Inherits Kostka
    Public Property TypPrzycisku As TypPrzyciskuEnum
    Public Property ObslugiwanySygnalizator As SygnalizatorPolsamoczynny
    Public Sub New()
        MyBase.New(TypKostki.Przycisk)
    End Sub
End Class

Public Enum TypPrzyciskuEnum
    SygnalZastepczy
    ZwolnieniePrzebiegow
End Enum