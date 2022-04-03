Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports System.Threading

Friend Class PolaczenieTCP
    Private Const ROZMIAR_BLOKU_AES As Integer = 16
    Private Const CZEKANIE_ZAMKNIECIE_MS As Integer = 100

    Friend Property AdresStacji As UShort

    Private _Stan As StanPolaczenia = StanPolaczenia.UstalanieKluczaSzyfrujacego
    Friend ReadOnly Property Stan As StanPolaczenia
        Get
            Return _Stan
        End Get
    End Property

    Private _CzasNawiazaniaPolaczenia As Date
    Public ReadOnly Property CzasNawiazaniaPolaczenia As Date
        Get
            Return _CzasNawiazaniaPolaczenia
        End Get
    End Property

    Private _OstatnieZapytanie As Date
    Public ReadOnly Property OstatnieZapytanie As Date
        Get
            Return _OstatnieZapytanie
        End Get
    End Property

    Private tcp As ZarzadzanieTCP
    Private Szyfrator As ICryptoTransform
    Private Deszyfrator As ICryptoTransform
    Private KlientTCP As TcpClient
    Private Strumien As NetworkStream
    Private bwTCP As BinaryWriter
    Private brTCP As BinaryReader
    Private rnd As New Random()
    Private WatekOdbierania As Thread
    Private Koniec As Boolean = False
    Private ZgloszonoKoniecPolaczenia As Boolean = False
    Private slockWyslijKomunikat As New Object
    Private slockZglosKoniecPol As New Object
    Private slockStanPolaczenia As New Object

    Friend Sub New(zarzTCP As ZarzadzanieTCP, klient As TcpClient)
        _CzasNawiazaniaPolaczenia = Now
        tcp = zarzTCP
        KlientTCP = klient
        Strumien = KlientTCP.GetStream()
        bwTCP = New BinaryWriter(Strumien)
        brTCP = New BinaryReader(Strumien)
        WatekOdbierania = New Thread(AddressOf OdbierajKomunikaty)
        WatekOdbierania.Start()
    End Sub

    Friend Sub Zakoncz(czekaj As Boolean)
        Koniec = True

        SyncLock slockStanPolaczenia
            If _Stan <> StanPolaczenia.Rozlaczony Then
                _Stan = StanPolaczenia.Rozlaczony
            Else
                Exit Sub
            End If
        End SyncLock

        Try
            If czekaj Then
                Strumien.Close(CZEKANIE_ZAMKNIECIE_MS)
            Else
                Strumien.Close()
            End If
        Catch
        End Try

        Try
            KlientTCP.Close()
        Catch
        End Try

    End Sub

    Friend Sub InicjujAes(kluczDH As Byte())
        If Szyfrator IsNot Nothing Or Deszyfrator IsNot Nothing Then Exit Sub

        Dim ziarno As Integer = 0
        Dim poz As Integer = 0
        For i As Integer = 0 To kluczDH.Length - 1
            ziarno = ziarno Xor (CType(kluczDH(i), Integer) << poz)
            poz += 8
            If poz >= 32 Then poz = 0
        Next

        Dim rnd As New Random(ziarno)
        Dim iv(ROZMIAR_BLOKU_AES - 1) As Byte
        Dim klucz(ROZMIAR_BLOKU_AES - 1) As Byte
        rnd.NextBytes(iv)
        rnd.NextBytes(klucz)

        Dim a As Aes = Aes.Create()
        a.IV = iv
        a.Key = klucz
        a.Mode = CipherMode.CBC
        a.Padding = PaddingMode.None

        Szyfrator = a.CreateEncryptor()
        Deszyfrator = a.CreateDecryptor()

        SyncLock slockStanPolaczenia
            _Stan = StanPolaczenia.UwierzytelnianieHaslem
        End SyncLock
    End Sub

    Friend Sub UstawStanWyborPosterunku()
        SyncLock slockStanPolaczenia
            If _Stan = StanPolaczenia.UwierzytelnianieHaslem Then _Stan = StanPolaczenia.WyborPosterunku
        End SyncLock
    End Sub

    Friend Sub UstawStanSterowanieRuchem()
        SyncLock slockStanPolaczenia
            If _Stan = StanPolaczenia.WyborPosterunku Then _Stan = StanPolaczenia.SterowanieRuchem
        End SyncLock
    End Sub

    Friend Sub WyslijKomunikat(kom As Komunikat)
        SyncLock slockStanPolaczenia
            If _Stan = StanPolaczenia.Rozlaczony Then Exit Sub
        End SyncLock

        Dim szyfruj As Boolean = True

        If TypKomunikatu.CzyKomunikatDH(kom.Typ) Then
            szyfruj = False
        ElseIf Szyfrator Is Nothing
            Exit Sub
        End If

        SyncLock slockWyslijKomunikat
            Try
                Dim b As Byte() = ZapiszKomunikat(kom)
                If szyfruj Then PrzetworzAes(Szyfrator, b)

                bwTCP.Write(b.Length)
                bwTCP.Write(b)
            Catch
                ZglosKoniecPolaczenia()
            End Try
        End SyncLock
    End Sub

    Private Function ZapiszKomunikat(kom As Komunikat) As Byte()
        Dim ms As New MemoryStream
        Dim bw As New BinaryWriter(ms)

        bw.Write(tcp.Wersja.WersjaGlowna)
        bw.Write(tcp.Wersja.WersjaBoczna)
        bw.Write(kom.Typ)

        kom.Zapisz(bw)

        Dim ile As Long = ROZMIAR_BLOKU_AES - (ms.Length Mod ROZMIAR_BLOKU_AES)
        If ile > 0 Then
            Dim b(CInt(ile) - 1) As Byte
            rnd.NextBytes(b)
            bw.Write(b)
        End If

        Return ms.ToArray
    End Function

    Private Sub OdbierajKomunikaty()
        Dim ile As Integer
        Dim b As Byte()

        Do Until Koniec
            Try
                ile = brTCP.ReadInt32
                b = brTCP.ReadBytes(ile)
                _OstatnieZapytanie = Now
                If Deszyfrator IsNot Nothing Then PrzetworzAes(Deszyfrator, b)
            Catch
                ZglosKoniecPolaczenia()
                Exit Do
            End Try

            Try
                OdczytajKomunikat(b)
            Catch
            End Try
        Loop
    End Sub

    Private Sub OdczytajKomunikat(b As Byte())
        Dim ms As New MemoryStream(b)
        Dim br As New BinaryReader(ms)

        Dim wersjaGlowna As UShort = br.ReadUInt16
        Dim wersjaBoczna As UShort = br.ReadUInt16
        Dim wersja As New WersjaPliku(wersjaGlowna, wersjaBoczna)

        If Not wersja.CzyObslugiwana(tcp.ObslugiwaneWersje) Then Exit Sub

        Dim typ As UShort = br.ReadUInt16

        SyncLock slockStanPolaczenia
            If Not (
            (_Stan = StanPolaczenia.UstalanieKluczaSzyfrujacego AndAlso TypKomunikatu.CzyKomunikatDH(typ)) Or
            (_Stan = StanPolaczenia.UwierzytelnianieHaslem AndAlso TypKomunikatu.CzyKomunikatUwierzytelniania(typ)) Or
            (_Stan = StanPolaczenia.WyborPosterunku AndAlso TypKomunikatu.CzyKomunikatWyboruPosterunku(typ)) Or
            (_Stan <> StanPolaczenia.UstalanieKluczaSzyfrujacego AndAlso TypKomunikatu.CzyKomunikatZakonczenia(typ))
            ) Then
                Exit Sub
            End If
        End SyncLock

        Dim metody As PrzetwOdebrKomunikatu = Nothing
        If tcp.DaneFabrykiObiektow.TryGetValue(typ, metody) Then
            If (Not metody.MetodaTworzaca.Equals(Nothing)) And (Not metody.MetodaZglaszajacaZdarzenie.Equals(Nothing)) Then
                Dim kom As Komunikat = metody.MetodaTworzaca(br)
                metody.MetodaZglaszajacaZdarzenie(Me, kom)
            End If
        End If
    End Sub

    Private Sub PrzetworzAes(obiekt As ICryptoTransform, dane As Byte())
        Dim poz As Integer = 0

        While poz < dane.Length
            obiekt.TransformBlock(dane, poz, ROZMIAR_BLOKU_AES, dane, poz)
            poz += ROZMIAR_BLOKU_AES
        End While
    End Sub

    Private Sub ZglosKoniecPolaczenia()
        SyncLock slockZglosKoniecPol
            If ZgloszonoKoniecPolaczenia Then Exit Sub
            ZgloszonoKoniecPolaczenia = True
        End SyncLock

        Zakoncz(False)
        tcp.PrzetworzZakonczeniePolaczenia(Me)
    End Sub
End Class