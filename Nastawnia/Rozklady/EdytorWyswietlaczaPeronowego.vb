Friend Class wndEdytorWyswietlaczaPeronowego
    Private dane As New DaneWyswietlaczaPeronowego
    Private wyswietlacz As WyswietlaczPeronowy

    Private Sub wndEdytorWyswietlaczaPeronowego_Load() Handles Me.Load
        PokazStyleWyswietlaczy()
    End Sub

    Private Sub txtPrzewoznik_TextChanged() Handles txtPrzewoznik.TextChanged
        dane.Przewoznik = txtPrzewoznik.Text
        Rysuj()
    End Sub

    Private Sub txtKategoria_TextChanged() Handles txtKategoria.TextChanged
        dane.Kategoria = txtKategoria.Text
        Rysuj()
    End Sub

    Private Sub txtNumer_TextChanged() Handles txtNumer.TextChanged
        dane.Numer = txtNumer.Text
        Rysuj()
    End Sub

    Private Sub txtNazwa_TextChanged() Handles txtNazwa.TextChanged
        dane.Nazwa = txtNazwa.Text
        Rysuj()
    End Sub

    Private Sub txtStacja_TextChanged() Handles txtStacja.TextChanged
        dane.Stacja = txtStacja.Text
        Rysuj()
    End Sub

    Private Sub rbTypOdjazd_CheckedChanged() Handles rbTypOdjazd.CheckedChanged
        If rbTypOdjazd.Checked Then
            dane.Typ = TypPostojuPociagu.Odjazd
            Rysuj()
        End If
    End Sub

    Private Sub rbTypPrzyjazd_CheckedChanged() Handles rbTypPrzyjazd.CheckedChanged
        If rbTypPrzyjazd.Checked Then
            dane.Typ = TypPostojuPociagu.Przyjazd
            Rysuj()
        End If
    End Sub

    Private Sub txtGodzina_TextChanged() Handles txtGodzina.TextChanged
        Dim c As TimeSpan
        dane.Czas = If(TimeSpan.TryParse(txtGodzina.Text, c), c, New TimeSpan(0, 0, 0))
        Rysuj()
    End Sub

    Private Sub txtOpoznienie_TextChanged() Handles txtOpoznienie.TextChanged
        Dim c As UInteger = 0
        dane.Opoznienie = If(UInteger.TryParse(txtOpoznienie.Text, c), c, 0UI)
        Rysuj()
    End Sub

    Private Sub txtPrzez_TextChanged() Handles txtPrzez.TextChanged
        dane.Przez = txtPrzez.Lines
        Rysuj()
    End Sub

    Private Sub txtSektory_TextChanged() Handles txtSektory.TextChanged
        dane.Sektory = txtSektory.Lines
        Rysuj()
    End Sub

    Private Sub txtUwagi_TextChanged() Handles txtUwagi.TextChanged
        dane.Uwagi = txtUwagi.Lines
        Rysuj()
    End Sub

    Private Sub cboStyl_SelectedIndexChanged() Handles cboStyl.SelectedIndexChanged
        wyswietlacz = CType(cboStyl.SelectedItem, ObiektComboBox(Of WyswietlaczPeronowy)).Wartosc
        Rysuj()
    End Sub

    Private Sub btnZapisz_Click() Handles btnZapisz.Click
        Dim dlg As New SaveFileDialog
        dlg.Filter = "Pliki BMP|*.bmp"
        If dlg.ShowDialog = DialogResult.OK Then
            pctTablica.Image.Save(dlg.FileName, Imaging.ImageFormat.Bmp)
            PokazKomunikat("Obraz wyświetlacza został zapisany.")
        End If
    End Sub

    Private Sub PokazStyleWyswietlaczy()
        cboStyl.Items.Clear()

        cboStyl.Items.Add(New ObiektComboBox(Of WyswietlaczPeronowy)(New WyswietlaczAustriacki, "Austriacki"))
    End Sub

    Private Sub Rysuj()
        If wyswietlacz IsNot Nothing Then
            Dim bm As Image = pctTablica.Image
            pctTablica.Image = wyswietlacz.Rysuj(dane)
            bm?.Dispose()
            btnZapisz.Enabled = True
        End If
    End Sub
End Class