using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusEtiquetaCorreios
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gera Etiquetas Correios");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            ClasseFuncoes.GeraEtiquetaCorreios(iCliente);




        }
    }
}
