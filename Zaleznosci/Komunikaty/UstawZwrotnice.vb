Public Class UstawZwrotnice  'k
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Ustawienie As UstawienieRozjazduEnum

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_ZWROTNICE
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(CType(Ustawienie, Byte))
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawZwrotnice
        kom.Adres = br.ReadUInt16
        kom.Ustawienie = CType(br.ReadByte, UstawienieRozjazduEnum)

        Return kom
    End Function
End Class