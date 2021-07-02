using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.Bling
{
    public class Cliente
    {
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
        public string fone { get; set; }
    }

    public class EnderecoEntrega
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

    public class Dimensoes
    {
        public string peso { get; set; }
        public string altura { get; set; }
        public string largura { get; set; }
        public string comprimento { get; set; }
        public string diametro { get; set; }
    }

    public class Volume
    {
        public string id { get; set; }
        public string idServico { get; set; }
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
        public Dimensoes dimensoes { get; set; }
        public string urlRastreamento { get; set; }
    }

    public class VolumeNota
    {
        public Volume volume { get; set; }
    }

    public class Transporte
    {
        public EnderecoEntrega enderecoEntrega { get; set; }
        public IList<Volume> volumes { get; set; }
        public string servico_correios { get; set; }
    }

    public class Notafiscal
    {
        public string serie { get; set; }
        public string numero { get; set; }
        public string loja { get; set; }
        public string numeroPedidoLoja { get; set; }
        public string tipo { get; set; }
        public string situacao { get; set; }
        public Cliente cliente { get; set; }
        public string contato { get; set; }
        public string cnpj { get; set; }
        public string vendedor { get; set; }
        public string dataEmissao { get; set; }
        public string valorNota { get; set; }
        public string chaveAcesso { get; set; }
        public string xml { get; set; }
        public string linkDanfe { get; set; }
        public object codigosRastreamento { get; set; }
        public IList<string> cfops { get; set; }
        public string tipoIntegracao { get; set; }
        public Transporte transporte { get; set; }
    }

    public class Notasfiscai
    {
        public Notafiscal notafiscal { get; set; }
    }

    public class Retorno
    {
        public IList<Notasfiscai> notasfiscais { get; set; }
    }

    public class NotaFiscaljsonBling
    {
        public Retorno retorno { get; set; }
    }

}
