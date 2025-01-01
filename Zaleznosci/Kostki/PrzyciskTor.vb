Public Class PrzyciskTor
    Inherits Tor
    Implements IPrzycisk

    Private Const BLAD_NAZWA As String = "Kostka przycisku z torem nie obsługuje własności Nazwa."
    Private Const BLAD_PRZYCISK As String = "Kostka przycisku z torem zawsze posiada przycisk, dlatego nie można zmienić własności posiadania przycisku."

    Public Property TypPrzycisku As TypPrzyciskuTorEnum
    Public Property SygnalizatorPolsamoczynny As SygnalizatorPolsamoczynny
    Public Property SygnalizatorManewrowy As SygnalizatorManewrowy

    Public Overloads Property Nazwa As String
        Get
            Throw New NotSupportedException(BLAD_NAZWA)
        End Get
        Set(value As String)
            Throw New NotSupportedException(BLAD_NAZWA)
        End Set
    End Property

    Public Property PosiadaPrzycisk As Boolean Implements IPrzycisk.PosiadaPrzycisk
        Get
            Return True
        End Get
        Set(value As Boolean)
            Throw New NotSupportedException(BLAD_PRZYCISK)
        End Set
    End Property

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety
    Public Property Zablokowany As Boolean Implements IPrzycisk.Zablokowany

    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorPolsamoczynny Is kostka Then SygnalizatorPolsamoczynny = Nothing
        If SygnalizatorManewrowy Is kostka Then SygnalizatorManewrowy = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CByte(TypPrzycisku))
        bw.Write(If(SygnalizatorPolsamoczynny Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorPolsamoczynny)))
        bw.Write(If(SygnalizatorManewrowy Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorManewrowy)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        TypPrzycisku = CType(br.ReadByte, TypPrzyciskuTorEnum)
        SygnalizatorPolsamoczynny = CType(konf.Kostki(br.ReadInt32), SygnalizatorPolsamoczynny)
        SygnalizatorManewrowy = CType(konf.Kostki(br.ReadInt32), SygnalizatorManewrowy)
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    JazdaSygnalizatorPolsamoczynny
    ManewrySygnalizatorPolsamoczynny
    ManewrySygnalizatorManewrowy
End Enum