Public Class angka

    Private Sub btnNol_Click(sender As Object, e As EventArgs) Handles btnNol.Click
        Using loader As New LoadingForm()
            loader.ShowDialog()
        End Using

        Dim kembali As New nol()
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

        Dim satu As New satu()

        satu.Show()
        Me.Hide()
    End Sub

    Private Sub btnTiga_Click(sender As Object, e As EventArgs) Handles btnTiga.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim dua As New dua()

        dua.Show()
        Me.Hide()
    End Sub

    Private Sub btnEmpat_Click(sender As Object, e As EventArgs) Handles btnEmpat.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim tiga As New tiga()

        tiga.Show()
        Me.Hide()
    End Sub

    Private Sub btnLima_Click(sender As Object, e As EventArgs) Handles btnLima.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim empat As New empat()

        empat.Show()
        Me.Hide()
    End Sub

    Private Sub btnEnam_Click(sender As Object, e As EventArgs) Handles btnEnam.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim lima As New lima()

        lima.Show()
        Me.Hide()
    End Sub

    Private Sub btnTujuh_Click(sender As Object, e As EventArgs) Handles btnTujuh.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim enam As New enam()

        enam.Show()
        Me.Hide()
    End Sub

    Private Sub btnDelapan_Click(sender As Object, e As EventArgs) Handles btnDelapan.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim tujuh As New tujuh()

        tujuh.Show()
        Me.Hide()

    End Sub

    Private Sub btnSembbilan_Click(sender As Object, e As EventArgs) Handles btnSembbilan.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim delapan As New delapan()

        delapan.Show()
        Me.Hide()
    End Sub

    Private Sub btnSepuluh_Click(sender As Object, e As EventArgs) Handles btnSepuluh.Click
        Dim fload As New LoadingForm()
        fload.ShowDialog()

        Dim sembilan As New sembilan()
        sembilan.Show()
        Me.Hide()
    End Sub

    Private Sub angka_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class