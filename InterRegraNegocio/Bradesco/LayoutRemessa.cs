
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRegraNegocio.Bradesco
{
    public class LayoutRemessa
    {
        public class RegistroHeader
        {
            public Controle Controle { get; set; }//
            public Servico Servico { get; set; }//
            public CNAB CNAB { get; set; }//
            public Empresa Empresa { get; set; }//
            public Informacao Informacao { get; set; }//
            public EnderecoEmpresa EnderecoEmpresa { get; set; }//
            public CNAB CNABEndereco { get; set; }//
            public Ocorrencias Ocorrencias { get; set; }//

        }//



        public class Empresa
        {
            public Inscricao Inscricao { get; set; }

            public Convenio Convenio { get; set; }

            public ContaCorrente ContaCorrente { get; set; }
            public Nome NomeEmpresa { get; set; }
        }



        public class EnderecoEmpresa
        {
            /// <summary>
            /// G032
            /// </summary>
            public string Logradouro { get; set; }
            /// <summary>
            /// G032
            /// </summary>
            public string Numero { get; set; }
            /// <summary>
            /// G032
            /// </summary>
            public string Complemento { get; set; }
            /// <summary>
            /// G033
            /// </summary>
            public string Cidade { get; set; }
            /// <summary>
            /// G034
            /// </summary>
            public string CEP { get; set; }
            /// <summary>
            /// G035
            /// </summary>
            public string ComplementoCEP { get; set; }
            /// <summary>
            /// G036
            /// </summary>
            public string Estado { get; set; }
        }

        public class Informacao
        {
            /// <summary>
            /// G031
            /// </summary>
            public string InformacaoEmrpesa { get; set; }
        }

        public class Nome
        {
            /// <summary>
            /// G013
            /// </summary>
            public string NomeEmpresa { get; set; }

        }

        public class Inscricao
        {
            /// <summary>
            /// G005
            /// </summary>
            public string Tipo { get; set; }////G001
            /// <summary>
            /// G006
            /// </summary>
            public string Numero { get; set; }////G002
        }

        public class Convenio
        {
            /// <summary>
            /// G007
            /// </summary>
            public string CodigoConvenio { get; set; }
        }
        public class ContaCorrente
        {

            public Agencia Agencia { get; set; }

            public Conta Conta { get; set; }
            public DV DigitoVerificadorAgenciaCOnta { get; set; }
        }

        public class Agencia
        {
            /// <summary>
            /// G008
            /// </summary>
            public string Codigo { get; set; }
            /// <summary>
            /// G009
            /// </summary>
            public string DV { get; set; }
        }

        public class Conta
        {
            /// <summary>
            /// G010
            /// </summary>
            public string Codigo { get; set; }
            /// <summary>
            /// G011
            /// </summary>
            public string DV { get; set; }
        }

        public class DV
        {
            /// <summary>
            /// G012
            /// </summary>
            public string Codigo { get; set; }
        }

        public class Controle
        {
            /// <summary>
            /// G001
            /// </summary>
            public string Banco { get; set; }////G001
            /// <summary>
            /// G002
            /// </summary>
            public string Lote { get; set; }////G002
            /// <summary>
            /// G003
            /// </summary>
            public string Registro { get; set; }////G003
        }//

        public class Servico
        {
            /// <summary>
            /// G028
            /// </summary>
            public string Operacao { get; set; }//G028
            /// <summary>
            /// G025
            /// </summary>
            public string TipoServico { get; set; }//G025
            /// <summary>
            /// G029
            /// </summary>
            public string FormaLancamento { get; set; }//G029
            /// <summary>
            /// G030
            /// </summary>
            public string NumeroVersaoLayoutLote { get; set; }//G030
        }//

        public class CNAB
        {
            /// <summary>
            /// G004
            /// </summary>
            public string CNABFREBRABAN { get; set; }//G004
        }//


        public class RegistroDetalheSegmentoJ
        {
            public Controle Controle { get; set; }
            public ServicoSegmentoJ ServicoSegmento { get; set; }//
            public Movimento Movimento { get; set; }//
            public Pagamento Pagamento { get; set; }//
            public NossoNumero NossoNumero { get; set; }//
            public CodigoMoeda CodigoMoeda { get; set; }//
            public CNAB cnab { get; set; }//
            public Ocorrencias Ocorrencias { get; set; }//

        }//

        public class ServicoSegmentoJ
        {
            /// <summary>
            /// G038
            /// </summary>
            public string NumeroRegistro { get; set; }//
            /// <summary>
            /// G039
            /// </summary>
            public string Segmento { get; set; }//
            public Movimento Movimento { get; set; }//


        }//

        public class Movimento
        {
            /// <summary>
            /// G060
            /// </summary>
            public string Tipo { get; set; }//
            /// <summary>
            /// G061
            /// </summary>
            public string Codigo { get; set; }//
        }//

        public class Pagamento
        {
            /// <summary>
            /// G063
            /// </summary>
            public string CodigoBarra { get; set; }//
            /// <summary>
            /// G013
            /// </summary>
            public string NomeCendente { get; set; }//
            /// <summary>
            /// G044
            /// </summary>
            public string DataVencimento { get; set; }//
            /// <summary>
            /// G042
            /// </summary>
            public string ValorTitulo { get; set; }//
            /// <summary>
            /// L002
            /// </summary>
            public string ValorDescontoAbatimento { get; set; }//
            /// <summary>
            /// L003
            /// </summary>
            public string ValorMoraMulta { get; set; }//
            /// <summary>
            /// P009
            /// </summary>
            public string DataPagamento { get; set; }//
            /// <summary>
            /// P010
            /// </summary>
            public string ValorPagamento { get; set; }//
            /// <summary>
            /// G041
            /// </summary>
            public string QuantidadeMoeda { get; set; }//
            /// <summary>
            /// G064
            /// </summary>
            public string ReferenciaSacado { get; set; }//
        }//
        public class NossoNumero
        {
            /// <summary>
            /// G043
            /// </summary>
            public string NumeroAtribuidoBanco { get; set; }//
        }//
        public class CodigoMoeda
        {
            /// <summary>
            /// G065
            /// </summary>
            public string Codigo { get; set; }//
        }//

        public class Ocorrencias
        {
            /// <summary>
            /// G059
            /// </summary>
            public string CodigoOcorrencia { get; set; }//
        }//

        public class NumeroAvisoDebito
        {
            /// <summary>
            /// G066
            /// </summary>
            public string Numero { get; set; }//
        }//

        public class RegistroTrailerPagamento
        {
            public Controle Controle { get; set; }//

            public CNAB cnab { get; set; }//
            public Totais Totais { get; set; }//

            public CNAB cnabTotal { get; set; }//
            public Ocorrencias Ocorrencias { get; set; }//

            public NumeroAvisoDebito NumeroAvisoDebito { get; set; }//

        }//

        public class Totais
        {
            /// <summary>
            /// G057
            /// </summary>
            public string QuantidadeRegistros { get; set; }//
            /// <summary>
            /// P007
            /// </summary>
            public string ValorTotal { get; set; }//
            /// <summary>
            /// G058
            /// </summary>
            public string QuantidadeMoeda { get; set; }//
        }//

    }//
}//
