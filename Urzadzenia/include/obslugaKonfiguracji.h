#ifndef OBSLUGA_KONFIGURACJI_H
#define OBSLUGA_KONFIGURACJI_H

#include "ogolne.h"
#include <avr/io.h>

// Dlugosc tablicy z konfiguracja urzadzen
#define DLUGOSC_DANE_URZ 5 * LICZBA_WYJSC
// Dlugosc naglowka z opisem urzadzenia
#define DLUGOSC_NAGLOWKA_URZ 4
// Maska dla bitow w numerze wyjscia, odpowiadajacych za numer pinu
#define NRWYJSCIA_BITY_PIN 7
// Maska dla bitow w numerze wyjscia, odpowiadajacych za numer portu
#define NRWYJSCIA_BITY_PORT 3
// Przesuniecie bitow oznaczajacych numer portu
#define NRWYJSCIA_PORT_PRZESUN 3

typedef struct DaneUrzadzenia {
    uint16_t Adres;
    uint8_t Typ;
    uint8_t Dlugosc;
} DaneUrzadzenia;

enum TypUrzadzenia {
    TypUrz_Swiatlo = 1,
    TypUrz_Wejscie = 2
};

extern uint8_t LiczbaUrzadzen;
extern uint8_t DaneUrzadzen[DLUGOSC_DANE_URZ];

void PobierzPortPin(uint8_t wartosc, uint8_t* port, uint8_t* pin);
void UstawKierunekPinow();

#endif