using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.MercadoLivre
{
    public class MercadoLivreProduto
    {
        public string id { get; set; }
        public string site_id { get; set; }
        public string title { get; set; }
        public object subtitle { get; set; }
        public int? seller_id { get; set; }
        public string category_id { get; set; }
        public object official_store_id { get; set; }
        public float? price { get; set; }
        public float? base_price { get; set; }
        public object original_price { get; set; }
        public object inventory_id { get; set; }
        public string currency_id { get; set; }
        public int? initial_quantity { get; set; }
        public int? available_quantity { get; set; }
        public int? sold_quantity { get; set; }
        public object[] sale_terms { get; set; }
        public string buying_mode { get; set; }
        public string listing_type_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime stop_time { get; set; }
        public DateTime end_time { get; set; }
        public DateTime expiration_time { get; set; }
        public string condition { get; set; }
        public string permalink { get; set; }
        public PictureProduto[] pictures { get; set; }
        public object video_id { get; set; }
        public object[] descriptions { get; set; }
        public bool accepts_mercadopago { get; set; }
        public object[] non_mercado_pago_payment_methods { get; set; }
        public Shipping shipping { get; set; }
        public string international_delivery_mode { get; set; }
        public Seller_Address seller_address { get; set; }
        public object seller_contact { get; set; }
        public Location location { get; set; }
        public GeolocationProduto geolocation { get; set; }
        public object[] coverage_areas { get; set; }
        public Attribute[] attributes { get; set; }
        public object[] warnings { get; set; }
        public string listing_source { get; set; }
        public object[] variations { get; set; }
        public string thumbnail { get; set; }
        public string secure_thumbnail { get; set; }
        public string status { get; set; }
        public object[] sub_status { get; set; }
        public string[] tags { get; set; }
        public object warranty { get; set; }
        public object catalog_product_id { get; set; }
        public object domain_id { get; set; }
        public object seller_custom_field { get; set; }
        public object parent_item_id { get; set; }
        public object differential_pricing { get; set; }
        public object[] deal_ids { get; set; }
        public bool automatic_relist { get; set; }
        public DateTime date_created { get; set; }
        public DateTime last_updated { get; set; }
        public object health { get; set; }
        public bool catalog_listing { get; set; }
        public object[] item_relations { get; set; }
    }

    public class ShippingProduto
    {
        public string mode { get; set; }
        public bool local_pick_up { get; set; }
        public bool free_shipping { get; set; }
        public object[] methods { get; set; }
        public object dimensions { get; set; }
        public object[] tags { get; set; }
        public string logistic_type { get; set; }
        public bool store_pick_up { get; set; }
    }

    public class Seller_Address
    {
        public int? id { get; set; }
        public string comment { get; set; }
        public string address_line { get; set; }
        public string zip_code { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public Search_Location search_location { get; set; }
    }

    public class CityProduto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class StateProduto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class CountryProduto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Search_Location
    {
        public Neighborhood neighborhood { get; set; }
        public City1 city { get; set; }
        public State1 state { get; set; }
    }




    public class Location
    {
    }

    public class GeolocationProduto
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class PictureProduto
    {
        public string id { get; set; }
        public string url { get; set; }
        public string secure_url { get; set; }
        public string size { get; set; }
        public string max_size { get; set; }
        public string quality { get; set; }
    }

    public class AttributeProduto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public object value_struct { get; set; }
        public ValueProduto[] values { get; set; }
        public string attribute_group_id { get; set; }
        public string attribute_group_name { get; set; }
    }

    public class ValueProduto
    {
        public string id { get; set; }
        public string name { get; set; }
        public object _struct { get; set; }
    }


}
