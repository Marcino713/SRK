﻿Public MustInherit Class Sygnalizator
    Inherits Tor
    Implements IAdres

    Public Property Adres As UShort = 0 Implements IAdres.Adres

    Public Sub New(typ As TypKostki)
        MyBase.New(typ)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
    End Sub
End Class