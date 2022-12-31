Public Class UsunietoPociag     's
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Stan As StanUsuwaniaPociagu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USUNIETO_POCIAG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UsunietoPociag
        kom.NrPociagu = br.ReadUInt32
        kom.Stan = CType(br.ReadByte, StanUsuwaniaPociagu)

        Return kom
    End Function
End Class

Public Enum StanUsuwaniaPociagu As Byte
    Usunieto
    PociagZajety
    BlednyNumer
End Enum