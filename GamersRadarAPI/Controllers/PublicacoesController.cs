using GamersRadarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacoesController : ControllerBase
    {

        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";

        //POST - Cadastrar
        /// <summary>
        /// Cadastra uma publicação
        /// </summary>
        /// <remarks>
        /// Exemplo de insersão do campo ImagemAnexo:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o.Net converte para VarBinary(tipo que está cadastrado no banco)
        /// </remarks>
        /// <param name = "publicacao" > Dados da publicação</param>
        [HttpPost]

        // Cria variavel que permite cadastrar publicações
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar(Publicacao publicacao)
        {
            try
            {
                // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
                // Abre a conexão com o banco de acordo com a connectionString criada
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    // Abre conexão com o banco
                    conexao.Open();

                    // Declara a query
                    string script = "INSERT INTO Publicacoes (Descricao, ImagemAnexo, DataHora, PerfilId) VALUES (@Descricao, @ImagemAnexo, @DataHora, @PerfilId)";

                    // Cria o comando de execução do banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Declaração de variável por parâmetro
                        cmd.Parameters.Add("@Descricao", SqlDbType.NVarChar).Value = publicacao.Descricao;
                        cmd.Parameters.Add("@ImagemAnexo", SqlDbType.VarBinary).Value = publicacao.ImagemAnexo;
                        cmd.Parameters.Add("@DataHora", SqlDbType.NVarChar).Value = publicacao.DataHora;
                        cmd.Parameters.Add("@PerfilId", SqlDbType.Int).Value = publicacao.PerfilId;

                        // Informa o tipo do comando
                        cmd.CommandType = CommandType.Text;

                        // Executa a query
                        cmd.ExecuteNonQuery();
                    }
                }

                // Retorna o usuario cadastrado
                return Ok(publicacao);
            }
            catch (System.Exception ex)
            {
                // Retorna um erro pré definido com uma mensagem
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message
                });
            }
        }

        // GET - Listar
        /// <summary>
        /// Lista todos as publicações cadastrados na aplicação
        /// </summary>
        /// <returns>Lista de publicações</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
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
                                    ImagemAnexo = (byte[])reader[2],
                                    DataHora = (DateTime)reader[3],
                                    PerfilId = (int)reader[4],
                                });
                            }
                        }
                    }
                }

                // Retorna o publicação cadastrado
                return Ok(publicacao);
            }
            catch (System.Exception ex)
            {
                // Retorna um erro pré definido com uma mensagem
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message
                });
            }

        }

        // PUT - Alterar
        /// <summary>
        /// Alterar os dados de uma publicação
        /// </summary>
        /// <remarks>
        /// Exemplo de insersão do campo ImagemAnexo:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o.Net converte para VarBinary(tipo que está cadastrado no banco)
        /// </remarks>
        /// <param name="publicacao">Todos as informações da publicação</param>
        /// <param name="id">Id da publicação</param>
        /// <returns>Publicação alterada</returns>
        [HttpPut("{id}")]

        //// O método alterar tem como parametro o id da publicação
        public IActionResult Alterar(int id, Publicacao publicacao)
        {
            try
            {
                // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
                // Abre a conexão com o banco de acordo com a connectionString criada
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    // Abre conexão com o banco
                    conexao.Open();

                    // Declara a query
                    string script = "UPDATE Publicacoes SET Descricao=@Descricao, ImagemAnexo=@ImagemAnexo, DataHora=@DataHora, PerfilId=@PerfilId WHERE Id=@id";

                    // Cria o comando de execução do banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Declaração de variável por parâmetro
                        // Pega o id que está vindo da url
                        cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                        cmd.Parameters.Add("@Descricao", SqlDbType.NVarChar).Value = publicacao.Descricao;
                        cmd.Parameters.Add("@ImagemAnexo", SqlDbType.VarBinary).Value = publicacao.ImagemAnexo;
                        cmd.Parameters.Add("@DataHora", SqlDbType.NVarChar).Value = publicacao.DataHora;
                        cmd.Parameters.Add("@PerfilId", SqlDbType.Int).Value = publicacao.PerfilId;

                        // Informa o tipo do comando
                        cmd.CommandType = CommandType.Text;

                        // Executa a query
                        cmd.ExecuteNonQuery();
                        publicacao.Id = id;
                    }
                }

                return Ok(publicacao);

            }
            catch (System.Exception ex)
            {
                // Retorna um erro pré definido com uma mensagem
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message
                });
            }
        }
    }
}
