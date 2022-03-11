Public Class WersjaPliku
    Public ReadOnly Property WersjaGlowna As UShort
    Public ReadOnly Property WersjaBoczna As UShort

    Public Sub New(glowna As UShort, boczna As UShort)
        WersjaGlowna = glowna
        WersjaBoczna = boczna
    End Sub

    Public Shared Operator =(w1 As WersjaPliku, w2 As WersjaPliku) As Boolean
        Return w1.WersjaGlowna = w2.WersjaGlowna And w1.WersjaBoczna = w2.WersjaBoczna
    End Operator

    Public Shared Operator <>(w1 As WersjaPliku, w2 As WersjaPliku) As Boolean
        Return Not w1 = w2
    End Operator

    Public Function CzyObslugiwana(dostepneWersje As WersjaPliku()) As Boolean
        For i As Integer = 0 To dostepneWersje.Length - 1
            If dostepneWersje(i) = Me Then Return True
        Next

        Return False
    End Function
End Class