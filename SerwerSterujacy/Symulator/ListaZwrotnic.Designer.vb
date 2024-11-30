<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ListaZwrotnic
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
        Me.vscPrzewin = New System.Windows.Forms.VScrollBar()
        Me.SuspendLayout()
        '
        'vscPrzewin
        '
        Me.vscPrzewin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vscPrzewin.LargeChange = 1
        Me.vscPrzewin.Location = New System.Drawing.Point(483, 61)
        Me.vscPrzewin.Name = "vscPrzewin"
        Me.vscPrzewin.Size = New System.Drawing.Size(17, 139)
        Me.vscPrzewin.TabIndex = 0
        Me.vscPrzewin.Visible = False
        '
        'ListaZwrotnic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.vscPrzewin)
        Me.Name = "ListaZwrotnic"
        Me.Size = New System.Drawing.Size(500, 200)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents vscPrzewin As VScrollBar
End Class
