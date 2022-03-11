Public Class WybranoPosterunek  's
    Inherits Komunikat

    Public Property Stan As StanUstawianegoPosterunku
    Public Property ZawartoscPliku As Byte()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.WYBRANO_POSTERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Stan)
        If ZawartoscPliku Is Nothing Then
            bw.Write(0)
        Else
            bw.Write(ZawartoscPliku.Length)
            bw.Write(ZawartoscPliku)
        End If
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New WybranoPosterunek
        kom.Stan = CType(br.ReadByte, StanUstawianegoPosterunku)
        Dim ile As Integer = br.ReadInt32
        If ile > 0 Then kom.ZawartoscPliku = br.ReadBytes(ile)

        Return kom
    End Function
End Class

Public Enum StanUstawianegoPosterunku As Byte
    WybranoPoprawnie
    PosterunekZajety
End Enum