Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure


Public Class sembilan
    ' Drawing state
    Private isDrawing As Boolean = False
    Private drawingBitmap As Bitmap
    Private g As Graphics
    Private currentColor As Color = Color.Black
    Private brushSize As Integer = 2
    Private lastPoint As Point
    Private mnistDataset As MNISTDataset
    Private ssimCalculator As SSIMCalculator


    Private Sub sembilan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inisialisasi canvas sesuai ukuran PictureBox
        drawingBitmap = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Using g As Graphics = Graphics.FromImage(drawingBitmap)
            g.Clear(Color.Transparent)
        End Using
        PictureBox1.Image = drawingBitmap

        g = Graphics.FromImage(drawingBitmap)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Set default selection di combobox jika kosong
        If ToolStripComboBox1.SelectedItem Is Nothing Then
            ToolStripComboBox1.SelectedIndex = 1 ' ukuran default ~2 (item index)
            brushSize = Convert.ToInt32(ToolStripComboBox1.SelectedItem) * 8
        Else
            brushSize = Convert.ToInt32(ToolStripComboBox1.SelectedItem) * 8
        End If

        Try
            ' Try to find dataset files in multiple locations
            Dim imagesPath As String = FindDatasetFile("train-images.idx3-ubyte")
            Dim labelsPath As String = FindDatasetFile("train-labels.idx1-ubyte")

            If String.IsNullOrEmpty(imagesPath) OrElse String.IsNullOrEmpty(labelsPath) Then
                Throw New FileNotFoundException("Dataset files not found. Please ensure the following files exist in 'dataset' folder:" & vbCrLf &
                                                "- train-images.idx3-ubyte" & vbCrLf &
                                                "- train-labels.idx1-ubyte")
            End If

            mnistDataset = New MNISTDataset()
            ' Hanya load dataset dengan label 9 untuk efisiensi
            mnistDataset.LoadDatasetByLabel(imagesPath, labelsPath, 9)
        Catch ex As Exception
            MessageBox.Show("Error loading dataset: " & ex.Message & vbCrLf & vbCrLf &
                           "Please ensure the dataset files are in the 'dataset' folder:" & vbCrLf &
                           "- train-images.idx3-ubyte" & vbCrLf &
                           "- train-labels.idx1-ubyte",
                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ssimCalculator = New SSIMCalculator()


    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            isDrawing = True
            lastPoint = e.Location
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If isDrawing Then
            Using g As Graphics = Graphics.FromImage(drawingBitmap)
                Using pen As New Pen(currentColor, brushSize)
                    pen.StartCap = Drawing2D.LineCap.Round
                    pen.EndCap = Drawing2D.LineCap.Round
                    g.DrawLine(pen, lastPoint, e.Location)
                End Using
            End Using
            lastPoint = e.Location
            PictureBox1.Invalidate()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        If e.Button = MouseButtons.Left Then
            isDrawing = False
        End If
    End Sub

    ' ToolStrip button actions
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        ' Pen
        currentColor = Color.Black
        Cursor = Cursors.Cross
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        ' Eraser: set color to PictureBox background (white)
        currentColor = PictureBox1.BackColor
        Cursor = Cursors.Default
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        ' Clear canvas
        g.Clear(PictureBox1.BackColor)
        PictureBox1.Invalidate()
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            If ToolStripComboBox1.SelectedItem IsNot Nothing Then
                brushSize = Convert.ToInt32(ToolStripComboBox1.SelectedItem)
            End If
        Catch ex As Exception
            ' ignore parse errors
        End Try
    End Sub

    Private Function FindDatasetFile(fileName As String) As String
        ' Try multiple possible locations
        Dim possiblePaths As New List(Of String)

        ' 1. Application startup path (output directory)
        possiblePaths.Add(Path.Combine(Application.StartupPath, "dataset", fileName))

        ' 2. Current directory
        possiblePaths.Add(Path.Combine(Directory.GetCurrentDirectory(), "dataset", fileName))

        ' 3. Project root (go up from bin/Debug/net8.0-windows)
        Dim appDir As String = Application.StartupPath
        If appDir.Contains("bin") Then
            Dim projectRoot As String = appDir
            While projectRoot.Contains("bin") AndAlso Not String.IsNullOrEmpty(projectRoot)
                projectRoot = Directory.GetParent(projectRoot).FullName
            End While
            possiblePaths.Add(Path.Combine(projectRoot, "dataset", fileName))
        End If

        ' 4. Relative to executable
        possiblePaths.Add(Path.Combine("dataset", fileName))

        ' Check each path
        For Each path As String In possiblePaths
            If File.Exists(path) Then
                Return path
            End If
        Next

        Return Nothing
    End Function

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        If g IsNot Nothing Then g.Dispose()
        If drawingBitmap IsNot Nothing Then drawingBitmap.Dispose()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If mnistDataset Is Nothing OrElse mnistDataset.GetImageCount() = 0 Then
            MessageBox.Show("Dataset belum dimuat!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Preprocess drawing: crop, center, and normalize
        Dim preprocessedBitmap As Bitmap = PreprocessDrawing(drawingBitmap)
        Dim drawnImage As Mat = BitmapToMat(preprocessedBitmap)

        ' Find best match using SSIM - search through all samples
        Dim bestMatch As Integer = -1
        Dim bestAccuracy As Double = 0.0
        Dim bestLabel As Integer = -1

        ' Search through all loaded samples (sudah difilter hanya label 9)
        Dim searchLimit As Integer = mnistDataset.GetImageCount() - 1

        ' Use progress indicator
        Application.DoEvents()

        For i As Integer = 0 To searchLimit
            Dim datasetImage As Mat = mnistDataset.GetImage(i)
            Dim datasetLabel As Integer = mnistDataset.GetLabel(i)
            Dim accuracy As Double = ssimCalculator.CalculateSSIM(drawnImage, datasetImage)

            If accuracy > bestAccuracy Then
                bestAccuracy = accuracy
                bestMatch = i
                bestLabel = datasetLabel
            End If
        Next

        ' Konversi akurasi ke persentase
        Dim accuracyPercent As Double = bestAccuracy * 100

        Try
            ' Jika prediksi = 9 DAN akurasi >= 40%, ke form benar
            ' Jika prediksi bukan 9 ATAU akurasi < 40%, ke form salah
            If bestLabel = 9 AndAlso bestAccuracy >= 0.4 Then
                Dim fLoad As New LoadingForm()
                fLoad.ShowDialog()
                Dim fBenar As New benar()
                fBenar.Akurasi = accuracyPercent
                fBenar.Show()
            Else
                ' Show loading (it will auto-close when finished), then open "salah"
                Dim fLoad As New LoadingForm()
                fLoad.ShowDialog()
                Dim fSalah As New salah()
                fSalah.Akurasi = accuracyPercent
                fSalah.Show()
            End If
        Catch ex As Exception
            ' Fallback: if opening forms fails, just show message
            MessageBox.Show("Gagal membuka form tujuan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            drawnImage.Dispose()
            preprocessedBitmap.Dispose()
        End Try
    End Sub

    Private Function PreprocessDrawing(originalBitmap As Bitmap) As Bitmap
        ' Step 1: Find bounding box of the drawing
        Dim minX As Integer = originalBitmap.Width
        Dim minY As Integer = originalBitmap.Height
        Dim maxX As Integer = 0
        Dim maxY As Integer = 0

        For y As Integer = 0 To originalBitmap.Height - 1
            For x As Integer = 0 To originalBitmap.Width - 1
                Dim pixel As Color = originalBitmap.GetPixel(x, y)
                Dim gray As Integer = CInt(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114)
                ' Check if pixel is dark (not white background)
                If gray < 200 Then
                    If x < minX Then minX = x
                    If x > maxX Then maxX = x
                    If y < minY Then minY = y
                    If y > maxY Then maxY = y
                End If
            Next
        Next

        ' If no drawing found, return resized original
        If minX >= maxX OrElse minY >= maxY Then
            Return ResizeImage(originalBitmap, 28, 28)
        End If

        ' Add padding
        Dim padding As Integer = 10
        minX = Math.Max(0, minX - padding)
        minY = Math.Max(0, minY - padding)
        maxX = Math.Min(originalBitmap.Width - 1, maxX + padding)
        maxY = Math.Min(originalBitmap.Height - 1, maxY + padding)

        ' Step 2: Crop to bounding box
        Dim width As Integer = maxX - minX + 1
        Dim height As Integer = maxY - minY + 1
        Dim cropped As New Bitmap(width, height)

        Using g As Graphics = Graphics.FromImage(cropped)
            g.DrawImage(originalBitmap, New Rectangle(0, 0, width, height),
                       New Rectangle(minX, minY, width, height), GraphicsUnit.Pixel)
        End Using

        ' Step 3: Resize to 28x28 while maintaining aspect ratio and centering
        Dim finalSize As Integer = 28
        Dim finalBitmap As New Bitmap(finalSize, finalSize)

        Using g As Graphics = Graphics.FromImage(finalBitmap)
            g.Clear(Color.White)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic

            ' Calculate scaling to fit while maintaining aspect ratio
            Dim scaleX As Double = finalSize / width
            Dim scaleY As Double = finalSize / height
            Dim scale As Double = Math.Min(scaleX, scaleY) * 0.9 ' 90% to add padding

            Dim scaledWidth As Integer = CInt(width * scale)
            Dim scaledHeight As Integer = CInt(height * scale)
            Dim offsetX As Integer = (finalSize - scaledWidth) \ 2
            Dim offsetY As Integer = (finalSize - scaledHeight) \ 2

            g.DrawImage(cropped, New Rectangle(offsetX, offsetY, scaledWidth, scaledHeight))
        End Using

        cropped.Dispose()
        Return finalBitmap
    End Function

    Private Function ResizeImage(img As Bitmap, width As Integer, height As Integer) As Bitmap
        Dim resized As New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(resized)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.DrawImage(img, 0, 0, width, height)
        End Using
        Return resized
    End Function

    Private Function BitmapToMat(bitmap As Bitmap) As Mat
        Dim mat As New Mat(bitmap.Height, bitmap.Width, DepthType.Cv8U, 1)
        Dim data As Byte() = New Byte(bitmap.Width * bitmap.Height - 1) {}

        ' Step 1: Convert to grayscale and invert
        Dim grayValues As Integer() = New Integer(bitmap.Width * bitmap.Height - 1) {}
        For y As Integer = 0 To bitmap.Height - 1
            For x As Integer = 0 To bitmap.Width - 1
                Dim pixel As Color = bitmap.GetPixel(x, y)
                Dim gray As Integer = CInt(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114)
                ' Invert: white (255) becomes black (0), black (0) becomes white (255)
                Dim inverted As Integer = 255 - gray
                grayValues(y * bitmap.Width + x) = inverted
            Next
        Next

        ' Step 2: Find min and max for better normalization
        Dim minVal As Integer = 255
        Dim maxVal As Integer = 0
        For i As Integer = 0 To grayValues.Length - 1
            If grayValues(i) < minVal Then minVal = grayValues(i)
            If grayValues(i) > maxVal Then maxVal = grayValues(i)
        Next

        ' Step 3: Apply adaptive threshold (lebih rendah untuk menangkap lebih banyak detail)
        Dim threshold As Integer = 30 ' Dikurangi dari 50 untuk lebih sensitif
        Dim range As Integer = maxVal - minVal

        For y As Integer = 0 To bitmap.Height - 1
            For x As Integer = 0 To bitmap.Width - 1
                Dim idx As Integer = y * bitmap.Width + x
                Dim inverted As Integer = grayValues(idx)

                ' Apply threshold
                If inverted < threshold Then
                    data(idx) = 0
                Else
                    ' Normalize dengan kontras yang lebih baik
                    If range > 0 Then
                        ' Stretch contrast untuk meningkatkan detail
                        Dim normalized As Double = (inverted - minVal) / range
                        ' Apply gamma correction untuk meningkatkan kontras
                        normalized = Math.Pow(normalized, 0.8)
                        data(idx) = CByte(Math.Min(255, Math.Max(0, normalized * 255)))
                    Else
                        data(idx) = CByte(inverted)
                    End If
                End If
            Next
        Next

        mat.SetTo(data)
        
        ' Step 4: Apply slight smoothing untuk mengurangi noise
        Dim smoothed As New Mat()
        CvInvoke.GaussianBlur(mat, smoothed, New Size(3, 3), 0.5)
        mat.Dispose()
        
        Return smoothed
    End Function
End Class