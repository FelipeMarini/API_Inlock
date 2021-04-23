﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webAPI.Domains
{
    public class TipoUsuarioDomain
    {

        public int idTipoUsuario { get; set; }

        [Required(ErrorMessage ="O título do tipo de usuário é obrigatório")]
        public string tituloTipoUsuario { get; set; }

    }
}