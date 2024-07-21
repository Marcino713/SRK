Public Class WybierzPosterunek
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Tryb As TrybPracyPosterunku

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.WYBIERZ_POSTERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Tryb)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New WybierzPosterunek
        kom.Adres = br.ReadUInt16
        kom.Tryb = CType(br.ReadByte, TrybPracyPosterunku)

        Return kom
    End Function
End Class

Public Enum TrybPracyPosterunku As Byte
    Polsamoczynny
    Samoczynny
End Enum