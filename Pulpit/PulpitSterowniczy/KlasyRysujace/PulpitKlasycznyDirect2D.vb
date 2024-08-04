Friend Class PulpitKlasycznyDirect2D
    Inherits PulpitKlasyczny(Of IntPtr, IntPtr, IntPtr, IntPtr)

    Public Sub New()
        MyBase.New(New UrzadzenieRysujaceDirect2D())
    End Sub

    Friend Overrides Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics)
        MyBase.Rysuj(ps, grp)
    End Sub
End Class