using InterRegraNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlusGerente
{
    public partial class frmClienteManutencao : MetroFramework.Forms.MetroForm
    {
        public bool lEditar = false;
        public frmClienteManutencao()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            SqlConnection oConexaoInterno = null;
            Dictionary<string, object> ParametrosSQL = new Dictionary<string, object>();

            try
            {
                oConexaoInterno = new SqlConnection(@"Server=191.252.61.62;Database=BaseInfo;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");
                oConexaoInterno.Open();
            }
            catch
            {

            }
            if (oConexaoInterno.State == ConnectionState.Closed)
            {
                return;
            }


            string sSql = "";
            if (lEditar)
            {
                sSql = "UPDATE CLIENTE SET USUARIOMAGALU=@USUARIOMAGALU,SENHAMAGALU=@SENHAMAGALU, CLIENTE=@CLIENTE,KEYB2W=@KEYB2W,USUARIOB2W=@USUARIOKEYB2W,IDMERCADOLIVRE=@IDMERCADOLIVRE,SENHAMERCADOLIVRE=@SENHAMERCADOLIVRE,REMETENTE=@REMETENTE,ENDERECO=@ENDERECO,CEP=@CEP,CIDADE=@CIDADE,UF=@UF,GUMGATOKENANYMARKET=@GUMGATOKENANYMARKET,PORARQUIVO=@PORARQUIVO,EMAILFORTEPLUS=@EMAILFORTEPLUS,SENHAFORTEPLUS=@SENHAFORTEPLUS,DOMINIOFORTEPLUS=@DOMINIOFORTEPLUS,PASTA=@PASTA,CNPJ=@CNPJ WHERE CODIGO = @CODIGO";
                ParametrosSQL.Add("CODIGO", editCodigo.Text);
            }
            else
            {
                sSql = "INSERT INTO CLIENTE(USUARIOMAGALU,SENHAMAGALU,CLIENTE,KEYB2W,USUARIOB2W,IDMERCADOLIVRE,SENHAMERCADOLIVRE,REMETENTE,ENDERECO,CEP,CIDADE,UF,GUMGATOKENANYMARKET,PORARQUIVO,EMAILFORTEPLUS,SENHAFORTEPLUS,DOMINIOFORTEPLUS,PASTA,CNPJ) VALUES (@USUARIOMAGALU,@SENHAMAGALU,@CLIENTE,@KEYB2W,@USUARIOKEYB2W,@IDMERCADOLIVRE,@SENHAMERCADOLIVRE,@REMETENTE,@ENDERECO,@CEP,@CIDADE,@UF,@GUMGATOKENANYMARKET,@PORARQUIVO,@EMAILFORTEPLUS,@SENHAFORTEPLUS,@DOMINIOFORTEPLUS,@PASTA,@CNPJ)";
            }

            ParametrosSQL.Add("CLIENTE", editCliente.Text);
            ParametrosSQL.Add("KEYB2W", editTokenB2W.Text);
            ParametrosSQL.Add("USUARIOKEYB2W", editUsuarioB2W.Text);
            ParametrosSQL.Add("IDMERCADOLIVRE", editIDMELI.Text);
            ParametrosSQL.Add("SENHAMERCADOLIVRE", editSenhaMELI.Text);

            ParametrosSQL.Add("USUARIOMAGALU", editUsuarioMagalu.Text);
            ParametrosSQL.Add("SENHAMAGALU", editSenhaMagalu.Text);

            ParametrosSQL.Add("CNPJ", editCNPJ.Text);
            ParametrosSQL.Add("REMETENTE", editRemetente.Text);
            ParametrosSQL.Add("ENDERECO", editEndereco.Text);
            ParametrosSQL.Add("CEP", editCEP.Text);
            ParametrosSQL.Add("CIDADE", editCidade.Text);
            ParametrosSQL.Add("UF", editUF.Text);

            ParametrosSQL.Add("GUMGATOKENANYMARKET", editAnymarket.Text);

            int iResultado = chkArquivo.Checked ? 1 : 0;
            ParametrosSQL.Add("PORARQUIVO", iResultado.ToString());
            ParametrosSQL.Add("PASTA", editPasta.Text.ToString());

            ParametrosSQL.Add("EMAILFORTEPLUS", editEmailFortePlus.Text);
            ParametrosSQL.Add("SENHAFORTEPLUS", editSenhaFortePlus.Text);
            ParametrosSQL.Add("DOMINIOFORTEPLUS", editDominioFortePlus.Text);


            ClasseParametros.ExecutabancoMySql(sSql, ParametrosSQL);
            MessageBox.Show("Cadastro com Sucesso!");
            oConexaoInterno.Close();
            oConexaoInterno.Dispose();

            if (lEditar)
                Close();

            LimpaCampos();
        }

        private void LimpaCampos()
        {
            editCliente.Text = "";
            editUsuarioB2W.Text = "";
            editIDMELI.Text = "";
            editSenhaMELI.Text = "";
        }

        private void FrmClienteManutencao_Load(object sender, EventArgs e)
        {
            SqlConnection oConexaoInterno = null;
            try
            {
                oConexaoInterno = new SqlConnection(@"Server=191.252.61.62;Database=BaseInfo;User=sa;Password=#BanLoc#InterPlace#2019#;MultipleActiveResultSets=true;");
                oConexaoInterno.Open();
            }
            catch
            {

            }
            if (oConexaoInterno.State == ConnectionState.Closed)
            {
                return;
            }
            if (lEditar)
            {
                DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql("SELECT USUARIOMAGALU,SENHAMAGALU,CLIENTE,KEYB2W,USUARIOB2W,IDMERCADOLIVRE,SENHAMERCADOLIVRE,REMETENTE,ENDERECO,CEP,CIDADE,UF, GUMGATOKENANYMARKET, PORARQUIVO,EMAILFORTEPLUS,SENHAFORTEPLUS,DOMINIOFORTEPLUS,CNPJ,PASTA FROM CLIENTE WHERE CODIGO = " + editCodigo.Text);
                editCliente.Text = dtbCliente.Rows[0]["CLIENTE"].ToString();
                editUsuarioB2W.Text = dtbCliente.Rows[0]["USUARIOB2W"].ToString();
                editTokenB2W.Text = dtbCliente.Rows[0]["KEYB2W"].ToString();
                editIDMELI.Text = dtbCliente.Rows[0]["IDMERCADOLIVRE"].ToString();
                editSenhaMELI.Text = dtbCliente.Rows[0]["SENHAMERCADOLIVRE"].ToString();
                editRemetente.Text = dtbCliente.Rows[0]["REMETENTE"].ToString();
                editEndereco.Text = dtbCliente.Rows[0]["ENDERECO"].ToString();
                editCEP.Text = dtbCliente.Rows[0]["CEP"].ToString();
                editCidade.Text = dtbCliente.Rows[0]["CIDADE"].ToString();
                editUF.Text = dtbCliente.Rows[0]["UF"].ToString();
                editAnymarket.Text = dtbCliente.Rows[0]["GUMGATOKENANYMARKET"].ToString();
                editUsuarioMagalu.Text = dtbCliente.Rows[0]["USUARIOMAGALU"].ToString();
                editSenhaMagalu.Text = dtbCliente.Rows[0]["SENHAMAGALU"].ToString();
                editEmailFortePlus.Text = dtbCliente.Rows[0]["EMAILFORTEPLUS"].ToString();
                editSenhaFortePlus.Text = dtbCliente.Rows[0]["SENHAFORTEPLUS"].ToString();
                editDominioFortePlus.Text = dtbCliente.Rows[0]["SENHAMAGALU"].ToString();
                editCNPJ.Text = dtbCliente.Rows[0]["CNPJ"].ToString();
                editPasta.Text = dtbCliente.Rows[0]["PASTA"].ToString();
                bool lResultado = false;
                if (dtbCliente.Rows[0]["PORARQUIVO"].ToString() != "")
                    lResultado = int.Parse(dtbCliente.Rows[0]["PORARQUIVO"].ToString()) == 1;
                chkArquivo.Checked = lResultado;
            }
            ActiveControl = editCliente;
            editCliente.Focus();

            oConexaoInterno.Close();
            oConexaoInterno.Dispose();
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
