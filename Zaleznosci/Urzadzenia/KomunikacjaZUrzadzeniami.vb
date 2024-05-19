Imports System.Threading

Public Class KomunikacjaZUrzadzeniami
    Friend Const DLUGOSC_KOMUNIKATU As Integer = 7

    Private strumien As Stream
    Private bw As BinaryWriter
    Private br As BinaryReader
    Private watekOdbierania As Thread
    Private slockZapisz As New Object
    Private DaneFabrykiObiektow As New Dictionary(Of Byte, MetodaOtwierajaca)

    Private _polaczony As Boolean
    Public ReadOnly Property Polaczony As Boolean
        Get
            Return _polaczony
        End Get
    End Property

    Private Delegate Sub MetodaOtwierajaca(br As BinaryReader)

    Public Event OdebranoUstawionoStanSygnalizatora(kom As UstawionoStanSygnalizatoraUrz)
    Public Event OdebranoUstawionoStanSygnalizatoraDrogowego(kom As UstawionoStanSygnalizatoraDrogowegoUrz)
    Public Event OdebranoUstawionoJasnoscLampy(kom As UstawionoJasnoscLampyUrz)
    Public Event OdebranoWykrytoOs(kom As WykrytoOsUrz)
    Public Event OdebranoZmienionoStanZwrotnicy(kom As ZmienionoStanZwrotnicyUrz)
    Public Event OdebranoZmienionoStanRogatki(kom As ZmienionoStanRogatkiUrz)

    Public Sub New()
        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.USTAWIONO_STAN_SYGNALIZATORA,
            Sub(br) RaiseEvent OdebranoUstawionoStanSygnalizatora(New UstawionoStanSygnalizatoraUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.USTAWIONO_STAN_SYGNALIZATORA_DROGOWEGO,
            Sub(br) RaiseEvent OdebranoUstawionoStanSygnalizatoraDrogowego(New UstawionoStanSygnalizatoraDrogowegoUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.USTAWIONO_JASNOSC_LAMPY,
            Sub(br) RaiseEvent OdebranoUstawionoJasnoscLampy(New UstawionoJasnoscLampyUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.WYKRYTO_OS,
            Sub(br) RaiseEvent OdebranoWykrytoOs(New WykrytoOsUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.ZMIENIONO_STAN_ZWROTNICY,
            Sub(br) RaiseEvent OdebranoZmienionoStanZwrotnicy(New ZmienionoStanZwrotnicyUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.ZMIENIONO_STAN_ROGATKI,
            Sub(br) RaiseEvent OdebranoZmienionoStanRogatki(New ZmienionoStanRogatkiUrz(br)))
    End Sub

    Public Sub Polacz(str As Stream)
        strumien = str
        bw = New BinaryWriter(str)
        br = New BinaryReader(str)
        watekOdbierania = New Thread(AddressOf OdbierajKomunikaty)
        watekOdbierania.Start()
        _polaczony = True
    End Sub

    Public Sub Rozlacz()
        Try
            strumien?.Close()
            strumien?.Dispose()
            strumien = Nothing
        Catch
        End Try

        _polaczony = False
    End Sub

    Public Sub UstawStanSygnalizatoraSamoczynnego(kom As UstawStanSygnalizatoraSamoczynnegoUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawStanSygnalizatoraManewrowego(kom As UstawStanSygnalizatoraManewrowegoUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawStanSygnalizatoraPowtarzajacego(kom As UstawStanSygnalizatoraPowtarzajacegoUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawStanSygnalizatoraPolsamoczynnego(kom As UstawStanSygnalizatoraPolsamoczynnegoUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawStanSygnalizatoraPrzejazdowego(kom As UstawStanSygnalizatoraPrzejazdowegoUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawStanSygnalizatoraDrogowego(kom As UstawStanSygnalizatoraDrogowegoUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawJasnoscLampy(kom As UstawJasnoscLampyUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawZwrotnice(kom As UstawZwrotniceUrz)
        Zapisz(kom)
    End Sub

    Public Sub UstawZwrotniceSerwisowo(kom As UstawZwrotniceSerwisowoUrz)
        Zapisz(kom)
    End Sub

    Public Sub ZamknijRogatke(kom As ZamknijRogatkeUrz)
        Zapisz(kom)
    End Sub

    Public Sub OtworzRogatke(kom As OtworzRogatkeUrz)
        Zapisz(kom)
    End Sub

    Private Sub Zapisz(kom As KomunikatUrzadzenia)
        SyncLock slockZapisz
            Try
                bw.Write(kom.Zapisz)
            Catch
            End Try
        End SyncLock
    End Sub

    Private Sub OdbierajKomunikaty()
        Dim typ As Byte
        Dim dane As Byte()
        Dim metoda As MetodaOtwierajaca = Nothing

        Do
            Try
                typ = br.ReadByte
                dane = br.ReadBytes(DLUGOSC_KOMUNIKATU - 1)

                If DaneFabrykiObiektow.TryGetValue(typ, metoda) Then
                    Using ms As New MemoryStream(dane)
                        Using br As New BinaryReader(ms)
                            metoda(br)
                        End Using
                    End Using
                End If

            Catch
                Exit Do
            End Try
        Loop
    End Sub

End Class