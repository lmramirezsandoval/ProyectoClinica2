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
using ProyectoClinica2.Models;

namespace ProyectoClinica2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TiposCitasController : ApiController
    {
        private ProyectoClinica2Context db = new ProyectoClinica2Context();

        // GET: api/TiposCitas
        public IQueryable<TiposCita> GetTiposCitas()
        {
            return db.TiposCitas;
        }

        // GET: api/TiposCitas/5
        [ResponseType(typeof(TiposCita))]
        public async Task<IHttpActionResult> GetTiposCita(int id)
        {
            TiposCita tiposCita = await db.TiposCitas.FindAsync(id);
            if (tiposCita == null)
            {
                return NotFound();
            }

            return Ok(tiposCita);
        }

        // PUT: api/TiposCitas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTiposCita(int id, TiposCita tiposCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tiposCita.TiposCitaId)
            {
                return BadRequest();
            }

            db.Entry(tiposCita).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposCitaExists(id))
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

        // POST: api/TiposCitas
        [ResponseType(typeof(TiposCita))]
        public async Task<IHttpActionResult> PostTiposCita(TiposCita tiposCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TiposCitas.Add(tiposCita);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tiposCita.TiposCitaId }, tiposCita);
        }

        // DELETE: api/TiposCitas/5
        [ResponseType(typeof(TiposCita))]
        public async Task<IHttpActionResult> DeleteTiposCita(int id)
        {
            TiposCita tiposCita = await db.TiposCitas.FindAsync(id);
            if (tiposCita == null)
            {
                return NotFound();
            }

            db.TiposCitas.Remove(tiposCita);
            await db.SaveChangesAsync();

            return Ok(tiposCita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TiposCitaExists(int id)
        {
            return db.TiposCitas.Count(e => e.TiposCitaId == id) > 0;
        }
    }
}