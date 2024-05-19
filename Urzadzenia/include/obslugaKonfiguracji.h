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

typedef struct DaneSerwomechanizmu {
    uint16_t PozycjaGlowna;
    uint16_t PozycjaBoczna;
    uint8_t PinSygnal;
    uint8_t PinZasilanie;
    // wykrywanie polozenia zwrotnicy wprost/rogatki otwartej
    uint8_t PinPozGlowna;
    // wykrywanie polozenia zwrotnicy bok/rogatki zakmnietej
    uint8_t PinPozInna;
} DaneSerwomechanizmu;

enum TypUrzadzenia {
    TypUrz_Swiatlo   = 1,
    TypUrz_Wejscie   = 2,
    TypUrz_Zwrotnica = 3,
    TypUrz_Rogatka   = 4
};

extern uint8_t LiczbaUrzadzen;
extern uint8_t DaneUrzadzen[DLUGOSC_DANE_URZ];

void PobierzPortPin(uint8_t wartosc, uint8_t* port, uint8_t* pin);
void UstawWyjscie(uint8_t wartosc, uint8_t* kierunki);
void UstawKierunekPinow();

#endif