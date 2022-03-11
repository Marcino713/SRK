Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports System.Threading

Public Class PolaczenieTCP
    Private Const ROZMIAR_BLOKU_AES As Integer = 16

    Private tcp As ZarzadzanieTCP
    Private AdresStacji As UShort
    Private Szyfrator As ICryptoTransform
    Private Deszyfrator As ICryptoTransform
    Private KlientTCP As TcpClient
    Private Strumien As NetworkStream
    Private bwTCP As BinaryWriter
    Private brTCP As BinaryReader
    Private rnd As New Random()
    Private WatekOdbierania As Thread
    Private Koniec As Boolean = False

    Friend Sub New(zarzTCP As ZarzadzanieTCP, klient As TcpClient)
        tcp = zarzTCP
        KlientTCP = klient
        Strumien = KlientTCP.GetStream()
        bwTCP = New BinaryWriter(Strumien)
        brTCP = New BinaryReader(Strumien)
        WatekOdbierania = New Thread(AddressOf OdbierajKomunikaty)
        WatekOdbierania.Start()
    End Sub

    Friend Sub Zakoncz()
        Koniec = True
        Strumien.Close()
    End Sub

    Friend Sub WyslijKomunikat(kom As Komunikat)
        SyncLock Me
            Dim b As Byte() = ZapiszKomunikat(kom)
            PrzetworzAes(Szyfrator, b)

            Try
                bwTCP.Write(b.Length)
                bwTCP.Write(b)
            Catch
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

                PrzetworzAes(Deszyfrator, b)
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

        Dim metody As PrzetwOdebrKomunikatu = Nothing
        If tcp.DaneFabrykiObiektow.TryGetValue(typ, metody) Then
            If (Not metody.MetodaTworzaca.Equals(Nothing)) And (Not metody.MetodaZglaszajacaZdarzenie.Equals(Nothing)) Then
                Dim kom As Komunikat = metody.MetodaTworzaca(br)
                metody.MetodaZglaszajacaZdarzenie(AdresStacji, kom)
            End If
        End If
    End Sub

    Private Sub InicjujAes(klucz As Byte())
        Dim a As Aes = Aes.Create()
        a.Key = klucz
        a.IV = klucz
        a.Mode = CipherMode.CBC
        Szyfrator = a.CreateEncryptor()
        Deszyfrator = a.CreateDecryptor()
    End Sub

    Private Sub PrzetworzAes(obiekt As ICryptoTransform, dane As Byte())
        Dim poz As Integer = 0

        While poz < dane.Length
            obiekt.TransformBlock(dane, poz, ROZMIAR_BLOKU_AES, dane, poz)
            poz += ROZMIAR_BLOKU_AES
        End While
    End Sub
End Class