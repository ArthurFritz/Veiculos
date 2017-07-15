using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Veiculos.Models;

namespace Veiculos.Controllers
{
    [Authorize]
    public class MultasController : ODataController
    {
        private ContextoDb db = new ContextoDb();

        // GET: odata/Multas
        [EnableQuery]
        public IQueryable<MultasModel> GetMultas()
        {
            return db.Multa;
        }

        // GET: odata/Multas(5)
        [EnableQuery]
        public SingleResult<MultasModel> GetMultasModel([FromODataUri] int key)
        {
            return SingleResult.Create(db.Multa.Where(multasModel => multasModel.id == key));
        }

        // PUT: odata/Multas(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<MultasModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MultasModel multasModel = db.Multa.Find(key);
            if (multasModel == null)
            {
                return NotFound();
            }

            patch.Put(multasModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MultasModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(multasModel);
        }

        // POST: odata/Multas
        public IHttpActionResult Post(MultasModel multasModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Multa.Add(multasModel);
            db.SaveChanges();

            return Created(multasModel);
        }

        // PATCH: odata/Multas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<MultasModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MultasModel multasModel = db.Multa.Find(key);
            if (multasModel == null)
            {
                return NotFound();
            }

            patch.Patch(multasModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MultasModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(multasModel);
        }

        // DELETE: odata/Multas(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            MultasModel multasModel = db.Multa.Find(key);
            if (multasModel == null)
            {
                return NotFound();
            }

            db.Multa.Remove(multasModel);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Multas(5)/Veiculo
        [EnableQuery]
        public SingleResult<VeiculoModel> GetVeiculo([FromODataUri] int key)
        {
            return SingleResult.Create(db.Multa.Where(m => m.id == key).Select(m => m.Veiculo));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MultasModelExists(int key)
        {
            return db.Multa.Count(e => e.id == key) > 0;
        }
    }
}
