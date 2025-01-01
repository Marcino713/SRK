Friend Class ObslugaZamkniecOdcinkow
    Private Shared ReadOnly CZAS_CZYSZCZENIA_ZAMKNIECIA As New TimeSpan(0, 5, 0)

    Private ZleconeZamknieciaOdcinkow As New Dictionary(Of Integer, DaneZamykaniaOdcinka)

    Public Sub UstawZamkniecieOdcinka(klient As Zaleznosci.KlientTCP, kom As Zaleznosci.UstawZamkniecieOdcinka, wspolrzedne As Zaleznosci.PunktCalkowity)
        klient?.WyslijUstawZamkniecieOdcinka(kom, Sub(numerKom)
                                                      SyncLock Me
                                                          CzyscZamkniecia()
                                                          ZleconeZamknieciaOdcinkow.Add(numerKom, New DaneZamykaniaOdcinka() With {.Wspolrzedne = wspolrzedne, .Czas = Now})
                                                      End SyncLock
                                                  End Sub)
    End Sub

    Public Function PrzetworzZamkniecieOdcinka(kom As Zaleznosci.ZmienionoZamkniecieOdcinka, ps As Pulpit.PulpitSterowniczy) As Boolean
        Dim dane As DaneZamykaniaOdcinka = Nothing
        Dim wynik As Boolean = False

        SyncLock Me
            If ZleconeZamknieciaOdcinkow.TryGetValue(kom.Numer, dane) Then
                If kom.Stan = Zaleznosci.StanZamykanegoOdcinka.Zamkniety Then
                    ps.DodajTabliczkeZamkniecia(kom.Adres, dane.Wspolrzedne)
                    wynik = True
                ElseIf kom.Stan = Zaleznosci.StanZamykanegoOdcinka.Otwarty Then
                    ps.UsunTabliczkeZamkniecia(kom.Adres)
                    wynik = True
                End If

                ZleconeZamknieciaOdcinkow.Remove(kom.Numer)
            End If

            CzyscZamkniecia()
        End SyncLock

        Return wynik
    End Function

    Private Sub CzyscZamkniecia()
        Dim czas As Date = Now - CZAS_CZYSZCZENIA_ZAMKNIECIA
        Dim lista As New List(Of Integer)

        For Each kv As KeyValuePair(Of Integer, DaneZamykaniaOdcinka) In ZleconeZamknieciaOdcinkow
            If kv.Value.Czas < czas Then lista.Add(kv.Key)
        Next

        If lista.Count > 0 Then
            For i As Integer = 0 To lista.Count - 1
                ZleconeZamknieciaOdcinkow.Remove(lista(i))
            Next
        End If
    End Sub

    Private Class DaneZamykaniaOdcinka
        Public Wspolrzedne As Zaleznosci.PunktCalkowity
        Public Czas As Date
    End Class
End Class