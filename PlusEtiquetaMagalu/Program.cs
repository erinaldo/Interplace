using InterRegraNegocio;
using InterRegraNegocio.MagazineLuiza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusEtiquetaMagalu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gerando etiquetas Magalu");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            MAGALUClasseFuncoes.EnviaNotasFaturadasMAGALU(iCliente);

        }
    }
}
