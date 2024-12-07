Public Class UwierzytelnijSie
    Inherits Komunikat

    Public Property Haslo As String
    Public Property TrybObserwatora As Boolean

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.UWIERZYTELNIJ_SIE
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        ZapiszTekst(bw, Haslo)
        bw.Write(TrybObserwatora)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UwierzytelnijSie
        kom.Haslo = OdczytajTekst(br)
        kom.TrybObserwatora = br.ReadBoolean

        Return kom
    End Function
End Class