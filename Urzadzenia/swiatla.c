#include "include/swiatla.h"

// avrdude -c usbasp -p m8 -U flash:w:bin/swiatla.hex:i
// Ustawianie portu: stty -F /dev/ttyUSB0 speed 19200 cs8 -cstopb -parenb
// Zapis: echo A > /dev/ttyUSB0
// Odczyt: cat < /dev/ttyUSB0

volatile StanPinu piny[LICZBA_WYJSC];
volatile uint8_t przetworzWejscia = 0;

void UstawTypPinowDlaWejsc() {
    DaneUrzadzenia* urz;
    uint8_t urzIx = 0;

    for (uint8_t i = 0; i < LiczbaUrzadzen; i++) {
        urz = (void*)&DaneUrzadzen[urzIx];
        urzIx += DLUGOSC_NAGLOWKA_URZ;

        if (urz->Typ == TypUrz_Wejscie) {
            piny[DaneUrzadzen[urzIx]].Typ = Wejscie;
        }

        urzIx += urz->Dlugosc;
    }
}

void UstawSwiatloSygnalizatora(uint16_t liczba, uint8_t poz, uint8_t ix) {
    uint16_t typ = (liczba >> (poz << 1)) & 3;
    if (typ == SYGN_WYLACZONY) piny[ix].Typ = Wylacz;
    if (typ == SYGN_MIGANIE)   piny[ix].Typ = MigSwiec;
    if (typ == SYGN_WLACZONY)  piny[ix].Typ = Wlacz;
}

uint8_t PobierzTypWylaczanegoSygnDrog(uint8_t typ) {
    if (typ == MigDrogWylaczony || typ == MigDrogOczekiwanieNaWlaczenie) {
        return Wylaczony;
    } else {
        return MigDrogWylaczOstatecznie;
    }
}

void PrzetworzKomunikatyWejsciowe() {
    Komunikat* kom;

    while ((kom = PobierzKomunikatWejsciowy()) != NULL) {
        DaneUrzadzenia* urz;
        uint8_t urzIx = 0;

        for (uint8_t i = 0; i < LiczbaUrzadzen; i++) {
            urz = (void*)&DaneUrzadzen[urzIx];
            urzIx += DLUGOSC_NAGLOWKA_URZ;

            if (kom->AdresUrzadzenia == urz->Adres) {
                volatile StanPinu *wy, *wy2;
                uint16_t daneSygn;
                uint8_t koniec;
                uint8_t typOdp = 0;

                switch (kom->Typ) {
                case TYP_KOM_USTAW_STAN_SYGNALIZATORA:
                    daneSygn = kom->Dane;
                    koniec = urzIx + urz->Dlugosc;

                    for (uint8_t j = urzIx, k = 0; j < koniec; j++, k++) {
                        UstawSwiatloSygnalizatora(daneSygn, k, DaneUrzadzen[j]);
                    }

                    typOdp = TYP_KOM_USTAWIONO_STAN_SYGNALIZATORA;
                    break;

                case TYP_KOM_USTAW_STAN_SYGNALIZATORA_DROGOWEGO:
                    wy  = &piny[DaneUrzadzen[urzIx]];
                    wy2 = &piny[DaneUrzadzen[urzIx + 1]];

                    if (kom->Dane == 1) {   //Wlacz
                        wy->Typ = MigDrogOczekiwanieNaWlaczenie;
                        wy->Wartosc = PWM_MAX;
                        wy2->Typ = MigDrogWlacz;
                        wy2->Wartosc = 0;
                    } else {                //Wylacz
                        wy->Typ  = PobierzTypWylaczanegoSygnDrog(wy->Typ);
                        wy2->Typ = PobierzTypWylaczanegoSygnDrog(wy2->Typ);
                    }

                    typOdp = TYP_KOM_USTAWIONO_STAN_SYGNALIZATORA_DROGOWEGO;
                    break;

                case TYP_KOM_USTAW_JASNOSC_LAMPY:
                    wy = &piny[DaneUrzadzen[urzIx]];
                    wy->Typ = Jasnosc;
                    wy->Wartosc = ((kom->Dane) & 0xFF) >> 3;
                    typOdp = TYP_KOM_USTAWIONO_JASNOSC_LAMPY;
                    break;
                }

                if (typOdp != 0) {
                    Komunikat* odp = RozpocznijKomunikatWyjsciowy();
                    odp->Typ = typOdp;
                    odp->AdresPosterunku = kom->AdresPosterunku;
                    odp->AdresUrzadzenia = kom->AdresUrzadzenia;
                    odp->Dane = 0;
                    ZakonczKomunikatWyjsciowy();
                }

                break;
            }

            urzIx += urz->Dlugosc;
        }
    }
}

