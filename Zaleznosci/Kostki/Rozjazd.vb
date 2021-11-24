Public MustInherit Class Rozjazd
    Inherits Tor
    Private Const LICZBA_ROZJAZDOW_ZALEZNYCH As Integer = 2

    Public Property PredkoscBoczna As Integer = 0
    Public Property Nazwa As String = ""
    Public Property Adres As UShort = 0
    Public Property ZaleznosciJesliWprost As KonfiguracjaRozjazduZaleznego()
    Public Property ZaleznosciJesliBok As KonfiguracjaRozjazduZaleznego()

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
        ZaleznosciJesliWprost = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
        ZaleznosciJesliBok = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        For i As Integer = 0 To ZaleznosciJesliWprost.Length - 1
            If ZaleznosciJesliWprost(i).RozjazdZalezny Is kostka Then ZaleznosciJesliWprost(i).RozjazdZalezny = Nothing
        Next

        For i As Integer = 0 To ZaleznosciJesliBok.Length - 1
            If ZaleznosciJesliBok(i).RozjazdZalezny Is kostka Then ZaleznosciJesliBok(i).RozjazdZalezny = Nothing
        Next
    End Sub

    Private Function PobierzDomyslnaKonfiguracje(ile As Integer) As KonfiguracjaRozjazduZaleznego()
        Dim t(ile - 1) As KonfiguracjaRozjazduZaleznego
        For i As Integer = 0 To ile - 1
            t(i) = New KonfiguracjaRozjazduZaleznego()
        Next
        Return t
    End Function

    Private Sub ZapiszZaleznosci(zalezn As KonfiguracjaRozjazduZaleznego(), bw As BinaryWriter, konf As KonfiguracjaZapisu)
        Dim zal As KonfiguracjaRozjazduZaleznego
        bw.Write(CType(zalezn.Length, Byte))
        For i As Integer = 0 To zalezn.Length - 1
            zal = zalezn(i)
            bw.Write(If(zal.RozjazdZalezny Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(zal.RozjazdZalezny)))
            bw.Write(CType(zal.Konfiguracja, Byte))
        Next
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
        ZapiszTekst(bw, Nazwa)
        bw.Write(CType(PredkoscBoczna, Short))
        ZapiszZaleznosci(ZaleznosciJesliWprost, bw, konf)
        ZapiszZaleznosci(ZaleznosciJesliBok, bw, konf)
    End Sub

    Private Function OtworzZaleznosci(br As BinaryReader, konf As KonfiguracjaOdczytu) As KonfiguracjaRozjazduZaleznego()
        Dim ile As Byte = br.ReadByte
        Dim zaleznosci(ile - 1) As KonfiguracjaRozjazduZaleznego
        For i As Integer = 0 To ile - 1
            Dim zal As New KonfiguracjaRozjazduZaleznego
            Dim id As Integer = br.ReadInt32
            zal.RozjazdZalezny = CType(konf.Kostki(id), Rozjazd)
            zal.Konfiguracja = CType(br.ReadByte, UstawienieRozjazduZaleznegoEnum)
            zaleznosci(i) = zal
        Next
        Return zaleznosci
    End Function

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
        Nazwa = OdczytajTekst(br)
        PredkoscBoczna = br.ReadInt16
        ZaleznosciJesliWprost = OtworzZaleznosci(br, konf)
        ZaleznosciJesliBok = OtworzZaleznosci(br, konf)
    End Sub
End Class

Public Class KonfiguracjaRozjazduZaleznego
    Public RozjazdZalezny As Rozjazd
    Public Konfiguracja As UstawienieRozjazduZaleznegoEnum
End Class

Public Enum UstawienieRozjazduZaleznegoEnum
    Wprost
    Bok
End Enum