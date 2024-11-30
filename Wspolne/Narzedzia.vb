Public Module Narzedzia
    Public Const OPIS_PLIKU_PULPITU As String = "Schemat posterunku ruchu"
    Public Const OPIS_PLIKU_POLACZEN As String = "Połączenia posterunków ruchu"
    Public Const FILTR_PLIKU_PULPITU As String = OPIS_PLIKU_PULPITU & "|*" & Zaleznosci.Pulpit.ROZSZERZENIE_PLIKU
    Public Const FILTR_PLIKU_POLACZEN As String = OPIS_PLIKU_POLACZEN & "|*" & Zaleznosci.PolaczeniaPosterunkow.ROZSZERZENIE_PLIKU

    Public Function ZadajPytanie(pytanie As String) As DialogResult
        Return MessageBox.Show(pytanie, "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Public Sub PokazBlad(komunikat As String)
        MessageBox.Show(komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub PokazKomunikat(komunikat As String)
        MessageBox.Show(komunikat, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function ZadajPytanieTrzyodpowiedziowe(pytanie As String) As DialogResult
        Return MessageBox.Show(pytanie, "Pytanie", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
    End Function

    Public Function PobierzZaznaczonyElementNaLiscie(lv As ListView) As ListViewItem
        If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count = 0 Then
            Return Nothing
        Else
            Return lv.SelectedItems(0)
        End If
    End Function

    Public Function PobierzTagZElementuListy(Of T)(lvi As ListViewItem) As T
        If lvi Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(lvi.Tag, T)
        End If
    End Function

    Public Function PobierzOznaczeniePociagu(nr As UInteger, nazwa As String) As String
        Dim ozn As String = nr.ToString
        If Not String.IsNullOrEmpty(nazwa) Then ozn &= " " & nazwa
        Return ozn
    End Function

    Public Function CzyRozne(a As UShort?, b As UShort?) As Boolean
        Return _
            (a.HasValue And (Not b.HasValue)) OrElse
            ((Not a.HasValue) And b.HasValue) OrElse
            (a.HasValue AndAlso b.HasValue AndAlso a.Value <> b.Value)
    End Function
End Module