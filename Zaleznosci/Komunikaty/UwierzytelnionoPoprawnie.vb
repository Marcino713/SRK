Public Class UwierzytelnionoPoprawnie   's
    Inherits Komunikat

    Public Property Posterunki As DanePosterunku()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.UWIERZYTELNIONO_POPRAWNIE
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(CType(Posterunki.Length, UShort))

        For i As Integer = 0 To Posterunki.Length - 1
            ZapiszTekst(bw, Posterunki(i).Nazwa)
            bw.Write(Posterunki(i).Adres)
            bw.Write(Posterunki(i).Stan)
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UwierzytelnionoPoprawnie
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