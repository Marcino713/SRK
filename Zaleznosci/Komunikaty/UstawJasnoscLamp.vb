Public Class UstawJasnoscLamp   'k
    Inherits Komunikat

    Public Property Jasnosci As JasnoscLampy()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_JASNOSC_LAMP
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(CType(Jasnosci.Length, UShort))

        For i As Integer = 0 To Jasnosci.Length - 1
            bw.Write(Jasnosci(i).Adres)
            bw.Write(Jasnosci(i).Jasnosc)
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawJasnoscLamp
        Dim ile As Integer = br.ReadUInt16
        ReDim kom.Jasnosci(ile - 1)

        For i As Integer = 0 To ile - 1
            kom.Jasnosci(i) = New JasnoscLampy
            kom.Jasnosci(i).Adres = br.ReadUInt16
            kom.Jasnosci(i).Jasnosc = br.ReadByte
        Next

        Return kom
    End Function
End Class

Public Class JasnoscLampy
    Public Property Adres As UShort
    Public Property Jasnosc As Byte
End Class