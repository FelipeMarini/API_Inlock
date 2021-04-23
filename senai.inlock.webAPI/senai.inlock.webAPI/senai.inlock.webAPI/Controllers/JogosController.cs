using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webAPI.Domains;
using senai.inlock.webAPI.Interfaces;
using senai.inlock.webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Controllers
{
    
    [Produces("application/json")]
    
    [Route("api/[controller]")]
    
    [ApiController]
    
    
    public class JogosController : ControllerBase
    {
        
        private IJogoRepository _jogoRepository { get; set; }

        
        public JogosController()
        {
            _jogoRepository = new JogoRepository();
        }

        
        
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<JogoDomain> listaJogos = _jogoRepository.ListarJogos();

            return Ok(listaJogos);
        }

        
        
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, JogoDomain jogoAtualizado)
        {
            JogoDomain jogoBuscado = _jogoRepository.BuscarPorIdJogo(id);

            if (jogoBuscado == null)
            {
                return NotFound(new { mensagem = "Jogo não encontrado!" });
            }

            try
            {
                _jogoRepository.AtualizarIdUrlJogo(id, jogoAtualizado);

                return NoContent();
            }

            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        
        
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           
            JogoDomain jogoBuscado = _jogoRepository.BuscarPorIdJogo(id);
          

            if (jogoBuscado == null)
            {
                
                return NotFound("Nenhum jogo encontrado!");
            
            }

            
            return Ok(jogoBuscado);
        }

        
        
        [Authorize]
        [HttpGet("buscar/{buscado}")]
        public IActionResult GetByName(string buscado)
        {
            JogoDomain jogoBuscado = _jogoRepository.BuscarPorNomeJogo(buscado);

            if (jogoBuscado == null)
            {
                return NotFound("Nenhum jogo encontrado!");
            }
            else
                return Ok(jogoBuscado);
        }

        
        
        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(JogoDomain novoJogo)
        {
            try 
            {
                
                if (String.IsNullOrWhiteSpace(novoJogo.nomeJogo))
                {
                   
                    return NotFound("Campo 'nomeJogo' obrigatório!");
                }

                if (String.IsNullOrWhiteSpace(novoJogo.descricaoJogo))
                {
                    
                    return NotFound("Campo 'descricao' obrigatório!");
                }


                _jogoRepository.CadastrarJogo(novoJogo);

                
                return StatusCode(201);
            }

            
            
            catch (Exception codErro)
            {
               
                return BadRequest(codErro);
            }
        
        }

        
        
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            _jogoRepository.DeleteJogo(id);

          
            return StatusCode(204);
        }
    
    
    
    }
}
