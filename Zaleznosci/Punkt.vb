Imports System.Drawing

Public Class Punkt
    Public Property X As UShort
    Public Property Y As UShort

    Public Function Konwertuj() As Point
        Return New Point(X, Y)
    End Function
End Class