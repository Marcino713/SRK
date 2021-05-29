Public Class wndEdytorPowierzchni
    Private Const NAZWA_DODAJ As String = "Dodawanie kostek"
    Private Const NAZWA_USUN As String = "Usuwanie kostek"

    Public KierunekEdycji As Zaleznosci.KierunekEdycjiPulpitu
    Public LiczbaKostek As Integer

    Public Sub New(Typ As TypEdycji)
        InitializeComponent()
        If Typ = TypEdycji.Dodaj Then Text = NAZWA_DODAJ
        If Typ = TypEdycji.Usun Then Text = NAZWA_USUN
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

    Public Enum TypEdycji
        Dodaj
        Usun
    End Enum

End Class