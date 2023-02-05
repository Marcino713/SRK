<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndDodawaniePolaczenia
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboPosterunek1 = New System.Windows.Forms.ComboBox()
        Me.cboTor1 = New System.Windows.Forms.ComboBox()
        Me.cboPosterunek2 = New System.Windows.Forms.ComboBox()
        Me.cboTor2 = New System.Windows.Forms.ComboBox()
        Me.btnDodaj = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Posterunek"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(215, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tor"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Posterunek"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(215, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Tor"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(254, 26)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Na listach wyświetlane są odcinki torów posiadające" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "jedną kostkę ustawiania kier" &
    "unku blokady."
        '
        'cboPosterunek1
        '
        Me.cboPosterunek1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPosterunek1.FormattingEnabled = True
        Me.cboPosterunek1.Location = New System.Drawing.Point(12, 72)
        Me.cboPosterunek1.Name = "cboPosterunek1"
        Me.cboPosterunek1.Size = New System.Drawing.Size(200, 21)
        Me.cboPosterunek1.TabIndex = 5
        '
        'cboTor1
        '
        Me.cboTor1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTor1.FormattingEnabled = True
        Me.cboTor1.Location = New System.Drawing.Point(218, 72)
        Me.cboTor1.Name = "cboTor1"
        Me.cboTor1.Size = New System.Drawing.Size(200, 21)
        Me.cboTor1.TabIndex = 6
        '
        'cboPosterunek2
        '
        Me.cboPosterunek2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPosterunek2.FormattingEnabled = True
        Me.cboPosterunek2.Location = New System.Drawing.Point(12, 112)
        Me.cboPosterunek2.Name = "cboPosterunek2"
        Me.cboPosterunek2.Size = New System.Drawing.Size(200, 21)
        Me.cboPosterunek2.TabIndex = 7
        '
        'cboTor2
        '
        Me.cboTor2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTor2.FormattingEnabled = True
        Me.cboTor2.Location = New System.Drawing.Point(218, 112)
        Me.cboTor2.Name = "cboTor2"
        Me.cboTor2.Size = New System.Drawing.Size(200, 21)
        Me.cboTor2.TabIndex = 8
        '
        'btnDodaj
        '
        Me.btnDodaj.Location = New System.Drawing.Point(262, 139)
        Me.btnDodaj.Name = "btnDodaj"
        Me.btnDodaj.Size = New System.Drawing.Size(75, 23)
        Me.btnDodaj.TabIndex = 9
        Me.btnDodaj.Text = "Dodaj"
        Me.btnDodaj.UseVisualStyleBackColor = True
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Location = New System.Drawing.Point(343, 139)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 10
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'wndDodawaniePolaczenia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 175)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnDodaj)
        Me.Controls.Add(Me.cboTor2)
        Me.Controls.Add(Me.cboPosterunek2)
        Me.Controls.Add(Me.cboTor1)
        Me.Controls.Add(Me.cboPosterunek1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "wndDodawaniePolaczenia"
        Me.Text = "Dodaj połączenie"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cboPosterunek1 As ComboBox
    Friend WithEvents cboTor1 As ComboBox
    Friend WithEvents cboPosterunek2 As ComboBox
    Friend WithEvents cboTor2 As ComboBox
    Friend WithEvents btnDodaj As Button
    Friend WithEvents btnAnuluj As Button
End Class
