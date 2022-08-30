using GamersRadarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        //Cria variavel de conexão com banco de dados
        //Readonly = apenas leitura
        //String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";

        //POST - Cadastrar
        /// <summary>
        /// Cadastra uma categoria
        /// </summary>
        /// <param name = "categoria" > Dados da categoria</param>
        [HttpPost]

        // Cria variavel que permite cadastrar categoria
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar(Categoria categoria)
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

                // Retorna a categoria cadastrada
                return Ok(categoria);
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
        /// Lista todos as categorias cadastrados na aplicação
        /// </summary>
        /// <returns>Lista de categorias</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
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

                // Retorna o perfil cadastrado
                return Ok(categoria);
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
        /// Alterar os dados de uma categoria
        /// </summary>
        /// <param name="categoria">Todos as informações da categoria</param>
        /// <param name="id">Id da categoria</param>
        /// <returns>Categoria alterada</returns>
        [HttpPut("{id}")]

        //// O método alterar tem como parametro o id da categoria
        public IActionResult Alterar(int id, Categoria categoria)
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

                return Ok(categoria);

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
