using DanfeSharp;
using DanfeSharp.Modelo;
using interRegraNegocio.B2W;
using interRegraNegocio.FortePlus;
using interRegraNegocio.MercadoLivre;
using InterRegraNegocio;
using InterRegraNegocio.B2W;
using InterRegraNegocio.Bradesco;
using InterRegraNegocio.CorreiosLocal;
using InterRegraNegocio.FortePlus;
using InterRegraNegocio.MagazineLuiza;
using InterRegraNegocio.MercadoLivre;

using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterGerente
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        System.Threading.Timer TTimerIniciaProcesso = null;
        bool lRodandoTimer = false;
        bool lSalvaEtiquetaB2W = false;
        bool lEnviaNotasFaturadasB2W = false;
        bool lEnviaNotasFaturadasMAGALU = false;
        bool lRecebePedidoEnviaMercadoLivreGeraEtiqueta = false;
        bool lAtualizaProdutos = false;
        bool lRecebePedidoMAGALUEnviaFortPlus = false;
        bool lEnviaProdutoMAGALU = false;
        bool lEnviaMensagemML = false;
        bool lRecebePedidoEnviaFortPlus = false;
        bool lSolicitaDirect = false;
        bool lEnviaProdutos = false;
        bool lBaixaXML = false;
        bool lServidor = false;
        bool lAviso = false;
        bool lAvisoCancelado = false;
        bool lAtualizaB2W = false;
        bool lEnviaProdutosNovos = false;
        bool lGNRE = false;


        public frmMain()
        {
            InitializeComponent();

            ClasseFuncoes.RecebeMensagemTelegram();
            //ClasseFuncoes.EnviaMensagemTelegramAsync("Teste Mensagem de Notficação!!!\n Confirme se recebeu por favor!");

        }

        private void ClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes oFrm = new frmClientes();
            oFrm.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //ClasseParametros.ConexaoMysql = new SqlConnection(@"Server=RODRIGO-PC\SQLEXPRESS;Database=BaseInfo;Trusted_Connection=True;MultipleActiveResultSets=true;");
            //ClasseParametros.Conexao = new SqlConnection(@"Server=191.252.61.62;Database=doiseletro;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");
            //ClasseParametros.ConexaoCliente = new SqlConnection(@"Server=191.252.61.62;Database=doiseletro;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");
            //ClasseParametros.Conexao.Open();
            //ClasseParametros.ConexaoCliente.Open();
            ClasseParametros.iconApp = iconApp;
            ClasseParametros.oEditLog = editLog;

            if (ClasseFuncoes.ConectaForteplus(5))
            {
                lblStatusFortePlus.Text = "FortPlus Conectado!";
                lblStatusFortePlus.ForeColor = Color.Green;
            }
            else
            {
                lblStatusFortePlus.Text = "FortPlus Não Conectado!";
                lblStatusFortePlus.ForeColor = Color.Red;
            }

            if (ClasseFuncoes.ConectaMercadoLivreAsync(5))
            {
                lblStatusMercadoLivre.Text = "MercadoLivre Conectado!";
                lblStatusMercadoLivre.ForeColor = Color.Green;
            }
            else
            {
                lblStatusMercadoLivre.Text = "MercadoLivre Não Conectado!";
                lblStatusMercadoLivre.ForeColor = Color.Red;
            }

            ClasseFuncoes.RetornaCodigoMAGALU(5);
            ClasseFuncoes.GeraEtiquetaCorreios(5);

            TTimerIniciaProcesso = new System.Threading.Timer(
                                   new TimerCallback(TimerIniciaProcesso),
                                   null,
                                   1000,
                                   5000);
        }

        private void TimerIniciaProcesso(object state)
        {
            if (lRodandoTimer || !lServidor)
                return;
            lRodandoTimer = true;

            IniciaTudo();

            lRodandoTimer = false;
        }

        private void IniciaTudo()
        {
            DataTable d = ClasseParametros.ConsultaBancoMysql("SELECT CODIGO FROM CLIENTE WHERE CODIGO = 5");

            if (d.Rows.Count > 0)
            {
                if (!lGNRE)
                {
                    ThreadStart oThreadStartGNRE = delegate
                    {
                        try
                        {
                            if (chkGNRE.Checked)
                            {
                                lGNRE = true;
                                ClasseFuncoes.GeraGNRE(5);
                                lGNRE = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            lGNRE = false;

                        }
                    };
                    Thread oThreadGNRE = new Thread(oThreadStartGNRE);
                    oThreadGNRE.Start();
                }


                if (!lAtualizaB2W)
                {
                    ThreadStart oThreadStartAtualizaB2W = delegate
                    {
                        try
                        {
                            if (chkAtualizaProdutoB2W.Checked)
                            {
                                lAtualizaB2W = true;
                                ClasseFuncoes.AtualizaB2W(5);
                                lAtualizaB2W = false;

                            }
                        }
                        catch
                        {
                            lAtualizaB2W = false;

                        }
                    };
                    Thread oThreadAtualizaB2W = new Thread(oThreadStartAtualizaB2W);
                    oThreadAtualizaB2W.Start();
                }






                if (!lAtualizaProdutos)
                {
                    ThreadStart oThreadStartAtualizaProdutos = delegate
                    {
                        try
                        {
                            if (chkAtualizaProduto.Checked)
                            {
                                lAtualizaProdutos = true;
                                ClasseFuncoes.AtualizaProdutos(5);
                                lAtualizaProdutos = false;

                            }
                        }
                        catch (Exception ex)
                        {
                            lAtualizaProdutos = false;

                        }
                    };
                    Thread oThreadAtualizaProdutos = new Thread(oThreadStartAtualizaProdutos);
                    oThreadAtualizaProdutos.Start();
                }


                if (!lEnviaProdutos)
                {
                    ThreadStart oThreadStartEnviaProdutoML = delegate
                {
                    try
                    {
                        if (chkEnviaProdutoML.Checked)
                        {
                            lEnviaProdutos = true;
                            ClasseFuncoes.EnviaProdutosAsync(editLog);
                            lEnviaProdutos = false;
                        }
                    }
                    catch
                    {
                        lEnviaProdutos = false;

                    }
                };
                    Thread oThreadEnviaProdutoML = new Thread(oThreadStartEnviaProdutoML);
                    oThreadEnviaProdutoML.Start();
                }



                if (!lBaixaXML)
                {
                    ThreadStart oThreadStartBaixaXML = delegate
                {
                    try
                    {
                        if (chkBaixaXML.Checked)
                        {
                            lBaixaXML = true;
                            ClasseFuncoes.BaixaXML(5);
                            lBaixaXML = false;
                        }
                    }
                    catch
                    {
                        lBaixaXML = false;

                    }
                };
                    Thread oThreadBaixaXML = new Thread(oThreadStartBaixaXML);
                    oThreadBaixaXML.Start();
                }


                if (!lRecebePedidoEnviaMercadoLivreGeraEtiqueta)
                {
                    ThreadStart oThreadStartGeraEtiquetaML = delegate
                {
                    try
                    {
                        if (chkGeraEtiquetaML.Checked)
                        {
                            lRecebePedidoEnviaMercadoLivreGeraEtiqueta = true;
                            ClasseFuncoes.RecebePedidoEnviaMercadoLivreGeraEtiquetaAsync(5);
                            lRecebePedidoEnviaMercadoLivreGeraEtiqueta = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lRecebePedidoEnviaMercadoLivreGeraEtiqueta = false;

                    }
                };
                    Thread oThreadGeraEtiquetaML = new Thread(oThreadStartGeraEtiquetaML);
                    oThreadGeraEtiquetaML.Start();
                }

                if (!lSalvaEtiquetaB2W)
                {
                    ThreadStart oThreadStartGeraEtiquetaB2W = delegate
                    {
                        try
                        {
                            if (chkGeraEtiquetaB2W.Checked)
                            {
                                lSalvaEtiquetaB2W = true;
                                ClasseFuncoes.SalvaEtiquetaB2W(5);
                                lSalvaEtiquetaB2W = false;
                            }
                        }
                        catch
                        {
                            lSalvaEtiquetaB2W = false;

                        }
                    };
                    Thread oThreadGeraEtiquetaB2W = new Thread(oThreadStartGeraEtiquetaB2W);
                    oThreadGeraEtiquetaB2W.Start();
                }


                if (!lSolicitaDirect)
                {
                    ThreadStart oThreadStartDirect = delegate
                {
                    try
                    {
                        if (chkDirect.Checked)
                        {
                            lSolicitaDirect = true;
                            ClasseFuncoes.SolicitaDirect(5);
                            lSolicitaDirect = false;
                        }
                    }
                    catch
                    {
                        lSolicitaDirect = false;

                    }
                };
                    Thread oThreadDirect = new Thread(oThreadStartDirect);
                    oThreadDirect.Start();
                }

                if (!lRecebePedidoEnviaFortPlus)
                {
                    ThreadStart oThreadStartEnviaPedidoMLFortPlu = delegate
                {
                    try
                    {
                        if (chkEnviaPedidoMLFortPlus.Checked)
                        {
                            lRecebePedidoEnviaFortPlus = true;
                            ClasseFuncoes.RecebePedidoEnviaFortPlusAsync(5);
                            lRecebePedidoEnviaFortPlus = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lRecebePedidoEnviaFortPlus = false;

                    }
                };
                    Thread oThreadEnviaPedidoMLFortPlu = new Thread(oThreadStartEnviaPedidoMLFortPlu);
                    oThreadEnviaPedidoMLFortPlu.Start();
                }


                if (!lEnviaMensagemML)
                {
                    ThreadStart oThreadStartMensagem = delegate
                {
                    try
                    {
                        if (chkMensagem.Checked)
                        {
                            lEnviaMensagemML = true;
                            ClasseFuncoes.EnviaMensagemMLAsync(5);
                            lEnviaMensagemML = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lEnviaMensagemML = false;

                    }
                };
                    Thread oThreadMensagem = new Thread(oThreadStartMensagem);
                    oThreadMensagem.Start();
                }

                if (!lEnviaProdutosNovos)
                {
                    ThreadStart oThreadStartEnviaProdutosNovos = delegate
                {
                    try
                    {
                        if (chkEnviaProdutosNovos.Checked)
                        {
                            lEnviaProdutosNovos = true;
                            ClasseFuncoes.EnviaProdutosNovos(editLog);
                            lEnviaProdutosNovos = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lEnviaProdutosNovos = false;

                    }
                };
                    Thread oThreadEnviaProdutosNovos = new Thread(oThreadStartEnviaProdutosNovos);
                    oThreadEnviaProdutosNovos.Start();
                }


                if (!lAtualizaProdutos)
                {
                    ThreadStart oThreadStartAtualizaProdutos = delegate
                    {
                        try
                        {
                            if (chkAtualizaProdutos.Checked)
                            {
                                lAtualizaProdutos = true;
                                ClasseFuncoes.AtualizaEstoque(editLog);
                                lAtualizaProdutos = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            lAtualizaProdutos = false;

                        }
                    };
                    Thread oThreadAtualizaProdutos = new Thread(oThreadStartAtualizaProdutos);
                    oThreadAtualizaProdutos.Start();
                }


                if (!lRecebePedidoMAGALUEnviaFortPlus)
                {
                    ThreadStart oThreadStartPedidoMAGALU = delegate
                {
                    try
                    {
                        if (chkPedidoMAGALU.Checked)
                        {
                            lRecebePedidoMAGALUEnviaFortPlus = true;
                            MAGALUClasseFuncoes.RecebePedidoMAGALUEnviaFortPlus(5);
                            lRecebePedidoMAGALUEnviaFortPlus = false;
                        }
                    }
                    catch
                    {
                        lRecebePedidoMAGALUEnviaFortPlus = false;

                    }
                };
                    Thread oThreadPedidoMAGALU = new Thread(oThreadStartPedidoMAGALU);
                    oThreadPedidoMAGALU.Start();

                }


                if (!lEnviaNotasFaturadasMAGALU)
                {

                    ThreadStart oThreadStartEnviaNotaMAGALU = delegate
                    {
                        try
                        {
                            if (chkEnviaNotaMAGALU.Checked)
                            {
                                lEnviaNotasFaturadasMAGALU = true;
                                MAGALUClasseFuncoes.EnviaNotasFaturadasMAGALU(5);
                                lEnviaNotasFaturadasMAGALU = false;
                            }
                        }
                        catch (Exception EX)
                        {
                            lEnviaNotasFaturadasMAGALU = false;

                        }
                    };
                    Thread oThreadEnviaNotaMAGALU = new Thread(oThreadStartEnviaNotaMAGALU);
                    oThreadEnviaNotaMAGALU.Start();
                }

                if (!lEnviaNotasFaturadasB2W)
                {
                    ThreadStart oThreadStartEnviaNotaB2W = delegate
                {
                    try
                    {
                        if (chkEnviaNotaB2W.Checked)
                        {
                            lEnviaNotasFaturadasB2W = true;
                            ClasseFuncoes.EnviaNotasFaturadasB2W(5);
                            lEnviaNotasFaturadasB2W = false;
                        }
                    }
                    catch
                    {
                        lEnviaNotasFaturadasB2W = false;

                    }
                };
                    Thread oThreadEnviaNotaB2W = new Thread(oThreadStartEnviaNotaB2W);
                    oThreadEnviaNotaB2W.Start();
                }


                if (!lAvisoCancelado)
                {
                    ThreadStart oThreadStartAvisoCancelado = delegate
                    {
                        try
                        {
                            if (chkAvisoCancelado.Checked)
                            {
                                lAvisoCancelado = true;
                                //ClasseFuncoes.EnviaRelatorioTelegramAsync();
                                ClasseFuncoes.EnviaMensagemPedidoCanceladoAsync(5);
                                lAvisoCancelado = false;
                            }
                        }
                        catch
                        {
                            lAvisoCancelado = false;

                        }
                    };
                    Thread oThreadAvisoCancelado = new Thread(oThreadStartAvisoCancelado);
                    oThreadAvisoCancelado.Start();
                }


                //lAviso
            }
        }

        private void CadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CategoriasMLFPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClasseFuncoes.EspelhaCategoriaMercadoLivreFortePlusAsync(barProgresso);
        }

        private void ÉServidorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void NãoÉServidorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void GeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfiguracao oFrm = new frmConfiguracao() { MdiParent = this };
            oFrm.Show();
        }

        private void ProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduto oFrm = new frmProduto() { MdiParent = this };
            oFrm.Show();
        }

        private void ComboEstacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboEstacao.SelectedIndex == 0)
            {
                lServidor = true;
                lRodandoTimer = true;
                IniciaTudo();
                lRodandoTimer = false;

            }
            else
            {
                lServidor = false;
            }
        }

        private void ComboFilial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClasseFuncoes.CarregaFiliais(comboFilial.Text);
        }

        private void Button1_Click(object sender, EventArgs e)
        {



        }

        private void RelatóriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelatorios oFrm = new frmRelatorios();
            oFrm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            IRestResponse oResposta = null;

            int iOffset = 0;
            int ilimit = 50;
            List<Result> lstPedidosMercadoLivre = new List<Result>();
            bool lContinua = true;
            Parameter pToken = new Parameter();

            var p = new Parameter();
            p.Name = "access_token";
            p.Value = ClasseParametros.oMeli.AccessToken;
            var ps = new List<Parameter>();
            ps.Add(p);

            if (ClasseParametros.oUsuarioMercadoLivre == null)
            {
                oResposta = ClasseParametros.oMeli.Get("/users/me", ps);
                if (oResposta.Content.Contains("invalid_token"))
                {
                    ClasseParametros.oMeli.refreshToken(ClasseParametros.oMeli.RefreshToken);
                    oResposta = ClasseParametros.oMeli.Get("/users/me", ps);

                }

                ClasseParametros.oUsuarioMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivreUsuario>(oResposta.Content);
            }


            while (lContinua)
            {
                pToken.Name = "access_token";
                pToken.Value = ClasseParametros.oMeli.AccessToken;
                ps = new List<Parameter>();
                ps.Add(pToken);
                p = new Parameter();
                p.Name = "seller";
                p.Value = ClasseParametros.oUsuarioMercadoLivre.id;
                ps.Add(p);
                p = new Parameter();
                p.Name = "offset";
                p.Value = iOffset;
                ps.Add(p);
                p = new Parameter();
                p.Name = "limit";
                p.Value = ilimit;
                ps.Add(p);

                //offset=3&limit=3
                MercadoLivrePedido oPedidoMercadoLivre = null;

                while (oPedidoMercadoLivre == null)
                {
                    oResposta = ClasseParametros.oMeli.Get("/orders/search", ps);
                    oPedidoMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivrePedido>(oResposta.Content);
                    if (oPedidoMercadoLivre.results == null)
                        ClasseFuncoes.ConectaMercadoLivreAsync(5);
                }
                if (oPedidoMercadoLivre.results == null)
                    return;

                if (oPedidoMercadoLivre.results.Length == 0)
                    break;

                foreach (Result oPedido in oPedidoMercadoLivre.results)
                {
                    if (oPedido.date_created > DateTime.Parse("01/01/2020 00:00:00") && oPedido.date_created < DateTime.Parse("01/02/2020"))
                    {

                        oResposta = null;
                        while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/IdExterno/" + oPedido.id.ToString().Trim());
                            RestRequest request = new RestRequest(Method.GET);
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Accept", "*/*");
                            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                            oResposta = client.Execute(request);

                            if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                ClasseFuncoes.ConectaForteplus(5);
                            }
                            else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                break;
                            }
                        }

                        Pedido oPedidoForPlus = Newtonsoft.Json.JsonConvert.DeserializeObject<Pedido>(oResposta.Content);
                        if (oPedidoForPlus != null)
                        {
                            oResposta = null;
                            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Pedido/Parent/" + oPedidoForPlus.id.ToString().Trim());
                                RestRequest request = new RestRequest(Method.GET);
                                request.AddHeader("Cache-Control", "no-cache");
                                request.AddHeader("Accept", "*/*");
                                request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                request.AddHeader("Content-Type", "application/json");
                                request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                oResposta = client.Execute(request);

                                if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                {
                                    ClasseFuncoes.ConectaForteplus(5);
                                }
                                else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                {
                                    break;
                                }
                            }
                            List<Pedido> oListPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(oResposta.Content);
                            if (oListPedido != null)
                            {
                                oPedidoForPlus = oListPedido[0];


                            }
                        }

                        if (oPedidoForPlus != null)
                        {


                            oResposta = null;
                            while (oResposta == null || oResposta.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                RestClient client = new RestClient(ClasseParametros.sURlFortPlus + "/api/Transmissao/" + oPedidoForPlus.id.ToString().Trim());
                                RestRequest request = new RestRequest(Method.GET);
                                request.AddHeader("Cache-Control", "no-cache");
                                request.AddHeader("Accept", "*/*");
                                request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
                                request.AddHeader("Content-Type", "application/json");
                                request.AddHeader("Authorization", "Bearer " + ClasseParametros.oJsonFortePluslogin.accessToken);

                                oResposta = client.Execute(request);

                                if (oResposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                {
                                    ClasseFuncoes.ConectaForteplus(5);
                                }
                                else if (oResposta.StatusCode == System.Net.HttpStatusCode.NotFound)
                                {
                                    break;
                                }
                            }
                            List<FortPlusXML> oListXML = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusXML>>(oResposta.Content);
                            if (oListXML.Count > 0)
                            {
                                string sXML = oListXML[0].trArquivoRetorno;
                                if (sXML != "")
                                {
                                    string sPasta = Directory.GetCurrentDirectory();
                                    DanfeViewModel oModel = DanfeViewModelCreator.CriarDeStringXml(sXML);

                                    oPedido.DIFAL = double.Parse(oModel.CalculoImposto.vICMSUFDest.ToString());
                                    oModel = null;
                                    lstPedidosMercadoLivre.Add(oPedido);

                                }
                            }
                        }

                    }

                }
                iOffset += 50;
            }







            string sJson = Newtonsoft.Json.JsonConvert.SerializeObject(lstPedidosMercadoLivre);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClasseFuncoes.CorrigeEndereco();
        }

        private void button4_Click(object sender, EventArgs e)
        {




        }

        private void button5_Click(object sender, EventArgs e)
        {

            DataTable d = ClasseParametros.ConsultaBancoMysql(@"SELECT V.NOTAFISCAL
FROM VENDAS V
GROUP BY V.NOTAFISCAL
HAVING COUNT(*) >1");
            int i = 1;

            barProgresso.Maximum = d.Rows.Count;
            barProgresso.Value = 0;

            foreach (DataRow r in d.Rows)
            {
                string sSql = "DELETE FROM VENDAS WHERE NOTAFISCAL = @NOTAFISCAL";
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                ParametrosSQL.Add("NOTAFISCAL", r["NOTAFISCAL"].ToString());
                ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                barProgresso.Value = i;
                i++;
                barProgresso.Refresh();
            }


            d = ClasseParametros.ConsultaBancoMysql("SELECT * FROM VENDAS");
            i = 1;
            barProgresso.Maximum = d.Rows.Count;
            barProgresso.Value = 0;

            foreach (DataRow r in d.Rows)
            {
                string sSql = "update VENDAS set codigo = @codigo where notafiscal = @nota";
                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                ParametrosSQL.Add("codigo", i.ToString());
                ParametrosSQL.Add("nota", r["notafiscal"].ToString());
                ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
                barProgresso.Value = i;
                i++;
                barProgresso.Refresh();
            }

            MessageBox.Show("acabou");


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cancelaProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCancelaProduto oFrm = new frmCancelaProduto() { MdiParent = this };
            oFrm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            //if (oPedido.invoices[0].key == "")
            //{
            //    GeraEtiquetaJADLOG(oPedido);


            //    continue;
            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClasseFuncoes.GeraRelatorioVendasMLAsync();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int iOffset = 1;
            int ilimit = 50;
            Parameter pToken = new Parameter();
            List<Result> lstVendidos = new List<Result>();
            MercadoLivrePedido oPedidoMercadoLivre = null;
            while (true)
            {
                try
                {
                    List<Parameter> ps = new List<Parameter>();
                    ClasseFuncoes.ConectaMercadoLivreAsync(5);
                    pToken.Name = "access_token";
                    pToken.Value = ClasseParametros.oMeli.AccessToken;
                    ps = new List<Parameter>();
                    ps.Add(pToken);
                    Parameter p = new Parameter();
                    p.Name = "seller";
                    p.Value = ClasseParametros.oMeli.UserId;
                    ps.Add(p);
                    p = new Parameter();
                    p.Name = "order.status";
                    p.Value = "paid";
                    ps.Add(p);
                    p = new Parameter();
                    p.Name = "offset";
                    p.Value = iOffset;
                    ps.Add(p);
                    p = new Parameter();
                    p.Name = "limit";
                    p.Value = ilimit;
                    ps.Add(p);
                    //Parameter sPedido = new Parameter();
                    //sPedido.Name = "q";
                    //sPedido.Value = "2463946456";
                    //ps.Add(sPedido);

                    p.Name = "sort";
                    p.Value = "date_desc";
                    ps.Add(p);

                    IRestResponse oResposta = ClasseParametros.oMeli.Get("/orders/search", ps);
                    oPedidoMercadoLivre = Newtonsoft.Json.JsonConvert.DeserializeObject<MercadoLivrePedido>(oResposta.Content);

                    if (oPedidoMercadoLivre.results.Length == 0)
                        break;

                    foreach (Result oPedido in oPedidoMercadoLivre.results)
                    {
                        if (oPedido.id.ToString().Trim() == "2463946456")
                        {

                        }


                        foreach (Order_Items oItem in oPedido.order_items)
                        {
                            if (oItem.item.id == "MLB1474936334")
                            {
                                lstVendidos.Add(oPedido);
                            }



                        }
                    }

                    iOffset++;

                }
                catch
                {

                }
            }
        }

        private void exportaXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExportaXML oForm = new frmExportaXML();
            oForm.ShowDialog();
        }

        private void btnRemessa_Click(object sender, EventArgs e)
        {
            ClasseFuncoes.GeraRemessa();
        }

        private void testeEtiquetaCorreioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ClasseFuncoes.SolitaJADLOG("32200634036601000338550010000142461000510329");
            ClasseFuncoes.VerificaEtiquetaAmazonJADLOG(null);

            List<string> aString = new List<string>();
            aString.Add("LU-8460500661021191");


            foreach (string s in aString)
            {

                MAGALUClasseFuncoes.IntegraManual(s);
            }

            //ClasseCorreios.SolicitaEtiqueta();
            //ClasseCorreios.GeraEtiqueta();
        }

        private void geraRemessaBradescoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtbSegmentoJ = new DataTable();
            dtbSegmentoJ.Columns.Add("CODIGOBARRA");
            dtbSegmentoJ.Columns.Add("NOMECEDENTE");
            dtbSegmentoJ.Columns.Add("DATAVENCIMENTO");
            dtbSegmentoJ.Columns.Add("VALORTITULO");
            dtbSegmentoJ.Columns.Add("VALORDESCONTOABATIMENTO");
            dtbSegmentoJ.Columns.Add("VALORMORAMULTA");
            dtbSegmentoJ.Columns.Add("DATAPAGAMENTO");
            dtbSegmentoJ.Columns.Add("VALORPAGAMENTO");
            dtbSegmentoJ.Columns.Add("REFERENCIASACADO");
            dtbSegmentoJ.Columns.Add("VALORPAGAMENTO");
            dtbSegmentoJ.Columns.Add("NOSSONUMERO");

            InterRegraNegocio.GNRE.TLote_GNREGuiasTDadosGNRE oLote = new InterRegraNegocio.GNRE.TLote_GNREGuiasTDadosGNRE();




            BradescoFuncoes oFuncoes = new BradescoFuncoes(new DataTable());
            oFuncoes.GeraArquivoRemessa();
        }

        private void btnRelatorioMargem_Click(object sender, EventArgs e)
        {
            List<Pedido> lstPedido = ClasseFuncoes.RetornaPedidosFortePlus();
            List<FortPlusXML> lstXMLs = ClasseFuncoes.RetornaListaXMLFortPlus();
            List<FortPlusEstoque> lstEstoque = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FortPlusEstoque>>(ClasseFuncoes.RetornaProdutoEstoqueFortPlus("").Content);
            List<ProdutoFortePlus> lstProdutos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoFortePlus>>(ClasseFuncoes.RetornaProdutosFortPlus().Content);
            IEnumerable<FortPlusXML> lstXMLsFiltrado = lstXMLs.Where(x => ((DateTime)x.trDmaEmissao) > DateTime.Now.AddDays(-90));

            foreach (FortPlusXML oXMl in lstXMLsFiltrado)
            {
                try
                {
                    IEnumerable<Pedido> lstPedidoFiltrado = lstPedido.Where(x => x.mvDocto == oXMl.trDocto && x.mvEntidade == "NFE");
                    Pedido oPedido = lstPedidoFiltrado.ElementAt(0);
                    List<PedidoItemFortPlus> oItens = ClasseFuncoes.RetornaItensPedido(oPedido.id);
                    foreach (PedidoItemFortPlus oItem in oItens)
                    {
                        if (oItem.mtValor > 0)
                        {
                            IEnumerable<ProdutoFortePlus> oProdutoFiltrado = lstProdutos.Where(x => x.id.ToString() == oItem.mtIdProduto.ToString());
                            ProdutoFortePlus oProduto = oProdutoFiltrado.ElementAt(0);

                            IEnumerable<FortPlusEstoque> oEstoqueFiltrado = lstEstoque.Where(x => x.codigo == oProduto.prCodigo);
                            FortPlusEstoque oEstoque = oEstoqueFiltrado.ElementAt(0);

                            string sVendedor = ClasseFuncoes.RetornaFantasiaVendedorFortPlus(oPedido.mvIdVendedor);

                            //FortePlusTabelaPrecoItem oPrecoItem = lstTabelaPreco.ElementAt(0);
                            string sSql = "INSERT INTO VENDASGERAIS (CODIGOVENDA,CODIGOVENDAMARKETPLACE,SKU,DESCRICAOPRODUTO,VALORCUSTO,VALORVENDA,MARKETPLACE,DATAHORAVENDA) ";
                            sSql += " VALUES('" + oXMl.trDocto.ToString() + "','" + oPedido.mvIdExterno + "','" + oProduto.prCodigo + "','" + oProduto.prNome + "','" + oEstoque.custo + "','" + oItem.mtValor + "','" + sVendedor + "','" + ((DateTime)oXMl.trDmaEmissao).ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            ClasseParametros.ExecutabancoMySql(sSql);
                        }
                    }
                }
                catch
                {

                }


            }

            MessageBox.Show("Acabou");










        }
    }
}
