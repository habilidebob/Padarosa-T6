using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Padarosa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Verificar o tamanho do e-mail e senha:
            if(txbEmail.Text.Length >= 6 && txbSenha.Text.Length >= 1)
            {
                // Fazer a validação:

            }
            else
            {
                MessageBox.Show("Verifique as informações digitadas!");
            }
        }
    }
}
