Public Module Dodatki
    Private Const KOLOR_MAX As Single = 255.0F

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

    Public Function KolorSkaliPredkosci(predkosc As UShort, predkoscMax As UShort) As Color
        If predkoscMax = 0 Then
            Return Color.FromArgb(CInt(KOLOR_MAX), 0, 0)
        Else
            Dim wsp As Single = CSng(predkosc) / predkoscMax
            If wsp < 0.0F Then wsp = 0.0F
            If wsp > 1.0F Then wsp = 1.0F
            Return Color.FromArgb(CInt((1.0F - wsp) * KOLOR_MAX), CInt(wsp * KOLOR_MAX), 0)
        End If
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
        PredkosciTorow
    End Enum

    Public Enum DodatkoweObiektyTrybDzialania
        Nic
        LicznikiOsi
    End Enum
End Module