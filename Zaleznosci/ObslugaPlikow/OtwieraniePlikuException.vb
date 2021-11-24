Public Class OtwieraniePlikuException
    Inherits Exception

    Public Sub New(komunikat As String)
        MyBase.New(komunikat)
    End Sub
End Class