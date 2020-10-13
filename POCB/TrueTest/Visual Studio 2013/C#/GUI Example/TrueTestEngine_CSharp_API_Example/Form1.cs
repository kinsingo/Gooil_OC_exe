using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrueTestEngine;
using RadiantCommon;

namespace TrueTestEngine_CSharp_API_Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var calibrationDBResult = TrueTest.APIResultEnum.Success;
            var sequenceDBResult = TrueTest.APIResultEnum.Success;
            var measurementDBResult = TrueTest.APIResultEnum.Success;

            string calibrationDatabase = Properties.Settings.Default.CalibrationDatabase;
            string sequenceFileName = Properties.Settings.Default.SequenceXMLPathFileName;
            string measurementFileName = Properties.Settings.Default.MeasurementDatabasePathFileName;

            // Initialize TrueTest
            TrueTest.Initialize(ref calibrationDatabase, 
                ref sequenceFileName, 
                ref measurementFileName, 
                this,   //Give TrueTest a reference to the main application form (this)
                ref calibrationDBResult,
                ref sequenceDBResult,
                ref measurementDBResult);

            // The strings are passed in by reference, so if the user browses to select a new file during initialization, the values will be automatically updated
            // Therefore we still need to save My.Settings.                   
            Properties.Settings.Default.CalibrationDatabase = calibrationDatabase;
            Properties.Settings.Default.SequenceXMLPathFileName = sequenceFileName;
            Properties.Settings.Default.MeasurementDatabasePathFileName = measurementFileName;
            Properties.Settings.Default.Save();

            // Add some event handlers to handle events coming from TrueTest
            AddTrueTestEventHandlers();

            // Tells TrueTest to take measurements with the camera rather than retrieving measurements from the database
            TrueTest.OperatingMode = TrueTest.OperatingModeEnum.UseCamera;

            // Makes sure to save all results into the measurement database
            TrueTest.AppSettings().SaveAnalysisResultsToDatabase = true;

            // If the user wants to make a camera measurement, make sure it gets saved to the database
            TrueTest.SaveMeasurementsToDatabase = true;

            this.Text = TrueTest.get_Sequence().Name;
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            TrueTest.Shutdown();
        }

        private void AddTrueTestEventHandlers()
        {
            TrueTest._MeasurementComplete += TrueTest__MeasurementComplete;
            TrueTest._MeasurementChanged += TrueTest__MeasurementChanged;
            TrueTest._DefectListChanged += TrueTest__DefectListChanged;
            TrueTest._ROIArrayChanged += TrueTest__ROIArrayChanged;
            TrueTest._AnalysisComplete += TrueTest__AnalysisComplete;
            TrueTest.SequenceComplete += TrueTest_SequenceComplete;   
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            // Disables buttons on the main form
            this.EnableButtons(false);

            // Clear and reset the bitmap control
            RiBitmapCtl1.ROIArray = null;
            RiBitmapCtl1.ClearDefects();
            RiBitmapCtl1.Measurement = null;
            RiBitmapCtl1.SetBackColor(Color.Black);

            // Ask for the serial number.  If the user quits or cancels, exit without starting the sequence
            string serialnumber = string.Empty;
            if (TrueTest.ShowSerialNumberDialog(ref serialnumber) != System.Windows.Forms.DialogResult.OK)
            {
                EnableButtons(true);
                return;
            }

            TrueTest.SequenceRunAll();
        }

        private void EnableButtons(bool Enabled)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { EnableButtons(Enabled); }));
            }
            else
            {
                RunButton.Enabled = Enabled;
                EditButton.Enabled = Enabled;
                if (Enabled) RunButton.Focus();
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            TrueTest.ShowEditSequenceDialog();
        }

        #region TrueTest Event Handling
        private void UpdateBitmap(MeasurementBase m)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { UpdateBitmap(m); }));
            }
            else
            {
                RiBitmapCtl1.Measurement = m;
            }
        }

        private void SetDefectList(List<DefectBase> DefectList)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { SetDefectList(DefectList); }));
            }
            else
            {
                RiBitmapCtl1.SetDefectList(DefectList);
                Application.DoEvents();
            }

        }

        private void SetROIArray(RegionOfInterest[] ROIArray)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => { SetROIArray(ROIArray); }));
            }
            else
            {
                RiBitmapCtl1.ROIArray = ROIArray;
                Application.DoEvents();
            }
        }

        void TrueTest_SequenceComplete(object sender, SequenceCompleteEventsArgs e)
        {
            var s = "Failed Tests= " + e.NumFailedTests.ToString();
            MessageBox.Show("Sequence Complete: " + e.SequenceName + Environment.NewLine + s);
            EnableButtons(true);

            if (e.PassFail == TrueTest.AnalysisResultEnum.Fail)
            {
                // *FAIL*
            }
        }

        void TrueTest__AnalysisComplete(object sender, string e)
        {
            var mode = ObjectRepository.ReadWriteEnum.ReadOnlyNoWait;            
            var ea = ObjectRepository.GetItem(e, ref mode) as TrueTestEngine.AnalysisCompleteEventArgs;
            if (ea != null)
            {
                string s = ea.Results.Count > 0 ? ea.Results[0].Name + "= " + ea.Results[0].Value.ToString() : "";
                MessageBox.Show("Analysis Complete: " + ea.AnalysisUserName + Environment.NewLine + s + Environment.NewLine + ea.PassFail.ToString());

                if (ea.PassFail == TrueTest.AnalysisResultEnum.Fail)
                {
                    // cancel the rest of the sequence
                    TrueTest.SequenceStop();
                }
            }
        }

        void TrueTest__ROIArrayChanged(object sender, string e)
        {
            var mode = ObjectRepository.ReadWriteEnum.ReadOnlyNoWait;
            var ea = ObjectRepository.GetItem(e, ref mode) as TrueTestEngine.ROIArrayChangedEventArgs;
            if (ea != null)
            {
                SetROIArray(ea.ROIArray);
            }
        }

        void TrueTest__DefectListChanged(object sender, string e)
        {
            if (!TrueTest.AppSettings().DisplayDefectList)
            {
                return;
            }

            var mode = ObjectRepository.ReadWriteEnum.ReadOnlyNoWait;
            var ea = ObjectRepository.GetItem(e, ref mode) as TrueTestEngine.DefectListChangedEventArgs;
            if (ea != null)
            {
                SetDefectList(ea.DefectList);
            }
        }

        void TrueTest__MeasurementChanged(object sender, string e)
        {
            if (!TrueTest.AppSettings().DisplayUpdatedMeasurements)
            {
                return;
            }

            var mode = ObjectRepository.ReadWriteEnum.ReadOnlyNoWait;
            var ea = ObjectRepository.GetItem(e, ref mode) as TrueTestEngine.MeasurementChangedEventArgs;
            if (ea != null)
            {
                UpdateBitmap(ea.Measurement);
            }
        }

        void TrueTest__MeasurementComplete(object sender, string e)
        {
            if (!TrueTest.AppSettings().DisplayCameraMeasurements)
            {
                return;
            }

            var mode = ObjectRepository.ReadWriteEnum.ReadOnlyNoWait;
            var ea = ObjectRepository.GetItem(e, ref mode) as TrueTestEngine.MeasurementCompleteEventArgs;
            if (ea != null)
            {
                UpdateBitmap(ea.Measurement);
            }
        }
        #endregion
               
    }
}
