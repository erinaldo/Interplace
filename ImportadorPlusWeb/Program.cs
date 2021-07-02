using interRegraNegocio.FortePlus;
using InterRegraNegocio;
using InterRegraNegocio.FortePlus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportadorPlusWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Iniciando importação de produto ");

            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");

            List<ProdutoFortePlus> lstProdutos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoFortePlus>>(ClasseFuncoes.RetornaProdutosFortPlus().Content);
            List<ProdutoComplemento> lstProdutosComplemento = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProdutoComplemento>>(ClasseFuncoes.RetornaProdutoComplementosFortPlus().Content);
            int i = 1;
            foreach (ProdutoFortePlus oProduto in lstProdutos)
            {
                ClasseParametros.sBanco = "sistemaweb";
                ClasseParametros.sIP = "187.45.196.191";
                ClasseParametros.sUsuario = "sistemaweb";
                ClasseParametros.sSenha = "Rodrigo06#";
                string sSql = "SELECT * FROM produto WHERE cProd = '" + oProduto.prCodigo + "'";
                DataTable d = ClasseParametros.ConsultaBancoMysql(sSql);
                string sCodigoProduto = "";

                Console.WriteLine(" Inserindo produto " + oProduto.prNome);
                if (d.Rows.Count == 0)
                {
                    sSql = "INSERT INTO produto(cProd, xProd,cEAN,uCom,vProd,emitente) ";
                    sSql += "VALUES('" + oProduto.prCodigo + "','" + oProduto.prNome + "','" + oProduto.prEan + "','UN','" + oProduto.prPrecoVenda + "','1')";
                    ClasseParametros.ExecutabancoMySql(sSql);
                    sSql = "SELECT * FROM produto WHERE cProd = '" + oProduto.prCodigo + "'";
                    d = ClasseParametros.ConsultaBancoMysql(sSql);
                    sCodigoProduto = d.Rows[0]["produtoid"].ToString();
                }
                else
                {
                    sCodigoProduto = d.Rows[0]["produtoid"].ToString();
                }

                List<ProdutoComplemento> lstProdutosComplementoAtual = lstProdutosComplemento.Where(x => x.cmIdProduto == oProduto.id).ToList();

                foreach (ProdutoComplemento oProdutoComplemento in lstProdutosComplementoAtual)
                {

                    string sMarketplace = ClasseFuncoes.RetornaNomeGlobalMK("MK", oProdutoComplemento.cmIdMarketPlace);
                    ClasseParametros.sBanco = "sistemaweb";
                    ClasseParametros.sIP = "187.45.196.191";
                    ClasseParametros.sUsuario = "sistemaweb";
                    ClasseParametros.sSenha = "Rodrigo06#";
                    Console.WriteLine(" Inserindo complemento " + sMarketplace);

                    sSql = "SELECT * FROM marketplace WHERE marketplace = '" + sMarketplace + "' ";
                    d = ClasseParametros.ConsultaBancoMysql(sSql);
                    string sCodigoMK = "";
                    if (d.Rows.Count == 0)
                    {
                        sSql = "INSERT INTO marketplace(marketplace) VALUES('" + sMarketplace + "')";
                        ClasseParametros.ExecutabancoMySql(sSql);
                        sSql = "SELECT * FROM marketplace WHERE marketplace = '" + sMarketplace + "' ";
                        d = ClasseParametros.ConsultaBancoMysql(sSql);
                        sCodigoMK = d.Rows[0]["codigo"].ToString();
                    }
                    else
                    {
                        sCodigoMK = d.Rows[0]["codigo"].ToString();
                    }


                    sSql = "SELECT * FROM produtomarketplace WHERE marketplace = '" + sCodigoMK + "' AND  produto = '" + sCodigoProduto + "'";
                    d = ClasseParametros.ConsultaBancoMysql(sSql);
                    if (d.Rows.Count == 0)
                    {
                        sSql = "INSERT INTO produtomarketplace(marketplace,produto,percentual,valor) VALUES('" + sCodigoMK + "','" + sCodigoProduto + "','" + oProdutoComplemento.cmPercentual.ToString() + "','" + oProdutoComplemento.cmPrecoDePor.ToString() + "')";
                        ClasseParametros.ExecutabancoMySql(sSql);
                    }


                }
                i++;
            }




            Console.WriteLine("Finalizado");



















        }
    }
}
