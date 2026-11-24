<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoadingForm
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoadingForm))
        ProgressBar1 = New ProgressBar()
        Timer1 = New Timer(components)
        pbLogo = New PictureBox()
        lblPercent = New Label()
        lblStatus = New Label()
        CType(pbLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(92, 40)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(280, 20)
        ProgressBar1.Style = ProgressBarStyle.Continuous
        ProgressBar1.TabIndex = 1
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 50
        ' 
        ' pbLogo
        ' 
        pbLogo.BackColor = Color.Transparent
        pbLogo.BackgroundImage = CType(resources.GetObject("pbLogo.BackgroundImage"), Image)
        pbLogo.BackgroundImageLayout = ImageLayout.Stretch
        pbLogo.Location = New Point(12, 12)
        pbLogo.Name = "pbLogo"
        pbLogo.Size = New Size(64, 64)
        pbLogo.TabIndex = 0
        pbLogo.TabStop = False
        ' 
        ' lblPercent
        ' 
        lblPercent.AutoSize = True
        lblPercent.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPercent.ForeColor = Color.White
        lblPercent.Location = New Point(374, 32)
        lblPercent.Name = "lblPercent"
        lblPercent.Size = New Size(49, 32)
        lblPercent.TabIndex = 2
        lblPercent.Text = "0%"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblStatus.ForeColor = Color.White
        lblStatus.Location = New Point(92, 12)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(85, 28)
        lblStatus.TabIndex = 3
        lblStatus.Text = "Memuat"
        ' 
        ' LoadingForm
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(430, 88)
        ControlBox = False
        Controls.Add(lblStatus)
        Controls.Add(lblPercent)
        Controls.Add(ProgressBar1)
        Controls.Add(pbLogo)
        FormBorderStyle = FormBorderStyle.None
        Name = "LoadingForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Loading..."
        CType(pbLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
