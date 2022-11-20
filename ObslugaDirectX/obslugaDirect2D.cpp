#include "obslugaDirect2D.hpp"

ID2D1Factory* FabrykaDirect2D = NULL;
IDWriteFactory* FabrykaDirectWrite = NULL;

wskaznik DLL InicjalizujD2D(HWND uchwyt, UINT szer, UINT wys) {
	DaneOkna* okno = new DaneOkna;
	FabrykaDirect2D->CreateHwndRenderTarget(D2D1::RenderTargetProperties(), D2D1::HwndRenderTargetProperties(uchwyt, D2D1::SizeU(szer, wys)), &okno->obraz);
	return (wskaznik)okno;
}

void DLL RozpocznijRysunekD2D(DaneOkna* hOkno, float r, float g, float b) {
	hOkno->obraz->BeginDraw();
	hOkno->obraz->Clear(D2D1::ColorF(r, g, b));
}

void DLL ZakonczRysunekD2D(DaneOkna* hOkno) {
	hOkno->obraz->EndDraw();

	for (size_t i = 0; i < hOkno->sciezki.size(); i++) SafeRelease(&hOkno->sciezki[i]);
	hOkno->sciezki.clear();
	for (size_t i = 0; i < hOkno->transformacje.size(); i++) delete hOkno->transformacje[i];
	hOkno->transformacje.clear();
}

void DLL ZmienRozmiarD2D(DaneOkna* hOkno, UINT szer, UINT wys) {
	if (hOkno->obraz == NULL) return;
	hOkno->obraz->Resize(D2D1::SizeU(szer, wys));
}

wskaznik DLL UtworzPedzelD2D(DaneOkna* hOkno, float r, float g, float b, float a) {
	ID2D1SolidColorBrush* pedzel = NULL;
	hOkno->obraz->CreateSolidColorBrush(D2D1::ColorF(r, g, b, a), &pedzel);
	hOkno->pedzle.push_back(pedzel);
	return (wskaznik)pedzel;
}

void DLL PosprzatajD2D(DaneOkna* hOkno) {
	for (size_t i = 0; i < hOkno->pedzle.size(); i++)   SafeRelease(&hOkno->pedzle[i]);
	for (size_t i = 0; i < hOkno->czcionki.size(); i++) SafeRelease(&hOkno->czcionki[i]);

	SafeRelease(&hOkno->obraz);
	delete hOkno;
}

void DLL TransformacjaResetujD2D(DaneOkna* hOkno) {
	hOkno->obraz->SetTransform(D2D1::Matrix3x2F::Identity());
}

void DLL TransformacjaDolaczD2D(DaneOkna* hOkno, D2D1::Matrix3x2F* tranformacja) {
	D2D1::Matrix3x2F tr;
	hOkno->obraz->GetTransform(&tr);
	hOkno->obraz->SetTransform(tr * (*tranformacja));
}

wskaznik DLL TransformacjaPobierzD2D(DaneOkna* hOkno) {
	D2D1::Matrix3x2F* tr = new D2D1::Matrix3x2F;
	hOkno->obraz->GetTransform(tr);
	hOkno->transformacje.push_back(tr);
	return (wskaznik)tr;
}

void DLL TransformacjaPrzesunD2D(DaneOkna* hOkno, float x, float y) {
	D2D1::Matrix3x2F transformacja;
	hOkno->obraz->GetTransform(&transformacja);

	hOkno->obraz->SetTransform(transformacja * D2D1::Matrix3x2F::Translation(x, y));
}

void DLL TransformacjaObrocD2D(DaneOkna* hOkno, float kat, float srodekX, float srodekY) {
	D2D1::Matrix3x2F transformacja;
	hOkno->obraz->GetTransform(&transformacja);

	hOkno->obraz->SetTransform(transformacja * D2D1::Matrix3x2F::Rotation(kat, D2D1::Point2F(srodekX, srodekY)));
}

void DLL TransformacjaSkalujD2D(DaneOkna* hOkno, float skalowanie) {
	D2D1::Matrix3x2F transformacja;
	hOkno->obraz->GetTransform(&transformacja);

	hOkno->obraz->SetTransform(transformacja * D2D1::Matrix3x2F::Scale(skalowanie, skalowanie));
}

