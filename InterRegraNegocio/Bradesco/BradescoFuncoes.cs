using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InterRegraNegocio.Bradesco.LayoutRemessa;

namespace InterRegraNegocio.Bradesco
{
    public class BradescoFuncoes
    {
        DataTable _dtbSegmentoJ = new DataTable();
        public BradescoFuncoes(DataTable dtbSegmentoJ)
        {
            _dtbSegmentoJ = dtbSegmentoJ;
        }

        public string GeraArquivoRemessa()
        {
            string sRetorno = "";
            int iLote = RetornaCodigoRemessa();

            #region header
            RegistroHeader oRegistroHeader = new RegistroHeader();
            Controle oControle = new Controle();

            oControle.Banco = "237";
            oControle.Lote = iLote.ToString().PadLeft(4, '0');
            oControle.Registro = "0";

            oRegistroHeader.Controle = oControle;

            Servico oServico = new Servico();
            oServico.Operacao = "R";
            oServico.TipoServico = "22";
            oServico.FormaLancamento = "11";
            oServico.NumeroVersaoLayoutLote = "40".PadLeft(3,' ');
            oRegistroHeader.Servico = oServico;

            CNAB oCNAB = new CNAB();
            oCNAB.CNABFREBRABAN = " ";
            oRegistroHeader.CNAB = oCNAB;

            Empresa oEmpresa = new Empresa();

            Inscricao oInscricao = new Inscricao();
            oInscricao.Tipo = "2";
            oInscricao.Numero = "34036601000176".PadLeft(14, ' ');
            oEmpresa.Inscricao = oInscricao;

            Convenio oConvenio = new Convenio();
            oConvenio.CodigoConvenio = "406748"; //todo: codigoconvenio
            oEmpresa.Convenio = oConvenio;

            ContaCorrente oContaCorrente = new ContaCorrente();
            Agencia oAgencia = new Agencia();
            oAgencia.Codigo = "2638".PadLeft(5, ' ');
            oAgencia.DV = " ";
            Conta oConta = new Conta();
            oConta.Codigo = "20453".PadLeft(12, ' ');
            oConta.DV = "6";
            DV oDigito = new DV();
            oDigito.Codigo = "6";

            Nome oNome = new Nome();
            oNome.NomeEmpresa = "2ELETRO COMERCIO E DISTRIBUICA";

            oContaCorrente.Agencia = oAgencia;
            oContaCorrente.Conta = oConta;
            oContaCorrente.DigitoVerificadorAgenciaCOnta = oDigito;
            oEmpresa.ContaCorrente = oContaCorrente;
            oEmpresa.NomeEmpresa = oNome;
            oRegistroHeader.Empresa = oEmpresa;

            Informacao oInformacao = new Informacao();
            oInformacao.InformacaoEmrpesa = ("PAGEMENTO GNRE REFERENTE AO DIA " + DateTime.Now.ToShortDateString()).PadLeft(40, ' ');

            oRegistroHeader.Informacao = oInformacao;

            EnderecoEmpresa oEndereco = new EnderecoEmpresa();
            oEndereco.Logradouro = "Avenida Barao Homem de Melo".PadLeft(30, ' ');
            oEndereco.Numero = "4324".PadLeft(5, ' ');
            oEndereco.Complemento = "PAVMTO2".PadLeft(15, ' ');
            oEndereco.Cidade = "Belo Horizonte".PadLeft(20, ' ');
            oEndereco.ComplementoCEP = "270";
            oEndereco.CEP = "30494";
            oEndereco.Estado = "MG";
            oRegistroHeader.EnderecoEmpresa = oEndereco;

            CNAB oCnabeEndereco = new CNAB();
            oCnabeEndereco.CNABFREBRABAN = "".PadLeft(8, ' ');
            oRegistroHeader.CNABEndereco = oCnabeEndereco;

            Ocorrencias oCorrencias = new Ocorrencias();
            oCorrencias.CodigoOcorrencia = "".PadLeft(10, ' ');
            oRegistroHeader.Ocorrencias = oCorrencias;
            #endregion

            #region segmentoJ
            List<RegistroDetalheSegmentoJ> lstSegmentoJ = new List<RegistroDetalheSegmentoJ>();
            int i = 0;
            double eValorTotal = 0;

            foreach (DataRow r in _dtbSegmentoJ.Rows)
            {
                RegistroDetalheSegmentoJ oSegmentoJ = new RegistroDetalheSegmentoJ();
                Controle oControleSegmento = new Controle();
                oControleSegmento.Banco = "237";
                oControleSegmento.Lote = iLote.ToString().PadLeft(4, '0');
                oControleSegmento.Registro = "3";

                oSegmentoJ.Controle = oControleSegmento;

                ServicoSegmentoJ oServicoSegmentoJ = new ServicoSegmentoJ();
                oServicoSegmentoJ.NumeroRegistro = i.ToString().PadLeft(3, ' ');
                oServicoSegmentoJ.Segmento = "J";

                Movimento oMovimento = new Movimento();
                oMovimento.Tipo = "0";
                oMovimento.Codigo = "00";
                oServicoSegmentoJ.Movimento = oMovimento;

                oSegmentoJ.ServicoSegmento = oServicoSegmentoJ;

                Pagamento oPagemento = new Pagamento();
                oPagemento.CodigoBarra = r["CODIGOBARRA"].ToString().PadLeft(44, ' ');
                oPagemento.NomeCendente = r["NOMECEDENTE"].ToString().PadLeft(30, ' ');
                oPagemento.DataVencimento = r["DATAVENCIMENTO"].ToString().PadLeft(8, ' ');
                oPagemento.ValorTitulo = r["VALORTITULO"].ToString().PadLeft(13);
                oPagemento.ValorDescontoAbatimento = r["VALORDESCONTOABATIMENTO"].ToString().PadLeft(13, ' ');
                oPagemento.ValorMoraMulta = r["VALORMORAMULTA"].ToString().PadLeft(44, ' ');
                oPagemento.DataPagamento = r["DATAPAGAMENTO"].ToString().PadLeft(8, ' ');
                oPagemento.ValorPagamento = r["VALORPAGAMENTO"].ToString().PadLeft(13, ' ');
                oPagemento.QuantidadeMoeda = "1".PadLeft(10);// r["QUANTIDADEMOEDA"].ToString().PadLeft(10);
                oPagemento.ReferenciaSacado = r["REFERENCIASACADO"].ToString().PadLeft(20, ' ');
                oSegmentoJ.Pagamento = oPagemento;

                eValorTotal += double.Parse(r["VALORPAGAMENTO"].ToString());

                oSegmentoJ.NossoNumero = new NossoNumero();
                oSegmentoJ.NossoNumero.NumeroAtribuidoBanco = r["NOSSONUMERO"].ToString().PadLeft(20, ' ');
                oSegmentoJ.CodigoMoeda = new CodigoMoeda();
                oSegmentoJ.CodigoMoeda.Codigo = "09";

                oSegmentoJ.cnab = new CNAB();
                oSegmentoJ.cnab.CNABFREBRABAN = "".PadLeft(6, ' ');
                oSegmentoJ.Ocorrencias = new Ocorrencias();
                oSegmentoJ.Ocorrencias.CodigoOcorrencia = "".PadLeft(10, ' ');
                lstSegmentoJ.Add(oSegmentoJ);
            }

            #endregion

            #region Trailer
            RegistroTrailerPagamento oRegistroTrailer = new RegistroTrailerPagamento();

            Controle oControleTrailer = new Controle();
            oControleTrailer.Banco = "237";
            oControleTrailer.Lote = iLote.ToString().PadLeft(4, '0');
            oControleTrailer.Registro = "9";
            oRegistroTrailer.Controle = oControleTrailer;

            oRegistroTrailer.cnab = new CNAB();
            oRegistroTrailer.cnab.CNABFREBRABAN = "".PadLeft(9, ' ');

            Totais oTotais = new Totais();
            oTotais.QuantidadeRegistros = lstSegmentoJ.Count.ToString().PadLeft(6, ' ');
            oTotais.ValorTotal = eValorTotal.ToString();
            oTotais.QuantidadeMoeda = "1".PadLeft(13, ' ');

            oRegistroTrailer.Totais = oTotais;

            oRegistroTrailer.NumeroAvisoDebito = new NumeroAvisoDebito();
            oRegistroTrailer.NumeroAvisoDebito.Numero = "".PadLeft(6, ' ');

            oRegistroTrailer.cnabTotal = new CNAB();
            oRegistroTrailer.cnabTotal.CNABFREBRABAN = "".PadLeft(165, ' ');

            oRegistroTrailer.Ocorrencias = new Ocorrencias();
            oRegistroTrailer.Ocorrencias.CodigoOcorrencia = "".PadLeft(10, ' ');

            #endregion

            //Gera Layout

            sRetorno = oRegistroHeader.Controle.Banco + oRegistroHeader.Controle.Lote + oRegistroHeader.Controle.Registro + oRegistroHeader.Servico.Operacao + oRegistroHeader.Servico.TipoServico + oRegistroHeader.Servico.FormaLancamento + oRegistroHeader.Servico.NumeroVersaoLayoutLote + oRegistroHeader.CNAB.CNABFREBRABAN;
            sRetorno += oRegistroHeader.Empresa.Inscricao.Tipo + oRegistroHeader.Empresa.Inscricao.Numero + oRegistroHeader.Empresa.ContaCorrente.Agencia.Codigo + oRegistroHeader.Empresa.ContaCorrente.Agencia.DV + oRegistroHeader.Empresa.ContaCorrente.Conta.Codigo + oRegistroHeader.Empresa.ContaCorrente.Conta.DV + oRegistroHeader.Empresa.ContaCorrente.DigitoVerificadorAgenciaCOnta.Codigo + oRegistroHeader.Empresa.NomeEmpresa.NomeEmpresa;
            sRetorno += oRegistroHeader.Informacao.InformacaoEmrpesa;
            sRetorno += oRegistroHeader.EnderecoEmpresa.Logradouro + oRegistroHeader.EnderecoEmpresa.Numero + oRegistroHeader.EnderecoEmpresa.Complemento + oRegistroHeader.EnderecoEmpresa.Cidade + oRegistroHeader.EnderecoEmpresa.CEP + oRegistroHeader.EnderecoEmpresa.ComplementoCEP + oRegistroHeader.EnderecoEmpresa.Estado + oRegistroHeader.CNABEndereco.CNABFREBRABAN + oRegistroHeader.Ocorrencias.CodigoOcorrencia;
            sRetorno += "\r\n";
            foreach (RegistroDetalheSegmentoJ oRegistroSegmentoJ in lstSegmentoJ)
            {
                sRetorno += oRegistroSegmentoJ.Controle.Banco + oRegistroSegmentoJ.Controle.Lote + oRegistroSegmentoJ.Controle.Registro + oRegistroSegmentoJ.ServicoSegmento.NumeroRegistro + oRegistroSegmentoJ.ServicoSegmento.Movimento.Tipo + oRegistroSegmentoJ.ServicoSegmento.Movimento.Codigo;
                sRetorno += oRegistroSegmentoJ.Pagamento.CodigoBarra + oRegistroSegmentoJ.Pagamento.NomeCendente + oRegistroSegmentoJ.Pagamento.DataVencimento + oRegistroSegmentoJ.Pagamento.ValorTitulo + oRegistroSegmentoJ.Pagamento.ValorDescontoAbatimento + oRegistroSegmentoJ.Pagamento.ValorMoraMulta + oRegistroSegmentoJ.Pagamento.DataPagamento + oRegistroSegmentoJ.Pagamento.ValorPagamento + oRegistroSegmentoJ.Pagamento.QuantidadeMoeda + oRegistroSegmentoJ.Pagamento.ReferenciaSacado;
                sRetorno += oRegistroSegmentoJ.NossoNumero.NumeroAtribuidoBanco + oRegistroSegmentoJ.CodigoMoeda.Codigo + oRegistroSegmentoJ.cnab.CNABFREBRABAN + oRegistroSegmentoJ.Ocorrencias.CodigoOcorrencia;
                sRetorno += "\r\n";
            }

            sRetorno += oRegistroTrailer.Controle.Banco + oRegistroTrailer.Controle.Lote + oRegistroTrailer.Controle.Registro + oRegistroTrailer.Totais.QuantidadeRegistros + oRegistroTrailer.Totais.ValorTotal + oRegistroTrailer.Totais.QuantidadeMoeda;
            sRetorno += oRegistroTrailer.NumeroAvisoDebito.Numero + oRegistroTrailer.cnabTotal.CNABFREBRABAN + oRegistroTrailer.Ocorrencias.CodigoOcorrencia;

            File.WriteAllText("C:\\temp\\remessa.rem", sRetorno);

            return sRetorno;
        }

        private int RetornaCodigoRemessa()
        {
            DataTable dtbRemessa = ClasseParametros.ConsultaBancoMysql("SELECT LOTEREMESSABRADESCO FROM CONFIGURACOES");
            int iAtual = int.Parse(dtbRemessa.Rows[0]["LOTEREMESSABRADESCO"].ToString());
            dtbRemessa.Dispose();
            ClasseParametros.ExecutabancoMySql("UPDATE CONFIGURACOES SET LOTEREMESSABRADESCO = LOTEREMESSABRADESCO + 1");

            return iAtual;
        }
    }
}
