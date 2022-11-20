Imports Zaleznosci.PlikiPulpitu

Public Class PrzyciskTor
    Inherits Tor
    Implements IPrzycisk

    Public Property TypPrzycisku As TypPrzyciskuTorEnum

    Private _ObslugiwanySygnalizator As Sygnalizator
    Public Property ObslugiwanySygnalizator As Sygnalizator
        Get
            Return _ObslugiwanySygnalizator
        End Get
        Set(value As Sygnalizator)
            If Not (value Is Nothing OrElse TypeOf value Is SygnalizatorPolsamoczynny OrElse TypeOf value Is SygnalizatorManewrowy) Then
                Throw New Exception("Przycisk z torem może być powiązany tylko z sygnalizatorem półsamoczynnym albo manewrowym.")
            End If

            _ObslugiwanySygnalizator = value
        End Set
    End Property

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety

    Public Sub New()
        MyBase.New(TypKostki.PrzyciskTor)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If _ObslugiwanySygnalizator Is kostka Then _ObslugiwanySygnalizator = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CType(TypPrzycisku, Byte))
        bw.Write(If(_ObslugiwanySygnalizator Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(_ObslugiwanySygnalizator)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        TypPrzycisku = CType(br.ReadByte, TypPrzyciskuTorEnum)
        Dim id As Integer = br.ReadInt32
        ObslugiwanySygnalizator = CType(konf.Kostki(id), Sygnalizator)
    End Sub
End Class

Public Enum TypPrzyciskuTorEnum
    SygnalizatorPolsamoczynny
    SygnalizatorManewrowy
    SygnalManewrowy     'sygnał manewrowy na sygnalizatorze półsamoczynnym
End Enum