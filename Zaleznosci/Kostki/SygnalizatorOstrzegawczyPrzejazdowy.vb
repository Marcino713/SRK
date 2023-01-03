Public Class SygnalizatorOstrzegawczyPrzejazdowy
    Inherits Sygnalizator

    Public Property Stan As StanSygnalizatoraOstrzegawczegoPrzejazdowego = StanSygnalizatoraOstrzegawczegoPrzejazdowego.Wygaszony

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorOstrzegawczyPrzejazdowy)
    End Sub
End Class

Public Enum StanSygnalizatoraOstrzegawczegoPrzejazdowego
    Wygaszony = 0
    PrzejazdZamkniety = 1
    PrzejazdUszkodzony = 2
End Enum