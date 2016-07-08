using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlPdfCelta
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var parameters = Environment.GetCommandLineArgs().ToList();
            
            string pathXML=parameters[1];
            string pathPDF = Path.GetDirectoryName(pathXML);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(pathXML, pathPDF));
        }
    }
}
