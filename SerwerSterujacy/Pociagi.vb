Friend Class wndPociagi
    Private Delegate Sub ZmianaStanuPociagu(pociag As Zaleznosci.StanPociagu)

    Private WithEvents Serwer As Zaleznosci.SerwerTCP
    Private PociagiSlownik As New Dictionary(Of UInteger, ListViewItem)
    Private slockPociag As New Object

    Private actDodajPociag As ZmianaStanuPociagu = AddressOf DodajPociag
    Private actPrzetworzZmianeParametruPociagu As Action(Of UInteger, Integer, String, ZmianaStanuPociagu) = AddressOf PrzetworzZmianeParametruPociagu
    Private actUsunPociag As Action(Of UInteger) = AddressOf UsunPociag

    Friend Sub New(serwer As Zaleznosci.SerwerTCP)
        InitializeComponent()

        Me.Serwer = serwer
        OdswiezListePociagow()
    End Sub

    Private Sub wndPociagi_FormClosed() Handles Me.FormClosed
        Serwer = Nothing
    End Sub

    Private Sub lvPociagi_SelectedIndexChanged() Handles lvPociagi.SelectedIndexChanged
        PokazDostepnoscKontrolek(PobierzZaznaczonyPociag() IsNot Nothing)
    End Sub

    Private Sub btnOdswiez_Click() Handles btnOdswiez.Click
        OdswiezListePociagow()
    End Sub

    Private Sub btnPrzenazwij_Click() Handles btnPrzenazwij.Click
        Dim poc As Zaleznosci.StanPociagu = PobierzZaznaczonyPociag()

        If poc IsNot Nothing Then
            Dim okno As New wndZmianaNazwyPociagu(poc.Numer, poc.Nazwa)

            If okno.ShowDialog = DialogResult.OK Then
                Serwer.ZmienNazwePociagu(poc.Numer, okno.NowaNazwa)
            End If
        End If
    End Sub

    Private Sub btnUsun_Click() Handles btnUsun.Click
        Dim poc As Zaleznosci.StanPociagu = PobierzZaznaczonyPociag()

        If poc IsNot Nothing Then
            If ZadajPytanie($"Czy usunąć pociąg { PobierzOznaczeniePociagu(poc.Numer, poc.Nazwa)}?") = DialogResult.Yes Then
                Serwer.UsunPociagZSieci(poc.Numer)
            End If
        End If
    End Sub

    Private Sub Serwer_DodanoPociag(pociag As Zaleznosci.StanPociagu) Handles Serwer.DodanoPociag
        Invoke(actDodajPociag, pociag)
    End Sub

    Private Sub Serwer_ZmienionNazwePociagu(nrPociagu As UInteger, nazwa As String) Handles Serwer.ZmienionNazwePociagu
        Dim metoda As ZmianaStanuPociagu = Sub(p) p.Nazwa = nazwa
        Invoke(actPrzetworzZmianeParametruPociagu, nrPociagu, 1, nazwa, metoda)
    End Sub

    Private Sub Serwer_ZmienionoLiczbeOsiPociagu(nrPociagu As UInteger, liczbaOsi As UShort) Handles Serwer.ZmienionoLiczbeOsiPociagu
        Dim metoda As ZmianaStanuPociagu = Sub(p) p.LiczbaOsi = liczbaOsi
        Invoke(actPrzetworzZmianeParametruPociagu, nrPociagu, 3, liczbaOsi.ToString, metoda)
    End Sub

    Private Sub Serwer_ZmienionoPosterunekSterujacyPociagiem(nrPociagu As UInteger, posterunek As String) Handles Serwer.ZmienionoPosterunekSterujacyPociagiem
        Dim metoda As ZmianaStanuPociagu = Sub(p) p.SterujacyPosterunek = posterunek
        Invoke(actPrzetworzZmianeParametruPociagu, nrPociagu, 5, posterunek, metoda)
    End Sub

    Private Sub Serwer_ZmienionoLokalizacjePociagu(nrPociagu As UInteger, lokalizacja As String) Handles Serwer.ZmienionoLokalizacjePociagu
        Dim metoda As ZmianaStanuPociagu = Sub(p) p.Lokalizacja = lokalizacja
        Invoke(actPrzetworzZmianeParametruPociagu, nrPociagu, 6, lokalizacja, metoda)
    End Sub

    Private Sub Serwer_UsunietoPociag(nrPociagu As UInteger) Handles Serwer.UsunietoPociag
        Invoke(actUsunPociag, nrPociagu)
    End Sub

    Private Sub PokazDostepnoscKontrolek(dostepne As Boolean)
        btnPrzenazwij.Enabled = dostepne
        btnUsun.Enabled = dostepne
    End Sub

    Private Sub PrzetworzZmianeParametruPociagu(nrPociagu As UInteger, kolumna As Integer, wartosc As String, metoda As ZmianaStanuPociagu)
        SyncLock slockPociag
            Dim lvi As ListViewItem = Nothing

            If PociagiSlownik.TryGetValue(nrPociagu, lvi) Then
                lvi.SubItems(kolumna).Text = wartosc
                metoda(CType(lvi.Tag, Zaleznosci.StanPociagu))
            End If
        End SyncLock
    End Sub

    Private Sub UsunPociag(nrPociagu As UInteger)
        SyncLock slockPociag
            Dim lvi As ListViewItem = Nothing

            If PociagiSlownik.TryGetValue(nrPociagu, lvi) Then
                PociagiSlownik.Remove(nrPociagu)
                lvPociagi.Items.Remove(lvi)
            End If
        End SyncLock
    End Sub

    Private Function PobierzZaznaczonyPociag() As Zaleznosci.StanPociagu
        SyncLock slockPociag
            If lvPociagi.SelectedItems IsNot Nothing AndAlso lvPociagi.SelectedItems.Count > 0 Then
                Return CType(lvPociagi.SelectedItems(0).Tag, Zaleznosci.StanPociagu)
            Else
                Return Nothing
            End If
        End SyncLock
    End Function

    Private Sub OdswiezListePociagow()
        Dim pociagi As Zaleznosci.StanPociagu() = Serwer.PobierzStanPociagow

        SyncLock slockPociag
            lvPociagi.SelectedItems.Clear()
            lvPociagi.Items.Clear()
            PociagiSlownik.Clear()
        End SyncLock

        For Each poc As Zaleznosci.StanPociagu In pociagi
            DodajPociag(poc)
        Next
    End Sub

    Private Sub DodajPociag(pociag As Zaleznosci.StanPociagu)
        SyncLock slockPociag
            Dim lvi As New ListViewItem(
                New String() {
                    pociag.Numer.ToString,
                    If(pociag.Nazwa, ""),
                    If(pociag.Sterowalny, "Tak", "Nie"),
                    pociag.LiczbaOsi.ToString,
                    If(pociag.DodajacyPosterunek, ""),
                    If(pociag.SterujacyPosterunek, ""),
                    If(pociag.Lokalizacja, "")
                }) With {.Tag = pociag}

            lvPociagi.Items.Add(lvi)
            PociagiSlownik.Add(pociag.Numer, lvi)
        End SyncLock
    End Sub
End Class