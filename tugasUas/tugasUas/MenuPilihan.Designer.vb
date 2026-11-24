<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuPilihan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MenuPilihan))
        btnAngka = New Button()
        btnHuruf = New Button()
        SuspendLayout()
        ' 
        ' btnAngka
        ' 
        btnAngka.BackColor = SystemColors.AppWorkspace
        btnAngka.Location = New Point(627, 392)
        btnAngka.Name = "btnAngka"
        btnAngka.Size = New Size(205, 73)
        btnAngka.TabIndex = 1
        btnAngka.Text = "ANGKA"
        btnAngka.UseVisualStyleBackColor = False
        ' 
        ' btnHuruf
        ' 
        btnHuruf.BackColor = SystemColors.ButtonShadow
        btnHuruf.Location = New Point(627, 273)
        btnHuruf.Name = "btnHuruf"
        btnHuruf.Size = New Size(205, 72)
        btnHuruf.TabIndex = 2
        btnHuruf.Text = "HURUF"
        btnHuruf.UseVisualStyleBackColor = False
        ' 
        ' MenuPilihan
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(1340, 666)
        Controls.Add(btnHuruf)
        Controls.Add(btnAngka)
        Name = "MenuPilihan"
        Text = "MenuPilihan"
        ResumeLayout(False)
    End Sub
    Friend WithEvents btnAngka As Button
    Friend WithEvents btnHuruf As Button
End Class
