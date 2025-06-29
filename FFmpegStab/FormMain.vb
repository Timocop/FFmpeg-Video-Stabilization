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

    Structure STURC_OPTIMAL_ZOOM
        Enum ENUM_ZOOM_TYPE
            NONE
            DYNAMIC
            OPTIMAL
        End Enum

        Dim iZoom As ENUM_ZOOM_TYPE
        Dim sText As String

        Sub New(_Text As String, _Zoom As ENUM_ZOOM_TYPE)
            sText = _Text
            iZoom = _Zoom
        End Sub

        Public Overrides Function ToString() As String
            Return sText
        End Function

    End Structure

    Structure STURC_HARDWARE_ACCELERATION
        Dim sEncoder As String
        Dim sText As String

        Sub New(_Text As String, _Encoder As String)
            sText = _Text
            sEncoder = _Encoder
        End Sub

        Public Overrides Function ToString() As String
            Return sText
        End Function

    End Structure

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TrackBar_StabShakiness_Scroll(Nothing, Nothing)
        TrackBar_StabAccuracy_Scroll(Nothing, Nothing)
        TrackBar_StabSmooth_Scroll(Nothing, Nothing)
        TrackBar_StabZoom_Scroll(Nothing, Nothing)

        ComboBox_EncodeQuality.Items.Clear()
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Very Low Quality (28)", 28))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Low Quality (22)", 22))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Medium Quality (18)", 18))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("High Quality (14)", 14))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Very High Quality (8)", 8))
        ComboBox_EncodeQuality.Items.Add(New STURC_ENCODE_QUALITY("Lossless (0)", 0))
        ComboBox_EncodeQuality.SelectedIndex = 2

        ComboBox_StabOptZoom.Items.Clear()
        ComboBox_StabOptZoom.Items.Add(New STURC_OPTIMAL_ZOOM("Static", STURC_OPTIMAL_ZOOM.ENUM_ZOOM_TYPE.NONE))
        ComboBox_StabOptZoom.Items.Add(New STURC_OPTIMAL_ZOOM("Dynamic zoom", STURC_OPTIMAL_ZOOM.ENUM_ZOOM_TYPE.DYNAMIC))
        ComboBox_StabOptZoom.Items.Add(New STURC_OPTIMAL_ZOOM("Automatic zoom", STURC_OPTIMAL_ZOOM.ENUM_ZOOM_TYPE.OPTIMAL))
        ComboBox_StabOptZoom.SelectedIndex = 1

        ComboBox_HwAcceleration.Items.Clear()
        ComboBox_HwAcceleration.Items.Add(New STURC_HARDWARE_ACCELERATION("None (Best quality)", "libx264"))
        ComboBox_HwAcceleration.Items.Add(New STURC_HARDWARE_ACCELERATION("NVIDIA NVENC", "h264_nvenc"))
        ComboBox_HwAcceleration.Items.Add(New STURC_HARDWARE_ACCELERATION("Intel QSV", "h264_qsv"))
        ComboBox_HwAcceleration.Items.Add(New STURC_HARDWARE_ACCELERATION("AMD AMF", "h264_amf"))
        ComboBox_HwAcceleration.SelectedIndex = 0
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

    Private Sub TrackBar_StabZoom_Scroll(sender As Object, e As EventArgs) Handles TrackBar_StabZoom.Scroll
        Label_StabZoom.Text = "Zoom (" & TrackBar_StabZoom.Value & "%)"
    End Sub

    Private Sub SetProgress(sText As String)
        Me.Text = String.Format("FFmpeg Video Stabilization - {0}", sText)
    End Sub

    Private Sub ParseFrameProgress(sData As String, iProcessedFrames As Long, iMaxProcessedFrames As Long)
        If (iMaxProcessedFrames < 1) Then
            Return
        End If

        'Fix newlines
        sData = String.Join(Environment.NewLine, sData.Split(New String() {vbCrLf, vbCr, vbLf}, StringSplitOptions.None))

        Dim mMatches = Regex.Matches(sData, "^frame=\s*(?<Frames>\d+)", RegexOptions.Multiline)
        If (mMatches.Count > 0) Then
            Dim mLastMatch = mMatches(mMatches.Count - 1)

            If (mLastMatch.Success) Then
                Dim iFrame As Long = Long.Parse(mLastMatch.Groups("Frames").Value)
                Dim iProgress As Integer = CInt(((iProcessedFrames + iFrame) / iMaxProcessedFrames) * 100)

                If (iProgress < 0) Then
                    iProgress = 0
                End If

                If (iProgress > 100) Then
                    iProgress = 100
                End If

                Me.BeginInvoke(Sub() ProgressBar_Progress.Value = iProgress)
            End If
        End If
    End Sub

    Private Sub ThreadProcess()
        Try
            Me.BeginInvoke(Sub() SetProgress("Starting..."))
            Me.BeginInvoke(Sub() Button_FileProcess.Text = "Abort")
            Me.BeginInvoke(Sub() ProgressBar_Progress.Value = 0)

            Dim sInputFile As String = CStr(Me.Invoke(Function() TextBox_FileInput.Text))
            Dim sOutputFile As String = CStr(Me.Invoke(Function() TextBox_FileOutput.Text))
            Dim sTmpFile As String = IO.Path.Combine(IO.Path.GetDirectoryName(IO.Path.GetTempPath()), Guid.NewGuid.ToString & ".mp4")

            Dim bRemoveDup As Boolean = CBool(Me.Invoke(Function() CheckBox_RemoveDup.Checked))
            Dim bConvertH264 As Boolean = CBool(Me.Invoke(Function() CheckBox_ConvertH264.Checked))
            Dim iShakiness As Integer = CInt(Me.Invoke(Function() TrackBar_StabShakiness.Value))
            Dim iAccuracy As Integer = CInt(Me.Invoke(Function() TrackBar_StabAccuracy.Value))

            Dim iSmoothing As Integer = CInt(Me.Invoke(Function() TrackBar_StabSmooth.Value))
            Dim iZoom As Integer = CInt(Me.Invoke(Function() TrackBar_StabZoom.Value))
            Dim iZoomMethod As Integer = DirectCast(Me.Invoke(Function() ComboBox_StabOptZoom.SelectedItem), STURC_OPTIMAL_ZOOM).iZoom
            Dim iZoomSpeed As Single = CSng(Me.Invoke(Function() NumericUpDown_StabOptZoomSpeed.Value))


            Dim sEncoder As String = DirectCast(Me.Invoke(Function() ComboBox_HwAcceleration.SelectedItem), STURC_HARDWARE_ACCELERATION).sEncoder
            Dim iQuality As Integer = DirectCast(Me.Invoke(Function() ComboBox_EncodeQuality.SelectedItem), STURC_ENCODE_QUALITY).iCRF
            Dim bPreviewMode As Boolean = CBool(Me.Invoke(Function() CheckBox_PreviewMode.Checked))

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

            If (True) Then
                Me.BeginInvoke(Sub() SetProgress("Reading input file..."))

                Using mProcess As New Process
                    Dim sArguments As New List(Of String)
                    sArguments.Add(String.Format("-i ""{0}""", sInputFile))
                    sArguments.Add(String.Format("-map 0:v:0"))
                    sArguments.Add(String.Format("-c copy"))
                    sArguments.Add(String.Format("-f null -"))

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = String.Join(" ", sArguments)
                        mProcess.StartInfo.WorkingDirectory = IO.Path.GetTempPath()

                        mProcess.StartInfo.UseShellExecute = False
                        mProcess.StartInfo.CreateNoWindow = True
                        mProcess.StartInfo.RedirectStandardError = True

                        mProcess.Start()

                        Dim sOutput As String = mProcess.StandardError.ReadToEnd()

                        'Fix newlines
                        sOutput = String.Join(Environment.NewLine, sOutput.Split(New String() {vbCrLf, vbCr, vbLf}, StringSplitOptions.None))

                        Dim mMatches = Regex.Matches(sOutput, "^frame=\s*(?<TotalFrames>\d+)", RegexOptions.Multiline)

                        If (mMatches.Count > 0) Then
                            Dim mLastMatch = mMatches(mMatches.Count - 1)
                            If (mLastMatch.Success AndAlso mLastMatch.Groups("TotalFrames").Success) Then
                                iTotalFrames = CInt(mLastMatch.Groups("TotalFrames").Value)

                                If (bRemoveDup) Then
                                    iProcessedMaxFrames += iTotalFrames
                                Else
                                    If (bConvertH264) Then
                                        iProcessedMaxFrames += iTotalFrames
                                    End If
                                End If

                                iProcessedMaxFrames += iTotalFrames * 2
                            End If
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
                    Dim sArguments As New List(Of String)
                    sArguments.Add(String.Format("-i ""{0}""", sInputFile))
                    sArguments.Add(String.Format("-vf ""scale={0},mpdecimate""", If(bPreviewMode, "iw/3:ih/3", "-1:-1")))
                    sArguments.Add(String.Format("-c:v {0}", sEncoder))

                    Select Case (sEncoder)
                        Case "libx264"
                            sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "ultrafast", "slow")))
                            sArguments.Add(String.Format("-crf {0}", iQuality))
                        Case "h264_nvenc"
                            sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "fast", "medium")))
                            sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                            sArguments.Add(String.Format("-rc:v constqp -cq {0}", iQuality))
                        Case "h264_qsv"
                            sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "faster", "fast")))
                            sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                            sArguments.Add(String.Format("-global_quality {0}", iQuality))
                        Case "h264_amf"
                            sArguments.Add(String.Format("-quality {0}", If(bPreviewMode, "speed", "balanced")))
                            sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                            sArguments.Add(String.Format("-rc cqp -qp_i {0} -qp_p {0}", iQuality))
                        Case Else
                            Throw New ArgumentException("Unknown encoder")
                    End Select

                    sArguments.Add(String.Format("-c:a copy", sEncoder))
                    sArguments.Add(String.Format("""{0}""", sTmpFile))

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = String.Join(" ", sArguments)
                        mProcess.StartInfo.WorkingDirectory = IO.Path.GetTempPath()

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

                IO.File.Delete(sOutputFile)
                IO.File.Move(sTmpFile, sOutputFile)
            Else
                If (bConvertH264) Then
                    ' ### convert ###
                    Me.BeginInvoke(Sub() SetProgress("Converting video..."))

                    Using mProcess As New Process
                        Dim sArguments As New List(Of String)
                        sArguments.Add(String.Format("-i ""{0}""", sInputFile))
                        sArguments.Add(String.Format("-vf ""scale={0}""", If(bPreviewMode, "iw/3:ih/3", "-1:-1")))
                        sArguments.Add(String.Format("-c:v {0}", sEncoder))

                        Select Case (sEncoder)
                            Case "libx264"
                                sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "ultrafast", "slow")))
                                sArguments.Add(String.Format("-crf {0}", iQuality))
                            Case "h264_nvenc"
                                sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "fast", "medium")))
                                sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                                sArguments.Add(String.Format("-rc:v constqp -cq {0}", iQuality))
                            Case "h264_qsv"
                                sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "faster", "fast")))
                                sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                                sArguments.Add(String.Format("-global_quality {0}", iQuality))
                            Case "h264_amf"
                                sArguments.Add(String.Format("-quality {0}", If(bPreviewMode, "speed", "balanced")))
                                sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                                sArguments.Add(String.Format("-rc cqp -qp_i {0} -qp_p {0}", iQuality))
                            Case Else
                                Throw New ArgumentException("Unknown encoder")
                        End Select

                        sArguments.Add(String.Format("-c:a copy", sEncoder))
                        sArguments.Add(String.Format("""{0}""", sTmpFile))

                        Try
                            mProcess.StartInfo.FileName = sFFmpegFile
                            mProcess.StartInfo.Arguments = String.Join(" ", sArguments)
                            mProcess.StartInfo.WorkingDirectory = IO.Path.GetTempPath()

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

                    IO.File.Delete(sOutputFile)
                    IO.File.Move(sTmpFile, sOutputFile)
                Else
                    IO.File.Copy(sInputFile, sOutputFile, True)
                End If
            End If

            If (True) Then
                ' ### vidstabdetect ###
                Me.BeginInvoke(Sub() SetProgress("Stabilization analysis..."))

                Dim sTransformFile As String = Guid.NewGuid.ToString & ".trf"

                Using mProcess As New Process
                    Dim sArguments As New List(Of String)
                    sArguments.Add(String.Format("-i ""{0}""", sOutputFile))
                    sArguments.Add(String.Format("-vf ""vidstabdetect=accuracy={0}:shakiness={1}:result='{2}'""", iAccuracy, iShakiness, sTransformFile))
                    sArguments.Add(String.Format("-f null -"))

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = String.Join(" ", sArguments)
                        mProcess.StartInfo.WorkingDirectory = IO.Path.GetTempPath()

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
                    Dim sArguments As New List(Of String)
                    sArguments.Add(String.Format("-i ""{0}""", sOutputFile))
                    sArguments.Add(String.Format("-vf ""vidstabtransform=input='{0}':smoothing={1}:zoom={2}:optzoom={3}:zoomspeed={4}""", sTransformFile, iSmoothing, iZoom, iZoomMethod, iZoomSpeed.ToString(Globalization.CultureInfo.InvariantCulture)))
                    sArguments.Add(String.Format("-c:v {0}", sEncoder))

                    Select Case (sEncoder)
                        Case "libx264"
                            sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "ultrafast", "slow")))
                            sArguments.Add(String.Format("-crf {0}", iQuality))
                        Case "h264_nvenc"
                            sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "fast", "medium")))
                            sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                            sArguments.Add(String.Format("-rc:v constqp -cq {0}", iQuality))
                        Case "h264_qsv"
                            sArguments.Add(String.Format("-preset {0}", If(bPreviewMode, "faster", "fast")))
                            sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                            sArguments.Add(String.Format("-global_quality {0}", iQuality))
                        Case "h264_amf"
                            sArguments.Add(String.Format("-quality {0}", If(bPreviewMode, "speed", "balanced")))
                            sArguments.Add(String.Format("-profile:v {0}", If(bPreviewMode, "main", "high")))
                            sArguments.Add(String.Format("-rc cqp -qp_i {0} -qp_p {0}", iQuality))
                        Case Else
                            Throw New ArgumentException("Unknown encoder")
                    End Select

                    sArguments.Add(String.Format("-c:a copy", sEncoder))
                    sArguments.Add(String.Format("""{0}""", sTmpFile))

                    Try
                        mProcess.StartInfo.FileName = sFFmpegFile
                        mProcess.StartInfo.Arguments = String.Join(" ", sArguments)
                        mProcess.StartInfo.WorkingDirectory = IO.Path.GetTempPath()

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

                IO.File.Delete(sOutputFile)
                IO.File.Move(sTmpFile, sOutputFile)
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
