Public Class UstawJasnoscLampyUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_JASNOSC_LAMPY
        End Get
    End Property

    Public Property Jasnosc As Byte

    Protected Overrides Function ZapiszKomunikat() As UShort
        Return Jasnosc
    End Function
End Class