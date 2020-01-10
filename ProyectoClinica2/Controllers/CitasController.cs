using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProyectoClinica2.Data;
using ProyectoClinica2.Interfaces;
using ProyectoClinica2.Models;

namespace ProyectoClinica2.Controllers
{
    [Authorize(Roles = "Admin, Agendador")]
    public class CitasController : ApiController
    {
        private ProyectoClinica2Context db = new ProyectoClinica2Context();
        private readonly ICitasService _citasService;

        public CitasController(ICitasService citasService)
        {
            _citasService = citasService;
        }

        // GET: api/Citas
        public IQueryable<Cita> GetCitas()
        {
            return db.Citas;
        }

        // GET: api/Citas/5
        [Authorize(Roles = "Paciente")]
        [ResponseType(typeof(Cita))]
        public async Task<IHttpActionResult> GetCita(int id)
        {
            Cita cita = await db.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }

        // PUT: api/Citas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCita(int id, Cita cita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cita.CitaId)
            {
                return BadRequest();
            }

            db.Entry(cita).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Citas
        [ResponseType(typeof(Cita))]
        public async Task<IHttpActionResult> PostCita(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Citas.Add(cita);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cita.CitaId }, cita);
        }

        // DELETE: api/Citas/5
        [Authorize(Roles = "Paciente")]
        [ResponseType(typeof(Cita))]
        public async Task<IHttpActionResult> DeleteCita(int id)
        {
            Cita cita = await db.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            db.Citas.Remove(cita);
            await db.SaveChangesAsync();

            return Ok(cita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CitaExists(int id)
        {
            return db.Citas.Count(e => e.CitaId == id) > 0;
        }
    }
}