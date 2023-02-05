Public Class PotwierdzKierunek
    Inherits Komunikat

    Public Property Adres As UShort

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.POTWIERDZ_KIERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New PotwierdzKierunek
        kom.Adres = br.ReadUInt16

        Return kom
    End Function
End Class