<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndKonfiguratorZwrotnic
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lvZwrotnice = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.numWprost = New System.Windows.Forms.NumericUpDown()
        Me.numBok = New System.Windows.Forms.NumericUpDown()
        Me.btnWprost = New System.Windows.Forms.Button()
        Me.btnBok = New System.Windows.Forms.Button()
        CType(Me.numWprost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBok, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(390, 39)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Narzędzie służy do określania czasu wypełnienia PWM w sygnałach sterujących" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "dla " &
    "serwomechanizmów przestawiających zwrotnice." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Umożliwia bezpośrednie wysyłanie u" &
    "stawień do serwomechanizmów."
        '
        'lvZwrotnice
        '
        Me.lvZwrotnice.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvZwrotnice.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvZwrotnice.FullRowSelect = True
        Me.lvZwrotnice.HideSelection = False
        Me.lvZwrotnice.Location = New System.Drawing.Point(12, 51)
        Me.lvZwrotnice.MultiSelect = False
        Me.lvZwrotnice.Name = "lvZwrotnice"
        Me.lvZwrotnice.Size = New System.Drawing.Size(390, 141)
        Me.lvZwrotnice.TabIndex = 1
        Me.lvZwrotnice.UseCompatibleStateImageBehavior = False
        Me.lvZwrotnice.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Posterunek"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Adres"
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Nazwa"
        Me.ColumnHeader3.Width = 100
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 195)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Jazda na wprost:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(108, 213)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "μs"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(138, 195)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Jazda na bok:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(237, 213)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(18, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "μs"
        '
        'numWprost
        '
        Me.numWprost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.numWprost.Enabled = False
        Me.numWprost.Location = New System.Drawing.Point(12, 211)
        Me.numWprost.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.numWprost.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numWprost.Name = "numWprost"
        Me.numWprost.Size = New System.Drawing.Size(90, 20)
        Me.numWprost.TabIndex = 2
        Me.numWprost.Value = New Decimal(New Integer() {1500, 0, 0, 0})
        '
        'numBok
        '
        Me.numBok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.numBok.Enabled = False
        Me.numBok.Location = New System.Drawing.Point(141, 211)
        Me.numBok.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.numBok.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numBok.Name = "numBok"
        Me.numBok.Size = New System.Drawing.Size(90, 20)
        Me.numBok.TabIndex = 4
        Me.numBok.Value = New Decimal(New Integer() {1500, 0, 0, 0})
        '
        'btnWprost
        '
        Me.btnWprost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWprost.Enabled = False
        Me.btnWprost.Location = New System.Drawing.Point(11, 237)
        Me.btnWprost.Name = "btnWprost"
        Me.btnWprost.Size = New System.Drawing.Size(92, 23)
        Me.btnWprost.TabIndex = 3
        Me.btnWprost.Text = "Ustaw"
        Me.btnWprost.UseVisualStyleBackColor = True
        '
        'btnBok
        '
        Me.btnBok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBok.Enabled = False
        Me.btnBok.Location = New System.Drawing.Point(140, 237)
        Me.btnBok.Name = "btnBok"
        Me.btnBok.Size = New System.Drawing.Size(92, 23)
        Me.btnBok.TabIndex = 5
        Me.btnBok.Text = "Ustaw"
        Me.btnBok.UseVisualStyleBackColor = True
        '
        'wndKonfiguratorZwrotnic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 272)
        Me.Controls.Add(Me.btnBok)
        Me.Controls.Add(Me.btnWprost)
        Me.Controls.Add(Me.numBok)
        Me.Controls.Add(Me.numWprost)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lvZwrotnice)
        Me.Controls.Add(Me.Label1)
        Me.Name = "wndKonfiguratorZwrotnic"
        Me.Text = "Konfigurator zwrotnic"
        CType(Me.numWprost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBok, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lvZwrotnice As ListView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents numWprost As NumericUpDown
    Friend WithEvents numBok As NumericUpDown
    Friend WithEvents btnWprost As Button
    Friend WithEvents btnBok As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
End Class
