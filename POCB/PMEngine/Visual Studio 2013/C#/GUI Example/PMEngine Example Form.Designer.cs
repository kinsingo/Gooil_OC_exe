[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
public partial class PMEngineExampleForm : System.Windows.Forms.Form
{

	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode()]
	protected override void Dispose(bool disposing)
	{
		try {
			if (disposing && components != null) {
				components.Dispose();
			}
		} finally {
			base.Dispose(disposing);
		}
	}

	//Required by the Windows Form Designer

	private System.ComponentModel.IContainer components;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
            this.PMEngineCheckBox = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.MeasSetupComboBox = new System.Windows.Forms.ComboBox();
            this.AdjustExposureButton = new System.Windows.Forms.Button();
            this.TakeMeasurementButton = new System.Windows.Forms.Button();
            this.XLocTextBox = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.YLocTextBox = new System.Windows.Forms.TextBox();
            this.GetValueButton = new System.Windows.Forms.Button();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.DetSizeTextBox = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.ChangeFocus = new System.Windows.Forms.Button();
            this.AutoMR = new System.Windows.Forms.CheckBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PMEngineCheckBox
            // 
            this.PMEngineCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.PMEngineCheckBox.AutoSize = true;
            this.PMEngineCheckBox.Location = new System.Drawing.Point(12, 12);
            this.PMEngineCheckBox.Name = "PMEngineCheckBox";
            this.PMEngineCheckBox.Size = new System.Drawing.Size(91, 23);
            this.PMEngineCheckBox.TabIndex = 0;
            this.PMEngineCheckBox.Text = "Start PMEngine";
            this.PMEngineCheckBox.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 42);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(110, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Measurement Setups:";
            // 
            // MeasSetupComboBox
            // 
            this.MeasSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MeasSetupComboBox.Enabled = false;
            this.MeasSetupComboBox.FormattingEnabled = true;
            this.MeasSetupComboBox.Location = new System.Drawing.Point(130, 42);
            this.MeasSetupComboBox.Name = "MeasSetupComboBox";
            this.MeasSetupComboBox.Size = new System.Drawing.Size(253, 21);
            this.MeasSetupComboBox.TabIndex = 2;
            // 
            // AdjustExposureButton
            // 
            this.AdjustExposureButton.Enabled = false;
            this.AdjustExposureButton.Location = new System.Drawing.Point(12, 69);
            this.AdjustExposureButton.Name = "AdjustExposureButton";
            this.AdjustExposureButton.Size = new System.Drawing.Size(163, 23);
            this.AdjustExposureButton.TabIndex = 3;
            this.AdjustExposureButton.Text = "Show Adjust Exposure Form";
            this.AdjustExposureButton.UseVisualStyleBackColor = true;
            // 
            // TakeMeasurementButton
            // 
            this.TakeMeasurementButton.Enabled = false;
            this.TakeMeasurementButton.Location = new System.Drawing.Point(13, 99);
            this.TakeMeasurementButton.Name = "TakeMeasurementButton";
            this.TakeMeasurementButton.Size = new System.Drawing.Size(162, 23);
            this.TakeMeasurementButton.TabIndex = 4;
            this.TakeMeasurementButton.Text = "Take Measurement";
            this.TakeMeasurementButton.UseVisualStyleBackColor = true;
            // 
            // XLocTextBox
            // 
            this.XLocTextBox.Location = new System.Drawing.Point(53, 146);
            this.XLocTextBox.Name = "XLocTextBox";
            this.XLocTextBox.Size = new System.Drawing.Size(70, 20);
            this.XLocTextBox.TabIndex = 7;
            this.XLocTextBox.Text = "0.5";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 149);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "X-Loc";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(141, 149);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 13);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "Y-Loc";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(13, 130);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(130, 13);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "Position (relative location):";
            // 
            // YLocTextBox
            // 
            this.YLocTextBox.Location = new System.Drawing.Point(182, 146);
            this.YLocTextBox.Name = "YLocTextBox";
            this.YLocTextBox.Size = new System.Drawing.Size(70, 20);
            this.YLocTextBox.TabIndex = 9;
            this.YLocTextBox.Text = "0.5";
            // 
            // GetValueButton
            // 
            this.GetValueButton.Enabled = false;
            this.GetValueButton.Location = new System.Drawing.Point(16, 181);
            this.GetValueButton.Name = "GetValueButton";
            this.GetValueButton.Size = new System.Drawing.Size(159, 23);
            this.GetValueButton.TabIndex = 13;
            this.GetValueButton.Text = "Get Value at Position";
            this.GetValueButton.UseVisualStyleBackColor = true;
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(53, 210);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.ReadOnly = true;
            this.ValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.ValueTextBox.TabIndex = 15;
            // 
            // DetSizeTextBox
            // 
            this.DetSizeTextBox.Location = new System.Drawing.Point(382, 146);
            this.DetSizeTextBox.Name = "DetSizeTextBox";
            this.DetSizeTextBox.Size = new System.Drawing.Size(70, 20);
            this.DetSizeTextBox.TabIndex = 11;
            this.DetSizeTextBox.Text = "25";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(302, 149);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 13);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "Detector Size:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(461, 149);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(23, 13);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "mm";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(13, 213);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(22, 13);
            this.Label7.TabIndex = 14;
            this.Label7.Text = "Lv:";
            // 
            // ChangeFocus
            // 
            this.ChangeFocus.Enabled = false;
            this.ChangeFocus.Location = new System.Drawing.Point(192, 69);
            this.ChangeFocus.Name = "ChangeFocus";
            this.ChangeFocus.Size = new System.Drawing.Size(119, 23);
            this.ChangeFocus.TabIndex = 16;
            this.ChangeFocus.Text = "ChangeFocus";
            this.ChangeFocus.UseVisualStyleBackColor = true;
            // 
            // AutoMR
            // 
            this.AutoMR.AutoSize = true;
            this.AutoMR.Location = new System.Drawing.Point(354, 74);
            this.AutoMR.Name = "AutoMR";
            this.AutoMR.Size = new System.Drawing.Size(101, 17);
            this.AutoMR.TabIndex = 17;
            this.AutoMR.Tag = "";
            this.AutoMR.Text = "Remove Morie?";
            this.AutoMR.UseVisualStyleBackColor = true;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(13, 245);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(532, 341);
            this.PictureBox1.TabIndex = 18;
            this.PictureBox1.TabStop = false;
            // 
            // PMEngineExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 598);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.AutoMR);
            this.Controls.Add(this.ChangeFocus);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.DetSizeTextBox);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.GetValueButton);
            this.Controls.Add(this.YLocTextBox);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.XLocTextBox);
            this.Controls.Add(this.TakeMeasurementButton);
            this.Controls.Add(this.AdjustExposureButton);
            this.Controls.Add(this.MeasSetupComboBox);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.PMEngineCheckBox);
            this.Name = "PMEngineExampleForm";
            this.Text = "PMEngine Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PMEngineExampleForm_FormClosing);
            this.Load += new System.EventHandler(this.PMEngineExampleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

	}
	internal System.Windows.Forms.CheckBox PMEngineCheckBox;
	internal System.Windows.Forms.Label Label1;
	internal System.Windows.Forms.ComboBox MeasSetupComboBox;
	internal System.Windows.Forms.Button AdjustExposureButton;
	internal System.Windows.Forms.Button TakeMeasurementButton;
	internal System.Windows.Forms.TextBox XLocTextBox;
	internal System.Windows.Forms.Label Label2;
	internal System.Windows.Forms.Label Label3;
	internal System.Windows.Forms.Label Label4;
	internal System.Windows.Forms.TextBox YLocTextBox;
	internal System.Windows.Forms.Button GetValueButton;
	internal System.Windows.Forms.TextBox ValueTextBox;
	internal System.Windows.Forms.TextBox DetSizeTextBox;
	internal System.Windows.Forms.Label Label5;
	internal System.Windows.Forms.Label Label6;
	internal System.Windows.Forms.Label Label7;
	internal System.Windows.Forms.Button ChangeFocus;
	internal System.Windows.Forms.CheckBox AutoMR;

	internal System.Windows.Forms.PictureBox PictureBox1;
}
