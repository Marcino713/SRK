﻿Friend Class Migacz
    Implements Zaleznosci.IMigacz

    Private pulpit As PulpitSterowniczy
    Private kostkiMigajace As New HashSet(Of Zaleznosci.Kostka)
    Private kostkiPrzelaczane As New HashSet(Of Zaleznosci.Kostka)
    Private wlaczony As Boolean = True
    Private slock As New Object

    Private _stanMigania As Boolean = True
    Friend ReadOnly Property WysokiStanMigania As Boolean Implements Zaleznosci.IMigacz.WysokiStanMigania
        Get
            Return _stanMigania
        End Get
    End Property

    Friend Sub New(pulpit As PulpitSterowniczy)
        Me.pulpit = pulpit
    End Sub

    Public Sub UstawKostke(kostka As Zaleznosci.Kostka) Implements Zaleznosci.IMigacz.UstawKostke
        Dim miga As Boolean

        If kostka.CzyPrzelaczana() Then
            miga = True

            SyncLock slock
                kostkiPrzelaczane.Add(kostka)
            End SyncLock
        Else
            miga = kostka.CzyMiga()

            SyncLock slock
                kostkiPrzelaczane.Remove(kostka)
            End SyncLock
        End If

        If miga Then WlaczKostke(kostka) Else WylaczKostke(kostka)
    End Sub

    Public Sub PrzelaczStan() Implements Zaleznosci.IMigacz.PrzelaczStan
        _stanMigania = Not _stanMigania

        SyncLock slock
            For Each k As Zaleznosci.Kostka In kostkiPrzelaczane
                k.ZmianaMigniecia()
            Next
        End SyncLock
    End Sub

    Public Sub Wylacz() Implements Zaleznosci.IMigacz.Wylacz
        SyncLock slock
            wlaczony = False
        End SyncLock
    End Sub

    Private Sub WlaczKostke(kostka As Zaleznosci.Kostka)
        SyncLock slock
            If Not wlaczony Then Exit Sub

            Dim ile As Integer = kostkiMigajace.Count
            kostkiMigajace.Add(kostka)

            If ile = 0 And kostkiMigajace.Count = 1 Then
                pulpit.WlaczZegarMigania()
            End If
        End SyncLock
    End Sub

    Private Sub WylaczKostke(kostka As Zaleznosci.Kostka)
        SyncLock slock
            If Not wlaczony Then Exit Sub

            Dim ile As Integer = kostkiMigajace.Count
            kostkiMigajace.Remove(kostka)

            If ile = 1 And kostkiMigajace.Count = 0 Then
                pulpit.WylaczZegarMigania()
            End If
        End SyncLock
    End Sub
End Class