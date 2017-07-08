using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class MultasModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public String tipo { get; set; }

        [Required]
        public DateTime data { get; set; }

        [Required]
        public Double valor { get; set; }

        [Required]
        public DateTime pagamento { get; set; }

        public virtual VeiculoModel veiculo { get; set; }
    }

}