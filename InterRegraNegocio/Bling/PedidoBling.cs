using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.Bling
{
    public class PedidoJsonBling
    {
        public RetornoBling retorno { get; set; }
    }

    public class RetornoBling
    {
        public PedidoBling[] pedidos { get; set; }
    }

    public class PedidoBling
    {
        public Pedido1 pedido { get; set; }
    }

    public class Pedido1
    {
        public string desconto { get; set; }
        public string observacoes { get; set; }
        public string observacaointerna { get; set; }
        public string data { get; set; }
        public string numero { get; set; }
        public string numeroOrdemCompra { get; set; }
        public string vendedor { get; set; }
        public string valorfrete { get; set; }
        public string totalprodutos { get; set; }
        public string totalvenda { get; set; }
        public string situacao { get; set; }
        public string dataSaida { get; set; }
        public string loja { get; set; }
        public string numeroPedidoLoja { get; set; }
        public string tipoIntegracao { get; set; }
        public ClienteBling cliente { get; set; }
        public Pagamento pagamento { get; set; }
        public TransporteBling transporte { get; set; }
        public Iten[] itens { get; set; }
        public Parcela[] parcelas { get; set; }
        public Codigosrastreamento codigosRastreamento { get; set; }
        public Nota nota { get; set; }
    }

    public class ClienteBling
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string rg { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public string fone { get; set; }
    }

    public class Pagamento
    {
        public string categoria { get; set; }
    }

    public class TransporteBling
    {
        public Enderecoentrega enderecoEntrega { get; set; }
        public VolumeBling[] volumes { get; set; }
        public string servico_correios { get; set; }
    }

    public class Enderecoentrega
    {
        public string nome { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
    }

    public class VolumeBling
    {
        public Volume1 volume { get; set; }
    }

    public class Volume1
    {
        public string id { get; set; }
        public string idServico { get; set; }
        public string idOrigem { get; set; }
        public string servico { get; set; }
        public string codigoServico { get; set; }
        public string codigoRastreamento { get; set; }
        public string valorFretePrevisto { get; set; }
        public object remessa { get; set; }
        public string dataSaida { get; set; }
        public object prazoEntregaPrevisto { get; set; }
        public string valorDeclarado { get; set; }
        public bool avisoRecebimento { get; set; }
        public bool maoPropria { get; set; }
        public DimensoesBling dimensoes { get; set; }
        public string urlRastreamento { get; set; }
    }

    public class DimensoesBling
    {
        public string peso { get; set; }
        public string altura { get; set; }
        public string largura { get; set; }
        public string comprimento { get; set; }
        public string diametro { get; set; }
    }

    public class Codigosrastreamento
    {
        public string codigoRastreamento { get; set; }
    }

    public class Nota
    {
        public string serie { get; set; }
        public string numero { get; set; }
        public string dataEmissao { get; set; }
        public string situacao { get; set; }
        public string valorNota { get; set; }
        public string chaveAcesso { get; set; }
    }

    public class Iten
    {
        public Item item { get; set; }
    }

    public class Item
    {
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string quantidade { get; set; }
        public string valorunidade { get; set; }
        public string precocusto { get; set; }
        public string descontoItem { get; set; }
        public string un { get; set; }
        public string pesoBruto { get; set; }
        public string largura { get; set; }
        public string altura { get; set; }
        public string profundidade { get; set; }
        public string descricaoDetalhada { get; set; }
        public string unidadeMedida { get; set; }
        public string gtin { get; set; }
    }

    public class Parcela
    {
        public Parcela1 parcela { get; set; }
    }

    public class Parcela1
    {
        public string idLancamento { get; set; }
        public string valor { get; set; }
        public string dataVencimento { get; set; }
        public string obs { get; set; }
        public string destino { get; set; }
        public Forma_Pagamento forma_pagamento { get; set; }
    }

    public class Forma_Pagamento
    {
        public string id { get; set; }
        public string descricao { get; set; }
        public string codigoFiscal { get; set; }
    }

}
