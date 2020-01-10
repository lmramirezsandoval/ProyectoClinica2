using ProyectoClinica2.Interfaces;
using ProyectoClinica2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoClinica2.Business
{
    public class CitasService : ICitasService
    {
        public Task<bool> AsignarCita(Cita cita)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelarCita(int id, Cita cita)
        {
            throw new NotImplementedException();
        }
    }
}