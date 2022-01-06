Friend Interface IObiektPliku(Of TZapis, TOdczyt)
    Function Zapisz(konf As TZapis) As Byte()
    Sub Otworz(dane As Byte(), konf As TOdczyt)
End Interface