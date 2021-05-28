Public Class Tor
    Inherits Kostka
    Public Property Predkosc As Integer
    Public Sub New()
        MyBase.New(TypKostki.Tor)
    End Sub
    Public Sub New(Typ As TypKostki)
        MyBase.New(Typ)
    End Sub
End Class