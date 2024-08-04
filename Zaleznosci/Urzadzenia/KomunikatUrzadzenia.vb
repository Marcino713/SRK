Public MustInherit Class KomunikatUrzadzenia
    Private Const MASKA_SWIATLA As Byte = 3

    Public MustOverride ReadOnly Property Typ As Byte
    Public Property AdresPosterunku As UShort
    Public Property AdresUrzadzenia As UShort

    Protected Overridable Function ZapiszKomunikat() As UShort
        Return 0US
    End Function

    Friend Function Zapisz() As Byte()
        Using ms As New MemoryStream(KomunikacjaZUrzadzeniami.DLUGOSC_KOMUNIKATU)
            Using bw As New BinaryWriter(ms)
                bw.Write(Typ)
                bw.Write(AdresPosterunku)
                bw.Write(AdresUrzadzenia)
                bw.Write(ZapiszKomunikat)

                Return ms.ToArray
            End Using
        End Using
    End Function

    Protected Function Otworz(br As BinaryReader) As UShort
        AdresPosterunku = br.ReadUInt16
        AdresUrzadzenia = br.ReadUInt16
        Return br.ReadUInt16
    End Function

    Protected Sub ZapiszStanSwiatla(ByRef dane As UShort, kolejnosc As Integer, stan As StanSwiatlaSygnalizatora)
        dane = dane Or CUShort((stan And MASKA_SWIATLA) << (kolejnosc << 1))
    End Sub
End Class

Friend Class TypKomunikatuUrzadzenia
    Friend Const USTAW_STAN_SYGNALIZATORA As Byte = 1
    Friend Const USTAWIONO_STAN_SYGNALIZATORA As Byte = 2
    Friend Const USTAW_STAN_SYGNALIZATORA_DROGOWEGO As Byte = 3
    Friend Const USTAWIONO_STAN_SYGNALIZATORA_DROGOWEGO As Byte = 4
    Friend Const USTAW_JASNOSC_LAMPY As Byte = 5
    Friend Const USTAWIONO_JASNOSC_LAMPY As Byte = 6
    Friend Const WYKRYTO_OS As Byte = 7
    Friend Const USTAW_ZWROTNICE_SERWISOWO As Byte = 8
    Friend Const USTAW_ZWROTNICE As Byte = 9
    Friend Const ZMIENIONO_STAN_ZWROTNICY As Byte = 10
    Friend Const ZAMKNIJ_ROGATKE As Byte = 11
    Friend Const OTWORZ_ROGATKE As Byte = 12
    Friend Const ZMIENIONO_STAN_ROGATKI As Byte = 13
End Class

Public Enum StanSwiatlaSygnalizatora
    Wylaczone = 0
    Migajace = 1
    Wlaczone = 2
End Enum