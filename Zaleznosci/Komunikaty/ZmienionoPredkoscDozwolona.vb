Public Class ZmienionoPredkoscDozwolona    's
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Predkosc As UShort

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_PREDKOSC_DOZWOLONA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Predkosc)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoPredkoscDozwolona
        kom.NrPociagu = br.ReadUInt32
        kom.Predkosc = br.ReadUInt16
        Return kom
    End Function
End Class