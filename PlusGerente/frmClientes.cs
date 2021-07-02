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
    public partial class frmClientes : MetroFramework.Forms.MetroForm
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
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
            DataTable dtbCliente = ClasseParametros.ConsultaBancoMysql("SELECT * FROM CLIENTE");
            gridClientes.DataSource = dtbCliente;
            oConexaoInterno.Close();
            oConexaoInterno.Dispose();
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            frmClienteManutencao oFrm = new frmClienteManutencao();
            oFrm.ShowDialog();
            CarregaDados();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            frmClienteManutencao oFrm = new frmClienteManutencao();
            oFrm.lEditar = true;
            oFrm.editCodigo.Text = gridClientes.Rows[gridClientes.SelectedRows[0].Index].Cells["CODIGO"].Value.ToString();
            oFrm.ShowDialog();
            CarregaDados();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ainda não exclui!");
        }
    }
}
