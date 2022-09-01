using GamersRadarAPI.Interfaces;
using GamersRadarAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Repositorie
{
    public class UsuarioRepository : IUsuarioRepository
    {

        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";
 
        public ICollection<Usuario> GetAll()
        {
            // Variavel de listagem de usuários
            var usuarios = new List<Usuario>();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os usuários no banco de dados
                string consulta = "SELECT * FROM Usuarios";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista
                            usuarios.Add(new Usuario
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Email = (string)reader[2],
                                Senha = (string)reader[3],
                            });
                        }
                    }
                }
            }
            return usuarios;
        }

        public Usuario GetById(int id)
        {
            // Variavel de listagem de usuários
            var usuarios = new Usuario();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os usuários no banco de dados
                string consulta = "SELECT * FROM Usuarios WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista
                            usuarios.Id = (int)reader[0];
                            usuarios.Nome = (string)reader[1];
                            usuarios.Email = (string)reader[2];
                            usuarios.Senha = (string)reader[3];
                        }
                    }
                }
            }
            return usuarios;
        }
        public Usuario Insert(Usuario usuario)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }

            return usuario;
        }

        public Usuario Update(int id, Usuario usuario)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "UPDATE Usuarios SET Nome=@Nome, Email=@Email, Senha=@Senha WHERE Id=@id";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    // Pega o id que está vindo da url
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                    usuario.Id = id;
                }
            }
            return usuario;
        }
    }
}
