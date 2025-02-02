Friend Class wndPredkosciDopuszczalneTorow
    Friend Sub ZmienPredkosc(predkosc As UShort)
        lblPredkoscMax.Text = $"{predkosc} km/godz"
    End Sub

    Friend Sub ZmianaAktywnejZakladki(zakladkaPulpit As Boolean)
        lblBlednaKarta.Visible = Not zakladkaPulpit
    End Sub

    Private Sub wndPredkosciDopuszczalneTorow_Load() Handles Me.Load
        RysujSlupek()
    End Sub

    Private Sub RysujSlupek()
        Dim szer As Integer = pctSkala.Width
        Dim wys As Integer = pctSkala.Height
        Dim wysMax As Integer = wys - 1
        Dim bm As New Bitmap(szer, wys)
        Dim gr As Graphics = Graphics.FromImage(bm)

        For i As Integer = 0 To wysMax
            Dim p As New Pen(Pulpit.KolorSkaliPredkosci(CUShort(i), CUShort(wysMax)))
            gr.DrawLine(p, 0, wysMax - i, szer, wysMax - i)
            p.Dispose()
        Next

        gr.Dispose()
        pctSkala.Image = bm
    End Sub
End Class