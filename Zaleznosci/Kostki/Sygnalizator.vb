Imports Zaleznosci.PlikiPulpitu

Public MustInherit Class Sygnalizator
    Inherits Tor
    Public Property Adres As UShort = 0
    Public Property Nazwa As String = ""
    Public Property OdcinekNastepujacy As OdcinekToru

    Public Event ZmienionoSygnal()

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        If NalezyDoOdcinka Is odcinek Then NalezyDoOdcinka = Nothing
        If OdcinekNastepujacy Is odcinek Then OdcinekNastepujacy = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
        ZapiszTekst(bw, Nazwa)
        bw.Write(If(OdcinekNastepujacy Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(OdcinekNastepujacy)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
        Nazwa = OdczytajTekst(br)
        Dim id As Integer = br.ReadInt32
        OdcinekNastepujacy = konf.OdcinkiTorow(id)
    End Sub
End Class