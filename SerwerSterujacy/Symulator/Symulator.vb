Friend Class wndSymulator
    Private Const NAPIS_WYSWIETLONO As String = "Wyświetlono"

    Private symulator As UrzadzenieSymulator
    Private migacz As New Migacz
    Private zaznaczonyWiersz As DaneOkien
    Private oknaSymulatora As New Dictionary(Of UShort, DaneOkien)
    Private zamykanieOkna As Boolean = False
    Private rysownik As Pulpit.TypRysownika = Pulpit.TypRysownika.KlasycznyDirect2D
    Private zdarzenia As Boolean = True

    Friend Sub New(posterunki As Zaleznosci.Pulpit(), symulator As UrzadzenieSymulator)
        InitializeComponent()

        Me.symulator = symulator
        symulator.Polacz(Me)
        PokazRysownikow()
        PokazPosterunki(posterunki)
    End Sub

    Friend Sub ZamknijBezPytania()
        zamykanieOkna = True
        Close()
    End Sub

    Friend Sub UsunOknoPulpitu(adres As UShort)
        If zamykanieOkna Then Exit Sub

        Dim okna As DaneOkien = Nothing
        If oknaSymulatora.TryGetValue(adres, okna) Then
            okna.OknoPulpitu = Nothing
            okna.ElementListy.SubItems(1).Text = String.Empty
        End If
    End Sub

    Friend Sub UsunOknoSygnalizacji(adres As UShort)
        If zamykanieOkna Then Exit Sub

        Dim okna As DaneOkien = Nothing
        If oknaSymulatora.TryGetValue(adres, okna) Then
            okna.OknoSygnalizacji = Nothing
            okna.ElementListy.SubItems(2).Text = String.Empty
        End If
    End Sub

    Friend Sub UsunOknoZwrotnic(adres As UShort)
        If zamykanieOkna Then Exit Sub

        Dim okna As DaneOkien = Nothing
        If oknaSymulatora.TryGetValue(adres, okna) Then
            okna.OknoZwrotnic = Nothing
            okna.ElementListy.SubItems(3).Text = String.Empty
        End If
    End Sub

    Friend Sub UstawSygnalizator(adresPost As UShort, adresSygn As UShort, stan As UShort)
        Dim okna As DaneOkien = Nothing
        If oknaSymulatora.TryGetValue(adresPost, okna) AndAlso okna.OknoSygnalizacji IsNot Nothing Then
            okna.OknoSygnalizacji.UstawStanSygnalizatora(adresSygn, stan)
        End If
    End Sub

    Friend Function UstawZwrotnice(adresPost As UShort, adresZwrot As UShort, stan As Zaleznosci.UstawienieRozjazduEnum) As Boolean
        Dim okna As DaneOkien = Nothing
        If oknaSymulatora.TryGetValue(adresPost, okna) AndAlso okna.OknoZwrotnic IsNot Nothing Then
            Return okna.OknoZwrotnic.UstawStanZwrotnicy(adresZwrot, stan)
        End If

        Return False
    End Function

    Friend Sub ZaznaczonoKostke(adresPosterunku As UShort, kostka As Zaleznosci.Kostka)
        Dim okno As DaneOkien = Nothing

        If oknaSymulatora.TryGetValue(adresPosterunku, okno) Then
            Dim sygn As Zaleznosci.Sygnalizator = TryCast(kostka, Zaleznosci.Sygnalizator)
            Dim zwrotn As Zaleznosci.Rozjazd = TryCast(kostka, Zaleznosci.Rozjazd)
            Dim zaznaczanaKostka As Zaleznosci.Kostka = Nothing
            Dim adresSygn As UShort? = Nothing
            Dim adresZwrotn As UShort? = Nothing

            If sygn IsNot Nothing Then
                zaznaczanaKostka = kostka
                adresSygn = sygn.Adres
            ElseIf zwrotn IsNot Nothing Then
                zaznaczanaKostka = kostka
                adresZwrotn = zwrotn.Adres
            End If

            okno.OknoPulpitu?.ZaznaczKostke(zaznaczanaKostka)
            okno.OknoSygnalizacji?.ZaznaczSygnalizator(adresSygn)
            okno.OknoZwrotnic?.ZaznaczZwrotnice(adresZwrotn)
        End If
    End Sub

    Private Sub wndSymulator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Not zamykanieOkna) AndAlso Wspolne.ZadajPytanie("Czy zakończyć tryb symulacji?") = DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If

        zamykanieOkna = True

        For Each okna As KeyValuePair(Of UShort, DaneOkien) In oknaSymulatora
            okna.Value.OknoPulpitu?.Close()
            okna.Value.OknoSygnalizacji?.Close()
            okna.Value.OknoZwrotnic?.Close()
        Next

        symulator.Polacz(Nothing)
    End Sub

    Private Sub tmrTimer_Tick() Handles tmrTimer.Tick
        migacz.Stan = Not migacz.Stan
    End Sub

    Private Sub cboRysownik_SelectedIndexChanged() Handles cboRysownik.SelectedIndexChanged
        If (Not zdarzenia) OrElse cboRysownik.SelectedItem Is Nothing Then Exit Sub

        rysownik = DirectCast(cboRysownik.SelectedItem, Wspolne.ObiektComboBox(Of Pulpit.TypRysownika)).Wartosc
    End Sub

    Private Sub lvPosterunki_SelectedIndexChanged() Handles lvPosterunki.SelectedIndexChanged
        Dim lvi As ListViewItem = Wspolne.PobierzZaznaczonyElementNaLiscie(lvPosterunki)

        If lvi IsNot Nothing Then
            PokazDostepnoscPrzyciskow(True)
            zaznaczonyWiersz = Wspolne.PobierzTagZElementuListy(Of DaneOkien)(lvi)
        Else
            PokazDostepnoscPrzyciskow(False)
            zaznaczonyWiersz = Nothing
        End If
    End Sub

    Private Sub btnPulpit_Click() Handles btnPulpit.Click
        If zaznaczonyWiersz Is Nothing Then Exit Sub

        If zaznaczonyWiersz.OknoPulpitu IsNot Nothing Then
            zaznaczonyWiersz.OknoPulpitu.Focus()
        Else
            zaznaczonyWiersz.OknoPulpitu = New wndStanPulpitu(Me, zaznaczonyWiersz.Pulpit, symulator, rysownik)
            zaznaczonyWiersz.OknoPulpitu.Show()
            zaznaczonyWiersz.ElementListy.SubItems(1).Text = NAPIS_WYSWIETLONO
        End If
    End Sub

    Private Sub btnSygnalizacja_Click() Handles btnSygnalizacja.Click
        If zaznaczonyWiersz Is Nothing Then Exit Sub

        If zaznaczonyWiersz.OknoSygnalizacji IsNot Nothing Then
            zaznaczonyWiersz.OknoSygnalizacji.Focus()
        Else
            zaznaczonyWiersz.OknoSygnalizacji = New wndStanSygnalizatorow(Me, zaznaczonyWiersz.Pulpit, migacz)
            zaznaczonyWiersz.OknoSygnalizacji.Show()
            zaznaczonyWiersz.ElementListy.SubItems(2).Text = NAPIS_WYSWIETLONO
        End If
    End Sub

    Private Sub btnZwrotnice_Click() Handles btnZwrotnice.Click
        If zaznaczonyWiersz Is Nothing Then Exit Sub

        If zaznaczonyWiersz.OknoZwrotnic IsNot Nothing Then
            zaznaczonyWiersz.OknoZwrotnic.Focus()
        Else
            zaznaczonyWiersz.OknoZwrotnic = New wndStanZwrotnic(Me, zaznaczonyWiersz.Pulpit, symulator)
            zaznaczonyWiersz.OknoZwrotnic.Show()
            zaznaczonyWiersz.ElementListy.SubItems(3).Text = NAPIS_WYSWIETLONO
        End If
    End Sub

    Private Sub PokazRysownikow()
        zdarzenia = False
        cboRysownik.Items.Clear()

        cboRysownik.Items.Add(New Wspolne.ObiektComboBox(Of Pulpit.TypRysownika)(Pulpit.TypRysownika.KlasycznyDirect2D, "Klasyczny Direct2D"))
        cboRysownik.Items.Add(New Wspolne.ObiektComboBox(Of Pulpit.TypRysownika)(Pulpit.TypRysownika.KlasycznyGDI, "Klasyczny GDI"))

        cboRysownik.SelectedIndex = 0
        zdarzenia = True
    End Sub

    Private Sub PokazPosterunki(posterunki As Zaleznosci.Pulpit())
        For i As Integer = 0 To posterunki.Length - 1
            Dim p As Zaleznosci.Pulpit = posterunki(i)
            Dim lvi As New ListViewItem
            Dim dane As New DaneOkien With {.Pulpit = p, .ElementListy = lvi}

            lvi.Text = p.Nazwa
            lvi.SubItems.Add(String.Empty)
            lvi.SubItems.Add(String.Empty)
            lvi.SubItems.Add(String.Empty)
            lvi.Tag = dane

            lvPosterunki.Items.Add(lvi)
            If Not oknaSymulatora.ContainsKey(p.Adres) Then
                oknaSymulatora.Add(p.Adres, dane)
            End If
        Next
    End Sub

    Private Sub PokazDostepnoscPrzyciskow(dostepne As Boolean)
        btnPulpit.Enabled = dostepne
        btnSygnalizacja.Enabled = dostepne
        btnZwrotnice.Enabled = dostepne
    End Sub

    Private Class DaneOkien
        Public ElementListy As ListViewItem
        Public Pulpit As Zaleznosci.Pulpit
        Public OknoPulpitu As wndStanPulpitu
        Public OknoSygnalizacji As wndStanSygnalizatorow
        Public OknoZwrotnic As wndStanZwrotnic
    End Class
End Class