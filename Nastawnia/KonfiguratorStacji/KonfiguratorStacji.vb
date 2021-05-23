Public Class wndKonfiguratorStacji
    Private Const SKALOWANIE_ZMIANA As Single = 0.05
    Private Const SKALOWANIE_MIN As Single = 30
    Private Const SKALOWANIE_MAX As Single = 200

    Private Konfiguracja As New KonfiguracjaRysowania
    Private Pulpit As New Zaleznosci.Pulpit

    Private Sub DodajKostkeDoListy(kostka As Zaleznosci.Kostka, nazwa As String, ix As Integer)
        Static Dim konf As New KonfiguracjaRysowania() With {.Skalowanie = 48, .RysujKrawedzieKostek = False}
        Dim pulpit As New Zaleznosci.Pulpit(1, 1)
        pulpit.Kostki(0, 0) = kostka
        imlKostki.Images.Add(Rysuj(pulpit, konf))
        lvKostki.Items.Add(nazwa, ix)
    End Sub

    Private Sub UtworzListeKostek()
        DodajKostkeDoListy(New Zaleznosci.Tor(), "Tor", 0)
        DodajKostkeDoListy(New Zaleznosci.TorKoniec(), "Koniec toru", 1)
        DodajKostkeDoListy(New Zaleznosci.ZakretLewo(), "Zakręt lewy", 2)
        DodajKostkeDoListy(New Zaleznosci.ZakretPrawo(), "Zakręt prawy", 3)
        DodajKostkeDoListy(New Zaleznosci.RozjazdLewo(), "Rozjazd lewy", 4)
        DodajKostkeDoListy(New Zaleznosci.RozjazdPrawo(), "Rozjazd prawy", 5)
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorManewrowy(), "Sygnalizator manewrowy", 6)
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorPolsamoczynny(), "Sygnalizator półsamoczynny", 7)
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorSamoczynny(), "Sygnalizator samoczynny", 8)
        DodajKostkeDoListy(New Zaleznosci.Przycisk(), "Przycisk", 9)
        DodajKostkeDoListy(New Zaleznosci.PrzyciskTor(), "Przycisk z torem", 10)
        DodajKostkeDoListy(New Zaleznosci.Kierunek(), "Wjazd/wyjazd ze stacji", 11)
    End Sub

    Private Sub RysujPulpit()
        Dim img As Image = pctPulpit.Image
        pctPulpit.Image = Rysuj(Pulpit, Konfiguracja)
        img?.Dispose()
    End Sub

    Private Sub mnuDodajKostki_Click() Handles mnuDodajKostki.Click

    End Sub

    Private Sub mnuUsunKostki_Click() Handles mnuUsunKostki.Click

    End Sub

    Private Sub wndKonfiguratorStacji_Load() Handles Me.Load
        UtworzListeKostek()

        Pulpit.Kostki(4, 2) = New Zaleznosci.Tor
        Pulpit.Kostki(3, 2) = New Zaleznosci.ZakretLewo
        Pulpit.Kostki(2, 2) = New Zaleznosci.ZakretPrawo
        Pulpit.Kostki(1, 2) = New Zaleznosci.TorKoniec
        Pulpit.Kostki(4, 3) = New Zaleznosci.RozjazdLewo() With {.Numer = 101, .Obrot = 90}
        Pulpit.Kostki(3, 3) = New Zaleznosci.RozjazdPrawo() With {.Numer = 102}
        Pulpit.Kostki(2, 3) = New Zaleznosci.SygnalizatorManewrowy
        Pulpit.Kostki(1, 3) = New Zaleznosci.SygnalizatorPolsamoczynny
        Pulpit.Kostki(0, 3) = New Zaleznosci.SygnalizatorSamoczynny
        Pulpit.Kostki(2, 5) = New Zaleznosci.Przycisk
        Pulpit.Kostki(1, 5) = New Zaleznosci.PrzyciskTor
        Pulpit.Kostki(0, 5) = New Zaleznosci.Kierunek

        RysujPulpit()
    End Sub

    Private Sub pctPulpit_MouseMove(sender As Object, e As MouseEventArgs) Handles pctPulpit.MouseMove
        If e.Button = MouseButtons.Left Then
            'Debug.Print("A")
        End If
    End Sub

    Private Sub pctPulpit_MouseWheel(sender As Object, e As MouseEventArgs) Handles pctPulpit.MouseWheel
        Dim sk As Single = Konfiguracja.Skalowanie + e.Delta * SKALOWANIE_ZMIANA
        If sk < SKALOWANIE_MIN Then sk = SKALOWANIE_MIN
        If sk > SKALOWANIE_MAX Then sk = SKALOWANIE_MAX
        Konfiguracja.Skalowanie = sk
        RysujPulpit()
    End Sub

    Private Sub lvKostki_MouseDown(sender As Object, e As MouseEventArgs) Handles lvKostki.MouseDown
        Dim lvi As ListViewItem = lvKostki.GetItemAt(e.X, e.Y)
        If lvi IsNot Nothing Then
            DoDragDrop(lvi.Text, DragDropEffects.All)
        End If
    End Sub

    Private Sub wndKonfiguratorStacji_Resize() Handles Me.Resize
        RysujPulpit()
    End Sub
End Class