using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public class FrutaRepository : IRepository
    {
        private Conexao conexao;

        public FrutaRepository()
        {
            conexao = new Conexao();
        }


        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM frutas WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(Fruta fruta)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE frutas SET
nome = @NOME , 
preco = @PRECO 
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", fruta.Nome);
            comando.Parameters.AddWithValue("@PRECO", fruta.Preco);
            comando.Parameters.AddWithValue("@ID", fruta.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;

        }

        public int Inserir(Fruta fruta)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO frutas (nome,preco)
OUTPUT INSERTED.ID
VALUES (@NOME , @PRECO)";
            comando.Parameters.AddWithValue("@NOME", fruta.Nome);
            comando.Parameters.AddWithValue("@PRECO", fruta.Preco);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;


        }

        public Fruta ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM frutas WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Fruta fruta = new Fruta();
            fruta.Id = Convert.ToInt32(linha["id"]);
            fruta.Nome = linha["nome"].ToString();
            fruta.Preco = Convert.ToDecimal(linha["preco"]);
            return fruta;
        }

        public List<Fruta> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM frutas WHERE nome LIKE @NOME";
            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@NOME", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Fruta> frutas = new List<Fruta>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Fruta fruta = new Fruta();
                fruta.Id = Convert.ToInt32(linha["id"]);
                fruta.Nome = linha["nome"].ToString();
                fruta.Preco = Convert.ToDecimal(linha["preco"]);
                frutas.Add(fruta);
            }
            return frutas;
        }
    }
}
