Public Class ZamknijRogatkeUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.ZAMKNIJ_ROGATKE
        End Get
    End Property

    Public Property CzasZamykaniaMs As UShort

    Protected Overrides Function ZapiszKomunikat() As UShort
        Return CzasZamykaniaMs
    End Function
End Class