Public Class benar
    Public FormAsal As Form
    Public Akurasi As Double = 0.0

    Private Sub benar_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Hentikan musik benar saat form ditutup
        StopMusik()
        ' Putar musik otomatis kembali
        PutarMusikOtomatis()
    End Sub

    Private Sub btnUlangi_Click(sender As Object, e As EventArgs) Handles btnUlangi.Click
        ' Hentikan musik benar
        StopMusik()
        
        ' Cek apakah ada form asal yang disimpan
        Dim fLoad As New LoadingForm()
        fLoad.ShowDialog()
        If FormAsal IsNot Nothing Then
            FormAsal.Show() ' Tampilkan lagi form asalnya (misal FormSoal5)
            ' Putar musik otomatis kembali
            PutarMusikOtomatis()
        End If

        Me.Close() ' Tutup form benar
    End Sub

    Private Sub btnMenuUtama_Click(sender As Object, e As EventArgs) Handles btnMenuUtama.Click
        ' Hentikan musik benar
        StopMusik()
        
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim fMenu As New MenuPilihan()
        fMenu.Show()
        
        ' Putar musik otomatis kembali
        PutarMusikOtomatis()
    End Sub

    Private Sub benar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tampilkan akurasi di label
        lblAkurasi.Text = "Kemiripan: " & Akurasi.ToString("F2") & "%"

        ' Putar musik benar.wav dengan loop
        PutarMusikBenar()
    End Sub

    Private Sub lblAkurasi_Click(sender As Object, e As EventArgs) Handles lblAkurasi.Click

    End Sub
End Class