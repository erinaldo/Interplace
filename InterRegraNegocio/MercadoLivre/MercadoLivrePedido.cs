using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.MercadoLivre
{

    public class MercadoLivrePedido
    {
        public string query { get; set; }
        public Result[] results { get; set; }
        public Sort sort { get; set; }
        public Available_Sorts[] available_sorts { get; set; }
        public object[] filters { get; set; }
        public Paging paging { get; set; }
        public string display { get; set; }
    }

    public class Sort
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Paging
    {
        public int? total { get; set; }
        public int? offset { get; set; }
        public int? limit { get; set; }
    }

    public class Result
    {
        public long? id { get; set; }
        public object comments { get; set; }
        public string status { get; set; }
        public object status_detail { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_closed { get; set; }
        public DateTime? expiration_date { get; set; }
        public DateTime? date_last_updated { get; set; }
        public bool hidden_for_seller { get; set; }
        public string currency_id { get; set; }
        public Order_Items[] order_items { get; set; }
        public double? total_amount { get; set; }
        public object[] mediations { get; set; }
        public Payment[] payments { get; set; }
        public Order_Request order_request { get; set; }
        public object pickup_id { get; set; }
        public object pack_id { get; set; }
        public Buyer buyer { get; set; }
        public Seller seller { get; set; }
        public Shipping shipping { get; set; }
        public Feedback feedback { get; set; }
        public string[] tags { get; set; }
        public double DIFAL { get; set; }
    }

    public class Order_Request
    {
        public object change { get; set; }
        public object _return { get; set; }
    }

    public class Buyer
    {
        public int? id { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public PhonePedido phone { get; set; }
        public Alternative_Pedido alternative_phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public Billing_Info billing_info { get; set; }
    }

    public class PhonePedido
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
        public bool verified { get; set; }
    }

    public class Alternative_Pedido
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
    }

    public class Billing_Info
    {
        public string doc_type { get; set; }
        public string doc_number { get; set; }
    }

    public class Seller
    {
        public int? id { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public Phone1 phone { get; set; }
        public Alternative_Phone1 alternative_phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class Phone1
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
        public bool verified { get; set; }
    }

    public class Alternative_Phone1
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
    }

    public class Shipping
    {
        public object substatus { get; set; }
        public string status { get; set; }
        public long? id { get; set; }
        public object service_id { get; set; }
        public string currency_id { get; set; }
        public string shipping_mode { get; set; }
        public string shipment_type { get; set; }
        public int? sender_id { get; set; }
        public object picking_type { get; set; }
        public Receiver_Address receiver_address { get; set; }
        public DateTime? date_created { get; set; }
        public float? cost { get; set; }
        public object date_first_printed { get; set; }
        public Shipping_Option shipping_option { get; set; }
        public Shipping_Items[] shipping_items { get; set; }
    }

    public class Receiver_Address
    {
        public int? id { get; set; }
        public string zip_code { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public string street_number { get; set; }
        public string street_name { get; set; }
        public State state { get; set; }
        public string comment { get; set; }
        public string address_line { get; set; }
        public Country country { get; set; }
        public City city { get; set; }
        public Neighborhood neighborhood { get; set; }

    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Shipping_Option
    {
        public object id { get; set; }
        public object shipping_method_id { get; set; }
        public string name { get; set; }
        public string currency_id { get; set; }
        public float? cost { get; set; }
        public Speed speed { get; set; }
        public Estimated_Delivery estimated_delivery { get; set; }
    }

    public class Speed
    {
        public object shipping { get; set; }
        public object handling { get; set; }
    }

    public class Estimated_Delivery
    {
        public object date { get; set; }
        public object time_from { get; set; }
        public object time_to { get; set; }
    }

    public class Shipping_Items
    {
        public string id { get; set; }
        public string description { get; set; }
        public int? quantity { get; set; }
        public object dimensions { get; set; }
    }

    public class Feedback
    {
        public object sale { get; set; }
        public object purchase { get; set; }
    }

    public class Order_Items
    {
        public Item item { get; set; }
        public int? quantity { get; set; }
        public int? differential_pricing_id { get; set; }
        public float sale_fee { get; set; }
        public string listing_type_id { get; set; }
        public object base_currency_id { get; set; }
        public float unit_price { get; set; }
        public float full_unit_price { get; set; }
        public object base_exchange_rate { get; set; }
        public string currency_id { get; set; }
        public object manufacturing_days { get; set; }
    }

    public class Item
    {
        public object seller_custom_field { get; set; }
        public string condition { get; set; }
        public string category_id { get; set; }
        public object variation_id { get; set; }
        public object[] variation_attributes { get; set; }
        public object seller_sku { get; set; }
        public object warranty { get; set; }
        public string id { get; set; }
        public string title { get; set; }
    }

    public class Payment
    {
        public long? id { get; set; }
        public long? order_id { get; set; }
        public int? payer_id { get; set; }
        public Collector collector { get; set; }
        public string currency_id { get; set; }
        public string status { get; set; }
        public object status_code { get; set; }
        public string status_detail { get; set; }
        public float? transaction_amount { get; set; }
        public float? shipping_cost { get; set; }
        public float? overpaid_amount { get; set; }
        public float total_paid_amount { get; set; }
        public float marketplace_fee { get; set; }
        public float? coupon_amount { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_last_modified { get; set; }
        public long? card_id { get; set; }
        public string reason { get; set; }
        public object activation_uri { get; set; }
        public string payment_method_id { get; set; }
        public int? installments { get; set; }
        public string issuer_id { get; set; }
        public Atm_Transfer_Reference atm_transfer_reference { get; set; }
        public object coupon_id { get; set; }
        public string operation_type { get; set; }
        public string payment_type { get; set; }
        public string[] available_actions { get; set; }
        public float? installment_amount { get; set; }
        public object deferred_period { get; set; }
        public DateTime? date_approved { get; set; }
        public string authorization_code { get; set; }
        public object transaction_order_id { get; set; }
    }

    public class Collector
    {
        public int? id { get; set; }
    }

    public class Atm_Transfer_Reference
    {
        public object company_id { get; set; }
        public string transaction_id { get; set; }
    }

    public class Available_Sorts
    {
        public string id { get; set; }
        public string name { get; set; }
    }


}
