#include "include/obslugaUart.h"

uint8_t volatile KomunikatyWejscie[KOMUNIKATY_WEJSCIE];
uint8_t volatile KomWePoz     = 0;
uint8_t volatile KomWeCzytaj  = 0;
uint8_t volatile KomWeOdbierz = 0;

uint8_t volatile KomunikatyWyjscie[KOMUNIKATY_WYJSCIE];
uint8_t volatile KomWyPoz    = 0;
uint8_t volatile KomWyZapisz = 0;
uint8_t volatile KomWyWyslij = 0;

void InicjalizujUart() {
    UBRRL = 25;
    UCSRC = (1 << URSEL) | (3 << UCSZ0);
    UCSRB = (1 << RXCIE) | (1 << RXEN) | (1 << TXEN);
}

Komunikat* PobierzKomunikatWejsciowy() {
    Komunikat* kom = NULL;
    if (KomWeCzytaj != KomWeOdbierz) {
        kom = (void*)&KomunikatyWejscie[KomWeCzytaj];
        KomWeCzytaj += DLUGOSC_KOMUNIKATU;
        if (KomWeCzytaj >= KOMUNIKATY_WEJSCIE) KomWeCzytaj = 0;
    }

    return kom;
}

void WyslijBajt() {
    uint8_t poz = KomWyWyslij + KomWyPoz;
    if (poz != KomWyZapisz) {
        UDR = KomunikatyWyjscie[poz];

        KomWyPoz++;
        if (KomWyPoz == DLUGOSC_KOMUNIKATU) {
            KomWyPoz = 0;
            KomWyWyslij += DLUGOSC_KOMUNIKATU;
            if (KomWyWyslij >= KOMUNIKATY_WYJSCIE) KomWyWyslij = 0;
        }
    } else {
        UCSRB &= ~((uint8_t)(1 << UDRIE));
    }
}

Komunikat* RozpocznijKomunikatWyjsciowy() {
    return (void*)&KomunikatyWyjscie[KomWyZapisz];
}

void ZakonczKomunikatWyjsciowy() {
    cli();
    KomWyZapisz += DLUGOSC_KOMUNIKATU;
    if (KomWyZapisz >= KOMUNIKATY_WYJSCIE) KomWyZapisz = 0;
    if (UCSRA & (1 << UDRE)) WyslijBajt();
    UCSRB |= (1 << UDRIE);
    sei();
}

ISR(USART_RXC_vect) {
    KomunikatyWejscie[KomWePoz + KomWeOdbierz] = UDR;
    KomWePoz++;
    if (KomWePoz == DLUGOSC_KOMUNIKATU) {
        KomWePoz = 0;
        KomWeOdbierz += DLUGOSC_KOMUNIKATU;
        if (KomWeOdbierz >= KOMUNIKATY_WEJSCIE) KomWeOdbierz = 0;
    }
}

ISR(USART_UDRE_vect) {
    WyslijBajt();
}