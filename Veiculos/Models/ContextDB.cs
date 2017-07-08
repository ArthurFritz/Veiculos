using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veiculos.Models
{
    class ContextoDb : DbContext
    {
        public ContextoDb() : base("DefaultConnection"){}

        public DbSet<PessoaModel> Pessoa { get; set; }
        public DbSet<FotoModel> Foto { get; set; }
        public DbSet<AssinaturaModel> Assinatura { get; set; }
        public DbSet<MultasModel> Multa { get; set; }
        public DbSet<VeiculoModel> Veiculo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }

}
