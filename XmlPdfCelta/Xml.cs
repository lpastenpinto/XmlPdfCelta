using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlPdfCelta
{
    class Xml
    {
        /*public string title { set; get; }
        public string subTitle { set; get; }

        public static List<Xml> readXml()
        {
            List<Xml> listXmls = new List<Xml>();
            Xml customXML = new Xml();
            customXML.title = "TITULO";
            customXML.subTitle = "Subttulo";
            listXmls.Add(customXML);

            Xml customXML2 = new Xml();
            customXML2.title = "TITULO2";
            customXML2.subTitle = "Subttulo2";
            listXmls.Add(customXML2);
            return listXmls;
        
        }*/
        XmlDocument xDoc = new XmlDocument();
        Factura Factura = new Factura();
        List<detalleFactura> detalleFactura = new List<detalleFactura>();
        public Xml(string path) {            
            xDoc.Load(path);
        }

        public Factura readXml() {
            XmlNodeList EnvioDTE = xDoc.GetElementsByTagName("EnvioDTE");
            XmlNodeList SetDTE =
               ((XmlElement)EnvioDTE[0]).GetElementsByTagName("SetDTE");

            XmlNodeList Caratula=
               ((XmlElement)SetDTE[0]).GetElementsByTagName("Caratula");

            XmlNodeList DTE =
               ((XmlElement)SetDTE[0]).GetElementsByTagName("DTE");

            XmlNodeList Documento =
               ((XmlElement)DTE[0]).GetElementsByTagName("Documento");

            XmlNodeList Encabezado =
               ((XmlElement)Documento[0]).GetElementsByTagName("Encabezado");

            XmlNodeList IdDoc =
               ((XmlElement)Encabezado[0]).GetElementsByTagName("IdDoc");

            XmlNodeList Emisor =
               ((XmlElement)Encabezado[0]).GetElementsByTagName("Emisor");

            XmlNodeList Receptor =
               ((XmlElement)Encabezado[0]).GetElementsByTagName("Receptor");

            XmlNodeList Totales =
               ((XmlElement)Encabezado[0]).GetElementsByTagName("Totales");

            XmlNodeList Detalles =
               ((XmlElement)Documento[0]).GetElementsByTagName("Detalle");

            
            Factura.RutEmisor = ((XmlElement)Emisor[0]).GetElementsByTagName("RUTEmisor")[0].InnerText;
            Factura.RznSoc = ((XmlElement)Emisor[0]).GetElementsByTagName("RznSoc")[0].InnerText;
            Factura.GiroEmis= ((XmlElement)Emisor[0]).GetElementsByTagName("GiroEmis")[0].InnerText;
            Factura.DirOrigen = ((XmlElement)Emisor[0]).GetElementsByTagName("DirOrigen")[0].InnerText;
            Factura.CmnaOrigen = ((XmlElement)Emisor[0]).GetElementsByTagName("CmnaOrigen")[0].InnerText;
            Factura.CiudadOrigen = ((XmlElement)Emisor[0]).GetElementsByTagName("CiudadOrigen")[0].InnerText;
            
            Factura.RutEmisor = ((XmlElement)Caratula[0]).GetElementsByTagName("RutEmisor")[0].InnerText;
            Factura.Folio= ((XmlElement)IdDoc[0]).GetElementsByTagName("Folio")[0].InnerText;
            Factura.FchEmis = ((XmlElement)IdDoc[0]).GetElementsByTagName("FchEmis")[0].InnerText;
            

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("RUTRecep")[0])) {
                Factura.RUTRecep = ((XmlElement)Receptor[0]).GetElementsByTagName("RUTRecep")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("RznSocRecep")[0])) {
                Factura.RznSocRecep = ((XmlElement)Receptor[0]).GetElementsByTagName("RznSocRecep")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("GiroRecep")[0])) {
                Factura.GiroRecep = ((XmlElement)Receptor[0]).GetElementsByTagName("GiroRecep")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("Contacto")[0])) {
                Factura.Contacto = ((XmlElement)Receptor[0]).GetElementsByTagName("Contacto")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("DirRecep")[0]))
            {
                Factura.DirRecep = ((XmlElement)Receptor[0]).GetElementsByTagName("DirRecep")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("CmnaRecep")[0]))
            {
                Factura.CmnaRecep = ((XmlElement)Receptor[0]).GetElementsByTagName("CmnaRecep")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("CiudadRecep")[0]))
            {
                Factura.CiudadRecep = ((XmlElement)Receptor[0]).GetElementsByTagName("CiudadRecep")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("DirPostal")[0]))
            {
                Factura.DirPostal = ((XmlElement)Receptor[0]).GetElementsByTagName("DirPostal")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("CmnaPostal")[0]))
            {
                Factura.CmnaPostal = ((XmlElement)Receptor[0]).GetElementsByTagName("CmnaPostal")[0].InnerText;
            }

            if (!Object.ReferenceEquals(null, ((XmlElement)Receptor[0]).GetElementsByTagName("CiudadPostal")[0]))
            {
                Factura.CiudadPostal = ((XmlElement)Receptor[0]).GetElementsByTagName("CiudadPostal")[0].InnerText;
            }

            Factura.MntNeto= ((XmlElement)Totales[0]).GetElementsByTagName("MntNeto")[0].InnerText;

            if (!Object.ReferenceEquals(null, ((XmlElement)Totales[0]).GetElementsByTagName("MntExe")[0]))
            {
                Factura.MntExe = ((XmlElement)Totales[0]).GetElementsByTagName("MntExe")[0].InnerText;
            }
            Factura.TasaIVA = ((XmlElement)Totales[0]).GetElementsByTagName("TasaIVA")[0].InnerText;
            Factura.IVA = ((XmlElement)Totales[0]).GetElementsByTagName("IVA")[0].InnerText;
            Factura.MntTotal = ((XmlElement)Totales[0]).GetElementsByTagName("MntTotal")[0].InnerText;

            
            Factura.FchResol= ((XmlElement)Caratula[0]).GetElementsByTagName("FchResol")[0].InnerText;
            Factura.NroResol = ((XmlElement)Caratula[0]).GetElementsByTagName("NroResol")[0].InnerText;
           
            List<detalleFactura> listDetalleFactura = new List<detalleFactura>();
            foreach (XmlElement detalle in Detalles)
            {

                detalleFactura detalleFactura = new detalleFactura();
                detalleFactura.NroLinDet = detalle.GetElementsByTagName("NroLinDet")[0].InnerText;

                XmlNodeList CdgItem = detalle.GetElementsByTagName("CdgItem");
                if (!Object.ReferenceEquals(null, (XmlElement)CdgItem[0])) {
                    detalleFactura.TpoCodigo = ((XmlElement)CdgItem[0]).GetElementsByTagName("TpoCodigo")[0].InnerText;
                    detalleFactura.VlrCodigo = ((XmlElement)CdgItem[0]).GetElementsByTagName("VlrCodigo")[0].InnerText;
                }

                detalleFactura.NmbItem = detalle.GetElementsByTagName("NmbItem")[0].InnerText;
                detalleFactura.DscItem = detalle.GetElementsByTagName("DscItem")[0].InnerText;                
                detalleFactura.MontoItem = detalle.GetElementsByTagName("MontoItem")[0].InnerText;

                if (!Object.ReferenceEquals(null, detalle.GetElementsByTagName("QtyItem")[0])) {
                    detalleFactura.QtyItem = detalle.GetElementsByTagName("QtyItem")[0].InnerText;
                }

                if (!Object.ReferenceEquals(null, detalle.GetElementsByTagName("PrcItem")[0])) {
                    detalleFactura.PrcItem = detalle.GetElementsByTagName("PrcItem")[0].InnerText;
                }

                if (!Object.ReferenceEquals(null, detalle.GetElementsByTagName("DescuentoPct")[0]))
                {
                    detalleFactura.DescuentoPct = detalle.GetElementsByTagName("DescuentoPct")[0].InnerText;
                }

                if (!Object.ReferenceEquals(null, detalle.GetElementsByTagName("DescuentoMonto")[0]))
                {
                    detalleFactura.DescuentoMonto = detalle.GetElementsByTagName("DescuentoMonto")[0].InnerText;
                }

                listDetalleFactura.Add(detalleFactura);
            }
            Factura.detalleFactura = listDetalleFactura;
            return Factura;
        }
    }
}
