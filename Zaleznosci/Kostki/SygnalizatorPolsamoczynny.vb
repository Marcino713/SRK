Public Class SygnalizatorPolsamoczynny
    Inherits Sygnalizator
    Public Property CzyManewrowy As Boolean
    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorPolsamoczynny)
    End Sub
End Class