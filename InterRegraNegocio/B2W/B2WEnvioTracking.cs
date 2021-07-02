using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.B2W
{
    public class B2WEnvioTracking
    {
        public ShipmentEnvioTracking shipment { get; set; }
        public InvoiceEnvioTracking invoice { get; set; }
        public string status { get; set; }
        public DateTime? estimated_delivery { get; set; }
    }

    public class ShipmentEnvioTracking
    {
        public string code { get; set; }
        public DateTime? delivered_carrier_date { get; set; }
        public TrackEnvioTracking track { get; set; }
        public ItemEnvioTracking[] items { get; set; }
    }

    public class TrackEnvioTracking
    {
        public string carrier { get; set; }
        public string code { get; set; }
        public string method { get; set; }
        public string url { get; set; }
    }

    public class ItemEnvioTracking
    {
        public int? qty { get; set; }
        public string sku { get; set; }
    }

    public class InvoiceEnvioTracking
    {
        public int? volume_qty { get; set; }
        public string key { get; set; }
    }


}
