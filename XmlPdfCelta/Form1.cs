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
        Factura Factura;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer2.RefreshReport();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                string outPath = "DocumentoElectronico_RUT" + Factura.RutEmisor + "_RznSoc" + Factura.RznSoc + "_Folio" + Factura.Folio + ".pdf";

                savePDF(outPath);
            }
            catch (Exception ex) {
                MessageBox.Show("Imposible exportar archivo. Verifique que el documento xml este cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
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
                    Factura = xml.readXml();
                    Factura.formatFactura();
                    facturaToPDF(Factura);
                }
                catch (Exception ex) {                   
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

            ReportParameter[] parameters = new ReportParameter[26];            
            parameters[0] = new ReportParameter("RznSoc", factura.RznSoc);
            parameters[1] = new ReportParameter("GiroEmis", factura.GiroEmis);
            parameters[2] = new ReportParameter("DirOrigen", factura.DirOrigen);
            parameters[3] = new ReportParameter("CmnaOrigen", factura.CmnaOrigen);
            parameters[4] = new ReportParameter("CiudadOrigen", factura.CiudadOrigen);
            parameters[5] = new ReportParameter("RutEmisor", factura.RutEmisor);
            parameters[6] = new ReportParameter("Folio", factura.Folio);
            parameters[7] = new ReportParameter("RUTRecep", factura.RUTRecep);
            parameters[8] = new ReportParameter("RznSocRecep", factura.RznSocRecep);
            parameters[9] = new ReportParameter("GiroRecep", factura.GiroRecep);
            parameters[10] = new ReportParameter("Contacto", factura.Contacto);
            parameters[11] = new ReportParameter("DirRecep", factura.DirRecep);
            parameters[12] = new ReportParameter("CmnaRecep", factura.CmnaRecep);
            parameters[13] = new ReportParameter("CiudadRecep", factura.CiudadRecep);
            parameters[14] = new ReportParameter("DirPostal", factura.DirPostal);
            parameters[15] = new ReportParameter("CmnaPostal", factura.CmnaPostal);
            parameters[16] = new ReportParameter("CiudadPostal", factura.CiudadPostal);
            parameters[17] = new ReportParameter("FchEmis", factura.FchEmis);
            parameters[18] = new ReportParameter("MntNeto", factura.MntNeto);
            parameters[19] = new ReportParameter("MntExe", factura.MntExe);
            parameters[20] = new ReportParameter("TasaIVA", factura.TasaIVA);
            parameters[21] = new ReportParameter("IVA", factura.IVA);
            parameters[22] = new ReportParameter("MntTotal", factura.MntTotal);
            parameters[23] = new ReportParameter("FchResol", factura.FchResol);
            parameters[24] = new ReportParameter("NroResol", factura.NroResol);
            parameters[25] = new ReportParameter("MntTotalString", factura.MntTotalString);
            

            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(dataSources);           
            this.reportViewer2.LocalReport.SetParameters(parameters);
            
            this.reportViewer2.RefreshReport();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "(*.pdf)|*.pdf";
                saveFileDialog1.Title = "Guardar Documento PDF";

                saveFileDialog1.FileName = "DocumentoElectronico_RUT" + Factura.RutEmisor + "_RznSoc" + Factura.RznSoc + "_Folio" + Factura.Folio + ".pdf";

                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    savePDF(saveFileDialog1.FileName);
                }
            }
            catch (Exception ex) {

                MessageBox.Show("Imposible exportar archivo. Verifique que el documento xml este cargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void savePDF(string outPath) {

            //string outPath = "DocumentoElectronico_RUT" + Factura.RutEmisor + "_RznSoc" + Factura.RznSoc + "_Folio" + Factura.Folio + ".pdf";

            outPath = @outPath;
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, filenameExtension;

            try
            {
                byte[] bytes = reportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                FileStream fs = File.Create(outPath);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                Process.Start(outPath);
            }
            catch (Exception)
            {
                MessageBox.Show("Imposible exportar archivo. Verifique que archivo pdf esta cerrado");
            }
        }

        private void buttonExportWord_Click(object sender, EventArgs e)
        {

            /*SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Word Doc (*.doc)|*.doc";
            saveFileDialog1.Title = "Guardar Documento WORD";

            saveFileDialog1.FileName = "DocumentoElectronico_RUT" + Factura.RutEmisor + "_RznSoc" + Factura.RznSoc + "_Folio" + Factura.Folio + ".docx";

            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                byte[] bytes = reportViewer2.LocalReport.Render("WORDOPENXML");

                FileStream file = new FileStream(@saveFileDialog1.FileName, FileMode.Create);
                file.Write(bytes, 0, bytes.Length);
                file.Close();
            }
            */
        }
    }
}
