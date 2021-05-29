<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndEdytorPowierzchni
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbLewo = New System.Windows.Forms.RadioButton()
        Me.rbDol = New System.Windows.Forms.RadioButton()
        Me.rbPrawo = New System.Windows.Forms.RadioButton()
        Me.rbGora = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.numLiczbaKostek = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox2.SuspendLayout()
        CType(Me.numLiczbaKostek, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbLewo)
        Me.GroupBox2.Controls.Add(Me.rbDol)
        Me.GroupBox2.Controls.Add(Me.rbPrawo)
        Me.GroupBox2.Controls.Add(Me.rbGora)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(228, 46)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Kierunek"
        '
        'rbLewo
        '
        Me.rbLewo.AutoSize = True
        Me.rbLewo.Location = New System.Drawing.Point(170, 19)
        Me.rbLewo.Name = "rbLewo"
        Me.rbLewo.Size = New System.Drawing.Size(51, 17)
        Me.rbLewo.TabIndex = 3
        Me.rbLewo.TabStop = True
        Me.rbLewo.Text = "Lewo"
        Me.rbLewo.UseVisualStyleBackColor = True
        '
        'rbDol
        '
        Me.rbDol.AutoSize = True
        Me.rbDol.Location = New System.Drawing.Point(121, 19)
        Me.rbDol.Name = "rbDol"
        Me.rbDol.Size = New System.Drawing.Size(43, 17)
        Me.rbDol.TabIndex = 2
        Me.rbDol.TabStop = True
        Me.rbDol.Text = "Dół"
        Me.rbDol.UseVisualStyleBackColor = True
        '
        'rbPrawo
        '
        Me.rbPrawo.AutoSize = True
        Me.rbPrawo.Location = New System.Drawing.Point(60, 19)
        Me.rbPrawo.Name = "rbPrawo"
        Me.rbPrawo.Size = New System.Drawing.Size(55, 17)
        Me.rbPrawo.TabIndex = 1
        Me.rbPrawo.TabStop = True
        Me.rbPrawo.Text = "Prawo"
        Me.rbPrawo.UseVisualStyleBackColor = True
        '
        'rbGora
        '
        Me.rbGora.AutoSize = True
        Me.rbGora.Checked = True
        Me.rbGora.Location = New System.Drawing.Point(6, 19)
        Me.rbGora.Name = "rbGora"
        Me.rbGora.Size = New System.Drawing.Size(48, 17)
        Me.rbGora.TabIndex = 0
        Me.rbGora.TabStop = True
        Me.rbGora.Text = "Góra"
        Me.rbGora.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Liczba kostek:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(84, 90)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Location = New System.Drawing.Point(165, 90)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 5
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'numLiczbaKostek
        '
        Me.numLiczbaKostek.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numLiczbaKostek.Location = New System.Drawing.Point(97, 65)
        Me.numLiczbaKostek.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numLiczbaKostek.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numLiczbaKostek.Name = "numLiczbaKostek"
        Me.numLiczbaKostek.Size = New System.Drawing.Size(143, 20)
        Me.numLiczbaKostek.TabIndex = 6
        Me.numLiczbaKostek.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'wndEdytorPowierzchni
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 128)
        Me.Controls.Add(Me.numLiczbaKostek)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "wndEdytorPowierzchni"
        Me.Text = "Edytor powierzchni pulpitu"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.numLiczbaKostek, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents rbLewo As RadioButton
    Friend WithEvents rbDol As RadioButton
    Friend WithEvents rbPrawo As RadioButton
    Friend WithEvents rbGora As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents btnAnuluj As Button
    Friend WithEvents numLiczbaKostek As NumericUpDown
End Class
