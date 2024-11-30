Friend Class wndStanSygnalizatorow
    Private Const SZER_SYGN As Integer = 80
    Private Const WYS_SYGN As Integer = 215
    Private Const WYS_NAZWA As Integer = 30

    Private Const SWIATLA_DODATKOWE As Zaleznosci.DostepneSwiatlaEnum = Zaleznosci.DostepneSwiatlaEnum.ZielonyPas Or
        Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas Or
        Zaleznosci.DostepneSwiatlaEnum.WskaznikKierunkuPrzeciwnego

    Private oknoSymulatora As wndSymulator
    Private pulpit As Zaleznosci.Pulpit
    Private sygnKontrolki As New List(Of PokazywaczSygnalizatora)
    Private sygnalizatory As New Dictionary(Of UShort, Sygnalizator)
    Private zaznaczonySygn As PokazywaczSygnalizatora
    Private migacz As Migacz

    Friend Sub New(oknoSymulatora As wndSymulator, pulpit As Zaleznosci.Pulpit, migacz As Migacz)
        InitializeComponent()

        Text = $"{Text} - {pulpit.Nazwa}"
        Me.oknoSymulatora = oknoSymulatora
        Me.pulpit = pulpit
        Me.migacz = migacz

        PokazSygnalizatory()
    End Sub

    Friend Sub UstawStanSygnalizatora(adres As UShort, stan As UShort)
        Dim sygn As Sygnalizator = Nothing

        If sygnalizatory.TryGetValue(adres, sygn) Then
            sygn.UstawSwiatla(stan)
        End If
    End Sub

    Friend Sub ZaznaczSygnalizator(adres As UShort?)
        If adres.HasValue Then
            If zaznaczonySygn?.Sygnalizator?.Kostka IsNot Nothing AndAlso zaznaczonySygn.Sygnalizator.Kostka.Adres <> adres.Value Then
                zaznaczonySygn.Zaznaczony = False
            End If

            zaznaczonySygn = sygnalizatory(adres.Value).Kontrolka
            zaznaczonySygn.Zaznaczony = True
        Else
            If zaznaczonySygn IsNot Nothing Then
                zaznaczonySygn.Zaznaczony = False
                zaznaczonySygn = Nothing
            End If
        End If
    End Sub

    Private Sub wndStanSygnalizatorow_FormClosing() Handles Me.FormClosing
        For i As Integer = 0 To sygnKontrolki.Count - 1
            sygnKontrolki(i).Sygnalizator.Usun()
            RemoveHandler sygnKontrolki(i).ZmianaZaznaczenia, AddressOf PokazywaczSygnalizatora_ZmianaZaznaczenia
        Next

        oknoSymulatora.UsunOknoSygnalizacji(pulpit.Adres)
    End Sub

    Private Sub PokazywaczSygnalizatora_ZmianaZaznaczenia(sygn As PokazywaczSygnalizatora, stan As Boolean)
        If zaznaczonySygn IsNot Nothing And zaznaczonySygn IsNot sygn Then
            zaznaczonySygn.Zaznaczony = False
            zaznaczonySygn = Nothing
        End If

        If stan Then zaznaczonySygn = sygn

        oknoSymulatora.ZaznaczonoKostke(pulpit.Adres, If(stan, sygn.Sygnalizator.Kostka, Nothing))
    End Sub

    Private Sub PokazSygnalizatory()
        Dim sygn As New List(Of Zaleznosci.Sygnalizator)

        pulpit.PrzeiterujKostki(Sub(x, y, k)
                                    Dim s As Zaleznosci.Sygnalizator = TryCast(k, Zaleznosci.Sygnalizator)
                                    If s IsNot Nothing Then sygn.Add(s)
                                End Sub)

        Dim sygnPosortowane As IEnumerable(Of Zaleznosci.Sygnalizator) = sygn.OrderBy(Function(s) s.Nazwa)
        Dim gr As Graphics = CreateGraphics()
        For Each s As Zaleznosci.Sygnalizator In sygnPosortowane
            DodajSygnalizator(s, gr)
        Next
        gr.Dispose()
    End Sub

    Private Sub DodajSygnalizator(kostka As Zaleznosci.Sygnalizator, gr As Graphics)
        Dim sygn As Sygnalizator = PobierzSygnalizator(kostka)
        Dim pnl As New Panel With {.Size = New Size(SZER_SYGN, WYS_SYGN + WYS_NAZWA)}
        Dim lbl As New Label With {.Text = kostka.Nazwa}
        Dim wyswSygn As New PokazywaczSygnalizatora With {.Sygnalizator = sygn, .Size = New Size(SZER_SYGN, WYS_SYGN)}
        Dim rozm As SizeF = gr.MeasureString(kostka.Nazwa, lbl.Font)

        lbl.Font = New Font(lbl.Font, FontStyle.Bold)
        lbl.Location = New Point(CInt((SZER_SYGN - rozm.Width) / 2.0F), WYS_SYGN)

        If Not sygnalizatory.ContainsKey(kostka.Adres) Then
            sygnalizatory.Add(kostka.Adres, sygn)
        End If

        AddHandler wyswSygn.ZmianaZaznaczenia, AddressOf PokazywaczSygnalizatora_ZmianaZaznaczenia

        sygnKontrolki.Add(wyswSygn)
        pnl.Controls.Add(wyswSygn)
        pnl.Controls.Add(lbl)
        flpSygnalizatory.Controls.Add(pnl)
    End Sub

    Private Function PobierzSygnalizator(kostka As Zaleznosci.Sygnalizator) As Sygnalizator
        Dim sygn As Sygnalizator
        Dim swiatla As List(Of SwiatloSygnalizatora)

        Select Case kostka.Typ
            Case Zaleznosci.TypKostki.SygnalizatorManewrowy
                swiatla = UtworzSwiatlaSygnManewrowego()
                sygn = New SygnalizatorPodstawowy(migacz, swiatla, swiatla.Count)

            Case Zaleznosci.TypKostki.SygnalizatorPolsamoczynny
                sygn = UtworzSygnalizatorPolsamoczynny(CType(kostka, Zaleznosci.SygnalizatorPolsamoczynny).DostepneSwiatla)

            Case Zaleznosci.TypKostki.SygnalizatorSamoczynny
                swiatla = UtworzSwiatlaSygnSamoczynnego(Zaleznosci.StawnoscSBL.Czterostawna)
                sygn = New SygnalizatorPodstawowy(migacz, swiatla, swiatla.Count)

            Case Zaleznosci.TypKostki.SygnalizatorPowtarzajacy
                swiatla = UtworzSwiatlaSygnPowtarzajacego()
                sygn = New SygnalizatorPodstawowy(migacz, swiatla, swiatla.Count)

            Case Zaleznosci.TypKostki.SygnalizatorOstrzegawczyPrzejazdowy
                sygn = New SygnalizatorOstrzegawczyPrzejazdowy(migacz, UtworzSwiatlaSygnTOP)

            Case Else
                Throw New ArgumentException("Nieznany typ sygnalizatora")
        End Select

        sygn.Kostka = kostka
        Return sygn
    End Function

    Private Function UtworzSwiatlaSygnManewrowego() As List(Of SwiatloSygnalizatora)
        Return New List(Of SwiatloSygnalizatora) From {SwiatloSygnalizatora.Niebieskie, SwiatloSygnalizatora.Biale}
    End Function

    Private Function UtworzSwiatlaSygnPowtarzajacego() As List(Of SwiatloSygnalizatora)
        Return New List(Of SwiatloSygnalizatora) From {SwiatloSygnalizatora.Pomaranczowe, SwiatloSygnalizatora.Zielone, SwiatloSygnalizatora.Biale}
    End Function

    Private Function UtworzSwiatlaSygnTOP() As List(Of SwiatloSygnalizatora)
        Return New List(Of SwiatloSygnalizatora) From {SwiatloSygnalizatora.Biale, SwiatloSygnalizatora.Biale, SwiatloSygnalizatora.Pomaranczowe, SwiatloSygnalizatora.Pomaranczowe}
    End Function

    Private Function UtworzSwiatlaSygnSamoczynnego(stawnosc As Zaleznosci.StawnoscSBL) As List(Of SwiatloSygnalizatora)
        If stawnosc = Zaleznosci.StawnoscSBL.Dwustawna Then
            Return New List(Of SwiatloSygnalizatora) From {SwiatloSygnalizatora.Czerwone, SwiatloSygnalizatora.Zielone}
        Else
            Return New List(Of SwiatloSygnalizatora) From {SwiatloSygnalizatora.Zielone, SwiatloSygnalizatora.Czerwone, SwiatloSygnalizatora.Pomaranczowe}
        End If
    End Function

    Private Function UtworzSygnalizatorPolsamoczynny(konf As Zaleznosci.DostepneSwiatlaEnum) As SygnalizatorPodstawowy
        Dim swiatla As New List(Of SwiatloSygnalizatora)
        Dim liczba As Integer
        Dim sygn As SygnalizatorPodstawowy

        If (konf And Zaleznosci.DostepneSwiatlaEnum.Zielone) <> 0 Then swiatla.Add(SwiatloSygnalizatora.Zielone)
        If (konf And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweGora) <> 0 Then swiatla.Add(SwiatloSygnalizatora.Pomaranczowe)
        If (konf And Zaleznosci.DostepneSwiatlaEnum.Czerwone) <> 0 Then swiatla.Add(SwiatloSygnalizatora.Czerwone)
        If (konf And Zaleznosci.DostepneSwiatlaEnum.PomaranczoweDol) <> 0 Then swiatla.Add(SwiatloSygnalizatora.Pomaranczowe)
        If (konf And Zaleznosci.DostepneSwiatlaEnum.Biale) <> 0 Then swiatla.Add(SwiatloSygnalizatora.Biale)

        liczba = swiatla.Count

        If (konf And SWIATLA_DODATKOWE) = 0 Then
            sygn = New SygnalizatorPodstawowy(migacz, swiatla, liczba)
        Else
            Dim dodSwiatla As New DodatkoweSwiatlaSygnalizatora

            If (konf And Zaleznosci.DostepneSwiatlaEnum.ZielonyPas) <> 0 Then
                dodSwiatla.ZielonyPas = swiatla.Count
                swiatla.Add(SwiatloSygnalizatora.Zielone)
            End If

            If (konf And Zaleznosci.DostepneSwiatlaEnum.PomaranczowyPas) <> 0 Then
                dodSwiatla.PomaranczowyPas = swiatla.Count
                swiatla.Add(SwiatloSygnalizatora.Pomaranczowe)
            End If

            If (konf And Zaleznosci.DostepneSwiatlaEnum.WskaznikKierunkuPrzeciwnego) <> 0 Then
                dodSwiatla.WskaznikKierPrzeciwnego = swiatla.Count
                swiatla.Add(SwiatloSygnalizatora.Biale)
            End If

            sygn = New SygnalizatorZeWskaznikami(migacz, swiatla, liczba, dodSwiatla)
        End If

        Return sygn
    End Function
End Class