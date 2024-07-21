Imports System.Numerics

Public Class DHZainicjalizowano
    Inherits Komunikat

    Public Property LiczbaB As BigInteger

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.DH_ZAINICJALIZOWANO
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        Dim b As Byte() = LiczbaB.ToByteArray
        bw.Write(CUShort(b.Length))
        bw.Write(b)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New DHZainicjalizowano
        Dim ile As Integer = br.ReadUInt16
        Dim b As Byte() = br.ReadBytes(ile)

        kom.LiczbaB = New BigInteger(b)

        Return kom
    End Function
End Class