<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class huruf
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(huruf))
        Label1 = New Label()
        btnNol = New Button()
        btnDua = New Button()
        btnTiga = New Button()
        btnEmpat = New Button()
        btnKembali = New Button()
        btnLima = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = SystemColors.ButtonShadow
        Label1.Font = New Font("Yu Gothic UI", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(278, 50)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(761, 38)
        Label1.TabIndex = 0
        Label1.Text = "Bergabunglah dalam Permainan, Pilih Huruf untuk Menulis"
        ' 
        ' btnNol
        ' 
        btnNol.BackColor = Color.DarkGreen
        btnNol.BackgroundImage = CType(resources.GetObject("btnNol.BackgroundImage"), Image)
        btnNol.BackgroundImageLayout = ImageLayout.Stretch
        btnNol.FlatStyle = FlatStyle.Popup
        btnNol.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNol.Location = New Point(42, 162)
        btnNol.Margin = New Padding(4)
        btnNol.Name = "btnNol"
        btnNol.Size = New Size(199, 152)
        btnNol.TabIndex = 1
        btnNol.UseVisualStyleBackColor = False
        ' 
        ' btnDua
        ' 
        btnDua.BackColor = Color.DarkGreen
        btnDua.BackgroundImage = CType(resources.GetObject("btnDua.BackgroundImage"), Image)
        btnDua.FlatStyle = FlatStyle.Popup
        btnDua.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnDua.Location = New Point(305, 162)
        btnDua.Margin = New Padding(4)
        btnDua.Name = "btnDua"
        btnDua.Size = New Size(199, 152)
        btnDua.TabIndex = 2
        btnDua.UseVisualStyleBackColor = False
        ' 
        ' btnTiga
        ' 
        btnTiga.BackColor = Color.DarkGreen
        btnTiga.BackgroundImage = CType(resources.GetObject("btnTiga.BackgroundImage"), Image)
        btnTiga.FlatStyle = FlatStyle.Popup
        btnTiga.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnTiga.Location = New Point(566, 162)
        btnTiga.Margin = New Padding(4)
        btnTiga.Name = "btnTiga"
        btnTiga.Size = New Size(199, 152)
        btnTiga.TabIndex = 3
        btnTiga.UseVisualStyleBackColor = False
        ' 
        ' btnEmpat
        ' 
        btnEmpat.BackColor = Color.DarkGreen
        btnEmpat.BackgroundImage = CType(resources.GetObject("btnEmpat.BackgroundImage"), Image)
        btnEmpat.FlatStyle = FlatStyle.Popup
        btnEmpat.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnEmpat.Location = New Point(834, 162)
        btnEmpat.Margin = New Padding(4)
        btnEmpat.Name = "btnEmpat"
        btnEmpat.Size = New Size(199, 152)
        btnEmpat.TabIndex = 5
        btnEmpat.UseVisualStyleBackColor = False
        ' 
        ' btnKembali
        ' 
        btnKembali.BackColor = Color.SandyBrown
        btnKembali.FlatStyle = FlatStyle.Popup
        btnKembali.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnKembali.Location = New Point(566, 453)
        btnKembali.Name = "btnKembali"
        btnKembali.Size = New Size(199, 34)
        btnKembali.TabIndex = 11
        btnKembali.Text = "Kembali"
        btnKembali.UseVisualStyleBackColor = False
        ' 
        ' btnLima
        ' 
        btnLima.BackColor = Color.DarkGreen
        btnLima.BackgroundImage = CType(resources.GetObject("btnLima.BackgroundImage"), Image)
        btnLima.FlatStyle = FlatStyle.Popup
        btnLima.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnLima.Location = New Point(1094, 162)
        btnLima.Margin = New Padding(4)
        btnLima.Name = "btnLima"
        btnLima.Size = New Size(199, 152)
        btnLima.TabIndex = 4
        btnLima.UseVisualStyleBackColor = False
        ' 
        ' huruf
        ' 
        AutoScaleDimensions = New SizeF(13F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1340, 666)
        Controls.Add(btnKembali)
        Controls.Add(btnEmpat)
        Controls.Add(btnLima)
        Controls.Add(btnTiga)
        Controls.Add(btnDua)
        Controls.Add(btnNol)
        Controls.Add(Label1)
        Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Margin = New Padding(4)
        Name = "huruf"
        Text = "Menu Angka"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnNol As Button
    Friend WithEvents btnDua As Button
    Friend WithEvents btnTiga As Button
    Friend WithEvents btnEmpat As Button
    Friend WithEvents btnKembali As Button
    Friend WithEvents btnLima As Button
End Class
