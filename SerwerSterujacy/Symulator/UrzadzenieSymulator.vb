Friend Class UrzadzenieSymulator
    Inherits Zaleznosci.KomunikacjaZUrzadzeniami

    Private okno As wndSymulator

    Public Sub Polacz(okno As wndSymulator)
        Me.okno = okno
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraSamoczynnego(kom As Zaleznosci.UstawStanSygnalizatoraSamoczynnegoUrz)
        UstawStanSygnalizatora(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraManewrowego(kom As Zaleznosci.UstawStanSygnalizatoraManewrowegoUrz)
        UstawStanSygnalizatora(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraPowtarzajacego(kom As Zaleznosci.UstawStanSygnalizatoraPowtarzajacegoUrz)
        UstawStanSygnalizatora(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraPolsamoczynnego(kom As Zaleznosci.UstawStanSygnalizatoraPolsamoczynnegoUrz)
        UstawStanSygnalizatora(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraPrzejazdowego(kom As Zaleznosci.UstawStanSygnalizatoraPrzejazdowegoUrz)
        UstawStanSygnalizatora(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraDrogowego(kom As Zaleznosci.UstawStanSygnalizatoraDrogowegoUrz)
        OdebrUstawionoStanSygnalizatoraDrogowego(Zaleznosci.KomunikatUrzadzenia.UtworzKomunikat(Of Zaleznosci.UstawionoStanSygnalizatoraDrogowegoUrz)(kom))
    End Sub

    Public Overrides Sub UstawJasnoscLampy(kom As Zaleznosci.UstawJasnoscLampyUrz)
        OdebrUstawionoJasnoscLampy(Zaleznosci.KomunikatUrzadzenia.UtworzKomunikat(Of Zaleznosci.UstawionoJasnoscLampyUrz)(kom))
    End Sub

    Public Overrides Sub UstawZwrotnice(kom As Zaleznosci.UstawZwrotniceUrz)
        Dim wynik As Boolean? = okno?.UstawZwrotnice(kom.AdresPosterunku, kom.AdresUrzadzenia, kom.Ustawienie)
        If Not (wynik.HasValue AndAlso wynik.Value) Then
            Dim odp As Zaleznosci.ZmienionoStanZwrotnicyUrz = Zaleznosci.KomunikatUrzadzenia.UtworzKomunikat(Of Zaleznosci.ZmienionoStanZwrotnicyUrz)(kom)
            odp.Stan = If(kom.Ustawienie = Zaleznosci.UstawienieRozjazduEnum.Wprost, Zaleznosci.StanRozjazdu.Wprost, Zaleznosci.StanRozjazdu.Bok)
            OdebrZmienionoStanZwrotnicy(odp)
        End If
    End Sub

    Public Overrides Sub UstawZwrotniceSerwisowo(kom As Zaleznosci.UstawZwrotniceSerwisowoUrz)
    End Sub

    Public Overrides Sub ZamknijRogatke(kom As Zaleznosci.ZamknijRogatkeUrz)
        UstawStanRogatki(kom, Zaleznosci.StanRogatki.Zamknieta)
    End Sub

    Public Overrides Sub OtworzRogatke(kom As Zaleznosci.OtworzRogatkeUrz)
        UstawStanRogatki(kom, Zaleznosci.StanRogatki.Otwarta)
    End Sub

    Friend Sub ZarejestrowanoOs(kom As Zaleznosci.WykrytoOsUrz)
        OdebrWykrytoOs(kom)
    End Sub

    Friend Sub PrzestawionoZwrotnice(kom As Zaleznosci.ZmienionoStanZwrotnicyUrz)
        OdebrZmienionoStanZwrotnicy(kom)
    End Sub

    Private Sub UstawStanSygnalizatora(kom As Zaleznosci.KomunikatUrzadzenia)
        okno?.UstawSygnalizator(kom.AdresPosterunku, kom.AdresUrzadzenia, kom.ZapiszKomunikat())
        OdebrUstawionoStanSygnalizatora(Zaleznosci.KomunikatUrzadzenia.UtworzKomunikat(Of Zaleznosci.UstawionoStanSygnalizatoraUrz)(kom))
    End Sub

    Private Sub UstawStanRogatki(kom As Zaleznosci.KomunikatUrzadzenia, stan As Zaleznosci.StanRogatki)
        Dim odp As Zaleznosci.ZmienionoStanRogatkiUrz = Zaleznosci.KomunikatUrzadzenia.UtworzKomunikat(Of Zaleznosci.ZmienionoStanRogatkiUrz)(kom)
        odp.Stan = stan
        OdebrZmienionoStanRogatki(odp)
    End Sub
End Class