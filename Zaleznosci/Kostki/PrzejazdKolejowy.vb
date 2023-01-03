Public Class PrzejazdKolejowy
    Inherits Tor

    Public Property Stan As StanPrzejazduKolejowego
    Public Property Awaria As Boolean = False

    Public Sub New()
        MyBase.New(TypKostki.PrzejazdKolejowy)
    End Sub
End Class

Public Enum StanPrzejazduKolejowego
    Otwarty = 1
    Zamykany = 2
    Zamkniety = 3
    Otwierany = 4
End Enum