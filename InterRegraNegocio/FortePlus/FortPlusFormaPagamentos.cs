using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{

    public class FortPlusFormaPagamentos
    {
        public FortPlusFormaPagamento[] Property1 { get; set; }
    }

    public class FortPlusFormaPagamento
    {
        public int? id { get; set; }
        public string fpCodigo { get; set; }
        public string fpNome { get; set; }
        public int fpIdCodigoSefaz { get; set; }
        public string fpLancaCaixa { get; set; }
        public string fpBaixaAuto { get; set; }
        public string fpPedidoVenda { get; set; }
        public string fpOperadoraCartao { get; set; }
        public string fpAceitaConvenio { get; set; }
        public string fpAceitaParcelar { get; set; }
        public string fpAceitaCompensacao { get; set; }
        public string fpChequeObrigatorio { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }

 
}
