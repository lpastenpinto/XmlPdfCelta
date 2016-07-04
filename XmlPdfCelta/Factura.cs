using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using System.Drawing;

namespace XmlPdfCelta
{
    class Factura
    {
        public string RznSoc { set; get; }
        public string GiroEmis { set; get; }
        public string DirOrigen { set; get; }
        public string CmnaOrigen { set; get; }
        public string CiudadOrigen { set; get; }

        public string RutEmisor { set; get; }
        public string Folio { set; get; }

        public string RUTRecep { set; get; }
        public string RznSocRecep { set; get; }
        public string GiroRecep { set; get; }
        public string Contacto { set; get; }
        public string DirRecep { set; get; }
        public string CmnaRecep { set; get; }
        public string CiudadRecep { set; get; }
        public string DirPostal { set; get; }
        public string CmnaPostal { set; get; }
        public string CiudadPostal { set; get; }
        public string FchEmis { set; get; }

        public string MntNeto { set; get; }
        public string MntExe { set; get; }
        public string TasaIVA { set; get; }
        public string IVA { set; get; }
        public string MntTotal { set; get; }

        public string FchResol { set; get; }
        public string NroResol { set; get; }

        public string MntTotalString { set; get; }

        public string TED { set; get; }
        public string TED_DD { set; get; }
        public string pathImageTED { set; get; }
        public List<detalleFactura> detalleFactura { set; get; }


        public void formatFactura() {
            this.FchResol = FormatStringFactura.dateTimeStringToFormat(this.FchResol);
            this.FchEmis = FormatStringFactura.dateTimeStringToFormat(this.FchEmis);

            this.MntTotalString = FormatStringFactura.numberToWord(this.MntTotal);

            this.MntNeto = FormatStringFactura.stringToPesos(this.MntNeto);
            this.MntExe = FormatStringFactura.stringToPesos(this.MntExe);
            this.TasaIVA = FormatStringFactura.ivaNewFormat(this.TasaIVA);
            this.IVA = FormatStringFactura.stringToPesos(this.IVA);
            this.MntTotal = FormatStringFactura.stringToPesos(this.MntTotal);

            


            foreach (detalleFactura detalle in this.detalleFactura) {
                detalle.PrcItem = FormatStringFactura.stringToPesos(detalle.PrcItem);
                detalle.DescuentoMonto = FormatStringFactura.stringToPesos(detalle.DescuentoMonto);
                detalle.MontoItem = FormatStringFactura.stringToPesos(detalle.MontoItem);
            }
        }

        public void generateImageTED(string pathImage) {
            string contenido = this.TED;
            //string pathImage = "imgCode.png";
            BarcodePDF417 pdf417 = new BarcodePDF417();

            pdf417.CodeRows =5;
            pdf417.CodeColumns = 18;
            pdf417.ErrorLevel = 5;
            pdf417.LenCodewords = 999;            
            pdf417.Options = BarcodePDF417.PDF417_FORCE_BINARY;
            pdf417.SetText(UTF8_to_ISO(contenido));
                        
            pdf417.CreateDrawingImage(Color.Black, Color.White).Save(pathImage);            
            
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

    }

    public class detalleFactura
    {
        public string NroLinDet { set; get; }
        public string TpoCodigo { set; get; }
        public string VlrCodigo { set; get; }
        public string NmbItem { set; get; }
        public string DscItem { set; get; }
        public string QtyItem { set; get; }
        public string PrcItem { set; get; }
        public string MontoItem { set; get; }

        public string DescuentoPct { get; set; }
        public string DescuentoMonto { set; get; }


    }
}
