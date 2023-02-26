Imports Zaleznosci.PlikiPulpitu

Public Class Kierunek
    Inherits Tor

    Public Property KierunekWyjazdu As KierunekWyjazduSBL
    Public Property Stawnosc As StawnoscSBL

    Public Property UstawionyKierunek As UstawionyKierunekSBL = UstawionyKierunekSBL.Zaden

    Private _UstawionyStanZmiany As UstawionyStanZmianyKierunkuSBL = UstawionyStanZmianyKierunkuSBL.Zaden
    Public Property UstawionyStanZmiany As UstawionyStanZmianyKierunkuSBL
        Get
            Return _UstawionyStanZmiany
        End Get
        Set(value As UstawionyStanZmianyKierunkuSBL)
            _UstawionyStanZmiany = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.Kierunek)
    End Sub

    Public Overrides Function CzyMiga() As Boolean
        Return MyBase.CzyMiga() OrElse _UstawionyStanZmiany <> UstawionyStanZmianyKierunkuSBL.Zaden
    End Function

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CByte(KierunekWyjazdu))
        bw.Write(CByte(Stawnosc))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
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