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
    public partial class MenuCaixa : Form
    {
        int comandaAtual = 0;

        public MenuCaixa()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if(txbComanda.Text == "")
            {
                MessageBox.Show("Informe o número da comanda!");
            }
            else
            {
                // Buscar as informações:
                int nComanda = int.Parse(txbComanda.Text);
                // Atribuir o número da comanda atual na global:
                comandaAtual = nComanda;
                // Armazenar o resultado em um DataTable
                var r = Banco.OrdemDAO.BuscarComanda(nComanda);
                // Verificar se existem linhas na resposta:
                if(r.Rows.Count == 0)
                {
                    MessageBox.Show("Não existem produtos nesta comanda!");
                }
                else
                {
                    // Atribuir a resposta no dgv:
                    dgvCaixa.DataSource = r;
                    // Somar a coluna "Total Item":
                    var soma = r.Compute("Sum(Total_Item)", "True");
                    // Mostrar o total:
                    lblTotal.Text = "Total: R$" + soma.ToString();
                }
                
            }
        }

        private void chbPagamento_CheckedChanged(object sender, EventArgs e)
        {
            // Desativar/ativar o btnEncerrar de acordo com o checkbox:
            btnEncerrar.Enabled = chbPagamento.Checked;
        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {
            // Verificar se o listar foi pressionado:
            if(comandaAtual != 0)
            {
                // Obter o ID da comanda:
                int idComanda = int.Parse(txbComanda.Text); // ?

                var r = MessageBox.Show("Tem certeza que deseja encerrar a comanda "
                    + idComanda + "?", "Aviso!", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    if (Banco.OrdemDAO.EncerrarComanda(idComanda))
                    {
                        MessageBox.Show("Comanda encerrada com sucesso!");
                        // Limpar o DGV:
                        dgvCaixa.DataSource = null;
                        // Desabilitar os botões:
                        btnEncerrar.Enabled = false;
                        chbPagamento.Checked = false;
                        txbComanda.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao encerrar comanda!");
                    }
                }
            }
            

        }
    }
}
