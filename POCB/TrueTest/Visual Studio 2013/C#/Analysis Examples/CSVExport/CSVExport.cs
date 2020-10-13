using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using RadiantCommon;
using TrueTestEngine;
using System.ComponentModel;
using System.Drawing;

[Serializable()]
//DisplayAnalysisBase contains a lot of good features for tests and we recomend inheriting from it for display testing
//This includes items like RADA and remove Morie
public class CSVExport : DisplayAnalysisBase
{
    private string mFilePath = "C:\\Radiant Vision Systems Data\\TrueTest\\UserData\\";
    private string mUserName = "CSVExport";
    private const string CSVExportName = "CSVExport";

    //This is an abstract property of DisplayAnalysisBase, so that it can uniquely identify this test
    [XmlIgnore(), Browsable(false)]
    public override string Name
    {
        get { return CSVExportName; }
    }

    //Also abstract. This name can be changed by the user inside of the TrueTest GUI sequence control
    public override string UserName
    {
        get { return mUserName; }
        set { mUserName = value; }
    }

    //This property belongs to this test only.  Add as many properties as you need to the test. 
    //They can be configured in real time inside the GUI sequence control
    public string FilePath
    {
        get { return mFilePath; }
        set { mFilePath = value; }
    }

    //This is where the bulk of the test execution resides.  This method overrides the abstract Execute_() method of DisplayAnalysisBase.  
    //Put all your test execution code here.
    protected override void Execute_()
    {
        //Get reference to current measurement
        MeasurementF m = base.PatternMeasurementList[0].CurrentMeasurement;

        //Extract the Tristimulus Y image array from the measurement
        float[,] image = m.GetTristimulusArrayF(MeasurementBase.TristimlusType.TrisY);

        //Open a text file to store the image array data into
        using(StreamWriter sw = new StreamWriter(FilePath + UserName + ".csv", false))
        {
            //Now index through the 2-dimentional array and store the values into our string
            for (int row = 0; row < m.NbrRows; row++)
            {
                StringBuilder sb = new StringBuilder();

                for (int col = 0; col < m.NbrCols; col++)
                {
                    sb.Append(string.Format("{0},", image[col, row]));
                }

                //Remove the last comma from each line
                sb.Length--;
                //Write the string into the file
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
