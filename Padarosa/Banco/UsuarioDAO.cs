using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaPadarosa;

namespace Padarosa.Banco
{
    public static class UsuarioDAO
    {
        public static DataTable Logar(Usuario u)
        {
            string comando = "SELECT id, nome_completo, email, senha" +
                " FROM usuarios WHERE email = @email AND senha = @senha";
            
            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);

            // Substituir os 'coringas' por valores:
            cmd.Parameters.AddWithValue("@email", u.Email);
            // Obter o hash da senha:
            string hashsenha = EasyEncryption.SHA.ComputeSHA256Hash(u.Senha);
            cmd.Parameters.AddWithValue("@senha", hashsenha);

            cmd.Prepare();

            // Declarar tabela que irá receber o resultado:
            DataTable tabela = new DataTable();
            // Preencher a tabela com o resultado da consulta
            tabela.Load(cmd.ExecuteReader());

            conexaoBD.Desconectar(con);

            return tabela;

        }
        public static DataTable ListarTudo()
        {
            string comando = "SELECT id, nome_completo, email" +
                " FROM usuarios";

            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);

            cmd.Prepare();

            // Declarar tabela que irá receber o resultado:
            DataTable tabela = new DataTable();
            // Preencher a tabela com o resultado da consulta
            tabela.Load(cmd.ExecuteReader());

            conexaoBD.Desconectar(con);

            return tabela;

        }
        public static bool Cadastrar(Usuario u)
        {
            string comando = "INSERT INTO usuarios (nome_completo, email, senha) " +
                "VALUES (@nome_completo, @email, @senha)";

            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);

            cmd.Parameters.AddWithValue("@nome_completo", u.NomeCompleto);
            cmd.Parameters.AddWithValue("@email", u.Email);
            // Tirar o hash da senha:
            string senhahash = EasyEncryption.SHA.ComputeSHA256Hash(u.Senha);
            cmd.Parameters.AddWithValue("@senha", senhahash);

            cmd.Prepare();

            try
            {
                if (cmd.ExecuteNonQuery() == 0)
                {
                    conexaoBD.Desconectar(con);
                    return false;
                }
                else
                {
                    conexaoBD.Desconectar(con);
                    return true;
                }
            }
            catch
            {
                conexaoBD.Desconectar(con);
                return false;
            }
           
        }

        /*
        public static int Editar()
        {

        }
        
        public static int Remover()
        {

        }
        */
    }
}
