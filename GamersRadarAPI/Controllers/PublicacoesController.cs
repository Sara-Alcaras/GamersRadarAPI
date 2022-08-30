using GamersRadarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace GamersRadarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacoesController : ControllerBase
    {

        // Cria variavel de conexão com banco de dados
        // Readonly = apenas leitura 
        // String = não pode ser alterada
        readonly string connectionString = "data source=DESKTOP-EJCPJI1\\SQLEXPRESS;Integrated Security=true;Initial Catalog=GamersRadar;TrustServerCertificate=True;";

        // POST - Cadastrar
        /// <summary>
        /// Cadastra uma publicação
        /// </summary>
        /// <remarks>
        /// Exemplo de insersão do campo ImagemAnexo:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o .Net converte para VarBinary(tipo que está no banco)
        /// </remarks>
        /// <param name="publicacao">Dados da publicação</param>
        /// <returns>Dados da publicação cadastrada</returns>

        [HttpPost]

        // Cria variavel que permite cadastrar perfil
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
                    string script = "INSERT INTO Publicacoes (Descricao, ImagemAnexo, DataHora, PerfilId) VALUES (@Biografia, @ImagemAnexo, @DataHora, @PerfilId)";

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
    }
}
