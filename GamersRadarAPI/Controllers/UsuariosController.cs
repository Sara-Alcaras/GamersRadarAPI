using GamersRadarAPI.Models;
using GamersRadarAPI.Repositorie;
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
        private UsuarioRepository repositorio = new UsuarioRepository();

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
                // chama a camada de conexão com o banco que está no repositorio
                repositorio.Insert(usuario);

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
                // Cria uma variavel que recebe a conexão com o banco
                var usuarios = repositorio.GetAll();

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
        [HttpPut("{id}")]
        // O método alterar tem como parametro o id do usuário
        public IActionResult Alterar(int id, Usuario usuario)
        {
            try
            {
                // Cria uma variavel que recebe o metodo listar por id
               var buscarUsuario = repositorio.GetById(id);
                // Se buscar usuario for nulo
                if (buscarUsuario == null)
                {
                    // Retorna erro 404 - Não encontrado
                    return NotFound();
                }

                // Cria uma variavel que recebe o método de alterar
                var usuarioAlterado = repositorio.Update(id, usuario);

                // Retorna o usuário alterado com sucesso
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
