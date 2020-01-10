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
using ProyectoClinica2.Filters;
using ProyectoClinica2.Models;

namespace ProyectoClinica2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PacientesController : ApiController
    {
        private ProyectoClinica2Context db = new ProyectoClinica2Context();

        // GET: api/Pacientes
        public IQueryable<Paciente> GetPacientes()
        {
            return db.Pacientes;
        }

        // GET: api/Pacientes/5
        [Route("api/Pacientes/{id:int}")]
        [PacienteCustomAuthorization("id")]
        [ResponseType(typeof(Paciente))]
        public async Task<IHttpActionResult> GetPaciente(int id)
        {
            Paciente paciente = await db.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        // PUT: api/Pacientes/5
        [Route("api/Pacientes/{id:int}")]
        [PacienteCustomAuthorization("id")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPaciente([FromUri]int id, Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paciente.PacienteId)
            {
                return BadRequest();
            }

            db.Entry(paciente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // POST: api/Pacientes
        [ResponseType(typeof(Paciente))]
        public async Task<IHttpActionResult> PostPaciente(Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pacientes.Add(paciente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = paciente.PacienteId }, paciente);
        }

        // DELETE: api/Pacientes/5
        [ResponseType(typeof(Paciente))]
        public async Task<IHttpActionResult> DeletePaciente(int id)
        {
            Paciente paciente = await db.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            db.Pacientes.Remove(paciente);
            await db.SaveChangesAsync();

            return Ok(paciente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PacienteExists(int id)
        {
            return db.Pacientes.Count(e => e.PacienteId == id) > 0;
        }
    }
}