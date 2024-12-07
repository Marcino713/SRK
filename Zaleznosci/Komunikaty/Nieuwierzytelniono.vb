Public Class Nieuwierzytelniono
    Inherits Komunikat

    Public Property Przyczyna As PrzyczynaNieuwierzytelnienia

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.NIEUWIERZYTELNIONO
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(Przyczyna)
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New Nieuwierzytelniono
        kom.Przyczyna = CType(br.ReadByte, PrzyczynaNieuwierzytelnienia)

        Return kom
    End Function
End Class

Public Enum PrzyczynaNieuwierzytelnienia As Byte
    BledneHaslo
    BrakTrybuObserwatora
End Enum