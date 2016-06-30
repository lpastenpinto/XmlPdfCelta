using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<detalleFactura> detalleFactura { set; get; }
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
