Public Class UstawionoTrybSamoczynnySygnalizatora
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As StanTrybuSamoczynnegoSygnalizatora

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAWIONO_TRYB_SAMOCZYNNY_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawionoTrybSamoczynnySygnalizatora
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanTrybuSamoczynnegoSygnalizatora)

        Return kom
    End Function
End Class

Public Enum StanTrybuSamoczynnegoSygnalizatora As Byte
    TrybSamoczynny
    TrybPolsamoczynny
    BlednyAdres
    BlednySygnalizator
End Enum