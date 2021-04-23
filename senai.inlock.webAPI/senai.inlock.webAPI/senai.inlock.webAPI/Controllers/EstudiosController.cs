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
    
    public class EstudiosController : ControllerBase
    {
        
        private IEstudioRepository _estudioRepository { get; set; }

        private IJogoRepository _jogoRepository { get; set; }

        public EstudiosController()  // método construtor
        {
            _estudioRepository = new EstudioRepository();
            _jogoRepository = new JogoRepository();
        }

        
        
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudios = _estudioRepository.ListarEstudios();

            return Ok(listaEstudios);
        }

        
       

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, EstudioDomain estudioAtualizado)
        {
            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorIdEstudio(id);

            if (estudioBuscado == null)
            {
                return NotFound(new { mensagem = "Estúdio não encontrado!" });
            }

            try
            {
                _estudioRepository.AtualizarIdUrlEstudio(id, estudioAtualizado);

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
            
            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorIdEstudio(id);
            // um "=" é atribuição, um "==" é uma comparação

            
            if (estudioBuscado == null)
            {
               
                return NotFound("Nenhum estúdio encontrado!");
            }

           
            return Ok(estudioBuscado);
        }

        
        
        [Authorize]
        [HttpGet("buscar/{buscado}")]
        public IActionResult GetByName(string buscado)
        {
            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorNomeEstudio(buscado);

            if (estudioBuscado == null)
            {
                return NotFound("Nenhum estúdio encontrado!");
            }
            
            return Ok(estudioBuscado);
        }

        
        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(EstudioDomain novoEstudio)
        {
            try 
            {
               
                if (String.IsNullOrWhiteSpace(novoEstudio.nomeEstudio))
                {
                   
                    return NotFound("Campo 'nome' obrigatório!");
                }

                
                _estudioRepository.CadastrarEstudio(novoEstudio);

                
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
           
            _estudioRepository.DeleteEstudio(id);

            
            return StatusCode(204);
        }


    }
}
