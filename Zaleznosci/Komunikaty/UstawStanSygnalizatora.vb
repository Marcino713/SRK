Public Class UstawStanSygnalizatora
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As UstawianyStanSygnalizatora

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_STAN_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawStanSygnalizatora
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, UstawianyStanSygnalizatora)

        Return kom
    End Function
End Class

Public Enum UstawianyStanSygnalizatora As Byte
    Zezwalajacy
    Manewrowy
    Zastepczy
End Enum