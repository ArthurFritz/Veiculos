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
    public class FotoController : ODataController
    {
        private ContextoDb db = new ContextoDb();

        // GET: odata/Foto
        [EnableQuery]
        public IQueryable<FotoModel> GetFoto()
        {
            return db.Foto;
        }

        // GET: odata/Foto(5)
        [EnableQuery]
        public SingleResult<FotoModel> GetFotoModel([FromODataUri] int key)
        {
            return SingleResult.Create(db.Foto.Where(fotoModel => fotoModel.id == key));
        }

        // PUT: odata/Foto(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<FotoModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FotoModel fotoModel = db.Foto.Find(key);
            if (fotoModel == null)
            {
                return NotFound();
            }

            patch.Put(fotoModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fotoModel);
        }

        // POST: odata/Foto
        public IHttpActionResult Post(FotoModel fotoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Foto.Add(fotoModel);
            db.SaveChanges();

            return Created(fotoModel);
        }

        // PATCH: odata/Foto(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<FotoModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FotoModel fotoModel = db.Foto.Find(key);
            if (fotoModel == null)
            {
                return NotFound();
            }

            patch.Patch(fotoModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fotoModel);
        }

        // DELETE: odata/Foto(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            FotoModel fotoModel = db.Foto.Find(key);
            if (fotoModel == null)
            {
                return NotFound();
            }

            db.Foto.Remove(fotoModel);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Foto(5)/Pessoa
        [EnableQuery]
        public SingleResult<PessoaModel> GetPessoa([FromODataUri] int key)
        {
            return SingleResult.Create(db.Foto.Where(m => m.id == key).Select(m => m.Pessoa));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FotoModelExists(int key)
        {
            return db.Foto.Count(e => e.id == key) > 0;
        }
    }
}
