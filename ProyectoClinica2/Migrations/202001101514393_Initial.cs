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
                        TiposCitaId = c.Int(nullable: false),
                        Paciente_PacienteId = c.Int(),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.Pacientes", t => t.Paciente_PacienteId)
                .Index(t => t.Paciente_PacienteId);
            
            CreateTable(
                "dbo.Pacientes",
                c => new
                    {
                        PacienteId = c.Int(nullable: false, identity: true),
                        PacienteNombre = c.String(),
                        PacienteApellidos = c.String(),
                        PacienteFechaNacimiento = c.DateTime(nullable: false),
                        PacienteTelefono = c.Int(nullable: false),
                        CitaId = c.Int(nullable: false),
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
            DropForeignKey("dbo.Citas", "Paciente_PacienteId", "dbo.Pacientes");
            DropIndex("dbo.Citas", new[] { "Paciente_PacienteId" });
            DropTable("dbo.TiposCitas");
            DropTable("dbo.Pacientes");
            DropTable("dbo.Citas");
        }
    }
}
