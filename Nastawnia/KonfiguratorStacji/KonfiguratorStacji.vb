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
    Private PaneleKonfKostek As Panel()
    Private ZaznaczonaKostka As Zaleznosci.Kostka

    Private Delegate Function SprawdzTypKostki(kostka As Zaleznosci.Kostka) As Boolean
    Private Delegate Function PobierzNazweKostki(kostka As Zaleznosci.Kostka) As String

#Region "Okno"

    Private Sub wndKonfiguratorStacji_Load() Handles Me.Load
        PaneleKonfKostek = {pnlKonfPrzycisk, pnlKonfRozjazd, pnlKonfSygn, pnlKonfTor}
        For i As Integer = 0 To PaneleKonfKostek.Length - 1
            PaneleKonfKostek(i).Width = splKartaPulpit.Panel2.Width
            PaneleKonfKostek(i).Location = New Point(0, 0)
        Next

        UtworzListeKostek()

        Pulpit.Kostki(4, 2) = New Zaleznosci.Tor
        Pulpit.Kostki(3, 2) = New Zaleznosci.Zakret
        Pulpit.Kostki(1, 2) = New Zaleznosci.TorKoniec
        Pulpit.Kostki(4, 3) = New Zaleznosci.RozjazdLewo() With {.Nazwa = "101", .Obrot = 90}
        Pulpit.Kostki(3, 3) = New Zaleznosci.RozjazdPrawo() With {.Nazwa = "102"}
        Pulpit.Kostki(2, 3) = New Zaleznosci.SygnalizatorManewrowy
        Pulpit.Kostki(1, 3) = New Zaleznosci.SygnalizatorPolsamoczynny
        Pulpit.Kostki(0, 3) = New Zaleznosci.SygnalizatorSamoczynny
        Pulpit.Kostki(2, 5) = New Zaleznosci.Przycisk
        Pulpit.Kostki(1, 5) = New Zaleznosci.PrzyciskTor
        Pulpit.Kostki(0, 5) = New Zaleznosci.Kierunek

        RysujPulpit()
    End Sub

    Private Sub wndKonfiguratorStacji_Resize() Handles Me.Resize
        RysujPulpit()
    End Sub

    Private Sub DodajKostkeDoListy(kostka As Zaleznosci.Kostka, nazwa As String, ix As Integer)
        Static Dim konf As New KonfiguracjaRysowania() With {.Skalowanie = 47, .RysujKrawedzieKostek = False}
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
        DodajKostkeDoListy(New Zaleznosci.Zakret(), "Zakręt", 2)
        DodajKostkeDoListy(New Zaleznosci.RozjazdLewo(), "Rozjazd lewy", 3)
        DodajKostkeDoListy(New Zaleznosci.RozjazdPrawo(), "Rozjazd prawy", 4)
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorManewrowy(), "Sygnalizator manewrowy", 5)
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorPolsamoczynny(), "Sygnalizator półsamoczynny", 6)
        DodajKostkeDoListy(New Zaleznosci.SygnalizatorSamoczynny(), "Sygnalizator samoczynny", 7)
        DodajKostkeDoListy(New Zaleznosci.Przycisk(), "Przycisk", 8)
        DodajKostkeDoListy(New Zaleznosci.PrzyciskTor(), "Przycisk z torem", 9)
        DodajKostkeDoListy(New Zaleznosci.Kierunek(), "Wjazd/wyjazd ze stacji", 10)
    End Sub

#End Region

#Region "Menu"

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

    Private Sub mnuNazwa_Click() Handles mnuNazwa.Click
        Dim wnd As New wndNazwaStacji(Pulpit.Nazwa)
        If wnd.ShowDialog = DialogResult.OK Then
            Pulpit.Nazwa = wnd.Nazwa
        End If
    End Sub

#End Region

