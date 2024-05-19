Public Class ZmienionoStanZwrotnicyUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.ZMIENIONO_STAN_ZWROTNICY
        End Get
    End Property

    Public Property Stan As StanRozjazdu

    Public Sub New(br As BinaryReader)
        Stan = CType(Otworz(br), StanRozjazdu)
    End Sub
End Class