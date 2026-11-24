Public Class LoadingForm
    Private rand As New Random()
    Private progressValue As Integer = 0
    Private dots As Integer = 0

    Private Sub LoadingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup visual style untuk tampilan seperti game
        Me.BackColor = Color.FromArgb(63, 79, 232)
        Me.ForeColor = Color.White

        ' ProgressBar deterministik untuk menampilkan persentase
        ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0

        lblPercent.Text = "0%"
        lblStatus.Text = "Memuat"

        ' Timer untuk animasi dan update progress
        Timer1.Interval = 50 ' update frame ~20fps
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Tambahkan kenaikan acak kecil untuk meniru loading game
        Dim inc As Integer = rand.Next(1, 5)

        ' Sesekali lompat lebih besar agar terasa dinamis
        If rand.NextDouble() < 0.05 Then
            inc += rand.Next(5, 12)
        End If

        progressValue = Math.Min(100, progressValue + inc)
        ProgressBar1.Value = progressValue
        lblPercent.Text = progressValue.ToString() & "%"

        ' Animasi titik pada status
        dots = (dots + 1) Mod 4
        lblStatus.Text = "Memuat" & New String("."c, dots)

        ' Saat selesai, hentikan dan tutup
        If progressValue >= 100 Then
            Timer1.Stop()
            Me.Close()
        End If
    End Sub
End Class
