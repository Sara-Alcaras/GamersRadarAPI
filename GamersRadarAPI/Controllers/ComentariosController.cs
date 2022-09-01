using GamersRadarAPI.Models;
using GamersRadarAPI.Repositorie;
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
        private ComentariosRepository repositorio = new ComentariosRepository();
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
                // chama a camada de conexão com o banco que está no repositorio
                repositorio.Insert(comentario);
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
                // Cria uma variavel que recebe a conexão com o banco
                var comentario = repositorio.GetAll();

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
                // Cria uma variavel que recebe o metodo listar por id
                var buscarComentario = repositorio.GetById(id);
                // Se buscar comentario nulo
                if (buscarComentario == null)
                {
                    // Retorna erro 404 - Não encontrado
                    return NotFound();
                }
                // Cria uma variavel que recebe o método de alterar
                var comentarioAlterado = repositorio.Update(id, comentario);
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
                // Cria uma variavel que recebe o metodo listar por id
                var buscarComentario = repositorio.GetById(id);
                // Se buscar comentario nulo
                if (buscarComentario == null)
                {
                    // Retorna erro 404 - Não encontrado
                    return NotFound();
                }

                // Se for falso exclui
                repositorio.Delete(id);

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
