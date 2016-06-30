using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Diagnostics;
using iTextSharp.text.pdf;

namespace XmlPdfCelta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            /*ReportDataSource dataSources = new ReportDataSource();
            dataSources.Name = "DataSet1";
            dataSources.Value = Xml.readXml();

            ReportParameter titulo = new ReportParameter("titulo", "Factura");
            
            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(dataSources);
            this.reportViewer2.LocalReport.SetParameters(new ReportParameter[] { titulo });
            this.reportViewer2.RefreshReport();*/
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer2.RefreshReport();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            //string outPath = @"C:\PDF\ReportOutput.pdf";
            string outPath = @"ReportOutput.pdf";
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, filenameExtension;
            byte[] bytes = reportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            FileStream fs = File.Create(outPath);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            Process.Start(outPath);
            
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
           
            openFile.Filter = "XML Files (*.xml)|*.xml";
            if (openFile.ShowDialog() == DialogResult.OK)
            {               
                Xml xml = new Xml(openFile.FileName);
                try
                {
                    Factura Factura = xml.readXml();
                    facturaToPDF(Factura);
                }
                catch (Exception exception) {                   
                    MessageBox.Show("Documento XML no tiene el formato correcto","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string contenido = @"<DD><RE>97036000-K</RE><TD>33</TD><F>22942343</F><FE>2016-06-24</FE><RR>50159080-0</RR><RSR>INDUSTRIAS CELTA LIMITADA</RSR><MNT>43047</MNT><IT1>COMISION/GASTO Canje</IT1><CAF version=""1.0""><DA><RE>97036000-K</RE><RS>BANCO SANTANDER CHILE</RS><TD>33</TD><RNG><D>22636639</D><H>23636638</H></RNG><FA>2016-04-26</FA><RSAPK><M>8kUS+x1Aa0r+HEmjwVwUieatLzytQewVJo8i2Uy+ZqAln8+5sIPNmfHnxoDm9EaHpFo8v6NikxIcOCzXPI+YcQ==</M><E>Aw==</E></RSAPK><IDK>300</IDK></DA><FRMA algoritmo=""SHA1withRSA"">DKCw0bGnh6eoyj0FONtfbz32cr9xcPtMV8K1UkCtcT0zQLWgf3IvZ5KF/EojtuXKuzZKNrxjbf8UyE7j/XlRBw==</FRMA></CAF><TSTED>2016-06-25T05:01:08</TSTED></DD>"; //<FRMT algoritmo=""SHA1withRSA"">TUb3tM9cYYfAFIix4GPHLUIOWF6hJ6Xvn0FpnpKNz2L33lXMrCovM8KoNXoPDzzuDG+gUDksLrwSTd0gJDJekQ==</FRMT>;           
            BarcodePDF417 pdf417 = new BarcodePDF417();
            
            pdf417.CodeRows = 5;            
            pdf417.CodeColumns=18;
            pdf417.ErrorLevel=5;
            pdf417.LenCodewords=999;
            
            pdf417.Options = BarcodePDF417.PDF417_FORCE_BINARY;
            pdf417.SetText(UTF8_to_ISO(contenido));
            
            pdf417.CreateDrawingImage(Color.Black, Color.White).Save("imgCode.png");
            //pdf417.SetText(contenido);

            //System.Drawing.Bitmap img = new Bitmap(pdf417.CreateDrawingImage(Color.Black, Color.White));
            //@"ReportOutput.pdf"
            //img.Save("imgCode.bmp");
           
        }

        private string UTF8_to_ISO(string value)
        {

          
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(value);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            string msg = iso.GetString(isoBytes);
            return msg;
        }

        private byte[] UTF8_to_IS(string value)
        {


            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(value);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);

            return isoBytes;
        }
        private void facturaToPDF(Factura factura) {

            ReportDataSource dataSources = new ReportDataSource();
            dataSources.Name = "DataSet1";
            dataSources.Value = factura.detalleFactura;

            /*ReportParameter[] parameters = new ReportParameter[2];            
            parameters[0] = new ReportParameter("par0", "value_par0");
            parameters[1] = new ReportParameter("par1", "value_par1");
            this.reportViewer1.LocalReport.SetParameters(parameters);
            */

            /* ReportParameter titulo = new ReportParameter("titulo", "Factura");
            ReportParameter titulo2 = new ReportParameter("titulo", "Factura");


            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(dataSources);
            this.reportViewer2.LocalReport.SetParameters(new ReportParameter[] { titulo,titulo2 });
            this.reportViewer2.RefreshReport();*/

        }
    }
}
