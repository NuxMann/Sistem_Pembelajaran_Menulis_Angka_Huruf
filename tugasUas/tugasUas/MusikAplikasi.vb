Imports System.IO
Imports System.Media
Imports System.Diagnostics
Imports System.Threading

Module MusikAplikasi

    Private soundPlayer As SoundPlayer = Nothing
    Private musikPath As String = Nothing
    Private isPlaying As Boolean = False
    Private loopThread As Thread = Nothing

    ' Prosedur untuk memutar musik dari folder musik
    Public Sub PutarMusikOtomatis()
        ' Hentikan musik yang sedang berjalan
        StopMusik()

        Try
            ' Daftar lokasi yang akan dicari
            Dim lokasiCari As New List(Of String) From {
                Path.Combine(Application.StartupPath, "musik", "lagu.wav"),
                Path.Combine(Application.StartupPath, "lagu.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "musik", "lagu.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "lagu.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "musik", "lagu.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lagu.wav")
            }

            ' Cari file di semua lokasi
            musikPath = Nothing
            For Each lokasi In lokasiCari
                If File.Exists(lokasi) Then
                    musikPath = lokasi
                    Exit For
                End If
            Next

            ' Jika file ditemukan, putar dengan loop
            If musikPath IsNot Nothing Then
                Dim fileInfo As New FileInfo(musikPath)
                If fileInfo.Exists AndAlso fileInfo.Length > 0 Then
                    ' Coba My.Computer.Audio dulu (paling sederhana)
                    Try
                        My.Computer.Audio.Play(musikPath, AudioPlayMode.BackgroundLoop)
                        Debug.WriteLine("✓ Musik diputar (My.Computer.Audio): " & musikPath)
                        isPlaying = True
                    Catch exAudio As Exception
                        Debug.WriteLine("My.Computer.Audio gagal: " & exAudio.Message)
                        ' Fallback: Gunakan SoundPlayer dengan loop manual
                        Try
                            isPlaying = True
                            soundPlayer = New SoundPlayer(musikPath)
                            soundPlayer.Load()

                            ' Buat thread untuk loop musik
                            loopThread = New Thread(AddressOf LoopMusik)
                            loopThread.IsBackground = True
                            loopThread.Start()

                            Debug.WriteLine("✓ Musik diputar (SoundPlayer loop): " & musikPath)
                        Catch exSound As Exception
                            Debug.WriteLine("SoundPlayer juga gagal: " & exSound.Message)
                            isPlaying = False
                        End Try
                    End Try
                Else
                    Debug.WriteLine("✗ File tidak valid atau kosong: " & musikPath)
                End If
            Else
                Debug.WriteLine("✗ File lagu.wav TIDAK DITEMUKAN")
            End If

        Catch ex As Exception
            Debug.WriteLine("Error memutar musik: " & ex.Message)
            isPlaying = False
        End Try
    End Sub

    ' Thread untuk loop musik
    Private Sub LoopMusik()
        While isPlaying AndAlso musikPath IsNot Nothing
            Try
                If soundPlayer IsNot Nothing Then
                    ' PlaySync akan block sampai musik selesai
                    soundPlayer.PlaySync()
                    ' Setelah selesai, buat SoundPlayer baru untuk loop
                    If isPlaying Then
                        soundPlayer.Dispose()
                        soundPlayer = New SoundPlayer(musikPath)
                        soundPlayer.Load()
                    End If
                Else
                    Exit While
                End If
            Catch
                Exit While
            End Try
        End While
    End Sub

    ' Prosedur untuk mematikan musik
    Public Sub StopMusik()
        isPlaying = False

        ' Hentikan thread loop
        If loopThread IsNot Nothing AndAlso loopThread.IsAlive Then
            Try
                loopThread.Join(1000) ' Tunggu maksimal 1 detik
            Catch
            End Try
            loopThread = Nothing
        End If

        ' Hentikan SoundPlayer
        If soundPlayer IsNot Nothing Then
            Try
                soundPlayer.Stop()
            Catch
            End Try
            Try
                soundPlayer.Dispose()
            Catch
            End Try
            soundPlayer = Nothing
        End If

        ' Hentikan My.Computer.Audio
        Try
            My.Computer.Audio.Stop()
        Catch
        End Try

        musikPath = Nothing
    End Sub

    ' Prosedur untuk memutar musik benar.wav dengan loop
    Public Sub PutarMusikBenar()
        ' Hentikan musik yang sedang berjalan
        StopMusik()

        Try
            ' Daftar lokasi yang akan dicari
            Dim lokasiCari As New List(Of String) From {
                Path.Combine(Application.StartupPath, "musik", "benar.wav"),
                Path.Combine(Application.StartupPath, "benar.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "musik", "benar.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "benar.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "musik", "benar.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "benar.wav")
            }

            ' Cari file di semua lokasi
            musikPath = Nothing
            For Each lokasi In lokasiCari
                If File.Exists(lokasi) Then
                    musikPath = lokasi
                    Exit For
                End If
            Next

            ' Jika file ditemukan, putar dengan loop
            If musikPath IsNot Nothing Then
                Dim fileInfo As New FileInfo(musikPath)
                If fileInfo.Exists AndAlso fileInfo.Length > 0 Then
                    Try
                        My.Computer.Audio.Play(musikPath, AudioPlayMode.BackgroundLoop)
                        Debug.WriteLine("✓ Musik benar diputar (My.Computer.Audio): " & musikPath)
                        isPlaying = True
                    Catch exAudio As Exception
                        Debug.WriteLine("My.Computer.Audio gagal: " & exAudio.Message)
                        ' Fallback: Gunakan SoundPlayer dengan loop manual
                        Try
                            isPlaying = True
                            soundPlayer = New SoundPlayer(musikPath)
                            soundPlayer.Load()

                            ' Buat thread untuk loop musik
                            loopThread = New Thread(AddressOf LoopMusik)
                            loopThread.IsBackground = True
                            loopThread.Start()

                            Debug.WriteLine("✓ Musik benar diputar (SoundPlayer loop): " & musikPath)
                        Catch exSound As Exception
                            Debug.WriteLine("SoundPlayer juga gagal: " & exSound.Message)
                            isPlaying = False
                        End Try
                    End Try
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("Error memutar musik benar: " & ex.Message)
            isPlaying = False
        End Try
    End Sub

    ' Prosedur untuk memutar musik salah.wav dengan loop
    Public Sub PutarMusikSalah()
        ' Hentikan musik yang sedang berjalan
        StopMusik()

        Try
            ' Daftar lokasi yang akan dicari
            Dim lokasiCari As New List(Of String) From {
                Path.Combine(Application.StartupPath, "musik", "salah.wav"),
                Path.Combine(Application.StartupPath, "salah.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "musik", "salah.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "salah.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "musik", "salah.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "salah.wav")
            }

            ' Cari file di semua lokasi
            musikPath = Nothing
            For Each lokasi In lokasiCari
                If File.Exists(lokasi) Then
                    musikPath = lokasi
                    Exit For
                End If
            Next

            ' Jika file ditemukan, putar dengan loop
            If musikPath IsNot Nothing Then
                Dim fileInfo As New FileInfo(musikPath)
                If fileInfo.Exists AndAlso fileInfo.Length > 0 Then
                    Try
                        My.Computer.Audio.Play(musikPath, AudioPlayMode.BackgroundLoop)
                        Debug.WriteLine("✓ Musik salah diputar (My.Computer.Audio): " & musikPath)
                        isPlaying = True
                    Catch exAudio As Exception
                        Debug.WriteLine("My.Computer.Audio gagal: " & exAudio.Message)
                        ' Fallback: Gunakan SoundPlayer dengan loop manual
                        Try
                            isPlaying = True
                            soundPlayer = New SoundPlayer(musikPath)
                            soundPlayer.Load()

                            ' Buat thread untuk loop musik
                            loopThread = New Thread(AddressOf LoopMusik)
                            loopThread.IsBackground = True
                            loopThread.Start()

                            Debug.WriteLine("✓ Musik salah diputar (SoundPlayer loop): " & musikPath)
                        Catch exSound As Exception
                            Debug.WriteLine("SoundPlayer juga gagal: " & exSound.Message)
                            isPlaying = False
                        End Try
                    End Try
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("Error memutar musik salah: " & ex.Message)
            isPlaying = False
        End Try
    End Sub

End Module
