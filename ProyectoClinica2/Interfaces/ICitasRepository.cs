using ProyectoClinica2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoClinica2.Interfaces
{
    public interface ICitasRepository: IDisposable
    {
        IQueryable<Cita> GetCitas();
        Task<Cita> GetCita(int id);
        Task<bool> ActualizarCita(int id, Cita cita);
        Task<bool> CancelarCita(int id);
        Task<bool> AsignarCita(Cita cita);
    }
}
