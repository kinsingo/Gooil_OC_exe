<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PMEngineExampleForm
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
        Me.PMEngineCheckBox = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.MeasSetupComboBox = New System.Windows.Forms.ComboBox
        Me.AdjustExposureButton = New System.Windows.Forms.Button
        Me.TakeMeasurementButton = New System.Windows.Forms.Button
        Me.XLocTextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.YLocTextBox = New System.Windows.Forms.TextBox
        Me.GetValueButton = New System.Windows.Forms.Button
        Me.ValueTextBox = New System.Windows.Forms.TextBox
        Me.DetSizeTextBox = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PMEngineCheckBox
        '
        Me.PMEngineCheckBox.Appearance = System.Windows.Forms.Appearance.Button
        Me.PMEngineCheckBox.AutoSize = True
        Me.PMEngineCheckBox.Location = New System.Drawing.Point(12, 12)
        Me.PMEngineCheckBox.Name = "PMEngineCheckBox"
        Me.PMEngineCheckBox.Size = New System.Drawing.Size(91, 23)
        Me.PMEngineCheckBox.TabIndex = 0
        Me.PMEngineCheckBox.Text = "Start PMEngine"
        Me.PMEngineCheckBox.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Measurement Setups:"
        '
        'MeasSetupComboBox
        '
        Me.MeasSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MeasSetupComboBox.Enabled = False
        Me.MeasSetupComboBox.FormattingEnabled = True
        Me.MeasSetupComboBox.Location = New System.Drawing.Point(130, 42)
        Me.MeasSetupComboBox.Name = "MeasSetupComboBox"
        Me.MeasSetupComboBox.Size = New System.Drawing.Size(253, 21)
        Me.MeasSetupComboBox.TabIndex = 2
        '
        'AdjustExposureButton
        '
        Me.AdjustExposureButton.Enabled = False
        Me.AdjustExposureButton.Location = New System.Drawing.Point(12, 69)
        Me.AdjustExposureButton.Name = "AdjustExposureButton"
        Me.AdjustExposureButton.Size = New System.Drawing.Size(163, 23)
        Me.AdjustExposureButton.TabIndex = 3
        Me.AdjustExposureButton.Text = "Show Adjust Exposure Form"
        Me.AdjustExposureButton.UseVisualStyleBackColor = True
        '
        'TakeMeasurementButton
        '
        Me.TakeMeasurementButton.Enabled = False
        Me.TakeMeasurementButton.Location = New System.Drawing.Point(13, 99)
        Me.TakeMeasurementButton.Name = "TakeMeasurementButton"
        Me.TakeMeasurementButton.Size = New System.Drawing.Size(162, 23)
        Me.TakeMeasurementButton.TabIndex = 4
        Me.TakeMeasurementButton.Text = "Take Measurement"
        Me.TakeMeasurementButton.UseVisualStyleBackColor = True
        '
        'XLocTextBox
        '
        Me.XLocTextBox.Location = New System.Drawing.Point(53, 146)
        Me.XLocTextBox.Name = "XLocTextBox"
        Me.XLocTextBox.Size = New System.Drawing.Size(70, 20)
        Me.XLocTextBox.TabIndex = 7
        Me.XLocTextBox.Text = "0.5"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "X-Loc"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(141, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Y-Loc"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Position (relative location):"
        '
        'YLocTextBox
        '
        Me.YLocTextBox.Location = New System.Drawing.Point(182, 146)
        Me.YLocTextBox.Name = "YLocTextBox"
        Me.YLocTextBox.Size = New System.Drawing.Size(70, 20)
        Me.YLocTextBox.TabIndex = 9
        Me.YLocTextBox.Text = "0.5"
        '
        'GetValueButton
        '
        Me.GetValueButton.Enabled = False
        Me.GetValueButton.Location = New System.Drawing.Point(16, 181)
        Me.GetValueButton.Name = "GetValueButton"
        Me.GetValueButton.Size = New System.Drawing.Size(159, 23)
        Me.GetValueButton.TabIndex = 13
        Me.GetValueButton.Text = "Get Value at Position"
        Me.GetValueButton.UseVisualStyleBackColor = True
        '
        'ValueTextBox
        '
        Me.ValueTextBox.Location = New System.Drawing.Point(53, 210)
        Me.ValueTextBox.Name = "ValueTextBox"
        Me.ValueTextBox.ReadOnly = True
        Me.ValueTextBox.Size = New System.Drawing.Size(100, 20)
        Me.ValueTextBox.TabIndex = 15
        '
        'DetSizeTextBox
        '
        Me.DetSizeTextBox.Location = New System.Drawing.Point(382, 146)
        Me.DetSizeTextBox.Name = "DetSizeTextBox"
        Me.DetSizeTextBox.Size = New System.Drawing.Size(70, 20)
        Me.DetSizeTextBox.TabIndex = 11
        Me.DetSizeTextBox.Text = "25"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(302, 149)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Detector Size:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(461, 149)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "mm"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 213)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(22, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Lv:"
        '
        'PMEngineExampleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 273)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DetSizeTextBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ValueTextBox)
        Me.Controls.Add(Me.GetValueButton)
        Me.Controls.Add(Me.YLocTextBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.XLocTextBox)
        Me.Controls.Add(Me.TakeMeasurementButton)
        Me.Controls.Add(Me.AdjustExposureButton)
        Me.Controls.Add(Me.MeasSetupComboBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PMEngineCheckBox)
        Me.Name = "PMEngineExampleForm"
        Me.Text = "PMEngine Example"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PMEngineCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MeasSetupComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AdjustExposureButton As System.Windows.Forms.Button
    Friend WithEvents TakeMeasurementButton As System.Windows.Forms.Button
    Friend WithEvents XLocTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents YLocTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GetValueButton As System.Windows.Forms.Button
    Friend WithEvents ValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DetSizeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
