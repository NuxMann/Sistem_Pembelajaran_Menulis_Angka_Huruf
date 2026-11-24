Public Class home


    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Using loader As New LoadingForm()
            loader.ShowDialog()
        End Using
        Me.Close()
    End Sub

    Private Sub btnMulai_Click(sender As Object, e As EventArgs) Handles btnMulai.Click
        Using loader As New LoadingForm()
            loader.ShowDialog()
        End Using

        Dim f2 As New MenuPilihan()
        f2.Show()
        Me.Hide()
    End Sub

    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PutarMusikOtomatis()

    End Sub
End Class