void PrzetworzWejscia() {
    DaneUrzadzenia* urz;
    uint8_t urzIx = 0;

    for (uint8_t i = 0; i < LiczbaUrzadzen; i++) {
        urz = (void*)&DaneUrzadzen[urzIx];
        urzIx += DLUGOSC_NAGLOWKA_URZ;

        if (urz->Typ == TypUrz_Wejscie) {
            uint8_t nr = DaneUrzadzen[urzIx];
            volatile StanPinu* p = &piny[nr];
            uint8_t port;
            uint8_t pin;
            uint8_t wartosc;
            uint8_t* wskaznikPortu;

            PobierzPortPin(nr, &port, &pin);
            wskaznikPortu = (uint8_t*)(0x30 + port * 3);    // 0x30 = PIND
            
            // odczyt wartosci z wejscia portu
            wartosc = (*wskaznikPortu) & (1 << pin);

            if (p->Wartosc == 0 && wartosc != 0) {
                Komunikat* odp = RozpocznijKomunikatWyjsciowy();
                odp->Typ = TYP_KOM_WYKRYTO_OS;
                odp->AdresPosterunku = 0;
                odp->AdresUrzadzenia = urz->Adres;
                odp->Dane = 0;
                ZakonczKomunikatWyjsciowy();
            }

            p->Wartosc = (wartosc == 0 ? 0 : 1);
        }

        urzIx += urz->Dlugosc;
    }
}

void ZwiekszWartosc(uint8_t *wartosc, uint8_t mnoznik) {
    if (*wartosc > ZMIEN_SZYBCIEJ) {
        *wartosc += ZMIANA_SZYBCIEJ * mnoznik;
        if (*wartosc > PWM_MAX) *wartosc = PWM_MAX;
    } else {
        *wartosc += mnoznik;
    }
}

void ZmniejszWartosc(uint8_t *wartosc, uint8_t mnoznik) {
    if (*wartosc > ZMIEN_SZYBCIEJ) {
        *wartosc -= ZMIANA_SZYBCIEJ * mnoznik;
    } else {
        if (mnoznik > *wartosc) {
            *wartosc = 0;
        } else {
            *wartosc -= mnoznik;
        }
    }
}

