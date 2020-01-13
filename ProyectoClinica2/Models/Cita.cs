using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoClinica2.Models
{
    public class Cita
    {
        public Cita()
        {
            CitaFecha = DateTime.Now;
        }
        public int CitaId { get; set; }
        public DateTime CitaFecha { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int TiposCitaId { get; set; }
        public TiposCita TiposCita { get; set; }
    }
}