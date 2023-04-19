<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndKomunikatZLista
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
        Me.lblKomunikat = New System.Windows.Forms.Label()
        Me.lstLista = New System.Windows.Forms.ListBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblKomunikat
        '
        Me.lblKomunikat.AutoSize = True
        Me.lblKomunikat.Location = New System.Drawing.Point(12, 9)
        Me.lblKomunikat.Name = "lblKomunikat"
        Me.lblKomunikat.Size = New System.Drawing.Size(0, 13)
        Me.lblKomunikat.TabIndex = 0
        '
        'lstLista
        '
        Me.lstLista.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstLista.FormattingEnabled = True
        Me.lstLista.Location = New System.Drawing.Point(12, 38)
        Me.lstLista.Name = "lstLista"
        Me.lstLista.Size = New System.Drawing.Size(315, 147)
        Me.lstLista.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(252, 195)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'wndKomunikatZLista
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 230)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lstLista)
        Me.Controls.Add(Me.lblKomunikat)
        Me.Name = "wndKomunikatZLista"
        Me.Text = "Komunikat"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblKomunikat As Label
    Friend WithEvents lstLista As ListBox
    Friend WithEvents btnOK As Button
End Class
