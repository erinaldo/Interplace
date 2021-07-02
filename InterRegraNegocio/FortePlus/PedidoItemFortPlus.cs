using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{

    public class PedidoItemFortPlus
    {
        public int? id { get; set; }
        public int? mtIdNfOrigem { get; set; }
        public int? mtIdMovto { get; set; }
        public int? mtIdProduto { get; set; }
        public float? mtQtde { get; set; }
        public float? mtValorUnitario { get; set; }
        public float? mtValorDesconto { get; set; }
        public float? mtValorDescontoRateio { get; set; }
        public float? mtPercDesconto { get; set; }
        public float? mtValor { get; set; }
        public float? mtValorTotal { get; set; }
        public float? mtValorFrete { get; set; }
        public float? mtValorSeguro { get; set; }
        public float? mtValorOutrasDespesas { get; set; }
        public float? mtCustoMedio { get; set; }
        public float? mtValorTabela { get; set; }
        public float? mtPesoBruto { get; set; }
        public float? mtPesoLiquido { get; set; }
        public int? mtIdCfop { get; set; }
        public int? mtIdNcm { get; set; }
        public int? mtIdLocalEstoque { get; set; }
        public float? mtValorAproxImposto { get; set; }
        public float? mtValorTributoEstadual { get; set; }
        public float? mtValorTributoImportado { get; set; }
        public float? mtValorTributoMunicipal { get; set; }
        public float? mtValorTributoNacional { get; set; }
        public float? mtPercEstadual { get; set; }
        public float? mtPercImportado { get; set; }
        public float? mtPercMunicipal { get; set; }
        public float? mtPercNacional { get; set; }
        public string mtVersaoIbpt { get; set; }
        public int? mtIdUnidade { get; set; }
        public string mtOrdemCompra { get; set; }
        public string mtOrdemItemCompra { get; set; }
        public string mtReferencia { get; set; }
        public string mtEntidade { get; set; }
        public string mtModalidadeBcIcms { get; set; }
        public int? mtIdCstIcms { get; set; }
        public double? mtBaseIcms { get; set; }
        public double? mtAliquotaIcms { get; set; }
        public double? mtPercentReducaoBaseIcms { get; set; }
        public double? mtValorIcms { get; set; }
        public double? mtValorReducaoIcms { get; set; }
        public double? mtAliquotaIcmsCr { get; set; }
        public double? mtValorIcmsCr { get; set; }
        public double? mtBaseIcmsCr { get; set; }
        public string mtModalidadeBcIcmsSt { get; set; }
        public double? mtPercentReducaoBaseIcmsSt { get; set; }
        public double? mtAliquotaIcmsSt { get; set; }
        public double? mtAliquotaMva { get; set; }
        public double? mtValorIcmsSt { get; set; }
        public double? mtValorReducaoIcmsSt { get; set; }
        public double? mtBaseIcmsSt { get; set; }
        public double? mtIdCstPis { get; set; }
        public double? mtBasePis { get; set; }
        public double? mtPercentReducaoBasePis { get; set; }
        public double? mtAliquotaPis { get; set; }
        public double? mtValorPis { get; set; }
        public int? mtIdCstCofins { get; set; }
        public double? mtBaseCofins { get; set; }
        public double? mtPercentReducaoBaseCofins { get; set; }
        public double? mtAliquotaCofins { get; set; }
        public double? mtValorCofins { get; set; }
        public double? mtIdCstIpi { get; set; }
        public double? mtBaseIpi { get; set; }
        public double? mtPercentReducaoBaseIpi { get; set; }
        public double? mtAliquotaIpi { get; set; }
        public double? mtValorIpi { get; set; }
        public int? mtIdCstIi { get; set; }
        public double? mtBaseIi { get; set; }
        public double? mtPercentReducaoBaseIi { get; set; }
        public double? mtValorIi { get; set; }
        public double? mtIdCstIssqn { get; set; }
        public double? mtBaseIssqn { get; set; }
        public double? mtPercentReducaoBaseIssqn { get; set; }
        public double? mtValorIssqn { get; set; }
        public double? mtAliquotainterEstadual { get; set; }
        public double? mtBaseDifal { get; set; }
        public double? mtAliquotaDifal { get; set; }
        public double? mtValorDifal { get; set; }
        public double? mtBaseFecp { get; set; }
        public double? mtAliquotaFecp { get; set; }
        public double? mtValorFecp { get; set; }
        public double? mtBaseFecpSt { get; set; }
        public double? mtAliquotaFecpSt { get; set; }
        public double? mtValorFecpSt { get; set; }
        public double? mtBaseFecpStRet { get; set; }
        public double? mtAliquotaFecpStRet { get; set; }
        public double? mtValorFecpStRet { get; set; }
        public double? mtValorIcmsPartUfOrigem { get; set; }
        public double? mtValorIcmsPartUfDestino { get; set; }
        public double? mtAliquotaIcmsPartUfOrigem { get; set; }
        public double? mtAliquotaIcmsPartUfDestino { get; set; }
        public double? mtBaseIcmsPartUfOrigem { get; set; }
        public double? mtBaseIcmsPartUfDestino { get; set; }
        public double? mtBaseCalculoKardex { get; set; }
        public double? mtPercentComissao { get; set; }
        public string mtGuid { get; set; }
        public string mtObservacao { get; set; }
        public DateTime? mtDmaItem { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public int? mtIdCest { get; set; }
        public int? mtIdParent { get; set; }
        public double? mtQtdeLiberada { get; set; }
        public double? mtQtdeSaldo { get; set; }
        public string mtVariacao { get; set; }
        public double? mtPrecoDePor { get; set; }
        public string mtAjCusto { get; set; }
        public string mtCEnqIpi { get; set; }
        public string mtAgregaTotalFecp { get; set; }
        public int? mtIdLote { get; set; }
        public string mtLote { get; set; }
        public DateTime? mtDmaLote { get; set; }
    }


}
