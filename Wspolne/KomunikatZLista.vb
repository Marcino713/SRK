Public Class wndKomunikatZLista
    Public Sub New(komunikat As String, elementy As IEnumerable(Of Komunikat))
        InitializeComponent()
        lblKomunikat.Text = komunikat

        For Each el As Komunikat In elementy
            lvLista.Items.Add(New ListViewItem() With {.ForeColor = el.Kolor, .Text = el.Tekst})
        Next
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        Close()
    End Sub
End Class

Public Class Komunikat
    Public Shared ReadOnly KOLOR_KOMUNIKAT As Color = Color.Black
    Public Shared ReadOnly KOLOR_OSTRZEZENIE As Color = Color.Orange
    Public Shared ReadOnly KOLOR_BLAD As Color = Color.Red

    Public Tekst As String
    Public Kolor As Color

    Public Sub New(tekst As String)
        Me.New(tekst, KOLOR_KOMUNIKAT)
    End Sub

    Public Sub New(tekst As String, kolor As Color)
        Me.Tekst = tekst
        Me.Kolor = kolor
    End Sub
End Class