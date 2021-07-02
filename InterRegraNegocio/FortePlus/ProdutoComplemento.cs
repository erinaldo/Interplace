using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.FortePlus
{

    public class ProdutosComplementos
    {
        public ProdutoComplemento[] Property1 { get; set; }
    }


    public class ProdutoComplemento
    {
        public int? id { get; set; }
        public int? cmIdProduto { get; set; }
        public string cmVariacao { get; set; }
        public int? cmIdMarketPlace { get; set; }
        public string cmIdExterno { get; set; }
        public float? cmPrecoDePor { get; set; }
        public float? cmPrecoVenda { get; set; }
        public string cmEan { get; set; }
        public string cmCaracteristica { get; set; }
        public string cmEspecificacaoTecnica { get; set; }
        public string cmImagem { get; set; }
        public string cmNomeImagem { get; set; }
        public string cmEntidade { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
        public string cmCodigo { get; set; }
        public float? cmPercentual { get; set; }
        public int? cmIdTipoAnuncio { get; set; }
        public string cmTitulo { get; set; }
        public string cmUrlImagem { get; set; }
    }


 


}
