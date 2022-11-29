using BibliotecaPadarosa;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padarosa.Banco
{
    public static class OrdemDAO
    {
        public static bool Lancar(Ordem ordem)
        {
            string comando = "INSERT INTO ordens_comandas " +
                "(id_ficha, id_produto, quantidade, id_resp) " +
                "VALUES (@id_ficha, @id_produto, @quantidade, @id_resp)";

            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);

            cmd.Parameters.AddWithValue("@id_ficha", ordem.IDFicha);
            cmd.Parameters.AddWithValue("@id_produto", ordem.IDProduto);
            cmd.Parameters.AddWithValue("@quantidade", ordem.Quantidade);
            cmd.Parameters.AddWithValue("@id_resp", ordem.IDResponsavel);

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
    }
}
