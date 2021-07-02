using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{
    public class MercadoLivreNotaFiscal
    {
        public string id { get; set; }
        public string status { get; set; }
        public string transaction_status { get; set; }
        public Issuer issuer { get; set; }
        public Recipient recipient { get; set; }
        public Shipment shipment { get; set; }
        public Item[] items { get; set; }
        public DateTime issued_date { get; set; }
        public string invoice_series { get; set; }
        public int invoice_number { get; set; }
        public Attributes attributes { get; set; }
        public Fiscal_Data fiscal_data { get; set; }
        public int amount { get; set; }
        public int items_amount { get; set; }
        public object[] errors { get; set; }
        public int items_quantity { get; set; }
    }

    public class Issuer
    {
        public string name { get; set; }
        public Identifications identifications { get; set; }
        public Phone phone { get; set; }
        public Address address { get; set; }
        public string user_id { get; set; }
        public string brand_name { get; set; }
    }

    public class Identifications
    {
        public string cnpj { get; set; }
        public string crt { get; set; }
        public string ie { get; set; }
        public string ie_type { get; set; }
    }



    public class Recipient
    {
        public string name { get; set; }
        public Identifications1 identifications { get; set; }
        public Phone1 phone { get; set; }
        public Address1 address { get; set; }
        public string external_recipient_id { get; set; }
    }

    public class Identifications1
    {
        public string cpf { get; set; }
    }

    public class Phone1
    {
        public string area_code { get; set; }
        public string number { get; set; }
    }

    public class Address1
    {
        public string street_name { get; set; }
        public string street_number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }

    public class Shipment
    {
        public string id { get; set; }
        public string site_id { get; set; }
        public string mode { get; set; }
        public string logistic_type { get; set; }
        public int buyer_cost { get; set; }
        public string paid_by { get; set; }
        public Carrier carrier { get; set; }
        public Volume[] volumes { get; set; }
        public object fiscal_model_id { get; set; }
    }

    public class Carrier
    {
        public string name { get; set; }
        public Identifications2 identifications { get; set; }
        public Phone2 phone { get; set; }
        public Address2 address { get; set; }
    }

    public class Identifications2
    {
        public string cnpj { get; set; }
        public object crt { get; set; }
        public string ie { get; set; }
        public string ie_type { get; set; }
    }

    public class Phone2
    {
        public string area_code { get; set; }
        public string number { get; set; }
    }

    public class Address2
    {
        public string street_name { get; set; }
        public string street_number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }

    public class Volume
    {
        public float net_weight { get; set; }
        public float gross_weight { get; set; }
    }

    public class Attributes
    {
        public string cnf { get; set; }
        public string order_source { get; set; }
        public string invoice_key { get; set; }
        public string environment_type { get; set; }
        public string xml_version { get; set; }
        public int status_code { get; set; }
        public string status_description { get; set; }
        public string receipt { get; set; }
        public DateTime receipt_date { get; set; }
        public DateTime invoice_creation_date { get; set; }
        public string protocol { get; set; }
        public string invoice_type { get; set; }
        public string emission_type { get; set; }
        public DateTime authorization_date { get; set; }
        public object cancellation_protocol { get; set; }
        public object cancellation_date { get; set; }
        public object cancellation_reason { get; set; }
        public object cancellation_error_code { get; set; }
        public object cancellation_error_description { get; set; }
        public object correction_letter { get; set; }
        public object reference_invoice { get; set; }
        public object reference_invoices { get; set; }
        public string danfe_location { get; set; }
        public string xml_location { get; set; }
        public bool include_freight { get; set; }
    }

    public class Fiscal_Data
    {
        public string customer_type { get; set; }
        public string transaction_type { get; set; }
        public string transaction_type_description { get; set; }
        public Message[] messages { get; set; }
        public Fiscal_Amounts[] fiscal_amounts { get; set; }
    }

    public class Message
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class Fiscal_Amounts
    {
        public string name { get; set; }
        public Attributes1 attributes { get; set; }
    }

    public class Attributes1
    {
        public int vpis { get; set; }
        public float vtottrib { get; set; }
        public int vbcst { get; set; }
        public int vst { get; set; }
        public int vicms { get; set; }
        public int vbc { get; set; }
        public int vicmsdeson { get; set; }
        public int vcofins { get; set; }
        public int amount { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string invoice_id { get; set; }
        public string seller_id { get; set; }
        public string external_order_id { get; set; }
        public string external_product_id { get; set; }
        public string external_variant_id { get; set; }
        public Attributes2 attributes { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public int total_amount { get; set; }
        public int shipping_buyer_cost { get; set; }
        public object discount_amount { get; set; }
        public Fiscal_Data1 fiscal_data { get; set; }
    }

    public class Attributes2
    {
        public object ean { get; set; }
        public string sku { get; set; }
        public object type { get; set; }
    }

    public class Fiscal_Data1
    {
        public Attributes3 attributes { get; set; }
        public Message1[] messages { get; set; }
        public Rule[] rules { get; set; }
    }

    public class Attributes3
    {
        public string ncm { get; set; }
        public object cest { get; set; }
        public string origin_type { get; set; }
        public string origin_detail { get; set; }
        public string cfop { get; set; }
        public string measurement_unit { get; set; }
    }

    public class Message1
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class Rule
    {
        public string name { get; set; }
        public Attributes4 attributes { get; set; }
    }

    public class Attributes4
    {
        public int municipal_tax { get; set; }
        public float vibpt { get; set; }
        public float pibpt { get; set; }
        public float federal_national_tax { get; set; }
        public object messages { get; set; }
        public float federal_imported_tax { get; set; }
        public int state_tax { get; set; }
        public string csosn { get; set; }
        public int vcofins { get; set; }
        public int pcofins { get; set; }
        public string cst { get; set; }
        public int vbc { get; set; }
        public int vpis { get; set; }
        public int ppis { get; set; }
    }

}
