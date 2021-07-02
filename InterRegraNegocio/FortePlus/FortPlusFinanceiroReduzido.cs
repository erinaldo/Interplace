using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{
    public class FortPlusFinanceiroReduzido
    {
        public string email { get; set; }
        public int? idFilial { get; set; }
        public int? idMovto { get; set; }
        public int? idFormaPagamento { get; set; }
        public int? idCondicaoPagamento { get; set; }
        public double? valor { get; set; }
    }
}
