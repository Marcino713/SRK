Public Class wndEdytorRozmiaruPowierzchni
    Private Const NAZWA_DODAJ As String = "Dodawanie kostek"
    Private Const NAZWA_USUN As String = "Usuwanie kostek"

    Public Property KierunekEdycji As Zaleznosci.KierunekEdycjiPulpitu
    Public Property LiczbaKostek As Integer

    Private typ As TypEdycji
    Private szerokoscObecna As Integer
    Private wysokoscObecna As Integer

    Public Sub New(Typ As TypEdycji, Szerokosc As Integer, Wysokosc As Integer)
        InitializeComponent()
        If Typ = TypEdycji.Dodaj Then Text = NAZWA_DODAJ
        If Typ = TypEdycji.Usun Then Text = NAZWA_USUN
        Me.typ = Typ
        szerokoscObecna = Szerokosc
        wysokoscObecna = Wysokosc
        rbGora.Checked = True
    End Sub

    Private Sub rbGoraDol_CheckedChanged() Handles rbGora.CheckedChanged, rbDol.CheckedChanged
        UstawMaksimum(wysokoscObecna)
    End Sub

    Private Sub rbPrawoLewo_CheckedChanged() Handles rbPrawo.CheckedChanged, rbLewo.CheckedChanged
        UstawMaksimum(szerokoscObecna)
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        KierunekEdycji = Zaleznosci.KierunekEdycjiPulpitu.Lewo
        If rbGora.Checked Then KierunekEdycji = Zaleznosci.KierunekEdycjiPulpitu.Gora
        If rbPrawo.Checked Then KierunekEdycji = Zaleznosci.KierunekEdycjiPulpitu.Prawo
        If rbDol.Checked Then KierunekEdycji = Zaleznosci.KierunekEdycjiPulpitu.Dol

        LiczbaKostek = CInt(numLiczbaKostek.Value)

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub UstawMaksimum(max As Integer)
        If typ = TypEdycji.Usun Then numLiczbaKostek.Maximum = max - 1
    End Sub

    Public Enum TypEdycji
        Dodaj
        Usun
    End Enum

End Class