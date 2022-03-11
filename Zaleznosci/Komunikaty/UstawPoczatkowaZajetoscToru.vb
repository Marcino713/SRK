Public Class UstawPoczatkowaZajetoscToru  'k
    Inherits Komunikat

    Public Property LiczbaOsi As UShort
    Public Property NrPociagu As UInteger
    Public Property PojazdSterowalny As Boolean
    Public Property WspolrzedneKostki As Punkt

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_POCZATKOWA_ZAJETOSC_TORU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(LiczbaOsi)
        bw.Write(NrPociagu)
        bw.Write(PojazdSterowalny)
        bw.Write(WspolrzedneKostki.X)
        bw.Write(WspolrzedneKostki.Y)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawPoczatkowaZajetoscToru
        kom.LiczbaOsi = br.ReadUInt16
        kom.NrPociagu = br.ReadUInt32
        kom.PojazdSterowalny = br.ReadBoolean
        kom.WspolrzedneKostki = New Punkt
        kom.WspolrzedneKostki.X = br.ReadUInt16
        kom.WspolrzedneKostki.Y = br.ReadUInt16

        Return kom
    End Function
End Class