Friend Class ObslugiwanyPosterunek
    Public Property NazwaPosterunku As String
    Public Property NazwaPliku As String
    Public Property Adres As UShort
    Public Property Polaczenie As PolaczenieTCP
    Public Property Zawartosc As Byte()
    Public Property DanePulpitu As Pulpit
    Public Property Obserwatorzy As New Dictionary(Of String, PolaczenieTCP)
End Class