using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.MercadoLivre
{
    public class MercadoLivreAtualizaTituloProduto
    {
        public string title { get; set; }
    }
    public class MercadoLivreAtualizaEstoqueProduto
    {
        public string available_quantity { get; set; }
    }
    public class MercadoLivreAtualizaPrecoProduto
    {
        public float? price { get; set; }
    }
}
