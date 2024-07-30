Public Class Punkt
    Implements IObiektPunktowy(Of UShort)

    Public Property X As UShort Implements IObiektPunktowy(Of UShort).X
    Public Property Y As UShort Implements IObiektPunktowy(Of UShort).Y

    Public Sub New()
    End Sub

    Public Sub New(x As Integer, y As Integer)
        Me.X = CUShort(x)
        Me.Y = CUShort(y)
    End Sub
End Class