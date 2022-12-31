Public Class DodanoPociag 's
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Stan As StanNadaniaNumeruPociagu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.DODANO_POCIAG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New DodanoPociag
        kom.NrPociagu = br.ReadUInt32
        kom.Stan = CType(br.ReadByte, StanNadaniaNumeruPociagu)

        Return kom
    End Function
End Class

Public Enum StanNadaniaNumeruPociagu As Byte
    Dobrze
    NrZajety
    BledneWspolrzedne
End Enum