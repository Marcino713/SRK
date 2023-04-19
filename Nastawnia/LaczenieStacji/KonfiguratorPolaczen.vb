﻿Imports System.ComponentModel

Public Class wndKonfiguratorPolaczen
    Private polaczenia As Zaleznosci.PolaczeniaStacji
    Private ZaznaczonyPosterunek As Zaleznosci.LaczonyPlikStacji
    Private czyZamknacBezPytania As Boolean = False

    Public Sub New(polaczenia As Zaleznosci.PolaczeniaStacji)
        InitializeComponent()

        Me.polaczenia = polaczenia

        OdswiezPosterunkiListView()
        OdswiezPosterunkiComboBox()
    End Sub

    Private Sub wndKonfiguratorPolaczen_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not czyZamknacBezPytania Then
            e.Cancel = Not PrzetworzPorzuceniePliku()
        End If
    End Sub

    Private Sub cboPosterunek_SelectedIndexChanged() Handles cboPosterunek.SelectedIndexChanged
        ZaznaczonyPosterunek = Nothing

        If cboPosterunek.SelectedItem IsNot Nothing Then
            ZaznaczonyPosterunek = DirectCast(cboPosterunek.SelectedItem, ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)).Wartosc
        End If

        OdswiezPolaczenia()
    End Sub

    Private Sub lvPolaczenia_SelectedIndexChanged() Handles lvPolaczenia.SelectedIndexChanged
        btnUsun.Enabled = lvPolaczenia.SelectedItems IsNot Nothing AndAlso lvPolaczenia.SelectedItems.Count > 0
    End Sub

    Private Sub btnDodaj_Click() Handles btnDodaj.Click
        Dim wnd As New wndDodawaniePolaczenia(polaczenia)
        If wnd.ShowDialog = DialogResult.OK And wnd.DodawanePolaczenie IsNot Nothing Then
            polaczenia.LaczaneTory.Add(wnd.DodawanePolaczenie)
            If wnd.DodawanePolaczenie.Posterunek1 Is ZaznaczonyPosterunek Or wnd.DodawanePolaczenie.Posterunek2 Is ZaznaczonyPosterunek Then
                OdswiezPolaczenia()
            End If
        End If
    End Sub

    Private Sub btnUsun_Click() Handles btnUsun.Click
        Dim pol As Zaleznosci.LaczoneOdcinkiTorow = PobierzZaznaczonyElement(Of Zaleznosci.LaczoneOdcinkiTorow)(lvPolaczenia)
        If pol Is Nothing Then Exit Sub

        If ZadajPytanie($"Czy usunąć połączenie torów {PobierzNazweStacjiToru(pol.Posterunek1, pol.Tor1)} i {PobierzNazweStacjiToru(pol.Posterunek2, pol.Tor2)}?") = DialogResult.Yes Then
            polaczenia.LaczaneTory.Remove(pol)
            OdswiezPolaczenia()
        End If
    End Sub

    Private Sub btnZapisz_Click() Handles btnZapisz.Click
        ZapiszPolaczenia()
    End Sub

    Private Sub btnZamknij_Click() Handles btnZamknij.Click
        If PrzetworzPorzuceniePliku() Then
            czyZamknacBezPytania = True
            Close()
        End If
    End Sub

    Private Sub OdswiezPosterunkiListView()
        lvPliki.Items.Clear()

        For Each plik As Zaleznosci.LaczonyPlikStacji In polaczenia.LaczanePliki
            Dim uwagi As String = ""

            Select Case plik.Uwagi
                Case Zaleznosci.UwagiLaczanegoPlikuStacji.Zmodyfikowany
                    uwagi = "Plik zmodyfikowany"
                Case Zaleznosci.UwagiLaczanegoPlikuStacji.BrakPliku
                    uwagi = "Brak pliku"
                Case Zaleznosci.UwagiLaczanegoPlikuStacji.BrakiPolaczen
                    uwagi = "Braki połączeń"
            End Select

            lvPliki.Items.Add(New ListViewItem({plik.NazwaPosterunku, plik.NazwaPliku, uwagi}))
        Next
    End Sub

    Private Sub OdswiezPosterunkiComboBox()
        cboPosterunek.Items.Clear()

        For Each plik As Zaleznosci.LaczonyPlikStacji In polaczenia.LaczanePliki
            cboPosterunek.Items.Add(
                New ObiektComboBox(Of Zaleznosci.LaczonyPlikStacji)(plik, plik.NazwaPosterunku)
                )
        Next
    End Sub

    Private Sub OdswiezPolaczenia()
        lvPolaczenia.Items.Clear()
        If ZaznaczonyPosterunek Is Nothing Then Exit Sub

        Dim elementy As New List(Of PolaczoneToryNaLiscie)

        For Each pol As Zaleznosci.LaczoneOdcinkiTorow In polaczenia.LaczaneTory
            Dim post1 As Zaleznosci.LaczonyPlikStacji = Nothing
            Dim post2 As Zaleznosci.LaczonyPlikStacji = Nothing
            Dim tor1 As Zaleznosci.OdcinekToru = Nothing
            Dim tor2 As Zaleznosci.OdcinekToru = Nothing

            If pol.Posterunek1 Is ZaznaczonyPosterunek Then
                post1 = pol.Posterunek1
                post2 = pol.Posterunek2
                tor1 = pol.Tor1
                tor2 = pol.Tor2
            ElseIf pol.Posterunek2 Is ZaznaczonyPosterunek Then
                post1 = pol.Posterunek2
                post2 = pol.Posterunek1
                tor1 = pol.Tor2
                tor2 = pol.Tor1
            End If

            If post1 IsNot Nothing Then
                Dim uwagi As String = ""

                Select Case pol.Uwagi
                    Case Zaleznosci.UwagiLaczonegoOdcinkaTorow.BrakToru1
                        uwagi = "Brak toru " & PobierzNazweStacjiToru(pol.Posterunek1, pol.Tor1)
                    Case Zaleznosci.UwagiLaczonegoOdcinkaTorow.BrakToru2
                        uwagi = "Brak toru " & PobierzNazweStacjiToru(pol.Posterunek2, pol.Tor2)
                    Case Zaleznosci.UwagiLaczonegoOdcinkaTorow.BrakObuTorow
                        uwagi = "Brak obu torów"
                End Select

                Dim lvi As New ListViewItem({PobierzNazweStacjiToru(post1, tor1), PobierzNazweStacjiToru(post2, tor2), uwagi})
                lvi.Tag = pol
                elementy.Add(New PolaczoneToryNaLiscie(lvi, tor1.Nazwa))
            End If
        Next

        elementy = elementy.OrderBy(Function(el As PolaczoneToryNaLiscie) el.NazwaToru).ToList

        For Each element As PolaczoneToryNaLiscie In elementy
            lvPolaczenia.Items.Add(element.Element)
        Next

        lvPolaczenia_SelectedIndexChanged()
    End Sub

    Private Function PobierzNazweStacjiToru(posterunek As Zaleznosci.LaczonyPlikStacji, tor As Zaleznosci.OdcinekToru) As String
        Return $"{tor.Nazwa} ({posterunek.NazwaPosterunku})"
    End Function

    ''' <summary>
    ''' Pyta użytkownika o zapisanie pliku, ewentualnie zapisuje i zwraca wartość określającą, czy można zamknąć okno konfiguratora
    ''' </summary>
    Private Function PrzetworzPorzuceniePliku() As Boolean
        Dim wynik As DialogResult = ZadajPytanieTrzyodpowiedziowe("Zapisać plik?")

        If wynik = DialogResult.Yes Then Return ZapiszPolaczenia()

        If wynik = DialogResult.Cancel Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ZapiszPolaczenia() As Boolean
        Dim wynik As Boolean = polaczenia.Zapisz
        If wynik Then
            PokazKomunikat("Plik został zapisany.")
        Else
            PokazBlad("Wystąpił błąd podczas zapisu pliku.")
        End If
        Return wynik
    End Function

    Private Class PolaczoneToryNaLiscie
        Friend Element As ListViewItem
        Friend NazwaToru As String

        Friend Sub New(el As ListViewItem, nazwa As String)
            Element = el
            NazwaToru = nazwa
        End Sub
    End Class

End Class