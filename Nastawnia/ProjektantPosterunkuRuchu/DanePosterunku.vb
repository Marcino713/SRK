Imports ObiektTypu = Wspolne.ObiektComboBox(Of Zaleznosci.TypPosterunku)

Public Class wndDanePosterunku
    Private Shared ReadOnly NAZWA_TYPU_POSTERUNKU As New Dictionary(Of Zaleznosci.TypPosterunku, String) From {
        {Zaleznosci.TypPosterunku.BocznicaStacyjna, "Bocznica stacyjna"},
        {Zaleznosci.TypPosterunku.BocznicaSzlakowa, "Bocznica szlakowa"},
        {Zaleznosci.TypPosterunku.GrupaTorowTowarowych, "Grupa torów towarowych"},
        {Zaleznosci.TypPosterunku.Ladownia, "Ładownia"},
        {Zaleznosci.TypPosterunku.Mijanka, "Mijanka"},
        {Zaleznosci.TypPosterunku.PosterunekBocznicowyStacyjny, "Posterunek bocznicowy stacyjny"},
        {Zaleznosci.TypPosterunku.PosterunekBocznicowySzlakowy, "Posterunek bocznicowy szlakowy"},
        {Zaleznosci.TypPosterunku.PosterunekOdgalezny, "Posterunek odgałęźny"},
        {Zaleznosci.TypPosterunku.PosterunekOdstepowy, "Posterunek odstępowy"},
        {Zaleznosci.TypPosterunku.PrzejscieGraniczne, "Przejście graniczne"},
        {Zaleznosci.TypPosterunku.PrzystanekOsobowy, "Przystanek osobowy"},
        {Zaleznosci.TypPosterunku.PrzystanekSluzbowy, "Przystanek służbowy"},
        {Zaleznosci.TypPosterunku.PunktPrzeladunkowy, "Punkt przeładunkowy"},
        {Zaleznosci.TypPosterunku.Stacja, "Stacja"},
        {Zaleznosci.TypPosterunku.StacjaTechniczna, "Stacja techniczna"}
    }

    Private p As Zaleznosci.Pulpit

    Public Sub New(p As Zaleznosci.Pulpit)
        InitializeComponent()
        Me.p = p
        txtAdres.Text = p.Adres.ToString()
        txtNazwa.Text = p.Nazwa
        txtSkrotTelegraficzny.Text = p.SkrotTelegraficzny
        PokazTypyPosterunkow(p.Typ)
        lblDataUtworzenia.Text = p.DataUtworzenia.ToString(DATA_FORMAT)
    End Sub

    Private Sub btnOK_Click() Handles btnOK.Click
        Dim adres As UShort

        If Not UShort.TryParse(txtAdres.Text, adres) Then
            Wspolne.PokazBlad("Adres posterunku musi być liczbą całkowitą dodatnią.")
            Exit Sub
        End If

        p.Adres = adres
        p.Nazwa = txtNazwa.Text
        p.SkrotTelegraficzny = txtSkrotTelegraficzny.Text
        UstawTypPosterunku()

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnAnuluj_Click() Handles btnAnuluj.Click
        Close()
    End Sub

    Private Sub PokazTypyPosterunkow(zaznaczone As Zaleznosci.TypPosterunku)
        Dim lista As New List(Of ObiektTypuPosterunku)(NAZWA_TYPU_POSTERUNKU.Count)
        Dim op As ObiektTypuPosterunku

        For Each kv As KeyValuePair(Of Zaleznosci.TypPosterunku, String) In NAZWA_TYPU_POSTERUNKU
            lista.Add(New ObiektTypuPosterunku() With {
                      .Obiekt = New ObiektTypu(kv.Key, kv.Value),
                      .Zaznaczony = (zaznaczone And kv.Key) <> 0
            })
        Next

        lista = lista.OrderBy(Function(o) o.Obiekt.Tekst).ToList()

        For i As Integer = 0 To lista.Count - 1
            op = lista(i)
            clbTyp.Items.Add(op.Obiekt)
            If op.Zaznaczony Then clbTyp.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub UstawTypPosterunku()
        Dim o As ObiektTypu
        p.Typ = Zaleznosci.TypPosterunku.Inny

        If clbTyp.CheckedItems IsNot Nothing Then
            For Each t As Object In clbTyp.CheckedItems
                o = CType(t, ObiektTypu)
                p.Typ = p.Typ Or o.Wartosc
            Next
        End If
    End Sub

    Private Class ObiektTypuPosterunku
        Public Obiekt As ObiektTypu
        Public Zaznaczony As Boolean
    End Class
End Class