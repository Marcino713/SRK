#include "include/serwomechanizmy.h"

volatile StanSerwomechanizmu serwomechanizmy[LICZBA_SERWOMECHANIZMOW_MAX];
volatile StanSerwomechanizmu* serwomechanizmyPosortowane[LICZBA_SERWOMECHANIZMOW_MAX];
volatile uint8_t serwSortIx;
volatile uint8_t posortowano;
volatile uint8_t wlaczPWM = 1;
volatile uint8_t czasPrzetworzWejscia;
uint8_t liczbaSerwomechanizmow;

void InicjalizujSerwomechanizmy(){
    DaneUrzadzenia* urz;
    uint8_t urzIx = 0;
    uint8_t s = 0;

    for (uint8_t i = 0; i < LiczbaUrzadzen; i++) {
        urz = (void*)&DaneUrzadzen[urzIx];
        urzIx += DLUGOSC_NAGLOWKA_URZ;

        if (urz->Typ == TypUrz_Zwrotnica || urz->Typ == TypUrz_Rogatka) {
            volatile DaneSerwomechanizmu* dane = PobierzKonfSerwomechanizmu(urz);
            serwomechanizmy[s].Flagi.Wlaczony = 1;
            serwomechanizmy[s].Urzadzenie = urz;
            serwomechanizmy[s].Wartosc = dane->PozycjaGlowna;
            serwomechanizmy[s].Docelowa = dane->PozycjaGlowna;
            serwomechanizmyPosortowane[s] = &serwomechanizmy[s];
            s++;
        }

        urzIx += urz->Dlugosc;
    }

    liczbaSerwomechanizmow = s;
}

void UstawParametryRuchu(volatile StanSerwomechanizmu* stan, uint16_t czasMs, uint16_t wartDocelowa, uint8_t czyPozGlowna) {
    int16_t roznica = (int16_t)wartDocelowa - (int16_t)stan->Wartosc;
    int16_t przebiegi = czasMs / DLUGOSC_SYGNALU_MS;
    stan->Zmiana = roznica / przebiegi;
    stan->Docelowa = wartDocelowa;
    stan->Flagi.TrybSerwisowy = 0;
    stan->Flagi.Wlaczony = stan->Zmiana != 0;
    stan->Flagi.DoPozGlownej = czyPozGlowna;
}

DaneSerwomechanizmu* PobierzKonfSerwomechanizmu(DaneUrzadzenia* urz) {
    return (DaneSerwomechanizmu*)((uint16_t)urz + DLUGOSC_NAGLOWKA_URZ);
}

void PrzetworzKomunikatyWejsciowe(){
    Komunikat* kom;
    volatile StanSerwomechanizmu* stan;

    while ((kom = PobierzKomunikatWejsciowy()) != NULL) {
        for (uint8_t i = 0; i < liczbaSerwomechanizmow; i++) {
            stan = &serwomechanizmy[i];

            if (kom->AdresUrzadzenia == stan->Urzadzenie->Adres) {
                DaneSerwomechanizmu* dane = PobierzKonfSerwomechanizmu(stan->Urzadzenie);

                switch (kom->Typ) {
                    case TYP_KOM_USTAW_ZWROTNICE_SERWISOWO:
                        stan->Docelowa = kom->Dane;
                        stan->Wartosc = kom->Dane;
                        stan->Zmiana = 0;
                        stan->Flagi.TrybSerwisowy = 1;
                        stan->Flagi.Wlaczony = 1;
                        break;

                    case TYP_KOM_USTAW_ZWROTNICE:
                        if (kom->Dane == Zwrotnica_Plus) {
                            UstawParametryRuchu(stan, CZAS_PRZESTAWIANIA_ZWROTNICY_MS, dane->PozycjaGlowna, 1);
                        } else if (kom->Dane == Zwrotnica_Minus) {
                            UstawParametryRuchu(stan, CZAS_PRZESTAWIANIA_ZWROTNICY_MS, dane->PozycjaBoczna, 0);
                        }
                        break;

                    case TYP_KOM_ZAMKNIJ_ROGATKE:
                        UstawParametryRuchu(stan, kom->Dane, dane->PozycjaBoczna, 0);
                        break;

                    case TYP_KOM_OTWORZ_ROGATKE:
                        UstawParametryRuchu(stan, kom->Dane, dane->PozycjaGlowna, 1);
                        break;
                }

                break;
            }
        }
    }

    posortowano = 0;
}

