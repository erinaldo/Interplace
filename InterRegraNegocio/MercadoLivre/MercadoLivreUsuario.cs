using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{

    public class MercadoLivreUsuario
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public DateTime registration_date { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string country_id { get; set; }
        public string email { get; set; }
        public Identification identification { get; set; }
        public string[] internal_tags { get; set; }
        public Address address { get; set; }
        public Phone phone { get; set; }
        public Alternative_Phone alternative_phone { get; set; }
        public string user_type { get; set; }
        public string[] tags { get; set; }
        public object logo { get; set; }
        public int points { get; set; }
        public string site_id { get; set; }
        public string permalink { get; set; }
        public string[] shipping_modes { get; set; }
        public string seller_experience { get; set; }
        public Bill_Data bill_data { get; set; }
        public Seller_Reputation seller_reputation { get; set; }
        public Buyer_Reputation buyer_reputation { get; set; }
        public Status status { get; set; }
        public string secure_email { get; set; }
        public Company company { get; set; }
        public Credit credit { get; set; }
        public Context context { get; set; }
        public Thumbnail thumbnail { get; set; }
    }

    public class Identification
    {
        public string number { get; set; }
        public string type { get; set; }
    }

    public class Address
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
    }

    public class Phone
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
        public bool verified { get; set; }
    }

    public class Alternative_Phone
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
    }

    public class Bill_Data
    {
        public string accept_credit_note { get; set; }
    }

    public class Seller_Reputation
    {
        public object level_id { get; set; }
        public object power_seller_status { get; set; }
        public Transactions transactions { get; set; }
        public Metrics metrics { get; set; }
    }

    public class Transactions
    {
        public int canceled { get; set; }
        public int completed { get; set; }
        public string period { get; set; }
        public Ratings ratings { get; set; }
        public int total { get; set; }
    }

    public class Ratings
    {
        public double negative { get; set; }
        public double neutral { get; set; }
        public double positive { get; set; }
    }

    public class Metrics
    {
        public Sales sales { get; set; }
        public Claims claims { get; set; }
        public Delayed_Handling_Time delayed_handling_time { get; set; }
        public Cancellations cancellations { get; set; }
    }

    public class Sales
    {
        public string period { get; set; }
        public int completed { get; set; }
    }

    public class Claims
    {
        public string period { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
    }

    public class Delayed_Handling_Time
    {
        public string period { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
    }

    public class Cancellations
    {
        public string period { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
    }

    public class Buyer_Reputation
    {
        public int canceled_transactions { get; set; }
        public object[] tags { get; set; }
        public Transactions1 transactions { get; set; }
    }

    public class Transactions1
    {
        public Canceled canceled { get; set; }
        public object completed { get; set; }
        public Not_Yet_Rated not_yet_rated { get; set; }
        public string period { get; set; }
        public object total { get; set; }
        public Unrated unrated { get; set; }
    }

    public class Canceled
    {
        public object paid { get; set; }
        public object total { get; set; }
    }

    public class Not_Yet_Rated
    {
        public object paid { get; set; }
        public object total { get; set; }
        public object units { get; set; }
    }

    public class Unrated
    {
        public object paid { get; set; }
        public object total { get; set; }
    }

    public class Status
    {
        public Billing billing { get; set; }
        public Buy buy { get; set; }
        public bool confirmed_email { get; set; }
        public Shopping_Cart shopping_cart { get; set; }
        public bool immediate_payment { get; set; }
        public List list { get; set; }
        public string mercadoenvios { get; set; }
        public string mercadopago_account_type { get; set; }
        public bool mercadopago_tc_accepted { get; set; }
        public string required_action { get; set; }
        public Sell sell { get; set; }
        public string site_status { get; set; }
        public string user_type { get; set; }
    }

    public class Billing
    {
        public bool allow { get; set; }
        public object[] codes { get; set; }
    }

    public class Buy
    {
        public bool allow { get; set; }
        public object[] codes { get; set; }
        public Immediate_Payment immediate_payment { get; set; }
    }

    public class Immediate_Payment
    {
        public object[] reasons { get; set; }
        public bool required { get; set; }
    }

    public class Shopping_Cart
    {
        public string buy { get; set; }
        public string sell { get; set; }
    }

    public class List
    {
        public bool allow { get; set; }
        public object[] codes { get; set; }
        public Immediate_Payment1 immediate_payment { get; set; }
    }

    public class Immediate_Payment1
    {
        public object[] reasons { get; set; }
        public bool required { get; set; }
    }

    public class Sell
    {
        public bool allow { get; set; }
        public object[] codes { get; set; }
        public Immediate_Payment2 immediate_payment { get; set; }
    }

    public class Immediate_Payment2
    {
        public object[] reasons { get; set; }
        public bool required { get; set; }
    }

    public class Company
    {
        public string brand_name { get; set; }
        public object city_tax_id { get; set; }
        public string corporate_name { get; set; }
        public string identification { get; set; }
        public string state_tax_id { get; set; }
        public string soft_descriptor { get; set; }
    }

    public class Credit
    {
        public double consumed { get; set; }
        public string credit_level_id { get; set; }
        public string rank { get; set; }
    }

    public class Context
    {
    }

    public class Thumbnail
    {
        public string picture_id { get; set; }
        public string picture_url { get; set; }
    }


}
