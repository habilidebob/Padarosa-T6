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
                // Instanciar o usuário:
                Usuario usuario = new Usuario();
                // Atribuir os valores dos campos no usuário:
                usuario.Email = txbEmail.Text;
                usuario.Senha = txbSenha.Text;

                // Consultar os dados no banco e obter o resultado em uma tabela:
                DataTable resultado = Banco.UsuarioDAO.Logar(usuario);

                // Verificar se houve resultado da consulta:
                if(resultado.Rows.Count > 0)
                {
                    // Limpar os campos:
                    txbEmail.Clear();
                    txbSenha.Clear();

                    // Atribuir os resultados da consultano obj usuario:
                    usuario.NomeCompleto = resultado.Rows[0]["nome_completo"].ToString();
                    usuario.Id = (int)resultado.Rows[0]["id"];


                    // Ir para o menu principal:
                    Views.MenuPrincipal menuPrincipal = new Views.MenuPrincipal(usuario);
                    // Esconder o form atual:
                    Hide();
                    // Mostrar o menu principal:
                    menuPrincipal.ShowDialog();

                    // Mostrar a tela de login após fechar o menu:
                    Show();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos");
                }



            }
            else
            {
                MessageBox.Show("Verifique as informações digitadas!");
            }
        }
    }
}
