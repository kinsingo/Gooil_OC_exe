using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueTestEngine;
using TrueTestPatternGenerator;

namespace ExamplePG
{
    [Serializable()]
    public class ExamplePG : PatternGeneratorBase
    {
        protected override void ShowPattern(TrueTestPattern p)
        {
            if (p != null && p.PatternTypeName == "SolidColorPattern")
            {
                switch (p.Name)
                {
                    case "Red 255":
                        //Handle red pattern
                        break;
                    case "Green 255":
                        //Handle green pattern
                        break;
                    case "Blue 255":
                        //Handle blue pattern
                        break;
                    case "White":
                        //Handle white pattern
                        break;
                }
            }
        }

        protected override bool Initialize()
        {
            AddEventHandlers();
            IsInitialized = true;
            return true;
        }

        protected override bool ShutDown()
        {
            RemoveEventHandlers();
            IsInitialized = false;
            return true;
        }

        private void AddEventHandlers()
        {
            TrueTest._AnalysisComplete += AnalysisComplete;
            TrueTest.SequenceComplete += SequenceComplete;
        }

        private void RemoveEventHandlers()
        {
            TrueTest._AnalysisComplete -= AnalysisComplete;
            TrueTest.SequenceComplete -= SequenceComplete;
        }

        private void AnalysisComplete(object sender, string e)
        {
            var mode = ObjectRepository.ReadWriteEnum.ReadOnlyNoWait;
            var ea = ObjectRepository.GetItem(e, ref mode) as TrueTestEngine.AnalysisCompleteEventArgs;

            //Do something with analysis results
        }

        private void SequenceComplete(object sender, SequenceCompleteEventsArgs e)
        {
            //Do something to handle end of sequence
            //For example, report results
        }
    }
}
