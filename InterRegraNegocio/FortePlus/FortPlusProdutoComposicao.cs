using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.FortePlus
{


    public class FortPlusProdutosComposicao
    {
        public FortPlusProdutoComposicao[] Property1 { get; set; }
    }

    public class FortPlusProdutoComposicao
    {
        public int id { get; set; }
        public int? pcIdProduto { get; set; }
        public int? pcIdProdutoComposicao { get; set; }
        public string pcVariacao { get; set; }
        public double pcQtde { get; set; }
        public string pcOpcional { get; set; }
        public string pcGuid { get; set; }
        public int? idFilial { get; set; }
        public int? idIncluidoPor { get; set; }
        public int? idAltaradoPor { get; set; }
        public DateTime? dmaInclusao { get; set; }
        public DateTime? dmaAlteracao { get; set; }
        public string ativo { get; set; }
    }


}
