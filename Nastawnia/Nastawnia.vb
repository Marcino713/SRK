Imports System.Threading

Public Class wndNastawnia
    Private Const FILTR_PLIKU As String = Zaleznosci.PolaczeniaStacji.OPIS_PLIKU & "|*" & Zaleznosci.PolaczeniaStacji.ROZSZERZENIE_PLIKU
    Private Const CZEKANIE_NA_ZAMKNIECIE As Integer = 2000

    Private WithEvents Klient As New Zaleznosci.KlientTCP
    Private WlaczoneOknoWyboruPost As Boolean = False
    Private Sygnalizatory As New Dictionary(Of UShort, Zaleznosci.Sygnalizator)

    Private actPokazStatus As Action(Of String, Color, Boolean) = AddressOf PokazStatusPolaczenia
    Private actPokazPulpit As Action(Of Zaleznosci.Pulpit) = AddressOf PokazPulpit
    Private actPokazBlad As Action(Of String) = AddressOf PokazBlad
    Private actPokazKomunikat As Action(Of String) = AddressOf PokazKomunikat

    Private Sub wndNastawnia_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not Klient.Uruchomiony Then Exit Sub

        If ZadajPytanie("Zamknąć okno? Spowoduje to rozłączenie z serwerem.") = DialogResult.Yes Then
            Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
            Dim t As New Thread(AddressOf ZamknijPolaczenie)
            t.Start(Klient)
            Klient = Nothing
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub mnuPolaczZSerwerem_Click() Handles mnuPolaczZSerwerem.Click
        Dim wnd As New wndWyborStacji(Klient)
        WlaczoneOknoWyboruPost = True
        wnd.ShowDialog()
        WlaczoneOknoWyboruPost = False
        If wnd.Pulpit IsNot Nothing Then
            actPokazPulpit(wnd.Pulpit)
            actPokazStatus("Połączono", Color.Green, False)
        End If
    End Sub

    Private Sub mnuRozlaczZSerwerem_Click() Handles mnuRozlaczZSerwerem.Click
        If ZadajPytanie("Czy rozłączyć z serwerem?") = DialogResult.Yes Then
            Klient.WyslijZakonczDzialanieKlienta(New Zaleznosci.ZakonczDzialanieKlienta() With {.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaDzialaniaKlienta.ZatrzymanieKlienta})
            actPokazStatus("Rozłączanie...", Color.Blue, False)
        End If
    End Sub

    Private Sub mnuZarzadzajSerwerem_Click() Handles mnuZarzadzajSerwerem.Click
        Dim t As New Thread(AddressOf PokazOknoSerwera)
        t.Start()
    End Sub

    Private Sub mnuKonfiguratorStacji_Click() Handles mnuKonfiguratorStacji.Click
        Dim wnd As New wndKonfiguratorStacji()
        wnd.Show()
    End Sub

    Private Sub mnuNowePolaczenia_Click() Handles mnuNowePolaczenia.Click
        PokazKomunikat("Plik połączeń należy zapisać w tym samym folderze, w którym znajdują się pliki konfiguracji posterunków ruchu.")
        WczytajPolaczenia(New SaveFileDialog, AddressOf Zaleznosci.PolaczeniaStacji.OtworzFolder, "Nie udało się zapisać pliku.")
    End Sub

    Private Sub mnuOtworzPolaczenia_Click() Handles mnuOtworzPolaczenia.Click
        WczytajPolaczenia(New OpenFileDialog, AddressOf Zaleznosci.PolaczeniaStacji.OtworzPlik, "Nie udało się otworzyć pliku.")
    End Sub

    Private Sub plpPulpit_WcisnietoPrzycisk(kostka As Zaleznosci.Kostka) Handles plpPulpit.WcisnietoPrzycisk
        Select Case kostka.Typ

            Case Zaleznosci.TypKostki.RozjazdLewo, Zaleznosci.TypKostki.RozjazdPrawo
                Dim roz As Zaleznosci.Rozjazd = DirectCast(kostka, Zaleznosci.Rozjazd)
                Dim stan As Zaleznosci.UstawienieRozjazduEnum
                If roz.Stan = Zaleznosci.UstawienieZwrotnicy.Wprost Then
                    stan = Zaleznosci.UstawienieRozjazduEnum.Bok
                Else
                    stan = Zaleznosci.UstawienieRozjazduEnum.Wprost
                End If
                Klient.WyslijUstawZwrotnice(New Zaleznosci.UstawZwrotnice() With {.Adres = roz.Adres, .Ustawienie = stan})

            Case Zaleznosci.TypKostki.Przycisk
                Dim prz As Zaleznosci.Przycisk = DirectCast(kostka, Zaleznosci.Przycisk)
                If prz.TypPrzycisku = Zaleznosci.TypPrzyciskuEnum.ZwolnieniePrzebiegow Then
                    Klient.WyslijZwolnijPrzebiegi(New Zaleznosci.ZwolnijPrzebiegi())
                ElseIf prz.TypPrzycisku = Zaleznosci.TypPrzyciskuEnum.SygnalZastepczy
                    If prz.ObslugiwanySygnalizator IsNot Nothing Then
                        Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {
                                                            .Adres = prz.ObslugiwanySygnalizator.Adres,
                                                            .Stan = Zaleznosci.UstawianyStanSygnalizatora.Zastepczy})
                    End If
                End If

            Case Zaleznosci.TypKostki.PrzyciskTor
                Dim prz As Zaleznosci.PrzyciskTor = DirectCast(kostka, Zaleznosci.PrzyciskTor)
                If prz.ObslugiwanySygnalizator IsNot Nothing Then
                    Dim stan As Zaleznosci.UstawianyStanSygnalizatora
                    Select Case prz.TypPrzycisku
                        Case Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorPolsamoczynny
                            stan = Zaleznosci.UstawianyStanSygnalizatora.Zezwalajacy
                        Case Zaleznosci.TypPrzyciskuTorEnum.SygnalizatorManewrowy, Zaleznosci.TypPrzyciskuTorEnum.SygnalManewrowy
                            stan = Zaleznosci.UstawianyStanSygnalizatora.Manewrowy
                    End Select
                    Klient.WyslijUstawStanSygnalizatora(New Zaleznosci.UstawStanSygnalizatora() With {.Adres = prz.ObslugiwanySygnalizator.Adres, .Stan = stan})
                End If

            Case Zaleznosci.TypKostki.Kierunek
                Dim kier As Zaleznosci.Kierunek = DirectCast(kostka, Zaleznosci.Kierunek)
                If kier.NalezyDoOdcinka IsNot Nothing Then
                    Klient.WyslijUstawKierunek(New Zaleznosci.UstawKierunek() With {.Adres = kier.NalezyDoOdcinka.Adres, .Kierunek = kier.KierunekWlaczany})
                End If

        End Select
    End Sub

    Private Sub Klient_ZakonczonoPolaczenie() Handles Klient.ZakonczonoPolaczenie
        Invoke(actPokazStatus, "Rozłączono", Color.Red, True)
        Invoke(actPokazPulpit, New Zaleznosci.Pulpit)
    End Sub

    Private Sub Klient_OdebranoZakonczonoSesjeKlienta(kom As Zaleznosci.ZakonczonoSesjeKlienta) Handles Klient.OdebranoZakonczonoSesjeKlienta
        Invoke(actPokazStatus, "Rozłączono", Color.Red, True)
        Invoke(actPokazPulpit, New Zaleznosci.Pulpit)

        If Not WlaczoneOknoWyboruPost Then
            Dim tresc As String = PrzyczynaZakonczeniaSesjiKlientaToString(kom.Przyczyna)
            If kom.Przyczyna = Zaleznosci.PrzyczynaZakonczeniaSesjiKlienta.RozlaczenieKlienta Then
                Invoke(actPokazKomunikat, tresc)
            Else
                Invoke(actPokazBlad, tresc)
            End If
        End If
    End Sub

    Private Sub Klient_OdebranoZakonczonoDzialanieSerwera(kom As Zaleznosci.ZakonczonoDzialanieSerwera) Handles Klient.OdebranoZakonczonoDzialanieSerwera
        Invoke(actPokazStatus, "Rozłączono", Color.Red, True)
        Invoke(actPokazPulpit, New Zaleznosci.Pulpit)
        If Not WlaczoneOknoWyboruPost Then Invoke(actPokazBlad, "Serwer został zatrzymany.")
    End Sub

    Private Sub Klient_OdebranoZmienionoStanSygnalizatora(kom As Zaleznosci.ZmienionoStanSygnalizatora) Handles Klient.OdebranoZmienionoStanSygnalizatora
        Dim sygn As Zaleznosci.Sygnalizator = Nothing
        If Not Sygnalizatory.TryGetValue(kom.Adres, sygn) Then Exit Sub

        Select Case sygn.Typ
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                Dim s As Zaleznosci.SygnalizatorManewrowy = DirectCast(sygn, Zaleznosci.SygnalizatorManewrowy)
                If kom.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Then
                    s.Stan = Zaleznosci.StanSygnalizatoraManewrowego.BrakWyjazdu
                Else
                    s.Stan = Zaleznosci.StanSygnalizatoraManewrowego.Manewrowy
                End If

            Case Zaleznosci.TypKostki.SygnalizatorSamoczynny
                Dim s As Zaleznosci.SygnalizatorSamoczynny = DirectCast(sygn, Zaleznosci.SygnalizatorSamoczynny)
                If kom.Stan = Zaleznosci.StanSygnalizatora.BrakWyjazdu Then
                    s.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.BrakWyjazdu
                Else
                    s.Stan = Zaleznosci.StanSygnalizatoraSamoczynnego.Zezwalajacy
                End If

            Case Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                Dim s As Zaleznosci.SygnalizatorPolsamoczynny = DirectCast(sygn, Zaleznosci.SygnalizatorPolsamoczynny)
                s.Stan = kom.Stan

        End Select

        plpPulpit.Invalidate()
    End Sub

    Private Sub WczytajPolaczenia(Okno As FileDialog, MetodaOtwierajaca As Func(Of String, Zaleznosci.PolaczeniaStacji), KomunikatBledu As String)
        Okno.Filter = FILTR_PLIKU

        If Okno.ShowDialog = DialogResult.OK Then
            Dim polaczenia As Zaleznosci.PolaczeniaStacji = MetodaOtwierajaca(Okno.FileName)
            If polaczenia Is Nothing Then
                PokazBlad(KomunikatBledu)
            Else
                Dim wnd As New wndKonfiguratorPolaczen(polaczenia)
                wnd.Show()
            End If
        End If
    End Sub

    Private Sub ZamknijPolaczenie(klient As Object)
        Thread.Sleep(CZEKANIE_NA_ZAMKNIECIE)
        CType(klient, Zaleznosci.KlientTCP).Zakoncz(False)
    End Sub

    Private Sub PokazOknoSerwera()
        Dim wnd As New SerwerSterujacy.wndOknoSerwera
        wnd.ShowDialog()
    End Sub

    Private Sub PokazStatusPolaczenia(tekst As String, kolor As Color, moznaPolaczyc As Boolean)
        tslStanPolaczenia.Text = tekst
        tslStanPolaczenia.ForeColor = kolor
        mnuPolaczZSerwerem.Enabled = moznaPolaczyc
        mnuRozlaczZSerwerem.Enabled = Not moznaPolaczyc
    End Sub

    Private Sub PokazPulpit(pulpit As Zaleznosci.Pulpit)
        plpPulpit.Pulpit = pulpit

        Sygnalizatory = pulpit.PobierzSygnalizatory()
    End Sub
End Class