using InterRegraNegocio;
using InterRegraNegocio.MagazineLuiza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusAtualizaProdutoMKP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Atualizando produto");
            int iCliente = 5;
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            int iCodigoCliente = 5;
            //ClasseFuncoes.ConectaMercadoLivre(5);

            //int? iCodigoProduto = ClasseFuncoes.RetornaCodigoProdutoFortPlusPorSKU("ASPMON01");

            //ClasseFuncoes.EnviaProdutosMercadoLivre(iCodigoProduto.ToString());


            MAGALUClasseFuncoes.AtualizaEstoqueProdutos(iCodigoCliente);
            ClasseFuncoes.AtualizaEstoqueML(iCodigoCliente);
            ClasseFuncoes.AtualizaB2W(iCodigoCliente);
            




        }
    }
}
