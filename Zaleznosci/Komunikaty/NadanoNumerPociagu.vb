Public Class NadanoNumerPociagu 's
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Stan As StanNadaniaNumeru

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.NADANO_NUMER_POCIAGU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New NadanoNumerPociagu
        kom.NrPociagu = br.ReadUInt32
        kom.Stan = CType(br.ReadByte, StanNadaniaNumeru)

        Return kom
    End Function
End Class

Public Enum StanNadaniaNumeru As Byte
    Dobrze
    NrZajety
    BledneWspolrzedne
End Enum