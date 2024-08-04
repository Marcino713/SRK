Public MustInherit Class Komunikat
    Public MustOverride ReadOnly Property Typ As UShort
    Public MustOverride Sub Zapisz(bw As BinaryWriter)
End Class

Friend Class TypKomunikatu
    Friend Const DH_INICJALIZUJ As UShort = 1
    Friend Const DH_ZAINICJALIZOWANO As UShort = 2
    Friend Const UWIERZYTELNIJ_SIE As UShort = 4
    Friend Const UWIERZYTELNIONO_POPRAWNIE As UShort = 5
    Friend Const NIEUWIERZYTELNIONO As UShort = 6
    Friend Const WYBIERZ_POSTERUNEK As UShort = 7
    Friend Const WYBRANO_POSTERUNEK As UShort = 8
    Friend Const DODAJ_POCIAG As UShort = 9
    Friend Const DODANO_POCIAG As UShort = 10
    Friend Const ZMIENIONO_STAN_TORU As UShort = 11
    Friend Const USTAW_ZWROTNICE As UShort = 12
    Friend Const ZMIENIONO_STAN_ZWROTNICY As UShort = 13
    Friend Const USTAW_STAN_SYGNALIZATORA As UShort = 14
    Friend Const ZMIENIONO_STAN_SYGNALIZATORA As UShort = 15
    Friend Const ZWOLNIJ_PRZEBIEG As UShort = 16
    Friend Const USTAW_KIERUNEK As UShort = 17
    Friend Const POTWIERDZ_KIERUNEK As UShort = 18
    Friend Const ZMIENIONO_KIERUNEK As UShort = 19
    Friend Const USTAW_JASNOSC_LAMP As UShort = 20
    Friend Const ZMIENIONO_JASNOSC_LAMP As UShort = 21
    Friend Const USTAW_PREDKOSC_POCIAGU As UShort = 22
    Friend Const ZMIENIONO_PREDKOSC_POCIAGU As UShort = 23
    Friend Const ZMIENIONO_PREDKOSC_DOZWOLONA As UShort = 24
    Friend Const INFORMACJA As UShort = 25
    Friend Const ZAKONCZ_DZIALANIE_KLIENTA As UShort = 26
    Friend Const ZAKONCZONO_DZIALANIE_SERWERA As UShort = 27
    Friend Const ZAKONCZONO_SESJE_KLIENTA As UShort = 28
    Friend Const POBIERZ_POCIAGI As UShort = 30
    Friend Const POBRANO_POCIAGI As UShort = 31
    Friend Const WYBIERZ_POCIAG As UShort = 32
    Friend Const WYBRANO_POCIAG As UShort = 33
    Friend Const WYSIADZ_Z_POCIAGU As UShort = 34
    Friend Const WYSIADZNIETO_Z_POCIAGU As UShort = 35
    Friend Const USUN_POCIAG As UShort = 36
    Friend Const USUNIETO_POCIAG As UShort = 37
    Friend Const ZMIENIONO_NAZWE_POCIAGU As UShort = 38
    Friend Const USTAW_STAN_PRZEJAZDU As UShort = 39
    Friend Const ZMIENIONO_STAN_PRZEJAZDU As UShort = 40

    Private Shared ReadOnly KOMUNIKATY_DH As New HashSet(Of UShort) From {DH_INICJALIZUJ, DH_ZAINICJALIZOWANO}
    Private Shared ReadOnly KOMUNIKATY_UWIERZYTELNIANIA As New HashSet(Of UShort) From {UWIERZYTELNIJ_SIE, UWIERZYTELNIONO_POPRAWNIE, NIEUWIERZYTELNIONO}
    Private Shared ReadOnly KOMUNIKATY_WYBORU_POSTERUNKU As New HashSet(Of UShort) From {WYBIERZ_POSTERUNEK, WYBRANO_POSTERUNEK}
    Private Shared ReadOnly KOMUNIKATY_ZAKONCZENIA As New HashSet(Of UShort) From {ZAKONCZ_DZIALANIE_KLIENTA, ZAKONCZONO_DZIALANIE_SERWERA, ZAKONCZONO_SESJE_KLIENTA}
    Private Shared ReadOnly KOMUNIKATY_STEROWANIA_RUCHEM As New HashSet(Of UShort) From {
        DODAJ_POCIAG, DODANO_POCIAG, ZMIENIONO_STAN_TORU, USTAW_ZWROTNICE, ZMIENIONO_STAN_ZWROTNICY, USTAW_STAN_SYGNALIZATORA, ZMIENIONO_STAN_SYGNALIZATORA, ZWOLNIJ_PRZEBIEG,
        USTAW_KIERUNEK, POTWIERDZ_KIERUNEK, ZMIENIONO_KIERUNEK, USTAW_JASNOSC_LAMP, ZMIENIONO_JASNOSC_LAMP, USTAW_PREDKOSC_POCIAGU, ZMIENIONO_PREDKOSC_POCIAGU, ZMIENIONO_PREDKOSC_DOZWOLONA,
        INFORMACJA, POBIERZ_POCIAGI, POBRANO_POCIAGI, WYBIERZ_POCIAG, WYBRANO_POCIAG, WYSIADZ_Z_POCIAGU, WYSIADZNIETO_Z_POCIAGU, USUN_POCIAG, USUNIETO_POCIAG, ZMIENIONO_NAZWE_POCIAGU,
        USTAW_STAN_PRZEJAZDU, ZMIENIONO_STAN_PRZEJAZDU
    }

    Friend Shared Function CzyKomunikatDH(typ As UShort) As Boolean
        Return KOMUNIKATY_DH.Contains(typ)
    End Function

    Friend Shared Function CzyKomunikatUwierzytelniania(typ As UShort) As Boolean
        Return KOMUNIKATY_UWIERZYTELNIANIA.Contains(typ)
    End Function

    Friend Shared Function CzyKomunikatWyboruPosterunku(typ As UShort) As Boolean
        Return KOMUNIKATY_WYBORU_POSTERUNKU.Contains(typ)
    End Function

    Friend Shared Function CzyKomunikatSterowaniaRuchem(typ As UShort) As Boolean
        Return KOMUNIKATY_STEROWANIA_RUCHEM.Contains(typ)
    End Function

    Friend Shared Function CzyKomunikatZakonczenia(typ As UShort) As Boolean
        Return KOMUNIKATY_ZAKONCZENIA.Contains(typ)
    End Function
End Class