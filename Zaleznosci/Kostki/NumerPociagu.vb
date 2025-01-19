Public Class NumerPociagu
    Inherits Tor

    Private Const LICZBA_ZMIAN_MIGACZA As Integer = 2

    Private numery As Integer()
    Private ix As Integer
    Private zmiany As Integer

    Public Sub New()
        MyBase.New(TypKostki.NumerPociagu)
    End Sub

    Public Overrides Function CzyPrzelaczana() As Boolean
        SyncLock Me
            Return numery IsNot Nothing AndAlso numery.Length > 1
        End SyncLock
    End Function

    Public Overrides Sub ZmianaMigniecia()
        SyncLock Me
            If numery IsNot Nothing Then
                zmiany += 1

                If zmiany >= LICZBA_ZMIAN_MIGACZA Then
                    zmiany = 0
                    ix += 1
                    If ix >= numery.Length Then ix = 0
                End If
            End If
        End SyncLock
    End Sub

    Public Sub UstawNumery(nr As Integer())
        SyncLock Me
            numery = nr
            ix = 0
            zmiany = 0
        End SyncLock

        Migacz?.UstawKostke(Me)
    End Sub

    Public Function PobierzNumer() As Integer
        SyncLock Me
            If numery Is Nothing Then
                Return 0
            Else
                Return numery(ix)
            End If
        End SyncLock
    End Function
End Class