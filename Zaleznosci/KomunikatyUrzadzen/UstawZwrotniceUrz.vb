Public Class UstawZwrotniceUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_ZWROTNICE
        End Get
    End Property

    Public Property Ustawienie As UstawienieRozjazduEnum

    Protected Overrides Function ZapiszKomunikat() As UShort
        Return CUShort(Ustawienie)
    End Function
End Class