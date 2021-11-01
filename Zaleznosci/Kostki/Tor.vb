Public Class Tor
    Inherits Kostka

    Public Property PredkoscZasadnicza As Integer
    Public Property NalezyDoOdcinka As OdcinekToru

    Public Sub New()
        MyBase.New(TypKostki.Tor)
    End Sub
    Public Sub New(Typ As TypKostki)
        MyBase.New(Typ)
    End Sub
    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        If NalezyDoOdcinka Is odcinek Then NalezyDoOdcinka = Nothing
    End Sub
End Class