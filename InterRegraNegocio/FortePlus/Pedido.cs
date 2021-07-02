using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{


    public class Pedidos
    {
        public Pedido[] ListaPedidos { get; set; }
    }

    public class Pedido
    {
        public int? id { get; set; }
        public int? mvDocto { get; set; }
        public int? mvIdPessoa { get; set; }
        public int? mvIdVendedor { get; set; }
        public int? mvIdSerie { get; set; }
        public int? mvIdModelo { get; set; }
        public int? mvIdTipoDocumento { get; set; }
        public int? mvIdTipoFrete { get; set; }
        public int? mvIdNatureza { get; set; }
        public int? mvIdParent { get; set; }
        public DateTime? mvDmaEmissao { get; set; }
        public DateTime? mvDmaEntradaSaida { get; set; }
        public string mvTipoMovimento { get; set; }
        public string mvTipoPedido { get; set; }
        public string mvPreNota { get; set; }
        public string mvFinNf { get; set; }
        public string mvPresenca { get; set; }
        public string mvTransmitir { get; set; }
        public int? mvIdTransportadora { get; set; }
        public int? mvQuantidade { get; set; }
        public string mvEspecie { get; set; }
        public string mvMarca { get; set; }
        public string mvNumeracao { get; set; }
        public float? mvPesoBruto { get; set; }
        public float? mvPesoLiquido { get; set; }
        public float? mvValorFrete { get; set; }
        public float? mvValorSeguro { get; set; }
        public float? mvValorDesconto { get; set; }
        public float? mvValorDescontoRateio { get; set; }
        public float? mvValorOutrasDespesasAcessoria { get; set; }
        public float? mvValorTotalProduto { get; set; }
        public float? mvValorTotalServico { get; set; }
        public double? mvValorTotal { get; set; }
        public float? mvValorAproxImposto { get; set; }
        public float? mvBaseIcms { get; set; }
        public float? mvValorIcms { get; set; }
        public float? mvBaseIcmsSt { get; set; }
        public float? mvValorIcmsSt { get; set; }
        public float? mvBaseIpi { get; set; }
        public float? mvValorIpi { get; set; }
        public float? mvValorIpiDevol { get; set; }
        public float? mvBasePis { get; set; }
        public float? mvValorPis { get; set; }
        public float? mvBaseCofins { get; set; }
        public float? mvValorCofins { get; set; }
        public float? mvBaseIss { get; set; }
        public float? mvValorIss { get; set; }
        public float? mvBaseIi { get; set; }
        public float? mvValorIi { get; set; }
        public float? mvIcmsDeson { get; set; }
        public float? mvValorFecp { get; set; }
        public float? mvValorFecpSt { get; set; }
        public float? mvValorFecpStRet { get; set; }
        public float? mvValorIcmsUfOrigem { get; set; }
        public float? mvValorIcmsUfDestino { get; set; }
        public string mvObservacao { get; set; }
        public string mvChaveAcesso { get; set; }
        public string mvVersao { get; set; }
        public string mvProtocolo { get; set; }
        public DateTime? mvDmaProtocolo { get; set; }
        public string mvTpEmis { get; set; }
        public string mvTpAmb { get; set; }
        public DateTime? mvDmaContingencia { get; set; }
        public string mvQrCode { get; set; }
        public string mvImagemQrCode { get; set; }
        public string mvUrlChave { get; set; }
        public string mvAutorizacaoCartao { get; set; }
        public string mvStatus { get; set; }
        public string mvGuid { get; set; }
        public string mvEntidade { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public DateTime? mvDmaEntrega { get; set; }
        public string mvPedidoParcial { get; set; }
        public int? mvIdTabelaPreco { get; set; }
        public string mvObservacaoFiscal { get; set; }
        public string mvObservacaoCpl { get; set; }
        public string mvChaveAcessoRef { get; set; }
        public string mvIdExterno { get; set; }
        public string mvIdRastreio { get; set; }
        public string mvUpdateStatus { get; set; }
        public int? mvIdStatus { get; set; }
        public string mvPedido { get; set; }
        public string mvRastreabilidade { get; set; }
        public string mvOperadorLogistico { get; set; }
    }

}
