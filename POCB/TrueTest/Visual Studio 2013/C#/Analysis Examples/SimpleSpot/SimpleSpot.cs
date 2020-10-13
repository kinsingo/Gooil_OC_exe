
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml.Serialization;
using RadiantCommon;
using RadiantCommonCS;
using System.Drawing;
using TrueTestEngine;
using System.ComponentModel;

[Serializable()]
public class SimpleSpot : DisplayAnalysisBase
{

    [XmlIgnore(), BrowsableAttribute(false)]
    public override string Name
    {
        get { return "Simple Spot"; }
    }

    public override string UserName { get; set; }

    #region "Blob Analysis Properties"
    [CategoryAttribute("Blob Analysis"), DescriptionAttribute("The graphical shape used to draw the blobs on the image.")]
    public RiBitmapCtl.BlobDrawShapeEnum BlobDrawShape { get; set; }

    [CategoryAttribute("Blob Analysis"), DescriptionAttribute("The observer viewing-distance of the display in units of the display height (H).")]
    public float ViewingDistance { get; set; }

    [CategoryAttribute("Blob Analysis"), DescriptionAttribute("The spatial scale of the border attenuation function. Typical value = 0.25")]
    public string BorderAttenuationScale { get; set; }

    [CategoryAttribute("Blob Analysis"), DescriptionAttribute("T/F value indicating whether to show intermediate images during processing.")]
    public bool ShowProcessing { get; set; }

    [CategoryAttribute("Blob Analysis"), DescriptionAttribute("The type of measurement image to display.")]
    public DisplayImageEnum DisplayImage { get; set; }

    #endregion

    #region "Simple Spot Properties"

    [CategoryAttribute("Simple Spot"), DescriptionAttribute("Local contrast threshold (%) for bright mura.")]
    public float BrightContrastThreshold { get; set; }
    //%

    [XmlIgnore(), CategoryAttribute("Simple Spot"), BrowsableAttribute(true), DescriptionAttribute("The color used to draw blobs in the corner regions.")]
    public Color DrawColorCorner { get; set; }
    [BrowsableAttribute(false), XmlElement("DrawColorCorner")]
    public string XmlDrawColorCorner
    {
        get { return Serialize(DrawColorCorner); }
        set { DrawColorCorner = (Color)Deserialize(value, typeof(System.Drawing.Color)); }
    }

    #endregion

