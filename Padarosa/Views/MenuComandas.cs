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
        public void Resetar()
        {
            // Limpar campos e resetar grbs
            txbComanda.Clear();
            txbProduto.Clear();
            txbProdutoLan.Clear();
            txbQuantidade.Clear();
            grbLancamento.Enabled = false;
            grbInformacoes.Enabled = true;
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
            // Frescura:
            txbProdutoLan.Text += " R$" + dadosLinha.Cells[2].Value.ToString();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if(txbComanda.Text.Length == 0)
            {
                MessageBox.Show("Informe o número da comanda!");
            }else if(txbProduto.Text.Length == 0)
            {
                MessageBox.Show("Informe o código do produto!");
            }
            else
            {
                // Ativar o GRB debaixo:
                grbLancamento.Enabled = true;
                // Desativar o GRB atual:
                grbInformacoes.Enabled = false;
            }
        }

        private void btnLancar_Click(object sender, EventArgs e)
        {
            if(txbQuantidade.Text == "")
            {
                MessageBox.Show("Informe a quantidade de produtos.");
            }
            else if(int.Parse(txbQuantidade.Text) < 1)
            {
                MessageBox.Show("Verifique a quantidade informada!");
            }
            else
            {
                // Efetuar cadastro:
                Ordem ordem = new Ordem();
                ordem.Quantidade = int.Parse(txbQuantidade.Text);
                ordem.IDFicha = int.Parse(txbComanda.Text);
                ordem.IDProduto = int.Parse(txbProduto.Text);
                ordem.IDResponsavel = usuario.Id;
                // Confirmar o lançamento:
                var r = MessageBox.Show("Você deseja lançar " + ordem.Quantidade +
                    " unidades de " + txbProdutoLan.Text + " na comanda " +
                    ordem.IDFicha + "?", "Atenção!", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                if(r == DialogResult.Yes)
                {
                    if (Banco.OrdemDAO.Lancar(ordem))
                    {
                        MessageBox.Show("O produto foi lançado na comanda!");
                        Resetar();
                    }
                    else
                    {
                        MessageBox.Show("Houve um erro ao efetuar o lançamento.");
                    }
                }
                else
                {
                    Resetar();
                }
            }
        }
    }
}
