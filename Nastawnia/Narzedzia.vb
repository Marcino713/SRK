Public Module Narzedzia
    Public Const DATA_FORMAT As String = "d.MM.yyyy H:mm"
    Public Const ARG_TYP_RYSOWNIKA As String = "-tp"
    Public Const ARG_TYP_RYSOWNIKA_KLASYCZNY_GDI As String = "klgdi"
    Public Const ARG_TYP_RYSOWNIKA_KLASYCZNY_D2D As String = "kld2d"
    Friend WybranyTypRysownika As Pulpit.TypRysownika = Pulpit.TypRysownika.KlasycznyDirect2D

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

    Friend Sub UstawTypRysownika(argumenty As String())
        Dim typ As String = PobierzWartoscArgumentuWierszaPolecen(argumenty, ARG_TYP_RYSOWNIKA)

        Select Case typ?.ToLower
            Case ARG_TYP_RYSOWNIKA_KLASYCZNY_GDI
                WybranyTypRysownika = Pulpit.TypRysownika.KlasycznyGDI

            Case ARG_TYP_RYSOWNIKA_KLASYCZNY_D2D
                WybranyTypRysownika = Pulpit.TypRysownika.KlasycznyDirect2D

            Case Else
                WybranyTypRysownika = Pulpit.TypRysownika.KlasycznyDirect2D

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
End Module