Public Module Narzedzia
    Public Function ZadajPytanie(Pytanie As String) As DialogResult
        Return MessageBox.Show(Pytanie, "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Public Sub PokazBlad(Komunikat As String)
        MessageBox.Show(Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Function PobierzOznaczeniePociagu(nr As UInteger, nazwa As String) As String
        Dim ozn As String = nr.ToString
        If Not String.IsNullOrEmpty(nazwa) Then ozn &= " " & nazwa
        Return ozn
    End Function
End Module