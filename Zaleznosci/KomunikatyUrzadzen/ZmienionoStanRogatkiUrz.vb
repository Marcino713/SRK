Public Class ZmienionoStanRogatkiUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.ZMIENIONO_STAN_ROGATKI
        End Get
    End Property

    Public Property Stan As StanRogatki

    Public Sub New()
    End Sub

    Public Sub New(br As BinaryReader)
        Stan = CType(Otworz(br), StanRogatki)
    End Sub
End Class

Public Enum StanRogatki
    Niezdefiniowany = 0
    Otwarta = 1
    Zamknieta = 2
End Enum