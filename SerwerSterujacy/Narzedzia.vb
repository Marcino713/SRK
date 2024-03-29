﻿Friend Module Narzedzia
    Friend Const FILTR_PLIKU_POLACZEN As String = Zaleznosci.PolaczeniaStacji.OPIS_PLIKU & "|*" & Zaleznosci.PolaczeniaStacji.ROZSZERZENIE_PLIKU

    Friend Function ZadajPytanie(tekst As String) As DialogResult
        Return MessageBox.Show(tekst, "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Friend Sub PokazBlad(tekst As String)
        MessageBox.Show(tekst, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Friend Function PobierzOznaczeniePociagu(nr As UInteger, nazwa As String) As String
        Dim ozn As String = nr.ToString
        If Not String.IsNullOrEmpty(nazwa) Then ozn &= " " & nazwa

        Return ozn
    End Function
End Module