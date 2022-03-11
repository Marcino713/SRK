Public MustInherit Class ZarzadzanieTCP

    Public ReadOnly Property Wersja As New WersjaPliku(0, 1)
    Public ReadOnly Property ObslugiwaneWersje As WersjaPliku() = {New WersjaPliku(0, 1)}
    Protected Friend DaneFabrykiObiektow As New Dictionary(Of UShort, PrzetwOdebrKomunikatu)

End Class