using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.B2W
{
    public class B2WProdutoEstoquepreco
    {
        public ProductPostEstoquepreco productEstoquePreco { get; set; }
    }

    public class ProductPostEstoquepreco
    {
        public float? price { get; set; }
        public float? promotional_price { get; set; }
        public float? qty { get; set; }

    }

    public class B2WProdutoPost
    {
        public ProductPost product { get; set; }
    }

    public class ProductPost
    {
        public string[] images { get; set; }
        public float? cost { get; set; }
        public string[] variation_attributes { get; set; }
        public float? length { get; set; }
        public string description { get; set; }
        public float? weight { get; set; }
        public SpecificationPost[] specifications { get; set; }
        public string ean { get; set; }
        public float? price { get; set; }
        public VariationPost[] variations { get; set; }
        public int? qty { get; set; }
        public string name { get; set; }
        public string nbm { get; set; }
        public float? width { get; set; }
        public float? promotional_price { get; set; }
        public CategoryPost[] categories { get; set; }
        public string sku { get; set; }
        public string brand { get; set; }
        public string status { get; set; }
        public float? height { get; set; }
    }

    public class SpecificationPost
    {
        public string value { get; set; }
        public string key { get; set; }
    }

    public class VariationPost
    {
        public string[] images { get; set; }
        public string ean { get; set; }
        public string qty { get; set; }
        public string sku { get; set; }
        public Specification1[] specifications { get; set; }
    }

    public class Specification1
    {
        public string value { get; set; }
        public string key { get; set; }
    }

    public class CategoryPost
    {
        public string code { get; set; }
        public string name { get; set; }
    }

}
