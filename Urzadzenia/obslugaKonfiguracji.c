#include "include/obslugaKonfiguracji.h"

uint8_t LiczbaUrzadzen = 5;
uint8_t DaneUrzadzen[DLUGOSC_DANE_URZ] = {
    // Sygnalizator polsamoczynny
    30, 0, TypUrz_Swiatlo, 7,
    8, 9, 10, 11, 12, 13, 17,
    // Lampa 1
    10, 0, TypUrz_Swiatlo, 1,
    5,
    // Lampa 2
    20, 0, TypUrz_Swiatlo, 1,
    6,
    // Sygnalizator drogowy
    40, 0, TypUrz_Swiatlo, 2,
    2, 4,
    // Wejscie licznika osi
    70, 0, TypUrz_Wejscie, 1,
    16};

void PobierzPortPin(uint8_t wartosc, uint8_t* port, uint8_t* pin) {
    *port = (wartosc >> NRWYJSCIA_PORT_PRZESUN) & NRWYJSCIA_BITY_PORT;
    *pin  = wartosc & NRWYJSCIA_BITY_PIN;
}

void UstawKierunekPinow() {
    uint8_t kierunek[LICZBA_PORTOW] = {0, 0, 0};
    DaneUrzadzenia* urz;
    uint8_t urzIx = 0;

    for (uint8_t i = 0; i < LiczbaUrzadzen; i++) {
        urz = (void*)&DaneUrzadzen[urzIx];
        urzIx += DLUGOSC_NAGLOWKA_URZ;

        if (urz->Typ == TypUrz_Swiatlo) {
            uint8_t koniec = urzIx + urz->Dlugosc;
            uint8_t port;
            uint8_t pin;
            
            for (uint8_t j = urzIx; j < koniec; j++) {
                PobierzPortPin(DaneUrzadzen[j], &port, &pin);
                kierunek[port] |= (1 << pin);
            }
        }

        urzIx += urz->Dlugosc;
    }

    uint8_t* port = (uint8_t*)0x31; // DDRD
    for (uint8_t i = 0; i < LICZBA_PORTOW; i++, port += 3) {
        *port = kierunek[i];
    }
}