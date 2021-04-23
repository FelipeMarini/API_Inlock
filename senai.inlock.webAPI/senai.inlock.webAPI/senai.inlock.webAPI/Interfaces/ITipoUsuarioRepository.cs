using senai.inlock.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Interfaces
{
    interface ITipoUsuarioRepository
    {

        List<TipoUsuarioDomain> ListarTiposUsuarios();


        TipoUsuarioDomain BuscarPorIdTipoUsuario(int id);

    }
}
