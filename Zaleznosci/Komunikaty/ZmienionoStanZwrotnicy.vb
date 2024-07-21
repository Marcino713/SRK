Public Class ZmienionoStanZwrotnicy
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Rozprucie As Boolean
    Public Property Stan As StanRozjazdu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_ZWROTNICY
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Rozprucie)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoStanZwrotnicy
        kom.Adres = br.ReadUInt16
        kom.Rozprucie = br.ReadBoolean
        kom.Stan = CType(br.ReadByte, StanRozjazdu)

        Return kom
    End Function
End Class