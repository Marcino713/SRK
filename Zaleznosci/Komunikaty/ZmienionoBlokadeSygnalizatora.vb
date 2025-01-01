Public Class ZmienionoBlokadeSygnalizatora
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As StanZmienionejBlokadySygnalizatora

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_BLOKADE_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoBlokadeSygnalizatora
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanZmienionejBlokadySygnalizatora)

        Return kom
    End Function
End Class

Public Enum StanZmienionejBlokadySygnalizatora As Byte
    Zablokowany
    Odblokowany
    BlednyAdres
    SygnalizatorNieblokowalny
End Enum