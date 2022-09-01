using GamersRadarAPI.Interfaces;
using GamersRadarAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Repository
{
    public class PerfilsRepository : IPerfilsRepository
    {
        // Cria variavel de conexão com banco de dados
        // Readonly = apenas leitura 
        // String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";
        public ICollection<Perfil> GetAll()
        {
            // Variavel de listagem de perfils
            var perfil = new List<Perfil>();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os perfils no banco de dados
                string consulta = "SELECT * FROM Perfil";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista
                            perfil.Add(new Perfil
                            {
                                Id = (int)reader[0],
                                Biografia = (string)reader[1],
                                Foto = (byte[])reader[2],
                                JogosInteresse = (string)reader[3],
                                UsuariosId = (int)reader[4],
                            });
                        }
                    }
                }
            }
            return perfil;
        }

        public Perfil GetById(int id)
        {
           // Variavel de listagem de perfils
            var perfil = new Perfil();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os perfils no banco de dados
                string consulta = "SELECT * FROM Perfil WHERE Id=@id ";

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

                            perfil.Id = (int)reader[0];
                            perfil.Biografia = (string)reader[1];
                            perfil.Foto = (byte[])reader[2];
                            perfil.JogosInteresse = (string)reader[3];
                            
                        }
                    }
                }
            }
            return perfil;
        }

        public Perfil Insert(Perfil perfil)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "INSERT INTO Perfil (Biografia, Foto, JogosInteresse, UsuariosId) VALUES (@Biografia, @Foto, @JogosInteresse, @UsuariosId)";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@Biografia", SqlDbType.NVarChar).Value = perfil.Biografia;
                    cmd.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = perfil.Foto;
                    cmd.Parameters.Add("@JogosInteresse", SqlDbType.NVarChar).Value = perfil.JogosInteresse;
                    cmd.Parameters.Add("@UsuariosId", SqlDbType.Int).Value = perfil.UsuariosId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
            return perfil;
        }

        public Perfil Update(int id, Perfil perfil)
        {

            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "UPDATE Perfil SET Biografia=@Biografia, Foto=@Foto, JogosInteresse=@JogosInteresse, UsuariosId=@UsuariosId WHERE Id=@id";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    // Pega o id que está vindo da url
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@Biografia", SqlDbType.NVarChar).Value = perfil.Biografia;
                    cmd.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = perfil.Foto;
                    cmd.Parameters.Add("@JogosInteresse", SqlDbType.NVarChar).Value = perfil.JogosInteresse;
                    cmd.Parameters.Add("@UsuariosId", SqlDbType.Int).Value = perfil.UsuariosId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                    perfil.Id = id;
                }
            }
            return perfil;
        }
    }
}
