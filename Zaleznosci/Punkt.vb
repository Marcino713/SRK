Imports System.Drawing

Public Class Punkt
    Public Property X As UShort
    Public Property Y As UShort

    Public Sub New()
    End Sub

    Public Sub New(x As Integer, y As Integer)
        Me.X = CUShort(x)
        Me.Y = CUShort(y)
    End Sub

    Public Function Konwertuj() As Point
        Return New Point(X, Y)
    End Function
End Class