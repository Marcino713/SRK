Public Class UstawBlokadeZwrotnicy
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Zablokowana As Boolean

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_BLOKADE_ZWROTNICY
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Zablokowana)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawBlokadeZwrotnicy
        kom.Adres = br.ReadUInt16
        kom.Zablokowana = br.ReadBoolean

        Return kom
    End Function
End Class