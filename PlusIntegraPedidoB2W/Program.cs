using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusIntegraPedidoB2W
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Integração B2W");
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");

            ClasseFuncoes.CarregaFiliais("34.036.601/0003-38 - 2ELETRO VAREJISTA");

            int iCodigoCliente = 5;

            //ClasseFuncoes.RecebeUmPedidoEnviaB2wEnviaFortPlus(iCodigoCliente, "Submarino-351870833701");

            ClasseFuncoes.RecebePedidoEnviaB2wEnviaFortPlus(iCodigoCliente);
        }
    }
}
