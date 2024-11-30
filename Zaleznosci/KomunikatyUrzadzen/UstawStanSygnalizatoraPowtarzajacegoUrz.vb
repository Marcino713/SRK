Public Class UstawStanSygnalizatoraPowtarzajacegoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Property PredkoscPowtarzana As PredkoscPowtarzanaSygnalizatora

    Public Overrides Function ZapiszKomunikat() As UShort
        Dim stanSwiatel As UShort

        Select Case PredkoscPowtarzana
            Case PredkoscPowtarzanaSygnalizatora.V0
                ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Wlaczone)
            Case PredkoscPowtarzanaSygnalizatora.V40V60
                ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Migajace)
            Case PredkoscPowtarzanaSygnalizatora.V100
                ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Migajace)
            Case PredkoscPowtarzanaSygnalizatora.VMax
                ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Wlaczone)
        End Select

        ZapiszStanSwiatla(stanSwiatel, 2, StanSwiatlaSygnalizatora.Wlaczone)
        Return stanSwiatel
    End Function

End Class