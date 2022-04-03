Public Class wndOknoSerwera
    Private Const FILTR_PLIKU As String = Zaleznosci.PolaczeniaStacji.OPIS_PLIKU & "|*" & Zaleznosci.PolaczeniaStacji.ROZSZERZENIE_PLIKU

    Private WithEvents Serwer As New Zaleznosci.SerwerTCP
    Private actZmianaCzasuPodlaczenia As Action(Of String, String) = AddressOf PokazZmianeCzasuPodlaczenia
    Private slockListaPosterunkow As New Object

    Public Sub New()
        InitializeComponent()
        PokazZatrzymanie()
    End Sub

    Private Sub wndOknoGlowne_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Serwer.Uruchomiony Then
            If Pytanie("Czy zamknąć okno? Spowoduje to zatrzymanie serwera.") = DialogResult.Yes Then
                Serwer.Zatrzymaj()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnPrzegladaj_Click() Handles btnPrzegladaj.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = FILTR_PLIKU

        If dlg.ShowDialog = DialogResult.OK Then
            txtSciezka.Text = dlg.FileName
        End If
    End Sub

    Private Sub btnWczytaj_Click() Handles btnWczytaj.Click
        If Serwer.Uruchomiony Then
            Blad("Przed wczytaniem pliku połączeń należy zatrzymać serwer.")
            Exit Sub
        End If

        If txtSciezka.Text = "" Then
            Blad("Należy podać ścieżkę pliku połączeń.")
            Exit Sub
        End If

        Dim wynik As Boolean = Serwer.WczytajPolaczenie(txtSciezka.Text)
        lblStanWczytania.Text = If(wynik, "Wczytano poprawnie", "Błąd wczytywania połączeń")
    End Sub

    Private Sub btnStart_Click() Handles btnStart.Click
        Dim port As UShort

        If Not UShort.TryParse(txtPort.Text, port) Then
            Blad("W polu Port należy podać liczbą całkowitą dodatnią.")
            Exit Sub
        End If

        If txtHaslo.Text = "" Then
            Blad("Należy podać hasło.")
            Exit Sub
        End If

        If Not Serwer.CzyWczytanoPosterunki Then
            Blad("Przed uruchomieniem serwera należy wczytać listę posterunków ruchu.")
            Exit Sub
        End If

        If Not Serwer.Uruchom(port, txtHaslo.Text) Then
            Blad("Nie udało się uruchomić serwera.")
        Else
            PokazUruchomienie()
        End If
    End Sub

    Private Sub btnStop_Click() Handles btnStop.Click
        Serwer.Zatrzymaj()
        PokazZatrzymanie()
    End Sub

    Private Sub lvPosterunki_SelectedIndexChanged() Handles lvPosterunki.SelectedIndexChanged
        SyncLock slockListaPosterunkow
            btnRozlacz.Enabled = lvPosterunki.SelectedItems IsNot Nothing AndAlso lvPosterunki.SelectedItems.Count > 0 AndAlso lvPosterunki.SelectedItems(0).SubItems(3).Text <> ""
        End SyncLock
    End Sub

    Private Sub btnRozlacz_Click() Handles btnRozlacz.Click
        Dim adres As UShort
        Dim nazwa As String

        SyncLock slockListaPosterunkow
            If lvPosterunki.SelectedItems Is Nothing OrElse lvPosterunki.SelectedItems.Count = 0 Then Exit Sub

            adres = UShort.Parse(lvPosterunki.SelectedItems(0).SubItems(2).Text)
            nazwa = lvPosterunki.SelectedItems(0).SubItems(0).Text
        End SyncLock

        If Pytanie("Czy rozłączyć posterunek " & nazwa & "?") = DialogResult.Yes Then
            Serwer.ZakonczPolaczenie(adres)
        End If
    End Sub

    Private Sub btnOdswiez_Click() Handles btnOdswiez.Click
        OdswiezPosterunki()
    End Sub

    Private Sub Serwer_UniewaznionoListePosterunkow() Handles Serwer.UniewaznionoListePosterunkow
        OdswiezPosterunki()
    End Sub

    Private Sub Serwer_ZmianaCzasuPodlaczenia(post As String, dataPodlaczenia As String) Handles Serwer.ZmianaCzasuPodlaczenia
        Invoke(actZmianaCzasuPodlaczenia, post, dataPodlaczenia)
    End Sub

    Private Sub PokazZmianeCzasuPodlaczenia(adres As String, data As String)
        SyncLock slockListaPosterunkow
            If data = "" AndAlso lvPosterunki.SelectedItems IsNot Nothing AndAlso lvPosterunki.SelectedItems.Count > 0 AndAlso lvPosterunki.SelectedItems(0).SubItems(2).Text = adres Then
                btnRozlacz.Enabled = False
            End If

            For Each lvi As ListViewItem In lvPosterunki.Items
                If lvi.SubItems(2).Text = adres Then
                    lvi.SubItems(3).Text = data
                    Exit Sub
                End If
            Next
        End SyncLock
    End Sub

    Private Sub OdswiezPosterunki()
        Dim polaczenia As Zaleznosci.StanObslugiwanegoPosterunku() = Serwer.PobierzStanPolaczen()

        SyncLock slockListaPosterunkow
            lvPosterunki.Items.Clear()
            btnRozlacz.Enabled = False
            If polaczenia Is Nothing Then Exit Sub

            For Each pol As Zaleznosci.StanObslugiwanegoPosterunku In polaczenia
                Dim lvi As New ListViewItem(New String() {pol.NazwaPosterunku, pol.NazwaPliku, pol.Adres, pol.DataPodlaczenia, pol.OstatnieZapytanie})
                lvPosterunki.Items.Add(lvi)
            Next
        End SyncLock
    End Sub

    Private Sub PokazUruchomienie()
        PokazKontrolkiPoZmianieStanu(True, "Uruchomiony", Color.Green)
    End Sub

    Private Sub PokazZatrzymanie()
        PokazKontrolkiPoZmianieStanu(False, "Zatrzymany", Color.Red)
    End Sub

    Private Sub PokazKontrolkiPoZmianieStanu(serwerUruchomiony As Boolean, tekst As String, kolor As Color)
        txtSciezka.Enabled = Not serwerUruchomiony
        btnPrzegladaj.Enabled = Not serwerUruchomiony
        btnWczytaj.Enabled = Not serwerUruchomiony
        txtPort.Enabled = Not serwerUruchomiony
        txtHaslo.Enabled = Not serwerUruchomiony
        btnStart.Enabled = Not serwerUruchomiony
        btnStop.Enabled = serwerUruchomiony
        lblStanSerwera.Text = tekst
        lblStanSerwera.ForeColor = kolor
        If Not serwerUruchomiony Then btnRozlacz.Enabled = False
        btnOdswiez.Enabled = serwerUruchomiony
    End Sub

    Private Function Pytanie(tekst As String) As DialogResult
        Return MessageBox.Show(tekst, "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Public Sub Blad(tekst As String)
        MessageBox.Show(tekst, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
End Class