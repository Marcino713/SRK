Friend Class wndStanPulpitu
    Private oknoSymulatora As wndSymulator
    Private symulator As UrzadzenieSymulator
    Private WithEvents danePulpitu As Zaleznosci.Pulpit

    Friend Sub New(oknoSymulatora As wndSymulator, danePulpitu As Zaleznosci.Pulpit, symulator As UrzadzenieSymulator, rysownik As Pulpit.TypRysownika)
        InitializeComponent()

        Me.oknoSymulatora = oknoSymulatora
        Me.danePulpitu = danePulpitu
        Me.symulator = symulator

        plpPulpit.TypRysownika = rysownik
        plpPulpit.Pulpit = danePulpitu
        plpPulpit.WarunekZaznaczeniaKostki = AddressOf CzyKostkaZaznaczalna
        plpPulpit.Wysrodkuj()

        Text = $"{Text} - {danePulpitu.Nazwa}"
    End Sub

    Friend Sub ZaznaczKostke(kostka As Zaleznosci.Kostka)
        plpPulpit.ZaznaczonaKostka = kostka
    End Sub

    Private Sub wndPulpit_FormClosing() Handles Me.FormClosing
        oknoSymulatora.UsunOknoPulpitu(danePulpitu.Adres)
        danePulpitu = Nothing
    End Sub

    Private Sub plpPulpit_ZarejestrowanoOs(adres As UShort) Handles plpPulpit.ZarejestrowanoOs
        symulator.ZarejestrowanoOs(New Zaleznosci.WykrytoOsUrz() With {.AdresPosterunku = danePulpitu.Adres, .AdresUrzadzenia = adres})
    End Sub

    Private Sub plpPulpit_ZmianaZaznaczeniaKostki(kostka As Zaleznosci.Kostka) Handles plpPulpit.ZmianaZaznaczeniaKostki
        oknoSymulatora.ZaznaczonoKostke(danePulpitu.Adres, kostka)
    End Sub

    Private Function CzyKostkaZaznaczalna(k As Zaleznosci.Kostka) As Boolean
        Return k Is Nothing OrElse TypeOf k Is Zaleznosci.Sygnalizator OrElse TypeOf k Is Zaleznosci.Rozjazd
    End Function
End Class