void PrzetworzZmianeStanu(uint8_t ix, uint8_t typ, uint8_t wartosc) {
    // Wlaczanie/wylaczanie swiatla
    if (typ == Wlacz) {
        if (wartosc >= PWM_MAX) {
            typ = Wlaczony;
        } else {
            ZwiekszWartosc(&wartosc, 2);
        }

    } else if (typ == Wylacz) {
        if (wartosc == 0) {
            typ = Wylaczony;
        } else {
            ZmniejszWartosc(&wartosc, 2);
        }
    }

    // Miganie swiatla
    else if (typ == MigCzekaj) {
        if (wartosc >= MIG_WYLACZONY_CZEKAJ) {
            typ = MigSwiec;
            wartosc = 0;
        } else {
            wartosc++;
        }

    } else if (typ == MigSwiec) {
        if (wartosc >= PWM_MAX) {
            typ = MigWylacz;
            wartosc -= ZMIANA_SZYBCIEJ;
        } else {
            ZwiekszWartosc(&wartosc, 1);
        }

    } else if (typ == MigWylacz) {
        if (wartosc == 0) {
            typ = MigCzekaj;
            wartosc++;
        } else {
            ZmniejszWartosc(&wartosc, 1);
        }
    }

    // Miganie swiatla na sygnalizatorze drogowym
    else if (typ == MigDrogWylaczony) {
        if (wartosc >= DROG_CZEKAJ) {
            typ = MigDrogWlacz;
            wartosc = 0;
        } else {
            ZwiekszWartosc(&wartosc, 1);
        }

    } else if (typ == MigDrogWlacz) {
        if (wartosc >= PWM_MAX) {
            typ = MigDrogWlaczony;
            wartosc = 0;
        } else {
            ZwiekszWartosc(&wartosc, 2);
        }

    } else if (typ == MigDrogWlaczony) {
        if (wartosc == DROG_CZEKAJ) {
            typ = MigDrogWylacz;
        } else {
            ZwiekszWartosc(&wartosc, 1);
        }

    } else if (typ == MigDrogWylacz || typ == MigDrogOczekiwanieNaWlaczenie) {
        if (wartosc == 0) {
            typ = MigDrogWylaczony;
        } else {
            ZmniejszWartosc(&wartosc, 2);
        }

    } else if (typ == MigDrogWylaczOstatecznie) {
        if (wartosc == 0) {
            typ = Wylaczony;
        } else {
            ZmniejszWartosc(&wartosc, 2);
        }
    }

    // Zapisz zmieniony stan
    piny[ix].Typ = typ;
    piny[ix].Wartosc = wartosc;
}

int main(void) {
    UstawKierunekPinow();
    InicjalizujUart();
    
    for (uint8_t i = 0; i < LICZBA_WYJSC; i++) {
        piny[i].Typ = Wylaczony;
        piny[i].Wartosc = 0;
    }

    UstawTypPinowDlaWejsc();

    // Wlacz licznik odmierzajacy czas do odczytywania stanu wejsc
    TCCR0 = (5 << CS00);
    TIMSK = (1 << TOIE0);

    sei();

    // PORTD 0x32 D, C, B +3

    uint8_t *port;
    uint8_t pin = 1;
    uint8_t stanPortu = 0;

    uint8_t pwm = 0;
    uint8_t zmiana = 0;

    uint8_t typ;
    uint8_t wartosc;

    while (1) {
        PrzetworzKomunikatyWejsciowe();
        port = (uint8_t*)0x32;  // PORTD

        // Przetworz wyjscia (swiatla)
        for (uint8_t i = 0; i < LICZBA_WYJSC; i++) {
            typ = piny[i].Typ;
            wartosc = piny[i].Wartosc;

            // Swiatlo wlaczone na stale
            if (typ == Wlaczony || typ == MigDrogWlaczony) stanPortu |= pin;

            // Stan swiatla zalezy od obecnej wartosci PWM
            if (typ == Wlacz || typ == Wylacz || typ == Jasnosc || typ == MigSwiec || typ == MigWylacz || typ == MigDrogWlacz || typ == MigDrogWylacz || typ == MigDrogWylaczOstatecznie) {
                if (pwm < wartosc) stanPortu |= pin;
            }

            if (zmiana == ZMIANA_MAX) PrzetworzZmianeStanu(i, typ, wartosc);

            // Obsluga wyjscia
            if (pin == PIN_MAX) {
                // Wszystkie piny w porcie zostaly przetworzone, ustaw stan wyjscia
                *port = stanPortu;
                port += 3;
                stanPortu = 0;
                pin = 1;
            } else {
                pin <<= 1;
            }
        }

        // Przetworz wejscia (czujniki osi)
        if (przetworzWejscia == 1) {
            przetworzWejscia = 0;
            PrzetworzWejscia();
        }

        // Obsluga postepu i ewentualnego rozpoczecia nowego cyklu
        pwm++;
        if (pwm > PWM_MAX) pwm = 0;

        zmiana++;
        if (zmiana > ZMIANA_MAX) zmiana = 0;
    }

    return 0;
}

ISR(TIMER0_OVF_vect) {
    przetworzWejscia = 1;
}