using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoClinica2.Data
{
    public class ProyectoClinica2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProyectoClinica2Context() : base("name=ProyectoClinica2Context")
        {
        }

        public System.Data.Entity.DbSet<ProyectoClinica2.Models.Paciente> Pacientes { get; set; }

        public System.Data.Entity.DbSet<ProyectoClinica2.Models.Cita> Citas { get; set; }

        public System.Data.Entity.DbSet<ProyectoClinica2.Models.TiposCita> TiposCitas { get; set; }
    }
}
