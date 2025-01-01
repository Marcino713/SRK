Public Class UstawBlokadeSygnalizatora
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Zablokowany As Boolean

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_BLOKADE_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Zablokowany)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawBlokadeSygnalizatora
        kom.Adres = br.ReadUInt16
        kom.Zablokowany = br.ReadBoolean

        Return kom
    End Function
End Class