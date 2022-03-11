Public Class ZazadanoUstawieniaKierunku 's
    Inherits UstawKierunek

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZAZADANO_USTAWIENIA_KIERUNKU
        End Get
    End Property
End Class