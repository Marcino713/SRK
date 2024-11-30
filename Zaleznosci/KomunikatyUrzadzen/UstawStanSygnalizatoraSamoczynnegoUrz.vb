Public Class UstawStanSygnalizatoraSamoczynnegoUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAW_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Property WolneOdstepy As Integer
    Public Property Stawnosc As StawnoscSBL

    Public Overrides Function ZapiszKomunikat() As UShort
        Dim stanSwiatel As UShort

        If Stawnosc = StawnoscSBL.Dwustawna Then

            If WolneOdstepy = 0 Then
                ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Wlaczone)
            Else
                ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Wlaczone)
            End If

        Else

            If WolneOdstepy = 0 Then
                ZapiszStanSwiatla(stanSwiatel, 1, StanSwiatlaSygnalizatora.Wlaczone)
            ElseIf WolneOdstepy = 1 Then
                ZapiszStanSwiatla(stanSwiatel, 2, StanSwiatlaSygnalizatora.Wlaczone)
            ElseIf WolneOdstepy = 2 And Stawnosc = StawnoscSBL.Czterostawna Then
                ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Migajace)
            Else
                ZapiszStanSwiatla(stanSwiatel, 0, StanSwiatlaSygnalizatora.Wlaczone)
            End If

        End If

        Return stanSwiatel
    End Function
End Class