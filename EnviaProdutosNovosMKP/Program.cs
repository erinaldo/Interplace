using InterRegraNegocio;
using InterRegraNegocio.MagazineLuiza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviaProdutosNovosMKP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Envia produto Novos");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            int iCodigoCliente = 5;

            ClasseFuncoes.EnviaProdutoB2W(iCodigoCliente);
            MAGALUClasseFuncoes.EnviaProdutoMAGALU(iCodigoCliente);
    
        }
    }
}
