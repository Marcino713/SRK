<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndWyborStacji
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
        Me.lblStanLaczenia = New System.Windows.Forms.Label()
        Me.btnPolacz = New System.Windows.Forms.Button()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtAdres = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbTrybSamoczynny = New System.Windows.Forms.RadioButton()
        Me.rbTrybPolsamoczynny = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnWybierz = New System.Windows.Forms.Button()
        Me.lvPosterunki = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblStanLaczenia)
        Me.GroupBox1.Controls.Add(Me.btnPolacz)
        Me.GroupBox1.Controls.Add(Me.txtHaslo)
        Me.GroupBox1.Controls.Add(Me.txtPort)
        Me.GroupBox1.Controls.Add(Me.txtAdres)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(193, 525)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Połączenie z serwerem"
        '
        'lblStanLaczenia
        '
        Me.lblStanLaczenia.AutoSize = True
        Me.lblStanLaczenia.Location = New System.Drawing.Point(6, 162)
        Me.lblStanLaczenia.Name = "lblStanLaczenia"
        Me.lblStanLaczenia.Size = New System.Drawing.Size(0, 13)
        Me.lblStanLaczenia.TabIndex = 7
        '
        'btnPolacz
        '
        Me.btnPolacz.Location = New System.Drawing.Point(5, 136)
        Me.btnPolacz.Name = "btnPolacz"
        Me.btnPolacz.Size = New System.Drawing.Size(75, 23)
        Me.btnPolacz.TabIndex = 4
        Me.btnPolacz.Text = "Połącz"
        Me.btnPolacz.UseVisualStyleBackColor = True
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(6, 110)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtHaslo.Size = New System.Drawing.Size(181, 20)
        Me.txtHaslo.TabIndex = 3
        Me.txtHaslo.Text = "a"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(6, 71)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(181, 20)
        Me.txtPort.TabIndex = 2
        Me.txtPort.Text = "100"
        '
        'txtAdres
        '
        Me.txtAdres.Location = New System.Drawing.Point(6, 32)
        Me.txtAdres.Name = "txtAdres"
        Me.txtAdres.Size = New System.Drawing.Size(181, 20)
        Me.txtAdres.TabIndex = 1
        Me.txtAdres.Text = "127.0.0.1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Hasło:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Port:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Adres:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rbTrybSamoczynny)
        Me.GroupBox2.Controls.Add(Me.rbTrybPolsamoczynny)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.btnWybierz)
        Me.GroupBox2.Controls.Add(Me.lvPosterunki)
        Me.GroupBox2.Location = New System.Drawing.Point(211, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(682, 525)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Dostępne posterunki ruchu"
        '
        'rbTrybSamoczynny
        '
        Me.rbTrybSamoczynny.AutoSize = True
        Me.rbTrybSamoczynny.Location = New System.Drawing.Point(178, 19)
        Me.rbTrybSamoczynny.Name = "rbTrybSamoczynny"
        Me.rbTrybSamoczynny.Size = New System.Drawing.Size(85, 17)
        Me.rbTrybSamoczynny.TabIndex = 6
        Me.rbTrybSamoczynny.Text = "Samoczynny"
        Me.rbTrybSamoczynny.UseVisualStyleBackColor = True
        '
        'rbTrybPolsamoczynny
        '
        Me.rbTrybPolsamoczynny.AutoSize = True
        Me.rbTrybPolsamoczynny.Checked = True
        Me.rbTrybPolsamoczynny.Location = New System.Drawing.Point(72, 19)
        Me.rbTrybPolsamoczynny.Name = "rbTrybPolsamoczynny"
        Me.rbTrybPolsamoczynny.Size = New System.Drawing.Size(100, 17)
        Me.rbTrybPolsamoczynny.TabIndex = 5
        Me.rbTrybPolsamoczynny.TabStop = True
        Me.rbTrybPolsamoczynny.Text = "Półsamoczynny"
        Me.rbTrybPolsamoczynny.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Tryb pracy:"
        '
        'btnWybierz
        '
        Me.btnWybierz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWybierz.Enabled = False
        Me.btnWybierz.Location = New System.Drawing.Point(602, 496)
        Me.btnWybierz.Name = "btnWybierz"
        Me.btnWybierz.Size = New System.Drawing.Size(75, 23)
        Me.btnWybierz.TabIndex = 8
        Me.btnWybierz.Text = "Wybierz"
        Me.btnWybierz.UseVisualStyleBackColor = True
        '
        'lvPosterunki
        '
        Me.lvPosterunki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvPosterunki.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvPosterunki.FullRowSelect = True
        Me.lvPosterunki.HideSelection = False
        Me.lvPosterunki.Location = New System.Drawing.Point(6, 42)
        Me.lvPosterunki.MultiSelect = False
        Me.lvPosterunki.Name = "lvPosterunki"
        Me.lvPosterunki.Size = New System.Drawing.Size(670, 448)
        Me.lvPosterunki.TabIndex = 7
        Me.lvPosterunki.UseCompatibleStateImageBehavior = False
        Me.lvPosterunki.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Adres"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Nazwa"
        Me.ColumnHeader2.Width = 300
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Stan"
        Me.ColumnHeader3.Width = 100
        '
        'wndWyborStacji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 549)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "wndWyborStacji"
        Me.Text = "Wybór posterunku ruchu"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtHaslo As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtAdres As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblStanLaczenia As Label
    Friend WithEvents btnPolacz As Button
    Friend WithEvents btnWybierz As Button
    Friend WithEvents lvPosterunki As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents rbTrybSamoczynny As RadioButton
    Friend WithEvents rbTrybPolsamoczynny As RadioButton
    Friend WithEvents Label4 As Label
End Class
