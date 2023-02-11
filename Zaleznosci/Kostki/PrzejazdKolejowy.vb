Imports Zaleznosci.PlikiPulpitu

Public Class PrzejazdKolejowy
    Inherits Tor

    Public Property NalezyDoPrzejazdu As PrzejazdKolejowoDrogowy

    Public Property Stan As StanPrzejazduKolejowego = StanPrzejazduKolejowego.Otwarty
    Public Property Awaria As Boolean = False

    Public Sub New()
        MyBase.New(TypKostki.PrzejazdKolejowy)
    End Sub

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