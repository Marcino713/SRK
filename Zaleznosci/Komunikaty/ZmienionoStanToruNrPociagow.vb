Public Class ZmienionoStanToruNrPociagow
    Inherits Komunikat

    Public Property Tory As AktualizowanyKawalekToruNrPociagow()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_TORU_NR_POCIAGOW
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        Dim tor As AktualizowanyKawalekToruNrPociagow
        bw.Write(CUShort(Tory.Length))

        For i As Integer = 0 To Tory.Length - 1
            tor = Tory(i)
            bw.Write(tor.WspolrzedneKostki.X)
            bw.Write(tor.WspolrzedneKostki.Y)

            If tor.NumeryPociagow Is Nothing Then
                bw.Write(0US)
            Else
                bw.Write(CUShort(tor.NumeryPociagow.Length))

                For j As Integer = 0 To tor.NumeryPociagow.Length - 1
                    bw.Write(tor.NumeryPociagow(j))
                Next
            End If
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoStanToruNrPociagow
        Dim ile As Integer = br.ReadUInt16
        Dim ile2 As Integer
        ReDim kom.Tory(ile - 1)

        For i As Integer = 0 To ile - 1
            Dim tor As New AktualizowanyKawalekToruNrPociagow
            tor.WspolrzedneKostki = New Punkt
            tor.WspolrzedneKostki.X = br.ReadUInt16
            tor.WspolrzedneKostki.Y = br.ReadUInt16
            ile2 = br.ReadUInt16

            If ile2 > 0 Then
                ReDim tor.NumeryPociagow(ile2 - 1)

                For j As Integer = 0 To ile2 - 1
                    tor.NumeryPociagow(j) = br.ReadInt32
                Next
            End If

            kom.Tory(i) = tor
        Next

        Return kom
    End Function
End Class

Public Class AktualizowanyKawalekToruNrPociagow
    Public Property WspolrzedneKostki As Punkt
    Public Property NumeryPociagow As Integer()
End Class