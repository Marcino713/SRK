Public Class wndKonfiguratorZwrotnic
    Private serwer As Zaleznosci.SerwerTCP

    Public Sub New(serwer As Zaleznosci.SerwerTCP)
        InitializeComponent()

        Me.serwer = serwer
        PokazListeZwrotnic()
    End Sub

    Private Sub lvZwrotnice_SelectedIndexChanged() Handles lvZwrotnice.SelectedIndexChanged
        PokazDostepnoscKontrolek(PobierzZaznaczonaZwrotnice() IsNot Nothing)
    End Sub

    Private Sub bnWprost_Click() Handles btnWprost.Click
        WyslijUstawienie(numWprost.Value)
    End Sub

    Private Sub btnBok_Click() Handles btnBok.Click
        WyslijUstawienie(numBok.Value)
    End Sub

    Private Sub PokazListeZwrotnic()
        Dim zwrotnice As IEnumerable(Of Zaleznosci.DaneZwrotnicy) = serwer.PobierzZwrotnice() _
        .OrderBy(Function(z) z.NazwaPosterunku) _
        .ThenBy(Function(z) z.AdresZwrotnicy)

        For Each z As Zaleznosci.DaneZwrotnicy In zwrotnice
            lvZwrotnice.Items.Add(New ListViewItem(New String() {z.NazwaPosterunku, z.AdresZwrotnicy.ToString, z.NazwaZwrotnicy}) With {.Tag = z})
        Next
    End Sub

    Private Function PobierzZaznaczonaZwrotnice() As Zaleznosci.DaneZwrotnicy
        If lvZwrotnice.SelectedItems Is Nothing OrElse lvZwrotnice.SelectedItems.Count = 0 Then Return Nothing
        Return CType(lvZwrotnice.SelectedItems(0).Tag, Zaleznosci.DaneZwrotnicy)
    End Function

    Private Sub WyslijUstawienie(wartosc As Decimal)
        Dim z As Zaleznosci.DaneZwrotnicy = PobierzZaznaczonaZwrotnice()
        If z IsNot Nothing Then
            serwer.UstawZwrotniceSerwisowo(New Zaleznosci.UstawZwrotniceSerwisowoUrz() With {.AdresPosterunku = z.AdresPosterunku, .AdresUrzadzenia = z.AdresZwrotnicy, .CzasWypelnienia = CUShort(wartosc)})
        End If
    End Sub

    Private Sub PokazDostepnoscKontrolek(dostepne As Boolean)
        numWprost.Enabled = dostepne
        btnWprost.Enabled = dostepne
        numBok.Enabled = dostepne
        btnBok.Enabled = dostepne
    End Sub

End Class