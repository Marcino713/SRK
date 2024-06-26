﻿Public Module Narzedzia
    Public Const DATA_FORMAT As String = "d.MM.yyyy H:mm"
    Public Const ARG_TYP_RYSOWNIKA As String = "-tp"
    Public Const ARG_TYP_RYSOWNIKA_KLASYCZNY_GDI As String = "klgdi"
    Public Const ARG_TYP_RYSOWNIKA_KLASYCZNY_D2D As String = "kld2d"
    Public TypRysownika As TypRysownika = TypRysownika.KlasycznyDirect2D

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

    Friend Function PobierzZaznaczonyElement(Of T)(lv As ListView) As T
        If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count = 0 Then
            Return Nothing
        Else
            Return DirectCast(lv.SelectedItems(0).Tag, T)
        End If
    End Function

    Friend Function PrzyczynaZakonczeniaSesjiKlientaToString(przyczyna As Zaleznosci.PrzyczynaZakonczeniaSesjiKlienta) As String
        Dim dodatek As String = ""

        Select Case przyczyna
            Case Zaleznosci.PrzyczynaZakonczeniaSesjiKlienta.RozlaczeniePrzezSerwer
                dodatek = " przez serwer"
            Case Zaleznosci.PrzyczynaZakonczeniaSesjiKlienta.PrzekroczenieCzasuOczekiwania
                dodatek = " z powodu zbyt długiego czasu inicjalizacji"
        End Select

        Return "Połączenie zostało zamknięte" & dodatek & "."
    End Function

    Friend Function PobierzOznaczeniePociagu(nr As UInteger, nazwa As String) As String
        Dim ozn As String = nr.ToString
        If Not String.IsNullOrEmpty(nazwa) Then ozn &= " " & nazwa

        Return ozn
    End Function

    Friend Sub UstawTypRysownika(argumenty As String())
        Dim typ As String = PobierzWartoscArgumentuWierszaPolecen(argumenty, ARG_TYP_RYSOWNIKA)

        Select Case typ?.ToLower
            Case ARG_TYP_RYSOWNIKA_KLASYCZNY_GDI
                TypRysownika = TypRysownika.KlasycznyGDI

            Case ARG_TYP_RYSOWNIKA_KLASYCZNY_D2D
                TypRysownika = TypRysownika.KlasycznyDirect2D

            Case Else
                TypRysownika = TypRysownika.KlasycznyDirect2D

        End Select
    End Sub

    Private Function PobierzWartoscArgumentuWierszaPolecen(listaArg As String(), argument As String) As String
        Dim ile As Integer = listaArg.Length - 1

        For i As Integer = 0 To ile
            If i < ile AndAlso String.Compare(listaArg(i), argument, StringComparison.OrdinalIgnoreCase) = 0 Then
                Return listaArg(i + 1)
            End If
        Next

        Return Nothing
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