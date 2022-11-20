Imports Zaleznosci.PlikiPulpitu

Public MustInherit Class Rozjazd
    Inherits Tor
    Implements IPrzycisk, IZakret

    Private Const LICZBA_ROZJAZDOW_ZALEZNYCH As Integer = 2

    Public Property PredkoscBoczna As Integer = 0
    Public Property Nazwa As String = ""
    Public Property Adres As UShort = 0

    Private _ZaleznosciJesliWprost As KonfiguracjaRozjazduZaleznego()
    Public Property ZaleznosciJesliWprost As KonfiguracjaRozjazduZaleznego()
        Get
            Return _ZaleznosciJesliWprost
        End Get
        Set(value As KonfiguracjaRozjazduZaleznego())
            WalidujLiczbeRozjazdow(value)
            _ZaleznosciJesliWprost = value
        End Set
    End Property

    Private _ZaleznosciJesliBok As KonfiguracjaRozjazduZaleznego()
    Public Property ZaleznosciJesliBok As KonfiguracjaRozjazduZaleznego()
        Get
            Return _ZaleznosciJesliBok
        End Get
        Set(value As KonfiguracjaRozjazduZaleznego())
            WalidujLiczbeRozjazdow(value)
            _ZaleznosciJesliBok = value
        End Set
    End Property

    Public Property Wcisniety As Boolean = False Implements IPrzycisk.Wcisniety
    Public Property Stan As UstawienieZwrotnicy = UstawienieZwrotnicy.Niezdefiniowany
    Public Property ZajetoscBok As ZajetoscToru = ZajetoscToru.Wolny
    Public Property Rozprucie As Boolean = False
    Public Property PrzytnijZakret As PrzycinanieZakretu Implements IZakret.PrzytnijZakret

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
        _ZaleznosciJesliWprost = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
        _ZaleznosciJesliBok = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
        ZapiszTekst(bw, Nazwa)
        bw.Write(CType(PredkoscBoczna, Short))
        ZapiszZaleznosci(_ZaleznosciJesliWprost, bw, konf)
        ZapiszZaleznosci(_ZaleznosciJesliBok, bw, konf)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
        Nazwa = OdczytajTekst(br)
        PredkoscBoczna = br.ReadInt16
        ZaleznosciJesliWprost = OtworzZaleznosci(br, konf)
        ZaleznosciJesliBok = OtworzZaleznosci(br, konf)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        For i As Integer = 0 To _ZaleznosciJesliWprost.Length - 1
            If _ZaleznosciJesliWprost(i).RozjazdZalezny Is kostka Then _ZaleznosciJesliWprost(i).RozjazdZalezny = Nothing
        Next

        For i As Integer = 0 To _ZaleznosciJesliBok.Length - 1
            If _ZaleznosciJesliBok(i).RozjazdZalezny Is kostka Then _ZaleznosciJesliBok(i).RozjazdZalezny = Nothing
        Next
    End Sub

    Private Sub ZapiszZaleznosci(zalezn As KonfiguracjaRozjazduZaleznego(), bw As BinaryWriter, konf As KonfiguracjaZapisu)
        Dim zal As KonfiguracjaRozjazduZaleznego
        bw.Write(CType(zalezn.Length, Byte))

        For i As Integer = 0 To zalezn.Length - 1
            zal = zalezn(i)
            bw.Write(If(zal.RozjazdZalezny Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(zal.RozjazdZalezny)))
            bw.Write(CType(zal.Konfiguracja, Byte))
        Next
    End Sub

    Private Function OtworzZaleznosci(br As BinaryReader, konf As KonfiguracjaOdczytu) As KonfiguracjaRozjazduZaleznego()
        Dim ile As Byte = br.ReadByte
        Dim zaleznosci(ile - 1) As KonfiguracjaRozjazduZaleznego

        For i As Integer = 0 To ile - 1
            Dim zal As New KonfiguracjaRozjazduZaleznego
            Dim id As Integer = br.ReadInt32
            zal.RozjazdZalezny = CType(konf.Kostki(id), Rozjazd)
            zal.Konfiguracja = CType(br.ReadByte, UstawienieRozjazduEnum)
            zaleznosci(i) = zal
        Next

        Return zaleznosci
    End Function

    Private Sub WalidujLiczbeRozjazdow(konf As KonfiguracjaRozjazduZaleznego())
        If konf Is Nothing OrElse konf.Length <> LICZBA_ROZJAZDOW_ZALEZNYCH Then
            Throw New Exception($"Liczba rozjazdów zależnych musi wynosić dokładnie {LICZBA_ROZJAZDOW_ZALEZNYCH}.")
        End If
    End Sub

    Private Function PobierzDomyslnaKonfiguracje(ile As Integer) As KonfiguracjaRozjazduZaleznego()
        Dim t(ile - 1) As KonfiguracjaRozjazduZaleznego

        For i As Integer = 0 To ile - 1
            t(i) = New KonfiguracjaRozjazduZaleznego()
        Next

        Return t
    End Function
End Class

Public Class KonfiguracjaRozjazduZaleznego
    Public RozjazdZalezny As Rozjazd
    Public Konfiguracja As UstawienieRozjazduEnum
End Class

Public Enum UstawienieRozjazduEnum
    Wprost
    Bok
End Enum

Public Enum UstawienieZwrotnicy As Byte
    Wprost
    Bok
    Niezdefiniowany
End Enum