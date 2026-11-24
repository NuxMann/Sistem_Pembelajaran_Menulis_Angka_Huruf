Public Class huruf
    Private Sub huruf_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnNol_Click(sender As Object, e As EventArgs) Handles btnNol.Click
        Using loader As New LoadingForm()
            loader.ShowDialog()
        End Using

        Dim kembali As New A()
        kembali.Show()
        Me.Hide()
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim kembali As New MenuPilihan()
        kembali.Show()
        Me.Hide()
    End Sub

    Private Sub btnDua_Click(sender As Object, e As EventArgs) Handles btnDua.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim satu As New B()

        satu.Show()
        Me.Hide()
    End Sub

    Private Sub btnTiga_Click(sender As Object, e As EventArgs) Handles btnTiga.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim dua As New C()

        dua.Show()
        Me.Hide()
    End Sub

    Private Sub btnEmpat_Click(sender As Object, e As EventArgs) Handles btnEmpat.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim tiga As New D()

        tiga.Show()
        Me.Hide()
    End Sub

    Private Sub btnLima_Click(sender As Object, e As EventArgs) Handles btnLima.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim empat As New E()

        empat.Show()
        Me.Hide()
    End Sub
End Class