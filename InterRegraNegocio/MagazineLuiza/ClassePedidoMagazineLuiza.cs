using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio
{
    public class JsonPedidoMagalu
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }

        public Order[] Orders { get; set; }
    }

    public class Order
    {
        public int CodigoCliente { get; set; }
        public int IdQueue { get; set; }
        public string IdOrder { get; set; }
        public string IdOrderMarketplace { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime PurchasedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string MarketplaceName { get; set; }
        public string StoreName { get; set; }
        public bool? UpdatedMarketplaceStatus { get; set; }
        public bool? InsertedErp { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string CustomerPfCpf { get; set; }
        public string ReceiverName { get; set; }
        public string CustomerPfName { get; set; }
        public string CustomerPjCnpj { get; set; }
        public string CustomerPjCorporatename { get; set; }
        public string DeliveryAddressStreet { get; set; }
        public string DeliveryAddressAdditionalInfo { get; set; }
        public string DeliveryAddressZipcode { get; set; }
        public string DeliveryAddressNeighborhood { get; set; }
        public string DeliveryAddressCity { get; set; }
        public string DeliveryAddressReference { get; set; }
        public string DeliveryAddressState { get; set; }
        public string DeliveryAddressNumber { get; set; }
        public string TelephoneMainNumber { get; set; }
        public string TelephoneSecundaryNumber { get; set; }
        public string TelephoneBusinessNumber { get; set; }
        public string TotalAmount { get; set; }
        public string TotalTax { get; set; }
        public string TotalFreight { get; set; }
        public string TotalDiscount { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerBirthDate { get; set; }
        public string CustomerPjIe { get; set; }
        public string OrderStatus { get; set; }
        public string InvoicedNumber { get; set; }
        public int? InvoicedLine { get; set; }
        public DateTime? InvoicedIssueDate { get; set; }
        public string InvoicedKey { get; set; }
        public string InvoicedDanfeXml { get; set; }
        public string ShippedTrackingUrl { get; set; }
        public string ShippedTrackingProtocol { get; set; }
        public DateTime? ShippedEstimatedDelivery { get; set; }
        public DateTime? ShippedCarrierDate { get; set; }
        public string ShippedCarrierName { get; set; }
        public string ShipmentExceptionObservation { get; set; }
        public DateTime? ShipmentExceptionOccurrenceDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string ShippedCodeERP { get; set; }
        public ProductMagalu[] Products { get; set; }
        public Payment[] Payments { get; set; }
    }

    public class ProductMagalu
    {
        public string IdSku { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Freight { get; set; }
        public decimal? Discount { get; set; }
        public string IdOrderPackage { get; set; }
    }

    public class Payment
    {
        public string Name { get; set; }
        public int Installments { get; set; }
        public decimal? Amount { get; set; }
    }



    public class Rootobject
    {
        public EtiquetaMAGALU[] Property1 { get; set; }
    }

    public class EtiquetaMAGALU
    {
        public string Url { get; set; }
        public DateTime ExpiresOn { get; set; }
        public OrderMAGALU[] Orders { get; set; }
    }

    public class OrderMAGALU
    {
        public string Order { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingUrl { get; set; }
    }


    public class EnvioEtiquetaMAGALU
    {
        public string Format { get; set; }
        public string[] Orders { get; set; }
    }





}
