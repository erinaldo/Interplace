using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusEtiquetaMrecado
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Geração de Etiqueta");
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            int iCodigoCliente = 5;

            ClasseFuncoes.RecebePedidoEnviaMercadoLivreGeraEtiquetaAsync(iCodigoCliente);


        }
    }
}
