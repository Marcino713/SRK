Public Class SygnalizatorPolsamoczynny
    Inherits SygnalizatorUzalezniony
    Public Property DostepneSwiatla As DostepneSwiatlaEnum
    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorPolsamoczynny)
    End Sub
End Class

Public Enum DostepneSwiatlaEnum
    Zielone = 1
    PomaranczoweGora = 2
    Czerwone = 4
    PomaranczoweDol = 8
    Biale = 16
    ZielonyPas = 32
    PomaranczowyPas = 64
End Enum