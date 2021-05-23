Public MustInherit Class Sygnalizator
    Inherits Kostka
    Public Property Nazwa As String
    Public Event ZmienionoSygnal()
    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub
End Class