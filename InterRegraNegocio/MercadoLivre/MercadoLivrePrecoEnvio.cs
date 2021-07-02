using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{

    public class MercadoLivrePrecoEnvios
    {
        public VariationPrecoEnvio[] variations { get; set; }
    }

    public class VariationPrecoEnvio
    {
        public long id { get; set; }
        public float? price { get; set; }
    }

}
