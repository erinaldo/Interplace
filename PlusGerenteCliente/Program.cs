using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlusGerenteCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
                ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
                ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
                DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql("SELECT PORANYMARKET,USUARIOMAGALU,SENHAMAGALU,PORARQUIVO, CODIGO, CLIENTE, IDMERCADOLIVRE, SENHAMERCADOLIVRE, CODEMERCADOLIVRE, " +
                        "                                                         ACCESSTOKENMERCADOLIVRE,REFRESHTOKENMERCADOLIVRE,KEYB2W,USUARIOB2W,REMETENTE,ENDERECO,CEP,CIDADE,UF,GUMGATOKENANYMARKET," +
                        "                                                         PASTA, MATRIZ, USUARIOFTP" +
                        "                                                  FROM CLIENTE WHERE MATRIZ <> 1 ORDER BY PORARQUIVO DESC");

                Dictionary<int, ThreadStart> dictThread = new Dictionary<int, ThreadStart>();

                foreach (DataRow r in dtbCliente.Rows)
                {
                    if(r["USUARIOFTP"].ToString() =="")
                    {
                        continue;
                    }





                    if (!dictThread.ContainsKey(int.Parse(r["CODIGO"].ToString())))
                    {
                        //ThreadStart oThreadStartAtualizaProdutos = delegate
                        //{
                        try
                        {
                            //ClasseFuncoes.ConectaMercadoLivre(int.Parse(r["CODIGO"].ToString()));
                            ClasseFuncoes.GeraEtiquetaClientesAsync(int.Parse(r["CODIGO"].ToString()));
                            //SalvaB2W(r["CODIGO"].ToString());
                            ClasseFinalizaEtiqueta.FinalizaEtiqueta(r["PASTA"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro: " + ex.Message);
                        }
                        finally
                        {
                            //dictThread.Remove(int.Parse(r["CODIGO"].ToString()));
                        }

                        //};
                        //Thread oThreadAtualizaProdutos = new Thread(oThreadStartAtualizaProdutos);
                        //oThreadAtualizaProdutos.Start();

                        //dictThread.Add(int.Parse(r["CODIGO"].ToString()), oThreadStartAtualizaProdutos);
                    }

                    //int iPorArquivo = 0;
                    //if (r["PORARQUIVO"].ToString() != "")
                    //    iPorArquivo = int.Parse(r["PORARQUIVO"].ToString());

                    //if (iPorArquivo == 1)
                    //    if (!lRodandoTimerFinalizaEtiqueta)
                    //    {
                    //        ThreadStart oThreadStartFinalizaEtiqueta = delegate
                    //        {
                    //            lRodandoTimerFinalizaEtiqueta = true;
                    //            lRodandoTimerFinalizaEtiqueta = false;
                    //        };
                    //        Thread oThreadFinalizaEtiqueta = new Thread(oThreadStartFinalizaEtiqueta);
                    //        oThreadFinalizaEtiqueta.Start();
                    //    }
                }

                Thread.Sleep(10000);
            }



        }
    }
}
