#ifndef SWIATLA_H
#define SWIATLA_H

#include "obslugaUart.h"
#include "obslugaKonfiguracji.h"

// Liczba poziomow wypelnienia PWM i liczba okrazen glownej petli na jeden pelny cykl wypelnienia PWM
#define PWM_MAX 32
// Liczba okrazen glownej petli, po ktorej mozna przetworzyc zmiany stanu urzadzen
#define ZMIANA_MAX 150
// Wartosc, po przekroczeniu ktorej szybciej zmienia sie poziom wypelnienia
#define ZMIEN_SZYBCIEJ 15
// Jak szybko zmienia sie wartosc po przekroczeniu progu
#define ZMIANA_SZYBCIEJ 2
// Jak dlugo swiatlo migajacego jest wylaczone
#define MIG_WYLACZONY_CZEKAJ 15
// Czas dla swiatla drogowego wlaczonego albo wylaczonego
#define DROG_CZEKAJ 20

// Ustawiany stan swiatla kolejowego
#define SYGN_WYLACZONY 0
#define SYGN_MIGANIE   1
#define SYGN_WLACZONY  2

typedef struct StanPinu {
    uint8_t Typ;
    uint8_t Wartosc;
} StanPinu;

enum TypPinu {
    // Swiecenie swiatla na sygnalizatorze
    Wylaczony = 0,
    Wlacz     = 1,
    Wlaczony  = 2,
    Wylacz    = 3,

    // Stala jasnosc swiatla
    Jasnosc   = 4,

    // Miganie swiatla
    MigCzekaj = 5,
    MigSwiec  = 6,
    MigWylacz = 7,

    // Miganie swiatla na sygnalizatorze drogowym
    MigDrogWylaczony = 8,
    MigDrogWlacz     = 9,
    MigDrogWlaczony  = 10,
    MigDrogWylacz    = 11,
    MigDrogOczekiwanieNaWlaczenie = 12,
    MigDrogWylaczOstatecznie      = 13,

    // Wejscie
    Wejscie = 14
};

void UstawTypPinowDlaWejsc();
void UstawSwiatloSygnalizatora(uint16_t liczba, uint8_t poz, uint8_t ix);
uint8_t PobierzTypWylaczanegoSygnDrog(uint8_t typ);
void PrzetworzKomunikatyWejsciowe();
void PrzetworzWejscia();
void ZwiekszWartosc(uint8_t *wartosc, uint8_t mnoznik);
void ZmniejszWartosc(uint8_t *wartosc, uint8_t mnoznik);
void PrzetworzZmianeStanu(uint8_t ix, uint8_t typ, uint8_t wartosc);
int main(void);

#endif