Public Class UstawStanPrzejazdu
    Inherits Komunikat

    Public Property Numer As UShort
    Public Property Stan As UstawianyStanPrzejazdu

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_STAN_PRZEJAZDU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Numer)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawStanPrzejazdu
        kom.Numer = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, UstawianyStanPrzejazdu)

        Return kom
    End Function
End Class

Public Enum UstawianyStanPrzejazdu As Byte
    Zamkniety
    Otwarty
End Enum