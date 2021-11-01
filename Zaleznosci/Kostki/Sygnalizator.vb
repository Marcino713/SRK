Public MustInherit Class Sygnalizator
    Inherits Tor
    Public Property Adres As Integer = 0
    Public Property Nazwa As String = ""
    Public Property OdcinekNastepujacy As OdcinekToru

    Public Event ZmienionoSygnal()

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub
    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        If NalezyDoOdcinka Is odcinek Then NalezyDoOdcinka = Nothing
        If OdcinekNastepujacy Is odcinek Then OdcinekNastepujacy = Nothing
    End Sub
End Class