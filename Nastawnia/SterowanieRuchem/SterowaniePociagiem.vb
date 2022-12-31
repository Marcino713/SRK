Friend Class wndSterowaniePociagiem
    Private WithEvents Klient As Zaleznosci.KlientTCP
    Private OknoNastawni As wndNastawnia
    Private NrPociagu As UInteger
    Private NazwaPociagu As String
    Private MnoznikPredkosci As Integer = 1
    Private ZdarzeniaWlaczone As Boolean = False
    Private PytaniePrzyZamykaniu As Boolean = True

    Private actPokazKomunikat As Action(Of String) = AddressOf PokazKomunikat
    Private actPokazBlad As Action(Of String) = AddressOf PokazBlad
    Private actUstawNazwePociagu As Action(Of String) = AddressOf UstawNazwePociagu
    Private actPokazPredkoscDozwolona As Action(Of UShort) = AddressOf PokazPredkoscDozwolona
    Private actPokazPredkoscBiezaca As Action(Of Short) = AddressOf PokazPredkoscBiezaca
    Private actZamknij As Action = Sub() Close()

    Friend Sub New(klient As Zaleznosci.KlientTCP, oknoNastawni As wndNastawnia, nrPociagu As UInteger, nazwaPociagu As String, predkoscMaksymalna As UShort)
        InitializeComponent()

        Me.Klient = klient
        Me.OknoNastawni = oknoNastawni
        Me.NrPociagu = nrPociagu
        lblNumer.Text = nrPociagu.ToString
        actUstawNazwePociagu(nazwaPociagu)
        prPredkosc.PredkoscMaksymalna = predkoscMaksymalna
        ZdarzeniaWlaczone = True
    End Sub

    Friend Sub Zamknij()
        PytaniePrzyZamykaniu = False
        Close()
    End Sub

    Private Sub wndSterowaniePociagiem_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If PytaniePrzyZamykaniu AndAlso Not WysiadzZPociagu() Then
            e.Cancel = True
        Else
            Klient = Nothing
            OknoNastawni.UsunOknoSterowaniaPociagiem(Me)
        End If
    End Sub

    Private Sub prPredkosc_ZmienionoPredkoscBiezaca(predkosc As UShort) Handles prPredkosc.ZmienionoPredkoscBiezaca
        lblPredkosc.Text = predkosc.ToString
        PokazDostepnoscKontrolek(predkosc = 0)

        If ZdarzeniaWlaczone Then
            Klient.WyslijUstawPredkoscPociagu(New Zaleznosci.UstawPredkoscPociagu() With {.NrPociagu = NrPociagu, .Predkosc = CShort(predkosc * MnoznikPredkosci)})
        End If
    End Sub

    Private Sub cbTyl_CheckedChanged() Handles cbTyl.CheckedChanged
        MnoznikPredkosci = If(cbTyl.Checked, -1, 1)
    End Sub

    Private Sub btnWysiadz_Click() Handles btnWysiadz.Click
        WysiadzZPociagu()
    End Sub

    Private Sub btnUsun_Click() Handles btnUsun.Click
        If ZadajPytanie($"Czy chcesz usunąć pociąg {PobierzOznaczeniePociagu(NrPociagu, NazwaPociagu)}?") = DialogResult.Yes Then
            prPredkosc.Enabled = False
            PokazDostepnoscKontrolek(False)
            Klient.WyslijUsunPociag(New Zaleznosci.UsunPociag() With {.NrPociagu = NrPociagu})
        End If
    End Sub

    Private Sub Klient_OdebranoWysiadznietoZPociagu(kom As Zaleznosci.WysiadznietoZPociagu) Handles Klient.OdebranoWysiadznietoZPociagu
        If NrPociagu = kom.NrPociagu Then
            PytaniePrzyZamykaniu = False
            Invoke(actPokazKomunikat, $"Maszynista wysiadł z pociągu {PobierzOznaczeniePociagu(NrPociagu, NazwaPociagu)}.")
            Invoke(actZamknij)
        End If
    End Sub

    Private Sub Klient_OdebranoUsunietoPociag(kom As Zaleznosci.UsunietoPociag) Handles Klient.OdebranoUsunietoPociag
        If NrPociagu = kom.NrPociagu Then
            PytaniePrzyZamykaniu = False
            Invoke(actPokazKomunikat, $"Pociąg {PobierzOznaczeniePociagu(NrPociagu, NazwaPociagu)} został usunięty.")
            Invoke(actZamknij)
        End If
    End Sub

    Private Sub Klient_OdebranoZmienionoNazwePociagu(kom As Zaleznosci.ZmienionoNazwePociagu) Handles Klient.OdebranoZmienionoNazwePociagu
        If NrPociagu = kom.NrPociagu Then
            Invoke(actUstawNazwePociagu, kom.Nazwa)
        End If
    End Sub

    Private Sub Klient_OdebranoZmienionoPredkoscDozwolona(kom As Zaleznosci.ZmienionoPredkoscDozwolona) Handles Klient.OdebranoZmienionoPredkoscDozwolona
        If NrPociagu = kom.NrPociagu Then
            Invoke(actPokazPredkoscDozwolona, kom.Predkosc)
        End If
    End Sub

    Private Sub Klient_OdebranoZmienionoPredkoscPociagu(kom As Zaleznosci.ZmienionoPredkoscPociagu) Handles Klient.OdebranoZmienionoPredkoscPociagu
        If NrPociagu = kom.NrPociagu Then
            If kom.Zrodlo = Zaleznosci.ZrodloZmianyPredkosci.Program And kom.Stan <> Zaleznosci.StanZmianyPredkosci.Zmieniono Then
                Dim blad As String = Nothing

                If kom.Stan = Zaleznosci.StanZmianyPredkosci.BlednyNumer Then
                    blad = $"Pociąg o numerze {kom.NrPociagu} nie istnieje."
                ElseIf kom.Stan = Zaleznosci.StanZmianyPredkosci.PociagNiesterowanyPrzezPosterunek Then
                    blad = $"Pociąg {kom.NrPociagu} nie może być sterowany przez bieżący posterunek."
                End If

                If blad IsNot Nothing Then
                    PytaniePrzyZamykaniu = False
                    Invoke(actPokazBlad, blad)
                    Invoke(actZamknij)
                End If

            ElseIf kom.Zrodlo <> Zaleznosci.ZrodloZmianyPredkosci.Program Then
                Invoke(actPokazPredkoscBiezaca, kom.Predkosc)
            End If
        End If
    End Sub

    Private Function WysiadzZPociagu() As Boolean
        If ZadajPytanie($"Czy chcesz zakończyć prowadzenie pociągu {PobierzOznaczeniePociagu(NrPociagu, NazwaPociagu)}?") = DialogResult.Yes Then
            prPredkosc.Enabled = False
            PokazDostepnoscKontrolek(False)
            Klient.WyslijWysiadzZPociagu(New Zaleznosci.WysiadzZPociagu() With {.NrPociagu = NrPociagu})
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PokazPredkoscDozwolona(predkosc As UShort)
        ZdarzeniaWlaczone = False
        prPredkosc.PredkoscDozwolona = predkosc
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub PokazPredkoscBiezaca(predkosc As Short)
        cbTyl.Checked = predkosc < 0
        ZdarzeniaWlaczone = False
        prPredkosc.PredkoscBiezaca = CUShort(Math.Abs(predkosc))
        ZdarzeniaWlaczone = True
    End Sub

    Private Sub UstawNazwePociagu(nazwa As String)
        lblNazwa.Text = nazwa
        NazwaPociagu = nazwa
    End Sub

    Private Sub PokazDostepnoscKontrolek(dostepne As Boolean)
        cbTyl.Enabled = dostepne
        btnWysiadz.Enabled = dostepne
        btnUsun.Enabled = dostepne
    End Sub
End Class