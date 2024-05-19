#ifndef SERWOMECHANIZMY_H
#define SERWOMECHANIZMY_H

#include "obslugaUart.h"
#include "obslugaKonfiguracji.h"

//mikrosekundy w milisekundzie
#define CZAS_US                         1000
#define WYPELNIENIE_MIN                 (1 * CZAS_US)
#define WYPELNIENIE_MAX                 (4 * CZAS_US)
#define WYPELNIENIE_SORTUJ              (15 * CZAS_US)
#define LICZBA_SERWOMECHANIZMOW_MAX     6
#define CZAS_PRZESTAWIANIA_ZWROTNICY_MS 500
#define CZESTOTLIWOSC_WYPELNIENIA       50
#define DLUGOSC_SYGNALU_MS              20
#define CZAS_PRZETWARZANIE_WEJSC        2

typedef struct FlagiSerwomechanizmu {
    uint8_t Wlaczony:      1;
    uint8_t PozGlowna:     1;
    uint8_t PozInna:       1;
    uint8_t TrybSerwisowy: 1;
    uint8_t DoPozGlownej:  1;
} FlagiSerwomechanizmu;

typedef struct StanSerwomechanizmu {
    uint16_t Wartosc;
    uint16_t Docelowa;
    int16_t Zmiana;
    FlagiSerwomechanizmu Flagi;
    DaneUrzadzenia* Urzadzenie;
} StanSerwomechanizmu;

enum UstawienieZwrotnicy {
    Zwrotnica_Niezdefiniowany = 0,
    Zwrotnica_Plus = 1,
    Zwrotnica_Minus = 2,
    Zwrotnica_Oba = 3
};

enum UstawienieRogatki {
    Rogatka_Niezdefiniowany = 0,
    Rogatka_Otwarta = 1,
    Rogatka_Zamknieta = 2,
    Rogatka_Oba = 3
};

DaneSerwomechanizmu* PobierzKonfSerwomechanizmu(DaneUrzadzenia* urz);

#endif