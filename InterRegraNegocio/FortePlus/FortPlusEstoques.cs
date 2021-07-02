using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{


    public class FortPlusEstoques
    {
        public FortPlusEstoque[] Property1 { get; set; }
    }

    public class FortPlusEstoque
    {
        public string id { get; set; }
        public string codigo { get; set; }
        public string nome { get; set; }
        public string variacao { get; set; }
        public string precoVenda { get; set; }
        public string qtdeAtual { get; set; }
        public string custo { get; set; }
        public string reserva { get; set; }
        public string estoqueAtual { get; set; }
        public string custoTotal { get; set; }
        public string custoTotalReal { get; set; }
        public string dmaMovto { get; set; }
        public string localEstoque { get; set; }
        public string idLocalEstoque { get; set; }
        public string idFilial { get; set; }
    }
}
