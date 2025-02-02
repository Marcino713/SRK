Public Module Narzedzia
    Public Const OPIS_PLIKU_PULPITU As String = "Schemat posterunku ruchu"
    Public Const OPIS_PLIKU_POLACZEN As String = "Połączenia posterunków ruchu"
    Public Const FILTR_PLIKU_PULPITU As String = OPIS_PLIKU_PULPITU & "|*" & Zaleznosci.Pulpit.ROZSZERZENIE_PLIKU
    Public Const FILTR_PLIKU_POLACZEN As String = OPIS_PLIKU_POLACZEN & "|*" & Zaleznosci.PolaczeniaPosterunkow.ROZSZERZENIE_PLIKU

    Private ReadOnly SKROT_TYPU_POSTERUNKU As New Dictionary(Of Zaleznosci.TypPosterunku, String) From {
        {Zaleznosci.TypPosterunku.Inny, ""},
        {Zaleznosci.TypPosterunku.BocznicaStacyjna, "BST"},
        {Zaleznosci.TypPosterunku.BocznicaSzlakowa, "BSZ"},
        {Zaleznosci.TypPosterunku.GrupaTorowTowarowych, "GT"},
        {Zaleznosci.TypPosterunku.Ladownia, "L"},
        {Zaleznosci.TypPosterunku.Mijanka, "M"},
        {Zaleznosci.TypPosterunku.PosterunekBocznicowyStacyjny, "PBST"},
        {Zaleznosci.TypPosterunku.PosterunekBocznicowySzlakowy, "PBSZ"},
        {Zaleznosci.TypPosterunku.PosterunekOdgalezny, "PODG"},
        {Zaleznosci.TypPosterunku.PosterunekOdstepowy, "ODST"},
        {Zaleznosci.TypPosterunku.PrzejscieGraniczne, "PGR"},
        {Zaleznosci.TypPosterunku.PrzystanekOsobowy, "PO"},
        {Zaleznosci.TypPosterunku.PrzystanekSluzbowy, "PS"},
        {Zaleznosci.TypPosterunku.PunktPrzeladunkowy, "PP"},
        {Zaleznosci.TypPosterunku.Stacja, "ST"},
        {Zaleznosci.TypPosterunku.StacjaTechniczna, "STTH"}
    }

    Public Function ZadajPytanie(pytanie As String) As DialogResult
        Return MessageBox.Show(pytanie, "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Public Sub PokazBlad(komunikat As String)
        MessageBox.Show(komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub PokazKomunikat(komunikat As String)
        MessageBox.Show(komunikat, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function ZadajPytanieTrzyodpowiedziowe(pytanie As String) As DialogResult
        Return MessageBox.Show(pytanie, "Pytanie", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
    End Function

    Public Function PobierzZaznaczonyElementNaLiscie(lv As ListView) As ListViewItem
        If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count = 0 Then
            Return Nothing
        Else
            Return lv.SelectedItems(0)
        End If
    End Function

    Public Function PobierzTagZElementuListy(Of T)(lvi As ListViewItem) As T
        If lvi Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(lvi.Tag, T)
        End If
    End Function

    Public Function PobierzOznaczeniePociagu(nr As UInteger, nazwa As String) As String
        Dim ozn As String = nr.ToString
        If Not String.IsNullOrEmpty(nazwa) Then ozn &= " " & nazwa
        Return ozn
    End Function

    Public Function PobierzTytulOkna(pulpit As Zaleznosci.Pulpit, poczatek As String, koniec As String) As String
        Dim sb As New Text.StringBuilder(300)
        sb.Append(poczatek)

        If pulpit.Nazwa <> String.Empty Then
            sb.Append(" - ")
            sb.Append(pulpit.Nazwa)
        End If

        If pulpit.SkrotTelegraficzny <> String.Empty Then
            sb.Append(" (")
            sb.Append(pulpit.SkrotTelegraficzny)
            sb.Append(")"c)
        End If

        If pulpit.Typ <> 0 Then
            sb.Append(" (")
            sb.Append(PobierzTypPosterunku(pulpit.Typ))
            sb.Append(")"c)
        End If

        sb.Append(koniec)
        Return sb.ToString()
    End Function

    Public Function PobierzTypPosterunku(typ As Zaleznosci.TypPosterunku) As String
        Dim lista As New List(Of String)(SKROT_TYPU_POSTERUNKU.Count)

        For Each kv As KeyValuePair(Of Zaleznosci.TypPosterunku, String) In SKROT_TYPU_POSTERUNKU
            If (typ And kv.Key) <> 0 Then
                lista.Add(kv.Value)
            End If
        Next

        lista.Sort()
        Return String.Join(", ", lista).ToLower()
    End Function

    Public Function CzyRozne(a As UShort?, b As UShort?) As Boolean
        Return _
            (a.HasValue And (Not b.HasValue)) OrElse
            ((Not a.HasValue) And b.HasValue) OrElse
            (a.HasValue AndAlso b.HasValue AndAlso a.Value <> b.Value)
    End Function
End Module