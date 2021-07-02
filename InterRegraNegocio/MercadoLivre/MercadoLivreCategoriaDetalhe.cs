using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.MercadoLivre
{
    public class MercadoLivreCategoriaDetalhe
    {
        public string id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string permalink { get; set; }
        public int? total_items_in_this_category { get; set; }
        public Path_From_Root[] path_from_root { get; set; }
        public Children_Categories[] children_categories { get; set; }
        public string attribute_types { get; set; }
        public Settings settings { get; set; }
        public object meta_categ_id { get; set; }
        public bool attributable { get; set; }
    }

    public class Settings
    {
        public bool adult_content { get; set; }
        public bool buying_allowed { get; set; }
        public string[] buying_modes { get; set; }
        public string catalog_domain { get; set; }
        public string coverage_areas { get; set; }
        public string[] currencies { get; set; }
        public bool fragile { get; set; }
        public string immediate_payment { get; set; }
        public string[] item_conditions { get; set; }
        public bool items_reviews_allowed { get; set; }
        public bool listing_allowed { get; set; }
        public int? max_description_length { get; set; }
        public int? max_pictures_per_item { get; set; }
        public int? max_pictures_per_item_var { get; set; }
        public int? max_sub_title_length { get; set; }
        public int? max_title_length { get; set; }
        public object maximum_price { get; set; }
        public int? minimum_price { get; set; }
        public object mirror_category { get; set; }
        public object mirror_master_category { get; set; }
        public object[] mirror_slave_categories { get; set; }
        public string price { get; set; }
        public string reservation_allowed { get; set; }
        public object[] restrictions { get; set; }
        public bool rounded_address { get; set; }
        public string seller_contact { get; set; }
        public string[] shipping_modes { get; set; }
        public string[] shipping_options { get; set; }
        public string shipping_profile { get; set; }
        public bool show_contact_information { get; set; }
        public string simple_shipping { get; set; }
        public string stock { get; set; }
        public object sub_vertical { get; set; }
        public bool subscribable { get; set; }
        public object[] tags { get; set; }
        public object vertical { get; set; }
        public string vip_subdomain { get; set; }
        public string[] buyer_protection_programs { get; set; }
    }

    public class Path_From_Root
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Children_Categories
    {
        public string id { get; set; }
        public string name { get; set; }
        public int? total_items_in_this_category { get; set; }
    }

}
