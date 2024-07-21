Public Class DodajPociag
    Inherits Komunikat

    Public Property LiczbaOsi As UShort
    Public Property NrPociagu As UInteger
    Public Property Nazwa As String
    Public Property PredkoscMaksymalna As UShort
    Public Property PojazdSterowalny As Boolean
    Public Property WspolrzedneKostki As Punkt

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.DODAJ_POCIAG
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(LiczbaOsi)
        bw.Write(NrPociagu)
        ZapiszTekst(bw, Nazwa)
        bw.Write(PredkoscMaksymalna)
        bw.Write(PojazdSterowalny)
        bw.Write(WspolrzedneKostki.X)
        bw.Write(WspolrzedneKostki.Y)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New DodajPociag
        kom.LiczbaOsi = br.ReadUInt16
        kom.NrPociagu = br.ReadUInt32
        kom.Nazwa = OdczytajTekst(br)
        kom.PredkoscMaksymalna = br.ReadUInt16
        kom.PojazdSterowalny = br.ReadBoolean
        kom.WspolrzedneKostki = New Punkt
        kom.WspolrzedneKostki.X = br.ReadUInt16
        kom.WspolrzedneKostki.Y = br.ReadUInt16

        Return kom
    End Function
End Class