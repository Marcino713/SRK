Imports System.Threading

Public Class UrzadzenieFizyczne
    Inherits KomunikacjaZUrzadzeniami

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

    Public Sub New()
        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.USTAWIONO_STAN_SYGNALIZATORA,
            Sub(br) OdebrUstawionoStanSygnalizatora(New UstawionoStanSygnalizatoraUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.USTAWIONO_STAN_SYGNALIZATORA_DROGOWEGO,
            Sub(br) OdebrUstawionoStanSygnalizatoraDrogowego(New UstawionoStanSygnalizatoraDrogowegoUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.USTAWIONO_JASNOSC_LAMPY,
            Sub(br) OdebrUstawionoJasnoscLampy(New UstawionoJasnoscLampyUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.WYKRYTO_OS,
            Sub(br) OdebrWykrytoOs(New WykrytoOsUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.ZMIENIONO_STAN_ZWROTNICY,
            Sub(br) OdebrZmienionoStanZwrotnicy(New ZmienionoStanZwrotnicyUrz(br)))

        DaneFabrykiObiektow.Add(
            TypKomunikatuUrzadzenia.ZMIENIONO_STAN_ROGATKI,
            Sub(br) OdebrZmienionoStanRogatki(New ZmienionoStanRogatkiUrz(br)))
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

    Public Overrides Sub UstawStanSygnalizatoraSamoczynnego(kom As UstawStanSygnalizatoraSamoczynnegoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraManewrowego(kom As UstawStanSygnalizatoraManewrowegoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraPowtarzajacego(kom As UstawStanSygnalizatoraPowtarzajacegoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraPolsamoczynnego(kom As UstawStanSygnalizatoraPolsamoczynnegoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraPrzejazdowego(kom As UstawStanSygnalizatoraPrzejazdowegoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawStanSygnalizatoraDrogowego(kom As UstawStanSygnalizatoraDrogowegoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawJasnoscLampy(kom As UstawJasnoscLampyUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawZwrotnice(kom As UstawZwrotniceUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub UstawZwrotniceSerwisowo(kom As UstawZwrotniceSerwisowoUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub ZamknijRogatke(kom As ZamknijRogatkeUrz)
        Zapisz(kom)
    End Sub

    Public Overrides Sub OtworzRogatke(kom As OtworzRogatkeUrz)
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
                dane = br.ReadBytes(KomunikatUrzadzenia.DLUGOSC_KOMUNIKATU - 1)

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