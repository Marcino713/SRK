<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndObserwatorzy
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
        Me.lvObserwatorzy = New System.Windows.Forms.ListView()
        Me.btnWyrzuc = New System.Windows.Forms.Button()
        Me.btnWyrzucZablokuj = New System.Windows.Forms.Button()
        Me.lblTytul = New System.Windows.Forms.Label()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnOdswiez = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvObserwatorzy
        '
        Me.lvObserwatorzy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvObserwatorzy.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvObserwatorzy.FullRowSelect = True
        Me.lvObserwatorzy.HideSelection = False
        Me.lvObserwatorzy.Location = New System.Drawing.Point(12, 25)
        Me.lvObserwatorzy.MultiSelect = False
        Me.lvObserwatorzy.Name = "lvObserwatorzy"
        Me.lvObserwatorzy.Size = New System.Drawing.Size(361, 210)
        Me.lvObserwatorzy.TabIndex = 0
        Me.lvObserwatorzy.UseCompatibleStateImageBehavior = False
        Me.lvObserwatorzy.View = System.Windows.Forms.View.Details
        '
        'btnWyrzuc
        '
        Me.btnWyrzuc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWyrzuc.Enabled = False
        Me.btnWyrzuc.Location = New System.Drawing.Point(137, 241)
        Me.btnWyrzuc.Name = "btnWyrzuc"
        Me.btnWyrzuc.Size = New System.Drawing.Size(115, 23)
        Me.btnWyrzuc.TabIndex = 2
        Me.btnWyrzuc.Text = "Wyrzuć"
        Me.btnWyrzuc.UseVisualStyleBackColor = True
        '
        'btnWyrzucZablokuj
        '
        Me.btnWyrzucZablokuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWyrzucZablokuj.Enabled = False
        Me.btnWyrzucZablokuj.Location = New System.Drawing.Point(258, 241)
        Me.btnWyrzucZablokuj.Name = "btnWyrzucZablokuj"
        Me.btnWyrzucZablokuj.Size = New System.Drawing.Size(115, 23)
        Me.btnWyrzucZablokuj.TabIndex = 3
        Me.btnWyrzucZablokuj.Text = "Wyrzuć i zablokuj"
        '
        'lblTytul
        '
        Me.lblTytul.AutoSize = True
        Me.lblTytul.Location = New System.Drawing.Point(9, 9)
        Me.lblTytul.Name = "lblTytul"
        Me.lblTytul.Size = New System.Drawing.Size(202, 13)
        Me.lblTytul.TabIndex = 3
        Me.lblTytul.Text = "Podłączeni obserwatorzy dla posterunku "
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Adres IP"
        Me.ColumnHeader1.Width = 170
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Czas podłączenia"
        Me.ColumnHeader2.Width = 170
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOdswiez.Location = New System.Drawing.Point(16, 241)
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(115, 23)
        Me.btnOdswiez.TabIndex = 1
        Me.btnOdswiez.Text = "Odśwież"
        Me.btnOdswiez.UseVisualStyleBackColor = True
        '
        'wndObserwatorzy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 276)
        Me.Controls.Add(Me.btnOdswiez)
        Me.Controls.Add(Me.lblTytul)
        Me.Controls.Add(Me.btnWyrzucZablokuj)
        Me.Controls.Add(Me.btnWyrzuc)
        Me.Controls.Add(Me.lvObserwatorzy)
        Me.Name = "wndObserwatorzy"
        Me.Text = "Lista obserwatorów"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvObserwatorzy As ListView
    Friend WithEvents btnWyrzuc As Button
    Friend WithEvents btnWyrzucZablokuj As Button
    Friend WithEvents lblTytul As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents btnOdswiez As Button
End Class
