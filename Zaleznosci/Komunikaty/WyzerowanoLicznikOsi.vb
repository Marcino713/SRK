Public Class WyzerowanoLicznikOsi
    Inherits Komunikat

    Public Property AdresOdcinka As UShort
    Public Property Stan As StanZerowanegoLicznikaOsi

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.WYZEROWANO_LICZNIK_OSI
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(AdresOdcinka)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New WyzerowanoLicznikOsi
        kom.AdresOdcinka = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, StanZerowanegoLicznikaOsi)

        Return kom
    End Function
End Class

Public Enum StanZerowanegoLicznikaOsi As Byte
    Wyzerowano
    BlednyAdres
End Enum