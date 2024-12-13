Public Class SygnalizatorOstrzegawczy
    Inherits SygnalizatorInformujacy

    Private Const NAZWA_TO As String = "To"

    Public Overrides Property Nazwa As String
        Get
            Return $"{NAZWA_TO}{SygnalizatorPowtarzany?.Nazwa}"
        End Get
        Set(value As String)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorOstrzegawczy)
    End Sub
End Class