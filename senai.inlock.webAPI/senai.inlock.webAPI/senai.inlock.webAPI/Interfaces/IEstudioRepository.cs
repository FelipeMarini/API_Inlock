using senai.inlock.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Interfaces
{
    public interface IEstudioRepository
    {
        
        void CadastrarEstudio(EstudioDomain novoEstudio);

        List<EstudioDomain> ListarEstudios();

        EstudioDomain BuscarPorIdEstudio(int id);

        void AtualizarIdUrlEstudio(int id, EstudioDomain estudioAtualizado);

        void DeleteEstudio(int id);

        EstudioDomain BuscarPorNomeEstudio(string nome);

    }
}
