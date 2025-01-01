Public Class UstawTrybSamoczynnySygnalizatora
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property TrybSamoczynny As Boolean

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_TRYB_SAMOCZYNNY_SYGNALIZATORA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(TrybSamoczynny)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawTrybSamoczynnySygnalizatora
        kom.Adres = br.ReadUInt16
        kom.TrybSamoczynny = br.ReadBoolean

        Return kom
    End Function
End Class