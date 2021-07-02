using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{

    public class ProdutosFortePlus
    {
        public ProdutoFortePlus[] aProduto { get; set; }
    }

    public class ProdutoFortePlus
    {
        public int? id { get; set; }
        public int? prIdGrupo { get; set; }
        public int? prIdSubGrupo { get; set; }
        public int? prIdTipo { get; set; }
        public int? prIdUnidadePrincipal { get; set; }
        public int? prIdUnidadeSecundaria { get; set; }
        public int? prIdMarca { get; set; }
        public int? prIdFornecedor { get; set; }
        public int? prIdFabricante { get; set; }
        public int? prIdNcm { get; set; }
        public int? prIdParent { get; set; }
        public int? prIdOrig { get; set; }
        public float? prPrecoVenda { get; set; }
        public float? prQuantidadeMinima { get; set; }
        public float? prPesoLiquido { get; set; }
        public float? prPesoBruto { get; set; }
        public float? prPercentComissao { get; set; }
        public string prCodigo { get; set; }
        public string prNome { get; set; }
        public object prEan { get; set; }
        public object prReferencia { get; set; }
        public string prEntidade { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public object idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public object dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public int? prIdCest { get; set; }
        public object prVariacao { get; set; }
        public object prIdExterno { get; set; }
        public object prUpdateEcommerce { get; set; }
        public object prUpdateEstoque { get; set; }
        public float? prPrecoDePor { get; set; }
        public object prUpdatePreco { get; set; }
        public string prEcommerce { get; set; }
        public string prGuid { get; set; }
        public float? prLoteMinimo { get; set; }
        public float? prProfundidade { get; set; }
        public float? prAltura { get; set; }
        public float? prLargura { get; set; }
        public float? prComprimento { get; set; }
        public float? prVolume { get; set; }
        public float? prQtdItemCaixa { get; set; }
    }

}
