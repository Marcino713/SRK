Public Class UstawStanSygnalizatoraPrzejazdowegoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Property Stan As StanSygnalizatoraOstrzegawczegoPrzejazdowego

    Public Overrides Function ZapiszKomunikat() As UShort
        Dim stanSwiatel As UShort

        If Stan = StanSygnalizatoraOstrzegawczegoPrzejazdowego.PrzejazdZamkniety Then
            ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Wlaczone)
            ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Wlaczone)

        ElseIf Stan = StanSygnalizatoraOstrzegawczegoPrzejazdowego.PrzejazdUszkodzony Then
            ZapiszStanSwiatla(stanSwiatel, 2, StanSwiatlaSygnalizatora.Wlaczone)
            ZapiszStanSwiatla(stanSwiatel, 3, StanSwiatlaSygnalizatora.Wlaczone)

        End If

        Return stanSwiatel
    End Function
End Class