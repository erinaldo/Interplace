
using Correios;
using InterRegraNegocio.ServicoCorreios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exception = System.Exception;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Xml;

namespace InterRegraNegocio.CorreiosLocal
{
    public static class ClasseCorreiosLocal
    {
        public static void SolicitaEtiqueta()
        {
            //ServicoCorreios.solicitaEtiquetas oEtiquetas = new ServicoCorreios.solicitaEtiquetas("C",);
            using (var correios = new ServicoCorreios.AtendeClienteClient())
            {
                DateTime d = correios.obterClienteAtualizacao("34036601000176", "sigep", "n5f9t8");
                contratoERP oContrato = correios.buscaContrato("9912477530", 20007183, "sigep", "n5f9t8");
                d = correios.obterClienteAtualizacao("22955980000461", "a22955980a", "8d0p1j");

                d = correios.obterClienteAtualizacao("22955980000119", "229559800001", "triwht");
                clienteERP oCliente = correios.buscaCliente("9912477530", "0075239175 ", "sigep", "n5f9t8"); // 2eletro

                oCliente = correios.buscaCliente("9912480328", "0075304740", "a22955980a", "8d0p1j"); // 22955980000461 - espirito santo
                oCliente = correios.buscaCliente("9912483304", "0075363364", "229559800001", "triwht");// 22955980000119 - rio de janeiro
                var o = correios.getStatusCartaoPostagem("0075304740", "229559800001", "triwht");
            }
        }

        public static void ConsultaRastreio(string sRastreio)
        {
            //using (var correios = new ServicoCorreios.validarPostagemSimultanea((sRastreio))
            //{
            //    var consulta = correios.consultaCEP(sCEP);


            //}

            //ServicoCorreios.servicoSigep o = new servicoSigep();

            //o..rastro a = new correios.rastro();
            //correios.sroxml xml = new correios.sroxml();
            //xml = a.buscaEventos("usuario", "senha", "tipo", "resultado", "lingua", "objetos");
            //Console.WriteLine(xml.objeto);
            //Console.ReadKey();


        }


        public static void ConsultaCEP(string sCEP)
        {
            using (var correios = new ServicoCorreios.AtendeClienteClient())
            {
                var consulta = correios.consultaCEP(sCEP);


            }

        }

        public static void RetornaPLP(string sUsuario, string sSenha)
        {
            sUsuario = "sigep";

            sSenha = "n5f9t8";


        }

        public static void GeraEtiqueta()
        {
            try
            {
                //sUsuario = "sigep";
                //sSenha = "n5f9t8";

                //var service = new CorreiosApi();
                //Correios.CorreiosServiceReference.clienteERP cliente = service.buscaCliente("9912480328", "0075304740", "a22955980a", "8d0p1j"); // 22955980000461 - espirito santo

                using (var correios = new ServicoCorreiosProducao.AtendeClienteClient())
                {
                    var oCliente = correios.buscaCliente("9912480328", "0075304740", "a22955980a", "8d0p1j"); // 22955980000461 - espirito santo

                    string s = correios.solicitaXmlPlp(0000000000000000000, "a22955980a", "8d0p1j");

                    oCliente = correios.buscaCliente("9912483304", "0075363364", "229559800001", "triwht");   // 22955980000119 - rio de janeiro
                    var o = correios.getStatusCartaoPostagem("0075304740", "a22955980a", "8d0p1j");

                    s = correios.solicitaEtiquetas("C", "22955980000461", 109819, 1, "a22955980a", "8d0p1j");

                    //var etiqueta = new CorreiosLabel("ME", "0001", "005", "123456");

                    //var sender = new Sender("Luar Faria", "QMS 17 casa 2 Cond. Mini chacaras", "sobradinho", "Setor de mansões", "73062708", "Brasilia", "DF");

                    //var receiver = new Receiver("Luar Faria", "QMS 17 casa 2 Cond. Mini chacaras", "sobradinho", "Setor de mansões", "73062708", "Brasilia", "DF");

                    //etiqueta.Generate("JH980121092BR", sender, receiver, CorreiosLabel.LabelType.PAC, Directory.GetCurrentDirectory() + "\\sedex-logo.png");

                    // var caminho = etiqueta.ExportPDF();


                }
            }
            catch
            {

            }
        }


