﻿Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPulpitu, Zaleznosci.KonfiguracjaOdczytuPulpitu)

Public Class PrzejazdKolejowoDrogowy
    Implements IObiektPlikuTyp

    Public Property Numer As UShort
    Public Property Nazwa As String = ""
    Public Property Tryb As TrybPrzejazduKolejowego
    Public Property CzasSwiatel As UShort
    Public Property CzasOpuszczania As UShort
    Public Property CzasPodnoszenia As UShort

    Private _AutomatyczneZamykanie As New List(Of PrzejazdAutomatyczneZamykanie)
    Public ReadOnly Property AutomatyczneZamykanie As List(Of PrzejazdAutomatyczneZamykanie)
        Get
            Return _AutomatyczneZamykanie
        End Get
    End Property

    Private _Rogatki As New List(Of PrzejazdRogatka)
    Public ReadOnly Property Rogatki As List(Of PrzejazdRogatka)
        Get
            Return _Rogatki
        End Get
    End Property

    Private _SygnalizatoryDrogowe As New List(Of PrzejazdElementWykonawczy)
    Public ReadOnly Property SygnalizatoryDrogowe As List(Of PrzejazdElementWykonawczy)
        Get
            Return _SygnalizatoryDrogowe
        End Get
    End Property

    Private _KostkiPrzejazdy As New HashSet(Of PrzejazdKolejowoDrogowyKostka)
    Public ReadOnly Property KostkiPrzejazdy As HashSet(Of PrzejazdKolejowoDrogowyKostka)
        Get
            Return _KostkiPrzejazdy
        End Get
    End Property

    Friend Function Zapisz(konf As KonfiguracjaZapisuPulpitu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(konf.Przejazdy(Me))
                bw.Write(Numer)
                ZapiszTekst(bw, Nazwa)
                bw.Write(CByte(Tryb))
                bw.Write(CzasSwiatel)
                bw.Write(CzasOpuszczania)
                bw.Write(CzasPodnoszenia)

                bw.Write(CUShort(_AutomatyczneZamykanie.Count))
                For Each a As PrzejazdAutomatyczneZamykanie In _AutomatyczneZamykanie
                    bw.Write(If(a.OdcinekWyjazd Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(a.OdcinekWyjazd)))
                    bw.Write(If(a.OdcinekPrzyjazd Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(a.OdcinekPrzyjazd)))
                    bw.Write(If(a.Sygnalizator Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(a.Sygnalizator)))
                Next

                ZapiszElementyWykonawcze(bw, _Rogatki)
                ZapiszElementyWykonawcze(bw, _SygnalizatoryDrogowe)

                Return ms.ToArray
            End Using
        End Using
    End Function

    Private Sub ZapiszElementyWykonawcze(Of T As PrzejazdElementWykonawczy)(bw As BinaryWriter, el As List(Of T))
        bw.Write(CUShort(el.Count))
        For Each e As T In el
            e.Zapisz(bw)
        Next
    End Sub

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytuPulpitu) As IObiektPlikuTyp
        Dim id As Integer = PobierzInt32(dane, 0, 4)
        Dim p As New PrzejazdKolejowoDrogowy
        konf.Przejazdy.Add(id, p)

        Return p
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytuPulpitu) Implements IObiektPlikuTyp.Otworz
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                ms.Seek(4, SeekOrigin.Begin)
                Numer = br.ReadUInt16
                Nazwa = OdczytajTekst(br)
                Tryb = CType(br.ReadByte, TrybPrzejazduKolejowego)
                CzasSwiatel = br.ReadUInt16
                CzasOpuszczania = br.ReadUInt16
                CzasPodnoszenia = br.ReadUInt16

                Dim ile As Integer = br.ReadUInt16
                _AutomatyczneZamykanie.Capacity = ile

                For i As Integer = 0 To ile - 1
                    Dim a As New PrzejazdAutomatyczneZamykanie
                    a.OdcinekWyjazd = konf.OdcinkiTorow(br.ReadInt32)
                    a.OdcinekPrzyjazd = konf.OdcinkiTorow(br.ReadInt32)
                    a.Sygnalizator = CType(konf.Kostki(br.ReadInt32), SygnalizatorOstrzegawczyPrzejazdowy)
                    _AutomatyczneZamykanie.Add(a)
                Next

                OdczytajElementyWykonawcze(br, _Rogatki)
                OdczytajElementyWykonawcze(br, _SygnalizatoryDrogowe)
            End Using
        End Using

        konf.Pulpit.Przejazdy.Add(Me)
    End Sub

    Private Sub OdczytajElementyWykonawcze(Of T As {PrzejazdElementWykonawczy, New})(br As BinaryReader, el As List(Of T))
        Dim ile As Integer = br.ReadUInt16
        el.Capacity = ile

        For i As Integer = 0 To ile - 1
            Dim e As New T
            e.Otworz(br)
            el.Add(e)
        Next
    End Sub

    Public Sub SortujAutomatyzacjaWyjazdNazwaRosnaco()
        _AutomatyczneZamykanie = _AutomatyczneZamykanie.OrderBy(Function(a) If(a.OdcinekWyjazd?.Nazwa, "")).ToList
    End Sub

    Public Sub SortujRogatkiAdresRosnaco()
        _Rogatki = _Rogatki.OrderBy(Function(r) r.Adres).ToList
    End Sub

    Public Sub SortujSygnalizatoryDrogoweAdresRosnaco()
        _SygnalizatoryDrogowe = _SygnalizatoryDrogowe.OrderBy(Function(s) s.Adres).ToList
    End Sub

    Friend Sub UsunSygnalizatorZPowiazan(sygnTop As SygnalizatorOstrzegawczyPrzejazdowy)
        For Each aut As PrzejazdAutomatyczneZamykanie In _AutomatyczneZamykanie
            If aut.Sygnalizator Is sygnTop Then aut.Sygnalizator = Nothing
        Next
    End Sub

    Friend Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        For Each aut As PrzejazdAutomatyczneZamykanie In _AutomatyczneZamykanie
            If aut.OdcinekWyjazd Is odcinek Then aut.OdcinekWyjazd = Nothing
            If aut.OdcinekPrzyjazd Is odcinek Then aut.OdcinekPrzyjazd = Nothing
        Next
    End Sub
End Class

<Flags>
Public Enum TrybPrzejazduKolejowego
    Automatyczny = 1
    Reczny = 2
End Enum