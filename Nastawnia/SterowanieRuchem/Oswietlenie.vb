Friend Class wndOswietlenie
    Private Klient As Zaleznosci.KlientTCP
    Private WithEvents Pulpit As Pulpit.PulpitSterowniczy
    Private WszystkieLampy As Dictionary(Of UShort, Zaleznosci.Lampa)
    Private ZaznaczoneLampy As New HashSet(Of Zaleznosci.Lampa)

    Private actOdswiezJasnosc As Action(Of Byte) = AddressOf OdswiezJasnosc

    Private _zaznLampaAdres As Integer = -1
    Friend ReadOnly Property ZaznaczonaLampa As Integer
        Get
            Return _zaznLampaAdres
        End Get
    End Property

    Public Sub New(klient As Zaleznosci.KlientTCP, pulpit As Pulpit.PulpitSterowniczy, lampy As Dictionary(Of UShort, Zaleznosci.Lampa))
        InitializeComponent()

        Me.Klient = klient
        Me.Pulpit = pulpit
        WszystkieLampy = lampy
    End Sub

    Public Sub OdswiezJasnoscZaznaczonejLampy(adres As UShort)
        Dim l As Zaleznosci.Lampa = Nothing

        If WszystkieLampy.TryGetValue(adres, l) Then
            Invoke(actOdswiezJasnosc, l.PobierzJasnosc)
        End If
    End Sub

    Private Sub wndOswietlenie_FormClosing() Handles Me.FormClosing
        Pulpit = Nothing
    End Sub

    Private Sub trbJasnoscUstawiana_Scroll() Handles trbJasnoscUstawiana.Scroll
        If ZaznaczoneLampy.Count = 0 Then Exit Sub

        Dim jasnosc As Byte = CByte(trbJasnoscUstawiana.Value)
        Dim tab(ZaznaczoneLampy.Count - 1) As Zaleznosci.JasnoscLampy
        Dim i As Integer = 0
        lblJasnoscUstawiana.Text = jasnosc.ToString

        For Each l As Zaleznosci.Lampa In ZaznaczoneLampy
            tab(i) = New Zaleznosci.JasnoscLampy With {.Adres = l.Adres, .Jasnosc = jasnosc}
            l.ZakolejkujZmianeJasnosci(jasnosc)
            i += 1
        Next

        Klient.WyslijUstawJasnoscLamp(New Zaleznosci.UstawJasnoscLamp() With {.Jasnosci = tab})
    End Sub

    Private Sub Pulpit_ZmianaZaznaczeniaOstatniejLampy(lampa As Zaleznosci.Lampa) Handles Pulpit.ZmianaZaznaczeniaOstatniejLampy
        Dim adr As String = ""
        Dim jasnosc As Integer = 0
        _zaznLampaAdres = -1

        If lampa IsNot Nothing Then
            adr = lampa.Adres.ToString
            jasnosc = lampa.PobierzJasnosc
            _zaznLampaAdres = lampa.Adres
        End If

        lblAdres.Text = adr
        lblStanLampy.Text = jasnosc.ToString
        trbStanLampy.Value = jasnosc
    End Sub

    Private Sub Pulpit_ZmianaZaznaczeniaLamp(lampy As HashSet(Of Zaleznosci.Lampa)) Handles Pulpit.ZmianaZaznaczeniaLamp
        trbJasnoscUstawiana.Enabled = lampy.Count > 0

        Dim lampySort As Zaleznosci.Lampa() = lampy.OrderBy(Function(k) k.Adres).ToArray
        ZaznaczoneLampy = lampy

        lvAdresy.Items.Clear()
        For Each l As Zaleznosci.Lampa In lampySort
            lvAdresy.Items.Add(l.Adres.ToString)
        Next
    End Sub

    Private Sub OdswiezJasnosc(jasnosc As Byte)
        trbStanLampy.Value = jasnosc
        lblStanLampy.Text = jasnosc.ToString
    End Sub
End Class