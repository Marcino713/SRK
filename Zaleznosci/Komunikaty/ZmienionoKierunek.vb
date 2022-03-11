Public Class ZmienionoKierunek  's
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Kierunek As StanKierunku

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_KIERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Kierunek)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoKierunek
        kom.Adres = br.ReadUInt16
        kom.Kierunek = CType(br.ReadByte, StanKierunku)

        Return kom
    End Function
End Class

Public Enum StanKierunku As Byte
    Zasadniczy
    Przeciwny
    Niezdefiniowany
End Enum