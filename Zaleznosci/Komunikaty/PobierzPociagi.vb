Public Class PobierzPociagi  'k
    Inherits Komunikat

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.POBIERZ_POCIAGI
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Return New PobierzPociagi
    End Function
End Class
