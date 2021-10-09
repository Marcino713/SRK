Public Class Tor
    Inherits Kostka
    Implements ITor

    Public Property PredkoscZasadnicza As Integer Implements ITor.PredkoscZasadnicza
    Public Property NalezyDoOdcinka As OdcinekToru Implements ITor.NalezyDoOdcinka

    Public Sub New()
        MyBase.New(TypKostki.Tor)
    End Sub
    Public Sub New(Typ As TypKostki)
        MyBase.New(Typ)
    End Sub
End Class