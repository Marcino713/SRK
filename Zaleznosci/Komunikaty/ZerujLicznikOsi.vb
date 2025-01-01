Public Class ZerujLicznikOsi
    Inherits Komunikat

    Public Property AdresOdcinka As UShort

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZERUJ_LICZNIK_OSI
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(AdresOdcinka)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZerujLicznikOsi
        kom.AdresOdcinka = br.ReadUInt16

        Return kom
    End Function
End Class