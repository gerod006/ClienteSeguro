using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Insurance;
using Insurance.Models;

namespace Insurance.Controllers
{
    public class SeguroController : ApiController
    {
        private SeguroEntities db = new SeguroEntities();

        // GET: api/Seguro
        public RespuestaModelo Getseguro()
        {

            RespuestaModelo respuestaModelo = new RespuestaModelo();
            respuestaModelo.ProcesoExitoso = true;
            respuestaModelo.Respuesta.Add(db.seguro);

            return respuestaModelo;

        }

        // GET: api/Seguro/5
        [ResponseType(typeof(seguro))]
        public IHttpActionResult Getseguro(long id)
        {
            seguro seguro = db.seguro.Find(id);
            if (seguro == null)
            {
                return NotFound();
            }

            return Ok(seguro);
        }

        // PUT: api/Seguro/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putseguro(long id, seguro seguro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seguro.id)
            {
                return BadRequest();
            }

            db.Entry(seguro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!seguroExists(id))
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

        // POST: api/Seguro
        [ResponseType(typeof(seguro))]
        public IHttpActionResult Postseguro(seguro seguro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.seguro.Add(seguro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seguro.id }, seguro);
        }

        // DELETE: api/Seguro/5
        [ResponseType(typeof(seguro))]
        public IHttpActionResult Deleteseguro(long id)
        {
            seguro seguro = db.seguro.Find(id);
            if (seguro == null)
            {
                return NotFound();
            }

            db.seguro.Remove(seguro);
            db.SaveChanges();

            return Ok(seguro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool seguroExists(long id)
        {
            return db.seguro.Count(e => e.id == id) > 0;
        }
    }
}