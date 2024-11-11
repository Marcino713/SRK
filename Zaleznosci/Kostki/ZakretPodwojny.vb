Public Class ZakretPodwojny
    Inherits TorPodwojnyNiezalezny
    Implements IZakret

    Private Const BLAD_NAZWA As String = "Podwójny tor ukośny nie może posiadać nazwy."
    Private Const BLAD As String = "Własność nie może być użyta w podwójnym torze ukośnym."

    Public Property PrzytnijZakret As PrzycinanieZakretu Implements IZakret.PrzytnijZakret
    Public Property PrzytnijZakretDrugi As PrzycinanieZakretu

    <Obsolete>
    Public Overloads Property Nazwa As String
        Get
            Throw New NotSupportedException(BLAD_NAZWA)
        End Get
        Set(value As String)
            Throw New NotSupportedException(BLAD_NAZWA)
        End Set
    End Property

    <Obsolete>
    Public Overrides Property RysowanieDodatkowychTrojkatow As DodatkoweTrojkatyTor
        Get
            Throw New NotSupportedException(BLAD)
        End Get
        Set(value As DodatkoweTrojkatyTor)
            Throw New NotSupportedException(BLAD)
        End Set
    End Property

    <Obsolete>
    Public Overrides Property RysowanieDodatkowychTrojkatowDrugi As DodatkoweTrojkatyTor
        Get
            Throw New NotSupportedException(BLAD)
        End Get
        Set(value As DodatkoweTrojkatyTor)
            Throw New NotSupportedException(BLAD)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.ZakretPodwojny)
    End Sub
End Class