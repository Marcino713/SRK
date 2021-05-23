<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndKonfiguratorStacji
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
        Me.components = New System.ComponentModel.Container()
        Me.mnuMenu = New System.Windows.Forms.MenuStrip()
        Me.mnuNarzedzia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDodajKostki = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUsunKostki = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuNazwa = New System.Windows.Forms.ToolStripMenuItem()
        Me.pctPulpit = New System.Windows.Forms.PictureBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lvKostki = New System.Windows.Forms.ListView()
        Me.imlKostki = New System.Windows.Forms.ImageList(Me.components)
        Me.tabUstawienia = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlPulpit = New System.Windows.Forms.Panel()
        Me.mnuMenu.SuspendLayout()
        CType(Me.pctPulpit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tabUstawienia.SuspendLayout()
        Me.pnlPulpit.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMenu
        '
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNarzedzia})
        Me.mnuMenu.Location = New System.Drawing.Point(0, 0)
        Me.mnuMenu.Name = "mnuMenu"
        Me.mnuMenu.Size = New System.Drawing.Size(789, 24)
        Me.mnuMenu.TabIndex = 0
        Me.mnuMenu.Text = "MenuStrip1"
        '
        'mnuNarzedzia
        '
        Me.mnuNarzedzia.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDodajKostki, Me.mnuUsunKostki, Me.ToolStripSeparator1, Me.mnuNazwa})
        Me.mnuNarzedzia.Name = "mnuNarzedzia"
        Me.mnuNarzedzia.Size = New System.Drawing.Size(70, 20)
        Me.mnuNarzedzia.Text = "Narzędzia"
        '
        'mnuDodajKostki
        '
        Me.mnuDodajKostki.Name = "mnuDodajKostki"
        Me.mnuDodajKostki.Size = New System.Drawing.Size(216, 22)
        Me.mnuDodajKostki.Text = "Dodaj kostki..."
        '
        'mnuUsunKostki
        '
        Me.mnuUsunKostki.Name = "mnuUsunKostki"
        Me.mnuUsunKostki.Size = New System.Drawing.Size(216, 22)
        Me.mnuUsunKostki.Text = "Usuń kostki..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(213, 6)
        '
        'mnuNazwa
        '
        Me.mnuNazwa.Name = "mnuNazwa"
        Me.mnuNazwa.Size = New System.Drawing.Size(216, 22)
        Me.mnuNazwa.Text = "Zmień nazwę posterunku..."
        '
        'pctPulpit
        '
        Me.pctPulpit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctPulpit.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.pctPulpit.Location = New System.Drawing.Point(0, 0)
        Me.pctPulpit.Name = "pctPulpit"
        Me.pctPulpit.Size = New System.Drawing.Size(623, 564)
        Me.pctPulpit.TabIndex = 0
        Me.pctPulpit.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(629, 27)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lvKostki)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tabUstawienia)
        Me.SplitContainer1.Size = New System.Drawing.Size(160, 564)
        Me.SplitContainer1.SplitterDistance = 282
        Me.SplitContainer1.TabIndex = 1
        '
        'lvKostki
        '
        Me.lvKostki.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvKostki.HideSelection = False
        Me.lvKostki.LargeImageList = Me.imlKostki
        Me.lvKostki.Location = New System.Drawing.Point(0, 0)
        Me.lvKostki.MultiSelect = False
        Me.lvKostki.Name = "lvKostki"
        Me.lvKostki.Size = New System.Drawing.Size(160, 282)
        Me.lvKostki.TabIndex = 0
        Me.lvKostki.UseCompatibleStateImageBehavior = False
        '
        'imlKostki
        '
        Me.imlKostki.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imlKostki.ImageSize = New System.Drawing.Size(48, 48)
        Me.imlKostki.TransparentColor = System.Drawing.Color.Transparent
        '
        'tabUstawienia
        '
        Me.tabUstawienia.Controls.Add(Me.TabPage1)
        Me.tabUstawienia.Controls.Add(Me.TabPage2)
        Me.tabUstawienia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabUstawienia.Location = New System.Drawing.Point(0, 0)
        Me.tabUstawienia.Name = "tabUstawienia"
        Me.tabUstawienia.SelectedIndex = 0
        Me.tabUstawienia.Size = New System.Drawing.Size(160, 278)
        Me.tabUstawienia.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(152, 252)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(152, 252)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnlPulpit
        '
        Me.pnlPulpit.AllowDrop = True
        Me.pnlPulpit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPulpit.Controls.Add(Me.pctPulpit)
        Me.pnlPulpit.Location = New System.Drawing.Point(0, 27)
        Me.pnlPulpit.Name = "pnlPulpit"
        Me.pnlPulpit.Size = New System.Drawing.Size(623, 564)
        Me.pnlPulpit.TabIndex = 2
        '
        'wndKonfiguratorStacji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 591)
        Me.Controls.Add(Me.pnlPulpit)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "wndKonfiguratorStacji"
        Me.Text = "Konfigurator stacji"
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        CType(Me.pctPulpit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tabUstawienia.ResumeLayout(False)
        Me.pnlPulpit.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mnuMenu As MenuStrip
    Friend WithEvents mnuNarzedzia As ToolStripMenuItem
    Friend WithEvents mnuDodajKostki As ToolStripMenuItem
    Friend WithEvents mnuUsunKostki As ToolStripMenuItem
    Friend WithEvents pctPulpit As PictureBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents pnlPulpit As Panel
    Friend WithEvents lvKostki As ListView
    Friend WithEvents imlKostki As ImageList
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mnuNazwa As ToolStripMenuItem
    Friend WithEvents tabUstawienia As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
End Class
