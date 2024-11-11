Public Class ZmienionoStanToru
    Inherits Komunikat

    Public Property Tory As AktualizowanyKawalekToru()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_TORU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        Dim tor As AktualizowanyKawalekToru
        bw.Write(CUShort(Tory.Length))

        For i As Integer = 0 To Tory.Length - 1
            tor = Tory(i)
            bw.Write(tor.WspolrzedneKostki.X)
            bw.Write(tor.WspolrzedneKostki.Y)
            bw.Write(tor.Polozenie)
            bw.Write(tor.Zajetosc)
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoStanToru
        Dim ile As Integer = br.ReadUInt16
        ReDim kom.Tory(ile - 1)

        For i As Integer = 0 To ile - 1
            Dim tor As New AktualizowanyKawalekToru
            tor.WspolrzedneKostki = New Punkt
            tor.WspolrzedneKostki.X = br.ReadUInt16
            tor.WspolrzedneKostki.Y = br.ReadUInt16
            tor.Polozenie = CType(br.ReadByte, PolozenieToru)
            tor.Zajetosc = CType(br.ReadByte, ZajetoscToru)

            kom.Tory(i) = tor
        Next

        Return kom
    End Function
End Class

Public Class AktualizowanyKawalekToru
    Public Property WspolrzedneKostki As Punkt
    Public Property Polozenie As PolozenieToru
    Public Property Zajetosc As ZajetoscToru
End Class

Public Enum PolozenieToru As Byte
    TorPierwszy = 1
    TorDrugi = 2
End Enum