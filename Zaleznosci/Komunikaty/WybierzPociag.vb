Public Class WybierzPociag  'k
    Inherits Komunikat

    Public Property NrPociagu As UInteger

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.WYBIERZ_POCIAG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New WybierzPociag
        kom.NrPociagu = br.ReadUInt32

        Return kom
    End Function
End Class