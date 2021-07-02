using DanfeSharp;
using DanfeSharp.Modelo;
using InterRegraNegocio.FortePlus;
using InterRegraNegocio.MercadoLivre;
using MercadoLibre.SDK;
using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telegram.Bot;
using Keys = OpenQA.Selenium.Keys;

namespace InterRegraNegocio
{
    public static class ClasseFinalizaEtiqueta
    {

        public static bool FinalizaEtiqueta(string sPasta)
        {
            bool lRetorno = false;

            try
            {
                string sPastaRetorno = sPasta + "\\13-RetornoOrla\\";
                if (!Directory.Exists(sPastaRetorno))
                    sPastaRetorno = "C:\\CLIENTES\\ORLA\\13-RetornoOrla\\";

                string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "\\";
                DataTable dtbImpressos = ClasseParametros.ConsultaBancoMysql("SELECT CHAVENOTA, CNPJ, NOTA,DATA, XML FROM NOTAMASTER WHERE FINALIZADO = 1 AND RETORNADO = 0");

                foreach (DataRow r in dtbImpressos.Rows)
                {
                    if (!Directory.Exists(sPastaRetorno + sPastaDataHoje))
                        Directory.CreateDirectory(sPastaRetorno + sPastaDataHoje);

                    string sNomeArquivo = r["CHAVENOTA"].ToString() + "_OK.txt";

                    File.WriteAllText(sPastaRetorno + sPastaDataHoje + sNomeArquivo, "OK");

                    ClasseParametros.ExecutabancoMySql("UPDATE NOTAMASTER SET RETORNADO = 1 WHERE CHAVENOTA = '" + r["CHAVENOTA"].ToString() + "'");
                }
            }
            catch (Exception ex)
            {
                ClasseFuncoes.SalvaLogServicos(ex.Message);

            }

            return lRetorno;
        }

    }


    public class ProdutoEtiqueta
    {
        public int Quantidade = 0;
        public string SKU = "";
        public string Descricao = "";
        public string Codigo = "";
        public int FaltandoLer = 0;
    }

    public static class ClasseParametros
    {
        public static Ini oIni = new Ini(Directory.GetCurrentDirectory() + "\\ini.ini");

        public static TextBox oEditLog = null;
        public static string sTokenMAGALU = "";
        public static string sEnderecoMagalu = "https://in.integracommerce.com.br";

        public static string sTokenBling = "";
        public static string sTokenEshopBling = "";

        public static NotifyIcon iconApp = null;
        public static ProgressBar barProgress = null;

        public static FortePluslogin oJsonFortePluslogin = null;
        public static Meli oMeli = null;


        public static string sURlFortPlus = "https://development.forteplus.com.br";
        public static string NomeProjeto = "https://development.forteplus.com.br";


        public static TelegramBotClient oClienteTelegram = new TelegramBotClient("1131875535:AAGHhDVIw4-CEX9k3YSRxRAgLGvYMe1fBaA");

        public static int iFilial = -1;
        public static string sUsuarioSistema = "";
        public static int iCodigoUsuarioSistema = -1;
        public static string sTokenJADLOG = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOjc2MzM3LCJkdCI6IjIwMTkwODIzIn0.68zANntX5PrwtqgNwrJOTBuWezwjgnyAqOVAJz3wJx4";

        public static string sUsuarioCorreios = "sigep";
        public static string sSenhaCorreios = "n5f9t8";
        public static string sCodigoAdministrativoCorreios = "17000190";
        public static string sContratoCorreios = "9992157880";
        public static string sCartaoCorreios = "0067599079";
        public static string sCNPJCorreios = "34028316000103";
        public static string sCodigoServicoCorreios = "";


        public static string sBanco = "";
        public static string sIP = "";
        public static string sUsuario = "";
        public static string sSenha = "";

