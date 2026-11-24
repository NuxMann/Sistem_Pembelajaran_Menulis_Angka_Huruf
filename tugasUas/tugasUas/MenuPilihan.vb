Public Class MenuPilihan
    Private Sub MenuPilihan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnAngka_Click(sender As Object, e As EventArgs) Handles btnAngka.Click
        Using loader As New LoadingForm()
            loader.ShowDialog()
        End Using

        ' Setelah loading selesai, buka Form2 dan sembunyikan Form1
        Dim f2 As New angka()
        f2.Show()
        Me.Hide()
    End Sub

    Private Sub btnHuruf_Click(sender As Object, e As EventArgs) Handles btnHuruf.Click
        Using loader As New LoadingForm()
            loader.ShowDialog()
        End Using

        ' Setelah loading selesai, buka Form2 dan sembunyikan Form1
        Dim f2 As New huruf()
        f2.Show()
        Me.Hide()
    End Sub
End Class