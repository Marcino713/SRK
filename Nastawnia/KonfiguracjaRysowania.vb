Public Class KonfiguracjaRysowania
    Public Skalowanie As Single = 50
    Public RysujKrawedzieKostek As Boolean = True
    Public DodatkoweObiekty As RysujDodatkoweObiekty
    Public ZaznaczonaKostka As Zaleznosci.Kostka
    Public ZaznaczonaLampa As Zaleznosci.Lampa
    Public ZaznaczonyOdcinek As Zaleznosci.OdcinekToru
    Public ZaznaczonyLicznik As Zaleznosci.ParaLicznikowOsi
End Class

Public Enum RysujDodatkoweObiekty
    Nic
    Lampy
    Tory
    Liczniki
End Enum