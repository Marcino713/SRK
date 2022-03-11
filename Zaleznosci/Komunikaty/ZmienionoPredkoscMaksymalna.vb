Public Class ZmienionoPredkoscMaksymalna    's
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Predkosc As Short

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_PREDKOSC_MAKSYMALNA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Predkosc)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoPredkoscMaksymalna
        Return Otworz(br, kom)
    End Function

    Public Shared Function Otworz(br As BinaryReader, kom As ZmienionoPredkoscMaksymalna) As Komunikat
        kom.NrPociagu = br.ReadUInt32
        kom.Predkosc = br.ReadInt16
        Return kom
    End Function
End Class