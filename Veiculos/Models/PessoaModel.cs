using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class PessoaModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public String nome { get; set; }

        [Required]
        public DateTime dataNascimento { get; set; }

        [Required]
        public String documento { get; set; }

        public virtual VeiculoModel Veiculo { get; set; }
    }

}