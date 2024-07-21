Friend Class wndWyborPociagu
    Private Const STATUS_WYBIERANIE As String = "Wybieranie..."
    Private Const STAN_POC_WOLNY As String = "Wolny"
    Private Const STAN_POC_ZAJETY As String = "Zajęty"

    Private WithEvents Klient As Zaleznosci.KlientTCP
    Private Pociagi As Zaleznosci.DaneWybieralnegoPociagu()
    Private WybieranyPociag As Zaleznosci.DaneWybieralnegoPociagu
    Private PociagiSlownik As New Dictionary(Of UInteger, ListViewItem)
    Private UtworzonoUchwytOkno As Boolean = False
    Private UtworzonoUchwytEtykieta As Boolean = False
    Private UtworzonoUchwytLista As Boolean = False
    Private WyslanoZapytanie As Boolean = False

    Private actPokazPociagi As Action = AddressOf PokazListePociagow
    Private actPokazStan As Action(Of String) = Sub(stan) lblStan.Text = stan
    Private actPokazDostepnoscKontrolek As Action = AddressOf PokazDostepnoscKontrolek
    Private actPokazBlad As Action(Of String) = AddressOf Wspolne.PokazBlad
    Private actUsunPociag As Action(Of UInteger) = AddressOf UsunPociag
    Private actPokazZajetoscPociagu As Action(Of UInteger) = AddressOf PokazZajetoscPociagu
    Private actZamknijOkno As Action = Sub() Close()

    Private _WybranyPociagNr As UInteger? = Nothing
    Friend ReadOnly Property WybranyPociagNr As UInteger?
        Get
            Return _WybranyPociagNr
        End Get
    End Property

    Private _WybranyPociagNazwa As String
    Friend ReadOnly Property WybranyPociagNazwa As String
        Get
            Return _WybranyPociagNazwa
        End Get
    End Property

    Private _WybranyPociagPredkoscMaksymalna As UShort
    Friend ReadOnly Property WybranyPociagPredkoscMaksymalna As UShort
        Get
            Return _WybranyPociagPredkoscMaksymalna
        End Get
    End Property

    Friend Sub New(klient As Zaleznosci.KlientTCP)
        InitializeComponent()

        Me.Klient = klient
    End Sub

    Private Sub wndWyborPociagu_FormClosed() Handles Me.FormClosed
        Klient = Nothing
    End Sub

    Private Sub wndWyborPociagu_HandleCreated() Handles Me.HandleCreated
        UtworzonoUchwytOkno = True
        WyslijZapytanie()
    End Sub

    Private Sub lblStan_HandleCreated() Handles lblStan.HandleCreated
        UtworzonoUchwytEtykieta = True
        WyslijZapytanie()
    End Sub

    Private Sub lvPociagi_HandleCreated() Handles lvPociagi.HandleCreated
        UtworzonoUchwytLista = True
        WyslijZapytanie()
    End Sub

    Private Sub lvPociagi_SelectedIndexChanged() Handles lvPociagi.SelectedIndexChanged
        actPokazDostepnoscKontrolek()
    End Sub

    Private Sub lvPociagi_DoubleClick() Handles lvPociagi.DoubleClick
        WybierzPociag()
    End Sub

    Private Sub btnWybierz_Click() Handles btnWybierz.Click
        WybierzPociag()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        actZamknijOkno()
    End Sub

    Private Sub Klient_OdebranoPobranoPociagi(kom As Zaleznosci.PobranoPociagi) Handles Klient.OdebranoPobranoPociagi
        Pociagi = kom.Pociagi
        Invoke(actPokazStan, "")
        Invoke(actPokazPociagi)
    End Sub

    Private Sub Klient_OdebranoWybranoPociag(kom As Zaleznosci.WybranoPociag) Handles Klient.OdebranoWybranoPociag
        If WybieranyPociag Is Nothing OrElse WybieranyPociag.Numer <> kom.NrPociagu Then Exit Sub

        If kom.Stan = Zaleznosci.StanWybranegoPociagu.Wybrany Then
            _WybranyPociagNr = WybieranyPociag.Numer
            _WybranyPociagNazwa = WybieranyPociag.Nazwa
            _WybranyPociagPredkoscMaksymalna = WybieranyPociag.PredkoscMaksymalna
            Invoke(actZamknijOkno)
        Else

            Dim bl As String

            If kom.Stan = Zaleznosci.StanWybranegoPociagu.Zajety Then
                bl = "Pociąg jest sterowany przez inny posterunek."
                Invoke(actPokazZajetoscPociagu, kom.NrPociagu)
            ElseIf kom.Stan = Zaleznosci.StanWybranegoPociagu.Niesterowalny Then
                bl = "Pociąg nie posiada napędu i nie może być sterowany."
                Invoke(actUsunPociag, kom.NrPociagu)
            Else
                bl = "Nie znaleziono pociągu o podanym numerze."
                Invoke(actUsunPociag, kom.NrPociagu)
            End If

            WybieranyPociag = Nothing
            Invoke(actPokazStan, "")
            Invoke(actPokazDostepnoscKontrolek)
            Invoke(actPokazBlad, bl)

        End If
    End Sub

    Private Sub WybierzPociag()
        Dim poc As Zaleznosci.DaneWybieralnegoPociagu = PobierzZaznaczonyPociag()
        If poc Is Nothing OrElse poc.Stan = Zaleznosci.StanWybieralnegoPociagu.Zajety Then Exit Sub

        WybieranyPociag = poc
        actPokazStan(STATUS_WYBIERANIE)
        actPokazDostepnoscKontrolek()
        Klient.WyslijWybierzPociag(New Zaleznosci.WybierzPociag With {.NrPociagu = poc.Numer})
    End Sub

    Private Sub WyslijZapytanie()
        If UtworzonoUchwytOkno And UtworzonoUchwytEtykieta And UtworzonoUchwytLista And Not WyslanoZapytanie Then
            WyslanoZapytanie = True
            Klient.WyslijPobierzPociagi(New Zaleznosci.PobierzPociagi())
        End If
    End Sub

    Private Sub PokazListePociagow()
        PociagiSlownik.Clear()
        lvPociagi.Items.Clear()
        If Pociagi Is Nothing Then Exit Sub
        Dim pocEn As IEnumerable(Of Zaleznosci.DaneWybieralnegoPociagu) = Pociagi.OrderBy(Function(x) x.Numer)

        For Each poc As Zaleznosci.DaneWybieralnegoPociagu In pocEn
            Dim lvi As New ListViewItem({
                    poc.Numer.ToString,
                    If(poc.Nazwa, ""),
                    If(poc.PredkoscMaksymalna = 0, "Niezdefiniowana", poc.PredkoscMaksymalna.ToString),
                    If(poc.Stan = Zaleznosci.StanWybieralnegoPociagu.Wolny, STAN_POC_WOLNY, STAN_POC_ZAJETY),
                    If(poc.DodajacyPosterunek, ""),
                    If(poc.Lokalizacja, "")
                    }
                ) With {.Tag = poc}

            PociagiSlownik.Add(poc.Numer, lvi)
            lvPociagi.Items.Add(lvi)
        Next
    End Sub

    Private Sub PokazDostepnoscKontrolek()
        Dim poc As Zaleznosci.DaneWybieralnegoPociagu = PobierzZaznaczonyPociag()
        Dim dostepne As Boolean = WybieranyPociag Is Nothing AndAlso poc IsNot Nothing AndAlso poc.Stan = Zaleznosci.StanWybieralnegoPociagu.Wolny

        btnWybierz.Enabled = dostepne
    End Sub

    Private Function PobierzZaznaczonyPociag() As Zaleznosci.DaneWybieralnegoPociagu
        If lvPociagi.SelectedItems Is Nothing OrElse lvPociagi.SelectedItems.Count = 0 Then Return Nothing

        Return CType(lvPociagi.SelectedItems(0).Tag, Zaleznosci.DaneWybieralnegoPociagu)
    End Function

    Private Sub UsunPociag(nr As UInteger)
        Dim lvi As ListViewItem = Nothing

        If PociagiSlownik.TryGetValue(nr, lvi) Then
            PociagiSlownik.Remove(nr)
            lvPociagi.Items.Remove(lvi)
        End If
    End Sub

    Private Sub PokazZajetoscPociagu(nr As UInteger)
        Dim lvi As ListViewItem = Nothing

        If PociagiSlownik.TryGetValue(nr, lvi) Then
            lvi.SubItems(3).Text = STAN_POC_ZAJETY
            CType(lvi.Tag, Zaleznosci.DaneWybieralnegoPociagu).Stan = Zaleznosci.StanWybieralnegoPociagu.Zajety
        End If
    End Sub
End Class