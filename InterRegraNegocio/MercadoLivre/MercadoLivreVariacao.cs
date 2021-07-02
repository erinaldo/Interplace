using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{
    public class MercadoLivreVariacao
    {
        public Variation[] variations { get; set; }
    }

    public class Variation
    {
        public long id { get; set; }
        public Attribute_Combinations[] attribute_combinations { get; set; }
        public int price { get; set; }
        public int available_quantity { get; set; }
        public int sold_quantity { get; set; }
        public string[] picture_ids { get; set; }
        public object seller_custom_field { get; set; }
        public object catalog_product_id { get; set; }
    }

    public class Attribute_Combinations
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
    }

}
