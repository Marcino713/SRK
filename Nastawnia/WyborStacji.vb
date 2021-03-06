Public Class wndWyborStacji
    Private Const STAN_WOLNY As String = "Wolny"
    Private Const STAN_ZAJETY As String = "Zajęty"

    Friend Pulpit As Zaleznosci.Pulpit

    Private WithEvents Klient As Zaleznosci.KlientTCP
    Private WybranyAdres As UShort
    Private post As Zaleznosci.DanePosterunku()
    Private actOdswiezPosterunki As Action = AddressOf OdswiezPosterunki
    Private actPokazStan As Action(Of Boolean, String) = AddressOf PokazStanPolaczenia
    Private actPokazBlad As Action(Of String) = AddressOf PokazBlad
    Private actZamknijOkno As Action = Sub() Close()
    Private actWyczyscPosterunki As Action = AddressOf WyczyscListePosterunkow
    Private actPokazZajetoscPosterunku As Action = AddressOf PokazZajetoscPosterunku

    Friend Sub New(obiektKlienta As Zaleznosci.KlientTCP)
        InitializeComponent()
        Klient = obiektKlienta
    End Sub

    Private Sub wndWyborStacji_FormClosing() Handles Me.FormClosing
        Dim k As Zaleznosci.KlientTCP = Klient
        Klient = Nothing
        If Pulpit Is Nothing Then k.Zakoncz(False)
    End Sub

    Private Sub btnPolacz_Click() Handles btnPolacz.Click
        Dim port As UShort

        If txtAdres.Text = "" Then
            PokazBlad("Należy podać adres serwera.")
            Exit Sub
        End If

        If Not UShort.TryParse(txtPort.Text, port) Then
            PokazBlad("Wartość pola Port musi być liczbą całkowitą dodatnią.")
            Exit Sub
        End If

        If txtHaslo.Text = "" Then
            PokazBlad("Należy podać hasło.")
            Exit Sub
        End If

        actWyczyscPosterunki()
        actPokazStan(False, "Łączenie...")
        Klient.Polacz(txtAdres.Text, port, txtHaslo.Text)
    End Sub

    Private Sub lvPosterunki_SelectedIndexChanged() Handles lvPosterunki.SelectedIndexChanged
        btnWybierz.Enabled = lvPosterunki.SelectedItems IsNot Nothing AndAlso lvPosterunki.SelectedItems.Count > 0
    End Sub

    Private Sub btnWybierz_Click() Handles btnWybierz.Click
        If lvPosterunki.SelectedItems Is Nothing OrElse lvPosterunki.SelectedItems.Count = 0 Then Exit Sub

        Dim dane As Zaleznosci.DanePosterunku = CType(lvPosterunki.SelectedItems(0).Tag, Zaleznosci.DanePosterunku)
        If dane.Stan = Zaleznosci.StanPosterunku.Zajety Then
            PokazBlad("Posterunek jest już zajęty.")
            Exit Sub
        End If

        Dim kom As New Zaleznosci.WybierzPosterunek
        If rbTrybPolsamoczynny.Checked Then kom.Tryb = Zaleznosci.TrybPracyPosterunku.Polsamoczynny Else kom.Tryb = Zaleznosci.TrybPracyPosterunku.Samoczynny
        kom.Adres = dane.Adres
        WybranyAdres = dane.Adres
        Klient.WyslijWybierzPosterunek(kom)
    End Sub

    Private Sub Klient_BladPolaczenia() Handles Klient.BladNawiazywaniaPolaczenia
        Invoke(actPokazStan, True, "Błąd łączenia z serwerem")
    End Sub

    Private Sub Klient_OdebranoNieuwierzytelniono(kom As Zaleznosci.Nieuwierzytelniono) Handles Klient.OdebranoNieuwierzytelniono
        Invoke(actPokazStan, True, "Błędne hasło")
    End Sub

    Private Sub Klient_OdebranoUwierzytelnionoPoprawnie(kom As Zaleznosci.UwierzytelnionoPoprawnie) Handles Klient.OdebranoUwierzytelnionoPoprawnie
        post = kom.Posterunki

        Invoke(actPokazStan, False, "Połączono")
        Invoke(actOdswiezPosterunki)
    End Sub

    Private Sub Klient_OdebranoWybranoPosterunek(kom As Zaleznosci.WybranoPosterunek) Handles Klient.OdebranoWybranoPosterunek
        If kom.Stan = Zaleznosci.StanUstawianegoPosterunku.WybranoPoprawnie Then
            Dim p As Zaleznosci.Pulpit = Zaleznosci.Pulpit.Otworz(kom.ZawartoscPliku)
            If p Is Nothing Then
                Invoke(actPokazBlad, "Nie udało się otworzyć pliku posterunku ruchu.")
                Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {
                                                     .Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.BladOtwarciaPlikuPosterunku}
                                                     )
            Else
                Pulpit = p
                Invoke(actZamknijOkno)
            End If
        ElseIf kom.Stan = Zaleznosci.StanUstawianegoPosterunku.PosterunekZajety
            Invoke(actPokazZajetoscPosterunku)
            Invoke(actPokazBlad, "Posterunek jest już zajęty.")
        End If
    End Sub

    Private Sub Klient_ZakonczonoPolaczenie() Handles Klient.ZakonczonoPolaczenie
        Invoke(actPokazBlad, "Połączenie z serwerem zostało zakończone.")
        Invoke(actWyczyscPosterunki)
        Invoke(actPokazStan, True, "")
    End Sub

    Private Sub Klient_OdebranoZakonczonoDzialanieSerwera(kom As Zaleznosci.ZakonczonoDzialanieSerwera) Handles Klient.OdebranoZakonczonoDzialanieSerwera
        Invoke(actPokazBlad, "Serwer został zatrzymany.")
        Invoke(actZamknijOkno)
    End Sub

    Private Sub Klient_OdebranoZakonczonoSesjeKlienta(kom As Zaleznosci.ZakonczonoSesjeKlienta) Handles Klient.OdebranoZakonczonoSesjeKlienta
        Invoke(actPokazBlad, PrzyczynaZakonczeniaSesjiKlientaToString(kom.Przyczyna))
        Invoke(actZamknijOkno)
    End Sub

    Private Sub OdswiezPosterunki()
        lvPosterunki.Items.Clear()
        btnWybierz.Enabled = False
        If post Is Nothing Then Exit Sub

        For i As Integer = 0 To post.Length - 1
            Dim lvi As New ListViewItem(New String() {post(i).Adres.ToString, post(i).Nazwa, StanPosterunkuToString(post(i).Stan)})
            lvi.Tag = post(i)
            lvPosterunki.Items.Add(lvi)
        Next
    End Sub

    Private Sub PokazStanPolaczenia(dostepnoscPrzycisku As Boolean, opis As String)
        txtAdres.Enabled = dostepnoscPrzycisku
        txtPort.Enabled = dostepnoscPrzycisku
        txtHaslo.Enabled = dostepnoscPrzycisku
        btnPolacz.Enabled = dostepnoscPrzycisku
        lblStanLaczenia.Text = opis
    End Sub

    Private Sub WyczyscListePosterunkow()
        post = Nothing
        OdswiezPosterunki()
    End Sub

    Private Sub PokazZajetoscPosterunku()
        If lvPosterunki.Items Is Nothing Then Exit Sub

        For i As Integer = 0 To lvPosterunki.Items.Count - 1
            Dim dp As Zaleznosci.DanePosterunku = CType(lvPosterunki.Items(i).Tag, Zaleznosci.DanePosterunku)
            If dp.Adres = WybranyAdres Then
                lvPosterunki.Items(i).SubItems(2).Text = STAN_ZAJETY
                Exit Sub
            End If
        Next
    End Sub

    Private Function StanPosterunkuToString(stan As Zaleznosci.StanPosterunku) As String
        Select Case stan
            Case Zaleznosci.StanPosterunku.Wolny
                Return STAN_WOLNY
            Case Zaleznosci.StanPosterunku.Zajety
                Return STAN_ZAJETY
        End Select

        Return ""
    End Function
End Class