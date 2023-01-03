Imports Zaleznosci.PlikiPulpitu

Public MustInherit Class Sygnalizator
    Inherits Tor
    Implements IAdres

    Public Property Adres As UShort = 0 Implements IAdres.Adres
    Public Property Nazwa As String = ""

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
        ZapiszTekst(bw, Nazwa)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
        Nazwa = OdczytajTekst(br)
    End Sub
End Class