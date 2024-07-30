Public Class UstawJasnoscLamp
    Inherits Komunikat

    Public Property Jasnosci As JasnoscLampy()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_JASNOSC_LAMP
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(CUShort(Jasnosci.Length))
        Dim jasnosc As JasnoscLampy

        For i As Integer = 0 To Jasnosci.Length - 1
            jasnosc = Jasnosci(i)
            bw.Write(jasnosc.Adres)
            bw.Write(jasnosc.Jasnosc)
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawJasnoscLamp
        Dim ile As Integer = br.ReadUInt16
        Dim jasnosc As JasnoscLampy
        ReDim kom.Jasnosci(ile - 1)

        For i As Integer = 0 To ile - 1
            jasnosc = New JasnoscLampy
            jasnosc.Adres = br.ReadUInt16
            jasnosc.Jasnosc = br.ReadByte

            kom.Jasnosci(i) = jasnosc
        Next

        Return kom
    End Function
End Class

Public Class JasnoscLampy
    Public Property Adres As UShort
    Public Property Jasnosc As Byte
End Class