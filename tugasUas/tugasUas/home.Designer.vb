<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Home
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(home))
        lblBelajar = New Label()
        btnMulai = New Button()
        btnKeluar = New Button()
        SuspendLayout()
        ' 
        ' lblBelajar
        ' 
        lblBelajar.AutoSize = True
        lblBelajar.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblBelajar.Location = New Point(360, 70)
        lblBelajar.Name = "lblBelajar"
        lblBelajar.Size = New Size(317, 48)
        lblBelajar.TabIndex = 0
        lblBelajar.Text = "BELAJAR MENULIS"
        ' 
        ' btnMulai
        ' 
        btnMulai.BackColor = Color.Tomato
        btnMulai.FlatStyle = FlatStyle.Flat
        btnMulai.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnMulai.ForeColor = Color.Snow
        btnMulai.Location = New Point(381, 231)
        btnMulai.Name = "btnMulai"
        btnMulai.Size = New Size(279, 53)
        btnMulai.TabIndex = 1
        btnMulai.Text = "MULAI"
        btnMulai.UseVisualStyleBackColor = False
        ' 
        ' btnKeluar
        ' 
        btnKeluar.BackColor = Color.Tomato
        btnKeluar.FlatStyle = FlatStyle.Flat
        btnKeluar.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnKeluar.ForeColor = Color.Snow
        btnKeluar.Location = New Point(381, 305)
        btnKeluar.Name = "btnKeluar"
        btnKeluar.Size = New Size(279, 53)
        btnKeluar.TabIndex = 2
        btnKeluar.Text = "KELUAR"
        btnKeluar.UseVisualStyleBackColor = False
        ' 
        ' home
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1031, 555)
        Controls.Add(btnKeluar)
        Controls.Add(btnMulai)
        Controls.Add(lblBelajar)
        Name = "home"
        Text = "Menu Utama"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblBelajar As Label
    Friend WithEvents btnMulai As Button
    Friend WithEvents btnKeluar As Button

End Class
