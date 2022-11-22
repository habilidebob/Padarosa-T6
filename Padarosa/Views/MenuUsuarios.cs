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
    public partial class MenuUsuarios : Form
    {
        // Global:
        Usuario usuario;
        public MenuUsuarios(Usuario usuario)
        {
            InitializeComponent();
            // Atribuir o local no global:
            this.usuario = usuario;
            // Mudar o label:
            lblInformacao.Text = "Você está logado como " + usuario.NomeCompleto;
            // Atualizar DGV:
            AtualizarDgv();
        }

        public void AtualizarDgv()
        {
            // Carregar os dados do banco no dgv:
            dgvUsuarios.DataSource = Banco.UsuarioDAO.ListarTudo();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Verificar se os campos estão vazios:
            if(txbEmailCad.Text.Length >= 5 && txbNomeCad.Text.Length > 2
                && txbSenhaCad.Text.Length >= 3)
            {
                // Instanciar o usuario:
                Usuario usuario = new Usuario();
                // Obter as informações dos campos:
                usuario.NomeCompleto = txbNomeCad.Text;
                usuario.Email = txbEmailCad.Text;
                usuario.Senha = txbSenhaCad.Text;

                // Enviar para o banco e verificar se deu certo:
                if(Banco.UsuarioDAO.Cadastrar(usuario))
                {
                    MessageBox.Show("Usuário cadastrado com sucesso!");
                    // Limpar os campos:
                    txbNomeCad.Clear();
                    txbEmailCad.Clear();
                    txbSenhaCad.Clear();
                    // Atualizar o dgv:
                    AtualizarDgv();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar usuário!");
                }

            }
            else
            {
                MessageBox.Show("Verifique as informações digitadas.");
            }
        }
    }
}
