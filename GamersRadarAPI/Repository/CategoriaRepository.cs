using GamersRadarAPI.Interfaces;
using GamersRadarAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";
        public ICollection<Categoria> GetAll()
        {
            // Variavel de listagem de categorias
            var categoria = new List<Categoria>();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os usuários no banco de dados
                string consulta = "SELECT * FROM Categorias";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista
                            categoria.Add(new Categoria
                            {
                                Id = (int)reader[0],
                                TipoCategoria = (string)reader[1],
                                PublicacoesId = (int)reader[2],

                            });
                        }
                    }
                }
            }
            return categoria;
        }

        public Categoria GetById(int id)
        {
            // Variavel de listagem de categorias
            var categoria = new Categoria();

            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Seleciona todos os usuários no banco de dados
                string consulta = "SELECT * FROM Categorias WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Enquanto o reader estiver lendo
                        while (reader.Read())
                        {
                            // Adiciona cada elemento da lista

                            categoria.Id = (int)reader[0];
                            categoria.TipoCategoria = (string)reader[1];
                            categoria.PublicacoesId = (int)reader[2];
                        }
                    }
                }
            }
            return categoria;
        }

        public Categoria Insert(Categoria categoria)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "INSERT INTO Categorias (TipoCategoria, PublicacoesId) VALUES (@TipoCategoria, @PublicacoesId)";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@TipoCategoria", SqlDbType.NVarChar).Value = categoria.TipoCategoria;
                    cmd.Parameters.Add("@PublicacoesId", SqlDbType.Int).Value = categoria.PublicacoesId;


                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
            return categoria;
        }

        public Categoria Update(int id, Categoria categoria)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre conexão com o banco
                conexao.Open();

                // Declara a query
                string script = "UPDATE Categorias SET TipoCategoria=@TipoCategoria, PublicacoesId=@PublicacoesId WHERE Id=@id";

                // Cria o comando de execução do banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    // Pega o id que está vindo da url
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@TipoCategoria", SqlDbType.NVarChar).Value = categoria.TipoCategoria;
                    cmd.Parameters.Add("@PublicacoesId", SqlDbType.Int).Value = categoria.PublicacoesId;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    // Executa a query
                    cmd.ExecuteNonQuery();
                    categoria.Id = id;
                }
            }
            return categoria;
        }
    }
}
