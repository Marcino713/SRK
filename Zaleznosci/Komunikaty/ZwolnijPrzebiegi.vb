Public Class ZwolnijPrzebiegi  'k
    Inherits Komunikat

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZWOLNIJ_PRZEBIEGI
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZwolnijPrzebiegi
        Return kom
    End Function
End Class