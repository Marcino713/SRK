Public Class UstawionoJasnoscLampyUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.USTAWIONO_JASNOSC_LAMPY
        End Get
    End Property

    Public Sub New(br As BinaryReader)
        Otworz(br)
    End Sub
End Class