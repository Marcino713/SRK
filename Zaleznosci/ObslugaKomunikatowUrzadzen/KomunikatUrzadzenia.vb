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

Public Enum StanSwiatlaSygnalizatora
    Wylaczone = 0
    Migajace = 1
    Wlaczone = 2
End Enum