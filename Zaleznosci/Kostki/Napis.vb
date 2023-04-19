Imports Zaleznosci.PlikiPulpitu

Public Class Napis
    Inherits Kostka

    Public Const ROZMIAR_DOMYSLNY As Single = 0.17F

    Public Property Tekst As String = ""
    Public Property Rozmiar As Single = ROZMIAR_DOMYSLNY

    Public Sub New()
        MyBase.New(TypKostki.Napis)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        ZapiszTekst(bw, Tekst)
        bw.Write(Rozmiar)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        Tekst = OdczytajTekst(br)
        Rozmiar = br.ReadSingle
    End Sub
End Class