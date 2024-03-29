﻿Public Class UstawKierunek
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As UstawianyStanKierunku

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.USTAW_KIERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New UstawKierunek
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, UstawianyStanKierunku)

        Return kom
    End Function
End Class

Public Enum UstawianyStanKierunku As Byte
    Wlacz
    Wylacz
    AnulujWlaczenie
End Enum