void DLL RysujLinieD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float grubosc, float x1, float y1, float x2, float y2) {
	hOkno->obraz->DrawLine(D2D1::Point2F(x1, y1), D2D1::Point2F(x2, y2), pedzel, grubosc);
}

void DLL RysujProstokatD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float grubosc, float lewo, float gora, float prawo, float dol) {
	hOkno->obraz->DrawRectangle(D2D1::RectF(lewo, gora, prawo, dol), pedzel, grubosc);
}

void DLL WypelnijProstokatD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float lewo, float gora, float prawo, float dol) {
	hOkno->obraz->FillRectangle(D2D1::RectF(lewo, gora, prawo, dol), pedzel);
}

void DLL WypelnijFigureD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, ID2D1PathGeometry* sciezka) {
	hOkno->obraz->FillGeometry(sciezka, pedzel);
}

void DLL WypelnijKoloD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, float srodekX, float srodekY, float r) {
	hOkno->obraz->FillEllipse(D2D1::Ellipse(D2D1::Point2F(srodekX, srodekY), r, r), pedzel);
}

wskaznik DLL UtworzGeometrieSciezkowaD2D(DaneOkna* hOkno, float x, float y) {
	ID2D1PathGeometry* sciezka = NULL;
	FabrykaDirect2D->CreatePathGeometry(&sciezka);
	sciezka->Open(&hOkno->zlew);
	hOkno->zlew->BeginFigure(D2D1::Point2F(x, y), D2D1_FIGURE_BEGIN_FILLED);
	hOkno->sciezki.push_back(sciezka);

	return (wskaznik)sciezka;
}

void DLL DodajLinieDoGeometriiD2D(DaneOkna* hOkno, float x, float y) {
	hOkno->zlew->AddLine(D2D1::Point2F(x, y));
}

void DLL DodajLukDoGeometriiD2D(DaneOkna* hOkno, float x, float y, float r) {
	hOkno->zlew->AddArc(D2D1::ArcSegment(D2D1::Point2F(x, y), D2D1::SizeF(r, r), 0, D2D1_SWEEP_DIRECTION_CLOCKWISE, D2D1_ARC_SIZE_SMALL));
}

void DLL ZakonczGeometrieD2D(DaneOkna* hOkno) {
	hOkno->zlew->EndFigure(D2D1_FIGURE_END_CLOSED);
	hOkno->zlew->Close();
	SafeRelease(&hOkno->zlew);
}

wskaznik DLL UtworzCzcionkeD2D(DaneOkna* hOkno, wchar_t* nazwaCzcionki, float rozmiar) {
	IDWriteTextFormat* czcionka = NULL;
	FabrykaDirectWrite->CreateTextFormat(
		nazwaCzcionki,
		NULL,
		DWRITE_FONT_WEIGHT_NORMAL,
		DWRITE_FONT_STYLE_NORMAL,
		DWRITE_FONT_STRETCH_NORMAL,
		rozmiar,
		L"",
		&czcionka
	);
	hOkno->czcionki.push_back(czcionka);

	return (wskaznik)czcionka;
}

D2D1_SIZE_F DLL ZmierzTekstD2D(IDWriteTextFormat* czcionka, wchar_t* tekst, float szer, float wys) {
	IDWriteTextLayout* obszar = NULL;
	FabrykaDirectWrite->CreateTextLayout(tekst, wcslen(tekst), czcionka, szer, wys, &obszar);
	DWRITE_TEXT_METRICS m;
	obszar->GetMetrics(&m);
	SafeRelease(&obszar);

	return D2D1::SizeF(m.width, min(m.height, wys));
}

void DLL RysujTekstD2D(DaneOkna* hOkno, ID2D1SolidColorBrush* pedzel, IDWriteTextFormat* czcionka, wchar_t* tekst, float x, float y, float szer, float wys) {
	hOkno->obraz->DrawTextW(
		tekst,
		wcslen(tekst),
		czcionka,
		D2D1::RectF(x, y, x + szer, y + wys),
		pedzel,
		D2D1_DRAW_TEXT_OPTIONS::D2D1_DRAW_TEXT_OPTIONS_CLIP);
}