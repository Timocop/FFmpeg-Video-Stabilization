Imports System.Text.RegularExpressions

Public Class FormMain

    Private g_mProcessingThread As Threading.Thread = Nothing

    Structure STURC_ENCODE_QUALITY
        Dim iCRF As Integer
        Dim sText As String

        Sub New(_Text As String, _CRF As Integer)
            sText = _Text
            iCRF = _CRF
        End Sub

        Public Overrides Function ToString() As String
            Return sText
        End Function

    End Structure

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TrackBar_StabShakiness_Scroll(Nothing, Nothing)
        TrackBar_StabAccuracy_Scroll(Nothing, Nothing)
        TrackBar_StabSmooth_Scroll(Nothing, Nothing)

        ComboBox_EncodeQuality.Items.Clear()
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Low Quality (22)", 22))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Medium Quality (18)", 18))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("High Quality (14)", 14))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Very High Quality (8)", 8))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Lossless (0)", 0))
        ComboBox_EncodeQuality.SelectedIndex = 1
    End Sub

    Private Sub Button_FileOpen_Click(sender As Object, e As EventArgs) Handles Button_FileOpen.Click
        Using mFile As New OpenFileDialog
            If (mFile.ShowDialog() = DialogResult.OK) Then
                TextBox_FileInput.Text = mFile.FileName
                TextBox_FileOutput.Text = IO.Path.Combine(IO.Path.GetDirectoryName(mFile.FileName), IO.Path.GetFileNameWithoutExtension(mFile.FileName) & "_stab.mp4")
            End If
        End Using
    End Sub

    Private Sub Button_FileSave_Click(sender As Object, e As EventArgs) Handles Button_FileSave.Click
        Using mFile As New OpenFileDialog
            If (mFile.ShowDialog() = DialogResult.OK) Then
                TextBox_FileOutput.Text = mFile.FileName
            End If
        End Using
    End Sub

    Private Sub Button_FileProcess_Click(sender As Object, e As EventArgs) Handles Button_FileProcess.Click
        If (g_mProcessingThread IsNot Nothing AndAlso g_mProcessingThread.IsAlive) Then
            g_mProcessingThread.Abort()
            g_mProcessingThread.Join()
            g_mProcessingThread = Nothing

            SetProgress("Done.")
            Button_FileProcess.Text = "Process"
            ProgressBar_Progress.Value = 100
            Return
        End If

        g_mProcessingThread = New Threading.Thread(AddressOf ThreadProcess)
        g_mProcessingThread.IsBackground = True
        g_mProcessingThread.Start()
    End Sub

    Private Sub TrackBar_StabShakiness_Scroll(sender As Object, e As EventArgs) Handles TrackBar_StabShakiness.Scroll
        Label_Shakeiness.Text = "Shakiness (" & TrackBar_StabShakiness.Value & ")"
    End Sub

    Private Sub TrackBar_StabAccuracy_Scroll(sender As Object, e As EventArgs) Handles TrackBar_StabAccuracy.Scroll
        Label_Accuracy.Text = "Accuracy (" & TrackBar_StabAccuracy.Value & ")"
    End Sub

    Private Sub TrackBar_StabSmooth_Scroll(sender As Object, e As EventArgs) Handles TrackBar_StabSmooth.Scroll
        Label_Smoothing.Text = "Smoothing (" & TrackBar_StabSmooth.Value & ")"
    End Sub

    Private Sub SetProgress(sText As String)
        Me.Text = String.Format("FFmpeg Video Stabilization - {0}", sText)
    End Sub

    Private Sub ParseFrameProgress(sData As String, iProcessedFrames As Long, iMaxProcessedFrames As Long)
        If (iMaxProcessedFrames < 1) Then
            Return
        End If

        Dim mMatch = Regex.Match(sData, "^frame=\s*(?<Frames>\d+)", RegexOptions.Multiline)
        If (mMatch.Success) Then
            Dim iFrame As Long = Long.Parse(mMatch.Groups("Frames").Value)
            Dim iProgress As Integer = CInt(((iProcessedFrames + iFrame) / iMaxProcessedFrames) * 100)

            If (iProgress < 0) Then
                iProgress = 0
            End If

            If (iProgress > 100) Then
                iProgress = 100
            End If

            Me.BeginInvoke(Sub() ProgressBar_Progress.Value = iProgress)
        End If
    End Sub

    Private Sub ThreadProcess()
        Try
            Me.BeginInvoke(Sub() SetProgress("Starting..."))
            Me.BeginInvoke(Sub() Button_FileProcess.Text = "Abort")

            Dim sInputFile As String = CStr(Me.Invoke(Function() TextBox_FileInput.Text))
            Dim sOutputFile As String = CStr(Me.Invoke(Function() TextBox_FileOutput.Text))
            Dim sTmpFile As String = IO.Path.Combine(IO.Path.GetDirectoryName(IO.Path.GetTempPath()), Guid.NewGuid.ToString & ".mp4")

            Dim bRemoveDup As Boolean = CBool(Me.Invoke(Function() CheckBox_RemoveDup.Checked))
            Dim bConvertH264 As Boolean = CBool(Me.Invoke(Function() CheckBox_ConvertH264.Checked))
            Dim iShakiness As Integer = CInt(Me.Invoke(Function() TrackBar_StabShakiness.Value))
            Dim iAccuracy As Integer = CInt(Me.Invoke(Function() TrackBar_StabAccuracy.Value))
            Dim iSmoothing As Integer = CInt(Me.Invoke(Function() TrackBar_StabSmooth.Value))
            Dim iQuality As Integer = DirectCast(Me.Invoke(Function() ComboBox_EncodeQuality.SelectedItem), STURC_ENCODE_QUALITY).iCRF

            Dim sFFmpegFile As String = IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")

            Dim iTotalFrames As Long = -1
            Dim iProcessedMaxFrames As Long = 0
            Dim iProcessedCurrentFrames As Long = 0

            Dim mProgressHandler As DataReceivedEventHandler = Sub(sender As Object, e As DataReceivedEventArgs)
                                                                   If (String.IsNullOrEmpty(e.Data)) Then
                                                                       Return
                                                                   End If

                                                                   ParseFrameProgress(e.Data, iProcessedCurrentFrames, iProcessedMaxFrames)
                                                               End Sub

            If (String.IsNullOrEmpty(sInputFile) OrElse sInputFile.Trim.Length = 0) Then
                Throw New ArgumentException("Invalid input file")
            End If

            If (String.IsNullOrEmpty(sOutputFile) OrElse sOutputFile.Trim.Length = 0) Then
                Throw New ArgumentException("Invalid output file")
            End If

            If (Not IO.File.Exists(sInputFile)) Then
                Throw New ArgumentException("Input file does not exist")
            End If

            IO.File.Copy(sInputFile, sOutputFile, True)

            If (True) Then
                Me.BeginInvoke(Sub() SetProgress("Reading input file..."))

                Using mProcess As New Process
                    Dim sArguments As String = String.Format("-i ""{0}"" -map 0:v:0 -c copy -f null -", sOutputFile)

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = sArguments
                        mProcess.StartInfo.WorkingDirectory = Application.StartupPath

                        mProcess.StartInfo.UseShellExecute = False
                        mProcess.StartInfo.CreateNoWindow = True
                        mProcess.StartInfo.RedirectStandardError = True

                        mProcess.Start()

                        Dim sOutput As String = mProcess.StandardError.ReadToEnd()
                        Dim mMatches = Regex.Match(sOutput, "^frame=\s*(?<TotalFrames>\d+)", RegexOptions.Multiline)
                        If (mMatches.Success AndAlso mMatches.Groups("TotalFrames").Success) Then
                            iTotalFrames = CInt(mMatches.Groups("TotalFrames").Value)

                            If (bRemoveDup) Then
                                iProcessedMaxFrames += iTotalFrames
                            Else
                                If (bConvertH264) Then
                                    iProcessedMaxFrames += iTotalFrames
                                End If
                            End If

                            iProcessedMaxFrames += iTotalFrames * 2
                        End If

                        'mProcess.WaitForExit dead-lock because BeginErrorReadLine()
                        While (Not mProcess.HasExited)
                            Threading.Thread.Sleep(10)
                        End While

                        If (mProcess.ExitCode <> 0) Then
                            Throw New ArgumentException("FFmpeg failed with exit code " & mProcess.ExitCode)
                        End If
                    Finally
                        Try
                            mProcess.Kill()
                        Catch ex As Exception
                        End Try
                    End Try
                End Using
            End If

            If (bRemoveDup) Then
                ' ### mpdecimate ###
                Me.BeginInvoke(Sub() SetProgress("Removing duplicated frames..."))

                Using mProcess As New Process
                    Dim sArguments As String = String.Format("-i ""{0}"" -vf mpdecimate -c:v libx264 -preset medium -crf {2} -c:a copy ""{1}""", sOutputFile, sTmpFile, iQuality)

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = sArguments
                        mProcess.StartInfo.WorkingDirectory = Application.StartupPath

                        mProcess.StartInfo.UseShellExecute = False
                        mProcess.StartInfo.CreateNoWindow = True
                        mProcess.StartInfo.RedirectStandardError = True

                        AddHandler mProcess.ErrorDataReceived, mProgressHandler

                        mProcess.Start()
                        mProcess.BeginErrorReadLine()

                        'mProcess.WaitForExit dead-lock because BeginErrorReadLine()
                        While (Not mProcess.HasExited)
                            Threading.Thread.Sleep(10)
                        End While

                        If (mProcess.ExitCode <> 0) Then
                            Throw New ArgumentException("FFmpeg failed with exit code " & mProcess.ExitCode)
                        End If
                    Finally
                        RemoveHandler mProcess.ErrorDataReceived, mProgressHandler
                        iProcessedCurrentFrames += iTotalFrames

                        Try
                            mProcess.Kill()
                        Catch ex As Exception
                        End Try
                    End Try
                End Using

                IO.File.Copy(sTmpFile, sOutputFile, True)
                IO.File.Delete(sTmpFile)
            Else
                If (bConvertH264) Then
                    ' ### convert ###
                    Me.BeginInvoke(Sub() SetProgress("Converting video..."))

                    Using mProcess As New Process
                        Dim sArguments As String = String.Format("-i ""{0}"" -c:v libx264 -preset medium -crf {2} -c:a copy ""{1}""", sOutputFile, sTmpFile, iQuality)

                        Try
                            mProcess.StartInfo.FileName = sFFmpegFile
                            mProcess.StartInfo.Arguments = sArguments
                            mProcess.StartInfo.WorkingDirectory = Application.StartupPath

                            mProcess.StartInfo.UseShellExecute = False
                            mProcess.StartInfo.CreateNoWindow = True
                            mProcess.StartInfo.RedirectStandardError = True

                            AddHandler mProcess.ErrorDataReceived, mProgressHandler

                            mProcess.Start()
                            mProcess.BeginErrorReadLine()

                            'mProcess.WaitForExit dead-lock because BeginErrorReadLine()
                            While (Not mProcess.HasExited)
                                Threading.Thread.Sleep(10)
                            End While

                            If (mProcess.ExitCode <> 0) Then
                                Throw New ArgumentException("FFmpeg failed with exit code " & mProcess.ExitCode)
                            End If
                        Finally
                            RemoveHandler mProcess.ErrorDataReceived, mProgressHandler
                            iProcessedCurrentFrames += iTotalFrames

                            Try
                                mProcess.Kill()
                            Catch ex As Exception
                            End Try
                        End Try
                    End Using

                    IO.File.Copy(sTmpFile, sOutputFile, True)
                    IO.File.Delete(sTmpFile)
                End If
            End If

            If (True) Then
                ' ### vidstabdetect ###
                Me.BeginInvoke(Sub() SetProgress("Stabilization analysis..."))

                Using mProcess As New Process
                    Dim sArguments As String = String.Format("-i ""{0}"" -vf ""vidstabdetect=accuracy={1}:shakiness={2}:result='transforms.trf'"" -f null -", sOutputFile, iAccuracy, iShakiness)

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = sArguments
                        mProcess.StartInfo.WorkingDirectory = Application.StartupPath

                        mProcess.StartInfo.UseShellExecute = False
                        mProcess.StartInfo.CreateNoWindow = True
                        mProcess.StartInfo.RedirectStandardError = True

                        AddHandler mProcess.ErrorDataReceived, mProgressHandler

                        mProcess.Start()
                        mProcess.BeginErrorReadLine()

                        'mProcess.WaitForExit dead-lock because BeginErrorReadLine()
                        While (Not mProcess.HasExited)
                            Threading.Thread.Sleep(10)
                        End While

                        If (mProcess.ExitCode <> 0) Then
                            Throw New ArgumentException("FFmpeg failed with exit code " & mProcess.ExitCode)
                        End If
                    Finally
                        RemoveHandler mProcess.ErrorDataReceived, mProgressHandler
                        iProcessedCurrentFrames += iTotalFrames

                        Try
                            mProcess.Kill()
                        Catch ex As Exception
                        End Try
                    End Try
                End Using

                Me.BeginInvoke(Sub() SetProgress("Stabilization process..."))

                Using mProcess As New Process
                    Dim sArguments As String = String.Format("-i ""{0}"" -vf ""vidstabtransform=input='transforms.trf':smoothing={3}"", -c:v libx264 -preset medium -crf {2} -c:a copy ""{1}""", sOutputFile, sTmpFile, iQuality, iSmoothing)

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = sArguments
                        mProcess.StartInfo.WorkingDirectory = Application.StartupPath

                        mProcess.StartInfo.UseShellExecute = False
                        mProcess.StartInfo.CreateNoWindow = True
                        mProcess.StartInfo.RedirectStandardError = True

                        AddHandler mProcess.ErrorDataReceived, mProgressHandler

                        mProcess.Start()
                        mProcess.BeginErrorReadLine()

                        'mProcess.WaitForExit dead-lock because BeginErrorReadLine()
                        While (Not mProcess.HasExited)
                            Threading.Thread.Sleep(10)
                        End While

                        If (mProcess.ExitCode <> 0) Then
                            Throw New ArgumentException("FFmpeg failed with exit code " & mProcess.ExitCode)
                        End If
                    Finally
                        RemoveHandler mProcess.ErrorDataReceived, mProgressHandler
                        iProcessedCurrentFrames += iTotalFrames

                        Try
                            mProcess.Kill()
                        Catch ex As Exception
                        End Try
                    End Try
                End Using

                IO.File.Copy(sTmpFile, sOutputFile, True)
                IO.File.Delete(sTmpFile)
            End If

            Me.BeginInvoke(Sub() SetProgress("Done."))
            Me.BeginInvoke(Sub() Button_FileProcess.Text = "Process")
            Me.BeginInvoke(Sub() ProgressBar_Progress.Value = 100)

        Catch ex As Threading.ThreadAbortException
            Throw

        Catch ex As Exception
            Me.BeginInvoke(Sub() SetProgress("Error."))
            Me.BeginInvoke(Sub() Button_FileProcess.Text = "Process")
            Me.BeginInvoke(Sub() ProgressBar_Progress.Value = 100)
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If (g_mProcessingThread IsNot Nothing AndAlso g_mProcessingThread.IsAlive) Then
            g_mProcessingThread.Abort()
            g_mProcessingThread.Join()
            g_mProcessingThread = Nothing
        End If
    End Sub
End Class
