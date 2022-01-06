Public Class wndNastawnia
    Private Const FILTR_PLIKU As String = Zaleznosci.PolaczeniaStacji.OPIS_PLIKU & "|*" & Zaleznosci.PolaczeniaStacji.ROZSZERZENIE_PLIKU

    Private Sub mnuKonfiguratorStacji_Click() Handles mnuKonfiguratorStacji.Click
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub

    Private Sub mnuNowePolaczenia_Click() Handles mnuNowePolaczenia.Click
        PokazKomunikat("Plik połączeń należy zapisać w tym samym folderze, w którym znajdują się pliki konfiguracji posterunków ruchu.")
        WczytajPolaczenia(New SaveFileDialog, AddressOf Zaleznosci.PolaczeniaStacji.OtworzFolder, "Nie udało się zapisać pliku.")
    End Sub

    Private Sub mnuOtworzPolaczenia_Click() Handles mnuOtworzPolaczenia.Click
        WczytajPolaczenia(New OpenFileDialog, AddressOf Zaleznosci.PolaczeniaStacji.OtworzPlik, "Nie udało się otworzyć pliku.")
    End Sub

    Private Sub WczytajPolaczenia(Okno As FileDialog, MetodaOtwierajaca As Func(Of String, Zaleznosci.PolaczeniaStacji), KomunikatBledu As String)
        Okno.Filter = FILTR_PLIKU

        If Okno.ShowDialog = DialogResult.OK Then
            Dim polaczenia As Zaleznosci.PolaczeniaStacji = MetodaOtwierajaca(Okno.FileName)
            If polaczenia Is Nothing Then
                PokazBlad(KomunikatBledu)
            Else
                Dim wnd As New wndKonfiguratorPolaczen(polaczenia)
                wnd.Show()
            End If
        End If
    End Sub
End Class