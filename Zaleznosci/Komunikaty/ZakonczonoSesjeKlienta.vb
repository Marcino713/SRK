Public Class ZakonczonoSesjeKlienta
    Inherits Komunikat

    Public Property Przyczyna As PrzyczynaZakonczeniaSesjiKlienta

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZAKONCZONO_SESJE_KLIENTA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Przyczyna)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZakonczonoSesjeKlienta
        kom.Przyczyna = CType(br.ReadByte(), PrzyczynaZakonczeniaSesjiKlienta)

        Return kom
    End Function
End Class

Public Enum PrzyczynaZakonczeniaSesjiKlienta As Byte
    RozlaczenieKlienta
    RozlaczeniePrzezSerwer
    PrzekroczenieCzasuOczekiwania
End Enum