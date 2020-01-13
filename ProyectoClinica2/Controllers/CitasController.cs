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
        private readonly ICitasRepository _citasService;

        public CitasController(ICitasRepository citasService)
        {
           this._citasService = citasService;
        }

        // GET: api/Citas
        public IQueryable<Cita> GetCitas()
        {
            return _citasService.GetCitas();
        }

        // GET: api/Citas/5
        [Authorize(Roles = "Paciente")]
        [ResponseType(typeof(Cita))]
        public async Task<IHttpActionResult> GetCita(int id)
        {
            Cita cita = await _citasService.GetCita(id);
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

            await _citasService.ActualizarCita(id, cita);          
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

            await _citasService.AsignarCita(cita);

            return CreatedAtRoute("DefaultApi", new { id = cita.CitaId }, cita);
        }

        // DELETE: api/Citas/5
        [Authorize(Roles = "Admin, Paciente")]
        [ResponseType(typeof(Cita))]
        public async Task<IHttpActionResult> DeleteCita(int id)
        {
            await _citasService.CancelarCita(id);

            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            _citasService.Dispose();
        }

    }
}