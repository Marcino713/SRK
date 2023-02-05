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
    Friend Const ZWOLNIJ_PRZEBIEGI As UShort = 16
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

    Friend Shared Function CzyKomunikatDH(typ As UShort) As Boolean
        Return typ = DH_INICJALIZUJ Or typ = DH_ZAINICJALIZOWANO
    End Function

    Friend Shared Function CzyKomunikatUwierzytelniania(typ As UShort) As Boolean
        Return typ = UWIERZYTELNIJ_SIE Or typ = UWIERZYTELNIONO_POPRAWNIE Or typ = NIEUWIERZYTELNIONO
    End Function

    Friend Shared Function CzyKomunikatWyboruPosterunku(typ As UShort) As Boolean
        Return typ = WYBIERZ_POSTERUNEK Or typ = WYBRANO_POSTERUNEK
    End Function

    Friend Shared Function CzyKomunikatSterowaniaRuchem(typ As UShort) As Boolean
        Return typ = DODAJ_POCIAG Or typ = DODANO_POCIAG Or typ = ZMIENIONO_STAN_TORU Or typ = USTAW_ZWROTNICE Or typ = ZMIENIONO_STAN_ZWROTNICY Or
               typ = USTAW_STAN_SYGNALIZATORA Or typ = ZMIENIONO_STAN_SYGNALIZATORA Or typ = ZWOLNIJ_PRZEBIEGI Or typ = USTAW_KIERUNEK Or typ = POTWIERDZ_KIERUNEK Or
               typ = ZMIENIONO_KIERUNEK Or typ = USTAW_JASNOSC_LAMP Or typ = ZMIENIONO_JASNOSC_LAMP Or typ = USTAW_PREDKOSC_POCIAGU Or typ = ZMIENIONO_PREDKOSC_POCIAGU Or
               typ = ZMIENIONO_PREDKOSC_DOZWOLONA Or typ = INFORMACJA Or typ = POBIERZ_POCIAGI Or typ = POBRANO_POCIAGI Or typ = WYBIERZ_POCIAG Or typ = WYBRANO_POCIAG Or
               typ = WYSIADZ_Z_POCIAGU Or typ = WYSIADZNIETO_Z_POCIAGU Or typ = USUN_POCIAG Or typ = USUNIETO_POCIAG Or typ = ZMIENIONO_NAZWE_POCIAGU
    End Function

    Friend Shared Function CzyKomunikatZakonczenia(typ As UShort) As Boolean
        Return typ = ZAKONCZ_DZIALANIE_KLIENTA Or typ = ZAKONCZONO_DZIALANIE_SERWERA Or typ = ZAKONCZONO_SESJE_KLIENTA
    End Function
End Class