        public static void GeraEtiquetaCorreios(string sCartaoPostagem, XmlNodeList nodesRemetente, XmlNode nodeObjetoPostal, string sChaveNota, string sCodigoCliente)
        {
            //sUsuario = "sigep";
            //sSenha = "n5f9t8";

            //var service = new CorreiosApi();
            //Correios.CorreiosServiceReference.clienteERP cliente = service.buscaCliente("9912480328", "0075304740", "a22955980a", "8d0p1j"); // 22955980000461 - espirito santo

            XmlNode nodeDestinatario = nodeObjetoPostal.SelectNodes("destinatario")[0];
            XmlNode nodeNacional = nodeObjetoPostal.SelectNodes("nacional")[0];
            XmlNode nodeRemetente = nodesRemetente[0];


            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
            string sEtiqueta = "";
            try
            {
                 sEtiqueta = @"^XA

^FO50,50^GFA,1300,1300,13,QFEK0NF,RFK0NF,RFK07MF,RF8J07MF,RFCJ03MF,:RFEJ01MF,:SFK0MF,SFK07LF,SF8J07LF,SFCJ03LF,:SFEJ01LF,:TFK0LF,TFK07KF,TF8J07KF,TFCJ03KF,:TFEJ01KF,IFEJ01C9249EJ0192493,IFEJ0191001CJ0392493,IFCJ0392493CJ0324927,IF8J03249278J0724927,IF8J0724927K0E4924F,IFK0E4924FK0E4921F,IFK0E4924EJ01C9249F,FFEJ01C9249EJ0193213F,FFEJ01D9259CJ0392493F,FFCJ0392493CJ0324927F,FF8J0326D278J0724927F,FF8J0724927K064924FF,FFK0E4924FK0EC925FF,FFK0E4924EJ01C9249FF,FEJ01C9249EJ019B253FF,FEJ01D9249CJ0392493FF,FCJ0392493CJ0326D27FF,F8J0336DB78J0724927FF,F8J0724927K064924IF,FK064D24FK0E4925IF,FK0E4924EK0C9249IF,EJ01C9249EJ01DB25BIF,LFC9B6DTF,LFE4924TF,MF6C967SF,MF24927SF,MF92493SF,:MFC9249SF,MFC9B49SF,MFE4924SF,MFE6496SF,NF24927RF,NF92493RF,:NFC9249RF,:NFE4924RF,NFE6C86RF,OF24927QF,OF92493QF,:OFC9249QF,:gKF,::::::::,::::::::00F81FC0FE07E001C7004,03F83FF0FF87F80FCF03FC01C,07F87FF8FFC7FC1FCF0FFE03C,0FF8IF8FFC7FC3FCF1IF07C,0F01F07CF3C79E7E0F1F1F8F8,1E01E03CF1C79E780F3C07CF,1E03C01EF3C71EF80F3C03DE,1C03C01EF3C73CFFCF7803DE,1C03C01EFF87FCFFCF7803CE,1C03C01EFF87F8FFCF7803CF,1E03C01EFF07F0FFCF7803CF,1E01E03CFF07F0F80F3C03C78,0F01F07CFF87F8780F3C07C3C,0FF8IFCF7C77C7E0F3E0F83C,07F8IF8F3E73E3FCF1IF81C,03F87FF0F1E71E1FCF0IF01C,00F81FC0F0C70E0FCF07FC01C,^FS

^CF0,29
^FO180,50^FDCartao de Postagem^FS
^FO180,80^FD{0}^FS
^FO180,115^FDFornec: {1}^FS
^FO180,155^FD{2}^FS
^FO180,195^FDVolume^FS
^FO180,235^FD{3}^FS
^FO500,50^GB250,150,3^FS
^FO600,70^FD{4}^FS
^FO500,110^FD{5}^FS
^FO500,150^FD{6}^FS
^CF0,23
^FO580,200^FDCORREIOS^FS

^BY3,2,180
^FO100,280^BC^FD{3}^FS
^CF0,30
^FO50,550^FDDestinatario^FS
^CF0,25
^FO60,590^FD{7}^FS
^FO60,630^FD{8}^FS
^FO60,670^FD{9}^FS
^FO60,710^FD{10}^FS

^BY3,2,180
^FO100,750^BC^FD{11}^FS
^CF0,30

^FO50,1000^FDRemetente^FS
^CF0,25
^FO60,1040^FD{12}^FS
^FO60,1080^FD{13}^FS
^FO60,1120^FD{14}^FS
^FO60,1160^FD{15}^FS

^XZ";
                sEtiqueta = string.Format(sEtiqueta, sCartaoPostagem, "001", "005", nodeObjetoPostal.SelectSingleNode("numero_etiqueta").InnerText, "PAC", "", "ME", nodeDestinatario.SelectSingleNode("nome_destinatario").InnerText,
                    nodeDestinatario.SelectSingleNode("logradouro_destinatario").InnerText + " - " + nodeDestinatario.SelectSingleNode("numero_end_destinatario").InnerText,
                    nodeNacional.SelectSingleNode("bairro_destinatario").InnerText + "," + nodeDestinatario.SelectSingleNode("complemento_destinatario").InnerText,
                    nodeNacional.SelectSingleNode("cep_destinatario").InnerText + "," + nodeNacional.SelectSingleNode("cidade_destinatario").InnerText + "-" + nodeNacional.SelectSingleNode("uf_destinatario").InnerText,
                    nodeNacional.SelectSingleNode("cep_destinatario").InnerText, nodeRemetente.SelectSingleNode("nome_remetente").InnerText, nodeRemetente.SelectSingleNode("logradouro_remetente").InnerText,
                nodeRemetente.SelectSingleNode("bairro_remetente").InnerText + "," + nodeRemetente.SelectSingleNode("complemento_remetente").InnerText,
                  nodeRemetente.SelectSingleNode("cep_remetente").InnerText + "," + nodeRemetente.SelectSingleNode("cidade_remetente").InnerText + "-" + nodeRemetente.SelectSingleNode("uf_remetente").InnerText);
            }
            catch(Exception ex)
            {

            }


            string sNumeroNota = nodeNacional.SelectSingleNode("numero_nota_fiscal").InnerText;
            string sSql = "";
            DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS WHERE NOTAFISCAL = '" + sChaveNota + "'");
            if (d.Rows.Count > 0)
            {
                sSql = "UPDATE VENDAS SET ETIQUETATXTTXT = @ETIQUETATXTTXT, ETIQUETATXT=@ETQ,LOJA=@LOJA,CODIGOCLIENTE=@CODIGOCLIENTE WHERE NOTAFISCAL = @NOTA";
            }
            else
            {
                sSql = "INSERT INTO VENDAS(NOTAFISCAL,ETIQUETATXT,ETIQUETATXTTXT,LOJA,DATACRIADO,LOTE,CODIGOCLIENTE) VALUES(@NOTA,@ETQ,@ETIQUETATXTTXT,@LOJA,@DATACRIADO,@LOTE,@CODIGOCLIENTE)";
                ParametrosSQL.Add("@DATACRIADO", DateTime.Now);
                ParametrosSQL.Add("@LOTE", ClasseParametros.PegaLote("CORREIOS", sCodigoCliente));
            }
            d.Dispose();
            byte[] aEtiqueta = null;
            aEtiqueta = Encoding.UTF8.GetBytes(sEtiqueta);

            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\XMLETQ\\" + sChaveNota + ".TXT", aEtiqueta); // Requires System.IO

            ParametrosSQL.Add("@ETQ", aEtiqueta);
            ParametrosSQL.Add("@ETIQUETATXTTXT", sEtiqueta);
            ParametrosSQL.Add("@NOTA", sChaveNota);
            ParametrosSQL.Add("@LOJA", "CORREIOS");
            ParametrosSQL.Add("@CODIGOCLIENTE", sCodigoCliente);
            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);

