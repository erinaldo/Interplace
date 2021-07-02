using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{
    public class MercadoLivreCategoriasProdutos
    {
        public MercadoLivreCategoriaProduto[] Property1 { get; set; }
    }

    public class MercadoLivreCategoriaProduto
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
