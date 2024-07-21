Public Class Informacja
    Inherits Komunikat

    Public Property Waznosc As WaznoscInformacji
    Public Property Rodzaj As RodzajInformacji

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.INFORMACJA
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Waznosc)
        bw.Write(Rodzaj)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New Informacja
        kom.Waznosc = CType(br.ReadByte(), WaznoscInformacji)
        kom.Rodzaj = CType(br.ReadByte(), RodzajInformacji)

        Return kom
    End Function
End Class

Public Enum WaznoscInformacji As Byte
    Krytyczna
    Wazna
End Enum

Public Enum RodzajInformacji As Byte
    UtraconoPolaczenieZUrzadzeniemWykonawczym
    UtraconoPolaczenieZMagistrala
    MinietoSygnalStoj
    WjechanoNaZajetyOdcinek
End Enum