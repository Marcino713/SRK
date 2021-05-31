Public Class wndKonfiguratorStacji
    Private Const SKALOWANIE_ZMIANA As Single = 0.05
    Private Const SKALOWANIE_MIN As Single = 30
    Private Const SKALOWANIE_MAX As Single = 200
    Private Const ZWIEKSZ_OBROT As Integer = 90
    Private Const KAT_PELNY As Integer = 360

    Private Konfiguracja As New KonfiguracjaRysowania
    Private Pulpit As New Zaleznosci.Pulpit
    Private PoprzedniPunkt As New Point(-1, -1)
    Private LokalizacjaPulpitu As New Point(-1, -1)

    Private Sub DodajKostkeDoListy(kostka As Zaleznosci.Kostka, nazwa As String, ix As Integer)
        Static Dim konf As New KonfiguracjaRysowania() With {.Skalowanie = 48, .RysujKrawedzieKostek = False}
        Dim pulpit As New Zaleznosci.Pulpit(1, 1)
        pulpit.Kostki(0, 0) = kostka
        imlKostki.Images.Add(Rysuj(pulpit, konf))
        Dim lvi As New ListViewItem(nazwa, ix)
        lvi.Tag = kostka.GetType()
        lvPulpitKostki.Items.Add(lvi)
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
        Dim obr As Image = Rysuj(Pulpit, Konfiguracja)
        pctPulpit.Image = obr
        pctPulpit.Size = obr.Size
        img?.Dispose()
    End Sub

    Private Sub mnuDodajKostki_Click() Handles mnuDodajKostki.Click
        Dim wnd As New wndEdytorPowierzchni(wndEdytorPowierzchni.TypEdycji.Dodaj)
        If wnd.ShowDialog = DialogResult.OK Then
            Pulpit.PowiekszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek)
            RysujPulpit()
        End If
    End Sub

    Private Sub mnuUsunKostki_Click() Handles mnuUsunKostki.Click
        Dim wnd As New wndEdytorPowierzchni(wndEdytorPowierzchni.TypEdycji.Usun)
        If (wnd.ShowDialog = DialogResult.OK) Then
            Try
                If Pulpit.PomniejszPulpit(wnd.KierunekEdycji, wnd.LiczbaKostek) Then
                    RysujPulpit()
                Else
                    PokazBlad("Nie udało się usunąć kostek - w wybranym zakresie usuwania pulpit nie jest pusty.")
                End If
            Catch ex As Exception
                PokazBlad("Wystąpił błąd podczas usuwania kostek:" & vbCrLf & ex.Message)
            End Try
        End If
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

    Private Sub pctPulpit_MouseWheel(sender As Object, e As MouseEventArgs) Handles pctPulpit.MouseWheel, pnlPulpit.MouseWheel
        Dim sk As Single = Konfiguracja.Skalowanie + e.Delta * SKALOWANIE_ZMIANA
        If sk < SKALOWANIE_MIN Then sk = SKALOWANIE_MIN
        If sk > SKALOWANIE_MAX Then sk = SKALOWANIE_MAX

        Konfiguracja.Skalowanie = sk
        Dim poz As Point = pctPulpit.PointToClient(MousePosition)
        Dim wspx As Double = poz.X / pctPulpit.Size.Width
        Dim wspy As Double = poz.Y / pctPulpit.Size.Height
        RysujPulpit()

        Dim nowy As New Point(CInt(wspx * pctPulpit.Width), CInt(wspy * pctPulpit.Height))
        Dim p As Point = pctPulpit.PointToClient(MousePosition)
        pctPulpit.Location = New Point(pctPulpit.Location.X + p.X - nowy.X, pctPulpit.Location.Y + p.Y - nowy.Y)
    End Sub

    Private Sub lvKostki_MouseDown(sender As Object, e As MouseEventArgs) Handles lvPulpitKostki.MouseDown
        Dim lvi As ListViewItem = lvPulpitKostki.GetItemAt(e.X, e.Y)
        If lvi IsNot Nothing Then
            lvi.Selected = True
            DoDragDrop(Activator.CreateInstance(DirectCast(lvi.Tag, Type)), DragDropEffects.Copy)
        End If
    End Sub

    Private Sub wndKonfiguratorStacji_Resize() Handles Me.Resize
        RysujPulpit()
    End Sub

    Private Sub pnlPulpit_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlPulpit.MouseMove, pctPulpit.MouseMove
        If e.Button = MouseButtons.Left Then
            If PoprzedniPunkt.X <> -1 Then
                Dim zm As Point = pnlPulpit.PointToClient(MousePosition)
                Dim zmX As Integer = zm.X - PoprzedniPunkt.X
                Dim zmY As Integer = zm.Y - PoprzedniPunkt.Y
                pctPulpit.Location = New Point(LokalizacjaPulpitu.X + zmX, LokalizacjaPulpitu.Y + zmY)
            End If
        End If
    End Sub

    Private Sub pnlPulpit_MouseUp() Handles pnlPulpit.MouseUp, pctPulpit.MouseUp
        PoprzedniPunkt.X = -1
        PoprzedniPunkt.Y = -1
    End Sub

    Private Sub pnlPulpit_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlPulpit.MouseDown, pctPulpit.MouseDown
        PoprzedniPunkt = pnlPulpit.PointToClient(MousePosition)
        LokalizacjaPulpitu.X = pctPulpit.Location.X
        LokalizacjaPulpitu.Y = pctPulpit.Location.Y
    End Sub

    Private Sub mnuNazwa_Click() Handles mnuNazwa.Click
        Dim wnd As New wndNazwaStacji(Pulpit.Nazwa)
        If wnd.ShowDialog = DialogResult.OK Then
            Pulpit.Nazwa = wnd.Nazwa
        End If
    End Sub

    Private Sub pnlPulpit_DragOver(sender As Object, e As DragEventArgs) Handles pnlPulpit.DragOver
        If Not e.Data.GetFormats()(0).StartsWith("Zaleznosci.") Then
            e.Effect = DragDropEffects.None
            Exit Sub
        End If

        Dim p As Point = PobierzKliknieteWspolrzedneKostki()
        If CzyKostkaWZakresiePulpitu(p) AndAlso Pulpit.Kostki(p.X, p.Y) Is Nothing Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
            p.X = -1
            p.Y = -1
        End If

        Konfiguracja.PrzesuwanaKostka = PobierzDodawanaKostke(e)
        If Konfiguracja.ZaznaczX <> p.X Or Konfiguracja.ZaznaczY <> p.Y Then
            Konfiguracja.ZaznaczX = p.X
            Konfiguracja.ZaznaczY = p.Y
            RysujPulpit()
        End If
    End Sub

    Private Sub pnlPulpit_DragDrop(sender As Object, e As DragEventArgs) Handles pnlPulpit.DragDrop
        Dim k As Zaleznosci.Kostka = PobierzDodawanaKostke(e)
        Pulpit.Kostki(Konfiguracja.ZaznaczX, Konfiguracja.ZaznaczY) = k

        Konfiguracja.WyczyscZaznaczenie()
        RysujPulpit()
    End Sub

    Private Function PobierzDodawanaKostke(e As DragEventArgs) As Zaleznosci.Kostka
        Return DirectCast(e.Data.GetData(e.Data.GetFormats()(0)), Zaleznosci.Kostka)
    End Function

    Private Function CzyKostkaWZakresiePulpitu(wspolrzedne As Point) As Boolean
        Return wspolrzedne.X >= 0 And wspolrzedne.X < Pulpit.Szerokosc And wspolrzedne.Y >= 0 And wspolrzedne.Y < Pulpit.Wysokosc
    End Function

    Private Function PobierzKliknieteWspolrzedneKostki() As Point
        Dim k As Point = pctPulpit.PointToClient(MousePosition)
        Return New Point(
        CInt(k.X / Konfiguracja.Skalowanie - 0.5),
        CInt(k.Y / Konfiguracja.Skalowanie - 0.5)
        )
    End Function

    Private Sub pctPulpit_Click() Handles pctPulpit.Click
        pctPulpit.Focus()
        Dim p As Point = PobierzKliknieteWspolrzedneKostki()
        If CzyKostkaWZakresiePulpitu(p) AndAlso Pulpit.Kostki(p.X, p.Y) IsNot Nothing Then
            Konfiguracja.ZaznaczX = p.X
            Konfiguracja.ZaznaczY = p.Y
        Else
            Konfiguracja.WyczyscZaznaczenie()
        End If
        RysujPulpit()
    End Sub

    Private Sub pctPulpit_LostFocus() Handles pctPulpit.LostFocus
        Konfiguracja.WyczyscZaznaczenie()
        RysujPulpit()
    End Sub

    Private Sub pctPulpit_KeyDown(sender As Object, e As KeyEventArgs) Handles pctPulpit.KeyDown
        Dim p As New Point(Konfiguracja.ZaznaczX, Konfiguracja.ZaznaczY)
        If CzyKostkaWZakresiePulpitu(p) Then

            If e.KeyData = Keys.R Then
                Dim obrot As Integer = Pulpit.Kostki(p.X, p.Y).Obrot
                obrot = (obrot + ZWIEKSZ_OBROT) Mod KAT_PELNY
                Pulpit.Kostki(p.X, p.Y).Obrot = obrot
                RysujPulpit()

            ElseIf e.KeyData = Keys.Delete
                Pulpit.Kostki(p.X, p.Y) = Nothing
                Konfiguracja.WyczyscZaznaczenie()
                RysujPulpit()
            End If

        End If
    End Sub
End Class