Imports Zaleznosci.PlikiPulpitu

Public Class Kierunek
    Inherits Tor
    Implements IPrzycisk

    Public Property KierunekWlaczany As KierunekWlaczanyEnum

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety

    Public Sub New()
        MyBase.New(TypKostki.Kierunek)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CType(KierunekWlaczany, Byte))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        KierunekWlaczany = CType(br.ReadByte, KierunekWlaczanyEnum)
    End Sub
End Class

Public Enum KierunekWlaczanyEnum
    Zasadniczy
    Przeciwny
End Enum