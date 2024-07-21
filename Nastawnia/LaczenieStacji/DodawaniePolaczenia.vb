Friend Class wndDodawaniePolaczenia
    Friend Property DodawanePolaczenie As Zaleznosci.LaczoneOdcinkiTorow
    Private polaczenia As Zaleznosci.PolaczeniaStacji

    Friend Sub New(polaczenia As Zaleznosci.PolaczeniaStacji)
        InitializeComponent()

        Me.polaczenia = polaczenia

        OdswiezListePosterunkow(cboPosterunek1)
        OdswiezListePosterunkow(cboPosterunek2)
    End Sub

    Private Sub cboPosterunek1_SelectedIndexChanged() Handles cboPosterunek1.SelectedIndexChanged
        Dim post As Zaleznosci.LaczonyPlikStacji = Nothing

        If cboPosterunek1.SelectedItem IsNot Nothing Then
            post = DirectCast(cboPosterunek1.SelectedItem, ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)).Wartosc
        End If

        OdswiezListeTorow(cboTor1, post)
    End Sub

    Private Sub cboPosterunek2_SelectedIndexChanged() Handles cboPosterunek2.SelectedIndexChanged
        Dim post As Zaleznosci.LaczonyPlikStacji = Nothing

        If cboPosterunek2.SelectedItem IsNot Nothing Then
            post = DirectCast(cboPosterunek2.SelectedItem, ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)).Wartosc
        End If

        OdswiezListeTorow(cboTor2, post)
    End Sub

    Private Sub btnDodaj_Click() Handles btnDodaj.Click
        If cboTor1.SelectedItem Is Nothing Then
            Wspolne.PokazBlad("Nie wybrano pierwszego toru.")
            Exit Sub
        End If

        If cboTor2.SelectedItem Is Nothing Then
            Wspolne.PokazBlad("Nie wybrano drugiego toru.")
            Exit Sub
        End If

        DodawanePolaczenie = New Zaleznosci.LaczoneOdcinkiTorow() With {
        .Posterunek1 = DirectCast(cboPosterunek1.SelectedItem, ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)).Wartosc,
        .Posterunek2 = DirectCast(cboPosterunek2.SelectedItem, ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)).Wartosc,
        .Tor1 = DirectCast(cboTor1.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru)).Wartosc,
        .Tor2 = DirectCast(cboTor2.SelectedItem, ObiektComboBox(Of Zaleznosci.OdcinekToru)).Wartosc
        }

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        Close()
    End Sub

    Private Sub OdswiezListePosterunkow(cbo As ComboBox)
        cbo.Items.Clear()

        For Each plik As Zaleznosci.LaczonyPlikStacji In polaczenia.LaczanePliki
            cbo.Items.Add(New ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)(plik, plik.NazwaPosterunku))
        Next
    End Sub

    Private Sub OdswiezListeTorow(cbo As ComboBox, posterunek As Zaleznosci.LaczonyPlikStacji)
        cbo.Items.Clear()
        If posterunek Is Nothing Then Exit Sub

        For Each tor As Zaleznosci.OdcinekToru In posterunek.OdcinkiTorow
            cbo.Items.Add(New ObiektComboBox(Of Zaleznosci.OdcinekToru)(tor, tor.Nazwa))
        Next
    End Sub
End Class