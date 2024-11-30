Public Class UstawStanSygnalizatoraDrogowegoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_STAN_SYGNALIZATORA_DROGOWEGO
        End Get
    End Property

    Public Property Wlaczony As Boolean

    Public Overrides Function ZapiszKomunikat() As UShort
        Return If(Wlaczony, 1US, 0US)
    End Function
End Class