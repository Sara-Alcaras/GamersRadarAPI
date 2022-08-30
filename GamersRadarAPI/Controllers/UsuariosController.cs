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
    public class UsuariosController : ControllerBase
    {
        // Cria variavel de conexão com banco de dados
        // Readonly = apenas leitura 
        // String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";

        // POST - Cadastrar
        /// <summary>
        /// Cadastra usuários na aplicação
        /// </summary>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Dados do usuário cadastrado</returns>
        [HttpPost]

        // Cria variavel que permite cadastrar usuario
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar(Usuario usuario)
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

                // Retorna o usuario cadastrado
                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                // Retorna um erro pré definido com uma mensagem
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message
                });
                ; }
        }

        // GET - Listar
        /// <summary>
        /// Lista todos os usuários cadastrados na aplicação
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
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

                // Retorna o usuario cadastrado
                return Ok(usuarios);
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
        /// Alterar os dados de um usuários
        /// </summary>
        /// <param name="usuario">Todos as informações do usuário</param>
        /// <param name="id">Id do usuário</param>
        /// <returns>Usuário alterado</returns>
        [HttpPut("/{id}")]
        // O método alterar tem como parametro o id do usuário
        public IActionResult Alterar(int id, Usuario usuario)
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

                return Ok(usuario);

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
