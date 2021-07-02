using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MagazineLuiza
{

    public class MAGALUCategoria
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public Category[] Categories { get; set; }
        public Categoriesmarketplace[] CategoriesMarketplace { get; set; }
    }

    public class Categoriesmarketplace
    {
        public string MarketPlaceName { get; set; }
        public Categorymarketplacelist CategoryMarketplaceList { get; set; }
    }

    public class Categorymarketplacelist
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }
        public int SubFamilyId { get; set; }
        public string SubFamilyName { get; set; }
    }


}
