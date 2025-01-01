Public Class ZmienionoZamkniecieOdcinka
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As StanZamykanegoOdcinka

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_ZAMKNIECIE_ODCINKA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoZamkniecieOdcinka
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanZamykanegoOdcinka)

        Return kom
    End Function
End Class

Public Enum StanZamykanegoOdcinka As Byte
    Zamkniety
    Otwarty
    BlednyAdres
    OdcinekZajety
End Enum