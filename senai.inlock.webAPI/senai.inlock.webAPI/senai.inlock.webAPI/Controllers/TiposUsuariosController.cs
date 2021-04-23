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
    
    
    public class TipoUsuariosController : ControllerBase
    {
        
        
        private ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }

        public TipoUsuariosController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        
        
        
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            List<TipoUsuarioDomain> listaTipoUsuario = _tipoUsuarioRepository.ListarTiposUsuarios();

            return Ok(listaTipoUsuario);
        }

        
        
        
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            
            TipoUsuarioDomain tipoUsuarioBuscado = _tipoUsuarioRepository.BuscarPorIdTipoUsuario(id);
            

            
            if (tipoUsuarioBuscado == null)
            {
                
                return NotFound("Nenhum tipo usuário encontrado!");
            
            }

            
            return Ok(tipoUsuarioBuscado);
        }


    
    }
}