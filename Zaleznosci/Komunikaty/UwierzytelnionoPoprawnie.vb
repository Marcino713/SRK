Public Class UwierzytelnionoPoprawnie
    Inherits Komunikat

    Public Property PredkoscMaksymalna As UShort    'maksymalna prędkość pociągów na całej sieci
    Public Property Posterunki As DanePosterunku()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.UWIERZYTELNIONO_POPRAWNIE
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        Dim post As DanePosterunku
        bw.Write(PredkoscMaksymalna)
        bw.Write(CUShort(Posterunki.Length))

        For i As Integer = 0 To Posterunki.Length - 1
            post = Posterunki(i)
            ZapiszTekst(bw, post.Nazwa)
            bw.Write(post.Adres)
            bw.Write(post.Stan)
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UwierzytelnionoPoprawnie
        kom.PredkoscMaksymalna = br.ReadUInt16

        Dim ile As Integer = br.ReadUInt16
        ReDim kom.Posterunki(ile - 1)

        For i As Integer = 0 To ile - 1
            Dim post As New DanePosterunku
            post.Nazwa = OdczytajTekst(br)
            post.Adres = br.ReadUInt16
            post.Stan = CType(br.ReadByte, StanPosterunku)
            kom.Posterunki(i) = post
        Next

        Return kom
    End Function
End Class

Public Class DanePosterunku
    Public Property Nazwa As String
    Public Property Adres As UShort
    Public Property Stan As StanPosterunku
End Class

Public Enum StanPosterunku As Byte
    Wolny
    Zajety
End Enum