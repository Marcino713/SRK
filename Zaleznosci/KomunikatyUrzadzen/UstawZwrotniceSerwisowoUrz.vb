Public Class UstawZwrotniceSerwisowoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_ZWROTNICE_SERWISOWO
        End Get
    End Property

    Public Property CzasWypelnienia As UShort

    Protected Overrides Function ZapiszKomunikat() As UShort
        Return CzasWypelnienia
    End Function
End Class