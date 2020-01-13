using ProyectoClinica2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoClinica2.Interfaces
{
    public interface IPacientesRepository : IDisposable
    {
        IQueryable<Paciente> GetPacientes();
        Task<Paciente> GetPaciente(int id);
        Task<bool> PutPaciente(int id, Paciente paciente);
        Task<bool> PostPaciente(Paciente paciente);
        Task<bool> DeletePaciente(int id);
    }
}
