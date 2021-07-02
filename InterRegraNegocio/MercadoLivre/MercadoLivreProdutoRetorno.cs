using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.MercadoLivre
{




    public class MercadoLivreProdutoRetorno
    {
        public string id { get; set; }
        public string site_id { get; set; }
        public string title { get; set; }
        public object subtitle { get; set; }
        public int? seller_id { get; set; }
        public string category_id { get; set; }
        public int? official_store_id { get; set; }
        public double? price { get; set; }
        public double? base_price { get; set; }
        public object original_price { get; set; }
        public string currency_id { get; set; }
        public int? initial_quantity { get; set; }
        public int? available_quantity { get; set; }
        public int? sold_quantity { get; set; }
        public Sale_Terms[] sale_terms { get; set; }
        public string buying_mode { get; set; }
        public string listing_type_id { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? stop_time { get; set; }
        public string condition { get; set; }
        public string permalink { get; set; }
        public string thumbnail { get; set; }
        public string secure_thumbnail { get; set; }
        public PictureProdutoRetorno[] pictures { get; set; }
        public object video_id { get; set; }
        public Description[] descriptions { get; set; }
        public bool accepts_mercadopago { get; set; }
        public object[] non_mercado_pago_payment_methods { get; set; }
        public ShippingProdutoRetorno ShippingProdutoRetorno { get; set; }
        public string international_delivery_mode { get; set; }
        public Seller_AddressProdutoRetorno Seller_AddressProdutoRetorno { get; set; }
        public object seller_contact { get; set; }
        public LocationProdutoRetorno LocationProdutoRetorno { get; set; }
        public GeoLocationProdutoRetorno geoLocationProdutoRetorno { get; set; }
        public object[] coverage_areas { get; set; }
        public Attribute[] attributes { get; set; }
        public object[] warnings { get; set; }
        public string listing_source { get; set; }
        public VariationProdutoRetorno[] variations { get; set; }
        public string status { get; set; }
        public object[] sub_status { get; set; }
        public string[] tags { get; set; }
        public string warranty { get; set; }
        public string catalog_product_id { get; set; }
        public string domain_id { get; set; }
        public object parent_item_id { get; set; }
        public object differential_pricing { get; set; }
        public string[] deal_ids { get; set; }
        public bool automatic_relist { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? last_updated { get; set; }
        public double? health { get; set; }
        public bool catalog_listing { get; set; }
    }

    public class ShippingProdutoRetorno
    {
        public string mode { get; set; }
        public Free_Methods[] free_methods { get; set; }
        public string[] tags { get; set; }
        public object dimensions { get; set; }
        public bool local_pick_up { get; set; }
        public bool free_ShippingProdutoRetorno { get; set; }
        public string logistic_type { get; set; }
        public bool store_pick_up { get; set; }
    }

    public class Free_Methods
    {
        public int? id { get; set; }
        public Rule rule { get; set; }
    }

    public class Rule
    {
        public bool _default { get; set; }
        public string free_mode { get; set; }
        public bool free_ShippingProdutoRetorno_flag { get; set; }
        public object value { get; set; }
    }

    public class Seller_AddressProdutoRetorno
    {
        public CityProdutoRetorno CityProdutoRetorno { get; set; }
        public StateProdutoRetorno StateProdutoRetorno { get; set; }
        public CountryProdutoRetorno CountryProdutoRetorno { get; set; }
        public Search_LocationProdutoRetornoProdutoRetorno Search_LocationProdutoRetornoProdutoRetorno { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public int? id { get; set; }
    }

    public class CityProdutoRetorno
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class StateProdutoRetorno
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class CountryProdutoRetorno
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Search_LocationProdutoRetornoProdutoRetorno
    {
        public NeighborhoodProdutoRetorno NeighborhoodProdutoRetorno { get; set; }
        public CityProdutoRetorno1 CityProdutoRetorno { get; set; }
        public StateProdutoRetorno1 StateProdutoRetorno { get; set; }
    }

    public class NeighborhoodProdutoRetorno
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class CityProdutoRetorno1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class StateProdutoRetorno1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class LocationProdutoRetorno
    {
    }

    public class GeoLocationProdutoRetorno
    {
        public float? latitude { get; set; }
        public float? longitude { get; set; }
    }

    public class Sale_Terms
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public Value_Struct value_struct { get; set; }
        public Value[] values { get; set; }
    }

    public class Value_Struct
    {
        public int? number { get; set; }
        public string unit { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public Struct _struct { get; set; }
    }

    public class Struct
    {
        public int? number { get; set; }
        public string unit { get; set; }
    }

    public class PictureProdutoRetorno
    {
        public string id { get; set; }
        public string url { get; set; }
        public string secure_url { get; set; }
        public string size { get; set; }
        public string max_size { get; set; }
        public string quality { get; set; }
    }

    public class Description
    {
        public string id { get; set; }
    }

    public class Attribute
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public Value_Struct1 value_struct { get; set; }
        public Value1[] values { get; set; }
        public string attribute_group_id { get; set; }
        public string attribute_group_name { get; set; }
    }

    public class Value_Struct1
    {
        public float? number { get; set; }
        public string unit { get; set; }
    }

    public class Value1
    {
        public string id { get; set; }
        public string name { get; set; }
        public Struct1 _struct { get; set; }
    }

    public class Struct1
    {
        public float? number { get; set; }
        public string unit { get; set; }
    }

    public class VariationProdutoRetorno
    {
        public long id { get; set; }
        public double? price { get; set; }
        public Attribute_Combinations[] attribute_combinations { get; set; }
        public double? available_quantity { get; set; }
        public double? sold_quantity { get; set; }
        public object[] sale_terms { get; set; }
        public string[] picture_ids { get; set; }
        public string catalog_product_id { get; set; }
    }

    public class Attribute_Combinations
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public object value_struct { get; set; }
        public Value2[] values { get; set; }
    }

    public class Value2
    {
        public string id { get; set; }
        public string name { get; set; }
        public object _struct { get; set; }
    }
}
