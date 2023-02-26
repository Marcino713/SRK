﻿Imports Zaleznosci.PlikiPulpitu

Public MustInherit Class Rozjazd
    Inherits Tor
    Implements IAdres, IPrzycisk, IZakret

    Private Const LICZBA_ROZJAZDOW_ZALEZNYCH As Integer = 2

    Public Property Adres As UShort = 0 Implements IAdres.Adres
    Public Property PredkoscBoczna As UShort = 0
    Public Property DlugoscBok As Single = 0.0F
    Public Property ZelektryfikowanyBok As Boolean = True
    Public Property KontrolaNiezajetosciBok As Boolean = True
    Public Property KierunekZasadniczy As UstawienieRozjazduEnum = UstawienieRozjazduEnum.Wprost
    Public Property PosiadaPrzycisk As Boolean = True Implements IPrzycisk.PosiadaPrzycisk

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
    Public Property PrzytnijZakret As PrzycinanieZakretu Implements IZakret.PrzytnijZakret
    Public Property Stan As StanRozjazdu = StanRozjazdu.Niezdefiniowany

    Private _ZajetoscBok As ZajetoscToru = ZajetoscToru.Wolny
    Public Property ZajetoscBok As ZajetoscToru
        Get
            Return _ZajetoscBok
        End Get
        Set(value As ZajetoscToru)
            _ZajetoscBok = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property

    Private _Rozprucie As Boolean = False
    Public Property Rozprucie As Boolean
        Get
            Return _Rozprucie
        End Get
        Set(value As Boolean)
            _Rozprucie = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property


    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
        _ZaleznosciJesliWprost = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
        _ZaleznosciJesliBok = PobierzDomyslnaKonfiguracje(LICZBA_ROZJAZDOW_ZALEZNYCH)
    End Sub

    Public Overrides Function CzyMiga() As Boolean
        Return MyBase.CzyMiga() OrElse _ZajetoscBok = ZajetoscToru.BlokadaNieustawiona Or _Rozprucie
    End Function

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
        bw.Write(PredkoscBoczna)
        bw.Write(DlugoscBok)
        bw.Write(ZelektryfikowanyBok)
        bw.Write(KontrolaNiezajetosciBok)
        bw.Write(CByte(KierunekZasadniczy))
        bw.Write(PosiadaPrzycisk)
        ZapiszZaleznosci(_ZaleznosciJesliWprost, bw, konf)
        ZapiszZaleznosci(_ZaleznosciJesliBok, bw, konf)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
        PredkoscBoczna = br.ReadUInt16
        DlugoscBok = br.ReadSingle
        ZelektryfikowanyBok = br.ReadBoolean
        KontrolaNiezajetosciBok = br.ReadBoolean
        KierunekZasadniczy = CType(br.ReadByte, UstawienieRozjazduEnum)
        PosiadaPrzycisk = br.ReadBoolean
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

Public Enum StanRozjazdu As Byte
    Wprost
    Bok
    Niezdefiniowany
End Enum