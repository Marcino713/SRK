Public Class UstawZwrotnice  'k
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Ustawienie As UstawianyStanRozjazdu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_ZWROTNICE
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Ustawienie)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawZwrotnice
        kom.Adres = br.ReadUInt16
        kom.Ustawienie = CType(br.ReadByte, UstawianyStanRozjazdu)

        Return kom
    End Function
End Class

Public Enum UstawianyStanRozjazdu As Byte
    Wprost
    Bok
    KasujRozprucie
End Enum