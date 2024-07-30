Public Class PunktCalkowity
    Implements IObiektPunktowy(Of Integer)

    Public Property X As Integer Implements IObiektPunktowy(Of Integer).X
    Public Property Y As Integer Implements IObiektPunktowy(Of Integer).Y

    Public Sub New()
    End Sub

    Public Sub New(x As Integer, y As Integer)
        Me.X = x
        Me.Y = y
    End Sub

    Public Sub New(x As Single, y As Single)
        Me.X = CInt(x)
        Me.Y = CInt(y)
    End Sub

    Public Shared Operator =(p1 As PunktCalkowity, p2 As PunktCalkowity) As Boolean
        Return p1.X = p2.X AndAlso p1.Y = p2.Y
    End Operator

    Public Shared Operator <>(p1 As PunktCalkowity, p2 As PunktCalkowity) As Boolean
        Return Not p1 = p2
    End Operator
End Class