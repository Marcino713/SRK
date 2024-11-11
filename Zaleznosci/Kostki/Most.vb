Public Class Most
    Inherits TorPodwojnyNiezalezny

    Private Const BLAD_NAZWA As String = "Most nie może posiadać nazwy."

    Public Sub New()
        MyBase.New(TypKostki.Most)
    End Sub

    <Obsolete>
    Public Overloads Property Nazwa As String
        Get
            Throw New NotSupportedException(BLAD_NAZWA)
        End Get
        Set(value As String)
            Throw New NotSupportedException(BLAD_NAZWA)
        End Set
    End Property
End Class