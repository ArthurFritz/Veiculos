namespace Veiculos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizaçãomapemaneto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VeiculoModel", "id", "dbo.PessoaModel");
            DropForeignKey("dbo.MultasModel", "veiculo_id", "dbo.VeiculoModel");
            DropIndex("dbo.VeiculoModel", new[] { "id" });
            DropPrimaryKey("dbo.VeiculoModel");
            AddColumn("dbo.VeiculoModel", "Proprietario_id", c => c.Int());
            AlterColumn("dbo.VeiculoModel", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.VeiculoModel", "id");
            CreateIndex("dbo.VeiculoModel", "Proprietario_id");
            AddForeignKey("dbo.VeiculoModel", "Proprietario_id", "dbo.PessoaModel", "id");
            AddForeignKey("dbo.MultasModel", "veiculo_id", "dbo.VeiculoModel", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MultasModel", "veiculo_id", "dbo.VeiculoModel");
            DropForeignKey("dbo.VeiculoModel", "Proprietario_id", "dbo.PessoaModel");
            DropIndex("dbo.VeiculoModel", new[] { "Proprietario_id" });
            DropPrimaryKey("dbo.VeiculoModel");
            AlterColumn("dbo.VeiculoModel", "id", c => c.Int(nullable: false));
            DropColumn("dbo.VeiculoModel", "Proprietario_id");
            AddPrimaryKey("dbo.VeiculoModel", "id");
            CreateIndex("dbo.VeiculoModel", "id");
            AddForeignKey("dbo.MultasModel", "veiculo_id", "dbo.VeiculoModel", "id");
            AddForeignKey("dbo.VeiculoModel", "id", "dbo.PessoaModel", "id");
        }
    }
}
