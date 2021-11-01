﻿Public MustInherit Class Kostka
    Public ReadOnly Property Typ As TypKostki
    Public Property Obrot As Integer
    Public Sub New(typ As TypKostki)
        Me.Typ = typ
    End Sub
    Protected Friend Overridable Sub UsunPowiazanie(kostka As Kostka)
    End Sub
    Protected Friend Overridable Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
    End Sub
End Class

Public Enum TypKostki
    Tor
    TorKoniec
    Zakret
    RozjazdLewo
    RozjazdPrawo
    SygnalizatorManewrowy
    SygnalizatorPolsamoczynny
    SygnalizatorSamoczynny
    Przycisk
    PrzyciskTor
    Kierunek
    Napis
End Enum