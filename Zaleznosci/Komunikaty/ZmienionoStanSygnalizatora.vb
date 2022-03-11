Public Class ZmienionoStanSygnalizatora 's
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As ZmienionyStanSygnalizatora

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoStanSygnalizatora
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, ZmienionyStanSygnalizatora)

        Return kom
    End Function
End Class

Public Enum ZmienionyStanSygnalizatora As Byte
    Zezwalajacy
    Manewrowy
    Zastepczy
    BrakWyjazdu
End Enum