void UstawWartosc (volatile StanSerwomechanizmu* stan) {
    if (stan->Wartosc != stan->Docelowa) {
        stan->Wartosc += stan->Zmiana;

        if (
            (stan->Zmiana >= 0 && stan->Wartosc >= stan->Docelowa) ||
            (stan->Zmiana <  0 && stan->Wartosc <= stan->Docelowa)) {
                stan->Wartosc = stan->Docelowa;
                if (stan->Flagi.TrybSerwisowy == 0) stan->Flagi.Wlaczony = 0;
        }
    }
}

void ZmienWartosci() {
    for (uint8_t i = 0; i < liczbaSerwomechanizmow; i++) {
        UstawWartosc(&serwomechanizmy[i]);
    }
}

void WylaczSygnalPWM(uint16_t licznik) {
    if (serwSortIx < liczbaSerwomechanizmow) {
        volatile StanSerwomechanizmu* stan = serwomechanizmyPosortowane[serwSortIx];

        if (stan->Wartosc < licznik) {
            uint8_t port;
            uint8_t pin;

            PobierzPortPin(PobierzKonfSerwomechanizmu(stan->Urzadzenie)->PinSygnal, &port, &pin);
            *((uint8_t*)(PORTD_NUM + port * 3)) &= ~(uint8_t)(1 << pin);
            serwSortIx++;
        }
    }
}

void WlaczSygnalPWM() {
    volatile StanSerwomechanizmu* stan;
    volatile DaneSerwomechanizmu* dane;
    uint8_t portZasilanie;
    uint8_t pinZasilanie;
    uint8_t* wskaznikZasilanie;
    uint8_t wartZasilanie;
    uint8_t port;
    uint8_t pin;

    for (uint8_t i = 0; i < liczbaSerwomechanizmow; i++) {
        stan = &serwomechanizmy[i];
        dane = PobierzKonfSerwomechanizmu(stan->Urzadzenie);

        PobierzPortPin(dane->PinZasilanie, &portZasilanie, &pinZasilanie);
        wskaznikZasilanie = (uint8_t*)(PORTD_NUM + portZasilanie * 3);
        wartZasilanie = (1 << pinZasilanie);
        
        if (stan->Flagi.Wlaczony) {
            *wskaznikZasilanie |= wartZasilanie;
            PobierzPortPin(dane->PinSygnal, &port, &pin);
            *((uint8_t*)(PORTD_NUM + port * 3)) |= (1 << pin);
        } else {
            *wskaznikZasilanie &= ~wartZasilanie;
        }
    }

    serwSortIx = 0;
    wlaczPWM = 0;
}

void Sortuj() {
    for (uint8_t i = 0; i < liczbaSerwomechanizmow; i++) {
        volatile StanSerwomechanizmu* s;
        uint16_t min = serwomechanizmyPosortowane[i]->Wartosc;
        uint8_t ix = i;

        for (uint8_t j = i; j < liczbaSerwomechanizmow; j++) {
            s = serwomechanizmyPosortowane[j];
            if (s->Wartosc < min) {
                min = s->Wartosc;
                ix = j;
            }
        }

        s = serwomechanizmyPosortowane[ix];
        serwomechanizmyPosortowane[ix] = serwomechanizmyPosortowane[i];
        serwomechanizmyPosortowane[i] = s;
    }

    posortowano = 1;
}