        public static string sEtiquetaJadLog = @"^XA

^FO50,100^GFA,2700,2700,30,N07,M01F8,M03FE,M0IF8,L03IFC,L07JF,K01KFC,K03KFE,K0MF8,J03MFE,J07NF,I01OFC,I07OFE,I0QF8,003QFE,007RF,01SFC,07SFE,0UF8,0UF8gG0FF07F8,1UFgH0FF07F8,1TFEM01ER01FF07F8,07SFCM03FR01FF07F8,03SF8M03FR01FF07F8,00RFEN07F8Q01FF07F8,007QFCN07F8Q01FF07F8,201QF002L07F8Q01FF07F8,380PFE00FL07F8Q01FF07F8,7C03OF801FL03F8Q01FF07F8,FF01OF007FL03FR01FF07F8,FF807MFC00FFL03FR01FF07F8,FFE01MF803FFM04R01FF07F8,IF00LFE00IFQ018L07IF07F8I02M0FE,IFC03KFC01IFL03C007FFCJ03JF07F8007FFK0JF,IFE01KF007IFL07F83JFJ0KF07F801IFCI03JFC,JF807JF00JFL07F83JF8001KF07F803IFEI07JFE,JFC03JF01JFL07F83JFC007KF07F807JF800KFE,KF00JF87JFL07F83JFE00LF07F80KF801KFE,KF807IFDKFL07F83KF00LF07F81KFC03KFE,KFE03OFL07F87KF01LF07F83KFE03KFE,LF00OFL07F87KF81LF07F83KFE07KFE,LFC03NFL07F83KF83LF07F83LF07KFE,LFE01NFL07F83F01FF83LF07F87FF8IF0IFC7FE,MF807MFL07F83I0FF83LF07F87FE03FF0FFE01FE,MFC07MFL07F8J07F83FFC3FF07F87FC01FF8FFC01FE,MFE07MFL07F8J0FF87FF01FF07F8FF800FF8FF801FE,MFE07MFL07F803JFC7FF01FF07F87F8007F8FF001FE,MFE07MFL07F80KFC7FE01FF07F87FI07F8FF001FE,MFE07MFL07F83KFC7FE01FF07F87FI07F8FF001FE,:MFE07MFL07F87KFC7FE01FF07F87FI07F8FF001FE,:MFE07MFL07F8LFC7FE01FF07F87FI07F8FF001FE,:MFE07MFL07F8LFC7FE01FF07F87F800FF8FF801FE,MFE07MFL07F8LFC7FE01FF07F87FC00FF87FC01FE,MFE07MFL07F8FFC0FFC3FF01FF07F83FE01FF07FE01FE,MFE07MFL07F8FF807FC3FFE3FF07F83FF07FF07FFC1FE,MFE07MFL07F8FFE07FC1LF07F83LF03IF1FE,MFE07MFL07F8LFC1LF07F81LF03IF1FE,MFE07MFL07F87KFC0LF07F81KFE03IF1FE,MFE07MFL07F87KFC07KF07F80KFC01IF1FE,MFE07MFL07F87KFC03KF07F807JF800IF1FE,MFE07MFL07F83KFC01KF07F803JFI07FF1FE,MFE07MFL07F83KFC007JF07F801IFCI03FF1FE,MFE07MFL07F81KFC003IFE03FI07FF8J0FF1FE,7LFE07MFL07F807JF8I07FF001EI01F8K01E1FE,7LFE07MFL07F801IFCgJ01FE,7LFE07LFEL0FF8gP03FE,3LFE07LFCI0183FF8gM0400FFE,0LFE07LF8I01JF8gM07JFC,07KFE07KFEJ01JFgN07JFC,01KFE07KFCJ01JFgN07JFC,00KFE07KFK01JFgN0KF8,003KF07JFEK01IFEgN0KF8,I0KF0KF8K01IFEgN0KF,I07PFL01IFCgN0JFE,I01OFEL01IF8gN0JFC,J0OF8L01IFgO07IF,J03NFN07F8gO01FF8,J01MFC,K07LF8,K03KFE,L0KF8,L07JF,L03IFE,M0IF8,M07FE,M01FC,N0F,^FS

^FO30,30^GB750,1150,3^FS

^FO320,50^BY4,2.0,00^BQN,2,5^F{qrcode}^FS

^CF0,25
^FO50,270^FDNota: {Nota}^FS
^FO50,295^FDPedido: {Pedido}^FS

^FO300,270^FDContrato: {Contrato}^FS

^BY3,2,150
^FO150,380^BC^FD{Tracking}^FS
^FO50,570^FDRecebedor: _______________________________________________^FS
^FO50,600^FDAssinatura: __________________^FS
^FO400,600^FDDocumento: __________________^FS

^FO30,672^GB750,370,3^FS
^FO30,672^GB200,20,30^FS
^FO40,678^AN,25,25^FR^FDDESTINATARIO^FS
^FO70,710^AN,25,25^FR^FD{NomeDestinatario}^FS
^FO70,740^AN,25,25^FR^FD{EndererecoNumeroDestinatario}^FS
^FO70,770^AN,25,25^FR^FD{BairroDestinatario}^FS
^FO70,800^AN,25,25^FR^FD{ComplementoDestinatario}^FS
^FO70,830^AN,25,25^FR^FD{CidadeEstadoDestinatario}^FS

^BY2,2,150
^FO70,850^BC^FD{Cep}^FS

^FO50,1060^AN,25,25^FR^FDRemetente: {NomeRemetente}^FS
^FO50,1090^AN,25,25^FR^FD{EnderecoNumeroRemetente}^FS
^FO50,1120^AN,25,25^FR^FD{ComplementoRemetente}^FS
^FO50,1150^AN,25,25^FR^FD{CEPCidadeEstadoRemetente}^FS
^XZ";

        public static string sEtiquetaFEDEX = @"^XA

^FO50,50^GFA,2625,2625,25,,:::::::::::::::::::::::::::::I01LFCR01NFE,:::::::::I01FFEV01KF,::::I01FFEK03FF8J07FC1KF,I01LF81JFI01IF1NFEIF8007FFC,I01LF83JFC007IFDNFEIFC00IFC,I01LF8KFE00SFE7FFE01IF8,I01LF9LF01SFE3FFE01IF,I01LFBLF83SFE3IF03FFE,I01LFBLF87SFE1IF87FFC,I01OF81FFC7SFE0IFCIF8,I01NFE00FFCIFC3OFE07LF,I01NFE007FEIF00OFE03KFE,I01NFC007JFE007NFE01KFE,I01IF001PFE007KF8K0KFC,I01FFE001PFC003KFL07JF8,I01FFE001PFC003KFL03JF,I01FFE001PFC003KFL03IFE,I01FFE001PFC003KFL01IFC,I01FFE001PFC003KFM0IFC,I01FFE001PFC003KFL01IFE,I01FFE001FFCJ01FFC003KFL03JF,I01FFE001FFCJ01FFC003KFL07JF8,I01FFE001FFCJ01FFE007NFE00KFCI01F,I01FFEI0FFCJ01FFE007NFE01KFEI020C,I01FFEI0FFE00FFEIF80OFE03KFEI04F6,I01FFEI07FF01FFEIFC3OFE07LFI0D92,I01FFEI07LFC7SFE0IFDIF8009B2,I01FFEI03LF87SFE0IF8IFC009E2,I01FFEI03LF83SFE1IF07FFE00992,I01FFEI01LF01SFE3FFE03IF00592,I01FFEJ0KFE00SFE7FFC01IF80414,I01FFEJ03JFC007IF9NFEIF800IFC0318,I01FFEK0JFI03IF1RF8007FFC00F,R03FFCJ07FC,,:::::::::::::::::::::::::::^FS
^FO30,30^GB750,1150,3^FS

^FO320,50^BY4,2.0,00^BQN,2,5^F{qrcode}^FS

^CF0,25
^FO50,270^FDNota: {Nota}^FS
^FO50,295^FDPedido: {Pedido}^FS

^FO300,270^FDContrato: {Contrato}^FS

^FO50,570^FDRecebedor: _______________________________________________^FS
^FO50,600^FDAssinatura: __________________^FS
^FO400,600^FDDocumento: __________________^FS

^FO30,672^GB750,370,3^FS
^FO30,672^GB200,20,30^FS
^FO40,678^AN,25,25^FR^FDDESTINATARIO^FS
^FO70,710^AN,25,25^FR^FD{NomeDestinatario}^FS
^FO70,740^AN,25,25^FR^FD{EndererecoNumeroDestinatario}^FS
^FO70,770^AN,25,25^FR^FD{BairroDestinatario}^FS
^FO70,800^AN,25,25^FR^FD{ComplementoDestinatario}^FS
^FO70,830^AN,25,25^FR^FD{CidadeEstadoDestinatario}^FS

^BY2,2,150
^FO70,850^BC^FD{Cep}^FS

^FO50,1060^AN,25,25^FR^FDRemetente: {NomeRemetente}^FS
^FO50,1090^AN,25,25^FR^FD{EnderecoNumeroRemetente}^FS
^FO50,1120^AN,25,25^FR^FD{ComplementoRemetente}^FS
^FO50,1150^AN,25,25^FR^FD{CEPCidadeEstadoRemetente}^FS
^XZ";

