#ifndef OBSLUGA_KONFIGURACJI_H
#define OBSLUGA_KONFIGURACJI_H

#include "ogolne.h"
#include <avr/io.h>

// Dlugosc tablicy z konfiguracja urzadzen
#define DLUGOSC_DANE_URZ 5 * LICZBA_WYJSC
// Dlugosc naglowka z opisem urzadzenia
#define DLUGOSC_NAGLOWKA_URZ 4

typedef struct DaneUrzadzenia {
    uint16_t Adres;
    uint8_t Typ;
    uint8_t Dlugosc;
} DaneUrzadzenia;

enum TypUrzadzenia {
    TypUrz_Swiatlo = 1
};

extern uint8_t LiczbaUrzadzen;
extern uint8_t DaneUrzadzen[DLUGOSC_DANE_URZ];

#endif