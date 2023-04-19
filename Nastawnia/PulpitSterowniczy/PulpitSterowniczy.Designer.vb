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
        Me.ctxMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctmWysrodkuj})
        Me.ctxMenu.Name = "ctxMenu"
        Me.ctxMenu.Size = New System.Drawing.Size(131, 26)
        '
        'ctmWysrodkuj
        '
        Me.ctmWysrodkuj.Name = "ctmWysrodkuj"
        Me.ctmWysrodkuj.Size = New System.Drawing.Size(130, 22)
        Me.ctmWysrodkuj.Text = "Wyśrodkuj"
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
End Class
