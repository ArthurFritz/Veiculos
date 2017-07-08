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
    public class VeiculoController : ApiController
    {
        private ContextoDb db = new ContextoDb();

        // GET: api/Veiculo
        public IEnumerable<VeiculoModel> GetVeiculo()
        {
            return db.Veiculo.ToList();
        }

        // GET: api/Veiculo/5
        [ResponseType(typeof(VeiculoModel))]
        public IHttpActionResult GetVeiculoModel(int id)
        {
            VeiculoModel veiculoModel = db.Veiculo.Find(id);
            if (veiculoModel == null)
            {
                return NotFound();
            }

            return Ok(veiculoModel);
        }

        // PUT: api/Veiculo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVeiculoModel(int id, VeiculoModel veiculoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veiculoModel.id)
            {
                return BadRequest();
            }

            db.Entry(veiculoModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoModelExists(id))
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

        // POST: api/Veiculo
        [ResponseType(typeof(VeiculoModel))]
        public IHttpActionResult PostVeiculoModel(VeiculoModel veiculoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Veiculo.Add(veiculoModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VeiculoModelExists(veiculoModel.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = veiculoModel.id }, veiculoModel);
        }

        // DELETE: api/Veiculo/5
        [ResponseType(typeof(VeiculoModel))]
        public IHttpActionResult DeleteVeiculoModel(int id)
        {
            VeiculoModel veiculoModel = db.Veiculo.Find(id);
            if (veiculoModel == null)
            {
                return NotFound();
            }

            db.Veiculo.Remove(veiculoModel);
            db.SaveChanges();

            return Ok(veiculoModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VeiculoModelExists(int id)
        {
            return db.Veiculo.Count(e => e.id == id) > 0;
        }
    }
}