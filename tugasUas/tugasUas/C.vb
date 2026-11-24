Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Runtime.InteropServices
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure

Public Class C

    ' Variabel Menggambar
    Private isDrawing As Boolean = False
    Private drawingBitmap As Bitmap
    Private gCanvas As Graphics
    Private currentColor As Color = Color.Black
    Private brushSize As Integer = 20
    Private lastPoint As Point

    ' Variabel AI & Dataset
    Private mnistDataset As MNISTDatasetHuruf
    Private ssimCalculator As SSIMCalculatorH

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Siapkan Canvas
        drawingBitmap = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Using gg As Graphics = Graphics.FromImage(drawingBitmap)
            gg.Clear(Color.Transparent)
        End Using
        PictureBox1.Image = drawingBitmap

        gCanvas = Graphics.FromImage(drawingBitmap)
        gCanvas.SmoothingMode = SmoothingMode.AntiAlias

        ' 2. Set ukuran brush
        If ToolStripComboBox1.Items.Count > 0 Then
            If ToolStripComboBox1.SelectedItem Is Nothing Then
                ToolStripComboBox1.SelectedIndex = Math.Min(1, ToolStripComboBox1.Items.Count - 1)
            End If
            Dim parsed As Integer
            If Integer.TryParse(ToolStripComboBox1.SelectedItem.ToString(), parsed) Then
                brushSize = parsed * 8
            End If
        End If

        ' 3. Load Dataset
        Try
            Dim imagesPath As String = FindDatasetFile("emnist-letters-train-images-idx3-ubyte")
            Dim labelsPath As String = FindDatasetFile("emnist-letters-train-labels-idx1-ubyte")

            If String.IsNullOrEmpty(imagesPath) OrElse String.IsNullOrEmpty(labelsPath) Then
                Throw New FileNotFoundException(
                "Dataset files not found. Pastikan file ada di folder 'dataset' atau folder bin/debug:" & vbCrLf &
                "- emnist-letters-train-images-idx3-ubyte" & vbCrLf &
                "- emnist-letters-train-labels-idx1-ubyte")
            End If

            mnistDataset = New MNISTDatasetHuruf()

            ' Hanya load dataset dengan label C (label 3) untuk efisiensi
            mnistDataset.LoadDatasetByLabel(imagesPath, labelsPath, 3)



        Catch ex As Exception
        End Try

        ' 4. Inisialisasi Calculator SSIM
        ssimCalculator = New SSIMCalculatorH()
    End Sub

    ' Variabel bantu untuk tombol intip


    ' ==========================================
    ' LOGIKA MENGGAMBAR
    ' ==========================================
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            isDrawing = True
            lastPoint = e.Location
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If isDrawing Then
            Using gg As Graphics = Graphics.FromImage(drawingBitmap)
                gg.SmoothingMode = SmoothingMode.AntiAlias
                Using pen As New Pen(currentColor, brushSize)
                    pen.StartCap = LineCap.Round
                    pen.EndCap = LineCap.Round
                    gg.DrawLine(pen, lastPoint, e.Location)
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

    ' ==========================================
    ' TOOLSTRIP BUTTONS
    ' ==========================================
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        currentColor = Color.Black
        Cursor = Cursors.Cross
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        currentColor = Color.White
        Cursor = Cursors.Default
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        gCanvas.Clear(Color.Transparent)
        PictureBox1.Invalidate()
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Dim parsed As Integer
        If ToolStripComboBox1.SelectedItem IsNot Nothing AndAlso Integer.TryParse(ToolStripComboBox1.SelectedItem.ToString(), parsed) Then
            brushSize = parsed
        End If
    End Sub

    ' ==========================================
    ' LOGIKA SUBMIT
    ' ==========================================
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' 1. Cek apakah dataset sudah siap
        If mnistDataset Is Nothing OrElse mnistDataset.GetImageCount() = 0 Then
            MessageBox.Show("Dataset belum dimuat!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Matikan tombol agar tidak diklik dua kali saat loading
        btnSubmit.Enabled = False
        btnSubmit.Text = "Sedang Memproses..."
        Cursor = Cursors.WaitCursor

        Try
            ' 3. Proses gambar input (Crop & Resize)
            Using preprocessedBitmap As Bitmap = PreprocessDrawing(drawingBitmap)
                Using drawnMat As Mat = BitmapToMat(preprocessedBitmap)

                    Dim bestIndex As Integer = -1
                    Dim bestScore As Double = -1.0 ' Skor awal paling rendah
                    Dim bestLabel As Integer = -1

                    Dim totalData As Integer = mnistDataset.GetImageCount()

                    ' 4. LOOPING SEMUA DATA (Brute Force)
                    ' Ini akan memakan waktu, tapi sangat teliti.
                    For i As Integer = 0 To totalData - 1

                        Dim datasetImage As Mat = mnistDataset.GetImage(i)

                        ' Hitung skor kemiripan (SSIM)
                        Dim score As Double = ssimCalculator.CalculateSSIM(drawnMat, datasetImage)

                        ' Jika skor lebih tinggi dari rekor sebelumnya, simpan!
                        If score > bestScore Then
                            bestScore = score
                            bestIndex = i
                            bestLabel = mnistDataset.GetLabel(i)
                        End If

                        ' Update status setiap 500 data (supaya aplikasi tidak terlihat macet)
                        If i Mod 500 = 0 Then
                            Application.DoEvents() ' Agar UI tetap jalan
                        End If
                    Next

                    ' 5. Tampilkan Hasil Akhir
                    ' Konversi akurasi ke persentase
                    Dim accuracyPercent As Double = bestScore * 100

                    ' Jika prediksi = C (label 3) DAN akurasi >= 40%, ke form benar
                    ' Jika prediksi bukan C ATAU akurasi < 40%, ke form salah
                    If bestLabel = 3 AndAlso bestScore >= 0.4 Then
                        Dim fLoad As New LoadingForm()
                        fLoad.ShowDialog()

                        Dim fBenar As New benar()
                        fBenar.Akurasi = accuracyPercent
                        Try
                            fBenar.FormAsal = Me
                        Catch
                        End Try
                        fBenar.Show()
                        Me.Hide()
                    Else
                        ' Show loading (it will auto-close when finished), then open "salah"
                        Dim fLoad As New LoadingForm()
                        fLoad.ShowDialog()

                        Dim fSalah As New salah()
                        fSalah.Akurasi = accuracyPercent
                        Try
                            fSalah.FormAsal = Me
                        Catch
                        End Try
                        fSalah.Show()
                        Me.Hide()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            ' Kembalikan tombol ke kondisi semula
            btnSubmit.Enabled = True
            btnSubmit.Text = "CEK HASIL"
            Cursor = Cursors.Default
        End Try
    End Sub

    ' ==========================================
    ' FUNGSI BANTUAN
    ' ==========================================
    Private Function LabelToLetter(label As Integer) As String
        If label >= 1 AndAlso label <= 26 Then
            Return ChrW(AscW("A"c) + label - 1).ToString()
        End If
        Return "?"
    End Function

    Private Function FindDatasetFile(fileName As String) As String
        Dim possiblePaths As New List(Of String) From {
            Path.Combine(Application.StartupPath, "dataset", fileName),
            Path.Combine(Directory.GetCurrentDirectory(), "dataset", fileName),
            Path.Combine(Application.StartupPath, fileName)
        }

        Dim appDir As String = Application.StartupPath
        If appDir.Contains("bin") Then
            Dim root As String = appDir
            While root.Contains("bin") AndAlso Directory.GetParent(root) IsNot Nothing
                root = Directory.GetParent(root).FullName
            End While
            possiblePaths.Add(Path.Combine(root, "dataset", fileName))
        End If

        For Each p In possiblePaths
            If File.Exists(p) Then Return p
        Next
        Return Nothing
    End Function

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        If gCanvas IsNot Nothing Then gCanvas.Dispose()
        If drawingBitmap IsNot Nothing Then drawingBitmap.Dispose()
        If mnistDataset IsNot Nothing Then mnistDataset.Dispose()
    End Sub

    ' ==========================================
    ' IMAGE PROCESSING (Tanpa Rotasi)
    ' ==========================================
    Private Function PreprocessDrawing(originalBitmap As Bitmap) As Bitmap
        ' Proses langsung bitmap asli (karena dataset sudah Flip Vertical)
        Dim sourceBitmap As Bitmap = originalBitmap
        Dim minX As Integer = sourceBitmap.Width
        Dim minY As Integer = sourceBitmap.Height
        Dim maxX As Integer = 0
        Dim maxY As Integer = 0
        Dim found As Boolean = False

        ' 1. Cari Bounding Box
        For y As Integer = 0 To sourceBitmap.Height - 1
            For x As Integer = 0 To sourceBitmap.Width - 1
                Dim c As Color = sourceBitmap.GetPixel(x, y)
                Dim gray As Integer = CInt(c.R * 0.299 + c.G * 0.587 + c.B * 0.114)

                ' Lebih sensitif untuk huruf (threshold lebih tinggi)
                If gray < 220 Then
                    found = True
                    If x < minX Then minX = x
                    If x > maxX Then maxX = x
                    If y < minY Then minY = y
                    If y > maxY Then maxY = y
                End If
            Next
        Next

        If Not found OrElse minX >= maxX OrElse minY >= maxY Then
            Return ResizeImage(sourceBitmap, 28, 28)
        End If

        Dim padding As Integer = 10
        minX = Math.Max(0, minX - padding)
        minY = Math.Max(0, minY - padding)
        maxX = Math.Min(sourceBitmap.Width - 1, maxX + padding)
        maxY = Math.Min(sourceBitmap.Height - 1, maxY + padding)

        Dim w As Integer = maxX - minX + 1
        Dim h As Integer = maxY - minY + 1

        Using cropped As New Bitmap(w, h)
            Using gg As Graphics = Graphics.FromImage(cropped)
                gg.Clear(Color.White)
                gg.DrawImage(sourceBitmap, New Rectangle(0, 0, w, h),
                             New Rectangle(minX, minY, w, h), GraphicsUnit.Pixel)
            End Using
            Return ResizeImage(cropped, 28, 28)
        End Using
    End Function

    Private Function ResizeImage(img As Bitmap, width As Integer, height As Integer) As Bitmap
        Dim resized As New Bitmap(width, height)
        Using gg As Graphics = Graphics.FromImage(resized)
            gg.Clear(Color.White)
            gg.InterpolationMode = InterpolationMode.HighQualityBicubic
            gg.DrawImage(img, 0, 0, width, height)
        End Using
        Return resized
    End Function

    Private Function BitmapToMat(bitmap As Bitmap) As Mat
        Dim w As Integer = bitmap.Width
        Dim h As Integer = bitmap.Height
        Dim data As Byte() = New Byte(w * h - 1) {}

        ' Step 1: Convert to grayscale and invert
        Dim grayValues As Integer() = New Integer(w * h - 1) {}
        For y As Integer = 0 To h - 1
            For x As Integer = 0 To w - 1
                Dim p As Color = bitmap.GetPixel(x, y)
                Dim gray As Integer = CInt(p.R * 0.299 + p.G * 0.587 + p.B * 0.114)
                ' Invert: white (255) becomes black (0), black (0) becomes white (255)
                Dim inv As Integer = 255 - gray
                grayValues(y * w + x) = inv
            Next
        Next

        ' Step 2: Find min and max for better normalization
        Dim minVal As Integer = 255
        Dim maxVal As Integer = 0
        For i As Integer = 0 To grayValues.Length - 1
            If grayValues(i) < minVal Then minVal = grayValues(i)
            If grayValues(i) > maxVal Then maxVal = grayValues(i)
        Next

        ' Step 3: Apply adaptive threshold (lebih rendah untuk huruf - lebih sensitif)
        Dim threshold As Integer = 20 ' Lebih rendah untuk menangkap lebih banyak detail huruf
        Dim range As Integer = maxVal - minVal

        For y As Integer = 0 To h - 1
            For x As Integer = 0 To w - 1
                Dim idx As Integer = y * w + x
                Dim inv As Integer = grayValues(idx)

                ' Apply threshold yang lebih rendah untuk huruf
                If inv < threshold Then
                    data(idx) = 0
                Else
                    ' Normalize dengan kontras yang lebih baik untuk huruf
                    If range > 0 Then
                        ' Stretch contrast untuk meningkatkan detail
                        Dim normalized As Double = (inv - minVal) / range
                        ' Apply gamma correction yang lebih rendah untuk lebih terang (huruf)
                        normalized = Math.Pow(normalized, 0.7) ' Lebih rendah dari 0.8 untuk lebih terang
                        ' Boost kontras lebih agresif untuk huruf
                        normalized = normalized * 1.15 ' Boost 15% lebih
                        data(idx) = CByte(Math.Min(255, Math.Max(0, normalized * 255)))
                    Else
                        data(idx) = CByte(inv)
                    End If
                End If
            Next
        Next

        Dim mat As New Mat(h, w, DepthType.Cv8U, 1)
        Marshal.Copy(data, 0, mat.DataPointer, data.Length)
        
        ' Step 4: Apply minimal smoothing untuk huruf (kurangi blur)
        Dim smoothed As New Mat()
        CvInvoke.GaussianBlur(mat, smoothed, New Size(3, 3), 0.3) ' Sigma lebih kecil untuk huruf
        mat.Dispose()
        
        Return smoothed
    End Function

End Class
