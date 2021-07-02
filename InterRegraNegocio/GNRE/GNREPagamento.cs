using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.GNRE
{
    public class GNREPagamento
    {
        public Headerarquivo HeaderArquivo { get; set; }
        public Headerlote HeaderLote { get; set; }
        public Segmentoj SegmentoJ { get; set; }
    }

    public class Headerarquivo
    {
        public int cedente_inscricao_tipo { get; set; }
        public long cedente_inscricao_numero { get; set; }
        public string codigo_convenio { get; set; }
        public int cedente_agencia { get; set; }
        public string cedente_agencia_dv { get; set; }
        public int cedente_conta { get; set; }
        public string cedente_conta_dv { get; set; }
        public string cedente_nome { get; set; }
        public int data_geracao_arquivo { get; set; }
        public int hora_geracao_arquivo { get; set; }
        public int numero_sequencial_arquivo { get; set; }
    }

    public class Headerlote
    {
        public int tipo_servico { get; set; }
        public int numero_versao_lote { get; set; }
        public int cedente_inscricao_tipo { get; set; }
        public long cedente_inscricao_numero { get; set; }
        public string codigo_convenio { get; set; }
        public int cedente_agencia { get; set; }
        public string cedente_agencia_dv { get; set; }
        public int cedente_conta { get; set; }
        public string cedente_conta_dv { get; set; }
        public string cedente_nome { get; set; }
        public string mensagem1 { get; set; }
    }

    public class Segmentoj
    {
        public int sequencial_registro_lote { get; set; }
        public int tipo_movimento { get; set; }
        public int codigo_instrucao_movimento { get; set; }
        public long codigo_de_barras { get; set; }
        public string favorecido_nome { get; set; }
        public int data_vencimento { get; set; }
        public string valor_nominal_titulo { get; set; }
        public string valor_desconto_abatimento { get; set; }
        public string valor_multa_juros { get; set; }
        public string valor_pagamento { get; set; }
        public int data_pagamento { get; set; }
        public string numero_documento_cliente { get; set; }
        public string numero_documento_banco { get; set; }
    }

}
