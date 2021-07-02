using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusEnviaProdutoSite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Atualizando Produtos");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            //ClasseFuncoes.AtualizaProdutos(iCliente);
            ClasseFuncoes.EnviaProdutoSite();
        }
    }
}
