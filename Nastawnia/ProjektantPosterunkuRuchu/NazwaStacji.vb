Public Class wndNazwaStacji
    Public Adres As UShort = 0
    Public Nazwa As String = ""

    Public Sub New(AdresStacji As Integer, NazwaStacji As String, DataUtworzenia As Date)
        InitializeComponent()
        txtAdres.Text = AdresStacji.ToString
        txtNazwa.Text = NazwaStacji
        lblDataUtworzenia.Text = DataUtworzenia.ToString(DATA_FORMAT)
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        If txtAdres.Text = "" Then
            Wspolne.PokazBlad("Należy podać adres posterunku.")
            Exit Sub
        End If

        If Not UShort.TryParse(txtAdres.Text, Adres) Then
            Wspolne.PokazBlad("Adres posterunku musi być liczbą dodatnią.")
            Exit Sub
        End If

        If txtNazwa.Text = "" Then
            Wspolne.PokazBlad("Należy podać nazwę posterunku.")
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