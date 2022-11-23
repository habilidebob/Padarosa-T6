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
        int idSelecionado = 0;
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

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ativar os groupboxes:
            grbRemover.Enabled = true;
            grbEditar.Enabled = true;

            // Obter o número da linha selecionada:
            int linhaSelecionada = dgvUsuarios.CurrentCell.RowIndex;
            // Obter toda a linha selecionada:
            var dadosLinha = dgvUsuarios.Rows[linhaSelecionada];

            // Popular os campos do editar:
            txbNomeEdi.Text = dadosLinha.Cells[1].Value.ToString();
            txbEmailEdi.Text = dadosLinha.Cells[2].Value.ToString();

            // Popular o "remover":
            lblRemover.Text = dadosLinha.Cells[0].Value.ToString() + " - "
                + dadosLinha.Cells[1].Value.ToString();

            // Salvar o id na variavel global:
            idSelecionado = (int)dadosLinha.Cells[0].Value;
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("Tem certeza que deseja remover o seguinte usuário \n" +
                "" + lblRemover.Text + "?", "Atenção!", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r == DialogResult.Yes && idSelecionado != 1)
            {
                // Apagar o jovenzinho:
                if(Banco.UsuarioDAO.Remover(idSelecionado) != -1)
                {
                    MessageBox.Show("Usuário removido!", "Atenção!", 
                        MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                    // Atualizar o DGV:
                    AtualizarDgv();
                    // Desativar os groupBox:
                    grbEditar.Enabled = false;
                    grbRemover.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Houve um erro ao remover o usuário!", "Atenção!",
                        MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }else if(idSelecionado == 1)
            {
                MessageBox.Show("O administrador não pode ser removido!", "Atenção!",
                        MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txbEmailEdi.Text.Length >= 5 && txbNomeEdi.Text.Length > 2)
            {
                Usuario usuario = new Usuario();
                usuario.Id = idSelecionado;
                usuario.NomeCompleto = txbNomeEdi.Text;
                usuario.Email = txbEmailEdi.Text;
                usuario.Senha = txbSenhaEdi.Text;
                // Chamar os bang do DAO:
                if (Banco.UsuarioDAO.Editar(usuario))
                {
                    MessageBox.Show("Usuário modificado com sucesso!");
                    if(chbSenha.Checked && txbSenhaEdi.Text == "")
                    {
                        MessageBox.Show("A senha não foi modificada!");
                    }
                    // Atualizar o DGV:
                    AtualizarDgv();
                    grbEditar.Enabled = false;
                    grbRemover.Enabled = false;
                    // Limpar o campo de senha
                    txbSenhaEdi.Clear();
                }
                else
                {
                    MessageBox.Show("Erro ao modificar usuário.");
                }
            }
            else
            {
                MessageBox.Show("Verifique as informações digitadas!");
            }
        }

        private void chbSenha_CheckedChanged(object sender, EventArgs e)
        {
            txbSenhaEdi.Enabled = chbSenha.Checked;
            txbSenhaEdi.Text = "";
        }
    }
}
