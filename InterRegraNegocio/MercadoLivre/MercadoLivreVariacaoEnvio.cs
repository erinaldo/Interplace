using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{

    public class VariationsVariacaoEnvio
    {
        public VariationVariacaoEnvio[] variations { get; set; }
    }

    public class VariationVariacaoEnvio
    {
        public long id { get; set; }
        public double available_quantity { get; set; }
    }

}
