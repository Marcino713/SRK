Public Class ZmienionoPredkoscPociagu
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Predkosc As Short
    Public Property Zrodlo As ZrodloZmianyPredkosci
    Public Property Stan As StanZmianyPredkosci

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_PREDKOSC_POCIAGU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Predkosc)
        bw.Write(Zrodlo)
        bw.Write(Stan)
    End Sub

    Public Overloads Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoPredkoscPociagu
        kom.NrPociagu = br.ReadUInt32
        kom.Predkosc = br.ReadInt16
        kom.Zrodlo = CType(br.ReadByte, ZrodloZmianyPredkosci)
        kom.Stan = CType(br.ReadByte, StanZmianyPredkosci)

        Return kom
    End Function
End Class

Public Enum ZrodloZmianyPredkosci As Byte
    Program
    Urzadzenie
    LimitPredkosciOdcinka
End Enum

Public Enum StanZmianyPredkosci As Byte
    Zmieniono
    PociagNiesterowanyPrzezPosterunek
    BlednyNumer
End Enum