Friend Class TypKomunikatu
    Friend Const DH_INICJALIZUJ As UShort = 1
    Friend Const DH_ZAINICJALIZOWANO As UShort = 2
    Friend Const UWIERZYTELNIJ_SIE As UShort = 4
    Friend Const UWIERZYTELNIONO_POPRAWNIE As UShort = 5
    Friend Const NIEUWIERZYTELNIONO As UShort = 6
    Friend Const WYBIERZ_POSTERUNEK As UShort = 7
    Friend Const WYBRANO_POSTERUNEK As UShort = 8
    Friend Const USTAW_POCZATKOWA_ZAJETOSC_TORU As UShort = 9
    Friend Const NADANO_NUMER_POCIAGU As UShort = 10
    Friend Const ZMIENIONO_STAN_TORU As UShort = 11
    Friend Const USTAW_ZWROTNICE As UShort = 12
    Friend Const ZMIENIONO_STAN_ZWROTNICY As UShort = 13
    Friend Const USTAW_STAN_SYGNALIZATORA As UShort = 14
    Friend Const ZMIENIONO_STAN_SYGNALIZATORA As UShort = 15
    Friend Const ZWOLNIJ_PRZEBIEGI As UShort = 16
    Friend Const USTAW_KIERUNEK As UShort = 17
    Friend Const ZAZADANO_USTAWIENIA_KIERUNKU As UShort = 18
    Friend Const ZMIENIONO_KIERUNEK As UShort = 19
    Friend Const USTAW_JASNOSC_LAMP As UShort = 20
    Friend Const ZMIENIONO_JASNOSC_LAMP As UShort = 21
    Friend Const USTAW_PREDKOSC_POCIAGU As UShort = 22
    Friend Const ZMIENIONO_PREDKOSC_POCIAGU As UShort = 23
    Friend Const ZMIENIONO_PREDKOSC_MAKSYMALNA As UShort = 24
    Friend Const INFORMACJA As UShort = 25
    Friend Const ZAKONCZ_DZIALANIE_KLIENTA As UShort = 26
    Friend Const ZAKONCZONO_DZIALANIE_SERWERA As UShort = 27
    Friend Const ZAKONCZONO_SESJE_KLIENTA As UShort = 28

    Friend Shared Function CzyKomunikatDH(typ As UShort) As Boolean
        Return typ = DH_INICJALIZUJ Or typ = DH_ZAINICJALIZOWANO
    End Function
End Class