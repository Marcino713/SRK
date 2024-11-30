Friend Class Migacz

    Private _stan As Boolean = False
    Public Property Stan As Boolean
        Get
            Return _stan
        End Get
        Set(value As Boolean)
            If value <> _stan Then
                _stan = value
                RaiseEvent ZmienionoStan()
            End If
        End Set
    End Property

    Public Event ZmienionoStan()
End Class