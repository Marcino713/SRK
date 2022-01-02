<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndKonfiguratorPolaczen
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lvPliki = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.btnDodaj = New System.Windows.Forms.Button()
        Me.lvPolaczenia = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cboPosterunek = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lvPliki)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(425, 183)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dostępne i zapisane posterunki ruchu"
        '
        'lvPliki
        '
        Me.lvPliki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPliki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvPliki.FullRowSelect = True
        Me.lvPliki.HideSelection = False
        Me.lvPliki.Location = New System.Drawing.Point(6, 19)
        Me.lvPliki.MultiSelect = False
        Me.lvPliki.Name = "lvPliki"
        Me.lvPliki.Size = New System.Drawing.Size(413, 158)
        Me.lvPliki.TabIndex = 0
        Me.lvPliki.UseCompatibleStateImageBehavior = False
        Me.lvPliki.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnUsun)
        Me.GroupBox2.Controls.Add(Me.btnDodaj)
        Me.GroupBox2.Controls.Add(Me.lvPolaczenia)
        Me.GroupBox2.Controls.Add(Me.cboPosterunek)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 201)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(425, 254)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Połączenia"
        '
        'btnUsun
        '
        Me.btnUsun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUsun.Enabled = False
        Me.btnUsun.Location = New System.Drawing.Point(90, 225)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 4
        Me.btnUsun.Text = "Usuń"
        Me.btnUsun.UseVisualStyleBackColor = True
        '
        'btnDodaj
        '
        Me.btnDodaj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDodaj.Location = New System.Drawing.Point(9, 225)
        Me.btnDodaj.Name = "btnDodaj"
        Me.btnDodaj.Size = New System.Drawing.Size(75, 23)
        Me.btnDodaj.TabIndex = 3
        Me.btnDodaj.Text = "Dodaj"
        Me.btnDodaj.UseVisualStyleBackColor = True
        '
        'lvPolaczenia
        '
        Me.lvPolaczenia.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPolaczenia.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvPolaczenia.FullRowSelect = True
        Me.lvPolaczenia.HideSelection = False
        Me.lvPolaczenia.Location = New System.Drawing.Point(9, 46)
        Me.lvPolaczenia.MultiSelect = False
        Me.lvPolaczenia.Name = "lvPolaczenia"
        Me.lvPolaczenia.Size = New System.Drawing.Size(410, 173)
        Me.lvPolaczenia.TabIndex = 2
        Me.lvPolaczenia.UseCompatibleStateImageBehavior = False
        Me.lvPolaczenia.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Posterunek 1"
        Me.ColumnHeader4.Width = 150
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Posterunek 2"
        Me.ColumnHeader5.Width = 150
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Uwagi"
        Me.ColumnHeader6.Width = 100
        '
        'cboPosterunek
        '
        Me.cboPosterunek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPosterunek.FormattingEnabled = True
        Me.cboPosterunek.Location = New System.Drawing.Point(76, 19)
        Me.cboPosterunek.Name = "cboPosterunek"
        Me.cboPosterunek.Size = New System.Drawing.Size(206, 21)
        Me.cboPosterunek.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Posterunek:"
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.Location = New System.Drawing.Point(281, 461)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(75, 23)
        Me.btnZapisz.TabIndex = 2
        Me.btnZapisz.Text = "Zapisz"
        Me.btnZapisz.UseVisualStyleBackColor = True
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.Location = New System.Drawing.Point(362, 461)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 3
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = True
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Nazwa"
        Me.ColumnHeader1.Width = 120
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Plik"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Uwagi"
        Me.ColumnHeader3.Width = 120
        '
        'wndKonfiguratorPolaczen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 496)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnZapisz)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "wndKonfiguratorPolaczen"
        Me.Text = "Konfigurator połączeń"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lvPliki As ListView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnUsun As Button
    Friend WithEvents btnDodaj As Button
    Friend WithEvents lvPolaczenia As ListView
    Friend WithEvents cboPosterunek As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnZapisz As Button
    Friend WithEvents btnZamknij As Button
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
End Class
