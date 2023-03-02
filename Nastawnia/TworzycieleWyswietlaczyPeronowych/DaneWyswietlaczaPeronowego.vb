Friend Class DaneWyswietlaczaPeronowego
    Public Przewoznik As String
    Public Kategoria As String
    Public Numer As String
    Public Nazwa As String
    Public Stacja As String
    Public Typ As TypPostojuPociagu
    Public Czas As TimeSpan
    Public Opoznienie As UInteger
    Public Przez As String()
    Public Sektory As String()
    Public Uwagi As String()
End Class

Friend Enum TypPostojuPociagu
    Odjazd
    Przyjazd
End Enum