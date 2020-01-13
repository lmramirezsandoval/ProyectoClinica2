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
using ProyectoClinica2.Interfaces;
using ProyectoClinica2.Models;

namespace ProyectoClinica2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PacientesController : ApiController
    {
        private readonly IPacientesRepository _pacientesService;

        public PacientesController(IPacientesRepository pacientesService)
        {
            this._pacientesService = pacientesService;
        }

        // GET: api/Pacientes
        public IQueryable<Paciente> GetPacientes()
        {
            return _pacientesService.GetPacientes();
        }

        // GET: api/Pacientes/5
        [Route("api/Pacientes/{id:int}")]
        [ResponseType(typeof(Paciente))]
        public async Task<IHttpActionResult> GetPaciente([FromUri] int id)
        {
            Paciente paciente = await _pacientesService.GetPaciente(id);
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

            await _pacientesService.PutPaciente(id, paciente);

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

            await _pacientesService.PostPaciente(paciente);

            return CreatedAtRoute("DefaultApi", new { id = paciente.PacienteId }, paciente);
        }

        // DELETE: api/Pacientes/5
        [ResponseType(typeof(Paciente))]
        public async Task<IHttpActionResult> DeletePaciente(int id)
        {
            await _pacientesService.DeletePaciente(id);
            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            _pacientesService.Dispose();
        }

    }
}