Public MustInherit Class Kostka
    Public ReadOnly Property Typ As TypKostki
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Obrot As Integer
    Public Sub New(typ As TypKostki)
        Me.Typ = typ
    End Sub
End Class

Public Enum TypKostki
    Tor
    TorKoniec
    ZakretLewo
    ZakretPrawo
    RozjazdLewo
    RozjazdPrawo
    SygnalizatorManewrowy
    SygnalizatorPolsamoczynny
    SygnalizatorSamoczynny
    Przycisk
    PrzyciskTor
    Kierunek
End Enum