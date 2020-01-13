using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoClinica2.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public string PacienteApellidos { get; set; }
        public DateTime PacienteFechaNacimiento { get; set; }
        public int PacienteTelefono { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }
}