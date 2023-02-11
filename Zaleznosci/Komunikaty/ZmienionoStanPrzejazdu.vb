Public Class ZmienionoStanPrzejazdu
    Inherits Komunikat

    Public Property Numer As UShort
    Public Property Stan As StanPrzejazduKolejowego
    Public Property Awaria As Boolean
    Public Property Blad As BladZmianyStanuPrzejazdu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_PRZEJAZDU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Numer)
        bw.Write(CByte(Stan))
        bw.Write(Awaria)
        bw.Write(Blad)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoStanPrzejazdu
        kom.Numer = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanPrzejazduKolejowego)
        kom.Awaria = br.ReadBoolean
        kom.Blad = CType(br.ReadByte, BladZmianyStanuPrzejazdu)

        Return kom
    End Function
End Class

Public Enum BladZmianyStanuPrzejazdu As Byte
    Brak
    BlednyNumer
End Enum