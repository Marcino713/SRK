Public MustInherit Class Sygnalizator
    Inherits Kostka
    Public Property Adres As Integer
    Public Property Nazwa As String = ""
    Public Property OdcinekNastepujacy As OdcinekToru
    Public Event ZmienionoSygnal()
    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub
End Class