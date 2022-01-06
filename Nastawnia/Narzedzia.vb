Public Module Narzedzia
    Public Const DATA_FORMAT As String = "d.MM.yyyy H:mm"

    Public Sub PokazBlad(Komunikat As String)
        MessageBox.Show(Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub PokazKomunikat(Komunikat As String)
        MessageBox.Show(Komunikat, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function ZadajPytanie(Pytanie As String) As DialogResult
        Return MessageBox.Show(Pytanie, "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Public Function ZadajPytanieTrzyodpowiedziowe(Pytanie As String) As DialogResult
        Return MessageBox.Show(Pytanie, "Pytanie", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
    End Function

    Public Function KolorRGB(wartosc As String) As Color
        If wartosc.Length <> 7 Then
            Throw New ArgumentException("Wartość koloru musi być siedmioznakowym ciagiem.")
        End If

        Return Color.FromArgb(
            Integer.Parse(wartosc(1) & wartosc(2), Globalization.NumberStyles.HexNumber),
            Integer.Parse(wartosc(3) & wartosc(4), Globalization.NumberStyles.HexNumber),
            Integer.Parse(wartosc(5) & wartosc(6), Globalization.NumberStyles.HexNumber)
            )
    End Function

    Friend Function PobierzZaznaczonyElement(Of T)(lv As ListView) As T
        If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count = 0 Then
            Return Nothing
        Else
            Return DirectCast(lv.SelectedItems(0).Tag, T)
        End If
    End Function

    Friend Class ObiektComboBox(Of T)
        Public Property Wartosc As T
        Public Property Tekst As String
        Public Sub New(el As T, napis As String)
            Wartosc = el
            Tekst = napis
        End Sub
        Public Overrides Function ToString() As String
            Return Tekst
        End Function
    End Class

End Module