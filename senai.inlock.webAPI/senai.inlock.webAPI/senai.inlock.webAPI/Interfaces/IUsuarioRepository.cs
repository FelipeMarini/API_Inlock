using senai.inlock.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Interfaces
{
    public interface IUsuarioRepository
    {

        UsuarioDomain BuscarPorEmailSenhaUsuario(string email, string senha);

        
        List<UsuarioDomain> ListarUsuarios();

        
        void CadastrarUsuario(UsuarioDomain novoUsuario);


        void DeleteUsuario(int id);


        UsuarioDomain BuscarPorIdUsuario(int id);


        void AtualizarIdUrlUsuario(int id, UsuarioDomain usuarioAtualizado);

    }
}
