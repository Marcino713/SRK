<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wndSymulator
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
        Me.components = New System.ComponentModel.Container()
        Me.lvPosterunki = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnPulpit = New System.Windows.Forms.Button()
        Me.btnSygnalizacja = New System.Windows.Forms.Button()
        Me.btnZwrotnice = New System.Windows.Forms.Button()
        Me.tmrTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboRysownik = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvPosterunki
        '
        Me.lvPosterunki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPosterunki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvPosterunki.FullRowSelect = True
        Me.lvPosterunki.HideSelection = False
        Me.lvPosterunki.Location = New System.Drawing.Point(6, 19)
        Me.lvPosterunki.MultiSelect = False
        Me.lvPosterunki.Name = "lvPosterunki"
        Me.lvPosterunki.Size = New System.Drawing.Size(583, 165)
        Me.lvPosterunki.TabIndex = 3
        Me.lvPosterunki.UseCompatibleStateImageBehavior = False
        Me.lvPosterunki.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Nazwa"
        Me.ColumnHeader1.Width = 170
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Pulpit"
        Me.ColumnHeader2.Width = 90
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Sygnalizacja"
        Me.ColumnHeader3.Width = 90
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Zwrotnice"
        Me.ColumnHeader4.Width = 90
        '
        'btnPulpit
        '
        Me.btnPulpit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPulpit.Enabled = False
        Me.btnPulpit.Location = New System.Drawing.Point(274, 190)
        Me.btnPulpit.Name = "btnPulpit"
        Me.btnPulpit.Size = New System.Drawing.Size(100, 40)
        Me.btnPulpit.TabIndex = 4
        Me.btnPulpit.Text = "Pulpit"
        Me.btnPulpit.UseVisualStyleBackColor = True
        '
        'btnSygnalizacja
        '
        Me.btnSygnalizacja.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSygnalizacja.Enabled = False
        Me.btnSygnalizacja.Location = New System.Drawing.Point(380, 190)
        Me.btnSygnalizacja.Name = "btnSygnalizacja"
        Me.btnSygnalizacja.Size = New System.Drawing.Size(100, 40)
        Me.btnSygnalizacja.TabIndex = 5
        Me.btnSygnalizacja.Text = "Sygnalizacja"
        Me.btnSygnalizacja.UseVisualStyleBackColor = True
        '
        'btnZwrotnice
        '
        Me.btnZwrotnice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZwrotnice.Enabled = False
        Me.btnZwrotnice.Location = New System.Drawing.Point(489, 190)
        Me.btnZwrotnice.Name = "btnZwrotnice"
        Me.btnZwrotnice.Size = New System.Drawing.Size(100, 40)
        Me.btnZwrotnice.TabIndex = 6
        Me.btnZwrotnice.Text = "Zwrotnice"
        Me.btnZwrotnice.UseVisualStyleBackColor = True
        '
        'tmrTimer
        '
        Me.tmrTimer.Enabled = True
        Me.tmrTimer.Interval = 500
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(147, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Styl pulpitu dla nowych okien:"
        '
        'cboRysownik
        '
        Me.cboRysownik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRysownik.FormattingEnabled = True
        Me.cboRysownik.Location = New System.Drawing.Point(165, 12)
        Me.cboRysownik.Name = "cboRysownik"
        Me.cboRysownik.Size = New System.Drawing.Size(221, 21)
        Me.cboRysownik.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lvPosterunki)
        Me.GroupBox1.Controls.Add(Me.btnPulpit)
        Me.GroupBox1.Controls.Add(Me.btnSygnalizacja)
        Me.GroupBox1.Controls.Add(Me.btnZwrotnice)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(595, 236)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Posterunki i statusy okien kontrolnych"
        '
        'wndSymulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 287)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cboRysownik)
        Me.Controls.Add(Me.Label2)
        Me.Name = "wndSymulator"
        Me.Text = "Symulator urządzeń srk"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvPosterunki As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents btnPulpit As Button
    Friend WithEvents btnSygnalizacja As Button
    Friend WithEvents btnZwrotnice As Button
    Friend WithEvents tmrTimer As Timer
    Friend WithEvents Label2 As Label
    Friend WithEvents cboRysownik As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
End Class
