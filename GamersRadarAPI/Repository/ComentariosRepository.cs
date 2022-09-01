using GamersRadarAPI.Interfaces;
using GamersRadarAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Repositorie
{
    public class ComentariosRepository : IComentariosRepository
    {

        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";
        public bool Delete(int id)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "DELETE FROM Comentarios WHERE Id=@Id";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa um inteiro das linhas que foram apagadas
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    // Se as linhas apagadas forem igual a zero
                    if (linhasAfetadas == 0)
                    { // Retorna falso
                        return false;
                    }
                }
            }
            // Se não retorna true
            return true;
        }

        public ICollection<Comentarios> GetAll()
        {
            // Variavel de listagem de Comentarios
            var comentario = new List<Comentarios>();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os usuários no banco de dados
                string consulta = "SELECT * FROM Comentarios";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista
                            comentario.Add(new Comentarios
                            {
                                Id = (int)reader[0],
                                Comentario = (string)reader[1],
                                DataComentario = (DateTime)reader[2],
                                PerfilId = (int)reader[3],
                                PublicacoesId = (int)reader[4],

                            });
                        }
                    }
                }
            }
            return comentario;
        }

        public Comentarios GetById(int id)
        {
            // Variavel de listagem de Comentarios
            var comentario = new Comentarios();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os usuários no banco de dados
                string consulta = "SELECT * FROM Comentarios WHERE Id=@id";

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

                            comentario.Id = (int)reader[0];
                            comentario.Comentario = (string)reader[1];
                            comentario.DataComentario = (DateTime)reader[2];
                            comentario.PerfilId = (int)reader[3];
                            comentario.PublicacoesId = (int)reader[4];
                        }
                    }
                }
            }
            return comentario;
        }

        public Comentarios Insert(Comentarios comentario)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "INSERT INTO Comentarios (Comentario, DataComentario, PerfilId, PublicacoesId) VALUES (@Comentario, @DataComentario, @PerfilId, @PublicacoesId)";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@Comentario", SqlDbType.NVarChar).Value = comentario.Comentario;
                    cmd.Parameters.Add("@DataComentario", SqlDbType.DateTime).Value = comentario.DataComentario;
                    cmd.Parameters.Add("@PerfilId", SqlDbType.Int).Value = comentario.PerfilId;
                    cmd.Parameters.Add("@PublicacoesId", SqlDbType.Int).Value = comentario.PublicacoesId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
            return comentario;
        }

        public Comentarios Update(int id, Comentarios comentario)
        {

            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "UPDATE INTO Comentarios (Comentario, DataComentario, PerfilId, PublicacoesId) VALUES (@Comentario, @DataComentario, @PerfilId, @PublicacoesId)";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@Comentario", SqlDbType.NVarChar).Value = comentario.Comentario;
                    cmd.Parameters.Add("@DataComentario", SqlDbType.DateTime).Value = comentario.DataComentario;
                    cmd.Parameters.Add("@PerfilId", SqlDbType.Int).Value = comentario.PerfilId;
                    cmd.Parameters.Add("@PublicacoesId", SqlDbType.Int).Value = comentario.PublicacoesId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                    comentario.Id = id;
                }
            }
            return comentario;
        }
    }
}
