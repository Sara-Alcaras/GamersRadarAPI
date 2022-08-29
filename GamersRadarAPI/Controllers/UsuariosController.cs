using GamersRadarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]

        // Cria variavel que permite cadastrar usuario
        public IActionResult Cadastrar(Usuario usuario)
        {
            // Utiliza a biblioteca sqlConnection = System.Data.SqlClient
            // Abre a conexão com o banco de acordo com a connectionString criada
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Declara a query
                string script = "INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";

                // Cria o comando de execução do banco
                using(SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Declaração de variável por parâmetro
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;

                    // Informa o tipo do comando
                    cmd.CommandType = CommandType.Text;

                    //Executa
                    cmd.ExecuteNonQuery();
                }
            }

            // Retorna o usuario cadastrado
            return Ok(usuario);
        }

        // GET - Listar

        // PUT - Alterar

        // DELETE - Excluir
    }
}
