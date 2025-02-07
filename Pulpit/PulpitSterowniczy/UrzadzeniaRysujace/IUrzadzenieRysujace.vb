Public Interface IUrzadzenieRysujace(Of TOlowek, TPedzel, TMacierz, TCzcionka)
    Sub Inicjalizuj(uchwyt As IntPtr, szer As UInteger, wys As UInteger)
    Sub RozpocznijRysunek(grp As Graphics, kolorTla As Color)
    Sub ZakonczRysunek()
    Sub ZmienRozmiar(szer As UInteger, wys As UInteger)
    Function UtworzOlowek(kolor As Color) As TOlowek
    Function UtworzPedzel(kolor As Color) As TPedzel

    Sub TransformacjaResetuj()
    Sub TransformacjaDolacz(tr As TMacierz)
    Function TransformacjaPobierz() As TMacierz
    Sub TransformacjaPrzesun(x As Single, y As Single)
    Sub TransformacjaObroc(kat As Single, srodekX As Single, srodekY As Single)
    Sub TransformacjaSkaluj(skalowanie As Single)

    Sub UstawPrzyciecie(x As Single, y As Single, szer As Single, wys As Single)
    Sub UsunPrzyciecie()

    Sub RysujLinie(pedzel As TOlowek, grubosc As Single, x1 As Single, y1 As Single, x2 As Single, y2 As Single)
    Sub RysujProstokat(pedzel As TOlowek, grubosc As Single, x As Single, y As Single, szer As Single, wys As Single)

    Sub WypelnijProstokat(pedzel As TPedzel, x As Single, y As Single, szer As Single, wys As Single)
    Sub WypelnijFigure(pedzel As TPedzel, punkty As PointF())
    Sub WypelnijKolo(pedzel As TPedzel, srodekX As Single, srodekY As Single, r As Single)
    Sub WypelnijTloSygnalizatora(pedzel As TPedzel, poczX As Single, koncX As Single, y As Single, r As Single)

    Function UtworzCzcionke(nazwa As String, rozmiar As Single, tymczasowa As Boolean) As TCzcionka
    Function ZmierzTekst(czcionka As TCzcionka, tekst As String, szer As Single, wys As Single) As SizeF
    Sub RysujTekst(pedzel As TPedzel, czcionka As TCzcionka, tekst As String, x As Single, y As Single, szer As Single, wys As Single)
End Interface