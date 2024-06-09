<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wndListaAdresowIP
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
        Me.rbWszyscy = New System.Windows.Forms.RadioButton()
        Me.rbOproczWybranych = New System.Windows.Forms.RadioButton()
        Me.rbTylkoWybrani = New System.Windows.Forms.RadioButton()
        Me.txtAdresy = New System.Windows.Forms.TextBox()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.btnOtworz = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dlgZapisz = New System.Windows.Forms.SaveFileDialog()
        Me.dlgOtworz = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(171, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Zezwól na połączenie z serwerem:"
        '
        'rbWszyscy
        '
        Me.rbWszyscy.AutoSize = True
        Me.rbWszyscy.Checked = True
        Me.rbWszyscy.Location = New System.Drawing.Point(6, 32)
        Me.rbWszyscy.Name = "rbWszyscy"
        Me.rbWszyscy.Size = New System.Drawing.Size(67, 17)
        Me.rbWszyscy.TabIndex = 2
        Me.rbWszyscy.TabStop = True
        Me.rbWszyscy.Text = "Wszyscy"
        Me.rbWszyscy.UseVisualStyleBackColor = True
        '
        'rbOproczWybranych
        '
        Me.rbOproczWybranych.AutoSize = True
        Me.rbOproczWybranych.Location = New System.Drawing.Point(6, 55)
        Me.rbOproczWybranych.Name = "rbOproczWybranych"
        Me.rbOproczWybranych.Size = New System.Drawing.Size(181, 17)
        Me.rbOproczWybranych.TabIndex = 3
        Me.rbOproczWybranych.Text = "Wszyscy oprócz adresów poniżej"
        Me.rbOproczWybranych.UseVisualStyleBackColor = True
        '
        'rbTylkoWybrani
        '
        Me.rbTylkoWybrani.AutoSize = True
        Me.rbTylkoWybrani.Location = New System.Drawing.Point(6, 78)
        Me.rbTylkoWybrani.Name = "rbTylkoWybrani"
        Me.rbTylkoWybrani.Size = New System.Drawing.Size(121, 17)
        Me.rbTylkoWybrani.TabIndex = 4
        Me.rbTylkoWybrani.Text = "Tylko adresy poniżej"
        Me.rbTylkoWybrani.UseVisualStyleBackColor = True
        '
        'txtAdresy
        '
        Me.txtAdresy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAdresy.Location = New System.Drawing.Point(7, 32)
        Me.txtAdresy.Multiline = True
        Me.txtAdresy.Name = "txtAdresy"
        Me.txtAdresy.Size = New System.Drawing.Size(328, 130)
        Me.txtAdresy.TabIndex = 6
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.Location = New System.Drawing.Point(6, 168)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(75, 23)
        Me.btnZapisz.TabIndex = 7
        Me.btnZapisz.Text = "Zapisz"
        Me.btnZapisz.UseVisualStyleBackColor = True
        '
        'btnOtworz
        '
        Me.btnOtworz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOtworz.Location = New System.Drawing.Point(87, 168)
        Me.btnOtworz.Name = "btnOtworz"
        Me.btnOtworz.Size = New System.Drawing.Size(75, 23)
        Me.btnOtworz.TabIndex = 8
        Me.btnOtworz.Text = "Otwórz"
        Me.btnOtworz.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(198, 327)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.Location = New System.Drawing.Point(279, 327)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 10
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.rbWszyscy)
        Me.GroupBox1.Controls.Add(Me.rbOproczWybranych)
        Me.GroupBox1.Controls.Add(Me.rbTylkoWybrani)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 106)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Opcje łączenia"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtAdresy)
        Me.GroupBox2.Controls.Add(Me.btnZapisz)
        Me.GroupBox2.Controls.Add(Me.btnOtworz)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 124)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(342, 197)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Lista adresów"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(241, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Każdy adres IP należy wprowadzić w osobnej linii."
        '
        'dlgZapisz
        '
        Me.dlgZapisz.Filter = "Pliki tekstowe|*.txt"
        '
        'dlgOtworz
        '
        Me.dlgOtworz.Filter = "Pliki tekstowe|*.txt"
        '
        'wndListaAdresowIP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 360)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "wndListaAdresowIP"
        Me.Text = "Adresy IP"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents rbWszyscy As RadioButton
    Friend WithEvents rbOproczWybranych As RadioButton
    Friend WithEvents rbTylkoWybrani As RadioButton
    Friend WithEvents txtAdresy As TextBox
    Friend WithEvents btnZapisz As Button
    Friend WithEvents btnOtworz As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents btnZamknij As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dlgZapisz As SaveFileDialog
    Friend WithEvents dlgOtworz As OpenFileDialog
End Class