uint8_t PobierzStanPinuWejsciowego(uint8_t nr) {
    uint8_t port;
    uint8_t pin;
    PobierzPortPin(nr, &port, &pin);
    uint8_t* wskaznikPortu = (uint8_t*)(PIND_NUM + port * 3);
    return (*wskaznikPortu >> pin) & 1;
}

void PrzetworzWejscia() {
    for (uint8_t i = 0; i < liczbaSerwomechanizmow; i++) {
        volatile StanSerwomechanizmu* stan = &serwomechanizmy[i];
        DaneSerwomechanizmu* dane = PobierzKonfSerwomechanizmu(stan->Urzadzenie);
        uint8_t pozGlowna = PobierzStanPinuWejsciowego(dane->PinPozGlowna);
        uint8_t pozInna = PobierzStanPinuWejsciowego(dane->PinPozInna);

        if (pozGlowna != stan->Flagi.PozGlowna || pozInna != stan->Flagi.PozInna) {
            stan->Flagi.PozGlowna = pozGlowna;
            stan->Flagi.PozInna = pozInna;

            uint8_t typKom = 0;
            uint16_t ustawienieUrz = 0;
            
            if (stan->Urzadzenie->Typ == TypUrz_Zwrotnica) {
                typKom = TYP_KOM_ZMIENIONO_STAN_ZWROTNICY;
                ustawienieUrz = Zwrotnica_Niezdefiniowany;

                if (pozGlowna != 0) ustawienieUrz |= Zwrotnica_Plus;
                if (pozInna != 0)   ustawienieUrz |= Zwrotnica_Minus;

            } else if (stan->Urzadzenie->Typ == TypUrz_Rogatka) {
                typKom = TYP_KOM_ZMIENIONO_STAN_ROGATKI;
                ustawienieUrz = Rogatka_Niezdefiniowany;

                if (pozGlowna != 0) ustawienieUrz |= Rogatka_Otwarta;
                if (pozInna != 0)   ustawienieUrz |= Rogatka_Zamknieta;
            }

            if (ustawienieUrz != 0 && stan->Flagi.TrybSerwisowy == 0) {
                stan->Flagi.Wlaczony = 0;
                stan->Docelowa = stan->Wartosc;
                stan->Zmiana = 0;
            }

            if (typKom != 0) {
                Komunikat* odp = RozpocznijKomunikatWyjsciowy();
                odp->Typ = typKom;
                odp->AdresPosterunku = 0;
                odp->AdresUrzadzenia = stan->Urzadzenie->Adres;
                odp->Dane = ustawienieUrz;
                ZakonczKomunikatWyjsciowy();
            }
        }
    }

    czasPrzetworzWejscia = 0;
}

int main() {
    InicjalizujSerwomechanizmy();
    UstawKierunekPinow();
    InicjalizujUart();

    TCCR1B = (1 << WGM12) | (1 << CS11);
    TIMSK = (1 << OCIE1A);
    OCR1A = DLUGOSC_SYGNALU_MS * CZAS_US;
    
    sei();

    while (1) {
        uint16_t licznik = TCNT1;

        if (licznik > WYPELNIENIE_MIN && licznik < WYPELNIENIE_MAX) {
            WylaczSygnalPWM(licznik);
        } else if (licznik > WYPELNIENIE_MAX && licznik < WYPELNIENIE_SORTUJ) {
            if (posortowano == 1) ZmienWartosci();
            PrzetworzKomunikatyWejsciowe();
        } else if (licznik > WYPELNIENIE_SORTUJ && posortowano == 0) {
            Sortuj();
        } 

        if (wlaczPWM) {
            WlaczSygnalPWM();
        }

        if (czasPrzetworzWejscia >= CZAS_PRZETWARZANIE_WEJSC) {
            PrzetworzWejscia();
        }
    }
    
    return 0;
}

ISR(TIMER1_COMPA_vect) {
    wlaczPWM = 1;
    czasPrzetworzWejscia++;
}