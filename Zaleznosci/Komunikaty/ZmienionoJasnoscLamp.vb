Public Class ZmienionoJasnoscLamp   's
    Inherits Komunikat

    Public Property Adresy As UShort()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_JASNOSC_LAMP
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(CType(Adresy.Length, UShort))

        For i As Integer = 0 To Adresy.Length - 1
            bw.Write(Adresy(i))
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoJasnoscLamp
        Dim ile As Integer = br.ReadUInt16
        ReDim kom.Adresy(ile - 1)

        For i As Integer = 0 To ile - 1
            kom.Adresy(i) = br.ReadUInt16
        Next

        Return kom
    End Function
End Class