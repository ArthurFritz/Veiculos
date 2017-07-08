using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class VeiculoModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public String marca { get; set; }

        [Required]
        public String modelo { get; set; }

        [Required]
        public String ano { get; set; }

        [Required]
        public String placa { get; set; }

        public virtual PessoaModel Proprietario { get; set; }

        public IEnumerable<MultasModel> Multas { get; set; }
    }

}