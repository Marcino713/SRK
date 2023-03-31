Public Class UstawStanSygnalizatoraManewrowegoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Property Stan As StanSygnalizatoraManewrowego

    Protected Overrides Function ZapiszKomunikat() As UShort
        Dim stanSwiatel As UShort
        Dim kolejnosc As Integer = If(Stan = StanSygnalizatoraManewrowego.BrakWyjazdu, 0, 1)

        ZapiszStanSwiatla(stanSwiatel, kolejnosc, StanSwiatlaSygnalizatora.Wlaczone)
        Return stanSwiatel
    End Function
End Class