Public Class PrzetwOdebrKomunikatu
    Friend MetodaTworzaca As TworzenieKomunikatu
    Friend MetodaZglaszajacaZdarzenie As ZglaszanieZdarzenia

    Friend Sub New(metTworzaca As TworzenieKomunikatu, metZglaszajaca As ZglaszanieZdarzenia)
        MetodaTworzaca = metTworzaca
        MetodaZglaszajacaZdarzenie = metZglaszajaca
    End Sub
End Class

Friend Delegate Function TworzenieKomunikatu(br As BinaryReader) As Komunikat
Friend Delegate Sub ZglaszanieZdarzenia(post As UShort, kom As Komunikat)