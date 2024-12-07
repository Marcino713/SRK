<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndStanPulpitu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Pulpit1 As Zaleznosci.Pulpit = New Zaleznosci.Pulpit()
        Me.plpPulpit = New Pulpit.PulpitSterowniczy()
        Me.stStan = New System.Windows.Forms.StatusStrip()
        Me.tslStanPolaczenia = New System.Windows.Forms.ToolStripStatusLabel()
        Me.stStan.SuspendLayout()
        Me.SuspendLayout()
        '
        'plpPulpit
        '
        Me.plpPulpit.AllowDrop = True
        Me.plpPulpit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plpPulpit.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.plpPulpit.DodatkoweObiekty = Pulpit.Dodatki.DodatkoweObiektyTrybDzialania.LicznikiOsi
        Me.plpPulpit.Location = New System.Drawing.Point(0, 0)
        Me.plpPulpit.MozliwoscWcisnieciaPrzycisku = False
        Me.plpPulpit.MozliwoscZaznaczeniaKostki = True
        Me.plpPulpit.MozliwoscZaznaczeniaLamp = False
        Me.plpPulpit.MozliwoscZaznaczeniaOdcinka = False
        Me.plpPulpit.Name = "plpPulpit"
        Me.plpPulpit.projDodatkoweObiekty = Pulpit.Dodatki.RysujDodatkoweObiekty.Nic
        Me.plpPulpit.projZaznaczonaLampa = Nothing
        Me.plpPulpit.projZaznaczonyLicznik = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazd = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazdAutomatyzacja = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazdRogatka = Nothing
        Me.plpPulpit.projZaznaczonyPrzejazdSygnDrog = Nothing
        Me.plpPulpit.Przesuniecie = New System.Drawing.Point(0, 0)
        Pulpit1.Adres = CType(0US, UShort)
        Pulpit1.Nazwa = ""
        Me.plpPulpit.Pulpit = Pulpit1
        Me.plpPulpit.Size = New System.Drawing.Size(800, 425)
        Me.plpPulpit.TabIndex = 0
        Me.plpPulpit.WarunekZaznaczeniaKostki = Nothing
        Me.plpPulpit.ZaznaczonaKostka = Nothing
        Me.plpPulpit.ZaznaczonyOdcinek = Nothing
        '
        'stStan
        '
        Me.stStan.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslStanPolaczenia})
        Me.stStan.Location = New System.Drawing.Point(0, 428)
        Me.stStan.Name = "stStan"
        Me.stStan.Size = New System.Drawing.Size(800, 22)
        Me.stStan.TabIndex = 1
        Me.stStan.Text = "StatusStrip1"
        '
        'tslStanPolaczenia
        '
        Me.tslStanPolaczenia.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tslStanPolaczenia.ForeColor = System.Drawing.Color.Red
        Me.tslStanPolaczenia.Name = "tslStanPolaczenia"
        Me.tslStanPolaczenia.Size = New System.Drawing.Size(95, 17)
        Me.tslStanPolaczenia.Text = "Brak połączenia"
        '
        'wndStanPulpitu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.stStan)
        Me.Controls.Add(Me.plpPulpit)
        Me.Name = "wndStanPulpitu"
        Me.Text = "Stan pulpitu"
        Me.stStan.ResumeLayout(False)
        Me.stStan.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents plpPulpit As Pulpit.PulpitSterowniczy
    Friend WithEvents stStan As StatusStrip
    Friend WithEvents tslStanPolaczenia As ToolStripStatusLabel
End Class
