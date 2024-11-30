Public Class UstawStanSygnalizatoraPolsamoczynnegoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Property PredkoscPowtarzana As PredkoscPowtarzanaSygnalizatora
    Public Property Predkosc As PredkoscSygnalizatora
    Public Property KierunekPrzeciwny As Boolean

    Public Overrides Function ZapiszKomunikat() As UShort
        Dim stanSwiatel As UShort

        If Predkosc <> PredkoscSygnalizatora.Manewrowy AndAlso Predkosc <> PredkoscSygnalizatora.Zastepczy AndAlso Predkosc <> PredkoscSygnalizatora.V0 Then
            Select Case PredkoscPowtarzana
                Case PredkoscPowtarzanaSygnalizatora.V0
                    ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Wlaczone)
                Case PredkoscPowtarzanaSygnalizatora.V40V60
                    ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Migajace)
                Case PredkoscPowtarzanaSygnalizatora.V100
                    ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Migajace)
                Case PredkoscPowtarzanaSygnalizatora.VMax
                    ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Wlaczone)
            End Select

            If KierunekPrzeciwny Then
                ZapiszStanSwiatla(stanSwiatel, 7, StanSwiatlaSygnalizatora.Wlaczone)
            End If
        End If

        Select Case Predkosc
            Case PredkoscSygnalizatora.V0
                ZapiszStanSwiatla(stanSwiatel, 2, StanSwiatlaSygnalizatora.Wlaczone)
            Case PredkoscSygnalizatora.V40
                ZapiszStanSwiatla(stanSwiatel, 3, StanSwiatlaSygnalizatora.Wlaczone)
            Case PredkoscSygnalizatora.V60
                ZapiszStanSwiatla(stanSwiatel, 3, StanSwiatlaSygnalizatora.Wlaczone)
                ZapiszStanSwiatla(stanSwiatel, 6, StanSwiatlaSygnalizatora.Wlaczone)
            Case PredkoscSygnalizatora.V100
                ZapiszStanSwiatla(stanSwiatel, 3, StanSwiatlaSygnalizatora.Wlaczone)
                ZapiszStanSwiatla(stanSwiatel, 5, StanSwiatlaSygnalizatora.Wlaczone)
            Case PredkoscSygnalizatora.Manewrowy
                ZapiszStanSwiatla(stanSwiatel, 4, StanSwiatlaSygnalizatora.Wlaczone)
            Case PredkoscSygnalizatora.Zastepczy
                ZapiszStanSwiatla(stanSwiatel, 2, StanSwiatlaSygnalizatora.Wlaczone)
                ZapiszStanSwiatla(stanSwiatel, 4, StanSwiatlaSygnalizatora.Migajace)
        End Select

        Return stanSwiatel
    End Function
End Class
