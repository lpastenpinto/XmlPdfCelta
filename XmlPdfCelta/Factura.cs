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
        public string CdgVendedor { set; get; }


        public string RutEmisor { set; get; }
        public string Folio { set; get; }        
        public string FchVenc { set; get; }
        public string Sucursal { set; get; }

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

        public List<Referencia> documentosReferencia { set; get; }


        public List<detalleFactura> detalleFactura { set; get; }


        public void formatFactura() {
            this.RznSoc = this.RznSoc.TrimEnd();
            this.GiroEmis = this.GiroEmis.TrimEnd();

            if (!Object.ReferenceEquals(null, this.DirOrigen))
            {
                this.DirOrigen = this.DirOrigen.TrimEnd();
            }
            
            if (!Object.ReferenceEquals(null, this.CmnaOrigen))
            {
                this.CmnaOrigen = this.CmnaOrigen.TrimEnd();
            }
            if (!Object.ReferenceEquals(null, this.CiudadOrigen))
            {
                this.CiudadOrigen = this.CiudadOrigen.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.CdgVendedor))
            {
                this.CdgVendedor = this.CdgVendedor.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.RznSocRecep))
            {
                this.RznSocRecep = this.RznSocRecep.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.GiroRecep))
            {
                this.GiroRecep = this.GiroRecep.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.Contacto))
            {
                this.Contacto = this.Contacto.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.DirRecep))
            {
                this.DirRecep = this.DirRecep.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.CmnaRecep))
            {
                this.CmnaRecep = this.CmnaRecep.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.CiudadRecep))
            {
                this.CiudadRecep = this.CiudadRecep.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.DirPostal))
            {
                this.DirPostal = this.DirPostal.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.CmnaPostal))
            {
                this.CmnaPostal = this.CmnaPostal.TrimEnd();
            }

            if (!Object.ReferenceEquals(null, this.CiudadPostal))
            {
                this.CiudadPostal = this.CiudadPostal.TrimEnd();
            }
            this.FchResol = FormatStringFactura.dateTimeStringToFormat(this.FchResol);
            this.FchEmis = FormatStringFactura.dateTimeStringToFormat(this.FchEmis);
            this.FchVenc = FormatStringFactura.dateTimeStringToFormat(this.FchVenc);
            //this.FchRef = FormatStringFactura.dateTimeStringToFormat(this.FchRef);

            this.MntTotalString = FormatStringFactura.numberToWord(this.MntTotal);

            this.MntNeto = FormatStringFactura.stringToPesos(this.MntNeto,false);
            this.MntExe = FormatStringFactura.stringToPesos(this.MntExe,false);
            this.TasaIVA = FormatStringFactura.doubletoString(this.TasaIVA,true);
            this.IVA = FormatStringFactura.stringToPesos(this.IVA,false);
            this.MntTotal = FormatStringFactura.stringToPesos(this.MntTotal,false);


            foreach (Referencia referencia in this.documentosReferencia)
            {
                referencia.FchRef = FormatStringFactura.dateTimeStringToFormat(referencia.FchRef);                                
            }

            foreach (detalleFactura detalle in this.detalleFactura) {
                detalle.QtyItem = FormatStringFactura.doubletoString(detalle.QtyItem,true);
                detalle.PrcItem = FormatStringFactura.stringToPesos(detalle.PrcItem,true);
                
                //detalle.PrcItem = FormatStringFactura.numberWithoutComma(detalle.PrcItem);
                detalle.DescuentoMonto = FormatStringFactura.stringToPesos(detalle.DescuentoMonto,true);
                detalle.MontoItem = FormatStringFactura.stringToPesos(detalle.MontoItem,true);
            }


        }

        public void generateImageTED(string pathImage) {
            string contenido = this.TED;
            //string pathImage = "imgCode.png";
            BarcodePDF417 pdf417 = new BarcodePDF417();

            pdf417.CodeRows = 5;
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
    public class Referencia {
        public string NroLinRef { set; get; }
        public string TpoDocRef { set; get; }
        public string FolioRef { set; get; }
        public string FchRef { set; get; }
        public string CodRef { set; get; }
        public string RazonRef { set; get; }

    }
}
