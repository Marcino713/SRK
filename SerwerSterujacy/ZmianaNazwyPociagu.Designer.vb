<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndZmianaNazwyPociagu
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
        Me.lblNumer = New System.Windows.Forms.Label()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.btnZmien = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
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
        Me.Label2.Location = New System.Drawing.Point(12, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nazwa:"
        '
        'lblNumer
        '
        Me.lblNumer.AutoSize = True
        Me.lblNumer.Location = New System.Drawing.Point(59, 9)
        Me.lblNumer.Name = "lblNumer"
        Me.lblNumer.Size = New System.Drawing.Size(0, 13)
        Me.lblNumer.TabIndex = 2
        '
        'txtNazwa
        '
        Me.txtNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNazwa.Location = New System.Drawing.Point(61, 28)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(156, 20)
        Me.txtNazwa.TabIndex = 3
        '
        'btnZmien
        '
        Me.btnZmien.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZmien.Location = New System.Drawing.Point(60, 54)
        Me.btnZmien.Name = "btnZmien"
        Me.btnZmien.Size = New System.Drawing.Size(76, 23)
        Me.btnZmien.TabIndex = 4
        Me.btnZmien.Text = "Zmień"
        Me.btnZmien.UseVisualStyleBackColor = True
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.Location = New System.Drawing.Point(142, 54)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(76, 23)
        Me.btnAnuluj.TabIndex = 5
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'wndZmianaNazwyPociagu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(229, 89)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZmien)
        Me.Controls.Add(Me.txtNazwa)
        Me.Controls.Add(Me.lblNumer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "wndZmianaNazwyPociagu"
        Me.Text = "Zmiana nazwy pociągu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblNumer As Label
    Friend WithEvents txtNazwa As TextBox
    Friend WithEvents btnZmien As Button
    Friend WithEvents btnAnuluj As Button
End Class
