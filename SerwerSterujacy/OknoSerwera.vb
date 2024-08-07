﻿Imports System.IO

Public Class wndOknoSerwera
    Private Const WCZYTYWANIE_BLAD As String = "Błąd wczytywania połączeń"
    Private Const WCZYTYWANIE_SUKCES As String = "Wczytano poprawnie"
    Private WithEvents Serwer As New Zaleznosci.SerwerTCP
    Private WithEvents OknoPociagow As wndPociagi
    Private WithEvents OknoAdresyIp As wndListaAdresowIP
    Private PosterunkiSlownik As New Dictionary(Of String, ListViewItem)

    Private actZmianaCzasuPodlaczenia As Action(Of String, String, String) = AddressOf PokazZmianeCzasuPodlaczenia
    Private slockListaPosterunkow As New Object

    Public Sub New()
        Me.New(True)
    End Sub

    Public Sub New(wczytajPlik As Boolean)
        InitializeComponent()
        PokazZatrzymanie()

        If wczytajPlik Then
            Dim argumenty As String() = Environment.GetCommandLineArgs()
            WczytajPlikPolaczen(argumenty)
        End If
    End Sub

    Private Sub wndOknoGlowne_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Serwer.Uruchomiony Then
            If Wspolne.ZadajPytanie("Czy zamknąć okno? Spowoduje to zatrzymanie serwera.") = DialogResult.Yes Then
                Serwer.Zatrzymaj()
                Serwer.RozlaczZUrzadzeniami()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnPrzegladaj_Click() Handles btnPrzegladaj.Click
        Dim dlg As New OpenFileDialog With {
            .Filter = Wspolne.FILTR_PLIKU_POLACZEN
        }

        If dlg.ShowDialog = DialogResult.OK Then
            txtSciezka.Text = dlg.FileName
        End If
    End Sub

    Private Sub btnWczytaj_Click() Handles btnWczytaj.Click
        If Serwer.Uruchomiony Then
            Wspolne.PokazBlad("Przed wczytaniem pliku połączeń należy zatrzymać serwer.")
            Exit Sub
        End If

        If txtSciezka.Text = "" Then
            Wspolne.PokazBlad("Należy podać ścieżkę pliku połączeń.")
            Exit Sub
        End If

        Dim wynik As Boolean = Serwer.WczytajPolaczenie(txtSciezka.Text)
        lblStanWczytania.Text = If(wynik, WCZYTYWANIE_SUKCES, WCZYTYWANIE_BLAD)
    End Sub

    Private Sub btnStart_Click() Handles btnStart.Click
        Dim port As UShort

        If Not UShort.TryParse(txtPortTCP.Text, port) Then
            Wspolne.PokazBlad("W polu Port należy podać liczbą całkowitą dodatnią.")
            Exit Sub
        End If

        If txtHaslo.Text = "" Then
            Wspolne.PokazBlad("Należy podać hasło.")
            Exit Sub
        End If

        If Not Serwer.CzyWczytanoPosterunki Then
            Wspolne.PokazBlad("Przed uruchomieniem serwera należy wczytać listę posterunków ruchu.")
            Exit Sub
        End If

        If Not Serwer.Uruchom(port, txtHaslo.Text) Then
            Wspolne.PokazBlad("Nie udało się uruchomić serwera.")
        Else
            PokazUruchomienie()
        End If
    End Sub

    Private Sub btnStop_Click() Handles btnStop.Click
        Serwer.Zatrzymaj()
        PokazZatrzymanie()
    End Sub

    Private Sub btnFiltrujAdresy_Click() Handles btnFiltrujAdresy.Click
        If OknoAdresyIp Is Nothing Then
            OknoAdresyIp = New wndListaAdresowIP(Serwer.AkceptacjaAdresow)
            OknoAdresyIp.Show()
        Else
            OknoAdresyIp.Focus()
        End If
    End Sub

    Private Sub btnUartPolacz_Click() Handles btnUartPolacz.Click
        Try
            spKomunikacja.PortName = txtUartPort.Text
            spKomunikacja.Open()
            Serwer.PolaczZUrzadzeniami(spKomunikacja.BaseStream)
            PokazStanKontrolekUart()
        Catch
            Wspolne.PokazBlad("Nie udało się nawiązać połączenia z modułem UART.")
        End Try
    End Sub

    Private Sub txtUartRozlacz_Click() Handles btnUartRozlacz.Click
        Serwer.RozlaczZUrzadzeniami()
        PokazStanKontrolekUart()
    End Sub

    Private Sub btnPociagi_Click() Handles btnPociagi.Click
        If OknoPociagow Is Nothing Then
            OknoPociagow = New wndPociagi(Serwer)
            OknoPociagow.Show()
        Else
            OknoPociagow.Focus()
        End If
    End Sub

    Private Sub btnKonfZwrotnic_Click() Handles btnKonfZwrotnic.Click
        If Not Serwer.CzyWczytanoPosterunki Then
            Wspolne.PokazBlad("Przed konfiguracją zwrotnic należy wczytać listę posterunków ruchu.")
            Exit Sub
        End If

        If Not Serwer.Uruchomiony And spKomunikacja.IsOpen Then
            Dim wnd As New wndKonfiguratorZwrotnic(Serwer)
            wnd.ShowDialog()
        End If
    End Sub

    Private Sub lvPosterunki_SelectedIndexChanged() Handles lvPosterunki.SelectedIndexChanged
        SyncLock slockListaPosterunkow
            btnRozlacz.Enabled =
                lvPosterunki.SelectedItems IsNot Nothing AndAlso
                lvPosterunki.SelectedItems.Count > 0 AndAlso
                CType(lvPosterunki.SelectedItems(0).Tag, Zaleznosci.StanObslugiwanegoPosterunku).DataPodlaczenia <> ""
        End SyncLock
    End Sub

    Private Sub btnRozlacz_Click() Handles btnRozlacz.Click
        Dim post As Zaleznosci.StanObslugiwanegoPosterunku

        SyncLock slockListaPosterunkow
            If lvPosterunki.SelectedItems Is Nothing OrElse lvPosterunki.SelectedItems.Count = 0 Then Exit Sub

            post = CType(lvPosterunki.SelectedItems(0).Tag, Zaleznosci.StanObslugiwanegoPosterunku)
        End SyncLock

        If Wspolne.ZadajPytanie($"Czy rozłączyć posterunek {post.NazwaPosterunku}?") = DialogResult.Yes Then
            Serwer.ZakonczPolaczenie(UShort.Parse(post.Adres))
        End If
    End Sub

    Private Sub btnOdswiez_Click() Handles btnOdswiez.Click
        OdswiezPosterunki()
    End Sub

    Private Sub OknoPociagow_FormClosed() Handles OknoPociagow.FormClosed
        OknoPociagow = Nothing
    End Sub

    Private Sub OknoAdresyIp_FormClosed() Handles OknoAdresyIp.FormClosed
        If OknoAdresyIp.DialogResult = DialogResult.OK Then
            Serwer.AkceptacjaAdresow = OknoAdresyIp.Ustawienia
        End If

        OknoAdresyIp = Nothing
    End Sub

    Private Sub Serwer_UniewaznionoListePosterunkow() Handles Serwer.UniewaznionoListePosterunkow
        OdswiezPosterunki()
    End Sub

    Private Sub Serwer_ZmianaCzasuPodlaczenia(post As String, dataPodlaczenia As String, adresIp As String) Handles Serwer.ZmianaCzasuPodlaczenia
        Invoke(actZmianaCzasuPodlaczenia, post, dataPodlaczenia, adresIp)
    End Sub

    Private Sub PokazZmianeCzasuPodlaczenia(adres As String, data As String, adresIp As String)
        SyncLock slockListaPosterunkow
            If lvPosterunki.SelectedItems IsNot Nothing AndAlso lvPosterunki.SelectedItems.Count > 0 AndAlso CType(lvPosterunki.SelectedItems(0).Tag, Zaleznosci.StanObslugiwanegoPosterunku).Adres = adres Then
                btnRozlacz.Enabled = data <> ""
            End If

            Dim lvi As ListViewItem = Nothing
            If PosterunkiSlownik.TryGetValue(adres, lvi) Then
                lvi.SubItems(3).Text = data
                lvi.SubItems(5).Text = adresIp
                Dim post As Zaleznosci.StanObslugiwanegoPosterunku = CType(lvi.Tag, Zaleznosci.StanObslugiwanegoPosterunku)
                post.DataPodlaczenia = data
            End If
        End SyncLock
    End Sub

    Private Sub OdswiezPosterunki()
        Dim polaczenia As Zaleznosci.StanObslugiwanegoPosterunku() = Serwer.PobierzStanPolaczen()

        SyncLock slockListaPosterunkow
            lvPosterunki.Items.Clear()
            PosterunkiSlownik.Clear()
            btnRozlacz.Enabled = False
            If polaczenia Is Nothing Then Exit Sub
            Dim polEn As IEnumerable(Of Zaleznosci.StanObslugiwanegoPosterunku) = polaczenia.OrderBy(Function(p) p.NazwaPosterunku)

            For Each pol As Zaleznosci.StanObslugiwanegoPosterunku In polEn
                Dim lvi As New ListViewItem(New String() {pol.NazwaPosterunku, pol.NazwaPliku, pol.Adres, pol.DataPodlaczenia, pol.OstatnieZapytanie, pol.AdresIp}) With {
                        .Tag = pol
                    }

                lvPosterunki.Items.Add(lvi)
                PosterunkiSlownik.Add(pol.Adres, lvi)
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
        txtPortTCP.Enabled = Not serwerUruchomiony
        txtHaslo.Enabled = Not serwerUruchomiony
        btnStart.Enabled = Not serwerUruchomiony
        btnStop.Enabled = serwerUruchomiony
        lblStanSerwera.Text = tekst
        lblStanSerwera.ForeColor = kolor
        btnPociagi.Enabled = serwerUruchomiony
        btnOdswiez.Enabled = serwerUruchomiony

        If Not serwerUruchomiony Then
            btnRozlacz.Enabled = False
            OknoPociagow?.Close()
        End If

        PokazStanKontrolekUart()
    End Sub

    Private Sub PokazStanKontrolekUart()
        Dim serwerDziala As Boolean = Serwer.Uruchomiony
        Dim uartDziala As Boolean = spKomunikacja.IsOpen

        lblUartStan.Text = If(uartDziala, "Połączono", "Rozłączono")
        lblUartStan.ForeColor = If(uartDziala, Color.Green, Color.Red)
        txtUartPort.Enabled = (Not serwerDziala) And (Not uartDziala)
        btnUartPolacz.Enabled = (Not serwerDziala) And (Not uartDziala)
        btnUartRozlacz.Enabled = (Not serwerDziala) And uartDziala
        btnKonfZwrotnic.Enabled = (Not serwerDziala) And uartDziala
    End Sub

    Private Sub WczytajPlikPolaczen(argumenty As String())
        Dim znalezionoPlik As Boolean = False

        For Each arg As String In argumenty
            If arg.EndsWith(Zaleznosci.PolaczeniaPosterunkow.ROZSZERZENIE_PLIKU) AndAlso File.Exists(arg) Then
                znalezionoPlik = True

                If Serwer.WczytajPolaczenie(arg) Then
                    txtSciezka.Text = arg
                    lblStanWczytania.Text = WCZYTYWANIE_SUKCES
                    Exit Sub
                End If
            End If
        Next

        If znalezionoPlik Then lblStanWczytania.Text = WCZYTYWANIE_BLAD
    End Sub
End Class