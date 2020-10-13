namespace TrueTestEngine_CSharp_API_Example
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            RadiantCommon.ROICircle roiCircle1 = new RadiantCommon.ROICircle();
            this.RiBitmapCtl1 = new RadiantCommon.RiBitmapCtl();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.EditButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RiBitmapCtl1
            // 
            this.RiBitmapCtl1.AllowScrollZoom = true;
            this.RiBitmapCtl1.BitmapImageScaleMode = RadiantCommon.RiBitmapCtl.ImageScaleMode.Fit;
            this.RiBitmapCtl1.BlobDrawShape = RadiantCommon.RiBitmapCtl.BlobDrawShapeEnum.Contours;
            this.RiBitmapCtl1.ContourLevelList = ((System.Collections.Generic.List<byte>)(resources.GetObject("RiBitmapCtl1.ContourLevelList")));
            this.RiBitmapCtl1.CrossHairsDisplay = false;
            this.RiBitmapCtl1.DBSettings = null;
            this.RiBitmapCtl1.DefectsSelectable = true;
            this.RiBitmapCtl1.DisplayLegend = true;
            this.RiBitmapCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RiBitmapCtl1.DoubleBuffering = true;
            this.RiBitmapCtl1.DragMode = RadiantCommon.RiBitmapDragModeType.Normal;
            this.RiBitmapCtl1.EnhanceColorLevel = 0;
            this.RiBitmapCtl1.FalseColorDisplay = true;
            this.RiBitmapCtl1.FalseColorOption = RadiantCommon.MeasurementBase.FalseColorOptions.ZeroToMax;
            this.RiBitmapCtl1.GammaLevel = 0.7F;
            this.RiBitmapCtl1.GridDisplay = false;
            this.RiBitmapCtl1.HelpButtonDisplay = false;
            this.RiBitmapCtl1.HideCursorBox = false;
            this.RiBitmapCtl1.ImageInitialized = false;
            this.RiBitmapCtl1.InterpolateImage = false;
            this.RiBitmapCtl1.LABReferenceColor = ((RadiantCommon.CIEColor)(resources.GetObject("RiBitmapCtl1.LABReferenceColor")));
            this.RiBitmapCtl1.Location = new System.Drawing.Point(0, 52);
            this.RiBitmapCtl1.LocationDigits = -1;
            this.RiBitmapCtl1.LogScaleDisplay = false;
            this.RiBitmapCtl1.Measurement = null;
            this.RiBitmapCtl1.Name = "RiBitmapCtl1";
            this.RiBitmapCtl1.OptionsButtonDisplay = true;
            this.RiBitmapCtl1.Pen = null;
            this.RiBitmapCtl1.PrintButtonDisplay = false;
            this.RiBitmapCtl1.QuantityDisplayed = RadiantCommon.QuantityEnum.Luminance;
            this.RiBitmapCtl1.RegionOfInterest = null;
            this.RiBitmapCtl1.RoadDisplay = false;
            this.RiBitmapCtl1.ROIArray = new RadiantCommon.RegionOfInterest[0];
            this.RiBitmapCtl1.ROISelectedIndex = -1;
            this.RiBitmapCtl1.ROISelectionEnabled = false;
            this.RiBitmapCtl1.SaveButtonDisplay = false;
            this.RiBitmapCtl1.ShowBlobInfo = true;
            this.RiBitmapCtl1.ShowContourLines = false;
            this.RiBitmapCtl1.ShowCrossHairsButton = true;
            this.RiBitmapCtl1.ShowCursor = true;
            this.RiBitmapCtl1.ShowDistanceUnitLabels = true;
            this.RiBitmapCtl1.ShowOptions = false;
            this.RiBitmapCtl1.Size = new System.Drawing.Size(859, 526);
            this.RiBitmapCtl1.TabIndex = 3;
            this.RiBitmapCtl1.ToolbarDisplayed = true;
            this.RiBitmapCtl1.ToolbarEnabled = true;
            this.RiBitmapCtl1.TrueColorString = "True Color";
            this.RiBitmapCtl1.ViewRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            roiCircle1.BackgroundBrush = System.Drawing.Color.Empty;
            roiCircle1.Diameter = 0F;
            roiCircle1.DistanceScale = RadiantCommon.DistanceScaleType.Physical;
            roiCircle1.DistanceUnit = RadiantCommon.DistanceUnitType.Millimeters;
            roiCircle1.DrawCircleCrosshairs = false;
            roiCircle1.Eval = null;
            roiCircle1.FOIsetID = 0;
            roiCircle1.Guid = new System.Guid("00000000-0000-0000-0000-000000000000");
            roiCircle1.Info = null;
            roiCircle1.LabelList = null;
            roiCircle1.Location = ((System.Drawing.PointF)(resources.GetObject("roiCircle1.Location")));
            roiCircle1.LocationDistanceScale = RadiantCommon.DistanceScaleType.Pixels;
            roiCircle1.LocationDistanceUnit = RadiantCommon.DistanceUnitType.Millimeters;
            roiCircle1.PenColor = System.Drawing.Color.Black;
            roiCircle1.PenDashOffset = 0F;
            roiCircle1.PenDashPattern = new float[] {
        1F,
        1F};
            roiCircle1.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            roiCircle1.PenWidth = 1F;
            roiCircle1.Radius = 0F;
            roiCircle1.Selected = true;
            roiCircle1.SequenceIndex = -1;
            roiCircle1.Transformation = null;
            this.RiBitmapCtl1.VirtualDetectorRegion = roiCircle1;
            this.RiBitmapCtl1.VirtualDetectorShow = true;
            this.RiBitmapCtl1.WhiteBackgroundDisplay = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.EditButton);
            this.Panel1.Controls.Add(this.RunButton);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(859, 52);
            this.Panel1.TabIndex = 2;
            // 
            // EditButton
            // 
            this.EditButton.Location = new System.Drawing.Point(110, 6);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(92, 40);
            this.EditButton.TabIndex = 1;
            this.EditButton.Text = "Edit...";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(12, 6);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(92, 40);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 578);
            this.Controls.Add(this.RiBitmapCtl1);
            this.Controls.Add(this.Panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal RadiantCommon.RiBitmapCtl RiBitmapCtl1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button EditButton;
        internal System.Windows.Forms.Button RunButton;
    }
}

