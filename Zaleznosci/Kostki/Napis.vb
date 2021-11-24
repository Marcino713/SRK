Public Class Napis
    Inherits Kostka

    Public Property Tekst As String = ""

    Public Sub New()
        MyBase.New(TypKostki.Napis)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        ZapiszTekst(bw, Tekst)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        Tekst = OdczytajTekst(br)
    End Sub
End Class