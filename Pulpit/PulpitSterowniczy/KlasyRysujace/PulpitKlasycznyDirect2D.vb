Friend Class PulpitKlasycznyDirect2D
    Inherits PulpitKlasyczny(Of IntPtr, IntPtr, IntPtr, IntPtr)

    Friend Shared ReadOnly OBIEKT As New Object

    Public Sub New()
        MyBase.New(New UrzadzenieRysujaceDirect2D())
    End Sub

    Friend Overrides Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics)
        SyncLock OBIEKT
            MyBase.Rysuj(ps, grp)
        End SyncLock
    End Sub
End Class