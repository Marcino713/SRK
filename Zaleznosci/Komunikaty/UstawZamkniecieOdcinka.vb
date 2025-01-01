Public Class UstawZamkniecieOdcinka
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Zamkniety As Boolean

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_ZAMKNIECIE_ODCINKA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Zamkniety)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawZamkniecieOdcinka
        kom.Adres = br.ReadUInt16
        kom.Zamkniety = br.ReadBoolean

        Return kom
    End Function
End Class