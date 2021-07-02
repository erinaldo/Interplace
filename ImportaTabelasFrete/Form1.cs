using ExcelDataReader;
using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ImportaTabelasFrete
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            if (dlgArquivo.ShowDialog() == DialogResult.OK)
            {
                editArquivoCIDATEN.Text = dlgArquivo.FileName;
            }

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {


            using (var stream = File.Open(editArquivoCIDATEN.Text, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:
                    int iLinhaAtual = 1;
                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            DateTime dVigente = DateTime.Now;
                            if (iLinhaAtual == 1)
                                dVigente = DateTime.Parse(reader.GetValue(0).ToString());
                            if (iLinhaAtual > 5)
                            {
                                if (reader.GetValue(0) == null)
                                    continue;
                                string sUF = reader.GetValue(0).ToString();
                                string sLocalidade = reader.GetValue(1).ToString();
                                string sCEPInicial = reader.GetValue(2).ToString().Split('a')[0].Trim();
                                string sCEPFinal = reader.GetValue(2).ToString().Split('a')[1].Trim();
                                string sExpresso = reader.GetValue(3).ToString();
                                string sRodo = reader.GetValue(4).ToString();
                                string sPreco = reader.GetValue(5).ToString();
                                string sFrap = reader.GetValue(6).ToString();
                                string sDistribuidor = reader.GetValue(7).ToString();
                                decimal eFaixaGris = decimal.Parse(reader.GetValue(9).ToString());

                                if (eFaixaGris < 1)
                                    eFaixaGris = 1;

                                string sSQL = "SELECT * FROM CIDATEN WHERE CEPINICIO = '@CEPINICIO' AND CEPFINAL = '@CEPFINAL'";
                                DataTable dtbCIDATEN = ClasseParametros.ConsultaBancoMysql(sSQL);
                                if (dtbCIDATEN.Rows.Count > 0)
                                {
                                    sSQL = "UPDATE CIDATEN SET UF='@UF', LOCALIDADE='@LOCALIDADE',EXPRESSO='@EXPRESSO',RODO='@RODO',PRECO='@PRECO',FRAP='@FRAP',DISTRIBUIDOR='@DISTRIBUIDOR'," +
                                        "FAIXAGRIS='@FAIXAGRIS',DATAVIGENTE='@DATAVIGENTE' WHERE CEPINICIO = '@CEPINICIO' AND CEPFINAL = '@CEPFINAL'";
                                }
                                else
                                {
                                    sSQL = "INSERT INTO CIDATEN(UF, LOCALIDADE,EXPRESSO,RODO,PRECO,FRAP,DISTRIBUIDOR,FAIXAGRIS,DATAVIGENTE, CEPINICIO ,CEPFINAL) " +
                                        "VALUES(@UF, @LOCALIDADE,@EXPRESSO,@RODO,@PRECO,@FRAP,@DISTRIBUIDOR,@FAIXAGRIS,@DATAVIGENTE, @CEPINICIO ,@CEPFINAL) ";
                                }

                                Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();
                                ParametrosSQL.Add("UF", sUF);
                                ParametrosSQL.Add("LOCALIDADE", sLocalidade);
                                ParametrosSQL.Add("EXPRESSO", sExpresso);
                                ParametrosSQL.Add("RODO", sRodo);
                                ParametrosSQL.Add("PRECO", sPreco);
                                ParametrosSQL.Add("FRAP", sFrap);
                                ParametrosSQL.Add("DISTRIBUIDOR", sDistribuidor);
                                ParametrosSQL.Add("FAIXAGRIS", eFaixaGris);
                                ParametrosSQL.Add("DATAVIGENTE", dVigente);
                                ParametrosSQL.Add("CEPINICIO", sCEPInicial);
                                ParametrosSQL.Add("CEPFINAL", sCEPFinal);

                                ClasseParametros.ExecutabancoMySql(sSQL, ParametrosSQL);
                            }

                            iLinhaAtual++;
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    //var result = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables
                    MessageBox.Show("Feito papito");
                }
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ClasseParametros.sBanco = ClasseParametros.oIni.IniReadValue("banco", "banco");
            ClasseParametros.sIP = ClasseParametros.oIni.IniReadValue("banco", "servidor");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
            ClasseParametros.sUsuario = ClasseParametros.oIni.IniReadValue("banco", "usuario");
        }
    }
}
