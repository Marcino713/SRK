Public Class UrzadzenieRysujaceDirect2D
    Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr), IDisposable

    Private Declare Auto Function InicjalizujD2D Lib "ObslugaDirectX.dll" (uchwyt As IntPtr, szer As UInteger, wys As UInteger) As IntPtr
    Private Declare Auto Sub RozpocznijRysunekD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, r As Single, g As Single, b As Single)
    Private Declare Auto Sub ZakonczRysunekD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr)
    Private Declare Auto Sub ZmienRozmiarD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, szer As UInteger, wys As UInteger)
    Private Declare Auto Function UtworzPedzelD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, r As Single, g As Single, b As Single, a As Single) As IntPtr
    Private Declare Auto Sub PosprzatajD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr)

    Private Declare Auto Sub TransformacjaResetujD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr)
    Private Declare Auto Sub TransformacjaDolaczD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, tr As IntPtr)
    Private Declare Auto Function TransformacjaPobierzD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr) As IntPtr
    Private Declare Auto Sub TransformacjaPrzesunD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, x As Single, y As Single)
    Private Declare Auto Sub TransformacjaObrocD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, kat As Single, srodekX As Single, srodekY As Single)
    Private Declare Auto Sub TransformacjaSkalujD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, skalowanie As Single)

    Private Declare Auto Sub RysujLinieD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, pedzel As IntPtr, grubosc As Single, x1 As Single, y1 As Single, x2 As Single, y2 As Single)
    Private Declare Auto Sub RysujProstokatD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, pedzel As IntPtr, grubosc As Single, lewo As Single, gora As Single, prawo As Single, dol As Single)

    Private Declare Auto Sub WypelnijProstokatD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, pedzel As IntPtr, lewo As Single, gora As Single, prawo As Single, dol As Single)
    Private Declare Auto Sub WypelnijFigureD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, pedzel As IntPtr, sciezka As IntPtr)
    Private Declare Auto Sub WypelnijKoloD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, pedzel As IntPtr, x As Single, y As Single, r As Single)

    Private Declare Auto Function UtworzGeometrieSciezkowaD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, x As Single, y As Single) As IntPtr
    Private Declare Auto Sub DodajLinieDoGeometriiD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, x As Single, y As Single)
    Private Declare Auto Sub DodajLukDoGeometriiD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, x As Single, y As Single, r As Single)
    Private Declare Auto Sub ZakonczGeometrieD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr)

    Private Declare Auto Function UtworzCzcionkeD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, nazwaCzcionki As String, rozmiar As Single) As IntPtr
    Private Declare Auto Function ZmierzTekstD2D Lib "ObslugaDirectX.dll" (czcionka As IntPtr, tekst As String, szer As Single, wys As Single) As SizeF
    Private Declare Auto Sub RysujTekstD2D Lib "ObslugaDirectX.dll" (hOkno As IntPtr, pedzel As IntPtr, czcionka As IntPtr, tekst As String, x As Single, y As Single, szer As Single, wys As Single)

    Private Const MAX_BAJT As Single = 255.0F
    Private Const PRZELICZNIK_ROZM_CZIONKI As Single = 0.96F / 0.72F

    Private hOkno As IntPtr
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            PosprzatajD2D(hOkno)
            disposedValue = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(disposing:=False)
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Sub Inicjalizuj(uchwyt As IntPtr, szer As UInteger, wys As UInteger) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).Inicjalizuj
        hOkno = InicjalizujD2D(uchwyt, szer, wys)
    End Sub

    Public Sub RozpocznijRysunek(grp As Graphics, kolorTla As Color) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).RozpocznijRysunek
        RozpocznijRysunekD2D(hOkno, kolorTla.R / MAX_BAJT, kolorTla.G / MAX_BAJT, kolorTla.B / MAX_BAJT)
    End Sub

    Public Sub ZakonczRysunek() Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).ZakonczRysunek
        ZakonczRysunekD2D(hOkno)
    End Sub

    Public Sub ZmienRozmiar(szer As UInteger, wys As UInteger) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).ZmienRozmiar
        ZmienRozmiarD2D(hOkno, szer, wys)
    End Sub

    Public Function UtworzOlowek(kolor As Color) As IntPtr Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).UtworzOlowek
        Return UtworzPedzel(kolor)
    End Function

    Public Function UtworzPedzel(kolor As Color) As IntPtr Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).UtworzPedzel
        Return UtworzPedzelD2D(hOkno, kolor.R / MAX_BAJT, kolor.G / MAX_BAJT, kolor.B / MAX_BAJT, kolor.A / MAX_BAJT)
    End Function

    Public Sub TransformacjaResetuj() Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).TransformacjaResetuj
        TransformacjaResetujD2D(hOkno)
    End Sub

    Public Sub TransformacjaDolacz(tr As IntPtr) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).TransformacjaDolacz
        TransformacjaDolaczD2D(hOkno, tr)
    End Sub

    Public Function TransformacjaPobierz() As IntPtr Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).TransformacjaPobierz
        Return TransformacjaPobierzD2D(hOkno)
    End Function

    Public Sub TransformacjaPrzesun(x As Single, y As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).TransformacjaPrzesun
        TransformacjaPrzesunD2D(hOkno, x, y)
    End Sub

    Public Sub TransformacjaObroc(kat As Single, srodekX As Single, srodekY As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).TransformacjaObroc
        TransformacjaObrocD2D(hOkno, kat, srodekX, srodekY)
    End Sub

    Public Sub TransformacjaSkaluj(skalowanie As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).TransformacjaSkaluj
        TransformacjaSkalujD2D(hOkno, skalowanie)
    End Sub

    Public Sub RysujLinie(pedzel As IntPtr, grubosc As Single, x1 As Single, y1 As Single, x2 As Single, y2 As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).RysujLinie
        RysujLinieD2D(hOkno, pedzel, grubosc, x1, y1, x2, y2)
    End Sub

    Public Sub RysujProstokat(pedzel As IntPtr, grubosc As Single, x As Single, y As Single, szer As Single, wys As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).RysujProstokat
        RysujProstokatD2D(hOkno, pedzel, grubosc, x, y, x + szer, y + wys)
    End Sub

    Public Sub WypelnijProstokat(pedzel As IntPtr, x As Single, y As Single, szer As Single, wys As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).WypelnijProstokat
        WypelnijProstokatD2D(hOkno, pedzel, x, y, x + szer, y + wys)
    End Sub

    Public Sub WypelnijFigure(pedzel As IntPtr, punkty() As PointF) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).WypelnijFigure
        Dim figura As IntPtr = UtworzGeometrieSciezkowaD2D(hOkno, punkty(0).X, punkty(0).Y)

        For i As Integer = 1 To punkty.Length - 1
            DodajLinieDoGeometriiD2D(hOkno, punkty(i).X, punkty(i).Y)
        Next

        ZakonczGeometrieD2D(hOkno)
        WypelnijFigureD2D(hOkno, pedzel, figura)
    End Sub

    Public Sub WypelnijKolo(pedzel As IntPtr, srodekX As Single, srodekY As Single, r As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).WypelnijKolo
        WypelnijKoloD2D(hOkno, pedzel, srodekX, srodekY, r)
    End Sub

    Public Sub WypelnijTloSygnalizatora(pedzel As IntPtr, poczX As Single, koncX As Single, y As Single, r As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).WypelnijTloSygnalizatora
        Dim figura As IntPtr = UtworzGeometrieSciezkowaD2D(hOkno, poczX, y + r)
        DodajLukDoGeometriiD2D(hOkno, poczX, y - r, r)
        DodajLinieDoGeometriiD2D(hOkno, koncX, y - r)
        DodajLukDoGeometriiD2D(hOkno, koncX, y + r, r)
        ZakonczGeometrieD2D(hOkno)
        WypelnijFigureD2D(hOkno, pedzel, figura)
    End Sub

    Public Function UtworzCzcionke(nazwaCzcionki As String, rozmiar As Single) As IntPtr Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).UtworzCzcionke
        Return UtworzCzcionkeD2D(hOkno, nazwaCzcionki, rozmiar * PRZELICZNIK_ROZM_CZIONKI)
    End Function

    Public Function ZmierzTekst(czcionka As IntPtr, tekst As String, szer As Single, wys As Single) As SizeF Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).ZmierzTekst
        If tekst Is Nothing Then Return New SizeF(0.0F, 0.0F)

        Return ZmierzTekstD2D(czcionka, tekst, szer, wys)
    End Function

    Public Sub RysujTekst(pedzel As IntPtr, czcionka As IntPtr, tekst As String, x As Single, y As Single, szer As Single, wys As Single) Implements IUrzadzenieRysujace(Of IntPtr, IntPtr, IntPtr, IntPtr).RysujTekst
        If tekst Is Nothing Then Exit Sub

        RysujTekstD2D(hOkno, pedzel, czcionka, tekst, x, y, szer, wys)
    End Sub
End Class