        public static string sEtiquetaCorreios = @"^XA
^FO50,50^GB700,450,3^FS
^CF0,30
^FO218,150^FDUSO EXCLUSIVO DOS CORREIOS^FS
^CF0,20
^FO158,190^FDCole aqui a etiqueta com o codigo identificador da encomenda^FS
^FO50,500^GB700,1,3^FS
^CF0,25
^FO50,510^FDRecebedor: ______________________________________________^FS
^FO50,550^FDAssinatura: ___________________^FS
^FO420,550^FDDocumento: ________________^FS

^FO50,590^GB700,80,3^FS
^FO50,590^GB460,20,30^FS
^FO60,595^AN,25,25^FR^FDENTREGA NO VIZINHO AUTORIZADA?^FS


^FO50,672^GB450,350,3^FS
^FO50,672^GB200,20,30^FS
^FO60,678^AN,25,25^FR^FDDESTINATARIO^FS
^FO70,710^AN,25,25^FR^FD{0}^FS
^FO70,740^AN,25,25^FR^FD{1}^FS
^FO70,770^AN,25,25^FR^FD{2}^FS
^FO70,800^AN,25,25^FR^FD{3}^FS

^BY3,1,150
^FO80,830^BC^FD{2}^FS

^FO505,618^BY4,2.0,65^BQN,2,10^FD{2}^FS

^FO70,1030^AN,25,25^FR^FD{4}^FS
^FO70,1060^AN,25,25^FR^FD{5}^FS
^FO70,1090^AN,25,25^FR^FD{6}^FS
^FO70,1120^AN,25,25^FR^FD{7}^FS
^XZ";

        public static MercadoLivreUsuario oUsuarioMercadoLivre = null;
        public static string EmailFortPlus { get; internal set; }
        public static string ClienteSelecionado { get; set; }
        public static bool lPermiteReimpressao { get; set; }
        public static bool lImpressaoRomaneioRetrato { get; set; }
        public static bool lImpressaoPorSKU { get; set; }


        // Mensagens para clientes
        public static string sMensagemAcabouComprar = @"Ola {0} , a equipe 2ELETRO agradece sua compra. Em instantes iniciaremos a emissão da nota fiscal e preparação do pedido para expedição.
ATENÇÃO
Qualquer problema, duvida ou sugestão, por gentileza entrar em contato diretamente conosco no telefone abaixo ANTES DE ABRIR UM CHAMADO PELO MERCADO LIVRE (RECLAMAÇÃO) onde seguirá um atendimento padrão que demora muito. Garantimos que estaremos prontos para solucionar qualquer necessidade rapidamente e com eficiência.
Atendimento ao Cliente
Whatsapp Business: (27) 993113781 –  EQUIPE 2ELETRO
Horário de funcionamento: Segunda a sexta das 08:00 ás 17:00 hrs.

LEMBRAMOS QUE OS CORREIOS ESTÃO TRABALHANDO COM PESSOAL REDUZIDO  E ATRASOS  PODEM OCORRER. PEDIMOS UM POUCO DE COMPREENSÃO NESTE MOMENTO TÃO DIFICIL.";

        public static string sMensagemNotaGerada = @"Olá, {0} segue a nota fiscal. Seu pedido já foi enviado para expedição.
ATENÇÃO
Qualquer problema, duvida ou sugestão, por gentileza entrar em contato diretamente conosco no telefone abaixo ANTES DE ABRIR UM CHAMADO PELO MERCADO LIVRE(RECLAMAÇÃO) onde seguirá um atendimento padrão que demora muito.Garantimos que estaremos prontos para solucionar qualquer necessidade rapidamente e com eficiência.
Atendimento ao Cliente
Whatsapp Business: (27) 993113781 –  EQUIPE 2ELETRO
Horário de funcionamento: Segunda a sexta das 08:00 ás 17:00 hrs.

LEMBRAMOS QUE OS CORREIOS ESTÃO TRABALHANDO COM PESSOAL REDUZIDO E ATRASOS PODEM OCORRER.PEDIMOS UM POUCO DE COMPREENSÃO NESTE MOMENTO TÃO DIFICIL.";

        public static string sMensagemPedidoEnviado = @"Ola {0}, enviamos seu pedido conforme rastreio {1}. Qualquer duvida entre em contato que estaremos prontos para atende-lo.
ATENÇÃO
Qualquer problema, duvida ou sugestão, por gentileza entrar em contato diretamente conosco no telefone abaixo ANTES DE ABRIR UM CHAMADO PELO MERCADO LIVRE (RECLAMAÇÃO) onde seguirá um atendimento padrão que demora muito. Garantimos que estaremos prontos para solucionar qualquer necessidade rapidamente e com eficiência.
Atendimento ao Cliente
Whatsapp Business: (27) 993113781 –  EQUIPE 2ELETRO
Horário de funcionamento: Segunda a sexta das 08:00 ás 17:00 hrs.
A equipe 2ELETRO agradece a preferencia.
LEMBRAMOS QUE OS CORREIOS ESTÃO TRABALHANDO COM PESSOAL REDUZIDO  E ATRASOS  PODEM OCORRER. PEDIMOS UM POUCO DE COMPREENSÃO NESTE MOMENTO TÃO DIFICIL.";

