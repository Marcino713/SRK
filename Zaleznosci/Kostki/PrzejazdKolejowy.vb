Imports Zaleznosci.PlikiPulpitu

Public Class PrzejazdKolejowy
    Inherits Tor

    Public Property NalezyDoPrzejazdu As PrzejazdKolejowoDrogowy

    Public Property Awaria As Boolean = False

    Private _Stan As StanPrzejazduKolejowego = StanPrzejazduKolejowego.Otwarty
    Public Property Stan As StanPrzejazduKolejowego
        Get
            Return _Stan
        End Get
        Set(value As StanPrzejazduKolejowego)
            _Stan = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.PrzejazdKolejowy)
    End Sub

    Public Overrides Function CzyMiga() As Boolean
        Return MyBase.CzyMiga() OrElse _Stan = StanPrzejazduKolejowego.Otwierany OrElse _Stan = StanPrzejazduKolejowego.Zamykany
    End Function

    Protected Friend Overrides Sub UsunPrzejazdZPowiazan(przejazd As PrzejazdKolejowoDrogowy)
        If NalezyDoPrzejazdu Is przejazd Then NalezyDoPrzejazdu = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(If(NalezyDoPrzejazdu Is Nothing, PUSTE_ODWOLANIE, konf.Przejazdy(NalezyDoPrzejazdu)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Dim id As Integer = br.ReadInt32
        NalezyDoPrzejazdu = konf.Przejazdy(id)

        NalezyDoPrzejazdu?.KostkiPrzejazdy.Add(Me)
    End Sub
End Class

Public Enum StanPrzejazduKolejowego
    Otwarty = 1
    Zamykany = 2
    Zamkniety = 3
    Otwierany = 4
End Enum