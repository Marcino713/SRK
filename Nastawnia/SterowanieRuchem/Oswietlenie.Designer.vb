<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndOswietlenie
    Inherits Wspolne.OknoPrzywracalne

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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblJasnoscUstawiana = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.trbJasnoscUstawiana = New System.Windows.Forms.TrackBar()
        Me.lvAdresy = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.trbStanLampy = New System.Windows.Forms.TrackBar()
        Me.lblAdres = New System.Windows.Forms.Label()
        Me.lblStanLampy = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.trbJasnoscUstawiana, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.trbStanLampy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblJasnoscUstawiana)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.trbJasnoscUstawiana)
        Me.GroupBox1.Controls.Add(Me.lvAdresy)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 364)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wybór jasności lamp"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 26)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Adresy zaznaczonych lamp" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(CTRL zaznacza wiele):"
        '
        'lblJasnoscUstawiana
        '
        Me.lblJasnoscUstawiana.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJasnoscUstawiana.AutoSize = True
        Me.lblJasnoscUstawiana.Location = New System.Drawing.Point(185, 323)
        Me.lblJasnoscUstawiana.Name = "lblJasnoscUstawiana"
        Me.lblJasnoscUstawiana.Size = New System.Drawing.Size(13, 13)
        Me.lblJasnoscUstawiana.TabIndex = 3
        Me.lblJasnoscUstawiana.Text = "0"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 323)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Jasność:"
        '
        'trbJasnoscUstawiana
        '
        Me.trbJasnoscUstawiana.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trbJasnoscUstawiana.LargeChange = 30
        Me.trbJasnoscUstawiana.Location = New System.Drawing.Point(61, 313)
        Me.trbJasnoscUstawiana.Maximum = 255
        Me.trbJasnoscUstawiana.Name = "trbJasnoscUstawiana"
        Me.trbJasnoscUstawiana.Size = New System.Drawing.Size(118, 45)
        Me.trbJasnoscUstawiana.SmallChange = 10
        Me.trbJasnoscUstawiana.TabIndex = 1
        Me.trbJasnoscUstawiana.TickFrequency = 10
        '
        'lvAdresy
        '
        Me.lvAdresy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvAdresy.Enabled = False
        Me.lvAdresy.Location = New System.Drawing.Point(6, 45)
        Me.lvAdresy.Name = "lvAdresy"
        Me.lvAdresy.Size = New System.Drawing.Size(204, 262)
        Me.lvAdresy.TabIndex = 0
        Me.lvAdresy.UseCompatibleStateImageBehavior = False
        Me.lvAdresy.View = System.Windows.Forms.View.List
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.trbStanLampy)
        Me.GroupBox2.Controls.Add(Me.lblAdres)
        Me.GroupBox2.Controls.Add(Me.lblStanLampy)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 382)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(216, 106)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Stan lampy"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Jasność:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Adres:"
        '
        'trbStanLampy
        '
        Me.trbStanLampy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trbStanLampy.Enabled = False
        Me.trbStanLampy.Location = New System.Drawing.Point(61, 52)
        Me.trbStanLampy.Maximum = 255
        Me.trbStanLampy.Name = "trbStanLampy"
        Me.trbStanLampy.Size = New System.Drawing.Size(118, 45)
        Me.trbStanLampy.TabIndex = 2
        Me.trbStanLampy.TickFrequency = 10
        '
        'lblAdres
        '
        Me.lblAdres.AutoSize = True
        Me.lblAdres.Location = New System.Drawing.Point(65, 36)
        Me.lblAdres.Name = "lblAdres"
        Me.lblAdres.Size = New System.Drawing.Size(13, 13)
        Me.lblAdres.TabIndex = 4
        Me.lblAdres.Text = "0"
        '
        'lblStanLampy
        '
        Me.lblStanLampy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStanLampy.AutoSize = True
        Me.lblStanLampy.Location = New System.Drawing.Point(185, 62)
        Me.lblStanLampy.Name = "lblStanLampy"
        Me.lblStanLampy.Size = New System.Drawing.Size(13, 13)
        Me.lblStanLampy.TabIndex = 1
        Me.lblStanLampy.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(181, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Jasność ostatnio zaznaczonej lampy:"
        '
        'wndOswietlenie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(240, 500)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "wndOswietlenie"
        Me.Text = "Sterowanie oświetleniem"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.trbJasnoscUstawiana, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.trbStanLampy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblJasnoscUstawiana As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents trbJasnoscUstawiana As TrackBar
    Friend WithEvents lvAdresy As ListView
    Friend WithEvents lblAdres As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents trbStanLampy As TrackBar
    Friend WithEvents lblStanLampy As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
