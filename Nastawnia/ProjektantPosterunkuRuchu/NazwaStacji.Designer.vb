﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndNazwaStacji
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
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAdres = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDataUtworzenia = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nazwa posterunku:"
        '
        'txtNazwa
        '
        Me.txtNazwa.Location = New System.Drawing.Point(117, 38)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(203, 20)
        Me.txtNazwa.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(164, 77)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Location = New System.Drawing.Point(245, 77)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 3
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Adres:"
        '
        'txtAdres
        '
        Me.txtAdres.Location = New System.Drawing.Point(117, 12)
        Me.txtAdres.Name = "txtAdres"
        Me.txtAdres.Size = New System.Drawing.Size(203, 20)
        Me.txtAdres.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Utworzono:"
        '
        'lblDataUtworzenia
        '
        Me.lblDataUtworzenia.AutoSize = True
        Me.lblDataUtworzenia.Location = New System.Drawing.Point(114, 61)
        Me.lblDataUtworzenia.Name = "lblDataUtworzenia"
        Me.lblDataUtworzenia.Size = New System.Drawing.Size(0, 13)
        Me.lblDataUtworzenia.TabIndex = 7
        '
        'wndNazwaStacji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 108)
        Me.Controls.Add(Me.lblDataUtworzenia)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtAdres)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtNazwa)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "wndNazwaStacji"
        Me.Text = "Edycja nazwy"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNazwa As TextBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnAnuluj As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtAdres As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblDataUtworzenia As Label
End Class
