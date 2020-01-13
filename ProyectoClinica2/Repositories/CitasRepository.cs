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

namespace ProyectoClinica2.Business
{
    public class CitasRepository : ICitasRepository, IDisposable
    {
        private readonly ProyectoClinica2Context db = new ProyectoClinica2Context();

        public IQueryable<Cita> GetCitas()
        {
            return db.Citas.Include(c => c.Paciente).Include(c => c.TiposCita);
        }

        public Task<Cita> GetCita(int id)
        {
           return db.Citas.FindAsync(id);
        }

        public async Task<bool> ActualizarCita(int id, Cita cita)
        {
            db.Entry(cita).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<bool> AsignarCita(Cita cita)
        {
            db.Pacientes.Attach(cita.Paciente);

            var citasActuales = db.Citas.Where(c => c.PacienteId == cita.PacienteId);
            var citasRepetidas = citasActuales.FirstOrDefault(c => c.CitaFecha.Day == cita.CitaFecha.Day);

            if (citasRepetidas != null)
                throw new ArgumentException("El paciente ya tiene una cita para ese mismo dia.");

            db.Citas.Add(cita);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelarCita(int id)
        {
            Cita cita = await db.Citas.FindAsync(id);
            if (cita == null)
            {
                throw new KeyNotFoundException();
            }

            DateTime hoy = DateTime.Now;
            TimeSpan diferencia = cita.CitaFecha - hoy; 

            if (diferencia.TotalHours < 24)
                throw new InvalidOperationException("El tiempo permitido para cancelar esta cita ya expiro.");

            db.Citas.Remove(cita);
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

        private bool CitaExists(int id)
        {
            return db.Citas.Count(e => e.CitaId == id) > 0;
        }
    }
}