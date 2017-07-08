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
    public class FotoController : ApiController
    {
        private ContextoDb db = new ContextoDb();

        // GET: api/Foto
        public IEnumerable<FotoModel> GetFoto()
        {
            return db.Foto.ToList();
        }

        // GET: api/Foto/5
        [ResponseType(typeof(FotoModel))]
        public IHttpActionResult GetFotoModel(int id)
        {
            FotoModel fotoModel = db.Foto.Find(id);
            if (fotoModel == null)
            {
                return NotFound();
            }

            return Ok(fotoModel);
        }

        // PUT: api/Foto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFotoModel(int id, FotoModel fotoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fotoModel.id)
            {
                return BadRequest();
            }

            db.Entry(fotoModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoModelExists(id))
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

        // POST: api/Foto
        [ResponseType(typeof(FotoModel))]
        public IHttpActionResult PostFotoModel(FotoModel fotoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Foto.Add(fotoModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fotoModel.id }, fotoModel);
        }

        // DELETE: api/Foto/5
        [ResponseType(typeof(FotoModel))]
        public IHttpActionResult DeleteFotoModel(int id)
        {
            FotoModel fotoModel = db.Foto.Find(id);
            if (fotoModel == null)
            {
                return NotFound();
            }

            db.Foto.Remove(fotoModel);
            db.SaveChanges();

            return Ok(fotoModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FotoModelExists(int id)
        {
            return db.Foto.Count(e => e.id == id) > 0;
        }
    }
}