
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml.Serialization;
using RadiantCommon;
using TrueTestEngine;
using System.ComponentModel;
using System.Drawing;

//The class name usually matches the test name. This class will become the test
[Serializable()]
//DisplayAnalysisBase contains a lot of good features for tests and we recomend inheriting from it for display testing
//This includes items like RADA and remove Morie
public class PNGExport : DisplayAnalysisBase
{
    private const string PNGExportName = "PNGExport";
    //This is a must override of DisplayAnalysisBase, so that it can uniquly identify this test
    [XmlIgnore(), Browsable(false)]
    public override string Name
    {
        get { return PNGExportName; }
    }

    //Also a must override, this name can be changed by the user inside of the TrueTest GUI sequence control
    public override string UserName { get; set; }

    //This property belongs to this test only, add as many properties as you need to the test and they can be configured in real time inside the GUI sequence control
    public string FilePath { get; set; }

    //This is where the bluk of the test execution resides, in this must override sub of DisplayAnalysisBase.  Put all your test execution code here

    protected override void Execute_()
	{
		//Store the current measurement in the object "m"
		MeasurementF m = base.PatternMeasurementList[0].CurrentMeasurement;

        byte[,] grayLevel = null;
        Bitmap b = m.CreateBitmapTrueColor(MeasurementBase.TransformMethod.sRGB_D65, new float[3] {1,1,1}, false, 2.2F, false, ref grayLevel);
        
		b.Save(FilePath, System.Drawing.Imaging.ImageFormat.Png);

	}

}