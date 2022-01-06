Imports Zaleznosci.PlikiPulpitu

Public Class Tor
    Inherits Kostka

    Public Property PredkoscZasadnicza As Integer
    Public Property NalezyDoOdcinka As OdcinekToru

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
        bw.Write(CType(PredkoscZasadnicza, Short))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        Dim id As Integer = br.ReadInt32
        NalezyDoOdcinka = konf.OdcinkiTorow(id)
        PredkoscZasadnicza = br.ReadInt16

        NalezyDoOdcinka?.KostkiTory.Add(Me)
    End Sub
End Class