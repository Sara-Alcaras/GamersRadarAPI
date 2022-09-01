using GamersRadarAPI.Models;
using GamersRadarAPI.Repository;
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
