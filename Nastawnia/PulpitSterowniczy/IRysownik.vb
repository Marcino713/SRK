Friend Interface IRysownik
    ReadOnly Property KOLKO_SZER As Single
    ReadOnly Property KOLOR_TOR_TEN_ODCINEK As Color
    ReadOnly Property KOLOR_TOR_PRZYPISANY As Color
    ReadOnly Property KOLOR_TOR_NIEPRZYPISANY As Color
    ReadOnly Property KOLOR_TOR_LICZNIK_ODCINEK_2 As Color

    Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics)
End Interface