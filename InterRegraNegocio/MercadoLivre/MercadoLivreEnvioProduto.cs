using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{
    public class MercadoLivreEnvioProduto
    {
        public string title { get; set; }
        public string category_id { get; set; }
        public float? price { get; set; }
        public string currency_id { get; set; }
        public int? available_quantity { get; set; }
        public string buying_mode { get; set; }
        public string listing_type_id { get; set; }
        public string condition { get; set; }
        public string description { get; set; }
        public string video_id { get; set; }
        public string warranty { get; set; }
        public List<Picture> pictures { get; set; }
    }

    public class Picture
    {
        public string source { get; set; }
    }


}
