﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button_FileOpen = New System.Windows.Forms.Button()
        Me.TextBox_FileInput = New System.Windows.Forms.TextBox()
        Me.TextBox_FileOutput = New System.Windows.Forms.TextBox()
        Me.Button_FileSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox_RemoveDup = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label_StabZoom = New System.Windows.Forms.Label()
        Me.TrackBar_StabZoom = New System.Windows.Forms.TrackBar()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown_StabOptZoomSpeed = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox_StabOptZoom = New System.Windows.Forms.ComboBox()
        Me.Label_Smoothing = New System.Windows.Forms.Label()
        Me.TrackBar_StabSmooth = New System.Windows.Forms.TrackBar()
        Me.Label_Accuracy = New System.Windows.Forms.Label()
        Me.TrackBar_StabAccuracy = New System.Windows.Forms.TrackBar()
        Me.Label_Shakeiness = New System.Windows.Forms.Label()
        Me.TrackBar_StabShakiness = New System.Windows.Forms.TrackBar()
        Me.Button_FileProcess = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBox_ConvertH264 = New System.Windows.Forms.CheckBox()
        Me.ComboBox_EncodeQuality = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ProgressBar_Progress = New System.Windows.Forms.ProgressBar()
        Me.CheckBox_PreviewMode = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBox_HwAcceleration = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.TrackBar_StabZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_StabOptZoomSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_StabSmooth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_StabAccuracy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_StabShakiness, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button_FileOpen
        '
        Me.Button_FileOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_FileOpen.Location = New System.Drawing.Point(580, 12)
        Me.Button_FileOpen.Name = "Button_FileOpen"
        Me.Button_FileOpen.Size = New System.Drawing.Size(75, 23)
        Me.Button_FileOpen.TabIndex = 0
        Me.Button_FileOpen.Text = "Open"
        Me.Button_FileOpen.UseVisualStyleBackColor = True
        '
        'TextBox_FileInput
        '
        Me.TextBox_FileInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_FileInput.Location = New System.Drawing.Point(63, 12)
        Me.TextBox_FileInput.Name = "TextBox_FileInput"
        Me.TextBox_FileInput.Size = New System.Drawing.Size(511, 22)
        Me.TextBox_FileInput.TabIndex = 1
        '
        'TextBox_FileOutput
        '
        Me.TextBox_FileOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_FileOutput.Location = New System.Drawing.Point(63, 40)
        Me.TextBox_FileOutput.Name = "TextBox_FileOutput"
        Me.TextBox_FileOutput.Size = New System.Drawing.Size(511, 22)
        Me.TextBox_FileOutput.TabIndex = 2
        '
        'Button_FileSave
        '
        Me.Button_FileSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_FileSave.Location = New System.Drawing.Point(580, 41)
        Me.Button_FileSave.Name = "Button_FileSave"
        Me.Button_FileSave.Size = New System.Drawing.Size(75, 23)
        Me.Button_FileSave.TabIndex = 3
        Me.Button_FileSave.Text = "Save"
        Me.Button_FileSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Input"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Output"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CheckBox_RemoveDup)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 68)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(643, 47)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label3.Location = New System.Drawing.Point(6, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(610, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Removes duplicated frames and converts the video to variable framerate. This will" &
    " result in better stabilization quality."
        '
        'CheckBox_RemoveDup
        '
        Me.CheckBox_RemoveDup.AutoSize = True
        Me.CheckBox_RemoveDup.Checked = True
        Me.CheckBox_RemoveDup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_RemoveDup.Location = New System.Drawing.Point(6, 0)
        Me.CheckBox_RemoveDup.Name = "CheckBox_RemoveDup"
        Me.CheckBox_RemoveDup.Size = New System.Drawing.Size(161, 17)
        Me.CheckBox_RemoveDup.TabIndex = 0
        Me.CheckBox_RemoveDup.Text = "Remove duplicated frames"
        Me.CheckBox_RemoveDup.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label_StabZoom)
        Me.GroupBox2.Controls.Add(Me.TrackBar_StabZoom)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown_StabOptZoomSpeed)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.ComboBox_StabOptZoom)
        Me.GroupBox2.Controls.Add(Me.Label_Smoothing)
        Me.GroupBox2.Controls.Add(Me.TrackBar_StabSmooth)
        Me.GroupBox2.Controls.Add(Me.Label_Accuracy)
        Me.GroupBox2.Controls.Add(Me.TrackBar_StabAccuracy)
        Me.GroupBox2.Controls.Add(Me.Label_Shakeiness)
        Me.GroupBox2.Controls.Add(Me.TrackBar_StabShakiness)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 174)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(643, 174)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Stabilization settings"
        '
        'Label_StabZoom
        '
        Me.Label_StabZoom.AutoSize = True
        Me.Label_StabZoom.Location = New System.Drawing.Point(6, 87)
        Me.Label_StabZoom.Name = "Label_StabZoom"
        Me.Label_StabZoom.Size = New System.Drawing.Size(36, 13)
        Me.Label_StabZoom.TabIndex = 11
        Me.Label_StabZoom.Text = "Zoom"
        '
        'TrackBar_StabZoom
        '
        Me.TrackBar_StabZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_StabZoom.AutoSize = False
        Me.TrackBar_StabZoom.LargeChange = 1
        Me.TrackBar_StabZoom.Location = New System.Drawing.Point(109, 87)
        Me.TrackBar_StabZoom.Maximum = 100
        Me.TrackBar_StabZoom.Minimum = -100
        Me.TrackBar_StabZoom.Name = "TrackBar_StabZoom"
        Me.TrackBar_StabZoom.Size = New System.Drawing.Size(528, 16)
        Me.TrackBar_StabZoom.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Auto zoom speed"
        '
        'NumericUpDown_StabOptZoomSpeed
        '
        Me.NumericUpDown_StabOptZoomSpeed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_StabOptZoomSpeed.DecimalPlaces = 2
        Me.NumericUpDown_StabOptZoomSpeed.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.NumericUpDown_StabOptZoomSpeed.Location = New System.Drawing.Point(109, 136)
        Me.NumericUpDown_StabOptZoomSpeed.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown_StabOptZoomSpeed.Name = "NumericUpDown_StabOptZoomSpeed"
        Me.NumericUpDown_StabOptZoomSpeed.Size = New System.Drawing.Size(528, 22)
        Me.NumericUpDown_StabOptZoomSpeed.TabIndex = 8
        Me.NumericUpDown_StabOptZoomSpeed.Value = New Decimal(New Integer() {25, 0, 0, 131072})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Auto zoom"
        '
        'ComboBox_StabOptZoom
        '
        Me.ComboBox_StabOptZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_StabOptZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_StabOptZoom.FormattingEnabled = True
        Me.ComboBox_StabOptZoom.Location = New System.Drawing.Point(109, 109)
        Me.ComboBox_StabOptZoom.Name = "ComboBox_StabOptZoom"
        Me.ComboBox_StabOptZoom.Size = New System.Drawing.Size(528, 21)
        Me.ComboBox_StabOptZoom.TabIndex = 6
        '
        'Label_Smoothing
        '
        Me.Label_Smoothing.AutoSize = True
        Me.Label_Smoothing.Location = New System.Drawing.Point(6, 65)
        Me.Label_Smoothing.Name = "Label_Smoothing"
        Me.Label_Smoothing.Size = New System.Drawing.Size(64, 13)
        Me.Label_Smoothing.TabIndex = 5
        Me.Label_Smoothing.Text = "Smoothing"
        '
        'TrackBar_StabSmooth
        '
        Me.TrackBar_StabSmooth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_StabSmooth.AutoSize = False
        Me.TrackBar_StabSmooth.LargeChange = 1
        Me.TrackBar_StabSmooth.Location = New System.Drawing.Point(109, 65)
        Me.TrackBar_StabSmooth.Maximum = 100
        Me.TrackBar_StabSmooth.Name = "TrackBar_StabSmooth"
        Me.TrackBar_StabSmooth.Size = New System.Drawing.Size(528, 16)
        Me.TrackBar_StabSmooth.TabIndex = 4
        Me.TrackBar_StabSmooth.Value = 10
        '
        'Label_Accuracy
        '
        Me.Label_Accuracy.AutoSize = True
        Me.Label_Accuracy.Location = New System.Drawing.Point(6, 43)
        Me.Label_Accuracy.Name = "Label_Accuracy"
        Me.Label_Accuracy.Size = New System.Drawing.Size(51, 13)
        Me.Label_Accuracy.TabIndex = 3
        Me.Label_Accuracy.Text = "Accuracy"
        '
        'TrackBar_StabAccuracy
        '
        Me.TrackBar_StabAccuracy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_StabAccuracy.AutoSize = False
        Me.TrackBar_StabAccuracy.LargeChange = 1
        Me.TrackBar_StabAccuracy.Location = New System.Drawing.Point(109, 43)
        Me.TrackBar_StabAccuracy.Maximum = 15
        Me.TrackBar_StabAccuracy.Minimum = 1
        Me.TrackBar_StabAccuracy.Name = "TrackBar_StabAccuracy"
        Me.TrackBar_StabAccuracy.Size = New System.Drawing.Size(528, 16)
        Me.TrackBar_StabAccuracy.TabIndex = 2
        Me.TrackBar_StabAccuracy.Value = 15
        '
        'Label_Shakeiness
        '
        Me.Label_Shakeiness.AutoSize = True
        Me.Label_Shakeiness.Location = New System.Drawing.Point(6, 21)
        Me.Label_Shakeiness.Name = "Label_Shakeiness"
        Me.Label_Shakeiness.Size = New System.Drawing.Size(58, 13)
        Me.Label_Shakeiness.TabIndex = 1
        Me.Label_Shakeiness.Text = "Shakiness"
        '
        'TrackBar_StabShakiness
        '
        Me.TrackBar_StabShakiness.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_StabShakiness.AutoSize = False
        Me.TrackBar_StabShakiness.LargeChange = 1
        Me.TrackBar_StabShakiness.Location = New System.Drawing.Point(109, 21)
        Me.TrackBar_StabShakiness.Minimum = 1
        Me.TrackBar_StabShakiness.Name = "TrackBar_StabShakiness"
        Me.TrackBar_StabShakiness.Size = New System.Drawing.Size(528, 16)
        Me.TrackBar_StabShakiness.TabIndex = 0
        Me.TrackBar_StabShakiness.Value = 5
        '
        'Button_FileProcess
        '
        Me.Button_FileProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_FileProcess.Location = New System.Drawing.Point(580, 408)
        Me.Button_FileProcess.Name = "Button_FileProcess"
        Me.Button_FileProcess.Size = New System.Drawing.Size(75, 23)
        Me.Button_FileProcess.TabIndex = 8
        Me.Button_FileProcess.Text = "Process"
        Me.Button_FileProcess.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.CheckBox_ConvertH264)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 121)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(643, 47)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label4.Location = New System.Drawing.Point(6, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(420, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Encodes the input video to H.264 first before doing stabilization to avoid issues" &
    "."
        '
        'CheckBox_ConvertH264
        '
        Me.CheckBox_ConvertH264.AutoSize = True
        Me.CheckBox_ConvertH264.Checked = True
        Me.CheckBox_ConvertH264.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_ConvertH264.Location = New System.Drawing.Point(6, 0)
        Me.CheckBox_ConvertH264.Name = "CheckBox_ConvertH264"
        Me.CheckBox_ConvertH264.Size = New System.Drawing.Size(112, 17)
        Me.CheckBox_ConvertH264.TabIndex = 0
        Me.CheckBox_ConvertH264.Text = "Convert to H.264"
        Me.CheckBox_ConvertH264.UseVisualStyleBackColor = True
        '
        'ComboBox_EncodeQuality
        '
        Me.ComboBox_EncodeQuality.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_EncodeQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EncodeQuality.FormattingEnabled = True
        Me.ComboBox_EncodeQuality.Location = New System.Drawing.Point(140, 381)
        Me.ComboBox_EncodeQuality.Name = "ComboBox_EncodeQuality"
        Me.ComboBox_EncodeQuality.Size = New System.Drawing.Size(160, 21)
        Me.ComboBox_EncodeQuality.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 384)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Encode Quality"
        '
        'ProgressBar_Progress
        '
        Me.ProgressBar_Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar_Progress.Location = New System.Drawing.Point(12, 408)
        Me.ProgressBar_Progress.Name = "ProgressBar_Progress"
        Me.ProgressBar_Progress.Size = New System.Drawing.Size(562, 23)
        Me.ProgressBar_Progress.TabIndex = 12
        '
        'CheckBox_PreviewMode
        '
        Me.CheckBox_PreviewMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_PreviewMode.AutoSize = True
        Me.CheckBox_PreviewMode.Location = New System.Drawing.Point(306, 384)
        Me.CheckBox_PreviewMode.Name = "CheckBox_PreviewMode"
        Me.CheckBox_PreviewMode.Size = New System.Drawing.Size(98, 17)
        Me.CheckBox_PreviewMode.TabIndex = 13
        Me.CheckBox_PreviewMode.Text = "Preview Mode"
        Me.ToolTip1.SetToolTip(Me.CheckBox_PreviewMode, "Speeds up the encoding by reducing the video resolution and faster compression")
        Me.CheckBox_PreviewMode.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 358)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Hardware acceleration"
        '
        'ComboBox_HwAcceleration
        '
        Me.ComboBox_HwAcceleration.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_HwAcceleration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HwAcceleration.FormattingEnabled = True
        Me.ComboBox_HwAcceleration.Location = New System.Drawing.Point(140, 354)
        Me.ComboBox_HwAcceleration.Name = "ComboBox_HwAcceleration"
        Me.ComboBox_HwAcceleration.Size = New System.Drawing.Size(160, 21)
        Me.ComboBox_HwAcceleration.TabIndex = 15
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(667, 443)
        Me.Controls.Add(Me.ComboBox_HwAcceleration)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CheckBox_PreviewMode)
        Me.Controls.Add(Me.ProgressBar_Progress)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBox_EncodeQuality)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button_FileProcess)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button_FileSave)
        Me.Controls.Add(Me.TextBox_FileOutput)
        Me.Controls.Add(Me.TextBox_FileInput)
        Me.Controls.Add(Me.Button_FileOpen)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FFmpeg Video Stabilization"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.TrackBar_StabZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_StabOptZoomSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_StabSmooth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_StabAccuracy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_StabShakiness, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_FileOpen As Button
    Friend WithEvents TextBox_FileInput As TextBox
    Friend WithEvents TextBox_FileOutput As TextBox
    Friend WithEvents Button_FileSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox_RemoveDup As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label_Accuracy As Label
    Friend WithEvents TrackBar_StabAccuracy As TrackBar
    Friend WithEvents Label_Shakeiness As Label
    Friend WithEvents TrackBar_StabShakiness As TrackBar
    Friend WithEvents Button_FileProcess As Button
    Friend WithEvents Label_Smoothing As Label
    Friend WithEvents TrackBar_StabSmooth As TrackBar
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBox_ConvertH264 As CheckBox
    Friend WithEvents ComboBox_EncodeQuality As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ProgressBar_Progress As ProgressBar
    Friend WithEvents CheckBox_PreviewMode As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox_StabOptZoom As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents NumericUpDown_StabOptZoomSpeed As NumericUpDown
    Friend WithEvents Label_StabZoom As Label
    Friend WithEvents TrackBar_StabZoom As TrackBar
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboBox_HwAcceleration As ComboBox
End Class
