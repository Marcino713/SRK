<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndSterowaniePociagiem
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
        Me.lblNumer = New System.Windows.Forms.Label()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.cbTyl = New System.Windows.Forms.CheckBox()
        Me.btnWysiadz = New System.Windows.Forms.Button()
        Me.lblPredkosc = New System.Windows.Forms.Label()
        Me.prPredkosc = New Nastawnia.PredkoscPociagu()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numer:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nazwa:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Prędkość:"
        '
        'lblNumer
        '
        Me.lblNumer.AutoSize = True
        Me.lblNumer.Location = New System.Drawing.Point(73, 9)
        Me.lblNumer.Name = "lblNumer"
        Me.lblNumer.Size = New System.Drawing.Size(39, 13)
        Me.lblNumer.TabIndex = 3
        Me.lblNumer.Text = "Label4"
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.Location = New System.Drawing.Point(73, 27)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(39, 13)
        Me.lblNazwa.TabIndex = 4
        Me.lblNazwa.Text = "Label5"
        '
        'cbTyl
        '
        Me.cbTyl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbTyl.AutoSize = True
        Me.cbTyl.Location = New System.Drawing.Point(13, 363)
        Me.cbTyl.Name = "cbTyl"
        Me.cbTyl.Size = New System.Drawing.Size(61, 17)
        Me.cbTyl.TabIndex = 2
        Me.cbTyl.Text = "Do tyłu"
        Me.cbTyl.UseVisualStyleBackColor = True
        '
        'btnWysiadz
        '
        Me.btnWysiadz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnWysiadz.Location = New System.Drawing.Point(12, 386)
        Me.btnWysiadz.Name = "btnWysiadz"
        Me.btnWysiadz.Size = New System.Drawing.Size(100, 23)
        Me.btnWysiadz.TabIndex = 3
        Me.btnWysiadz.Text = "Wysiądź"
        Me.btnWysiadz.UseVisualStyleBackColor = True
        '
        'lblPredkosc
        '
        Me.lblPredkosc.AutoSize = True
        Me.lblPredkosc.Location = New System.Drawing.Point(73, 45)
        Me.lblPredkosc.Name = "lblPredkosc"
        Me.lblPredkosc.Size = New System.Drawing.Size(13, 13)
        Me.lblPredkosc.TabIndex = 5
        Me.lblPredkosc.Text = "0"
        '
        'prPredkosc
        '
        Me.prPredkosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.prPredkosc.Location = New System.Drawing.Point(12, 61)
        Me.prPredkosc.Name = "prPredkosc"
        Me.prPredkosc.PredkoscBiezaca = CType(0US, UShort)
        Me.prPredkosc.PredkoscDozwolona = CType(100US, UShort)
        Me.prPredkosc.PredkoscMaksymalna = CType(300US, UShort)
        Me.prPredkosc.Size = New System.Drawing.Size(136, 296)
        Me.prPredkosc.TabIndex = 1
        '
        'btnUsun
        '
        Me.btnUsun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUsun.Location = New System.Drawing.Point(12, 415)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(100, 23)
        Me.btnUsun.TabIndex = 4
        Me.btnUsun.Text = "Usuń"
        Me.btnUsun.UseVisualStyleBackColor = True
        '
        'wndSterowaniePociagiem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(160, 450)
        Me.Controls.Add(Me.btnUsun)
        Me.Controls.Add(Me.lblPredkosc)
        Me.Controls.Add(Me.btnWysiadz)
        Me.Controls.Add(Me.prPredkosc)
        Me.Controls.Add(Me.cbTyl)
        Me.Controls.Add(Me.lblNazwa)
        Me.Controls.Add(Me.lblNumer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "wndSterowaniePociagiem"
        Me.Text = "Sterowanie pociagiem"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblNumer As Label
    Friend WithEvents lblNazwa As Label
    Friend WithEvents cbTyl As CheckBox
    Friend WithEvents prPredkosc As PredkoscPociagu
    Friend WithEvents btnWysiadz As Button
    Friend WithEvents lblPredkosc As Label
    Friend WithEvents btnUsun As Button
End Class
