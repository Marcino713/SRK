<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndWyborPociagu
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
        Me.btnWybierz = New System.Windows.Forms.Button()
        Me.lvPociagi = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblStan = New System.Windows.Forms.Label()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Utworzone pociągi:"
        '
        'btnWybierz
        '
        Me.btnWybierz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWybierz.Enabled = False
        Me.btnWybierz.Location = New System.Drawing.Point(554, 266)
        Me.btnWybierz.Name = "btnWybierz"
        Me.btnWybierz.Size = New System.Drawing.Size(75, 23)
        Me.btnWybierz.TabIndex = 2
        Me.btnWybierz.Text = "Wybierz"
        Me.btnWybierz.UseVisualStyleBackColor = True
        '
        'lvPociagi
        '
        Me.lvPociagi.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPociagi.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lvPociagi.FullRowSelect = True
        Me.lvPociagi.HideSelection = False
        Me.lvPociagi.Location = New System.Drawing.Point(12, 25)
        Me.lvPociagi.MultiSelect = False
        Me.lvPociagi.Name = "lvPociagi"
        Me.lvPociagi.Size = New System.Drawing.Size(698, 235)
        Me.lvPociagi.TabIndex = 1
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
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Stan"
        Me.ColumnHeader3.Width = 70
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Dodający posterunek"
        Me.ColumnHeader4.Width = 150
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Lokalizacja"
        Me.ColumnHeader5.Width = 150
        '
        'lblStan
        '
        Me.lblStan.AutoSize = True
        Me.lblStan.Location = New System.Drawing.Point(202, 9)
        Me.lblStan.Name = "lblStan"
        Me.lblStan.Size = New System.Drawing.Size(79, 13)
        Me.lblStan.TabIndex = 3
        Me.lblStan.Text = "Wczytywanie..."
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.Location = New System.Drawing.Point(635, 266)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 4
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Prędkosć maks."
        Me.ColumnHeader6.Width = 90
        '
        'wndWyborPociagu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 301)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.lblStan)
        Me.Controls.Add(Me.lvPociagi)
        Me.Controls.Add(Me.btnWybierz)
        Me.Controls.Add(Me.Label1)
        Me.Name = "wndWyborPociagu"
        Me.Text = "Wybór pociagu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnWybierz As Button
    Friend WithEvents lvPociagi As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents lblStan As Label
    Friend WithEvents btnAnuluj As Button
    Friend WithEvents ColumnHeader6 As ColumnHeader
End Class
