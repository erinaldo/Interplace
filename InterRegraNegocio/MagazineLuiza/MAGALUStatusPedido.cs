using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MagazineLuiza
{
    public class MAGALUPedidoProcessado
    {
        public string IdOrder { get; set; }
        public string OrderStatus { get; set; }
    }


    public class MAGALUPedidoFaturado
    {
        public string IdOrder { get; set; }
        public string OrderStatus { get; set; }
        public string InvoicedNumber { get; set; }
        public int InvoicedLine { get; set; }
        public DateTime? InvoicedIssueDate { get; set; }
        public string InvoicedKey { get; set; }
        public string InvoicedDanfeXml { get; set; }
    }



    public class MAGALUPedidoDespachado
    {
        public string IdOrder { get; set; }
        public string OrderStatus { get; set; }
        public string ShippedTrackingUrl { get; set; }
        public string ShippedTrackingProtocol { get; set; }
        public DateTime? ShippedEstimatedDelivery { get; set; }
        public DateTime? ShippedCarrierDate { get; set; }
        public string ShippedCarrierName { get; set; }
    }



    public class MAGALUPedidoEntregue
    {
        public string IdOrder { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }



}
