Public Class ZakonczDzialanieKlienta 'k
    Inherits Komunikat

    Public Property Przyczyna As PrzyczynaZakonczeniaDzialaniaKlienta

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZAKONCZ_DZIALANIE_KLIENTA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Przyczyna)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZakonczDzialanieKlienta
        kom.Przyczyna = CType(br.ReadByte(), PrzyczynaZakonczeniaDzialaniaKlienta)

        Return kom
    End Function
End Class

Public Enum PrzyczynaZakonczeniaDzialaniaKlienta As Byte
    ZatrzymanieKlienta
    BladOtwarciaPlikuPosterunku
End Enum