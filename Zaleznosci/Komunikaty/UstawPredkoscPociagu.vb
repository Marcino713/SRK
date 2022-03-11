Public Class UstawPredkoscPociagu   'k
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Predkosc As Short

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_PREDKOSC_POCIAGU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Predkosc)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawPredkoscPociagu
        kom.NrPociagu = br.ReadUInt32
        kom.Predkosc = br.ReadInt16

        Return kom
    End Function
End Class