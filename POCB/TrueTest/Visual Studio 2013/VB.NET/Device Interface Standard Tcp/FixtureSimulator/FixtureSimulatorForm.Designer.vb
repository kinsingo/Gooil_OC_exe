<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FixtureSimulatorForm
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
        Me.openButton = New System.Windows.Forms.Button()
        Me.closeButton = New System.Windows.Forms.Button()
        Me.outputTextBox = New System.Windows.Forms.TextBox()
        Me.commandsListBox = New System.Windows.Forms.ListBox()
        Me.inputTextBox = New System.Windows.Forms.TextBox()
        Me.sendButton = New System.Windows.Forms.Button()
        Me.CustomCommandBox = New System.Windows.Forms.TextBox()
        Me.SendCustomButton = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.IPAddressForm = New System.Windows.Forms.TextBox()
        Me.PortForm = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'openButton
        '
        Me.openButton.Location = New System.Drawing.Point(16, 15)
        Me.openButton.Name = "openButton"
        Me.openButton.Size = New System.Drawing.Size(75, 23)
        Me.openButton.TabIndex = 0
        Me.openButton.Text = "Open"
        Me.openButton.UseVisualStyleBackColor = True
        '
        'closeButton
        '
        Me.closeButton.Location = New System.Drawing.Point(16, 47)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(75, 23)
        Me.closeButton.TabIndex = 1
        Me.closeButton.Text = "Close"
        Me.closeButton.UseVisualStyleBackColor = True
        '
        'outputTextBox
        '
        Me.outputTextBox.Location = New System.Drawing.Point(9, 204)
        Me.outputTextBox.Multiline = True
        Me.outputTextBox.Name = "outputTextBox"
        Me.outputTextBox.ReadOnly = True
        Me.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.outputTextBox.Size = New System.Drawing.Size(361, 124)
        Me.outputTextBox.TabIndex = 2
        '
        'commandsListBox
        '
        Me.commandsListBox.FormattingEnabled = True
        Me.commandsListBox.Location = New System.Drawing.Point(274, 15)
        Me.commandsListBox.Name = "commandsListBox"
        Me.commandsListBox.Size = New System.Drawing.Size(97, 82)
        Me.commandsListBox.TabIndex = 3
        '
        'inputTextBox
        '
        Me.inputTextBox.Location = New System.Drawing.Point(9, 372)
        Me.inputTextBox.Multiline = True
        Me.inputTextBox.Name = "inputTextBox"
        Me.inputTextBox.ReadOnly = True
        Me.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.inputTextBox.Size = New System.Drawing.Size(361, 116)
        Me.inputTextBox.TabIndex = 4
        '
        'sendButton
        '
        Me.sendButton.Location = New System.Drawing.Point(274, 108)
        Me.sendButton.Name = "sendButton"
        Me.sendButton.Size = New System.Drawing.Size(96, 23)
        Me.sendButton.TabIndex = 5
        Me.sendButton.Text = "Send Selected"
        Me.sendButton.UseVisualStyleBackColor = True
        '
        'CustomCommandBox
        '
        Me.CustomCommandBox.AcceptsReturn = True
        Me.CustomCommandBox.Location = New System.Drawing.Point(390, 32)
        Me.CustomCommandBox.Name = "CustomCommandBox"
        Me.CustomCommandBox.ShortcutsEnabled = False
        Me.CustomCommandBox.Size = New System.Drawing.Size(187, 20)
        Me.CustomCommandBox.TabIndex = 6
        '
        'SendCustomButton
        '
        Me.SendCustomButton.Location = New System.Drawing.Point(583, 32)
        Me.SendCustomButton.Name = "SendCustomButton"
        Me.SendCustomButton.Size = New System.Drawing.Size(75, 23)
        Me.SendCustomButton.TabIndex = 7
        Me.SendCustomButton.Text = "Send"
        Me.SendCustomButton.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(402, 219)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(256, 256)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(399, 203)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Pattern Display"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(108, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "IP Address"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(109, 52)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(26, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Port"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 188)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "OutputBox"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 356)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "InputBox"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(108, 137)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Terminator"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Enabled = False
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label10.Location = New System.Drawing.Point(172, 129)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Padding = New System.Windows.Forms.Padding(14, 3, 14, 3)
        Me.Label10.Size = New System.Drawing.Size(56, 21)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "CrLf"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(387, 15)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 13)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Custom Commands"
        '
        'ClearButton
        '
        Me.ClearButton.Location = New System.Drawing.Point(16, 80)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(75, 23)
        Me.ClearButton.TabIndex = 31
        Me.ClearButton.Text = "Clear Logs"
        Me.ClearButton.UseVisualStyleBackColor = True
        '
        'IPAddressForm
        '
        Me.IPAddressForm.Location = New System.Drawing.Point(172, 18)
        Me.IPAddressForm.Name = "IPAddressForm"
        Me.IPAddressForm.Size = New System.Drawing.Size(56, 20)
        Me.IPAddressForm.TabIndex = 32
        Me.IPAddressForm.Text = "127.0.0.1"
        '
        'PortForm
        '
        Me.PortForm.Location = New System.Drawing.Point(172, 50)
        Me.PortForm.Name = "PortForm"
        Me.PortForm.Size = New System.Drawing.Size(56, 20)
        Me.PortForm.TabIndex = 33
        Me.PortForm.Text = "5000"
        '
        'FixtureSimulatorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 506)
        Me.Controls.Add(Me.PortForm)
        Me.Controls.Add(Me.IPAddressForm)
        Me.Controls.Add(Me.ClearButton)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.SendCustomButton)
        Me.Controls.Add(Me.CustomCommandBox)
        Me.Controls.Add(Me.sendButton)
        Me.Controls.Add(Me.inputTextBox)
        Me.Controls.Add(Me.commandsListBox)
        Me.Controls.Add(Me.outputTextBox)
        Me.Controls.Add(Me.closeButton)
        Me.Controls.Add(Me.openButton)
        Me.Name = "FixtureSimulatorForm"
        Me.Text = "Standard DI Fixture Simulator"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents openButton As System.Windows.Forms.Button
    Friend WithEvents closeButton As System.Windows.Forms.Button
    Friend WithEvents outputTextBox As System.Windows.Forms.TextBox
    Friend WithEvents commandsListBox As System.Windows.Forms.ListBox
    Friend WithEvents inputTextBox As System.Windows.Forms.TextBox
    Friend WithEvents sendButton As System.Windows.Forms.Button
    Friend WithEvents CustomCommandBox As System.Windows.Forms.TextBox
    Friend WithEvents SendCustomButton As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ClearButton As System.Windows.Forms.Button
    Friend WithEvents IPAddressForm As System.Windows.Forms.TextBox
    Friend WithEvents PortForm As System.Windows.Forms.TextBox

End Class
