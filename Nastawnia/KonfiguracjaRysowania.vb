Public Class KonfiguracjaRysowania
    Public KolorKostki As Color = KolorRGB("#99FFCC")
    Public Skalowanie As Single = 50
    Public RysujKrawedzieKostek As Boolean = True
    Public ZaznaczX As Integer = -1
    Public ZaznaczY As Integer = -1
    Public PrzesuwanaKostka As Zaleznosci.Kostka
    Public RysujLampy As Boolean = False
    Public ZaznaczonaLampa As Zaleznosci.Lampa

    Public Sub WyczyscZaznaczenie()
        ZaznaczX = -1
        ZaznaczY = -1
        PrzesuwanaKostka = Nothing
    End Sub
End Class