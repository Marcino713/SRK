Public Interface IRysownik
    ReadOnly Property KOLKO_SZER As Single
    ReadOnly Property KOLOR_TOR_TEN_ODCINEK As Color
    ReadOnly Property KOLOR_TOR_PRZYPISANY As Color
    ReadOnly Property KOLOR_TOR_NIEPRZYPISANY As Color
    ReadOnly Property KOLOR_TOR_LICZNIK_ODCINEK_2 As Color
    ReadOnly Property KOLOR_TLO_SYGNALIZATOR_WYROZNIONY As Color

    Property UniewaznioneSasiedztwoTorow As Boolean

    Sub Inicjalizuj(uchwyt As IntPtr, szer As UInteger, wys As UInteger)
    Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics)
    Sub ZmienRozmiar(szer As UInteger, wys As UInteger)
End Interface