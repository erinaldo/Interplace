using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{


    public class MercadoLivreTipoAnuncios
    {
        public MercadoLivreTipoAnuncio[] Property1 { get; set; }
    }

    public class MercadoLivreTipoAnuncio
    {
        public string site_id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

  
}
