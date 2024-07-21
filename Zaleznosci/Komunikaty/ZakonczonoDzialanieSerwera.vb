Public Class ZakonczonoDzialanieSerwera
    Inherits Komunikat

    Public Property Przyczyna As PrzyczynaZakonczeniaDzialaniaSerwera

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZAKONCZONO_DZIALANIE_SERWERA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Przyczyna)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZakonczonoDzialanieSerwera
        kom.Przyczyna = CType(br.ReadByte(), PrzyczynaZakonczeniaDzialaniaSerwera)

        Return kom
    End Function
End Class

Public Enum PrzyczynaZakonczeniaDzialaniaSerwera As Byte
    ZatrzymanieSerwera
End Enum