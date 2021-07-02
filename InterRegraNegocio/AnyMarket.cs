using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio
{
    public class AnyMarket
    {
        public Link[] links { get; set; }
        public Content[] content { get; set; }
        public Page page { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Content
    {
        public int CodigoCliente { get; set; }
        public string TokenCliente { get; set; }

        public string UsuarioMAGALU { get; set; }
        public string SenhaMAGALU { get; set; }

        public string NomeRemetente { get; set; }
        public string EnderecoRemetente { get; set; }
        public string NumeroRemetente { get; set; }
        public string BairroRemetente { get; set; }
        public string CEPRemetente { get; set; }
        public string CidadeRemetente { get; set; }
        public string UFRemetente { get; set; }


        public int id { get; set; }
        public string accountName { get; set; }
        public string marketPlaceId { get; set; }
        public string marketPlaceNumber { get; set; }
        public string partnerId { get; set; }
        public string marketPlace { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime paymentDate { get; set; }
        public string transmissionStatus { get; set; }
        public string status { get; set; }
        public string marketPlaceUrl { get; set; }
        public string marketPlaceShipmentStatus { get; set; }
        public Invoice invoice { get; set; }
        public string marketPlaceStatus { get; set; }
        public float discount { get; set; }
        public float freight { get; set; }
        public float sellerFreight { get; set; }
        public float interestValue { get; set; }
        public float gross { get; set; }
        public float total { get; set; }
        public Shipping shipping { get; set; }
        public Anymarketaddress anymarketAddress { get; set; }
        public Buyer buyer { get; set; }
        public Payment[] payments { get; set; }
        public Item[] items { get; set; }
        public string deliverStatus { get; set; }
        public int idAccount { get; set; }
        public bool fulfillment { get; set; }
        public Tracking tracking { get; set; }
        public Billingaddress billingAddress { get; set; }
        public string subChannel { get; set; }
        public string subChannelNormalized { get; set; }
        public string marketPlaceStatusComplement { get; set; }
        public string shippingOptionId { get; set; }
        public string observation { get; set; }
    }

    public class Invoice
    {
        public string accessKey { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        public DateTime date { get; set; }
        public string cfop { get; set; }
        public string companyStateTaxId { get; set; }
    }

    public class Shipping
    {
        public string city { get; set; }
        public string state { get; set; }
        public string stateNameNormalized { get; set; }
        public string country { get; set; }
        public string countryAcronymNormalized { get; set; }
        public string countryNameNormalized { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string comment { get; set; }
        public string zipCode { get; set; }
        public string receiverName { get; set; }
        public DateTime promisedShippingTime { get; set; }
        public string neighborhood { get; set; }
        public string reference { get; set; }
        public string receiverPhone { get; set; }
    }

    public class Anymarketaddress
    {
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string comment { get; set; }
        public string receiverName { get; set; }
        public DateTime promisedShippingTime { get; set; }
        public string neighborhood { get; set; }
        public string reference { get; set; }
    }

    public class Buyer
    {
        public string marketPlaceId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string document { get; set; }
        public string documentType { get; set; }
        public string phone { get; set; }
        public string documentNumberNormalized { get; set; }
        public string cellPhone { get; set; }
    }

    public class Tracking
    {
        public string number { get; set; }
        public string carrier { get; set; }
        public DateTime date { get; set; }
        public DateTime shippedDate { get; set; }
        public string url { get; set; }
    }

    public class Billingaddress
    {
        public string city { get; set; }
        public string state { get; set; }
        public string stateNameNormalized { get; set; }
        public string number { get; set; }
        public string neighborhood { get; set; }
        public string street { get; set; }
        public string comment { get; set; }
        public string reference { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }
        public string countryAcronymNormalized { get; set; }
        public string countryNameNormalized { get; set; }
    }

    public class PaymentAnyMarket
    {
        public string method { get; set; }
        public string status { get; set; }
        public float value { get; set; }
        public int installments { get; set; }
        public string marketplaceId { get; set; }
        public string paymentMethodNormalized { get; set; }
        public string paymentDetailNormalized { get; set; }
        public float gatewayFee { get; set; }
        public float marketplaceFee { get; set; }
    }

    public class Item
    {
        public Product product { get; set; }
        public Sku sku { get; set; }
        public int amount { get; set; }
        public float unit { get; set; }
        public float gross { get; set; }
        public float total { get; set; }
        public float discount { get; set; }
        public Shipping1[] shippings { get; set; }
        public string idInMarketPlace { get; set; }
        public string marketPlaceId { get; set; }
        public int orderItemId { get; set; }
        public string officialStoreId { get; set; }
        public string officialStoreName { get; set; }
        public string listingType { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Sku
    {
        public int id { get; set; }
        public string title { get; set; }
        public string partnerId { get; set; }
    }

    public class Shipping1
    {
        public int id { get; set; }
        public string shippingtype { get; set; }
        public string shippingCarrierNormalized { get; set; }
        public string shippingCarrierTypeNormalized { get; set; }
    }


}
