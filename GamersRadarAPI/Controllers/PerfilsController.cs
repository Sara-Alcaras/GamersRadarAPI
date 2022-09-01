using GamersRadarAPI.Models;
using GamersRadarAPI.Repository;
using GamersRadarAPI.Utils;
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
        private PerfilsRepository repositorio = new PerfilsRepository();
        // POST - Cadastrar
        /// <summary>
        /// Cadastra perfil na aplicação
        /// </summary>
        /// <param name="perfil">Dados do perfil</param>
        /// <returns>Dados do perfil cadastrado</returns>
        [HttpPost]

        // Cria variavel que permite cadastrar perfil
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar([FromForm] Perfil perfil, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                //Define os tipos de arquivos que podem ser colocados
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                //Define a pasta onde vai ficar salvo
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                // Se for vazio ou nulo
                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
    
                }
                perfil.Foto = uploadResultado;

                #endregion

                // Chama a camada de conexão com o banco que está no repositorio
                repositorio.Insert(perfil);

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
                // Cria uma variavel que recebe a conexão com o banco
                var perfil = repositorio.GetAll();

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
        /// <param name="perfil">Todos as informações do perfil</param>
        /// <param name="id">Id do perfil</param>
        /// <returns>Perfil alterado</returns>
        [HttpPut("{id}")]

        // O método alterar tem como parametro o id do perfil
        public IActionResult Alterar(int id, [FromForm] Perfil perfil, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                //Define os tipos de arquivos que podem ser colocados
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                //Define a pasta onde vai ficar salvo
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                // Se for vazio ou nulo
                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");

                }
                perfil.Foto = uploadResultado;

                #endregion
                // Cria uma variavel que recebe o metodo listar por id
                var buscarPerfil = repositorio.GetById(id);
                // Se buscar perfil nulo
                if (buscarPerfil == null)
                {
                    // Retorna erro 404 - Não encontrado
                    return NotFound();
                }

                // Cria uma variavel que recebe o método de alterar
                var perfilAlterado = repositorio.Update(id, perfil);
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
