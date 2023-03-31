#include "include/obslugaKonfiguracji.h"

uint8_t LiczbaUrzadzen = 4;
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
    2, 4};