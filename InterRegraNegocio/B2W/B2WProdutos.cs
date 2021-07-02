using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.B2W
{

    public class ProdutosB2W
    {
        public Product[] products { get; set; }
        public int total { get; set; }
    }

    public class Product
    {
        public string sku { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public bool removed { get; set; }
        public float? qty { get; set; }
        public float? price { get; set; }
        public float? promotional_price { get; set; }
        public float cost { get; set; }
        public float weight { get; set; }
        public float height { get; set; }
        public float width { get; set; }
        public float length { get; set; }
        public string brand { get; set; }
        public string ean { get; set; }
        public string nbm { get; set; }
        public Category[] categories { get; set; }
        public string[] images { get; set; }
        public Specification[] specifications { get; set; }
        public Association[] associations { get; set; }

        public Variation[] variations { get; set; }

        public string[] variation_attributes { get; set; }



    }

    public class Category
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Specification
    {
        public string key { get; set; }
        public object value { get; set; }
    }

    public class Association
    {
        public string platform { get; set; }
        public string status { get; set; }
    }

    public class Produto
    {
        public string[] images { get; set; }
        public float? cost { get; set; }
        public string[] variation_attributes { get; set; }
        public float? length { get; set; }
        public string description { get; set; }
        public float? weight { get; set; }
        public Specification[] specifications { get; set; }
        public string ean { get; set; }
        public float? price { get; set; }
        public Variation[] variations { get; set; }
        public int? qty { get; set; }
        public string name { get; set; }
        public string nbm { get; set; }
        public float? width { get; set; }
        public float? promotional_price { get; set; }
        public Category[] categories { get; set; }
        public string sku { get; set; }
        public string brand { get; set; }
        public string status { get; set; }
        public float? height { get; set; }
    }

    public class Variation
    {
        public string[] images { get; set; }
        public string ean { get; set; }
        public string qty { get; set; }
        public string sku { get; set; }
        public Specification[] specifications { get; set; }
    }

    public class B2WProdutoAtualiza
    {
        public Produto product { get; set; }
    }
}