        public static string sMensagemPedidoEntregue = @"Ola {0}, identificamos que seu pedido foi entregue. Esperamos que  tenha tido uma excelente experiencia . Se tiver alguma duvida, sugestão ou reclamação, por favor envie-nos uma mensagem que atenderemos imediatamente. Evite abrir reclamação, pois pode retardar o atendimento. 
Atendimento ao Cliente
Whatsapp Business: (27) 993113781 –  EQUIPE 2ELETRO
Horário de funcionamento: Segunda a sexta das 08:00 ás 17:00 hrs.
Se puder nos avaliar positivamente ficaremos gratos. A equipe 2ELETRO agradece a preferencia.

LEMBRAMOS QUE OS CORREIOS ESTÃO TRABALHANDO COM PESSOAL REDUZIDO  E ATRASOS  PODEM OCORRER. PEDIMOS UM POUCO DE COMPREENSÃO NESTE MOMENTO TÃO DIFICIL.";

        public static int PegaCodigo(string sDescricao, string sCodigoCliente)
        {
            int iRetorno = 0;
            string sSql = "";
            DataTable d = ConsultaBancoMysql("SELECT CONTADOR FROM CONTADORES WHERE DESCRICAO = '" + sDescricao + "' AND CODIGOCLIENTE = '" + sCodigoCliente + "' ");

            if (d.Rows.Count == 0)
            {
                sSql = "INSERT INTO CONTADORES(DESCRICAO,CONTADOR,CODIGOCLIENTE) " +
                   " VALUES('" + sDescricao + "', 1, '" + sCodigoCliente + "')";
                ExecutabancoMySql(sSql);
                iRetorno = 1;
            }
            else
            {
                iRetorno = int.Parse(d.Rows[0]["CONTADOR"].ToString()) + 1;
                sSql = "UPDATE CONTADORES SET CONTADOR='" + iRetorno.ToString() + "' WHERE DESCRICAO='" + sDescricao + "' AND CODIGOCLIENTE = " + sCodigoCliente;
                //ParametrosSQL.Add("DESCRICAO", sLoja);
                ExecutabancoMySql(sSql);
                //ParametrosSQL.Clear();
                //d = ConsultaBancoMysql("SELECT CONTADOR FROM CONTADORES WHERE DESCRICAO = '" + sLoja + "'", null, TransacaoConexao);
                //iRetorno = int.Parse(d.Rows[0]["CONTADOR"].ToString());
            }

            d.Dispose();


            return iRetorno;
        }


        public static int PegaLote(string sLoja, string sCodigoCliente)
        {

            int iRetorno = 0;
            string sSql = "";
            DataTable d = ConsultaBancoMysql("SELECT CONTADOR FROM CONTADORES WHERE DESCRICAO = '" + sLoja + "' AND CODIGOCLIENTE = '" + sCodigoCliente + "' ");

            if (d.Rows.Count == 0)
            {
                sSql = "INSERT INTO CONTADORES(DESCRICAO,CONTADOR,CODIGOCLIENTE) " +
                   " VALUES('" + sLoja + "', 1, '" + sCodigoCliente + "')";
                ExecutabancoMySql(sSql);
                iRetorno = 1;
            }
            else
            {
                iRetorno = int.Parse(d.Rows[0]["CONTADOR"].ToString());
            }

            sSql = "SELECT COUNT(*) AS CONTAGEM FROM VENDAS WHERE LOJA ='" + sLoja + "' AND LOTE = '" + iRetorno.ToString() + "' AND CODIGOCLIENTE = '" + sCodigoCliente + "'";
            d = ConsultaBancoMysql(sSql);
            int iTotal = int.Parse(d.Rows[0]["CONTAGEM"].ToString());
            if (iTotal >= 100)
            {
                iRetorno++;
                sSql = "UPDATE CONTADORES SET CONTADOR='" + iRetorno.ToString() + "' WHERE DESCRICAO='" + sLoja + "' AND CODIGOCLIENTE = " + sCodigoCliente;
                //ParametrosSQL.Add("DESCRICAO", sLoja);
                ExecutabancoMySql(sSql);
                //ParametrosSQL.Clear();
                //d = ConsultaBancoMysql("SELECT CONTADOR FROM CONTADORES WHERE DESCRICAO = '" + sLoja + "'", null, TransacaoConexao);
                //iRetorno = int.Parse(d.Rows[0]["CONTADOR"].ToString());
            }

            d.Dispose();


            return iRetorno;
        }

        private static byte[] DescampactaEtiqueta(string sNotaFiscal, byte[] aEtiqueta)
        {
            byte[] aRetorno = { };
            try
            {
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\temp.zip", aEtiqueta); // Requires System.IO
                string sPastaDescompactado = Directory.GetCurrentDirectory() + "\\temp";

                if (!Directory.Exists(sPastaDescompactado))
                {
                    Directory.CreateDirectory(sPastaDescompactado);
                }

                string sArquivoZIP = Directory.GetCurrentDirectory() + "\\temp.zip";
                File.Delete(sPastaDescompactado + "\\Controle.pdf");
                File.Delete(sPastaDescompactado + "\\Etiqueta de envio.txt");
                File.Delete(sPastaDescompactado + "\\Etiquetas.zpl");
                File.Delete(sPastaDescompactado + "\\plp.pdf");

                ZipFile.ExtractToDirectory(sArquivoZIP, sPastaDescompactado, Encoding.UTF8);
                byte[] aArquivo = { };
                if (File.Exists(sPastaDescompactado + "\\Etiqueta de envio.txt"))
                {
                    aRetorno = File.ReadAllBytes(sPastaDescompactado + "\\Etiqueta de envio.txt");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + sNotaFiscal + ".txt", aRetorno); // Requires System.IO
                }
                else if (File.Exists(sPastaDescompactado + "\\Etiquetas.zpl"))
                {
                    aRetorno = File.ReadAllBytes(sPastaDescompactado + "\\Etiquetas.zpl");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + sNotaFiscal + ".txt", aRetorno); // Requires System.IO
                }
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(ex.Message);
            }

