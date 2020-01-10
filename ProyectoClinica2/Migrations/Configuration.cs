namespace ProyectoClinica2.Migrations
{
    using ProyectoClinica2.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProyectoClinica2.Data.ProyectoClinica2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProyectoClinica2.Data.ProyectoClinica2Context context)
        {
            context.Pacientes.AddOrUpdate(p => p.PacienteId,
                new Paciente()
                {
                    PacienteId = 1,
                    PacienteNombre = "Fernando",
                    PacienteApellidos = "Rodriguez Salcedo",
                    PacienteFechaNacimiento = new DateTime(1975, 5, 4),
                    PacienteTelefono = Convert.ToInt32(4256836),
                    CitaId = 1
                },
                new Paciente()
                {
                    PacienteId = 2,
                    PacienteNombre = "Alonso",
                    PacienteApellidos = "Rojas Sanchez",
                    PacienteFechaNacimiento = new DateTime(1984, 9, 13),
                    PacienteTelefono = Convert.ToInt32(4103275),
                    CitaId = 2
                },
                new Paciente()
                {
                    PacienteId = 3,
                    PacienteNombre = "Paula",
                    PacienteApellidos = "Cardona Arias",
                    PacienteFechaNacimiento = new DateTime(1990, 11, 24),
                    PacienteTelefono = Convert.ToInt32(4853022),
                    CitaId = 3
                }
            );

            context.Citas.AddOrUpdate(c => c.CitaId,
                new Cita()
                {
                    CitaId = 1,
                    CitaFecha = new DateTime(2019, 06, 02),
                    TiposCitaId = 1
                },
                new Cita()
                {
                    CitaId = 2,
                    CitaFecha = new DateTime(2019, 07, 20),
                    TiposCitaId = 4
                },
                new Cita()
                {
                    CitaId = 3,
                    CitaFecha = new DateTime(2019, 07, 21),
                    TiposCitaId = 2
                }
            );

            context.TiposCitas.AddOrUpdate(t => t.TiposCitaId,
                new TiposCita()
                {
                    TiposCitaId = 1,
                    TiposCitaDescripcion = "Medicina General"
                },
                new TiposCita()
                {
                    TiposCitaId = 2,
                    TiposCitaDescripcion = "Odontologia"
                },
                new TiposCita()
                {
                    TiposCitaId = 3,
                    TiposCitaDescripcion = "Pediatria"
                },
                new TiposCita()
                {
                    TiposCitaId = 4,
                    TiposCitaDescripcion = "Neurologia"
                }
            );
        }
    }
}
