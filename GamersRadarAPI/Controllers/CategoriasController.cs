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
    public class CategoriasController : ControllerBase
    {
        private CategoriaRepository repositorio = new CategoriaRepository();
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
                // chama a camada de conexão com o banco que está no repositorio
                repositorio.Insert(categoria);
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
                // Cria uma variavel que recebe a conexão com o banco
                var categoria = repositorio.GetAll();

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
                // Cria uma variavel que recebe o metodo listar por id
                var buscarCategoria = repositorio.GetById(id);
                // Se buscar categoria nulo
                if (buscarCategoria == null)
                {
                    // Retorna erro 404 - Não encontrado
                    return NotFound();
                }
                // Cria uma variavel que recebe o método de alterar
                var categoriaAlterada = repositorio.Update(id, categoria);
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