            return aRetorno;
        }

        public static bool SalvaEtiqueta(string sNotaFiscal, byte[] aArquivo, int iCliente, string sPedido, string sLOJA)
        {

            bool lRetorno = false;
            string sSql = "";
            byte[] aEtiqueta = DescampactaEtiqueta(sNotaFiscal, aArquivo);

            try
            {
                DataTable d = ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + sNotaFiscal + "'");
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

                if (d.Rows.Count > 0)
                {
                    sSql = "UPDATE VENDAS SET ETIQUETATXT=@ETQ,ETIQUETATXTTXT=@ETIQUETATXTTXT,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE,DATACRIADO=@DATACRIADO WHERE NOTAFISCAL = @NOTA";

                    ParametrosSQL.Add("ETQ", aEtiqueta);
                    ParametrosSQL.Add("ETIQUETATXTTXT", Encoding.UTF8.GetString(aEtiqueta));
                    ParametrosSQL.Add("NOTA", sNotaFiscal);
                    ParametrosSQL.Add("LOJA", sLOJA);
                    ParametrosSQL.Add("CODIGOCLIENTE", iCliente);
                    ParametrosSQL.Add("DATACRIADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                }
                else
                {
                    sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE,ETIQUETATXTTXT,PEDIDO) VALUES(@NOTA,@ETQ,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE,@ETIQUETATXTTXT,@PEDIDO)";

                    ParametrosSQL.Add("ETQ", aEtiqueta);
                    ParametrosSQL.Add("ETIQUETATXTTXT", Encoding.UTF8.GetString(aEtiqueta));
                    ParametrosSQL.Add("NOTA", sNotaFiscal);
                    ParametrosSQL.Add("LOJA", sLOJA);
                    ParametrosSQL.Add("CODIGOCLIENTE", iCliente);
                    ParametrosSQL.Add("DATACRIADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    ParametrosSQL.Add("LOTE", PegaLote("MELI", iCliente.ToString()));
                    ParametrosSQL.Add("PEDIDO", sPedido);

                }
                d.Dispose();

                ExecutabancoMySql(sSql, ParametrosSQL);
                ParametrosSQL.Clear();

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                }

                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + sNotaFiscal + ".TXT", aEtiqueta); // Requires System.IO

                lRetorno = true;
            }
            catch (Exception e)
            {
                MostraErro(e.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(e.Message);
            }
            finally
            {
                //TransacaoConexao.Dispose();
            }


            return lRetorno;
        }


        public static void MostraErro(string sErro, NotifyIcon iconApp)
        {
            try
            {
                if (iconApp != null)
                {
                    iconApp.BalloonTipText = "Erro:\n" + sErro;
                    iconApp.BalloonTipTitle = "Info Placer";
                    iconApp.BalloonTipIcon = ToolTipIcon.Warning;
                    iconApp.ShowBalloonTip(5000);
                    using (System.IO.StreamWriter file =
                 new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\erro.txt", true))
                    {
                        file.WriteLine(sErro);
                    }
                }
            }
            catch (Exception ex)
            {
                ClasseFuncoes.SalvaLogServicos(ex.Message);
            }
        }

        //public static bool Executabanco(string sSql, SqlConnection oConexaoInterna, Dictionary<string, object> dictSql = null)
        //{
        //    int iRetorno = -1;
        //    bool lRetorno = false;
        //    SqlTransaction TransacaoConexao = null;
        //    try
        //    {
        //        TransacaoConexao = oConexaoInterna.BeginTransaction();
        //        SqlCommand oCmd = new SqlCommand();
        //        try
        //        {
        //            oCmd.CommandText = sSql;
        //            oCmd.Connection = oConexaoInterna;
        //            oCmd.Transaction = TransacaoConexao;
        //            if (dictSql != null)
        //                foreach (KeyValuePair<string, object> pairSql in dictSql)
        //                {
        //                    SqlParameter oParametro = new SqlParameter();
        //                    oParametro.ParameterName = pairSql.Key;
        //                    oParametro.Value = CorrigeParametro(pairSql.Value);
        //                    if (pairSql.Key.Contains("DATA") && pairSql.Value == null)
        //                        oParametro.Value = DateTime.Parse("01/01/1900 00:00:00");
        //                    if (oParametro.Value == "")
        //                        oParametro.Value = string.Empty;
        //                    //oParametro.Value = oParametro.Value.ToString().Replace(",", ".");
        //                    //if (pairSql.Key.Contains("@"))
        //                    oCmd.Parameters.Add(oParametro);
        //                    //else
        //                    //oCmd.Parameters.Add("@" + pairSql.Key, pairSql.Value);
        //                }
        //            oCmd.ExecuteNonQuery();

        //            TransacaoConexao.Commit();
        //            //if (sSql.Contains("INSERT"))
        //            //{
        //            //    // If has last inserted id, add a parameter to hold it.
        //            //    if (oCmd. != null) oCmd.Parameters.Add(new MySqlParameter("newId", oCmd.LastInsertedId));

        //            //    // Return the id of the new record. Convert from Int64 to Int32 (int).
        //            //    iRetorno = Convert.ToInt32(oCmd.Parameters["@newId"].Value);
        //            //}
        //            lRetorno = true;
        //        }
        //        catch (SqlException e)
        //        {
        //            TransacaoConexao.Rollback();
        //            MostraErro(e.Message, iconApp);
        //            ClasseFuncoes.SalvaLogServicos(e.Message);
        //        }
        //        catch (Exception e)
        //        {
        //            TransacaoConexao.Rollback();
        //            MostraErro(e.Message, iconApp);
        //            ClasseFuncoes.SalvaLogServicos(e.Message);
        //        }
        //        finally
        //        {
        //            TransacaoConexao.Dispose();
        //            oCmd.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MostraErro(ex.Message, iconApp);
        //        ClasseFuncoes.SalvaLogServicos(ex.Message);
        //    }
        //    if (dictSql != null)
        //        dictSql.Clear();
        //    return lRetorno;
        //}

        public static object CorrigeParametro(object oObject)
        {
            return oObject == null ? DBNull.Value : oObject;

        }

        public static bool ExecutabancoMySql(string sSql, Dictionary<string, object> dictSql = null)
        {
            int iRetorno = -1;
            bool lRetorno = false;
            MySqlConnection ConexaoMysql = null;

            if (sBanco.Contains("interplace"))
            {
                ConexaoMysql = new MySqlConnection(@"server=" + sIP + ";uid=" + sUsuario + ";pwd=interplace2020;database=" + sBanco);
            }
            else if (sBanco.Contains("frete"))
            {
                ConexaoMysql = new MySqlConnection(@"server=" + sIP + ";uid=" + sUsuario + ";pwd=Interplace2020;database=" + sBanco);

            }
            else
            {
                ConexaoMysql = new MySqlConnection(@"server=" + sIP + ";uid=" + sUsuario + ";pwd=" + sSenha + ";database=" + sBanco);
            }

            try
            {
                ConexaoMysql.Open();

                //TransacaoConexaoMysql = ConexaoMysql.BeginTransaction();
                MySqlCommand oCmd = new MySqlCommand();
                try
                {
                    oCmd.CommandText = sSql;
                    oCmd.Connection = ConexaoMysql;
                    //oCmd.Transaction = TransacaoConexaoMysql;
                    if (dictSql != null)
                        foreach (KeyValuePair<string, object> pairSql in dictSql)
                        {
                            MySqlParameter oParametro = new MySqlParameter();
                            oParametro.ParameterName = pairSql.Key;
                            oParametro.Value = CorrigeParametro(pairSql.Value);
                            if (pairSql.Key.Contains("DATA") && pairSql.Value == null)
                                oParametro.Value = DateTime.Parse("01/01/1900 00:00:00");
                            if (oParametro.Value == "")
                                oParametro.Value = string.Empty;
                            //oParametro.Value = oParametro.Value.ToString().Replace(",", ".");
                            //if (pairSql.Key.Contains("@"))
                            oCmd.Parameters.Add(oParametro);
                            //else
                            //oCmd.Parameters.Add("@" + pairSql.Key, pairSql.Value);
                        }
                    oCmd.ExecuteNonQuery();

                    //TransacaoConexaoMysql.Commit();
                    //if (sSql.Contains("INSERT"))
                    //{
                    //    // If has last inserted id, add a parameter to hold it.
                    //    if (oCmd. != null) oCmd.Parameters.Add(new MySqlParameter("newId", oCmd.LastInsertedId));

                    //    // Return the id of the new record. Convert from Int64 to Int32 (int).
                    //    iRetorno = Convert.ToInt32(oCmd.Parameters["@newId"].Value);
                    //}
                    lRetorno = true;
                }

                catch (Exception e)
                {
                    //TransacaoConexaoMysql.Rollback();
                    MostraErro(e.Message, iconApp);
                    ///    ClasseFuncoes.SalvaLogServicos(e.Message);
                }
                finally
                {
                    //TransacaoConexao.Dispose();
                    oCmd.Dispose();
                    if (ConexaoMysql.State == ConnectionState.Open)
                    {
                        ConexaoMysql.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(ex.Message);
            }
            if (dictSql != null)
                dictSql.Clear();
            return lRetorno;
        }

        public static void CarregaParametros()
        {
            DataTable dtbLogin = ClasseParametros.ConsultaBancoMysql("SELECT * FROM LOGIN WHERE CODIGO = " + ClasseParametros.iCodigoUsuarioSistema.ToString());

            if (dtbLogin.Rows.Count > 0)
            {
                ClasseParametros.sUsuarioSistema = dtbLogin.Rows[0]["USUARIO"].ToString();
                ClasseParametros.iCodigoUsuarioSistema = int.Parse(dtbLogin.Rows[0]["CODIGO"].ToString());
                ClasseParametros.lPermiteReimpressao = int.Parse(dtbLogin.Rows[0]["PERMITEREIMPRESSAO"].ToString()) == 1;
                ClasseParametros.lImpressaoRomaneioRetrato = int.Parse(dtbLogin.Rows[0]["IMPRESSAORETRATOROMANEIO"].ToString()) == 1;
                ClasseParametros.lImpressaoPorSKU = int.Parse(dtbLogin.Rows[0]["IMPRESSAOEANSKU"].ToString()) == 1;
            }
        }

        public static DataTable ConsultaBancoMysql(string sSql, Dictionary<string, object> dictSql = null)
        {
            DataTable dtbRetorno = new DataTable();
            MySqlCommand oCmd = new MySqlCommand();
            MySqlConnection ConexaoMysql = null;
            if (sBanco.Contains("interplace"))
            {
                ConexaoMysql = new MySqlConnection(@"server=" + sIP + ";uid=" + sUsuario + ";pwd=interplace2020;database=" + sBanco);
            }
            else if (sBanco.Contains("frete"))
            {
                ConexaoMysql = new MySqlConnection(@"server=" + sIP + ";uid=" + sUsuario + ";pwd=Interplace2020;database=" + sBanco);

            }
            else
            {
                ConexaoMysql = new MySqlConnection(@"server=" + sIP + ";uid=" + sUsuario + ";pwd=" + sSenha + ";database=" + sBanco);
            }

            try
            {
                ConexaoMysql.Open();

                //if (oTrInterno == null)
                //    TransacaoConexaoMysql = ConexaoMysql.BeginTransaction();
                //else
                //    TransacaoConexaoMysql = oTrInterno;
                oCmd.CommandText = sSql;
                oCmd.Connection = ConexaoMysql;
                //oCmd.Transaction = TransacaoConexaoMysql;

                if (dictSql != null)
                    foreach (KeyValuePair<string, object> pairSql in dictSql)
                    {
                        MySqlParameter oParametro = new MySqlParameter();
                        oParametro.ParameterName = pairSql.Key;
                        oParametro.Value = pairSql.Value;
                        //if (pairSql.Key.Contains("@"))
                        oCmd.Parameters.Add(oParametro);
                        //else
                        //oCmd.Parameters.Add("@" + pairSql.Key, pairSql.Value);
                    }

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(oCmd);
                objAdapter.ResetFillLoadOption();
                objAdapter.Fill(dtbRetorno);
                objAdapter.Dispose();

                //if (oTrInterno == null)
                //    TransacaoConexaoMysql.Commit();
            }
            catch (Exception e)
            {
                try
                {

                    if (e.Message == "SqlConnection não dá suporte para transações paralelas.")
                    {
                        ConexaoMysql.Close();
                        ConexaoMysql.Open();
                    }

                }
                catch
                {

                }

                if (e.Message.Contains("fechada"))
                {
                    ConexaoMysql.Close();
                    ConexaoMysql.Open();
                }

                MostraErro(e.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(e.Message);

            }
            finally
            {
                oCmd.Dispose();
                if (ConexaoMysql.State == ConnectionState.Open)
                {
                    ConexaoMysql.Close();
                }
            }
            if (dictSql != null)
                dictSql.Clear();

            return dtbRetorno;
        }

        public static bool SalvaEtiquetaAnyMarket(string sNotaFiscal, byte[] aEtiqueta, string sLoja, int iCliente, string sNomeMarketplace, string sPedido)
        {

            string sSql = "";
            bool lRetorno = false;

            try
            {
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

                DataTable d = ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + sNotaFiscal + "'");
                ParametrosSQL.Add("ETQ", aEtiqueta);
                ParametrosSQL.Add("NOTA", sNotaFiscal);
                ParametrosSQL.Add("ETIQUETATXTTXT", Encoding.UTF8.GetString(aEtiqueta));
                ParametrosSQL.Add("LOJA", sLoja.ToUpper());
                ParametrosSQL.Add("CODIGOCLIENTE", iCliente);
                ParametrosSQL.Add("MARKETPLACE", sNomeMarketplace);
                ParametrosSQL.Add("PEDIDO", sPedido);
                ParametrosSQL.Add("DATACRIADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (d.Rows.Count > 0)
                {
                    sSql = "UPDATE VENDAS SET ETIQUETATXT=@ETQ,ETIQUETATXTTXT=@ETIQUETATXTTXT,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE,MARKETPLACE=@MARKETPLACE,PEDIDO=@PEDIDO WHERE NOTAFISCAL = @NOTA";
                }
                else
                {
                    sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE,MARKETPLACE,PEDIDO,ETIQUETATXTTXT) VALUES(@NOTA,@ETQ,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE,@MARKETPLACE,@PEDIDO,@ETIQUETATXTTXT)";

                    ParametrosSQL.Add("LOTE", PegaLote(sLoja, iCliente.ToString()));

                }

                ExecutabancoMySql(sSql, ParametrosSQL);


                d.Dispose();
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                }

                if (aEtiqueta == null)
                    return lRetorno;
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + sNotaFiscal + ".TXT", aEtiqueta); // Requires System.IO

                lRetorno = true;
            }
            catch (Exception e)
            {
                MostraErro(e.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(e.Message);
            }
            finally
            {
            }

            return lRetorno;


        }

        public static void SalvaBancoPDFZPLMagalu(Order oPedido, string sUsuario, string sSenha)
        {


            string sAPI = ClasseFuncoes.Base64Encode(sUsuario + ":" + sSenha);
            string sBody = @"{
  'Format': 'ZPL',
  'Orders': [
    '" + oPedido.IdOrder + "' " +
    "]    }";

            var client = new RestClient(ClasseParametros.sEnderecoMagalu + "/api/Order/ShippingLabels");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Length", "71");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", "Basic " + sAPI);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", sBody, ParameterType.RequestBody);
            Thread.Sleep(1500);
            IRestResponse response = client.Execute(request);

            client = null;
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return;
            }

            //EtiquetaMAGALU oJson = Newtonsoft.Json.JsonConvert.DeserializeObject<EtiquetaMAGALU>(response.Content);

            List<EtiquetaMAGALU> oEtiqueta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EtiquetaMAGALU>>(response.Content);

            string sSite = oEtiqueta[0].Url;
            using (var clientWeb = new WebClient())
            {
                clientWeb.DownloadFile(sSite, Directory.GetCurrentDirectory() + "\\tempmagalu.zip");
            }
            DescampactaSalvaEtiquetaMAGALU(oPedido, oEtiqueta[0]);

        }



        private static void DescampactaSalvaEtiquetaMAGALU(Order oPedido, EtiquetaMAGALU oEtiqueta)
        {


            try
            {
                //File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\temp.zip", aEtiqueta); // Requires System.IO
                string sPastaDescompactado = Directory.GetCurrentDirectory() + "\\temp";

                if (!Directory.Exists(sPastaDescompactado))
                {
                    Directory.CreateDirectory(sPastaDescompactado);
                }

                string sArquivoZIP = Directory.GetCurrentDirectory() + "\\tempmagalu.zip";

                File.Delete(sPastaDescompactado + "\\plp.pdf");

                File.Delete(sPastaDescompactado + "\\etiquetas.zpl");

                ZipFile.ExtractToDirectory(sArquivoZIP, sPastaDescompactado, Encoding.UTF8);

                byte[] aArquivoZPL = { };
                if (File.Exists(sPastaDescompactado + "\\etiquetas.zpl"))
                {
                    aArquivoZPL = File.ReadAllBytes(sPastaDescompactado + "\\etiquetas.zpl");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + oPedido.InvoicedKey + ".zpl", aArquivoZPL); // Requires System.IO
                }

                byte[] aArquivoPDF = { };
                if (File.Exists(sPastaDescompactado + "\\plp.pdf"))
                {
                    aArquivoPDF = File.ReadAllBytes(sPastaDescompactado + "\\plp.pdf");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLPDF\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLPDF\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLPDF\\" + oPedido.InvoicedKey + ".pdf", aArquivoPDF); // Requires System.IO
                }

                try
                {
                    DataTable d = ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + oPedido.InvoicedKey + "'");
                    Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                    ParametrosSQL.Add("@ETQ", aArquivoZPL);
                    ParametrosSQL.Add("@ETIQUETATXTTXT", Encoding.UTF8.GetString(aArquivoZPL));
                    ParametrosSQL.Add("@ETIQUETAPDF", aArquivoPDF);
                    ParametrosSQL.Add("@NOTA", oPedido.InvoicedKey);
                    ParametrosSQL.Add("@LOJA", "MAGAZINE_LUIZA");
                    ParametrosSQL.Add("@CODIGOCLIENTE", oPedido.CodigoCliente);
                    ParametrosSQL.Add("@MARKETPLACE", oPedido.MarketplaceName);
                    ParametrosSQL.Add("@PEDIDO", oPedido.IdOrder);
                    ParametrosSQL.Add("@TRACKINGETIQUETA", oEtiqueta.Orders[0].TrackingCode);

                    string sSql = "";

                    if (d.Rows.Count > 0)
                    {
                        sSql = "UPDATE VENDAS SET ETIQUETATXT=@ETQ,ETIQUETATXTTXT=@ETIQUETATXTTXT,ETIQUETAPDF=@ETIQUETAPDF,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE,MARKETPLACE=@MARKETPLACE,PEDIDO=@PEDIDO,TRACKINGETIQUETA=@TRACKINGETIQUETA WHERE NOTAFISCAL = @NOTA";
                    }
                    else
                    {
                        sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE,MARKETPLACE,PEDIDO,ETIQUETATXTTXT,ETIQUETAPDF,TRACKINGETIQUETA) VALUES(@NOTA,@ETQ,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE,@MARKETPLACE,@PEDIDO,@ETIQUETATXTTXT,@ETIQUETAPDF,@TRACKINGETIQUETA)";

                        ParametrosSQL.Add("@DATACRIADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        ParametrosSQL.Add("@LOTE", PegaLote("MAGAZINE_LUIZA", oPedido.CodigoCliente.ToString()));

                    }
                    d.Dispose();

                    ExecutabancoMySql(sSql, ParametrosSQL);

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQMAGAZINELUIZA\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQMAGAZINELUIZA\\");
                    }

                    if (aArquivoPDF == null || aArquivoZPL == null)
                        return;

                }
                catch (Exception e)
                {
                    MostraErro(e.Message, iconApp);
                    ClasseFuncoes.SalvaLogServicos(e.Message);

                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(ex.Message);

            }

        }

        private static void DescampactaSalvaEtiquetaMAGALU(string oPedido, EtiquetaMAGALU oEtiqueta, string sChaveNota)
        {


            try
            {
                //File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\temp.zip", aEtiqueta); // Requires System.IO
                string sPastaDescompactado = Directory.GetCurrentDirectory() + "\\temp";

                if (!Directory.Exists(sPastaDescompactado))
                {
                    Directory.CreateDirectory(sPastaDescompactado);
                }

                string sArquivoZIP = Directory.GetCurrentDirectory() + "\\tempmagalu.zip";

                File.Delete(sPastaDescompactado + "\\plp.pdf");

                File.Delete(sPastaDescompactado + "\\etiquetas.zpl");

                ZipFile.ExtractToDirectory(sArquivoZIP, sPastaDescompactado, Encoding.UTF8);

                byte[] aArquivoZPL = { };
                if (File.Exists(sPastaDescompactado + "\\etiquetas.zpl"))
                {
                    aArquivoZPL = File.ReadAllBytes(sPastaDescompactado + "\\etiquetas.zpl");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQ\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQ\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + sChaveNota + ".zpl", aArquivoZPL); // Requires System.IO
                }

                byte[] aArquivoPDF = { };
                if (File.Exists(sPastaDescompactado + "\\plp.pdf"))
                {
                    aArquivoPDF = File.ReadAllBytes(sPastaDescompactado + "\\plp.pdf");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLPDF\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLPDF\\");
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLPDF\\" + sChaveNota + ".pdf", aArquivoPDF); // Requires System.IO
                }

                try
                {
                    DataTable d = ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + sChaveNota + "'");
                    Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                    ParametrosSQL.Add("@ETQ", aArquivoZPL);
                    ParametrosSQL.Add("@ETIQUETATXTTXT", Encoding.UTF8.GetString(aArquivoZPL));
                    ParametrosSQL.Add("@ETIQUETAPDF", aArquivoPDF);
                    ParametrosSQL.Add("@NOTA", sChaveNota);
                    ParametrosSQL.Add("@LOJA", "MAGAZINE_LUIZA");
                    ParametrosSQL.Add("@CODIGOCLIENTE", "5");
                    ParametrosSQL.Add("@MARKETPLACE", "MAGAZINE_LUIZA");
                    ParametrosSQL.Add("@PEDIDO", oPedido);
                    ParametrosSQL.Add("@TRACKINGETIQUETA", oEtiqueta.Orders[0].TrackingCode);

                    string sSql = "";

                    if (d.Rows.Count > 0)
                    {
                        sSql = "UPDATE VENDAS SET ETIQUETATXT=@ETQ,ETIQUETATXTTXT=@ETIQUETATXTTXT,ETIQUETAPDF=@ETIQUETAPDF,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE,MARKETPLACE=@MARKETPLACE,PEDIDO=@PEDIDO,TRACKINGETIQUETA=@TRACKINGETIQUETA WHERE NOTAFISCAL = @NOTA";
                    }
                    else
                    {
                        sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE,MARKETPLACE,PEDIDO,ETIQUETATXTTXT,ETIQUETAPDF,TRACKINGETIQUETA) VALUES(@NOTA,@ETQ,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE,@MARKETPLACE,@PEDIDO,@ETIQUETATXTTXT,@ETIQUETAPDF,@TRACKINGETIQUETA)";

                        ParametrosSQL.Add("@DATACRIADO", DateTime.Now);
                        ParametrosSQL.Add("@LOTE", PegaLote("MAGAZINE_LUIZA", "5".ToString()));

                    }
                    d.Dispose();

                    ExecutabancoMySql(sSql, ParametrosSQL);

                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\XMLETQMAGAZINELUIZA\\"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\XMLETQMAGAZINELUIZA\\");
                    }

                    if (aArquivoPDF == null || aArquivoZPL == null)
                        return;

                }
                catch (Exception e)
                {
                    MostraErro(e.Message, iconApp);
                    ClasseFuncoes.SalvaLogServicos(e.Message);

                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                MostraErro(ex.Message, iconApp);
                ClasseFuncoes.SalvaLogServicos(ex.Message);

            }

        }

        public static void PegaControles(Control oControl, ref Dictionary<int, Control> lstControle)
        {
            foreach (Control oC in oControl.Controls)
            {
                if (oC.HasChildren)
                {
                    PegaControles(oC, ref lstControle);
                }
                else
                {
                    lstControle.Add(lstControle.Count + 1, oC);
                }
            }
        }


    }
}
