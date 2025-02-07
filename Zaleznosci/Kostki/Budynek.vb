Public Class Budynek
    Inherits Kostka

    Public Property Tekst As String = ""
    Public Property TypBudynku As RodzajBudynku = RodzajBudynku.Ogolny

    Public Sub New()
        MyBase.New(TypKostki.Budynek)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        ZapiszTekst(bw, Tekst)
        bw.Write(CByte(TypBudynku))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        Tekst = OdczytajTekst(br)
        TypBudynku = CType(br.ReadByte(), RodzajBudynku)
    End Sub
End Class

Public Enum RodzajBudynku
    Ogolny = 0
    Nastawnia = 1
End Enum