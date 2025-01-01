<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PulpitSterowniczy
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tmrPrzycisk = New System.Windows.Forms.Timer(Me.components)
        Me.tmrMiganie = New System.Windows.Forms.Timer(Me.components)
        Me.ctxMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctmWysrodkuj = New System.Windows.Forms.ToolStripMenuItem()
        Me.tspUrzadzenia = New System.Windows.Forms.ToolStripSeparator()
        Me.tspOdcinekToru = New System.Windows.Forms.ToolStripSeparator()
        Me.ctmBlokadaZwrotnicy = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctmBlokadaSygnalizatora = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctmZamkniecieOdcinka = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctmZerujLicznikOsi = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctmTrybSamoczynny = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrPrzycisk
        '
        '
        'tmrMiganie
        '
        Me.tmrMiganie.Interval = 500
        '
        'ctxMenu
        '
        Me.ctxMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctmBlokadaZwrotnicy, Me.ctmBlokadaSygnalizatora, Me.ctmTrybSamoczynny, Me.tspUrzadzenia, Me.ctmZamkniecieOdcinka, Me.ctmZerujLicznikOsi, Me.tspOdcinekToru, Me.ctmWysrodkuj})
        Me.ctxMenu.Name = "ctxMenu"
        Me.ctxMenu.Size = New System.Drawing.Size(198, 170)
        '
        'ctmWysrodkuj
        '
        Me.ctmWysrodkuj.Name = "ctmWysrodkuj"
        Me.ctmWysrodkuj.Size = New System.Drawing.Size(197, 22)
        Me.ctmWysrodkuj.Text = "Wyśrodkuj"
        '
        'tspUrzadzenia
        '
        Me.tspUrzadzenia.Name = "tspUrzadzenia"
        Me.tspUrzadzenia.Size = New System.Drawing.Size(194, 6)
        '
        'tspOdcinekToru
        '
        Me.tspOdcinekToru.Name = "tspOdcinekToru"
        Me.tspOdcinekToru.Size = New System.Drawing.Size(194, 6)
        '
        'ctmBlokadaZwrotnicy
        '
        Me.ctmBlokadaZwrotnicy.Name = "ctmBlokadaZwrotnicy"
        Me.ctmBlokadaZwrotnicy.Size = New System.Drawing.Size(197, 22)
        Me.ctmBlokadaZwrotnicy.Text = "Blokada zwrotnicy..."
        '
        'ctmBlokadaSygnalizatora
        '
        Me.ctmBlokadaSygnalizatora.Name = "ctmBlokadaSygnalizatora"
        Me.ctmBlokadaSygnalizatora.Size = New System.Drawing.Size(197, 22)
        Me.ctmBlokadaSygnalizatora.Text = "Blokada sygnalizatora..."
        '
        'ctmZamkniecieOdcinka
        '
        Me.ctmZamkniecieOdcinka.Name = "ctmZamkniecieOdcinka"
        Me.ctmZamkniecieOdcinka.Size = New System.Drawing.Size(197, 22)
        Me.ctmZamkniecieOdcinka.Text = "Zamknięcie odcinka..."
        '
        'ctmZerujLicznikOsi
        '
        Me.ctmZerujLicznikOsi.Name = "ctmZerujLicznikOsi"
        Me.ctmZerujLicznikOsi.Size = New System.Drawing.Size(197, 22)
        Me.ctmZerujLicznikOsi.Text = "Zeruj licznik osi..."
        '
        'ctmTrybSamoczynny
        '
        Me.ctmTrybSamoczynny.Name = "ctmTrybSamoczynny"
        Me.ctmTrybSamoczynny.Size = New System.Drawing.Size(197, 22)
        Me.ctmTrybSamoczynny.Text = "Tryb samoczynny..."
        '
        'PulpitSterowniczy
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ContextMenuStrip = Me.ctxMenu
        Me.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Name = "PulpitSterowniczy"
        Me.ctxMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tmrPrzycisk As Timer
    Friend WithEvents tmrMiganie As Timer
    Friend WithEvents ctxMenu As ContextMenuStrip
    Friend WithEvents ctmWysrodkuj As ToolStripMenuItem
    Friend WithEvents ctmBlokadaZwrotnicy As ToolStripMenuItem
    Friend WithEvents ctmBlokadaSygnalizatora As ToolStripMenuItem
    Friend WithEvents tspUrzadzenia As ToolStripSeparator
    Friend WithEvents ctmZamkniecieOdcinka As ToolStripMenuItem
    Friend WithEvents ctmZerujLicznikOsi As ToolStripMenuItem
    Friend WithEvents tspOdcinekToru As ToolStripSeparator
    Friend WithEvents ctmTrybSamoczynny As ToolStripMenuItem
End Class
