using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Domains
{
    public class UsuarioDomain
    {

        public int idUsuario { get; set; }

        
        [Required(ErrorMessage = "obrigatório preencher o campo de email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }


        [Required(ErrorMessage = "informe a senha")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A senha precisa ter no mínimo 3 caracteres" )]
        [DataType(DataType.Password)]
        public string senha { get; set; }


        public int idTipoUsuario { get; set; }


        public TipoUsuarioDomain tipoUsuario { get; set; }

    
    }
}
