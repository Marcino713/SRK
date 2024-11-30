Public Class OtworzRogatkeUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.OTWORZ_ROGATKE
        End Get
    End Property

    Public Property CzasOtwieraniaMs As UShort

    Public Overrides Function ZapiszKomunikat() As UShort
        Return CzasOtwieraniaMs
    End Function
End Class