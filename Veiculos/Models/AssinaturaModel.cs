using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class AssinaturaModel
    {
        [Key]
        public int id { get; set; }

        public String assinatura { get; set; }

        public virtual PessoaModel Pessoa { get; set; }

    }

}