Imports Zaleznosci.PlikiPulpitu

Public Class Kierunek
    Inherits Tor

    Public Property Nazwa As String
    Public Property KierunekWyjazdu As KierunekWyjazduSBL
    Public Property Stawnosc As StawnoscSBL

    Public Property UstawionyKierunek As UstawionyKierunekSBL = UstawionyKierunekSBL.Zaden
    Public Property UstawionyStanZmiany As UstawionyStanZmianyKierunkuSBL = UstawionyStanZmianyKierunkuSBL.Zaden

    Public Sub New()
        MyBase.New(TypKostki.Kierunek)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        ZapiszTekst(bw, Nazwa)
        bw.Write(CByte(KierunekWyjazdu))
        bw.Write(CByte(Stawnosc))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Nazwa = OdczytajTekst(br)
        KierunekWyjazdu = CType(br.ReadByte, KierunekWyjazduSBL)
        Stawnosc = CType(br.ReadByte, StawnoscSBL)
    End Sub
End Class

Public Enum KierunekWyjazduSBL
    Lewo
    Prawo
End Enum

Public Enum StawnoscSBL
    Dwustawna
    Trzystawna
    Czterostawna
End Enum

Public Enum UstawionyKierunekSBL
    Zaden
    Lewo
    Prawo
End Enum

Public Enum UstawionyStanZmianyKierunkuSBL
    Zaden
    OczekiwanieNaPotwierdzenie
    Wlaczanie
    Wylaczanie
End Enum