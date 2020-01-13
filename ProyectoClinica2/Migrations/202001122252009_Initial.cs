namespace ProyectoClinica2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        CitaFecha = c.DateTime(nullable: false),
                        PacienteId = c.Int(nullable: false),
                        TiposCitaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.Pacientes", t => t.PacienteId, cascadeDelete: true)
                .ForeignKey("dbo.TiposCitas", t => t.TiposCitaId, cascadeDelete: true)
                .Index(t => t.PacienteId)
                .Index(t => t.TiposCitaId);
            
            CreateTable(
                "dbo.Pacientes",
                c => new
                    {
                        PacienteId = c.Int(nullable: false, identity: true),
                        PacienteNombre = c.String(),
                        PacienteApellidos = c.String(),
                        PacienteFechaNacimiento = c.DateTime(nullable: false),
                        PacienteTelefono = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PacienteId);
            
            CreateTable(
                "dbo.TiposCitas",
                c => new
                    {
                        TiposCitaId = c.Int(nullable: false, identity: true),
                        TiposCitaDescripcion = c.String(),
                    })
                .PrimaryKey(t => t.TiposCitaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Citas", "TiposCitaId", "dbo.TiposCitas");
            DropForeignKey("dbo.Citas", "PacienteId", "dbo.Pacientes");
            DropIndex("dbo.Citas", new[] { "TiposCitaId" });
            DropIndex("dbo.Citas", new[] { "PacienteId" });
            DropTable("dbo.TiposCitas");
            DropTable("dbo.Pacientes");
            DropTable("dbo.Citas");
        }
    }
}
