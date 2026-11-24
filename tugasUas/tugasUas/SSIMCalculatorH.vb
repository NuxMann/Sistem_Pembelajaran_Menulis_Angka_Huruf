Imports Emgu.CV
Imports Emgu.CV.Structure

Public Class SSIMCalculatorH
    ' Fungsi untuk menghitung SSIM antara dua gambar
    Public Function CalculateSSIM(a As Mat, b As Mat) As Double
        ' Pastikan kedua gambar tidak null dan memiliki ukuran yang sama
        If a Is Nothing OrElse b Is Nothing Then Return 0.0
        If a.Rows <> b.Rows OrElse a.Cols <> b.Cols Then Return 0.0
        If a.NumberOfChannels <> 1 OrElse b.NumberOfChannels <> 1 Then Return 0.0

        Using imgA = a.ToImage(Of Gray, Byte)()
            Using imgB = b.ToImage(Of Gray, Byte)()
                Dim w As Integer = imgA.Width
                Dim h As Integer = imgA.Height
                Dim n As Integer = w * h
                If n = 0 Then Return 0.0

                Dim sumA As Double = 0
                Dim sumB As Double = 0

                ' Hitung jumlah nilai intensitas piksel untuk kedua gambar
                For y As Integer = 0 To h - 1
                    For x As Integer = 0 To w - 1
                        sumA += imgA.Data(y, x, 0)
                        sumB += imgB.Data(y, x, 0)
                    Next
                Next

                Dim muA As Double = sumA / n
                Dim muB As Double = sumB / n

                ' Hitung variansi dan kovarians
                Dim varA As Double = 0
                Dim varB As Double = 0
                Dim cov As Double = 0

                For y As Integer = 0 To h - 1
                    For x As Integer = 0 To w - 1
                        Dim da As Double = imgA.Data(y, x, 0) - muA
                        Dim db As Double = imgB.Data(y, x, 0) - muB
                        varA += da * da
                        varB += db * db
                        cov += da * db
                    Next
                Next

                varA /= n
                varB /= n
                cov /= n

                ' Parameter SSIM constants
                Dim L As Double = 255.0
                Dim K1 As Double = 0.01
                Dim K2 As Double = 0.03
                Dim C1 As Double = (K1 * L) * (K1 * L)
                Dim C2 As Double = (K2 * L) * (K2 * L)

                ' Hitung SSIM
                Dim numerator As Double = (2 * muA * muB + C1) * (2 * cov + C2)
                Dim denominator As Double = (muA * muA + muB * muB + C1) * (varA + varB + C2)

                If denominator = 0 Then Return 0.0
                Dim ssim As Double = numerator / denominator

                ' Klamp hasil SSIM agar berada dalam rentang -1 hingga 1
                If ssim < -1 Then ssim = -1
                If ssim > 1 Then ssim = 1
                Return ssim
            End Using
        End Using
    End Function
End Class
