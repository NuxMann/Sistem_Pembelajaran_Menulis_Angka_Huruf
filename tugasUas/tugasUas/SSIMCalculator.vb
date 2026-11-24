Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure

Public Class SSIMCalculator
    ' Menggunakan constants yang lebih sensitif untuk meningkatkan akurasi
    Private Const C1 As Double = 0.01 ^ 2
    Private Const C2 As Double = 0.03 ^ 2

    Public Function CalculateSSIM(img1 As Mat, img2 As Mat) As Double
        Dim resized As Mat = Nothing
        Dim img2ToUse As Mat = img2

        ' Ensure images are same size
        If img1.Size <> img2.Size Then
            resized = New Mat()
            CvInvoke.Resize(img2, resized, img1.Size)
            img2ToUse = resized
        End If

        ' Convert to double for calculation
        Dim img1Double As New Mat()
        Dim img2Double As New Mat()
        img1.ConvertTo(img1Double, DepthType.Cv64F)
        img2ToUse.ConvertTo(img2Double, DepthType.Cv64F)

        ' Calculate mean
        Dim mean1 As MCvScalar = CvInvoke.Mean(img1Double)
        Dim mean2 As MCvScalar = CvInvoke.Mean(img2Double)
        Dim mu1 As Double = mean1.V0
        Dim mu2 As Double = mean2.V0

        ' Calculate variance and covariance
        Dim img1Squared As New Mat()
        Dim img2Squared As New Mat()
        Dim img1Img2 As New Mat()

        CvInvoke.Multiply(img1Double, img1Double, img1Squared)
        CvInvoke.Multiply(img2Double, img2Double, img2Squared)
        CvInvoke.Multiply(img1Double, img2Double, img1Img2)

        Dim mean1Squared As MCvScalar = CvInvoke.Mean(img1Squared)
        Dim mean2Squared As MCvScalar = CvInvoke.Mean(img2Squared)
        Dim mean12 As MCvScalar = CvInvoke.Mean(img1Img2)

        Dim sigma1Squared As Double = mean1Squared.V0 - mu1 * mu1
        Dim sigma2Squared As Double = mean2Squared.V0 - mu2 * mu2
        Dim sigma12 As Double = mean12.V0 - mu1 * mu2

        ' Calculate SSIM
        Dim numerator1 As Double = 2 * mu1 * mu2 + C1
        Dim numerator2 As Double = 2 * sigma12 + C2
        Dim denominator1 As Double = mu1 * mu1 + mu2 * mu2 + C1
        Dim denominator2 As Double = sigma1Squared + sigma2Squared + C2

        ' Calculate SSIM (handle division by zero)
        Dim ssim As Double = 0.0
        If denominator1 > 0 AndAlso denominator2 > 0 Then
            ssim = (numerator1 * numerator2) / (denominator1 * denominator2)
        End If

        ' Cleanup
        img1Double.Dispose()
        img2Double.Dispose()
        img1Squared.Dispose()
        img2Squared.Dispose()
        img1Img2.Dispose()
        If resized IsNot Nothing Then
            resized.Dispose()
        End If

        Return Math.Max(0, Math.Min(1, ssim))
    End Function
End Class

