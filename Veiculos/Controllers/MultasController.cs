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
using Veiculos.Models;

namespace Veiculos.Controllers
{
    public class MultasController : ApiController
    {
        private ContextoDb db = new ContextoDb();

        // GET: api/Multas
        public IEnumerable<MultasModel> GetMulta()
        {
            return db.Multa.ToList();
        }

        // GET: api/Multas/5
        [ResponseType(typeof(MultasModel))]
        public IHttpActionResult GetMultasModel(int id)
        {
            MultasModel multasModel = db.Multa.Find(id);
            if (multasModel == null)
            {
                return NotFound();
            }

            return Ok(multasModel);
        }

        // PUT: api/Multas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMultasModel(int id, MultasModel multasModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != multasModel.id)
            {
                return BadRequest();
            }

            db.Entry(multasModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MultasModelExists(id))
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

        // POST: api/Multas
        [ResponseType(typeof(MultasModel))]
        public IHttpActionResult PostMultasModel(MultasModel multasModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Multa.Add(multasModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = multasModel.id }, multasModel);
        }

        // DELETE: api/Multas/5
        [ResponseType(typeof(MultasModel))]
        public IHttpActionResult DeleteMultasModel(int id)
        {
            MultasModel multasModel = db.Multa.Find(id);
            if (multasModel == null)
            {
                return NotFound();
            }

            db.Multa.Remove(multasModel);
            db.SaveChanges();

            return Ok(multasModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MultasModelExists(int id)
        {
            return db.Multa.Count(e => e.id == id) > 0;
        }
    }
}