using ProyectoClinica2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoClinica2.Interfaces
{
    public interface ICitasService
    {

        Task<bool> CancelarCita(int id, Cita cita);
        Task<bool> AsignarCita(Cita cita);
    }
}
