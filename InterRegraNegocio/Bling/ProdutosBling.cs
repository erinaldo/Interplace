using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.Bling
{



    public class ProdutosBling
    {
        public Retorno retorno { get; set; }
    }

    public class RetornoProdutosBling
    {
        public Produto[] produtos { get; set; }
    }

    public class Produto
    {
        public Produto1 produto { get; set; }
    }

    public class Produto1
    {
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string tipo { get; set; }
        public string situacao { get; set; }
        public string unidade { get; set; }
        public string preco { get; set; }
        public string precoCusto { get; set; }
        public string descricaoCurta { get; set; }
        public string descricaoComplementar { get; set; }
        public string dataInclusao { get; set; }
        public string dataAlteracao { get; set; }
        public object imageThumbnail { get; set; }
        public string urlVideo { get; set; }
        public string nomeFornecedor { get; set; }
        public string codigoFabricante { get; set; }
        public string marca { get; set; }
        public string class_fiscal { get; set; }
        public string cest { get; set; }
        public string origem { get; set; }
        public string idGrupoProduto { get; set; }
        public string linkExterno { get; set; }
        public string observacoes { get; set; }
        public object grupoProduto { get; set; }
        public string garantia { get; set; }
        public string descricaoFornecedor { get; set; }
        public string idFabricante { get; set; }
        public Categoria categoria { get; set; }
        public string pesoLiq { get; set; }
        public string pesoBruto { get; set; }
        public string estoqueMinimo { get; set; }
        public string estoqueMaximo { get; set; }
        public string gtin { get; set; }
        public string gtinEmbalagem { get; set; }
        public string larguraProduto { get; set; }
        public string alturaProduto { get; set; }
        public string profundidadeProduto { get; set; }
        public string unidadeMedida { get; set; }
        public int itensPorCaixa { get; set; }
        public int volumes { get; set; }
        public string localizacao { get; set; }
        public string crossdocking { get; set; }
        public string condicao { get; set; }
        public string freteGratis { get; set; }
        public string producao { get; set; }
        public string dataValidade { get; set; }
        public string spedTipoItem { get; set; }
        public Estrutura[] estrutura { get; set; }
    }

    public class Categoria
    {
        public string id { get; set; }
        public string descricao { get; set; }
    }

    public class Estrutura
    {
        public Componente componente { get; set; }
    }

    public class Componente
    {
        public string nome { get; set; }
        public string codigo { get; set; }
    }

}
