Public MustInherit Class Komunikat
    Public MustOverride ReadOnly Property Typ As UShort
    Public MustOverride Sub Zapisz(bw As BinaryWriter)
End Class