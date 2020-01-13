using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoClinica2.Models
{
    public class TiposCita
    {
        public int TiposCitaId { get; set; }
        public string TiposCitaDescripcion { get; set; }

        public ICollection<Cita> Citas { get; set; }
    }
}