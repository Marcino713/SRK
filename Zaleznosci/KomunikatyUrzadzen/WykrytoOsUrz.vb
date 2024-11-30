Public Class WykrytoOsUrz
    Inherits KomunikatUrzadzenia

    Public Overrides ReadOnly Property Typ As Byte
        Get
            Return TypKomunikatuUrzadzenia.WYKRYTO_OS
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(br As BinaryReader)
        Otworz(br)
    End Sub
End Class