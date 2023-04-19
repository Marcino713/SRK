<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndPociagi
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
        Me.lvPociagi = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPrzenazwij = New System.Windows.Forms.Button()
        Me.btnOdswiez = New System.Windows.Forms.Button()
        Me.btnWyrzucMaszyniste = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvPociagi
        '
        Me.lvPociagi.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPociagi.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader8, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvPociagi.FullRowSelect = True
        Me.lvPociagi.HideSelection = False
        Me.lvPociagi.Location = New System.Drawing.Point(12, 25)
        Me.lvPociagi.MultiSelect = False
        Me.lvPociagi.Name = "lvPociagi"
        Me.lvPociagi.Size = New System.Drawing.Size(918, 384)
        Me.lvPociagi.TabIndex = 0
        Me.lvPociagi.UseCompatibleStateImageBehavior = False
        Me.lvPociagi.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Numer"
        Me.ColumnHeader1.Width = 70
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Nazwa"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Prędkość maks."
        Me.ColumnHeader8.Width = 90
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Sterowalny"
        Me.ColumnHeader3.Width = 70
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Liczba osi"
        Me.ColumnHeader4.Width = 70
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Dodajacy posterunek"
        Me.ColumnHeader5.Width = 150
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Sterujący posterunek"
        Me.ColumnHeader6.Width = 150
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Lokalizacja"
        Me.ColumnHeader7.Width = 150
        '
        'btnUsun
        '
        Me.btnUsun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsun.Enabled = False
        Me.btnUsun.Location = New System.Drawing.Point(856, 415)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 4
        Me.btnUsun.Text = "Usuń"
        Me.btnUsun.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Utworzone pociągi:"
        '
        'btnPrzenazwij
        '
        Me.btnPrzenazwij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrzenazwij.Enabled = False
        Me.btnPrzenazwij.Location = New System.Drawing.Point(775, 415)
        Me.btnPrzenazwij.Name = "btnPrzenazwij"
        Me.btnPrzenazwij.Size = New System.Drawing.Size(75, 23)
        Me.btnPrzenazwij.TabIndex = 3
        Me.btnPrzenazwij.Text = "Przenazwij"
        Me.btnPrzenazwij.UseVisualStyleBackColor = True
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOdswiez.Location = New System.Drawing.Point(538, 415)
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(75, 23)
        Me.btnOdswiez.TabIndex = 1
        Me.btnOdswiez.Text = "Odśwież"
        Me.btnOdswiez.UseVisualStyleBackColor = True
        '
        'btnWyrzucMaszyniste
        '
        Me.btnWyrzucMaszyniste.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWyrzucMaszyniste.Enabled = False
        Me.btnWyrzucMaszyniste.Location = New System.Drawing.Point(619, 415)
        Me.btnWyrzucMaszyniste.Name = "btnWyrzucMaszyniste"
        Me.btnWyrzucMaszyniste.Size = New System.Drawing.Size(150, 23)
        Me.btnWyrzucMaszyniste.TabIndex = 2
        Me.btnWyrzucMaszyniste.Text = "Wyrzuć maszynistę"
        Me.btnWyrzucMaszyniste.UseVisualStyleBackColor = True
        '
        'wndPociagi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 450)
        Me.Controls.Add(Me.btnWyrzucMaszyniste)
        Me.Controls.Add(Me.btnOdswiez)
        Me.Controls.Add(Me.btnPrzenazwij)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnUsun)
        Me.Controls.Add(Me.lvPociagi)
        Me.Name = "wndPociagi"
        Me.Text = "Pociągi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvPociagi As ListView
    Friend WithEvents btnUsun As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents btnPrzenazwij As Button
    Friend WithEvents btnOdswiez As Button
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents btnWyrzucMaszyniste As Button
End Class
