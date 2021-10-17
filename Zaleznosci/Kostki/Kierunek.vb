Public Class Kierunek
    Inherits Tor
    Public Property KierunekWlaczany As KierunekWlaczanyEnum
    Public Sub New()
        MyBase.New(TypKostki.Kierunek)
    End Sub
End Class

Public Enum KierunekWlaczanyEnum
    Zasadniczy
    Przeciwny
End Enum