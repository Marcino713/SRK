Public Class ZmienionoKierunek
    Inherits Komunikat

    Public Property Adres As UShort
    Public Property Stan As ObecnyStanKierunku
    Public Property StanZmiany As StanZmianyKierunku
    Public Property Blad As BladZmianyKierunku

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.ZMIENIONO_KIERUNEK
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(Stan)
        bw.Write(StanZmiany)
        bw.Write(Blad)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New ZmienionoKierunek
        kom.Adres = br.ReadUInt16
        kom.Stan = CType(br.ReadByte, ObecnyStanKierunku)
        kom.StanZmiany = CType(br.ReadByte, StanZmianyKierunku)
        kom.Blad = CType(br.ReadByte, BladZmianyKierunku)

        Return kom
    End Function
End Class

Public Enum ObecnyStanKierunku As Byte
    Neutralny
    Wyjazd
    Przyjazd
End Enum

Public Enum StanZmianyKierunku As Byte
    Brak
    ZadanieWlaczenia
    AnulowanieWlaczenia
    Wlaczanie
    Zwalnianie
End Enum

Public Enum BladZmianyKierunku As Byte
    Brak
    BlokadaWZadanymStanie
    BlednyAdres
    NieOczekujeNaPotwierdzenie
End Enum