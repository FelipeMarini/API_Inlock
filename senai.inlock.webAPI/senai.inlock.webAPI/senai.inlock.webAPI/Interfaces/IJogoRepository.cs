using senai.inlock.webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Interfaces
{
    public interface IJogoRepository
    {

        void CadastrarJogo(JogoDomain novoJogo);

        List<JogoDomain> ListarJogos();

        JogoDomain BuscarPorIdJogo(int id);

        void AtualizarIdUrlJogo(int id, JogoDomain jogoAtualizado);

        void DeleteJogo(int id);

        JogoDomain BuscarPorNomeJogo(string nome);


    }
}
