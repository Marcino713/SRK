Public Class OknoPrzywracalne
    Inherits Form

    Private stan As FormWindowState

    Public Sub Przywroc()
        If WindowState = FormWindowState.Minimized Then WindowState = stan
        Focus()
    End Sub

    Private Sub OknoPrzywracalne_Resize() Handles Me.Resize
        If WindowState <> FormWindowState.Minimized Then
            stan = WindowState
        End If
    End Sub
End Class