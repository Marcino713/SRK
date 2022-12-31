Friend Class wndDodawaniePociagu
    Private Const STAN_DODAWANIE As String = "Dodawanie..."

    Private WithEvents Klient As Zaleznosci.KlientTCP
    Private Pulpit As PulpitSterowniczy
    Private Numer As UInteger

    Private actPokazStan As Action(Of String) = Sub(s) lblDodawanie.Text = s
    Private actPokazDostepnoscKontrolek As Action(Of Boolean) = AddressOf PokazDostepnoscKontrolek
    Private actPokazBlad As Action(Of String) = AddressOf PokazBlad
    Private actPokazKomunikat As Action(Of String) = AddressOf PokazKomunikat
    Private actZamknij As Action = Sub() Close()

    Public Sub New(klient As Zaleznosci.KlientTCP, pulpit As PulpitSterowniczy)
        InitializeComponent()

        Me.Klient = klient
        Me.Pulpit = pulpit
    End Sub

    Private Sub wndDodawaniePociagu_FormClosing() Handles Me.FormClosing
        Klient = Nothing
    End Sub

    Private Sub btnDodaj_Click() Handles btnDodaj.Click
        Dim nrPoc As UInteger
        Dim osie As UShort
        Dim zazn As Zaleznosci.Kostka = Pulpit.ZaznaczonaKostka

        If Not UInteger.TryParse(txtNrPociagu.Text, nrPoc) Then
            actPokazBlad("W polu Numer pociągu należy podać liczbę całkowitą dodatnią.")
            Exit Sub
        End If

        If Not UShort.TryParse(txtLiczbaOsi.Text, osie) Then
            actPokazBlad("W polu Liczba osi należy podać liczbę całkowitą dodatnią.")
            Exit Sub
        End If

        If zazn Is Nothing OrElse Not Zaleznosci.Kostka.CzyTorBezRozjazdu(zazn.Typ) Then
            actPokazBlad("Należy na schemacie zaznaczyć kostkę, na której znajduje się pociąg.")
            Exit Sub
        End If

        Numer = nrPoc
        actPokazStan(STAN_DODAWANIE)
        actPokazDostepnoscKontrolek(False)
        Dim p As Zaleznosci.Punkt = Pulpit.Pulpit.ZnajdzKostke(zazn)
        Klient.WyslijDodajPociag(New Zaleznosci.DodajPociag() With
                {.NrPociagu = nrPoc, .Nazwa = txtNazwa.Text, .LiczbaOsi = osie, .PojazdSterowalny = cboSterowalny.Checked, .WspolrzedneKostki = p})
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        actZamknij()
    End Sub

    Private Sub Klient_OdebranoDodanoPociag(kom As Zaleznosci.DodanoPociag) Handles Klient.OdebranoDodanoPociag
        If kom.NrPociagu = Numer Then
            Numer = 0
            Invoke(actPokazStan, "")
            Invoke(actPokazDostepnoscKontrolek, True)

            Select Case kom.Stan
                Case Zaleznosci.StanNadaniaNumeruPociagu.Dobrze
                    Invoke(actPokazKomunikat, "Pociąg został dodany.")
                    Invoke(actZamknij)
                Case Zaleznosci.StanNadaniaNumeruPociagu.NrZajety
                    Invoke(actPokazBlad, "Pociąg o podanym numerze już istnieje.")
                Case Zaleznosci.StanNadaniaNumeruPociagu.BledneWspolrzedne
                    Invoke(actPokazBlad, "Pociąg nie mógł zostać dodany na wskazanej kostce.")
            End Select
        End If
    End Sub

    Private Sub PokazDostepnoscKontrolek(dostepne As Boolean)
        btnDodaj.Enabled = dostepne
    End Sub
End Class