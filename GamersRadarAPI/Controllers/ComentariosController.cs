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
    public class ComentariosController : ControllerBase
    {

        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";

        //POST - Cadastrar
        /// <summary>
        /// Cadastra um comentario
        /// </summary>
        /// <param name = "comentario" > Dados do comentario</param>
        [HttpPost]

        // Cria variavel que permite cadastrar comentario
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar(Comentarios comentario)
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

                // Retorna o comentario cadastrado
                return Ok(comentario);
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
        /// Lista todos os comentarios cadastrados na aplicação
        /// </summary>
        /// <returns>Lista de comentarios</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
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

                // Retorna o comentario cadastrado
                return Ok(comentario);
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
        /// Alterar os dados de um comentario
        /// </summary>
        /// <param name="comentario">Todos as informações de um comentario</param>
        /// <param name="id">Id da cacomentario</param>
        /// <returns>Comentario alterada</returns>
        [HttpPut("{id}")]

        //// O método alterar tem como parametro o id da comentario
        public IActionResult Alterar(int id, Comentarios comentario)
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

                return Ok(comentario);

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

        /// <summary>
        /// Deleta todos dados de um comentario
        /// </summary>
        /// <param name="id">Id do cacomentario</param>
        /// <returns>Mensagem de exclusão</returns>

        // DELETE - Excluir
        [HttpDelete("{id}")]

        //// O método exclui com base no id passado
        public IActionResult Deletar(int id)
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
                    string script = "DELETE FROM Comentarios WHERE Id=@Id";

                    // Cria o comando de execução do banco
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        // Declaração de variável por parâmetro
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        // Informa o tipo do comando
                        cmd.CommandType = CommandType.Text;

                        // Executa a query
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(new
                {
                    msg = "Comentário exlcuído com sucesso!"
                });

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
