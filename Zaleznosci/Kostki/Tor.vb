Imports Zaleznosci.PlikiPulpitu

Public Class Tor
    Inherits Kostka

    Public Property PredkoscZasadnicza As UShort
    Public Property NalezyDoOdcinka As OdcinekToru

    Public Property Zajetosc As ZajetoscToru = ZajetoscToru.Wolny
    Public Overridable Property RysowanieDodatkowychTrojkatow As DodatkoweTrojkatyTor

    Public Sub New()
        MyBase.New(TypKostki.Tor)
    End Sub

    Public Sub New(Typ As TypKostki)
        MyBase.New(Typ)
    End Sub

    Protected Friend Overrides Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        If NalezyDoOdcinka Is odcinek Then NalezyDoOdcinka = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        bw.Write(If(NalezyDoOdcinka Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(NalezyDoOdcinka)))
        bw.Write(PredkoscZasadnicza)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        Dim id As Integer = br.ReadInt32
        NalezyDoOdcinka = konf.OdcinkiTorow(id)
        PredkoscZasadnicza = br.ReadUInt16

        NalezyDoOdcinka?.KostkiTory.Add(Me)
    End Sub
End Class

Public Enum ZajetoscToru As Byte
    Wolny
    Zajety
    PrzebiegUtwierdzony
End Enum

<Flags>
Public Enum DodatkoweTrojkatyTor
    LewoGora = 1
    LewoDol = 2
    PrawoDol = 4
    PrawoGora = 8
End Enum