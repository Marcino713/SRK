Public Class UstawKierunek  'k
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Kierunek As KierunekWlaczanyEnum

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_KIERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(CType(Kierunek, Byte))
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawKierunek
        kom.Adres = br.ReadUInt16
        kom.Kierunek = CType(br.ReadByte, KierunekWlaczanyEnum)

        Return kom
    End Function
End Class