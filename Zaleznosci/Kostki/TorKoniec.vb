Public Class TorKoniec
    Inherits Kostka

    Public Sub New()
        MyBase.New(TypKostki.TorKoniec)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
    End Sub
End Class