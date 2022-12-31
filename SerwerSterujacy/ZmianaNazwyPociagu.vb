Friend Class wndZmianaNazwyPociagu
    Public Property NowaNazwa As String

    Public Sub New(numer As UInteger, nazwa As String)
        InitializeComponent()
        lblNumer.Text = numer.ToString
        txtNazwa.Text = nazwa
    End Sub

    Private Sub btnZmien_Click() Handles btnZmien.Click
        NowaNazwa = txtNazwa.Text
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class