            //string sPastaXML = sParametro + "\\07-EnvioSaidaNF\\";
            //string sPastaEtiqueta = sParametro + "\\12-Etiquetas\\";

            //if (sPastaXML.ToLower().Contains("multifast"))
            //{
            //    sPastaXML += "Pendentes\\";
            //    sPastaEtiqueta += "Pendentes\\";
            //}

            //if (!Directory.Exists(sPastaXML))
            //{
            //    sPastaXML = "C" + sPastaXML.Substring(1);
            //    sPastaEtiqueta = "C" + sPastaEtiqueta.Substring(1);

            //}

            //if (!Directory.Exists(sPastaEtiqueta + "\\JSONTXTBACKUP\\"))
            //{
            //    Directory.CreateDirectory(sPastaEtiqueta + "\\JSONTXTBACKUP\\");
            //}

            //string sPastaEtiquetaBackup = sPastaEtiqueta + "\\JSONTXTBACKUP\\";
            //string sPastaDataHoje = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "\\";
            //if (!Directory.Exists(sPastaEtiquetaBackup + sPastaDataHoje))
            //{
            //    Directory.CreateDirectory(sPastaEtiquetaBackup + sPastaDataHoje);
            //}


            //File.WriteAllText(sPastaEtiquetaBackup + sPastaDataHoje + sChaveNota + ".txt",sEtiqueta);
            //File.Delete(sCaminho);

        }

        public static enderecoERP RetornaEnderecoPorCEP(string sCEP)
        {
            enderecoERP oEndereco = null;

            try
            {
                using (var correios = new ServicoCorreios.AtendeClienteClient())
                {
                    oEndereco = correios.consultaCEP(sCEP);
                }
            }
            catch (Exception ex)
            {

            }
            return oEndereco;
        }
    }
}
