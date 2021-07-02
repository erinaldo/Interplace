using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusEtiquetasBling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Geração de Etiqueta");
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            ClasseParametros.ClienteSelecionado = "";
            int iCodigoCliente = -1;
            DataTable dtbCliente = new DataTable();

            while (true)
            {
                Console.WriteLine("Digite o código do cliente: ");
                string sCodigo = Console.ReadLine();

                iCodigoCliente = int.Parse(sCodigo);

                dtbCliente = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE WHERE CODIGO = " + sCodigo);
                if (dtbCliente.Rows.Count > 0)
                {
                    Console.WriteLine("Cliente selecionado " + dtbCliente.Rows[0]["CLIENTE"].ToString() + ", está certo? (S/N)");
                    string sResposta = Console.ReadLine();
                    if (sResposta == "S")
                    {
                        ClasseParametros.ClienteSelecionado = dtbCliente.Rows[0]["CLIENTE"].ToString();
                        break;
                    }
                }
            }

            Console.Title = "Sistema do cliente " + ClasseParametros.ClienteSelecionado;


            ClasseParametros.sTokenBling = dtbCliente.Rows[0]["TOKENBLING"].ToString();


            if (dtbCliente.Rows[0]["ACCESSTOKENMERCADOLIVRE"].ToString() == "")
            {
                ClasseFuncoes.ConectaMercadoLivreAsync(iCodigoCliente);
            }

            //_ = ClasseFuncoes.RelatorioCustoBling(iCodigoCliente);
            //_ = ClasseFuncoes.LancaNotasManual(iCodigoCliente);

            _ = ClasseFuncoes.ExecuteGetFiscalDocumentsAsync(iCodigoCliente);
            


        }
    }
}
