Friend Module Dodatki
    Public Function KolorRGB(wartosc As String, Optional przezroczystosc As Byte = 255) As Color
        If wartosc.Length <> 7 Then
            Throw New ArgumentException("Wartość koloru musi być siedmioznakowym ciagiem.")
        End If

        Return Color.FromArgb(
            przezroczystosc,
            Integer.Parse(wartosc(1) & wartosc(2), Globalization.NumberStyles.HexNumber),
            Integer.Parse(wartosc(3) & wartosc(4), Globalization.NumberStyles.HexNumber),
            Integer.Parse(wartosc(5) & wartosc(6), Globalization.NumberStyles.HexNumber)
            )
    End Function

    Public Class PrzeciaganaKostka
        Public Kostka As Zaleznosci.Kostka
        Public Zrodlo As IntPtr

        Public Sub New(kostka As Zaleznosci.Kostka, zrodlo As IntPtr)
            Me.Kostka = kostka
            Me.Zrodlo = zrodlo
        End Sub
    End Class

    Public Enum TypRysownika
        KlasycznyGDI
        KlasycznyDirect2D
    End Enum

    Public Enum RysujDodatkoweObiekty
        Nic
        Lampy
        OdcinkiTorow
        Liczniki
        Przejazdy
        PrzejazdyAutomatyzacja
        PrzejazdyRogatki
        PrzejazdySygnDrog
    End Enum
End Module