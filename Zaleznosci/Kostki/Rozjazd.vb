Public MustInherit Class Rozjazd
    Inherits Kostka
    Public Property PredkoscGlowna As Integer
    Public Property PredkoscBoczna As Integer
    Public Property Numer As Integer
    Public Property Adres As Integer
    Public Property ZaleznosciJesliWprost As KonfiguracjaRozjazduZaleznego()
    Public Property ZaleznosciJesliBok As KonfiguracjaRozjazduZaleznego()
    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub
End Class

Public Class KonfiguracjaRozjazduZaleznego
    Public RozjazdZalezny As Rozjazd
    Public Konfiguracja As UstawienieRozjazduZaleznegoEnum
End Class

Public Enum UstawienieRozjazduZaleznegoEnum
    Wprost
    Bok
End Enum