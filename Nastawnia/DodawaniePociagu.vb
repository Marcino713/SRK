Friend Class wndDodawaniePociagu
    Private Klient As Zaleznosci.KlientTCP
    Private Pulpit As PulpitSterowniczy

    Public Sub New(klient As Zaleznosci.KlientTCP, pulpit As PulpitSterowniczy)
        InitializeComponent()

        Me.Klient = klient
        Me.Pulpit = pulpit
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        Dim nrPoc As UInteger
        Dim osie As UShort
        Dim zazn As Zaleznosci.Kostka = Pulpit.ZaznaczonaKostka

        If Not UInteger.TryParse(txtNrPociagu.Text, nrPoc) Then
            PokazBlad("W polu Numer pociągu należy podać liczbę całkowitą dodatnią.")
            Exit Sub
        End If

        If Not UShort.TryParse(txtLiczbaOsi.Text, osie) Then
            PokazBlad("W polu Liczba osi należy podać liczbę całkowitą dodatnią.")
            Exit Sub
        End If

        If zazn Is Nothing OrElse Not Zaleznosci.CzyTorBezRozjazdu(zazn.Typ) Then
            PokazBlad("Należy na schemacie zaznaczyć kostkę, na której znajduje się pociąg.")
            Exit Sub
        End If

        Dim p As Zaleznosci.Punkt = Pulpit.Pulpit.ZnajdzKostke(zazn)
        Klient.WyslijUstawPoczatkowaZajetoscToru(New Zaleznosci.UstawPoczatkowaZajetoscToru() With
                {.NrPociagu = nrPoc, .LiczbaOsi = osie, .PojazdSterowalny = cboSterowalny.Checked, .WspolrzedneKostki = p})
        Close()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        Close()
    End Sub
End Class