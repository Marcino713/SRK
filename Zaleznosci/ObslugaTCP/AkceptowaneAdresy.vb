Imports System.Net

Public Class AkceptowaneAdresy
    Public Typ As TypAkceptowaniaAdresow = TypAkceptowaniaAdresow.Wszyscy
    Public Zbior As New HashSet(Of IPAddress)

    Public Sub New()
    End Sub

    Public Sub New(el As AkceptowaneAdresy)
        Typ = el.Typ
        If el.Zbior IsNot Nothing Then
            Zbior = New HashSet(Of IPAddress)(el.Zbior)
        End If
    End Sub
End Class

Public Enum TypAkceptowaniaAdresow
    Wszyscy
    OproczWybranych
    TylkoWybrani
End Enum