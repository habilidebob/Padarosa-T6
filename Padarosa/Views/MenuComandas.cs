using BibliotecaPadarosa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Padarosa.Views
{
    public partial class MenuComandas : Form
    {
        Usuario usuario;
        public MenuComandas(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            AtualizarDgv();
        }

        public void AtualizarDgv()
        {
            dgvProdutos.DataSource = Banco.ProdutoDAO.ListarTudo();
        }

        private void txbQuantidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obter o número da linha selecionada:
            int linhaSelecionada = dgvProdutos.CurrentCell.RowIndex;
            // Obter toda a linha selecionada:
            var dadosLinha = dgvProdutos.Rows[linhaSelecionada];

            txbProduto.Text = dadosLinha.Cells[0].Value.ToString();
            txbProdutoLan.Text = dadosLinha.Cells[1].Value.ToString();
        }
    }
}
