using ProyectoClinica2.Data;
using ProyectoClinica2.Interfaces;
using ProyectoClinica2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoClinica2.Repositories
{
    public class PacientesRepository : IPacientesRepository, IDisposable
    {
        private readonly ProyectoClinica2Context db = new ProyectoClinica2Context();

        public IQueryable<Paciente> GetPacientes()
        {
            return db.Pacientes.Include(p => p.Citas);
        }
        public Task<Paciente> GetPaciente(int id)
        {
            return db.Pacientes.FindAsync(id);
        }

        public async Task<bool> PostPaciente(Paciente paciente)
        {
            db.Pacientes.Add(paciente);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PutPaciente(int id, Paciente paciente)
        {
            db.Entry(paciente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeletePaciente(int id)
        {
            Paciente paciente = await db.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                throw new KeyNotFoundException();
            }

            db.Pacientes.Remove(paciente);
            await db.SaveChangesAsync();

            return true;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool PacienteExists(int id)
        {
            return db.Pacientes.Count(e => e.PacienteId == id) > 0;
        }
    }
}