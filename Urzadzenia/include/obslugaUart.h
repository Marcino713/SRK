#ifndef OBSLUGA_UART_H
#define OBSLUGA_UART_H

#include "ogolne.h"
#include <avr/io.h>
#include <avr/interrupt.h>

// Liczba bajtow komunikatu
#define DLUGOSC_KOMUNIKATU 7
// Dlugosc kolejki komunikatow
#define LICZBA_KOMUNIKATOW 10
// Dlugosc wejsciowej kolejki komunikatow w bajtach
#define KOMUNIKATY_WEJSCIE (LICZBA_KOMUNIKATOW * DLUGOSC_KOMUNIKATU)
// Dlugosc wyjsciowej kolejki komunikatow w bajtach
#define KOMUNIKATY_WYJSCIE (LICZBA_KOMUNIKATOW * DLUGOSC_KOMUNIKATU)

// Typy komunikatow
#define TYP_KOM_USTAW_STAN_SYGNALIZATORA               1
#define TYP_KOM_USTAWIONO_STAN_SYGNALIZATORA           2
#define TYP_KOM_USTAW_STAN_SYGNALIZATORA_DROGOWEGO     3
#define TYP_KOM_USTAWIONO_STAN_SYGNALIZATORA_DROGOWEGO 4
#define TYP_KOM_USTAW_JASNOSC_LAMPY                    5
#define TYP_KOM_USTAWIONO_JASNOSC_LAMPY                6
#define TYP_KOM_WYKRYTO_OS                             7
#define TYP_KOM_USTAW_ZWROTNICE_SERWISOWO              8
#define TYP_KOM_USTAW_ZWROTNICE                        9
#define TYP_KOM_ZMIENIONO_STAN_ZWROTNICY               10
#define TYP_KOM_ZAMKNIJ_ROGATKE                        11
#define TYP_KOM_OTWORZ_ROGATKE                         12
#define TYP_KOM_ZMIENIONO_STAN_ROGATKI                 13

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