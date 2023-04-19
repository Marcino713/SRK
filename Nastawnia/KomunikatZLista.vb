Public Class wndKomunikatZLista
    Public Sub New(komunikat As String, elementy As IEnumerable(Of String))
        InitializeComponent()
        lblKomunikat.Text = komunikat

        For Each el As String In elementy
            lstLista.Items.Add(el)
        Next
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        Close()
    End Sub
End Class