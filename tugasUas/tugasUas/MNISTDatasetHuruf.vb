Imports System.IO
Imports System.Runtime.InteropServices
Imports Emgu.CV
Imports Emgu.CV.CvEnum

Public Class MNISTDatasetHuruf
    Implements IDisposable

    Private images As List(Of Mat)
    Private labels As List(Of Integer)

    Public Sub New()
        images = New List(Of Mat)()
        labels = New List(Of Integer)()
    End Sub

    ' Fungsi untuk memuat dataset hanya A–E (label 1–5)
    Public Sub LoadDataset(imagesPath As String, labelsPath As String)
        If Not File.Exists(imagesPath) OrElse Not File.Exists(labelsPath) Then
            Throw New FileNotFoundException("Dataset files not found")
        End If

        images.Clear()
        labels.Clear()

        ' ====== 1) Baca semua label ======
        Dim allLabels As Integer()

        Using fsLabels As New FileStream(labelsPath, FileMode.Open, FileAccess.Read)
            Using brLabels As New BinaryReader(fsLabels)
                brLabels.ReadInt32() ' magic (ignored)
                Dim numLabels As Integer = SwapEndian(brLabels.ReadInt32())

                allLabels = New Integer(numLabels - 1) {}
                For i As Integer = 0 To numLabels - 1
                    allLabels(i) = CInt(brLabels.ReadByte())
                Next
            End Using
        End Using

        ' ====== 2) Baca images, cek label apakah A–E (label 1–5) ======
        Using fsImages As New FileStream(imagesPath, FileMode.Open, FileAccess.Read)
            Using brImages As New BinaryReader(fsImages)
                brImages.ReadInt32() ' magic (ignored)
                Dim numImages As Integer = SwapEndian(brImages.ReadInt32())
                Dim rows As Integer = SwapEndian(brImages.ReadInt32())
                Dim cols As Integer = SwapEndian(brImages.ReadInt32())

                Dim n As Integer = Math.Min(numImages, allLabels.Length)
                Dim oneLen As Integer = rows * cols

                For i As Integer = 0 To n - 1
                    Dim imageData As Byte() = brImages.ReadBytes(oneLen)
                    If imageData.Length <> oneLen Then Exit For

                    Dim label As Integer = allLabels(i)
                    If label >= 1 AndAlso label <= 5 Then ' Hanya huruf kapital A-E (label 1-5)
                        Dim raw As New Mat(rows, cols, DepthType.Cv8U, 1)
                        Marshal.Copy(imageData, 0, raw.DataPointer, imageData.Length)

                        ' === Fix orientasi EMNIST Letters (umum: transpose + flip horizontal) ===
                        Dim corrected As New Mat()

                        ' 1. Transpose (Tukar posisi X dan Y)
                        CvInvoke.Transpose(raw, corrected)

                        ' 2. Flip (Gunakan VERTICAL agar huruf tegak dan tidak terbalik kiri-kanan)
                        ' SEBELUMNYA: FlipType.Horizontal (Beresiko Mirror)
                        ' GANTI JADI: FlipType.Vertical
                        CvInvoke.Flip(corrected, corrected, FlipType.Vertical)

                        images.Add(corrected)
                        labels.Add(label)

                        raw.Dispose()
                    End If
                Next
            End Using
        End Using
    End Sub

    Private Function SwapEndian(value As Integer) As Integer
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        Array.Reverse(bytes)
        Return BitConverter.ToInt32(bytes, 0)
    End Function

    Public Function GetImage(index As Integer) As Mat
        If index >= 0 AndAlso index < images.Count Then Return images(index)
        Return Nothing
    End Function

    Public Function GetLabel(index As Integer) As Integer
        If index >= 0 AndAlso index < labels.Count Then Return labels(index)
        Return -1
    End Function

    Public Function GetImageCount() As Integer
        Return images.Count
    End Function

    ' Method untuk hanya memuat dataset dengan label tertentu (lebih efisien)
    Public Sub LoadDatasetByLabel(imagesPath As String, labelsPath As String, targetLabel As Integer)
        If Not File.Exists(imagesPath) OrElse Not File.Exists(labelsPath) Then
            Throw New FileNotFoundException("Dataset files not found")
        End If

        images.Clear()
        labels.Clear()

        ' ====== 1) Baca semua label ======
        Dim allLabels As Integer()

        Using fsLabels As New FileStream(labelsPath, FileMode.Open, FileAccess.Read)
            Using brLabels As New BinaryReader(fsLabels)
                brLabels.ReadInt32() ' magic (ignored)
                Dim numLabels As Integer = SwapEndian(brLabels.ReadInt32())

                allLabels = New Integer(numLabels - 1) {}
                For i As Integer = 0 To numLabels - 1
                    allLabels(i) = CInt(brLabels.ReadByte())
                Next
            End Using
        End Using

        ' ====== 2) Baca images, hanya simpan yang labelnya sesuai ======
        Using fsImages As New FileStream(imagesPath, FileMode.Open, FileAccess.Read)
            Using brImages As New BinaryReader(fsImages)
                brImages.ReadInt32() ' magic (ignored)
                Dim numImages As Integer = SwapEndian(brImages.ReadInt32())
                Dim rows As Integer = SwapEndian(brImages.ReadInt32())
                Dim cols As Integer = SwapEndian(brImages.ReadInt32())

                Dim n As Integer = Math.Min(numImages, allLabels.Length)
                Dim oneLen As Integer = rows * cols

                For i As Integer = 0 To n - 1
                    Dim imageData As Byte() = brImages.ReadBytes(oneLen)
                    If imageData.Length <> oneLen Then Exit For

                    Dim label As Integer = allLabels(i)
                    ' Hanya simpan jika labelnya sesuai dengan target
                    If label = targetLabel Then
                        Dim raw As New Mat(rows, cols, DepthType.Cv8U, 1)
                        Marshal.Copy(imageData, 0, raw.DataPointer, imageData.Length)

                        ' === Fix orientasi EMNIST Letters ===
                        Dim corrected As New Mat()

                        ' 1. Transpose (Tukar posisi X dan Y)
                        CvInvoke.Transpose(raw, corrected)

                        ' 2. Flip Vertical
                        CvInvoke.Flip(corrected, corrected, FlipType.Vertical)

                        images.Add(corrected)
                        labels.Add(label)

                        raw.Dispose()
                    End If
                Next
            End Using
        End Using
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If images IsNot Nothing Then
            For Each m As Mat In images
                If m IsNot Nothing Then m.Dispose()
            Next
            images.Clear()
        End If
        If labels IsNot Nothing Then labels.Clear()
    End Sub
End Class
