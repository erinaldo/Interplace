using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.B2W
{


    public class B2WPedidos
    {
        public int? total { get; set; }
        public B2WPedido[] orders { get; set; }
    }

    public class B2WPedido
    {
        public string code { get; set; }
        public float? shipping_cost { get; set; }
        public float? seller_shipping_cost { get; set; }
        public string estimated_delivery_shift { get; set; }
        public float? total_ordered { get; set; }
        public string channel { get; set; }
        public Billing_Address billing_address { get; set; }
        public DateTime? placed_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? imported_at { get; set; }
        public DateTime? exported_at { get; set; }
        public string shipping_carrier { get; set; }
        public string shipping_estimate_id { get; set; }
        public Shipment[] shipments { get; set; }
        public string[] tags { get; set; }
        public string calculation_type { get; set; }
        public string shipping_method_id { get; set; }
        public Invoice[] invoices { get; set; }
        public float? interest { get; set; }
        public string shipping_method { get; set; }
        public string sync_status { get; set; }
        public Payment[] payment { get; set; }
        public DateTime? expedition_limit_date { get; set; }
        public DateTime? estimated_delivery { get; set; }
        public Shipping_Address shipping_address { get; set; }
        public Item1[] items { get; set; }
        public Customer customer { get; set; }
        public Status status { get; set; }
        public DateTime? delivered_date { get; set; }
        public bool available_to_sync { get; set; }
    }

    public class Billing_Address
    {
        public string country { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string reference { get; set; }
        public string number { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public string secondary_phone { get; set; }
        public string street { get; set; }
        public string detail { get; set; }
        public string neighborhood { get; set; }
        public string complement { get; set; }
        public string region { get; set; }
    }

    public class Shipping_Address
    {
        public string country { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string reference { get; set; }
        public string number { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public string secondary_phone { get; set; }
        public string street { get; set; }
        public string detail { get; set; }
        public string neighborhood { get; set; }
        public string complement { get; set; }
        public string region { get; set; }
    }

    public class Customer
    {
        public string state_registration { get; set; }
        public string tax_payer { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string vat_number { get; set; }
        public string name { get; set; }
        public string[] phones { get; set; }
        public string email { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string label { get; set; }
        public string type { get; set; }
    }

    public class Shipment
    {
        public string code { get; set; }
        public DateTime? delivered_carrier_date { get; set; }
        public Item[] items { get; set; }
        public Track[] tracks { get; set; }
    }

    public class Item
    {
        public int? qty { get; set; }
        public string sku { get; set; }
    }

    public class Track
    {
        public string carrier { get; set; }
        public string code { get; set; }
        public string method { get; set; }
        public string url { get; set; }
    }

    public class Invoice
    {
        public string number { get; set; }
        public DateTime? issue_date { get; set; }
        public string line { get; set; }
        public string key { get; set; }
    }



    public class Payment
    {
        public DateTime? transaction_date { get; set; }
        public string autorization_id { get; set; }
        public string card_issuer { get; set; }
        public string method { get; set; }
        public string description { get; set; }
        public Sefaz sefaz { get; set; }
        public float? value { get; set; }
        public int? parcels { get; set; }
        public string status { get; set; }
    }

    public class Sefaz
    {
        public string id_payment { get; set; }
        public string payment_indicator { get; set; }
        public string id_card_issuer { get; set; }
        public string name_card_issuer { get; set; }
        public string name_payment { get; set; }
        public string type_integration { get; set; }
    }

    public class Item1
    {
        public float? original_price { get; set; }
        public float? special_price { get; set; }
        public float? shipping_cost { get; set; }
        public string product_id { get; set; }
        public int? qty { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }


}
