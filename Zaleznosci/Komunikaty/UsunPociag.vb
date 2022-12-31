Public Class UsunPociag     'k
    Inherits Komunikat

    Public Property NrPociagu As UInteger

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USUN_POCIAG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UsunPociag
        kom.NrPociagu = br.ReadUInt32

        Return kom
    End Function
End Class