    #region "Blob Size Limits"
    [CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The minimum diameter (millimeters) of blobs to be detected.")]
    public float MinBlobSizeMM { get; set; }

    [CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The maximum diameter (millimeters) of blobs to be detected.")]
    public float MaxBlobSizeMM { get; set; }

    [CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The minimum number of CCD pixels in a blob.")]
    public int MinCCDPixels { get; set; }

    [CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The maximum number of CCD pixels in a blob.")]
    public int MaxCCDPixels { get; set; }

    [CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The shape of the blobs which will be counted.")]
    public ROIDefect.DefectShapeType BlobShape { get; set; }

    [CategoryAttribute("Blob Size Limits"), DescriptionAttribute("The aspect ratio threshold value for blobs to be counted.")]
    public float AspectRatioThreshold { get; set; }

    #endregion

    [XmlIgnore()]
    private CIEColor mGlobalReference { get; set; }

    private float MuraAreaMM { get; set; }

    protected override void Execute_()
    {
        MeasurementF mf = base.PatternMeasurementList[0].CurrentMeasurement;

        if (mf.Content == MeasurementBase.MeasurementContentType.TriStimulus)
        {
            mf = ImageProcess.MakePhotopic(mf);
            MeasurementChanged_(mf);
        }

        ImageProcess.ConvolutionTypeEnum ReferenceConvolutionType = ImageProcess.ConvolutionTypeEnum.PadWithZeros;

        MeasurementF localContrast = base.MeasureLocalContrast(mf, ViewingDistance, BorderAttenuationScale, ShowProcessing, ReferenceConvolutionType);
        
        MeasurementChanged_(localContrast, false, MeasurementBase.FalseColorOptions.MinToMax);

        Rectangle rect = new Rectangle();
        ResultList.AddRange(AnalyzeLocalContrastImage(localContrast, BlobTypeEnum.Both, rect));

        MeasurementF BrightLocalContrast = ImageProcess.RemoveNegatives(localContrast);
        MeasurementChanged_(BrightLocalContrast);

        RadiantCommon.ThresholdF T = new RadiantCommon.ThresholdF(BrightContrastThreshold, ImageProcess.ThresholdMethod.AbsoluteValue, ImageProcess.ThresholdType.Floor, 0, true);
        BrightLocalContrast = T.Threshold(BrightLocalContrast, RadiantCommonCS.TristimType.All, rect);
        MeasurementChanged_(BrightLocalContrast);

        List<DefectBase> BlobList = FindBlobs(BrightLocalContrast, mf, BrightContrastThreshold + 1E-05f, DrawColorCorner, ImageProcess.BlobType.Bright, MinBlobSizeMM, MaxBlobSizeMM, MinCCDPixels, MaxCCDPixels, BlobShape,
        AspectRatioThreshold, false);

        switch (DisplayImage)
        {
            case DisplayImageEnum.Blobs:
                List<DefectBase> TempDefectList = null;
                ImageProcess.BlobUnitEnum TempBlobNumber = ImageProcess.BlobUnitEnum.None;
                MeasurementF Blobs = ImageProcess.MakeBlobMeasurement(BlobList, TempDefectList,localContrast,ref TempBlobNumber); //BlobList, TempDefectList, ref localContrast);
                if (Blobs != null)
                {
                    OnMeasurementChanged(Blobs);                    
                }
                break;
            case DisplayImageEnum.LocalContrast:
                OnShowMeasurement(localContrast, false, MeasurementBase.FalseColorOptions.MinToMax);
                break;
            case DisplayImageEnum.Measurement:
                OnMeasurementChanged(mf);
                break;
        }

        OnDefectListChanged(BlobList, BlobDrawShape);

        int index = 1;
        foreach (RadiantCommon.DefectBase d in BlobList)
        {
            ResultList.Add(new Result(this, "Spot " + index.ToString() + " Area", d.NumPixels * localContrast.ScaleFactorCol * localContrast.ScaleFactorRow * 1000 * 1000, "mm^2"));
            index += 1;
        }

        base.PatternMeasurementList[0].DefectList = BlobList;
    }

    private void MeasurementChanged_(MeasurementBase m, bool FalseColorDisplay, MeasurementBase.FalseColorOptions FalseColorOption, float MinValue = 0, float MaxValue = 0)
    {
        if (ShowProcessing == false)
            return;
        OnShowMeasurement(m, FalseColorDisplay, FalseColorOption, MinValue, MaxValue);
    }
    private void MeasurementChanged_(MeasurementBase m)
    {
        if (ShowProcessing == false)
            return;
        OnMeasurementChanged(m);
    }

}

//**BEGIN PSEUDO CODE***
//The measurement is taken from the repository.
//It is converted to Photopic only with a call to the ImageProcess library.
//A Local Contrast measurement is generated with a call to a member of DisplayAnalysisBase
//The measurement is updated on the screen
//A call to a member of DisplayAnalysisBase is used to analyze the image and provide RMS contrast, AvgAbsContrast, MinContrast & Max Contrast to the results list
//Negative Contrast values are removed with a call to ImageProcess
//Screen Update
//An absolute threshold is created and applied to the local contrast
//Screen update
//A call to “FindBlobs”  produces a list of defects detected in the processed image
//The screen image is updated based on the found defects and the users choice for what type of image to display
//All the blobs that were found are written out to the analysis list with their areas in mm^2
//***END OF LINE***

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
