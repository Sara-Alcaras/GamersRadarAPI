using GamersRadarAPI.Interfaces;
using GamersRadarAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Repository
{
    public class PublicacoesRepository : IPublicacoesRepository
    {
        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";
        public ICollection<Publicacao> GetAll()
        {
            // Variavel de listagem de publicações
            var publicacao = new List<Publicacao>();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos as publicações no banco de dados
                string consulta = "SELECT * FROM Publicacoes";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista
                            publicacao.Add(new Publicacao
                            {
                                Id = (int)reader[0],
                                Descricao = (string)reader[1],
                                PerfilId = (int)reader[2],
                                ImagemAnexo = (string)reader[3].ToString()
                            });
                        }
                    }
                }
            }
            return publicacao;
        }

        public Publicacao GetById(int id)
        {
            // Variavel de listagem de publicações
            var publicacao = new Publicacao();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos as publicações no banco de dados
                string consulta = "SELECT * FROM Publicacoes WHERE Id=@id";

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
                            publicacao.Id = (int)reader[0];
                            publicacao.Descricao = (string)reader[1];
                            publicacao.PerfilId = (int)reader[2];
                            publicacao.ImagemAnexo = (string)reader[3].ToString();


                        }
                    }
                }
            }
            return publicacao;
        }

        public Publicacao Insert(Publicacao publicacao)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "INSERT INTO Publicacoes (Descricao, ImagemAnexo, PerfilId) VALUES (@Descricao, @ImagemAnexo, @PerfilId)";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@Descricao", SqlDbType.NVarChar).Value = publicacao.Descricao;
                    cmd.Parameters.Add("@ImagemAnexo", SqlDbType.NVarChar).Value = publicacao.ImagemAnexo;
                    cmd.Parameters.Add("@PerfilId", SqlDbType.Int).Value = publicacao.PerfilId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
            return publicacao;
        }
        public Publicacao Update(int id, Publicacao publicacao)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "UPDATE Publicacoes SET Descricao=@Descricao, ImagemAnexo=@ImagemAnexo, PerfilId=@PerfilId WHERE Id=@id";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    // Pega o id que está vindo da url
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@Descricao", SqlDbType.NVarChar).Value = publicacao.Descricao;
                    cmd.Parameters.Add("@ImagemAnexo", SqlDbType.VarBinary).Value = publicacao.ImagemAnexo;
                    cmd.Parameters.Add("@PerfilId", SqlDbType.Int).Value = publicacao.PerfilId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                    publicacao.Id = id;
                }
            }
            return publicacao;
        }
    }
}
