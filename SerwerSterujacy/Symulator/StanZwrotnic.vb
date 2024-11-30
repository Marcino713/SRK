Friend Class wndStanZwrotnic
    Private oknoSymulatora As wndSymulator
    Private pulpit As Zaleznosci.Pulpit
    Private symulator As UrzadzenieSymulator
    Private zwrotniceSlownik As New Dictionary(Of UShort, Zaleznosci.Kostka)

    Friend Sub New(oknoSymulatora As wndSymulator, pulpit As Zaleznosci.Pulpit, symulator As UrzadzenieSymulator)
        InitializeComponent()

        Me.oknoSymulatora = oknoSymulatora
        Me.pulpit = pulpit
        Me.symulator = symulator

        Text = $"{Text} - {pulpit.Nazwa}"
        PokazZwrotnice()
    End Sub

    Friend Function UstawStanZwrotnicy(adres As UShort, stan As Zaleznosci.UstawienieRozjazduEnum) As Boolean
        Return lstZwrotnice.UstawZwrotnice(adres, stan)
    End Function

    Friend Sub ZaznaczZwrotnice(adres As UShort?)
        lstZwrotnice.ZaznaczonaZwrotnica = adres
    End Sub

    Private Sub wndStanZwrotnic_FormClosing() Handles Me.FormClosing
        oknoSymulatora.UsunOknoZwrotnic(pulpit.Adres)
    End Sub

    Private Sub cbAutomatyczne_CheckedChanged() Handles cbAutomatyczne.CheckedChanged
        lstZwrotnice.Automatyczne = cbAutomatyczne.Checked
    End Sub

    Private Sub lstZwrotnice_ZmienionoStanZwrotnicy(adres As UShort, stan As Zaleznosci.StanRozjazdu) Handles lstZwrotnice.ZmienionoStanZwrotnicy
        symulator.PrzestawionoZwrotnice(New Zaleznosci.ZmienionoStanZwrotnicyUrz() With {.AdresPosterunku = pulpit.Adres, .AdresUrzadzenia = adres, .Stan = stan})
    End Sub

    Private Sub lstZwrotnice_ZmienionoZaznaczenieZwrotnicy(adres As UShort?) Handles lstZwrotnice.ZmienionoZaznaczenieZwrotnicy
        oknoSymulatora.ZaznaczonoKostke(pulpit.Adres, If(adres.HasValue, zwrotniceSlownik(adres.Value), Nothing))
    End Sub

    Private Sub PokazZwrotnice()
        Dim zwrotnice As New List(Of Zaleznosci.Rozjazd)

        pulpit.PrzeiterujKostki(Sub(x, y, k)
                                    Dim r As Zaleznosci.Rozjazd = TryCast(k, Zaleznosci.Rozjazd)
                                    If r IsNot Nothing Then
                                        zwrotnice.Add(r)
                                        zwrotniceSlownik.Add(r.Adres, r)
                                    End If
                                End Sub)

        lstZwrotnice.PokazZwrotnice(zwrotnice.OrderBy(Function(z) z.Nazwa).ToArray())
    End Sub
End Class