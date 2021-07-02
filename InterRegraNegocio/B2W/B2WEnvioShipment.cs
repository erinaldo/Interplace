using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.B2W
{


    public class B2WEnvioShipment
    {
        public ShipmentEnvioShipment shipment { get; set; }
        public InvoiceEnvioShipment invoice { get; set; }
        public string status { get; set; }
        public DateTime estimated_delivery { get; set; }
    }

    public class ShipmentEnvioShipment
    {
        public string code { get; set; }
        public DateTime delivered_carrier_date { get; set; }
        public TrackEnvioShipment track { get; set; }
        public ItemEnvioShipment[] items { get; set; }
    }

    public class TrackEnvioShipment
    {
        public string carrier { get; set; }
        public string code { get; set; }
        public string method { get; set; }
        public string url { get; set; }
    }

    public class ItemEnvioShipment
    {
        public int qty { get; set; }
        public string sku { get; set; }
    }

    public class InvoiceEnvioShipment
    {
        public int volume_qty { get; set; }
        public string key { get; set; }
    }


}
