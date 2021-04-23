using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Domains
{
    public class JogoDomain
    {

        public int idJogo { get; set; }

        
        [Required(ErrorMessage = "O nome do jogo é obrigatório")]
        public string nomeJogo { get; set; }

        
        public string descricaoJogo { get; set; }

        
        public DateTime dataLancamento { get; set; }

        
        [Required(ErrorMessage = "O valor do jogo é obrigatório")]
        public double valorJogo { get; set; }

        
        public int idEstudio { get; set; }


        public EstudioDomain estudio { get; set; }


    }
}
