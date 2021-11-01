Public Class wndNastawnia
    Private Sub wndNastawnia_Load() Handles MyBase.Load
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub

    Private Sub mnuKonfiguratorStacji_Click() Handles mnuKonfiguratorStacji.Click
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub
End Class