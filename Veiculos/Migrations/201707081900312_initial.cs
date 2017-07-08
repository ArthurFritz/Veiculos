namespace Veiculos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssinaturaModel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        assinatura = c.String(),
                        PessoaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PessoaModel", t => t.PessoaID, cascadeDelete: true)
                .Index(t => t.PessoaID);
            
            CreateTable(
                "dbo.PessoaModel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false),
                        dataNascimento = c.DateTime(nullable: false),
                        documento = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.VeiculoModel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        marca = c.String(nullable: false),
                        modelo = c.String(nullable: false),
                        ano = c.String(nullable: false),
                        placa = c.String(nullable: false),
                        PessoaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PessoaModel", t => t.PessoaID, cascadeDelete: true)
                .Index(t => t.PessoaID);
            
            CreateTable(
                "dbo.MultasModel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tipo = c.String(nullable: false),
                        data = c.DateTime(nullable: false),
                        valor = c.Double(nullable: false),
                        pagamento = c.DateTime(nullable: false),
                        VeiculoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.VeiculoModel", t => t.VeiculoID, cascadeDelete: true)
                .Index(t => t.VeiculoID);
            
            CreateTable(
                "dbo.FotoModel",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fotoRosto = c.String(),
                        fotoPerfil = c.String(),
                        fotoCorpoInteiro = c.String(),
                        PessoaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PessoaModel", t => t.PessoaID, cascadeDelete: true)
                .Index(t => t.PessoaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FotoModel", "PessoaID", "dbo.PessoaModel");
            DropForeignKey("dbo.AssinaturaModel", "PessoaID", "dbo.PessoaModel");
            DropForeignKey("dbo.VeiculoModel", "PessoaID", "dbo.PessoaModel");
            DropForeignKey("dbo.MultasModel", "VeiculoID", "dbo.VeiculoModel");
            DropIndex("dbo.FotoModel", new[] { "PessoaID" });
            DropIndex("dbo.MultasModel", new[] { "VeiculoID" });
            DropIndex("dbo.VeiculoModel", new[] { "PessoaID" });
            DropIndex("dbo.AssinaturaModel", new[] { "PessoaID" });
            DropTable("dbo.FotoModel");
            DropTable("dbo.MultasModel");
            DropTable("dbo.VeiculoModel");
            DropTable("dbo.PessoaModel");
            DropTable("dbo.AssinaturaModel");
        }
    }
}
