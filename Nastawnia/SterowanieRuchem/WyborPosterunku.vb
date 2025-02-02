Public Class wndWyborPosterunku
    Private Const STAN_WOLNY As String = "Wolny"
    Private Const STAN_ZAJETY As String = "Zajęty"
    Private Const POLACZENIE_ROZLACZONO As String = "Rozłączono"

    Private _Pulpit As Zaleznosci.Pulpit
    Friend ReadOnly Property Pulpit As Zaleznosci.Pulpit
        Get
            Return _Pulpit
        End Get
    End Property

    Private _PredkoscMaksymalnaSieci As UShort
    Friend ReadOnly Property PredkoscMaksymalnaSieci As UShort
        Get
            Return _PredkoscMaksymalnaSieci
        End Get
    End Property

    Private _TrybObserwatora As Boolean
    Friend ReadOnly Property TrybObserwatora As Boolean
        Get
            Return _TrybObserwatora
        End Get
    End Property

    Private WithEvents Klient As Zaleznosci.KlientTCP
    Private WybranyAdres As UShort
    Private Posterunki As Zaleznosci.DanePosterunku()
    Private Rozlaczanie As Boolean

    Private actOdswiezPosterunki As Action = AddressOf OdswiezPosterunki
    Private actPokazStan As Action(Of Boolean, String) = AddressOf PokazStanPolaczenia
    Private actPokazBlad As Action(Of String) = AddressOf Wspolne.PokazBlad
    Private actZamknijOkno As Action = Sub() Close()
    Private actWyczyscPosterunki As Action = AddressOf WyczyscListePosterunkow
    Private actPokazZajetoscPosterunku As Action = AddressOf PokazZajetoscPosterunku

    Friend Sub New(obiektKlienta As Zaleznosci.KlientTCP)
        InitializeComponent()
        lblStanLaczenia.Text = POLACZENIE_ROZLACZONO
        Klient = obiektKlienta
    End Sub

    Private Sub wndWyborPosterunku_FormClosing() Handles Me.FormClosing
        Dim k As Zaleznosci.KlientTCP = Klient
        Klient = Nothing
        If _Pulpit Is Nothing Then k.Zakoncz(False)
    End Sub

    Private Sub cbTrybObserwatora_CheckedChanged() Handles cbTrybObserwatora.CheckedChanged
        _TrybObserwatora = cbTrybObserwatora.Checked
    End Sub

    Private Sub btnPolacz_Click() Handles btnPolacz.Click
        Dim port As UShort

        If txtAdres.Text = "" Then
            Wspolne.PokazBlad("Należy podać adres serwera.")
            Exit Sub
        End If

        If Not UShort.TryParse(txtPort.Text, port) Then
            Wspolne.PokazBlad("Wartość pola Port musi być liczbą całkowitą dodatnią.")
            Exit Sub
        End If

        If txtHaslo.Text = "" Then
            Wspolne.PokazBlad("Należy podać hasło.")
            Exit Sub
        End If

        actWyczyscPosterunki()
        actPokazStan(False, "Łączenie...")
        Rozlaczanie = False
        Dim dane As New Zaleznosci.DanePolaczeniaKlienta With {
            .AdresIp = txtAdres.Text,
            .Port = port,
            .Haslo = txtHaslo.Text,
            .Obserwator = _TrybObserwatora}
        Klient.Polacz(dane)
    End Sub

    Private Sub btnRozlacz_Click() Handles btnRozlacz.Click
        Rozlaczanie = True
        Klient.Zakoncz(False)
        Invoke(actWyczyscPosterunki)
        Invoke(actPokazStan, True, POLACZENIE_ROZLACZONO)
    End Sub

    Private Sub lvPosterunki_SelectedIndexChanged() Handles lvPosterunki.SelectedIndexChanged
        btnWybierz.Enabled = lvPosterunki.SelectedItems IsNot Nothing AndAlso lvPosterunki.SelectedItems.Count > 0
    End Sub

    Private Sub lvPosterunki_DoubleClick() Handles lvPosterunki.DoubleClick
        WybierzPosterunek()
    End Sub

    Private Sub btnWybierz_Click() Handles btnWybierz.Click
        WybierzPosterunek()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        actZamknijOkno()
    End Sub

    Private Sub Klient_BladPolaczenia() Handles Klient.BladNawiazywaniaPolaczenia
        Invoke(actPokazStan, True, "Błąd łączenia z serwerem")
    End Sub

    Private Sub Klient_OdebranoNieuwierzytelniono(kom As Zaleznosci.Nieuwierzytelniono) Handles Klient.OdebranoNieuwierzytelniono
        Dim przyczyna As String

        Select Case kom.Przyczyna
            Case Zaleznosci.PrzyczynaNieuwierzytelnienia.BledneHaslo
                przyczyna = "Błędne hasło"
            Case Zaleznosci.PrzyczynaNieuwierzytelnienia.BrakTrybuObserwatora
                przyczyna = "Nieobsługiwany tryb obserwatora"
            Case Else
                przyczyna = "Błąd uwierzytelniania"
        End Select

        Invoke(actPokazStan, True, przyczyna)
    End Sub

    Private Sub Klient_OdebranoUwierzytelnionoPoprawnie(kom As Zaleznosci.UwierzytelnionoPoprawnie) Handles Klient.OdebranoUwierzytelnionoPoprawnie
        _PredkoscMaksymalnaSieci = kom.PredkoscMaksymalna
        Posterunki = kom.Posterunki

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
                _Pulpit = p
                Invoke(actZamknijOkno)
            End If

        ElseIf kom.Stan = Zaleznosci.StanUstawianegoPosterunku.PosterunekZajety Then
            Invoke(actPokazZajetoscPosterunku)
            Invoke(actPokazBlad, "Posterunek jest już zajęty.")
        End If
    End Sub

    Private Sub Klient_ZakonczonoPolaczenie() Handles Klient.ZakonczonoPolaczenie
        If Not Rozlaczanie Then Invoke(actPokazBlad, "Połączenie z serwerem zostało zakończone.")
        Invoke(actWyczyscPosterunki)
        Invoke(actPokazStan, True, POLACZENIE_ROZLACZONO)
    End Sub

    Private Sub Klient_OdebranoZakonczonoDzialanieSerwera(kom As Zaleznosci.ZakonczonoDzialanieSerwera) Handles Klient.OdebranoZakonczonoDzialanieSerwera
        Invoke(actPokazBlad, "Serwer został zatrzymany.")
        Invoke(actZamknijOkno)
    End Sub

    Private Sub Klient_OdebranoZakonczonoSesjeKlienta(kom As Zaleznosci.ZakonczonoSesjeKlienta) Handles Klient.OdebranoZakonczonoSesjeKlienta
        If Not Rozlaczanie Then Invoke(actPokazBlad, PrzyczynaZakonczeniaSesjiKlientaToString(kom.Przyczyna))
        Invoke(actZamknijOkno)
    End Sub

    Private Sub WybierzPosterunek()
        If lvPosterunki.SelectedItems Is Nothing OrElse lvPosterunki.SelectedItems.Count = 0 Then Exit Sub

        Dim dane As Zaleznosci.DanePosterunku = CType(lvPosterunki.SelectedItems(0).Tag, Zaleznosci.DanePosterunku)
        If (Not _TrybObserwatora) AndAlso dane.Stan = Zaleznosci.StanPosterunku.Zajety Then
            Wspolne.PokazBlad("Posterunek jest już zajęty.")
            Exit Sub
        End If

        Dim kom As New Zaleznosci.WybierzPosterunek
        If rbTrybPolsamoczynny.Checked Then kom.Tryb = Zaleznosci.TrybPracyPosterunku.Polsamoczynny Else kom.Tryb = Zaleznosci.TrybPracyPosterunku.Samoczynny
        kom.Adres = dane.Adres
        WybranyAdres = dane.Adres
        Klient.WyslijWybierzPosterunek(kom)
    End Sub

    Private Sub OdswiezPosterunki()
        lvPosterunki.Items.Clear()
        btnWybierz.Enabled = False
        If Posterunki Is Nothing Then Exit Sub
        Dim postEn As IEnumerable(Of Zaleznosci.DanePosterunku) = Posterunki.OrderBy(Function(p) p.Adres)

        For Each p As Zaleznosci.DanePosterunku In postEn
            Dim lvi As New ListViewItem(New String() {p.Adres.ToString, p.Nazwa, StanPosterunkuToString(p.Stan)}) With {
                .Tag = p
            }
            lvPosterunki.Items.Add(lvi)
        Next
    End Sub

    Private Sub PokazStanPolaczenia(dostepnosc As Boolean, opis As String)
        txtAdres.Enabled = dostepnosc
        txtPort.Enabled = dostepnosc
        txtHaslo.Enabled = dostepnosc
        btnPolacz.Enabled = dostepnosc
        btnRozlacz.Enabled = Not dostepnosc
        cbTrybObserwatora.Enabled = dostepnosc
        lblStanLaczenia.Text = opis
    End Sub

    Private Sub WyczyscListePosterunkow()
        Posterunki = Nothing
        OdswiezPosterunki()
    End Sub

    Private Sub PokazZajetoscPosterunku()
        If lvPosterunki.Items Is Nothing Then Exit Sub

        For i As Integer = 0 To lvPosterunki.Items.Count - 1
            Dim lvi As ListViewItem = lvPosterunki.Items(i)
            Dim dp As Zaleznosci.DanePosterunku = CType(lvi.Tag, Zaleznosci.DanePosterunku)

            If dp.Adres = WybranyAdres Then
                lvi.SubItems(2).Text = STAN_ZAJETY
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