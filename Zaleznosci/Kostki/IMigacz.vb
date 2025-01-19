Public Interface IMigacz
    ReadOnly Property WysokiStanMigania As Boolean

    Sub UstawKostke(kostka As Kostka)
    Sub PrzelaczStan()
    Sub Wylacz()
End Interface