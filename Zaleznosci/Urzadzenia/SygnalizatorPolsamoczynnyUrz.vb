Public Class SygnalizatorPolsamoczynnyUrz
    Public Property Predkosc As PredkoscSygnalizatora
    Public Property PredkoscPowtarzana As PredkoscPowtarzanaSygnalizatora
    Public Property KierunekPrzeciwny As Boolean
End Class

Public Enum PredkoscSygnalizatora
    V0
    V40
    V60
    V100
    VMax
    Manewrowy
    Zastepczy
End Enum

Public Enum PredkoscPowtarzanaSygnalizatora
    V0
    V40V60
    V100
    VMax
End Enum