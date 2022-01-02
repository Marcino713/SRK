Public Class wndNastawnia
    Private Const FILTR_PLIKU As String = Zaleznosci.PolaczeniaStacji.OPIS_PLIKU & "|*" & Zaleznosci.PolaczeniaStacji.ROZSZERZENIE_PLIKU

    Private Sub wndNastawnia_Load() Handles MyBase.Load
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub

    Private Sub mnuKonfiguratorStacji_Click() Handles mnuKonfiguratorStacji.Click
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub

    Private Sub mnuNowePolaczenia_Click() Handles mnuNowePolaczenia.Click
        PokazKomunikat("Plik połączeń należy zapisać w tym samym folderze, w którym znajdują się pliki konfiguracji posterunków ruchu.")
        Dim dlg As New SaveFileDialog
        dlg.Filter = FILTR_PLIKU
        If dlg.ShowDialog = DialogResult.OK Then
            Dim polaczenia As Zaleznosci.PolaczeniaStacji = Zaleznosci.PolaczeniaStacji.OtworzFolder(dlg.FileName)
            If polaczenia Is Nothing Then
                PokazBlad("Nie udało się zapisać pliku.")
            Else
                Dim wnd As New wndKonfiguratorPolaczen(polaczenia)
                wnd.Show()
            End If
        End If
    End Sub

    Private Sub mnuOtworzPolaczenia_Click() Handles mnuOtworzPolaczenia.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = FILTR_PLIKU
        If dlg.ShowDialog = DialogResult.OK Then
            Dim polaczenia As Zaleznosci.PolaczeniaStacji = Zaleznosci.PolaczeniaStacji.OtworzPlik(dlg.FileName)
            If polaczenia Is Nothing Then
                PokazBlad("Nie udało się otworzyć pliku.")
            Else
                Dim wnd As New wndKonfiguratorPolaczen(polaczenia)
                wnd.Show()
            End If
        End If
    End Sub
End Class