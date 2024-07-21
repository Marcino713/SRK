Imports System.IO
Imports System.Net
Imports System.Text

Public Class wndListaAdresowIP
    Public Property Ustawienia As Zaleznosci.AkceptowaneAdresy

    Public Sub New(adresy As Zaleznosci.AkceptowaneAdresy)
        InitializeComponent()
        Ustawienia = If(adresy IsNot Nothing, adresy, New Zaleznosci.AkceptowaneAdresy)
        PokazWartosci()
    End Sub

    Private Sub PokazWartosci()
        Select Case Ustawienia.Typ
            Case Zaleznosci.TypAkceptowaniaAdresow.Wszyscy
                rbWszyscy.Checked = True
            Case Zaleznosci.TypAkceptowaniaAdresow.OproczWybranych
                rbOproczWybranych.Checked = True
            Case Zaleznosci.TypAkceptowaniaAdresow.TylkoWybrani
                rbTylkoWybrani.Checked = True
        End Select

        Dim sb As New StringBuilder
        For Each adr As IPAddress In Ustawienia.Zbior
            sb.AppendLine(adr.ToString())
        Next
        txtAdresy.Text = sb.ToString()
    End Sub

    Private Function OdczytajWartosci() As Zaleznosci.AkceptowaneAdresy
        Dim ust As New Zaleznosci.AkceptowaneAdresy

        If rbWszyscy.Checked Then
            ust.Typ = Zaleznosci.TypAkceptowaniaAdresow.Wszyscy
        ElseIf rbOproczWybranych.Checked Then
            ust.Typ = Zaleznosci.TypAkceptowaniaAdresow.OproczWybranych
        Else
            ust.Typ = Zaleznosci.TypAkceptowaniaAdresow.TylkoWybrani
        End If

        Dim adresy As String() = txtAdresy.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        Dim adr As String
        Dim adrIp As IPAddress = Nothing

        For i As Integer = 0 To adresy.Length - 1
            adr = adresy(i)

            If Not String.IsNullOrWhiteSpace(adr) Then
                If IPAddress.TryParse(adr, adrIp) Then
                    ust.Zbior.Add(adrIp)
                Else
                    Wspolne.PokazBlad($"Adres w linii {i + 1} jest niepoprawny.")
                    Return Nothing
                End If
            End If
        Next

        Return ust
    End Function

    Private Sub btnZapisz_Click() Handles btnZapisz.Click
        If dlgZapisz.ShowDialog = DialogResult.OK Then
            Try
                File.WriteAllText(dlgZapisz.FileName, txtAdresy.Text)
            Catch
                Wspolne.PokazBlad("Wystąpił błąd podczas zapisu pliku z listą adresów.")
            End Try
        End If
    End Sub

    Private Sub btnOtworz_Click() Handles btnOtworz.Click
        If dlgOtworz.ShowDialog = DialogResult.OK Then
            Try
                txtAdresy.Text = File.ReadAllText(dlgOtworz.FileName)
            Catch
                Wspolne.PokazBlad("Wystąpił błąd podczas odczytu pliku z listą adresów.")
            End Try
        End If
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        Dim ust As Zaleznosci.AkceptowaneAdresy = OdczytajWartosci()
        If ust IsNot Nothing Then
            Ustawienia = ust
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub btnZamknij_Click() Handles btnZamknij.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class