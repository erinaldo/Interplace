using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio
{
    public class EtiquetaJSON
    {
        public PlpEtiqueta plp { get; set; }
        public Docsexterno[] docsExternos { get; set; }
    }

    public class PlpEtiqueta
    {
        public int id { get; set; }
        public string codExterno { get; set; }
        public string dtEnvio { get; set; }
        public string tpAgrupamento { get; set; }
        public Resumoservico[] resumoServicos { get; set; }
    }

    public class Resumoservico
    {
        public string codServico { get; set; }
        public string nomeServico { get; set; }
        public int quantidadeAwbs { get; set; }
    }

    public class Docsexterno
    {
        public long codCliente { get; set; }
        public string docExterno { get; set; }
        public string dtPrometida { get; set; }
        public string tpEntrega { get; set; }
        public float pesoTotal { get; set; }
        public string marca { get; set; }
        public int qtVolumes { get; set; }
        public string numeroContratoTransp { get; set; }
        public string nomeEmbarcador { get; set; }
        public int? telefoneEmbarcador { get; set; }
        public string emailEmbarcador { get; set; }
        public string tpServico { get; set; }
        public string numNotaFiscal { get; set; }
        public string serieNotaFiscal { get; set; }
        public string megaRota { get; set; }
        public string rota { get; set; }
        public string telefoneContato { get; set; }
        public float vlEntrega { get; set; }
        public string cartaoPostagem { get; set; }
        public string servicoAdicional { get; set; }
        public Destinatario destinatario { get; set; }
        public Remetente remetente { get; set; }
        public Awb[] awbs { get; set; }
    }

    public class Destinatario
    {
        public string nome { get; set; }
        public string enderecoLogradouro { get; set; }
        public string enderecoNumero { get; set; }
        public string enderecoComplemento { get; set; }
        public string enderecoBairro { get; set; }
        public string enderecoCidade { get; set; }
        public string enderecoUf { get; set; }
        public string enderecoCep { get; set; }
    }

    public class Remetente
    {
        public string nome { get; set; }
        public string enderecoLogradouro { get; set; }
        public string enderecoNumero { get; set; }
        public string enderecoComplemento { get; set; }
        public string enderecoBairro { get; set; }
        public string enderecoCidade { get; set; }
        public string enderecoUf { get; set; }
        public string enderecoCep { get; set; }
    }

    public class Awb
    {
        public string codigoAwb { get; set; }
        public int posicaoVolume { get; set; }
        public Iten[] itens { get; set; }
    }

    public class Iten
    {
        public string descricao { get; set; }
        public int quantidade { get; set; }
        public float peso { get; set; }
    }
}
