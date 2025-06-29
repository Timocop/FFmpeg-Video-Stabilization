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

    Private Sub ThreadProcess()
        Try
            Me.Invoke(Sub() SetProgress("Starting..."))

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

            IO.File.Copy(sInputFile, sOutputFile, True)

            If (bRemoveDup) Then
                ' ### mpdecimate ###
                Me.Invoke(Sub() SetProgress("Removing duplicated frames..."))

                Dim sArguments As String = String.Format("-i ""{0}"" -vf mpdecimate -c:v libx264 -preset medium -crf {2} -c:a copy ""{1}""", sOutputFile, sTmpFile, iQuality)
                ExecuteProgram(sFFmpegFile, sArguments, Application.StartupPath, 0)

                IO.File.Copy(sTmpFile, sOutputFile, True)
                IO.File.Delete(sTmpFile)
            Else
                If (bConvertH264) Then
                    ' ### mpdecimate ###
                    Me.Invoke(Sub() SetProgress("Converting video..."))

                    Dim sArguments As String = String.Format("-i ""{0}"" -c:v libx264 -preset medium -crf {2} -c:a copy ""{1}""", sOutputFile, sTmpFile, iQuality)
                    ExecuteProgram(sFFmpegFile, sArguments, Application.StartupPath, 0)

                    IO.File.Copy(sTmpFile, sOutputFile, True)
                    IO.File.Delete(sTmpFile)
                End If
            End If

            If (True) Then
                ' ### vidstabdetect ###
                Me.Invoke(Sub() SetProgress("Stabilization analysis..."))

                Dim sArgumentsPre As String = String.Format("-i ""{0}"" -vf ""vidstabdetect=accuracy={1}:shakiness={2}:result='transforms.trf'"" -f null -", sOutputFile, iAccuracy, iShakiness)
                ExecuteProgram(sFFmpegFile, sArgumentsPre, Application.StartupPath, 0)

                Me.Invoke(Sub() SetProgress("Stabilization process..."))

                Dim sArgumentsPost As String = String.Format("-i ""{0}"" -vf ""vidstabtransform=input='transforms.trf':smoothing={3}"", -c:v libx264 -preset medium -crf {2} -c:a copy ""{1}""", sOutputFile, sTmpFile, iQuality, iSmoothing)
                ExecuteProgram(sFFmpegFile, sArgumentsPost, Application.StartupPath, 0)

                IO.File.Copy(sTmpFile, sOutputFile, True)
                IO.File.Delete(sTmpFile)
            End If

            Me.BeginInvoke(Sub() SetProgress("Done."))
            MessageBox.Show("Done.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Threading.ThreadAbortException
            Throw

        Catch ex As Exception
            Me.BeginInvoke(Sub() SetProgress("Error."))
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExecuteProgram(sPath As String, sArguments As String, sWorkingDirectory As String, ByRef r_ExitCode As Integer)
        ExecuteProgram(sPath, sArguments, sWorkingDirectory, Nothing, r_ExitCode)
    End Sub

    Private Sub ExecuteProgram(sPath As String, sArguments As String, sWorkingDirectory As String, mEnvironmentVariables As Dictionary(Of String, String), ByRef r_ExitCode As Integer)
        r_ExitCode = 0

        Using i As New Process
            Try
                i.StartInfo.FileName = sPath
                i.StartInfo.Arguments = sArguments
                i.StartInfo.WorkingDirectory = sWorkingDirectory

                i.StartInfo.UseShellExecute = False
                i.StartInfo.CreateNoWindow = True
                i.StartInfo.RedirectStandardOutput = True

                If (mEnvironmentVariables IsNot Nothing) Then
                    For Each mVar In mEnvironmentVariables
                        i.StartInfo.EnvironmentVariables(mVar.Key) = mVar.Value
                    Next
                End If

                i.Start()
                i.WaitForExit()

                r_ExitCode = i.ExitCode
            Finally
                If (Not i.HasExited) Then
                    i.Kill()
                End If
            End Try
        End Using
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If (g_mProcessingThread IsNot Nothing AndAlso g_mProcessingThread.IsAlive) Then
            g_mProcessingThread.Abort()
            g_mProcessingThread.Join()
            g_mProcessingThread = Nothing
        End If
    End Sub
End Class
