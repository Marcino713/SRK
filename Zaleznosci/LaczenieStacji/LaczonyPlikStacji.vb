﻿Imports Zaleznosci.PlikiPolaczen
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPolaczen.KonfiguracjaZapisu, Zaleznosci.PlikiPolaczen.KonfiguracjaOdczytu)

Public Class LaczonyPlikStacji
    Implements IObiektPlikuTyp

    Public Property NazwaPliku As String = ""
    Public Property NazwaPosterunku As String = ""
    Public Property Uwagi As UwagiLaczanegoPlikuStacji = UwagiLaczanegoPlikuStacji.OK
    Public Property Skrot As Byte()
    Public Property OdcinkiTorow As OdcinekToru()
    Public Property Adres As UShort

    Private _ZawartoscPosterunku As Byte()
    Public ReadOnly Property ZawartoscPosterunku As Byte()
        Get
            Return _ZawartoscPosterunku
        End Get
    End Property

    Private _DanePulpitu As Pulpit
    Public ReadOnly Property DanePulpitu As Pulpit
        Get
            Return _DanePulpitu
        End Get
    End Property

    Public Shared Function WczytajPulpit(sciezka As String) As LaczonyPlikStacji
        Dim b As Byte() = File.ReadAllBytes(sciezka)
        Dim zawartoscPulpitu As Pulpit = Pulpit.Otworz(b)

        If zawartoscPulpitu IsNot Nothing Then
            Return New LaczonyPlikStacji With {
                .NazwaPliku = Path.GetFileName(sciezka),
                .NazwaPosterunku = zawartoscPulpitu.Nazwa,
                .Skrot = ObliczSkrot(b),
                .OdcinkiTorow = PobierzDostepneTory(zawartoscPulpitu.OdcinkiTorow),
                .Adres = zawartoscPulpitu.Adres,
                ._ZawartoscPosterunku = b,
                ._DanePulpitu = zawartoscPulpitu
            }
        Else
            Return Nothing
        End If
    End Function

    Private Shared Function PobierzDostepneTory(tory As List(Of OdcinekToru)) As OdcinekToru()
        Dim dostepneTory As New List(Of OdcinekToru)

        For Each tor As OdcinekToru In tory
            Dim kierunek As Integer = 0

            For Each kostkaTor As Tor In tor.KostkiTory
                If kostkaTor.Typ = TypKostki.Kierunek Then
                    kierunek += 1
                End If
            Next

            If kierunek = 1 Then dostepneTory.Add(tor)
        Next

        Return dostepneTory.OrderBy(Function(t As OdcinekToru) t.Nazwa).ToArray()
    End Function

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Dim id As Integer
        If Not konf.UzytePliki.TryGetValue(Me, id) Then Return Nothing

        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(id)
                ZapiszTekst(bw, NazwaPliku)
                bw.Write(CType(Skrot.Length, Byte))
                bw.Write(Skrot)
                Return ms.ToArray
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPlikuTyp
        Dim id As Integer
        Dim plik As New LaczonyPlikStacji

        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                id = br.ReadInt32()
                plik.NazwaPliku = OdczytajTekst(br)
                Dim ile As Integer = br.ReadByte
                plik.Skrot = br.ReadBytes(ile)
            End Using
        End Using

        Dim zawartoscPulpitu As Pulpit = Nothing
        Dim pelnaSciezka As String = konf.SciezkaFolderu & plik.NazwaPliku
        Dim b As Byte() = Nothing

        If File.Exists(pelnaSciezka) Then
            b = File.ReadAllBytes(pelnaSciezka)
            zawartoscPulpitu = Pulpit.Otworz(b)
        End If

        If zawartoscPulpitu Is Nothing Then
            plik.NazwaPosterunku = plik.NazwaPliku
            plik.OdcinkiTorow = New List(Of OdcinekToru)().ToArray
        Else
            plik._ZawartoscPosterunku = b
            plik._DanePulpitu = zawartoscPulpitu
            plik.Adres = zawartoscPulpitu.Adres
            plik.NazwaPosterunku = zawartoscPulpitu.Nazwa
            plik.OdcinkiTorow = PobierzDostepneTory(zawartoscPulpitu.OdcinkiTorow)
        End If

        konf.PlikiStacji.Add(id, plik)
        Return plik
    End Function

    Friend Sub Otworz(dane As Byte(), konf As KonfiguracjaOdczytu) Implements IObiektPlikuTyp.Otworz
        konf.Polaczenia.LaczanePliki.Add(Me)
    End Sub

End Class

Public Enum UwagiLaczanegoPlikuStacji
    OK
    Zmodyfikowany
    BrakPliku
    BrakiPolaczen
End Enum