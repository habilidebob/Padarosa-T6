using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BibliotecaPadarosa;

namespace Padarosa.Views
{
    public partial class MenuPrincipal : Form
    {
        // usuario global:
        Usuario usuario = new Usuario();

        public MenuPrincipal(Usuario usuario)
        {
            InitializeComponent();
            lblInformacao.Text = "Você está logado como " + usuario.NomeCompleto;
            // Atribuir o usuário local no global:
            this.usuario = usuario;
            // Verificar privilégios do usuário:
            if(usuario.Id != 1)
            {
                btnUsuarios.Enabled = false;
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            
            MenuUsuarios menuUsuarios = new MenuUsuarios(usuario);
            menuUsuarios.ShowDialog();
        }

        private void btnComandas_Click(object sender, EventArgs e)
        {
            MenuComandas menuComandas = new MenuComandas(usuario);
            menuComandas.ShowDialog();
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            MenuCaixa menuCaixa = new MenuCaixa();
            menuCaixa.ShowDialog();
        }
    }
}
