Public Interface IZakret
    Property PrzytnijZakret As PrzycinanieZakretu
End Interface

<Flags>
Public Enum PrzycinanieZakretu
    Prawo = 1
    Dol = 2
    UmniejszPrawo = 4
    UmniejszDol = 8
End Enum