﻿Imports Zaleznosci.PlikiPulpitu

Public Class Tor
    Inherits Kostka

    Public Property PredkoscZasadnicza As UShort = 0
    Public Property Nazwa As String = ""
    Public Property Dlugosc As Single = 0.0F
    Public Property Zelektryfikowany As Boolean = True
    Public Property KontrolaNiezajetosci As Boolean = True
    Public Property NalezyDoOdcinka As OdcinekToru = Nothing


    Private _Zajetosc As ZajetoscToru = ZajetoscToru.Wolny
    Public Property Zajetosc As ZajetoscToru
        Get
            Return _Zajetosc
        End Get
        Set(value As ZajetoscToru)
            _Zajetosc = value
            Migacz?.UstawKostke(Me)
        End Set
    End Property

    Public Overridable Property RysowanieDodatkowychTrojkatow As DodatkoweTrojkatyTor

    Public Sub New()
        MyBase.New(TypKostki.Tor)
    End Sub

    Public Sub New(Typ As TypKostki)
        MyBase.New(Typ)
    End Sub

    Public Overrides Function CzyMiga() As Boolean
        Return _Zajetosc = ZajetoscToru.BlokadaNieustawiona
    End Function

    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        If NalezyDoOdcinka Is odcinek Then NalezyDoOdcinka = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        bw.Write(PredkoscZasadnicza)
        ZapiszTekst(bw, Nazwa)
        bw.Write(Dlugosc)
        bw.Write(Zelektryfikowany)
        bw.Write(KontrolaNiezajetosci)
        bw.Write(If(NalezyDoOdcinka Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(NalezyDoOdcinka)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        PredkoscZasadnicza = br.ReadUInt16
        Nazwa = OdczytajTekst(br)
        Dlugosc = br.ReadSingle
        Zelektryfikowany = br.ReadBoolean
        KontrolaNiezajetosci = br.ReadBoolean

        Dim id As Integer = br.ReadInt32
        NalezyDoOdcinka = konf.OdcinkiTorow(id)
        NalezyDoOdcinka?.KostkiTory.Add(Me)
    End Sub
End Class

Public Enum ZajetoscToru As Byte
    Wolny
    Zajety
    PrzebiegUtwierdzony
    BlokadaNieustawiona
End Enum

<Flags>
Public Enum DodatkoweTrojkatyTor
    LewoGora = 1
    LewoDol = 2
    PrawoDol = 4
    PrawoGora = 8
End Enum