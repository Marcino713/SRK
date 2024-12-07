Friend Class wndObserwatorzy
    Private adres As UShort
    Private WithEvents serwer As Zaleznosci.SerwerTCP
    Private actPokazObserwatorow As Action = AddressOf PokazObserwatorow

    Friend Sub New(nazwaPosterunku As String, adres As UShort, serwer As Zaleznosci.SerwerTCP)
        InitializeComponent()

        Me.adres = adres
        Me.serwer = serwer

        Text = $"{Text} - {nazwaPosterunku}"
        lblTytul.Text &= nazwaPosterunku

        PokazObserwatorow()
    End Sub

    Private Sub wndObserwatorzy_FormClosing() Handles Me.FormClosing
        serwer = Nothing
    End Sub

    Private Sub lvObserwatorzy_SelectedIndexChanged() Handles lvObserwatorzy.SelectedIndexChanged
        PokazDostepnoscKontrolek()
    End Sub

    Private Sub btnOdswiez_Click() Handles btnOdswiez.Click
        PokazObserwatorow()
    End Sub

    Private Sub btnWyrzuc_Click() Handles btnWyrzuc.Click
        Dim lvi As ListViewItem = Wspolne.PobierzZaznaczonyElementNaLiscie(lvObserwatorzy)
        If lvi Is Nothing Then Exit Sub

        Dim ip As String = lvi.Text
        If Wspolne.ZadajPytanie($"Czy wyrzucić obserwatora o adresie IP {ip}?") = DialogResult.No Then
            Exit Sub
        End If

        serwer.ZakonczPolaczenieObserwatora(adres, ip)
    End Sub

    'Private Sub btnWyrzucZablokuj_Click() Handles btnWyrzucZablokuj.Click
    'End Sub

    Private Sub serwer_ZmienionoLiczbeObserwatorow(post As String, liczba As Integer) Handles serwer.ZmienionoLiczbeObserwatorow
        If UShort.Parse(post) = adres Then Invoke(actPokazObserwatorow)
    End Sub

    Private Sub PokazObserwatorow()
        Dim obserwatorzy As Zaleznosci.ObserwatorPosterunku() = serwer.PobierzObserwatorow(adres)
        Dim ip As String = String.Empty
        Dim zazn As ListViewItem = Wspolne.PobierzZaznaczonyElementNaLiscie(lvObserwatorzy)

        If zazn IsNot Nothing Then ip = zazn.Text
        lvObserwatorzy.Items.Clear()

        For i As Integer = 0 To obserwatorzy.Length - 1
            Dim obs As Zaleznosci.ObserwatorPosterunku = obserwatorzy(i)
            Dim lvi As New ListViewItem

            lvi.Text = obs.AdresIP
            lvi.SubItems.Add(obs.CzasPodlaczenia)
            If obs.AdresIP = ip Then lvi.Selected = True
            lvObserwatorzy.Items.Add(lvi)
        Next

        PokazDostepnoscKontrolek()
    End Sub

    Private Sub PokazDostepnoscKontrolek()
        Dim dostepne As Boolean = lvObserwatorzy.SelectedItems IsNot Nothing AndAlso lvObserwatorzy.SelectedItems.Count > 0

        btnWyrzuc.Enabled = dostepne
        'btnWyrzucZablokuj.Enabled = dostepne
    End Sub
End Class