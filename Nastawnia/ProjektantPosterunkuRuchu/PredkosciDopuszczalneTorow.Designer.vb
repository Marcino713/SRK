<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndPredkosciDopuszczalneTorow
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
        Me.pctSkala = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPredkoscMax = New System.Windows.Forms.Label()
        Me.lblBlednaKarta = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.pctSkala, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pctSkala
        '
        Me.pctSkala.Location = New System.Drawing.Point(15, 48)
        Me.pctSkala.Name = "pctSkala"
        Me.pctSkala.Size = New System.Drawing.Size(30, 256)
        Me.pctSkala.TabIndex = 0
        Me.pctSkala.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 291)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "0 km/godz"
        '
        'lblPredkoscMax
        '
        Me.lblPredkoscMax.AutoSize = True
        Me.lblPredkoscMax.Location = New System.Drawing.Point(51, 48)
        Me.lblPredkoscMax.Name = "lblPredkoscMax"
        Me.lblPredkoscMax.Size = New System.Drawing.Size(0, 13)
        Me.lblPredkoscMax.TabIndex = 2
        '
        'lblBlednaKarta
        '
        Me.lblBlednaKarta.AutoSize = True
        Me.lblBlednaKarta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblBlednaKarta.ForeColor = System.Drawing.Color.Red
        Me.lblBlednaKarta.Location = New System.Drawing.Point(12, 307)
        Me.lblBlednaKarta.Name = "lblBlednaKarta"
        Me.lblBlednaKarta.Size = New System.Drawing.Size(113, 39)
        Me.lblBlednaKarta.TabIndex = 3
        Me.lblBlednaKarta.Text = "Podgląd prędkości" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "dostępny jest tylko" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "dla karty Pulpit."
        Me.lblBlednaKarta.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 26)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Kolor dopuszczalnej" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "prędkości na torze:"
        '
        'wndPredkosciDopuszczalneTorow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(142, 363)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblBlednaKarta)
        Me.Controls.Add(Me.lblPredkoscMax)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pctSkala)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wndPredkosciDopuszczalneTorow"
        Me.Text = "Prędkosci torów"
        Me.TopMost = True
        CType(Me.pctSkala, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pctSkala As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPredkoscMax As Label
    Friend WithEvents lblBlednaKarta As Label
    Friend WithEvents Label2 As Label
End Class
