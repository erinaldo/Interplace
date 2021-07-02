using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusIntegraPedidoMercadoLivre
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Integra Pedido Mercado Livre");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");

            ClasseFuncoes.CarregaFiliais("34.036.601/0003-38 - 2ELETRO VAREJISTA");

            ClasseFuncoes.RecebePedidoEnviaFortPlusAsync(iCliente);
            //ClasseFuncoes.RetornaCodigoLocalEstoque("LE", "34");
        }
    }
}
