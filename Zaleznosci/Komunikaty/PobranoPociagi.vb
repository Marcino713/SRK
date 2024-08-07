﻿Public Class PobranoPociagi
    Inherits Komunikat

    Public Property Pociagi As DaneWybieralnegoPociagu()

    Public Overrides ReadOnly Property Typ As UShort
        Get
            Return TypKomunikatu.POBRANO_POCIAGI
        End Get
    End Property

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        bw.Write(CUShort(Pociagi.Length))
        Dim pociag As DaneWybieralnegoPociagu

        For i As Integer = 0 To Pociagi.Length - 1
            pociag = Pociagi(i)

            bw.Write(pociag.Numer)
            ZapiszTekst(bw, pociag.Nazwa)
            bw.Write(pociag.PredkoscMaksymalna)
            bw.Write(pociag.Stan)
            ZapiszTekst(bw, pociag.DodajacyPosterunek)
            ZapiszTekst(bw, pociag.Lokalizacja)
        Next
    End Sub

    Public Shared Function Otworz(br As BinaryReader) As Komunikat
        Dim kom As New PobranoPociagi
        Dim ile As Integer = br.ReadUInt16
        ReDim kom.Pociagi(ile - 1)

        For i As Integer = 0 To ile - 1
            Dim poc As New DaneWybieralnegoPociagu
            poc.Numer = br.ReadUInt32
            poc.Nazwa = OdczytajTekst(br)
            poc.PredkoscMaksymalna = br.ReadUInt16
            poc.Stan = CType(br.ReadByte, StanWybieralnegoPociagu)
            poc.DodajacyPosterunek = OdczytajTekst(br)
            poc.Lokalizacja = OdczytajTekst(br)
            kom.Pociagi(i) = poc
        Next

        Return kom
    End Function
End Class

Public Class DaneWybieralnegoPociagu
    Public Property Numer As UInteger
    Public Property Nazwa As String
    Public Property PredkoscMaksymalna As UShort
    Public Property Stan As StanWybieralnegoPociagu
    Public Property DodajacyPosterunek As String
    Public Property Lokalizacja As String
End Class

Public Enum StanWybieralnegoPociagu As Byte
    Wolny
    Zajety
End Enum