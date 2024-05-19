#ifndef OGOLNE_H
#define OGOLNE_H

#define F_CPU 8000000UL

#ifndef __AVR_ATmega8__
#define __AVR_ATmega8__
#endif

#define NULL 0
// Liczba portow
#define LICZBA_PORTOW 3
// Liczba pinow w jednym porcie
#define LICZBA_PINOW 8
// Liczba obslugiwanych pinow
#define LICZBA_WYJSC (LICZBA_PORTOW * LICZBA_PINOW)
// Numer pinu o najwiekszej wartosci
#define PIN_MAX 0x80
// Adres portu D jako liczba
#define PORTD_NUM 0x32
// Adres pinu D jako liczba
#define PIND_NUM 0x30

#endif