using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Radiant;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AssemblyLoader loader = new AssemblyLoader(@"C:\Program Files\Radiant Vision Systems\ProMetric 10.11");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PMEngineExampleForm());
        }
    }
}
