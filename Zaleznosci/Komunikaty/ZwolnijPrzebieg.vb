Public Class ZwolnijPrzebieg
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Przebieg As ZwalnianyPrzebieg

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZWOLNIJ_PRZEBIEG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Przebieg)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZwolnijPrzebieg
        kom.Adres = br.ReadUInt16
        kom.Przebieg = CType(br.ReadByte, ZwalnianyPrzebieg)

        Return kom
    End Function
End Class

Public Enum ZwalnianyPrzebieg As Byte
    Zezwalajacy
    Manewrowy
End Enum