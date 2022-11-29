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
    }
}
