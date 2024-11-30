Friend Class PulpitKlasycznyGDI
    Inherits PulpitKlasyczny(Of Pen, Brush, Drawing2D.Matrix, Font)

    Friend Sub New()
        MyBase.New(New UrzadzenieRysujaceGDI)
    End Sub

    Friend Overrides Sub Rysuj(ps As PulpitSterowniczy, grp As Graphics)
        If grp Is Nothing Then Exit Sub

        SyncLock Me
            MyBase.Rysuj(ps, grp)
        End SyncLock
    End Sub
End Class