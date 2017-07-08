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
    public class AssinaturaController : ApiController
    {
        private ContextoDb db = new ContextoDb();

        // GET: api/Assinatura
        public IQueryable<AssinaturaModel> GetAssinatura()
        {
            return db.Assinatura;
        }

        // GET: api/Assinatura/5
        [ResponseType(typeof(AssinaturaModel))]
        public IHttpActionResult GetAssinaturaModel(int id)
        {
            AssinaturaModel assinaturaModel = db.Assinatura.Find(id);
            if (assinaturaModel == null)
            {
                return NotFound();
            }

            return Ok(assinaturaModel);
        }

        // PUT: api/Assinatura/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssinaturaModel(int id, AssinaturaModel assinaturaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assinaturaModel.id)
            {
                return BadRequest();
            }

            db.Entry(assinaturaModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssinaturaModelExists(id))
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

        // POST: api/Assinatura
        [ResponseType(typeof(AssinaturaModel))]
        public IHttpActionResult PostAssinaturaModel(AssinaturaModel assinaturaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assinatura.Add(assinaturaModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assinaturaModel.id }, assinaturaModel);
        }

        // DELETE: api/Assinatura/5
        [ResponseType(typeof(AssinaturaModel))]
        public IHttpActionResult DeleteAssinaturaModel(int id)
        {
            AssinaturaModel assinaturaModel = db.Assinatura.Find(id);
            if (assinaturaModel == null)
            {
                return NotFound();
            }

            db.Assinatura.Remove(assinaturaModel);
            db.SaveChanges();

            return Ok(assinaturaModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssinaturaModelExists(int id)
        {
            return db.Assinatura.Count(e => e.id == id) > 0;
        }
    }
}