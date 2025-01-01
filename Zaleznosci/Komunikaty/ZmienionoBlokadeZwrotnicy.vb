Public Class ZmienionoBlokadeZwrotnicy
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As StanZmienionejBlokadyZwrotnicy

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_BLOKADE_ZWROTNICY
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoBlokadeZwrotnicy
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanZmienionejBlokadyZwrotnicy)

        Return kom
    End Function
End Class

Public Enum StanZmienionejBlokadyZwrotnicy As Byte
    Zablokowana
    Odblokowana
    BlednyAdres
End Enum