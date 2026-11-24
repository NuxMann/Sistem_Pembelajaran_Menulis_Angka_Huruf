Public Class salah

    Public FormAsal As Form
    Public Akurasi As Double = 0.0

    Private Sub salah_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Hentikan musik salah saat form ditutup
        StopMusik()
        ' Putar musik otomatis kembali
        PutarMusikOtomatis()
    End Sub

    Private Sub btnUlangi_Click(sender As Object, e As EventArgs) Handles btnUlangi.Click
        ' Hentikan musik salah
        StopMusik()
        
        ' Cek apakah ada form asal yang disimpan
        Dim fLoad As New LoadingForm()
        fLoad.ShowDialog()
        If FormAsal IsNot Nothing Then
            FormAsal.Show() ' Tampilkan lagi form asalnya (misal FormSoal5)
            ' Putar musik otomatis kembali
            PutarMusikOtomatis()
        End If

        Me.Close() ' Tutup form salah
    End Sub

    Private Sub btnMenuUtama_Click(sender As Object, e As EventArgs) Handles btnMenuUtama.Click
        ' Hentikan musik salah
        StopMusik()
        
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim fMenu As New MenuPilihan()
        fMenu.Show()
        
        ' Putar musik otomatis kembali
        PutarMusikOtomatis()
    End Sub

    Private Sub salah_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tampilkan akurasi di label
        lblAkurasi.Text = "Kemiripan: " & Akurasi.ToString("F2") & "%"

        ' Putar musik salah.wav dengan loop
        PutarMusikSalah()
    End Sub

    Private Sub lblAkurasi_Click(sender As Object, e As EventArgs) Handles lblAkurasi.Click

    End Sub
End Class