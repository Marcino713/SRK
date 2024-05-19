#include "include/obslugaKonfiguracji.h"

uint8_t LiczbaUrzadzen = 2;
uint8_t DaneUrzadzen[DLUGOSC_DANE_URZ] = {/*
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
    16*/

    // Zwrotnica
    71, 0, TypUrz_Zwrotnica, 8,
    0xE8, 3, 0xD0, 3, 5, 8, 7, 16,   // 1000/976
    //Rogatka
    21, 0, TypUrz_Zwrotnica, 8,
    0xD0, 7, 0xE8, 7, 6, 9, 2, 4     // 2000/2024
};

void PobierzPortPin(uint8_t wartosc, uint8_t* port, uint8_t* pin) {
    *port = (wartosc >> NRWYJSCIA_PORT_PRZESUN) & NRWYJSCIA_BITY_PORT;
    *pin  = wartosc & NRWYJSCIA_BITY_PIN;
}

void UstawWyjscie(uint8_t wartosc, uint8_t* kierunki) {
    uint8_t port;
    uint8_t pin;

    PobierzPortPin(wartosc, &port, &pin);
    kierunki[port] |= (1 << pin);
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

            for (uint8_t j = urzIx; j < koniec; j++) {
                UstawWyjscie(DaneUrzadzen[j], kierunek);
            }

        } else if(urz->Typ == TypUrz_Zwrotnica || urz->Typ == TypUrz_Rogatka) {
            DaneSerwomechanizmu* serw = (void*)&DaneUrzadzen[urzIx];
            UstawWyjscie(serw->PinSygnal, kierunek);
            UstawWyjscie(serw->PinZasilanie, kierunek);

        }

        urzIx += urz->Dlugosc;
    }

    uint8_t* port = (uint8_t*)0x31; // DDRD
    for (uint8_t i = 0; i < LICZBA_PORTOW; i++, port += 3) {
        *port = kierunek[i];
    }
}