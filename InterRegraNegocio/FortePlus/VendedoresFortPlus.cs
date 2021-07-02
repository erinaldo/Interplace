using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{

    public class VendedoresFortPlus
    {
        public VendedorFortPlus[] Property1 { get; set; }
    }

    public class VendedorFortPlus
    {
        public int? id { get; set; }
        public string psTipoPessoa { get; set; }
        public string psCodigo { get; set; }
        public string psNome { get; set; }
        public string psNomeFantasia { get; set; }
        public string psCpfCnpj { get; set; }
        public DateTime? psDmaNascimento { get; set; }
        public string psCep { get; set; }
        public string psLogradouro { get; set; }
        public string psNumero { get; set; }
        public string psComplemento { get; set; }
        public string psBairro { get; set; }
        public int? psIdUf { get; set; }
        public int? psIdMunicipio { get; set; }
        public int? psIdPais { get; set; }
        public int? psIdRegiao { get; set; }
        public int? psIdRota { get; set; }
        public string psEmail { get; set; }
        public string psSite { get; set; }
        public string psTelFixo { get; set; }
        public string psTelCelular { get; set; }
        public string psTelFax { get; set; }
        public string psTelOutros { get; set; }
        public string psContatoFixo { get; set; }
        public string psContatoCelular { get; set; }
        public string psInscricaoMunicipal { get; set; }
        public string psInscricaoEstadual { get; set; }
        public string psInscricaoSuframa { get; set; }
        public string psRegimeTributario { get; set; }
        public string psTipoInscricao { get; set; }
        public string psTipoContribIcms { get; set; }
        public string psConsumidorFinal { get; set; }
        public string psProdutorRural { get; set; }
        public string psRecolheIss { get; set; }
        public int? psIdVendedor { get; set; }
        public string psObservacao { get; set; }
        public string psEntidade { get; set; }
        public string psTipoComissao { get; set; }
        public float? psPercentComissao { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public int? psIdTabelaPreco { get; set; }
        public string psBaseInclui { get; set; }
        public string psGuid { get; set; }
        public int? psIdClasse { get; set; }
        public string psOver { get; set; }
        public string psIdExterno { get; set; }
    }

}
