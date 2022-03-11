Public Class ZakonczonoSesjeKlienta   's
    Inherits Komunikat

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZAKONCZONO_SESJE_KLIENTA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZakonczonoSesjeKlienta
        Return kom
    End Function
End Class