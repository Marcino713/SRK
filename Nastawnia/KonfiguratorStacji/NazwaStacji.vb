Public Class wndNazwaStacji
    Public Nazwa As String
    Private Sub btnOK_Click() Handles btnOK.Click
        If txtNazwa.Text = "" Then
            PokazBlad("Nie podano nazwy posterunku.")
            Exit Sub
        End If

        Nazwa = txtNazwa.Text
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        Close()
    End Sub
End Class