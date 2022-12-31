Public Class WybranoPociag  's
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Stan As StanWybranegoPociagu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.WYBRANO_POCIAG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New WybranoPociag
        kom.NrPociagu = br.ReadUInt32
        kom.Stan = CType(br.ReadByte, StanWybranegoPociagu)

        Return kom
    End Function
End Class

Public Enum StanWybranegoPociagu As Byte
    Wybrany
    Niesterowalny
    Zajety
    BlednyNumer
End Enum