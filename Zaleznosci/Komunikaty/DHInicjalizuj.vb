Imports System.Numerics

Public Class DHInicjalizuj  'k
    Inherits Komunikat

    Public Property LiczbaA As BigInteger
    Public Property LiczbaG As Integer
    Public Property LiczbaP As BigInteger

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.DH_INICJALIZUJ
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        Dim b As Byte()

        b = LiczbaA.ToByteArray()
        bw.Write(CType(b.Length, UShort))
        bw.Write(b)

        bw.Write(LiczbaG)

        b = LiczbaP.ToByteArray()
        bw.Write(CType(b.Length, UShort))
        bw.Write(b)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New DHInicjalizuj
        Dim ile As Integer
        Dim b As Byte()

        ile = br.ReadUInt16
        b = br.ReadBytes(ile)
        kom.LiczbaA = New BigInteger(b)

        kom.LiczbaG = br.ReadInt32

        ile = br.ReadUInt16
        b = br.ReadBytes(ile)
        kom.LiczbaP = New BigInteger(b)

        Return kom
    End Function
End Class