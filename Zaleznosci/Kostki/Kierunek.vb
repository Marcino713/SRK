Public Class Kierunek
    Inherits Kostka
    Public Property KierunekWlaczany As KierunekWlaczanyEnum
    Public Sub New()
        MyBase.New(TypKostki.Kierunek)
    End Sub
End Class

Public Enum KierunekWlaczanyEnum
    Zasadniczy
    Przeciwny
End Enum