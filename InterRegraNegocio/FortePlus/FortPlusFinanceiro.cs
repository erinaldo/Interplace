using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{
    public class FortPlusFinanceiro
    {
        public int? id { get; set; }
        public int? fnIdPessoa { get; set; }
        public int? fnIdMovto { get; set; }
        public int? fnIdFormaPagto { get; set; }
        public int? fnIdCondicaoPagto { get; set; }
        public int? fnIdTipoDocto { get; set; }
        public int? fnIdParent { get; set; }
        public int? fnIdCaixa { get; set; }
        public int? fnIdBanco { get; set; }
        public int? fnParcela { get; set; }
        public int? fnNumParcela { get; set; }
        public DateTime? fnDmaEmissao { get; set; }
        public DateTime? fnDmaVencto { get; set; }
        public DateTime? fnDmaVenctoReal { get; set; }
        public DateTime? fnDmaBaixa { get; set; }
        public DateTime? fnDmaLote { get; set; }
        public float? fnValor { get; set; }
        public float? fnPercJuros { get; set; }
        public float? fnValorJuros { get; set; }
        public float? fnPercMulta { get; set; }
        public float? fnValorMulta { get; set; }
        public float? fnPercDesconto { get; set; }
        public float? fnValorDesconto { get; set; }
        public float? fnSaldo { get; set; }
        public string fnDocto { get; set; }
        public string fnNumCheque { get; set; }
        public string fnEmitenteCheque { get; set; }
        public string fnBancoCheque { get; set; }
        public string fnAgenciaCheque { get; set; }
        public string fnContacheque { get; set; }
        public string fnTelEmitCheque { get; set; }
        public string fnMotivoDevCheque { get; set; }
        public string fnLote { get; set; }
        public string fnNossoNumero { get; set; }
        public string fnBloqueio { get; set; }
        public string fnStatus { get; set; }
        public string fnHistorico { get; set; }
        public string fnEntidade { get; set; }
        public string fnGuid { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public int? fnIdVendedor { get; set; }
        public string fnComissaoGerada { get; set; }
        public float? fnBaseCalculo { get; set; }
        public float? fnPercentComissao { get; set; }
        public string fnTipoComissao { get; set; }
        public string fnDc { get; set; }
        public string fnInstrucao { get; set; }
        public string fnGuidRenegociacao { get; set; }
    }

}
