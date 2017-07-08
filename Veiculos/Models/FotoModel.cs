using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class FotoModel
    {
        [Key]
        public int id { get; set; }

        public String fotoRosto { get; set; }

        public String fotoPerfil { get; set; }

        public String fotoCorpoInteiro { get; set; }

        public virtual PessoaModel Pessoa { get; set; }
    }

}