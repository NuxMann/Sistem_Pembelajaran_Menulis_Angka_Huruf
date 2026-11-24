<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class salah
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(salah))
        btnMenuUtama = New Button()
        btnUlangi = New Button()
        lblAkurasi = New Label()
        SuspendLayout()
        ' 
        ' btnMenuUtama
        ' 
        btnMenuUtama.BackColor = Color.RosyBrown
        btnMenuUtama.Location = New Point(536, 555)
        btnMenuUtama.Name = "btnMenuUtama"
        btnMenuUtama.Size = New Size(313, 53)
        btnMenuUtama.TabIndex = 0
        btnMenuUtama.Text = "MENU UTAMA"
        btnMenuUtama.UseVisualStyleBackColor = False
        ' 
        ' btnUlangi
        ' 
        btnUlangi.BackColor = Color.RosyBrown
        btnUlangi.Location = New Point(536, 485)
        btnUlangi.Name = "btnUlangi"
        btnUlangi.Size = New Size(313, 53)
        btnUlangi.TabIndex = 1
        btnUlangi.Text = "ULANGI"
        btnUlangi.UseVisualStyleBackColor = False
        ' 
        ' lblAkurasi
        ' 
        lblAkurasi.AutoSize = True
        lblAkurasi.BackColor = Color.Transparent
        lblAkurasi.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblAkurasi.ForeColor = Color.Black
        lblAkurasi.Location = New Point(25, 26)
        lblAkurasi.Name = "lblAkurasi"
        lblAkurasi.Size = New Size(111, 38)
        lblAkurasi.TabIndex = 3
        lblAkurasi.Text = "akurasi"
        ' 
        ' salah
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1340, 666)
        Controls.Add(lblAkurasi)
        Controls.Add(btnUlangi)
        Controls.Add(btnMenuUtama)
        Name = "salah"
        Text = "hasil"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnMenuUtama As Button
    Friend WithEvents btnUlangi As Button
    Friend WithEvents lblAkurasi As Label
End Class

