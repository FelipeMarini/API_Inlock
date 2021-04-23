using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webAPI.Domains;
using senai.inlock.webAPI.Interfaces;
using senai.inlock.webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace senai.inlock.webAPI.Controllers
{
    
    [Produces("application/json")]
    
    [Route("api/[controller]")]
    
    [ApiController]
    
    
    public class UsuariosController : ControllerBase
    {
        
        private IUsuarioRepository _usuarioRepository { get; set; }

        
        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }


        
        
        
        [HttpPost("login")]    
        public IActionResult Login(UsuarioDomain login)
        {
            
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenhaUsuario(login.email, login.senha);

            
            if (usuarioBuscado != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.tipoUsuario.tituloTipoUsuario)
                };

                var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao"));

                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "inLock.webAPI",
                    audience: "inLock.webAPI",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    }
                );
            
            }

            return NotFound("Email ou senha inválidos!");
        
        }


        
        
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            List<UsuarioDomain> listaUsuarios = _usuarioRepository.ListarUsuarios();

            return Ok(listaUsuarios);
        }

        
        
        
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, UsuarioDomain usuarioAtualizado)
        {
            
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorIdUsuario(id);

            if (usuarioBuscado == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado!" });
            }

            try
            {
                _usuarioRepository.AtualizarIdUrlUsuario(id, usuarioAtualizado);

                return NoContent();
            }

            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        
        
        
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorIdUsuario(id);
            

            if (usuarioBuscado == null)
            {
                
                return NotFound("Nenhum usuário encontrado!");
            
            }

            
            return Ok(usuarioBuscado);
        
        }


        
        
        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            
            try 
            {
                
                if (String.IsNullOrWhiteSpace(novoUsuario.email))
                {
                    
                    return NotFound("Campo email obrigatório!");
                
                }

                if (String.IsNullOrWhiteSpace(novoUsuario.senha))
                {
                    
                    return NotFound("Campo senha obrigatório!");
                }

                
                _usuarioRepository.CadastrarUsuario(novoUsuario);

                
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
            
            _usuarioRepository.DeleteUsuario(id);

            
            return StatusCode(204);
        
        }
    
    
    
    }
}