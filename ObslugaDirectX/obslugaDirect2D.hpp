#pragma once
#ifndef OBSLUGA_DIRECT2D_H
#define OBSLUGA_DIRECT2D_H

#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <d2d1.h>
#include <dwrite.h>
#include <vector>
#include "narzedzia.hpp"

#define DLL __declspec(dllexport) _stdcall
typedef int wskaznik;

struct DaneOkna {
	std::vector<D2D1::Matrix3x2F*> transformacje;
	std::vector<ID2D1SolidColorBrush*> pedzle;
	std::vector<ID2D1PathGeometry*> sciezki;
	std::vector<IDWriteTextFormat*> czcionki;
	ID2D1HwndRenderTarget* obraz = NULL;
	ID2D1GeometrySink* zlew = NULL;
};

extern "C" {
	wskaznik DLL InicjalizujD2D(HWND uchwyt, UINT szer, UINT wys);
	void DLL RozpocznijRysunekD2D(DaneOkna* hOkno);
	void DLL ZakonczRysunekD2D(DaneOkna* hOkno);
	void DLL ZmienRozmiarD2D(DaneOkna* hOkno, UINT szer, UINT wys);
	wskaznik DLL UtworzPedzelD2D(DaneOkna* hOkno, float r, float g, float b, float a);
	void DLL PosprzatajD2D(DaneOkna* hOkno);

	void DLL TransformacjaResetujD2D(DaneOkna* hOkno);
	void DLL TransformacjaDolaczD2D(DaneOkna* hOkno, D2D1::Matrix3x2F* tranformacja);
	wskaznik DLL TransformacjaPobierzD2D(DaneOkna* hOkno);
	void DLL TransformacjaPrzesunD2D(DaneOkna* hOkno, float x, float y);
	void DLL TransformacjaObrocD2D(DaneOkna* hOkno, float kat, float srodekX, float srodekY);
	void DLL TransformacjaSkalujD2D(DaneOkna* hOkno, float skalowanie);

	void DLL RysujLinieD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float grubosc, float x1, float y1, float x2, float y2);
	void DLL RysujProstokatD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float grubosc, float lewo, float gora, float prawo, float dol);

	void DLL CzyscD2D(DaneOkna* hOkno, float r, float g, float b);
	void DLL WypelnijProstokatD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float lewo, float gora, float prawo, float dol);
	void DLL WypelnijFigureD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, ID2D1PathGeometry* sciezka);
	void DLL WypelnijKoloD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float srodekX, float srodekY, float r);

	wskaznik DLL UtworzGeometrieSciezkowaD2D(DaneOkna* hOkno, float x, float y);
	void DLL DodajLinieDoGeometriiD2D(DaneOkna* hOkno, float x, float y);
	void DLL DodajLukDoGeometriiD2D(DaneOkna* hOkno, float x, float y, float r);
	void DLL ZakonczGeometrieD2D(DaneOkna* hOkno);

	wskaznik DLL UtworzCzcionkeD2D(DaneOkna* hOkno, wchar_t* nazwaCzcionki, float rozmiar);
	D2D1_SIZE_F DLL ZmierzTekstD2D(IDWriteTextFormat* czcionka, wchar_t* tekst, float szer, float wys);
	void DLL RysujTekstD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, IDWriteTextFormat* czcionka, wchar_t* tekst, float x, float y, float szer, float wys);
}

extern ID2D1Factory* FabrykaDirect2D;
extern IDWriteFactory* FabrykaDirectWrite;

#endif