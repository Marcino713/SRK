<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndStanZwrotnic
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lstZwrotnice = New SerwerSterujacy.ListaZwrotnic()
        Me.cbAutomatyczne = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lstZwrotnice
        '
        Me.lstZwrotnice.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstZwrotnice.Location = New System.Drawing.Point(12, 35)
        Me.lstZwrotnice.Name = "lstZwrotnice"
        Me.lstZwrotnice.Size = New System.Drawing.Size(776, 403)
        Me.lstZwrotnice.TabIndex = 0
        Me.lstZwrotnice.ZaznaczonaZwrotnica = Nothing
        '
        'cbAutomatyczne
        '
        Me.cbAutomatyczne.AutoSize = True
        Me.cbAutomatyczne.Location = New System.Drawing.Point(12, 12)
        Me.cbAutomatyczne.Name = "cbAutomatyczne"
        Me.cbAutomatyczne.Size = New System.Drawing.Size(160, 17)
        Me.cbAutomatyczne.TabIndex = 1
        Me.cbAutomatyczne.Text = "Automatyczne przestawianie"
        Me.cbAutomatyczne.UseVisualStyleBackColor = True
        '
        'wndStanZwrotnic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.cbAutomatyczne)
        Me.Controls.Add(Me.lstZwrotnice)
        Me.Name = "wndStanZwrotnic"
        Me.Text = "Stan zwrotnic"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstZwrotnice As ListaZwrotnic
    Friend WithEvents cbAutomatyczne As CheckBox
End Class
