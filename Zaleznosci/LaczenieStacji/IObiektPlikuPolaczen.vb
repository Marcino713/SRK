Friend Interface IObiektPlikuPolaczen
    Function Zapisz(uzytePliki As Dictionary(Of LaczonyPlikStacji, Integer)) As Byte()
    Sub Otworz(dane As Byte(), konf As KonfiguracjaOdczytuPolaczen, polaczenia As PolaczeniaStacji)
End Interface