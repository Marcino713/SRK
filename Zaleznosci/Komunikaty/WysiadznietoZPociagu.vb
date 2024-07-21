Public Class WysiadznietoZPociagu
    Inherits Komunikat

    Public Property NrPociagu As UInteger
    Public Property Stan As StanWysiadzniecia

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.WYSIADZNIETO_Z_POCIAGU
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(NrPociagu)
        bw.Write(Stan)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New WysiadznietoZPociagu
        kom.NrPociagu = br.ReadUInt32
        kom.Stan = CType(br.ReadByte, StanWysiadzniecia)

        Return kom
    End Function
End Class

Public Enum StanWysiadzniecia As Byte
    Wysiadznieto
    PociagNiesterowanyPrzezPosterunek
    BlednyNumer
    Wyrzucono
End Enum