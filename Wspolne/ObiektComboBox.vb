Public Class ObiektComboBox(Of T)
    Public Property Wartosc As T
    Public Property Tekst As String

    Public Sub New(el As T, napis As String)
        Wartosc = el
        Tekst = napis
    End Sub

    Public Overrides Function ToString() As String
        Return Tekst
    End Function
End Class