#Region "Zakładka Pulpit"

    Private Sub lvKostki_MouseDown(sender As Object, e As MouseEventArgs) Handles lvPulpitKostki.MouseDown
        Dim lvi As ListViewItem = lvPulpitKostki.GetItemAt(e.X, e.Y)
        If lvi IsNot Nothing Then
            lvi.Selected = True
            DoDragDrop(Activator.CreateInstance(DirectCast(lvi.Tag, Type)), DragDropEffects.Copy)
        End If
    End Sub


    'Tor
    Private Sub txtKonfTorPredkosc_TextChanged() Handles txtKonfTorPredkosc.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Tor).Predkosc = PobierzLiczbeNieujemna(txtKonfTorPredkosc)
    End Sub


    'Rozjazd
    Private Sub txtKonfRozjazdAdres_TextChanged() Handles txtKonfRozjazdAdres.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).Adres = PobierzLiczbeNieujemna(txtKonfRozjazdAdres)
    End Sub

    Private Sub txtKonfRozjazdNazwa_TextChanged() Handles txtKonfRozjazdNazwa.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).Nazwa = txtKonfRozjazdNazwa.Text
    End Sub

    Private Sub txtKonfRozjazdPredkZasad_TextChanged() Handles txtKonfRozjazdPredkZasad.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).PredkoscZasadnicza = PobierzLiczbeNieujemna(txtKonfRozjazdPredkZasad)
    End Sub

    Private Sub txtKonfRozjazdPredkBoczna_TextChanged() Handles txtKonfRozjazdPredkBoczna.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).PredkoscBoczna = PobierzLiczbeNieujemna(txtKonfRozjazdPredkBoczna)
    End Sub

    Private Sub cboKonfRozjazdWprost1_SelectedIndexChanged() Handles cboKonfRozjazdWprost1.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdWprost1, rbKonfRozjazdWprost1Plus, rbKonfRozjazdWprost1Minus, roz) Then
            DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub cboKonfRozjazdWprost2_SelectedIndexChanged() Handles cboKonfRozjazdWprost2.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdWprost2, rbKonfRozjazdWprost2Plus, rbKonfRozjazdWprost2Minus, roz) Then
            DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub cboKonfRozjazdBok1_SelectedIndexChanged() Handles cboKonfRozjazdBok1.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdBok1, rbKonfRozjazdBok1Plus, rbKonfRozjazdBok1Minus, roz) Then
            DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub cboKonfRozjazdBok2_SelectedIndexChanged() Handles cboKonfRozjazdBok2.SelectedIndexChanged
        Dim roz As Zaleznosci.Rozjazd = Nothing
        If PrzetworzZaznaczenieRozjazduZaleznego(cboKonfRozjazdBok2, rbKonfRozjazdBok2Plus, rbKonfRozjazdBok2Minus, roz) Then
            DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).RozjazdZalezny = roz
        End If
    End Sub

    Private Sub rbKonfRozjazdWprost1Plus_CheckedChanged() Handles rbKonfRozjazdWprost1Plus.CheckedChanged
        If rbKonfRozjazdWprost1Plus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdWprost1Minus_CheckedChanged() Handles rbKonfRozjazdWprost1Minus.CheckedChanged
        If rbKonfRozjazdWprost1Minus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdWprost2Plus_CheckedChanged() Handles rbKonfRozjazdWprost2Plus.CheckedChanged
        If rbKonfRozjazdWprost2Plus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdWprost2Minus_CheckedChanged() Handles rbKonfRozjazdWprost2Minus.CheckedChanged
        If rbKonfRozjazdWprost2Minus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdBok1Plus_CheckedChanged() Handles rbKonfRozjazdBok1Plus.CheckedChanged
        If rbKonfRozjazdBok1Plus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdBok1Minus_CheckedChanged() Handles rbKonfRozjazdBok1Minus.CheckedChanged
        If rbKonfRozjazdBok1Minus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub

    Private Sub rbKonfRozjazdBok2Plus_CheckedChanged() Handles rbKonfRozjazdBok2Plus.CheckedChanged
        If rbKonfRozjazdBok2Plus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost
    End Sub

    Private Sub rbKonfRozjazdBok2Minus_CheckedChanged() Handles rbKonfRozjazdBok2Minus.CheckedChanged
        If rbKonfRozjazdBok2Minus.Checked Then DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd).ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Bok
    End Sub


    'Sygnalizacja
    Private Sub txtKonfSygnAdres_TextChanged() Handles txtKonfSygnAdres.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Sygnalizator).Adres = PobierzLiczbeNieujemna(txtKonfSygnAdres)
    End Sub

    Private Sub txtKonfSygnNazwa_TextChanged() Handles txtKonfSygnNazwa.TextChanged
        DirectCast(ZaznaczonaKostka, Zaleznosci.Sygnalizator).Nazwa = txtKonfSygnNazwa.Text
    End Sub

    Private Sub cboKonfSygnSygnNast_SelectedIndexChanged() Handles cboKonfSygnSygnNast.SelectedIndexChanged
        If cboKonfSygnSygnNast.SelectedItem Is Nothing Then Exit Sub
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cboKonfSygnSygnNast.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka))
        DirectCast(ZaznaczonaKostka, Zaleznosci.SygnalizatorUzalezniony).SygnalizatorNastepny = DirectCast(el.Wartosc, Zaleznosci.Sygnalizator)
    End Sub

    Private Sub cbKonfSygnZiel_CheckedChanged() Handles cbKonfSygnZiel.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnZiel, Zaleznosci.DostepneSwiatlaEnum.Zielone)
    End Sub

    Private Sub cbKonfSygnPomGor_CheckedChanged() Handles cbKonfSygnPomGor.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnPomGor, Zaleznosci.DostepneSwiatlaEnum.PomaranczoweGora)
    End Sub

    Private Sub cbKonfSygnCzer_CheckedChanged() Handles cbKonfSygnCzer.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnCzer, Zaleznosci.DostepneSwiatlaEnum.Czerwone)
    End Sub

    Private Sub cbKonfSygnPomDol_CheckedChanged() Handles cbKonfSygnPomDol.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnPomDol, Zaleznosci.DostepneSwiatlaEnum.PomaranczoweDol)
    End Sub

    Private Sub cbKonfSygnBiale_CheckedChanged() Handles cbKonfSygnBiale.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnBiale, Zaleznosci.DostepneSwiatlaEnum.Biale)
    End Sub

    Private Sub cbKonfSygnZielPas_CheckedChanged() Handles cbKonfSygnZielPas.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnZielPas, Zaleznosci.DostepneSwiatlaEnum.ZielonyPas)
    End Sub

    Private Sub cbKonfSygnPomPas_CheckedChanged() Handles cbKonfSygnPomPas.CheckedChanged
        UstawDostepneSwiatlo(cbKonfSygnPomPas, Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas)
    End Sub


    'Przycisk
    Private Sub cboKonfPrzyciskTyp_SelectedIndexChanged() Handles cboKonfPrzyciskTyp.SelectedIndexChanged
        Dim aktywny As Boolean = False
        Dim el As Object = cboKonfPrzyciskTyp.SelectedItem

        If el Is Nothing Then
            aktywny = False
        Else
            aktywny = True

            Select Case ZaznaczonaKostka.Typ
                Case Zaleznosci.TypKostki.Przycisk
                    Dim typ As Zaleznosci.TypPrzyciskuEnum = DirectCast(el, ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)).Wartosc
                    Dim prz As Zaleznosci.Przycisk = DirectCast(ZaznaczonaKostka, Zaleznosci.Przycisk)
                    prz.TypPrzycisku = typ
                    If typ = Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow Then aktywny = False
                    cboKonfPrzyciskSygnalizator.Items.Clear()
                    cboKonfPrzyciskSygnalizator.Items.AddRange(PobierzElementyDoComboBox(AddressOf CzySygnalizatorPolsamoczynny, AddressOf PobierzNazweSygnalizatora))
                    ZaznaczElement(cboKonfPrzyciskSygnalizator, prz.ObslugiwanySygnalizator)

                Case Zaleznosci.TypKostki.PrzyciskTor
                    Dim typ As Zaleznosci.TypPrzyciskuTorEnum = DirectCast(el, ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)).Wartosc
                    Dim prz As Zaleznosci.PrzyciskTor = DirectCast(ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                    If (prz.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy And (typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny Or typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy)) Or
                            ((prz.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny Or prz.TypPrzycisku = Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy) And typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy) Then
                        prz.ObslugiwanySygnalizator = Nothing
                    End If
                    prz.TypPrzycisku = typ
                    Dim f As SprawdzTypKostki
                    If typ = Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy Then
                        f = AddressOf CzySygnalizatorManewrowy
                    Else
                        f = AddressOf CzySygnalizatorPolsamoczynny
                    End If
                    cboKonfPrzyciskSygnalizator.Items.Clear()
                    cboKonfPrzyciskSygnalizator.Items.AddRange(PobierzElementyDoComboBox(f, AddressOf PobierzNazweSygnalizatora))
                    ZaznaczElement(cboKonfPrzyciskSygnalizator, prz.ObslugiwanySygnalizator)

            End Select

        End If

        cboKonfPrzyciskSygnalizator.Enabled = aktywny
    End Sub

    Private Sub cboKonfPrzyciskSygnalizator_SelectedIndexChanged() Handles cboKonfPrzyciskSygnalizator.SelectedIndexChanged
        If cboKonfPrzyciskSygnalizator.SelectedItem Is Nothing Then Exit Sub

        Dim sygn As Zaleznosci.Kostka = DirectCast(cboKonfPrzyciskSygnalizator.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc
        Select Case ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(ZaznaczonaKostka, Zaleznosci.Przycisk)
                prz.ObslugiwanySygnalizator = DirectCast(sygn, Zaleznosci.SygnalizatorPolsamoczynny)

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                prz.ObslugiwanySygnalizator = DirectCast(sygn, Zaleznosci.Sygnalizator)

        End Select
    End Sub


    'Walidacja
    Private Function PobierzLiczbeNieujemna(obiekt As TextBox) As Integer
        Dim predkosc As Integer = 0
        If Integer.TryParse(obiekt.Text, predkosc) Then
            If predkosc < 0 Then predkosc = 0
        End If

        Return predkosc
    End Function


    'Wyświetlanie paneli
    Private Sub UkryjPaneleKonf()
        For i As Integer = 0 To PaneleKonfKostek.Length - 1
            PaneleKonfKostek(i).Visible = False
        Next
    End Sub

    Private Sub PokazPanelKonf()
        Select Case ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Tor, Zaleznosci.TypKostki.Zakret
                PokazKonfTor()
            Case Zaleznosci.TypKostki.RozjazdLewo, Zaleznosci.TypKostki.RozjazdPrawo
                PokazKonfRozjazd()
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy, Zaleznosci.TypKostki.SygnalizatorSamoczynny, Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                PokazKonfSygn()
            Case Zaleznosci.TypKostki.Przycisk, Zaleznosci.TypKostki.PrzyciskTor
                PokazKonfPrzycisk()
        End Select
    End Sub

    Private Sub PokazKonfTor()
        Dim tor As Zaleznosci.Tor = DirectCast(ZaznaczonaKostka, Zaleznosci.Tor)

        txtKonfTorPredkosc.Text = tor.Predkosc.ToString()
        pnlKonfTor.Visible = True
    End Sub

    Private Sub PokazKonfRozjazd()
        Dim roz As Zaleznosci.Rozjazd = DirectCast(ZaznaczonaKostka, Zaleznosci.Rozjazd)
        txtKonfRozjazdAdres.Text = roz.Adres.ToString
        txtKonfRozjazdNazwa.Text = roz.Nazwa
        txtKonfRozjazdPredkZasad.Text = roz.PredkoscZasadnicza.ToString
        txtKonfRozjazdPredkBoczna.Text = roz.PredkoscBoczna.ToString

        Dim pusty As New ObiektComboBox(Of Zaleznosci.Kostka)(Nothing, "")
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzyRozjazd, AddressOf PobierzNazweRozjazdu)

        cboKonfRozjazdWprost1.Items.Clear()
        cboKonfRozjazdWprost2.Items.Clear()
        cboKonfRozjazdBok1.Items.Clear()
        cboKonfRozjazdBok2.Items.Clear()

        cboKonfRozjazdWprost1.Items.Add(pusty)
        cboKonfRozjazdWprost2.Items.Add(pusty)
        cboKonfRozjazdBok1.Items.Add(pusty)
        cboKonfRozjazdBok2.Items.Add(pusty)

        cboKonfRozjazdWprost1.Items.AddRange(el)
        cboKonfRozjazdWprost2.Items.AddRange(el)
        cboKonfRozjazdBok1.Items.AddRange(el)
        cboKonfRozjazdBok2.Items.AddRange(el)

        ZaznaczElement(cboKonfRozjazdWprost1, roz.ZaleznosciJesliWprost(0).RozjazdZalezny)
        ZaznaczElement(cboKonfRozjazdWprost2, roz.ZaleznosciJesliWprost(1).RozjazdZalezny)
        ZaznaczElement(cboKonfRozjazdBok1, roz.ZaleznosciJesliBok(0).RozjazdZalezny)
        ZaznaczElement(cboKonfRozjazdBok2, roz.ZaleznosciJesliBok(1).RozjazdZalezny)

        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliWprost(0).RozjazdZalezny, rbKonfRozjazdWprost1Plus, rbKonfRozjazdWprost1Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliWprost(1).RozjazdZalezny, rbKonfRozjazdWprost2Plus, rbKonfRozjazdWprost2Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliBok(0).RozjazdZalezny, rbKonfRozjazdBok1Plus, rbKonfRozjazdBok1Minus)
        AktywujPrzyciskiKonfiguracjiRozjazdu(roz.ZaleznosciJesliBok(1).RozjazdZalezny, rbKonfRozjazdBok2Plus, rbKonfRozjazdBok2Minus)

        If roz.ZaleznosciJesliWprost(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdWprost1Plus.Checked = True Else rbKonfRozjazdWprost1Minus.Checked = True
        If roz.ZaleznosciJesliWprost(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdWprost2Plus.Checked = True Else rbKonfRozjazdWprost2Minus.Checked = True
        If roz.ZaleznosciJesliBok(0).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdBok1Plus.Checked = True Else rbKonfRozjazdBok1Minus.Checked = True
        If roz.ZaleznosciJesliBok(1).Konfiguracja = Zaleznosci.UstawienieRozjazduZaleznegoEnum.Wprost Then rbKonfRozjazdBok2Plus.Checked = True Else rbKonfRozjazdBok2Minus.Checked = True

        pnlKonfRozjazd.Visible = True
    End Sub

    Private Sub PokazKonfSygn()
        Dim sygn As Zaleznosci.Sygnalizator = DirectCast(ZaznaczonaKostka, Zaleznosci.Sygnalizator)
        txtKonfSygnAdres.Text = sygn.Adres.ToString
        txtKonfSygnNazwa.Text = sygn.Nazwa.ToString
        cboKonfSygnSygnNast.Enabled = (sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorManewrowy)
        pnlKonfSygnSwiatla.Visible = (sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny)

        cboKonfSygnOdcinekNast.Items.Clear()

        cboKonfSygnSygnNast.Items.Clear()
        Dim sygn_nast As Zaleznosci.Sygnalizator = Nothing
        If sygn.Typ <> Zaleznosci.TypKostki.SygnalizatorManewrowy Then
            Dim pusty_sygn As New ObiektComboBox(Of Zaleznosci.Kostka)(Nothing, "")
            Dim sygnalizatory As ObiektComboBox(Of Zaleznosci.Kostka)() = PobierzElementyDoComboBox(AddressOf CzySygnalizatorUzalezniony, AddressOf PobierzNazweSygnalizatora)
            cboKonfSygnSygnNast.Items.Add(pusty_sygn)
            cboKonfSygnSygnNast.Items.AddRange(sygnalizatory)
            sygn_nast = DirectCast(sygn, Zaleznosci.SygnalizatorUzalezniony).SygnalizatorNastepny
            ZaznaczElement(cboKonfSygnSygnNast, sygn_nast)
        End If

        If sygn.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Then
            Dim sw As Zaleznosci.DostepneSwiatlaEnum = DirectCast(ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny).DostepneSwiatla
            cbKonfSygnZiel.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Zielone) <> 0
            cbKonfSygnPomGor.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweGora) <> 0
            cbKonfSygnCzer.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Czerwone) <> 0
            cbKonfSygnPomDol.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweDol) <> 0
            cbKonfSygnBiale.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.Biale) <> 0
            cbKonfSygnZielPas.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.ZielonyPas) <> 0
            cbKonfSygnPomPas.Checked = (sw And Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas) <> 0
        End If

        pnlKonfSygn.Visible = True
    End Sub

    Private Sub PokazKonfPrzycisk()
        cboKonfPrzyciskTyp.Items.Clear()

        Select Case ZaznaczonaKostka.Typ
            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(ZaznaczonaKostka, Zaleznosci.Przycisk)
                cboKonfPrzyciskTyp.Items.AddRange({
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)(Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy, "Sygnał zastępczy"),
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuEnum)(Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow, "Zwolnienie przebiegów")
                })
                cboKonfPrzyciskTyp.SelectedIndex = prz.TypPrzycisku

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(ZaznaczonaKostka, Zaleznosci.PrzyciskTor)
                cboKonfPrzyciskTyp.Items.AddRange({
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny, "Sygnalizator półsamoczynny"),
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy, "Sygnalizator manewrowy"),
                New ObiektComboBox(Of Zaleznosci.TypPrzyciskuTorEnum)(Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy, "Sygnał manewrowy na sygnalizatorze półsamoczynnym")
                })
                cboKonfPrzyciskTyp.SelectedIndex = prz.TypPrzycisku

        End Select

        pnlKonfPrzycisk.Visible = True
    End Sub

    'Inne
    Private Function PrzetworzZaznaczenieRozjazduZaleznego(cbo As ComboBox, rbPlus As RadioButton, rbMinus As RadioButton, ByRef rozjazd As Zaleznosci.Rozjazd) As Boolean
        If cbo.SelectedItem Is Nothing Then Return False
        Dim el As ObiektComboBox(Of Zaleznosci.Kostka) = DirectCast(cbo.SelectedItem, ObiektComboBox(Of Zaleznosci.Kostka))
        AktywujPrzyciskiKonfiguracjiRozjazdu(el.Wartosc, rbPlus, rbMinus)
        rozjazd = DirectCast(el.Wartosc, Zaleznosci.Rozjazd)
        Return True
    End Function

    Private Sub AktywujPrzyciskiKonfiguracjiRozjazdu(rozjazdZalezny As Zaleznosci.Kostka, rbPlus As RadioButton, rbMinus As RadioButton)
        If rozjazdZalezny Is Nothing Then
            rbPlus.Enabled = False
            rbMinus.Enabled = False
        Else
            rbPlus.Enabled = True
            rbMinus.Enabled = True
        End If
    End Sub

    Private Function CzyRozjazd(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.RozjazdLewo Or kostka.Typ = Zaleznosci.TypKostki.RozjazdPrawo
    End Function

    Private Function PobierzNazweRozjazdu(kostka As Zaleznosci.Kostka) As String
        Return DirectCast(kostka, Zaleznosci.Rozjazd).Nazwa
    End Function

    Private Sub UstawDostepneSwiatlo(cb As CheckBox, kolor As Zaleznosci.DostepneSwiatlaEnum)
        Dim sygn As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(ZaznaczonaKostka, Zaleznosci.SygnalizatorPolsamoczynny)
        If cb.Checked Then
            sygn.DostepneSwiatla = sygn.DostepneSwiatla Or kolor
        Else
            sygn.DostepneSwiatla = sygn.DostepneSwiatla And (Not kolor)
        End If
    End Sub

    Private Function CzySygnalizatorUzalezniony(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny Or kostka.Typ = Zaleznosci.TypKostki.SygnalizatorSamoczynny
    End Function

    Private Function CzySygnalizatorPolsamoczynny(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
    End Function

    Private Function CzySygnalizatorManewrowy(kostka As Zaleznosci.Kostka) As Boolean
        Return kostka.Typ = Zaleznosci.TypKostki.SygnalizatorManewrowy
    End Function

    Private Function PobierzNazweSygnalizatora(kostka As Zaleznosci.Kostka) As String
        Return DirectCast(kostka, Zaleznosci.Sygnalizator).Nazwa
    End Function

    Private Function PobierzElementyDoComboBox(sprawdzanie As SprawdzTypKostki, nazwa As PobierzNazweKostki) As ObiektComboBox(Of Zaleznosci.Kostka)()
        Dim kostki As New List(Of ObiektComboBox(Of Zaleznosci.Kostka))

        For x As Integer = 0 To Pulpit.Szerokosc - 1
            For y As Integer = 0 To Pulpit.Wysokosc - 1
                Dim k As Zaleznosci.Kostka = Pulpit.Kostki(x, y)
                If k IsNot Nothing AndAlso sprawdzanie(k) AndAlso k IsNot ZaznaczonaKostka Then
                    kostki.Add(New ObiektComboBox(Of Zaleznosci.Kostka)(k, nazwa(k)))
                End If
            Next
        Next

        Return kostki.OrderBy(Function(k As ObiektComboBox(Of Zaleznosci.Kostka)) nazwa(k.Wartosc)).ToArray()
    End Function

    Private Sub ZaznaczElement(cbo As ComboBox, el As Zaleznosci.Kostka)
        If el Is Nothing Then
            cbo.SelectedItem = Nothing
            Exit Sub
        End If

        For i As Integer = 0 To cbo.Items.Count - 1
            If DirectCast(cbo.Items(i), ObiektComboBox(Of Zaleznosci.Kostka)).Wartosc Is el Then
                cbo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

#End Region

#Region "Pulpit"

    'Private Sub pctPulpit_LostFocus() Handles pctPulpit.LostFocus
    '    Konfiguracja.WyczyscZaznaczenie()
    '    RysujPulpit()
    'End Sub

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
                Pulpit.UsunKostkeZPowiazan(ZaznaczonaKostka)
                Konfiguracja.WyczyscZaznaczenie()
                RysujPulpit()
            End If

        End If
    End Sub

    Private Sub pctPulpit_Click() Handles pctPulpit.Click
        pctPulpit.Focus()
        UkryjPaneleKonf()
        Dim p As Point = PobierzKliknieteWspolrzedneKostki()
        If CzyKostkaWZakresiePulpitu(p) AndAlso Pulpit.Kostki(p.X, p.Y) IsNot Nothing Then
            Konfiguracja.ZaznaczX = p.X
            Konfiguracja.ZaznaczY = p.Y
            ZaznaczonaKostka = Pulpit.Kostki(p.X, p.Y)
            PokazPanelKonf()
        Else
            Konfiguracja.WyczyscZaznaczenie()
            ZaznaczonaKostka = Nothing
        End If
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

    Private Sub RysujPulpit()
        Dim img As Image = pctPulpit.Image
        Dim obr As Image = Rysuj(Pulpit, Konfiguracja)
        pctPulpit.Image = obr
        pctPulpit.Size = obr.Size
        img?.Dispose()
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


#End Region

    Private Class ObiektComboBox(Of T)
        Public Wartosc As T
        Public Tekst As String
        Public Sub New(el As T, napis As String)
            Wartosc = el
            Tekst = napis
        End Sub
        Public Overrides Function ToString() As String
            Return Tekst
        End Function
    End Class

End Class