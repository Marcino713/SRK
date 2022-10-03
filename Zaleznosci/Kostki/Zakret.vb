Public Class Zakret
    Inherits Tor
    Implements IZakret

    Private Const BLAD As String = "Własność nie może być użyta w torze ukośnym."

    Public Property PrzytnijZakret As PrzycinanieZakretu Implements IZakret.PrzytnijZakret

    <Obsolete>
    Public Overrides Property RysowanieDodatkowychTrojkatow As DodatkoweTrojkatyTor
        Get
            Throw New NotSupportedException(BLAD)
        End Get
        Set(value As DodatkoweTrojkatyTor)
            Throw New NotSupportedException(BLAD)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.Zakret)
    End Sub
End Class