Imports Zaleznosci.PlikiPolaczen
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPolaczen.KonfiguracjaZapisu, Zaleznosci.PlikiPolaczen.KonfiguracjaOdczytu)

Public Class LaczoneOdcinkiTorow
    Implements IObiektPlikuTyp

    Public Property Posterunek1 As LaczonyPlikStacji
    Public Property Tor1 As OdcinekToru
    Public Property Posterunek2 As LaczonyPlikStacji
    Public Property Tor2 As OdcinekToru
    Public Property Uwagi As UwagiLaczonegoOdcinkaTorow = UwagiLaczonegoOdcinkaTorow.OK

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream()
            Using bw As New BinaryWriter(ms)
                bw.Write(PobierzIdPliku(Posterunek1, konf.UzytePliki))
                ZapiszTekst(bw, Tor1.Nazwa)
                bw.Write(PobierzIdPliku(Posterunek2, konf.UzytePliki))
                ZapiszTekst(bw, Tor2.Nazwa)
                Return ms.ToArray
            End Using
        End Using
    End Function

    Private Function PobierzIdPliku(plik As LaczonyPlikStacji, uzytePliki As Dictionary(Of LaczonyPlikStacji, Integer)) As Integer
        Dim id As Integer
        If Not uzytePliki.TryGetValue(plik, id) Then
            id = uzytePliki.Count
            uzytePliki.Add(plik, id)
        End If
        Return id
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPlikuTyp
        Return New LaczoneOdcinkiTorow
    End Function

    Friend Sub Otworz(dane As Byte(), konf As KonfiguracjaOdczytu) Implements IObiektPlikuTyp.Otworz
        Dim Tor1Istnieje As Boolean
        Dim Tor2Istnieje As Boolean

        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                Dim id As Integer
                Dim nazwa As String

                id = br.ReadInt32()
                Posterunek1 = konf.PlikiStacji(id)
                nazwa = OdczytajTekst(br)
                Tor1Istnieje = PobierzTor(nazwa, Posterunek1, Tor1)

                id = br.ReadInt32()
                Posterunek2 = konf.PlikiStacji(id)
                nazwa = OdczytajTekst(br)
                Tor2Istnieje = PobierzTor(nazwa, Posterunek2, Tor2)
            End Using
        End Using

        If Tor1Istnieje = False AndAlso Tor2Istnieje = False Then
            Uwagi = UwagiLaczonegoOdcinkaTorow.BrakObuTorow
        ElseIf Tor1Istnieje = False
            Uwagi = UwagiLaczonegoOdcinkaTorow.BrakToru1
        ElseIf Tor2Istnieje = False
            Uwagi = UwagiLaczonegoOdcinkaTorow.BrakToru2
        End If

        konf.Polaczenia.LaczaneTory.Add(Me)
    End Sub

    Private Function PobierzTor(nazwa As String, posterunek As LaczonyPlikStacji, ByRef tor As OdcinekToru) As Boolean
        tor = Nothing

        If posterunek.OdcinkiTorow IsNot Nothing Then
            For Each t As OdcinekToru In posterunek.OdcinkiTorow
                If t.Nazwa = nazwa Then
                    tor = t
                    Exit For
                End If
            Next
        End If

        If tor Is Nothing Then
            tor = New OdcinekToru() With {.Nazwa = nazwa}
            Return False
        Else
            Return True
        End If
    End Function

End Class

Public Enum UwagiLaczonegoOdcinkaTorow
    OK
    BrakToru1
    BrakToru2
    BrakObuTorow
End Enum