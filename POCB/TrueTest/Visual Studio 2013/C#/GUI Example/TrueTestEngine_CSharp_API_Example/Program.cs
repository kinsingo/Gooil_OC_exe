using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Radiant;

namespace TrueTestEngine_CSharp_API_Example
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AssemblyLoader loader = new AssemblyLoader(@"C:\Program Files\Radiant Vision Systems\TrueTest 1.7");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
