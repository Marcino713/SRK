<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wndStanSygnalizatorow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.flpSygnalizatory = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'flpSygnalizatory
        '
        Me.flpSygnalizatory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpSygnalizatory.AutoScroll = True
        Me.flpSygnalizatory.Location = New System.Drawing.Point(12, 12)
        Me.flpSygnalizatory.Name = "flpSygnalizatory"
        Me.flpSygnalizatory.Size = New System.Drawing.Size(776, 426)
        Me.flpSygnalizatory.TabIndex = 0
        '
        'wndStanSygnalizatorow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.flpSygnalizatory)
        Me.Name = "wndStanSygnalizatorow"
        Me.Text = "Stan sygnalizatorów"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents flpSygnalizatory As FlowLayoutPanel
End Class
