Public MustInherit Class Rozjazd
    Inherits Kostka
    Public Property PredkoscGlowna As Integer
    Public Property PredkoscBoczna As Integer
    Public Property Numer As Integer
    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub
End Class