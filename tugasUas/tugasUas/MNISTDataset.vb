Imports System.IO
Imports Emgu.CV
Imports Emgu.CV.CvEnum

Public Class MNISTDataset
    Implements IDisposable

    Private images As List(Of Mat)
    Private labels As List(Of Integer)
    Private imageCount As Integer

    Public Sub New()
        images = New List(Of Mat)()
        labels = New List(Of Integer)()
        imageCount = 0
    End Sub

    Public Sub LoadDataset(imagesPath As String, labelsPath As String)
        If Not File.Exists(imagesPath) OrElse Not File.Exists(labelsPath) Then
            Throw New FileNotFoundException("Dataset files not found")
        End If

        Dim numLabels As Integer = 0

        ' Load labels
        Using fsLabels As New FileStream(labelsPath, FileMode.Open, FileAccess.Read)
            Using brLabels As New BinaryReader(fsLabels)
                ' Skip magic number (4 bytes)
                brLabels.ReadInt32()
                ' Read number of labels
                numLabels = SwapEndian(brLabels.ReadInt32())
                labels.Clear()

                For i As Integer = 0 To numLabels - 1
                    labels.Add(brLabels.ReadByte())
                Next
            End Using
        End Using

        ' Load images
        Using fsImages As New FileStream(imagesPath, FileMode.Open, FileAccess.Read)
            Using brImages As New BinaryReader(fsImages)
                ' Skip magic number (4 bytes)
                brImages.ReadInt32()
                ' Read number of images
                Dim numImages As Integer = SwapEndian(brImages.ReadInt32())
                Dim rows As Integer = SwapEndian(brImages.ReadInt32())
                Dim cols As Integer = SwapEndian(brImages.ReadInt32())

                images.Clear()
                imageCount = Math.Min(numImages, numLabels)

                For i As Integer = 0 To imageCount - 1
                    Dim imageData As Byte() = brImages.ReadBytes(rows * cols)
                    Dim mat As New Mat(rows, cols, DepthType.Cv8U, 1)
                    mat.SetTo(imageData)
                    images.Add(mat)
                Next
            End Using
        End Using
    End Sub

    ' Method untuk hanya memuat dataset dengan label tertentu (lebih efisien)
    Public Sub LoadDatasetByLabel(imagesPath As String, labelsPath As String, targetLabel As Integer)
        If Not File.Exists(imagesPath) OrElse Not File.Exists(labelsPath) Then
            Throw New FileNotFoundException("Dataset files not found")
        End If

        images.Clear()
        labels.Clear()
        imageCount = 0

        ' Baca semua label dulu untuk mengetahui index mana yang sesuai
        Dim allLabels As Integer()
        Using fsLabels As New FileStream(labelsPath, FileMode.Open, FileAccess.Read)
            Using brLabels As New BinaryReader(fsLabels)
                brLabels.ReadInt32() ' Skip magic number
                Dim numLabels As Integer = SwapEndian(brLabels.ReadInt32())
                allLabels = New Integer(numLabels - 1) {}

                For i As Integer = 0 To numLabels - 1
                    allLabels(i) = CInt(brLabels.ReadByte())
                Next
            End Using
        End Using

        ' Baca images, hanya simpan yang labelnya sesuai
        Using fsImages As New FileStream(imagesPath, FileMode.Open, FileAccess.Read)
            Using brImages As New BinaryReader(fsImages)
                brImages.ReadInt32() ' Skip magic number
                Dim numImages As Integer = SwapEndian(brImages.ReadInt32())
                Dim rows As Integer = SwapEndian(brImages.ReadInt32())
                Dim cols As Integer = SwapEndian(brImages.ReadInt32())

                Dim imageSize As Integer = rows * cols
                Dim n As Integer = Math.Min(numImages, allLabels.Length)

                For i As Integer = 0 To n - 1
                    Dim imageData As Byte() = brImages.ReadBytes(imageSize)
                    
                    ' Hanya simpan jika labelnya sesuai
                    If allLabels(i) = targetLabel Then
                        Dim mat As New Mat(rows, cols, DepthType.Cv8U, 1)
                        mat.SetTo(imageData)
                        images.Add(mat)
                        labels.Add(targetLabel)
                        imageCount += 1
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
        If index >= 0 AndAlso index < images.Count Then
            Return images(index)
        End If
        Return Nothing
    End Function

    Public Function GetLabel(index As Integer) As Integer
        If index >= 0 AndAlso index < labels.Count Then
            Return labels(index)
        End If
        Return -1
    End Function

    Public Function GetImageCount() As Integer
        Return imageCount
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        If images IsNot Nothing Then
            For Each img As Mat In images
                If img IsNot Nothing Then
                    img.Dispose()
                End If
            Next
            images.Clear()
        End If
    End Sub
End Class

