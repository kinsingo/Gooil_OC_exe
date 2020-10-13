using RiPMEngine;
using RadiantCommon;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;

public partial class PMEngineExampleForm
{
	PMEngine mPMObj;
	MeasurementSetup measSetup;
	PMMeasurementF meas;

    public PMEngineExampleForm()
    {
        InitializeComponent();
    }

	private void PMEngineExampleForm_Load(object sender, EventArgs e)
	{
		PMEngineCheckBox.CheckedChanged += PMEngineCheckBox_CheckedChanged;
		MeasSetupComboBox.SelectedIndexChanged += MeasSetupComboBox_SelectedIndexChanged;
		AdjustExposureButton.Click += AdjustExposureButton_Click;
		TakeMeasurementButton.Click += TakeMeasurementButton_Click;
		GetValueButton.Click += GetValueButton_Click;
		ChangeFocus.Click += ChangeFocus_Click;
		AutoMR.CheckedChanged += AutoMR_CheckedChanged;
	}

	private void PMEngineCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		if (PMEngineCheckBox.Checked) 
        {
			//create a new pmengine obj
			mPMObj = new PMEngine();

			//set the cal database
			//manually change this string to the path to your camera's database
            mPMObj.SetCalibrationDatabase("C:\\Radiant Vision Systems Data\\Camera Data\\Calibration Files\\PM Calibration Demo Camera.calx");

			//start the camera
			mPMObj.InitializeCamera();

			//set the measurement database
			mPMObj.MeasurementDatabaseName = "C:\\Radiant Vision Systems Data\\ProMetric\\UserData\\Sample.pmxm";

			//enable miscellaneous controls
			MeasSetupComboBox.Enabled = true;
			AdjustExposureButton.Enabled = true;
			TakeMeasurementButton.Enabled = true;
			ChangeFocus.Enabled = true;

			//get the list of measurement setups and put into the combobox
			ListItem[] msList = new ListItem[0];
			mPMObj.GetMeasurementSetupList(ref msList);
			CommonFunctions.LoadComboBoxNoSelect(ref MeasSetupComboBox, msList);
			MeasSetupComboBox.SelectedIndex = 0;
		} 
        else 
        {
			//set the PMEngine object to nothing
			mPMObj = null;

			//disable controls on the form to prevent errors
			MeasSetupComboBox.Enabled = false;
			AdjustExposureButton.Enabled = false;
			TakeMeasurementButton.Enabled = false;
			GetValueButton.Enabled = false;
		}
	}

	private void MeasSetupComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		//get the measurementsetup ID from the combobox
		int measSetupID = CommonFunctions.GetListItemIDfromComboBox(MeasSetupComboBox);

		//read the measurementsetup from the database
		measSetup = mPMObj.ReadMeasurementSetupfromDatabase(measSetupID);

		//additionally alter the measurementsetup if needed
        measSetup.SubFrameRegion = new System.Drawing.Rectangle(0, 0, 0, 0);    //maximizes subframe
	}
	
	private void AdjustExposureButton_Click(object sender, EventArgs e)
	{
		//Shows the adjust exposure form (exposure times are saved to exposure property)
		mPMObj.ShowAdjustExposureForm(ref measSetup);
	}

	private void TakeMeasurementButton_Click(object sender, EventArgs e)
	{
		//Take a measurement
        int isBlank = 0;
        float[] averageIntensity = {0};
        meas = mPMObj.TakeMeasurementF(measSetup, "Descript", 0, 0, "", "", ref isBlank, ref averageIntensity);

		//Enable the get value button
		this.GetValueButton.Enabled = true;

		Bitmap bitmap = meas.CreateBitmapMonochrome(new Measurement(meas).GetTristimulusArray(MeasurementBase.TristimlusType.TrisY));
		PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
		PictureBox1.Image = bitmap;
	}

	private void GetValueButton_Click(object sender, EventArgs e)
	{
		//Create a circular detector for the size specified
		ROICircle ROICircleObj = new ROICircle(DistanceUnitType.Millimeters, float.Parse(DetSizeTextBox.Text));

		//Set other detector properties, such as location
		float locationX = float.Parse(XLocTextBox.Text);
		float locationY = float.Parse(YLocTextBox.Text);
		ROICircleObj.LocationDistanceScale = DistanceScaleType.Relative;
		ROICircleObj.set_Center(meas, new PointF(locationX, locationY));

		//create a new CIEColor object
		CIEColor CIEColorObj;
		CIEColorObj = ROICircleObj.GetColor(meas);

		//display the luminance value
		ValueTextBox.Text = CIEColorObj.Lv.ToString();
	}

	private void ChangeFocus_Click(object sender, EventArgs e)
	{
		float focus = float.Parse(Interaction.InputBox("Enter the focus distance", "Focus Setter"));
		measSetup.LensDistance = focus;
		float fstop = float.Parse(Interaction.InputBox("Enter the f-stop", "F-Stop Setter"));
		measSetup.LensfStop = fstop;
	}

	private void AutoMR_CheckedChanged(object sender, EventArgs e)
	{
		measSetup.RemoveMoire = AutoMR.Checked;
	}

	private void PMEngineExampleForm_FormClosing(object sender, FormClosingEventArgs e)
	{
        if (mPMObj != null)
        {
            mPMObj.Shutdown();
        }
	}
}
