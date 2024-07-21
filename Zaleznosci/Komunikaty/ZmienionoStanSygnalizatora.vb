Public Class ZmienionoStanSygnalizatora
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As StanSygnalizatora

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoStanSygnalizatora
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanSygnalizatora)

        Return kom
    End Function
End Class