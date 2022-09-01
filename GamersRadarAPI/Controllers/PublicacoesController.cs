﻿using GamersRadarAPI.Models;
using GamersRadarAPI.Repository;
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
    public class PublicacoesController : ControllerBase
    {
        private PublicacoesRepository repositorio = new PublicacoesRepository();

        //POST - Cadastrar
        /// <summary>
        /// Cadastra uma publicação
        /// </summary>
        /// <remarks>
        /// Exemplo de insersão do campo ImagemAnexo:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o.Net converte para VarBinary(tipo que está cadastrado no banco)
        /// </remarks>
        /// <param name = "publicacao" > Dados da publicação</param>
        [HttpPost]

        // Cria variavel que permite cadastrar publicações
        // ActionResult = Quando vários tipo de retorno são possíveis em uma ação.
        public IActionResult Cadastrar(Publicacao publicacao)
        {
            try
            {
                // Chama a camada de conexão com o banco que está no repositorio
                repositorio.Insert(publicacao);

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

        // GET - Listar
        /// <summary>
        /// Lista todos as publicações cadastrados na aplicação
        /// </summary>
        /// <returns>Lista de publicações</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                // Cria uma variavel que recebe a conexão com o banco
                var publicacao = repositorio.GetAll();

                // Retorna o publicação cadastrado
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

        // PUT - Alterar
        /// <summary>
        /// Alterar os dados de uma publicação
        /// </summary>
        /// <remarks>
        /// Exemplo de insersão do campo ImagemAnexo:
        /// "VEVTVEUgRk9UTw=="
        /// Sempre inserir em formato Base64, pois o.Net converte para VarBinary(tipo que está cadastrado no banco)
        /// </remarks>
        /// <param name="publicacao">Todos as informações da publicação</param>
        /// <param name="id">Id da publicação</param>
        /// <returns>Publicação alterada</returns>
        [HttpPut("{id}")]

        //// O método alterar tem como parametro o id da publicação
        public IActionResult Alterar(int id, Publicacao publicacao)
        {
            try
            {
                // Cria uma variavel que recebe o metodo listar por id
                var buscarPublicacao= repositorio.GetById(id);
                // Se buscar publicação for nulo
                if (buscarPublicacao == null)
                {
                    // Retorna erro 404 - Não encontrado
                    return NotFound();
                }

                // Cria uma variavel que recebe o método de alterar
                var publicacaoAlterada = repositorio.Update(id, publicacao);

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
