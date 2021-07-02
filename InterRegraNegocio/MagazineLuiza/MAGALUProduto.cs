using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MagazineLuiza
{

    public class MAGALUProduto
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public Product[] Products { get; set; }
    }

    public class Product
    {
        public string IdProduct { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string NbmOrigin { get; set; }
        public string NbmNumber { get; set; }
        public string WarrantyTime { get; set; }
        public bool Active { get; set; }
        public Category[] Categories { get; set; }
        public Marketplacestructure[] MarketplaceStructures { get; set; }
        public Attribute[] Attributes { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
    }

    public class Marketplacestructure
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SKUProduto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float? Height { get; set; }
        public float? Width { get; set; }
        public float? Length { get; set; }
        public float? Weight { get; set; }
        public bool Status { get; set; }
        public string Variation { get; set; }
        public string IdSku { get; set; }
        public string IdSkuErp { get; set; }
        public string IdProduct { get; set; }
        public string CodeEan { get; set; }
        public string CodeNcm { get; set; }
        public string CodeIsbn { get; set; }
        public string CodeNbm { get; set; }
        public Price Price { get; set; }
        public int StockQuantity { get; set; }
        public string MainImageUrl { get; set; }
        public string[] UrlImages { get; set; }
        public Attribute[] Attributes { get; set; }
    }

    public class Price
    {
        public float? ListPrice { get; set; }
        public float? SalePrice { get; set; }
    }


    public class MagaluEstoques
    {
        public MagaluEstoque[] Property1 { get; set; }
    }

    public class MagaluEstoque
    {
        public string IdSku { get; set; }
        public int Quantity { get; set; }
    }




}
