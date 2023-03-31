#ifndef OBSLUGA_UART_H
#define OBSLUGA_UART_H

#include "ogolne.h"
#include <avr/io.h>
#include <avr/interrupt.h>

// Liczba bajtow komunikatu
#define DLUGOSC_KOMUNIKATU 7
// Dlugosc wejsciowej kolejki komunikatow
#define KOMUNIKATY_WEJSCIE 10 * DLUGOSC_KOMUNIKATU
// Dlugosc wyjsciowej kolejki komunikatow
#define KOMUNIKATY_WYJSCIE 10 * DLUGOSC_KOMUNIKATU

// Typy komunikatow
#define TYP_KOM_USTAW_STAN_SYGNALIZATORA 1
#define TYP_KOM_USTAWIONO_STAN_SYGNALIZATORA 2
#define TYP_KOM_USTAW_STAN_SYGNALIZATORA_DROGOWEGO 3
#define TYP_KOM_USTAWIONO_STAN_SYGNALIZATORA_DROGOWEGO 4
#define TYP_KOM_USTAW_JASNOSC_LAMPY 5
#define TYP_KOM_USTAWIONO_JASNOSC_LAMPY 6

typedef struct Komunikat {
    uint8_t Typ;
    uint16_t AdresPosterunku;
    uint16_t AdresUrzadzenia;
    uint16_t Dane;
} Komunikat;

void InicjalizujUart();
Komunikat* PobierzKomunikatWejsciowy();
void WyslijBajt();
Komunikat* RozpocznijKomunikatWyjsciowy();
void ZakonczKomunikatWyjsciowy();

#endif