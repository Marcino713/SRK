Friend Interface IObiektPliku
    Function Zapisz(konf As KonfiguracjaZapisu) As Byte()
    Sub Otworz(dane As Byte(), konf As KonfiguracjaOdczytu, pulpit As Pulpit)
End Interface