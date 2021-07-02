using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{

    public class FortePlusTabelaPrecoItens
    {
        public FortePlusTabelaPrecoItem[] Property1 { get; set; }
    }

    public class FortePlusTabelaPrecoItem
    {
        public int? id { get; set; }
        public int? mtIdNfOrigem { get; set; }
        public int? mtIdMovto { get; set; }
        public int? mtIdParent { get; set; }
        public int? mtIdProduto { get; set; }
        public int? mtQtde { get; set; }
        public int? mtQtdeAutorizada { get; set; }
        public int? mtQtdeFaturada { get; set; }
        public int? mtQtdeLiberada { get; set; }
        public int? mtQtdeSaldoLiberado { get; set; }
        public int? mtQtdeSaldo { get; set; }
        public int? mtValorUnitario { get; set; }
        public int? mtValorDesconto { get; set; }
        public int? mtValorDescontoRateio { get; set; }
        public int? mtPercDesconto { get; set; }
        public int? mtValor { get; set; }
        public int? mtValorTotal { get; set; }
        public int? mtValorFrete { get; set; }
        public int? mtValorSeguro { get; set; }
        public int? mtValorOutrasDespesas { get; set; }
        public int? mtCustoMedio { get; set; }
        public int? mtValorTabela { get; set; }
        public int? mtPesoBruto { get; set; }
        public int? mtPesoLiquido { get; set; }
        public int? mtIdCfop { get; set; }
        public int? mtIdNcm { get; set; }
        public int? mtIdLote { get; set; }
        public int? mtIdCentroCusto { get; set; }
        public int? mtIdPlanoConta { get; set; }
        public string mtLote { get; set; }
        public int? mtIdCest { get; set; }
        public int? mtIdLocalEstoque { get; set; }
        public int? mtValorAproxImposto { get; set; }
        public int? mtValorTributoEstadual { get; set; }
        public int? mtValorTributoImportado { get; set; }
        public int? mtValorTributoMunicipal { get; set; }
        public int? mtValorTributoNacional { get; set; }
        public int? mtPercEstadual { get; set; }
        public int? mtPercImportado { get; set; }
        public int? mtPercMunicipal { get; set; }
        public int? mtPercNacional { get; set; }
        public string mtVersaoIbpt { get; set; }
        public int? mtIdUnidade { get; set; }
        public string mtOrdemCompra { get; set; }
        public string mtOrdemItemCompra { get; set; }
        public string mtReferencia { get; set; }
        public string mtCodigo { get; set; }
        public string mtEntidade { get; set; }
        public string mtVariacao { get; set; }
        public string mtOpcional { get; set; }
        public string mtStatus { get; set; }
        public string mtAjCusto { get; set; }
        public DateTime? mtDmaNecessidade { get; set; }
        public DateTime? mtDmaLote { get; set; }
        public DateTime? mtDmaAutorizacao { get; set; }
        public int? mtIdAutorizadoPor { get; set; }
        public string mtModalidadeBcIcms { get; set; }
        public int? mtIdCstIcms { get; set; }
        public int? mtBaseIcms { get; set; }
        public int? mtAliquotaIcms { get; set; }
        public int? mtPercentReducaoBaseIcms { get; set; }
        public int? mtValorIcms { get; set; }
        public int? mtValorReducaoIcms { get; set; }
        public int? mtAliquotaIcmsCr { get; set; }
        public int? mtValorIcmsCr { get; set; }
        public int? mtBaseIcmsCr { get; set; }
        public string mtModalidadeBcIcmsSt { get; set; }
        public int? mtPercentReducaoBaseIcmsSt { get; set; }
        public int? mtAliquotaIcmsSt { get; set; }
        public int? mtAliquotaMva { get; set; }
        public int? mtValorIcmsSt { get; set; }
        public int? mtValorReducaoIcmsSt { get; set; }
        public int? mtBaseIcmsSt { get; set; }
        public int? mtIdCstPis { get; set; }
        public int? mtBasePis { get; set; }
        public int? mtPercentReducaoBasePis { get; set; }
        public int? mtAliquotaPis { get; set; }
        public int? mtValorPis { get; set; }
        public int? mtIdCstCofins { get; set; }
        public int? mtBaseCofins { get; set; }
        public int? mtPercentReducaoBaseCofins { get; set; }
        public int? mtAliquotaCofins { get; set; }
        public int? mtValorCofins { get; set; }
        public int? mtIdCstIpi { get; set; }
        public int? mtBaseIpi { get; set; }
        public int? mtPercentReducaoBaseIpi { get; set; }
        public int? mtAliquotaIpi { get; set; }
        public int? mtValorIpi { get; set; }
        public string mtCEnqIpi { get; set; }
        public int? mtIdCstIi { get; set; }
        public int? mtBaseIi { get; set; }
        public int? mtPercentReducaoBaseIi { get; set; }
        public int? mtAliquotaIi { get; set; }
        public int? mtValorIi { get; set; }
        public int? mtValorAduaneiro { get; set; }
        public int? mtValorIOF { get; set; }
        public string mtNumeroDI { get; set; }
        public DateTime? mtDmaDI { get; set; }
        public string mtLocalDesembaraco { get; set; }
        public int? mtIdUfDesembaraco { get; set; }
        public DateTime? mtDmaDesembaraco { get; set; }
        public string mtViaTransporte { get; set; }
        public int? mtValorAFRMM { get; set; }
        public string mtTipointermediacao { get; set; }
        public string mtNumeroAdicao { get; set; }
        public string mtSequencialAdicao { get; set; }
        public string mtCodFabricanteEstrangeiro { get; set; }
        public int? mtValorDescontoDI { get; set; }
        public string mtNumeroDrawback { get; set; }
        public int? mtIdCstIssqn { get; set; }
        public int? mtBaseIssqn { get; set; }
        public int? mtPercentReducaoBaseIssqn { get; set; }
        public int? mtAliquotaIssqn { get; set; }
        public int? mtValorIssqn { get; set; }
        public int? mtValorIssqnRetido { get; set; }
        public int? mtAliquotaCsll { get; set; }
        public int? mtValorCsll { get; set; }
        public int? mtValorIr { get; set; }
        public int? mtValorInss { get; set; }
        public int? mtValorDeducoes { get; set; }
        public int? mtAliquotaintra { get; set; }
        public int? mtAliquotainterEstadual { get; set; }
        public int? mtBaseDifal { get; set; }
        public int? mtAliquotaDifal { get; set; }
        public int? mtValorDifal { get; set; }
        public int? mtBaseFecp { get; set; }
        public int? mtAliquotaFecp { get; set; }
        public int? mtValorFecp { get; set; }
        public int? mtBaseFecpSt { get; set; }
        public int? mtAliquotaFecpSt { get; set; }
        public int? mtValorFecpSt { get; set; }
        public string mtAgregaTotalFecp { get; set; }
        public int? mtBaseFecpStRet { get; set; }
        public int? mtAliquotaFecpStRet { get; set; }
        public int? mtValorFecpStRet { get; set; }
        public int? mtValorIcmsPartUfOrigem { get; set; }
        public int? mtValorIcmsPartUfDestino { get; set; }
        public int? mtAliquotaIcmsPartUfOrigem { get; set; }
        public int? mtAliquotaIcmsPartUfDestino { get; set; }
        public int? mtBaseIcmsPartUfOrigem { get; set; }
        public int? mtBaseIcmsPartUfDestino { get; set; }
        public int? mtBaseCalculoKardex { get; set; }
        public int? mtValorMarketplace { get; set; }
        public int? mtPercentComissao { get; set; }
        public string mtGuid { get; set; }
        public string mtObservacao { get; set; }
        public DateTime? mtDmaItem { get; set; }
        public int? mtPrecoDePor { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }

}
