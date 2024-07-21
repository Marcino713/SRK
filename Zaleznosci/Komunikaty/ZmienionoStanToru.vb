Public Class ZmienionoStanToru
    Inherits Komunikat

    Public Property Tory As AktualizowanyKawalekToru()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_STAN_TORU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(CUShort(Tory.Length))

        For i As Integer = 0 To Tory.Length - 1
            bw.Write(Tory(i).WspolrzedneKostki.X)
            bw.Write(Tory(i).WspolrzedneKostki.Y)
            bw.Write(Tory(i).Polozenie)
            bw.Write(Tory(i).Zajetosc)
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
    TorGlowny
    RozjazdWBok
End Enum