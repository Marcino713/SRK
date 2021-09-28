Public MustInherit Class Rozjazd
    Inherits Tor
    Private Const LICZBA_ROZJAZDOW_ZALEZNYCH As Integer = 2

    Public Property PredkoscBoczna As Integer
    Public Property Nazwa As String = ""
    Public Property Adres As Integer
    Public Property ZaleznosciJesliWprost As KonfiguracjaRozjazduZaleznego()
    Public Property ZaleznosciJesliBok As KonfiguracjaRozjazduZaleznego()
    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
        ZaleznosciJesliWprost = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
        ZaleznosciJesliBok = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
    End Sub

    Public Overrides Sub UsunPowiazanie(kostka As Kostka)
        For i As Integer = 0 To ZaleznosciJesliWprost.Length - 1
            If ZaleznosciJesliWprost(i).RozjazdZalezny Is kostka Then ZaleznosciJesliWprost(i).RozjazdZalezny = Nothing
        Next

        For i As Integer = 0 To ZaleznosciJesliBok.Length - 1
            If ZaleznosciJesliBok(i).RozjazdZalezny Is kostka Then ZaleznosciJesliBok(i).RozjazdZalezny = Nothing
        Next
    End Sub

    Private Function PobierzDomyslnaKonfiguracje(ile As Integer) As KonfiguracjaRozjazduZaleznego()
        Dim t(ile - 1) As KonfiguracjaRozjazduZaleznego
        For i As Integer = 0 To ile - 1
            t(i) = New KonfiguracjaRozjazduZaleznego()
        Next
        Return t
    End Function
End Class

Public Class KonfiguracjaRozjazduZaleznego
    Public RozjazdZalezny As Rozjazd
    Public Konfiguracja As UstawienieRozjazduZaleznegoEnum
End Class

Public Enum UstawienieRozjazduZaleznegoEnum
    Wprost
    Bok
End Enum