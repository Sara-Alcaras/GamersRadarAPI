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
    public class PerfilsController : ControllerBase
    {
        // Cria variavel de conexão com banco de dados
        // Readonly = apenas leitura 
        // String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";

        // POST - Cadastrar
        /// <summary>
        /// Cadastra perfil na aplicação
        /// </summary>
        /// <param name="perfil">Dados do perfil</param>
        /// <returns>Dados do perfil cadastrado</returns>
        /// <remarks>
        /// Exemplo de insersão do campo FOTO:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o .Net converte para VarBinary(tipo que está cadastrado no banco)
        /// </remarks>
        [HttpPost]

        // Cria variavel que permite cadastrar perfil
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar(Perfil perfil)
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

                // Retorna o usuario cadastrado
                return Ok(perfil);
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
        /// Lista todos os perfils cadastrados na aplicação
        /// </summary>
        /// <returns>Lista de perfils</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
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

                // Retorna o perfil cadastrado
                return Ok(perfil);
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
        /// Alterar os dados de um perfil
        /// </summary>
        /// <remarks>
        /// Exemplo de insersão do campo FOTO:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o .Net converte para VarBinary(tipo que está cadastrado no banco)
        /// </remarks>
        /// <param name="perfil">Todos as informações do perfil</param>
        /// <param name="id">Id do perfil</param>
        /// <returns>Perfil alterado</returns>
        [HttpPut("{id}")]

        // O método alterar tem como parametro o id do perfil
        public IActionResult Alterar(int id, Perfil perfil)
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

                return Ok(perfil);

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
