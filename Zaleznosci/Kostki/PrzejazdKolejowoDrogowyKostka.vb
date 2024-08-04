Public Class PrzejazdKolejowoDrogowyKostka
    Inherits Tor

    Private Const BLAD As String = "Kostka przejazdu kolejowego nie obsługuje właściwości Nazwa."

    Public Property NalezyDoPrzejazdu As PrzejazdKolejowoDrogowy
    Public Overloads Property Nazwa As String
        Get
            Throw New NotSupportedException(BLAD)
        End Get
        Set(value As String)
            Throw New NotSupportedException(BLAD)
        End Set
    End Property


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

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(If(NalezyDoPrzejazdu Is Nothing, PUSTE_ODWOLANIE, konf.Przejazdy(NalezyDoPrzejazdu)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
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