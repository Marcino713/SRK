Public Class ZmienionoNazwePociagu
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Nazwa As String

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_NAZWE_POCIAGU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        ZapiszTekst(bw, Nazwa)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoNazwePociagu
        kom.NrPociagu = br.ReadUInt32
        kom.Nazwa = OdczytajTekst(br)

        Return kom
    End Function
End Class