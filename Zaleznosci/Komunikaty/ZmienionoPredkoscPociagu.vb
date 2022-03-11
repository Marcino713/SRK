Public Class ZmienionoPredkoscPociagu   's
    Inherits ZmienionoPredkoscMaksymalna

    Public Property Zrodlo As ZrodloZmianyPredkosci

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_PREDKOSC_POCIAGU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        MyBase.Zapisz(bw)
        bw.Write(Zrodlo)
    End Sub

    Public Overloads Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoPredkoscPociagu
        Otworz(br, kom)
        kom.Zrodlo = CType(br.ReadByte, ZrodloZmianyPredkosci)

        Return kom
    End Function
End Class

Public Enum ZrodloZmianyPredkosci As Byte
    Program
    Urzadzenie
    LimitPredkosciOdcinka
End Enum