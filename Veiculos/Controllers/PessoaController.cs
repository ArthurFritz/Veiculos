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
    public class PessoaController : ODataController
    {
        private ContextoDb db = new ContextoDb();

        // GET: odata/Pessoa
        [EnableQuery]
        public IQueryable<PessoaModel> GetPessoa()
        {
            return db.Pessoa;
        }

        // GET: odata/Pessoa(5)
        [EnableQuery]
        public SingleResult<PessoaModel> GetPessoaModel([FromODataUri] int key)
        {
            return SingleResult.Create(db.Pessoa.Where(pessoaModel => pessoaModel.id == key));
        }

        // PUT: odata/Pessoa(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PessoaModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PessoaModel pessoaModel = db.Pessoa.Find(key);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            patch.Put(pessoaModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pessoaModel);
        }

        // POST: odata/Pessoa
        public IHttpActionResult Post(PessoaModel pessoaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pessoa.Add(pessoaModel);
            db.SaveChanges();

            return Created(pessoaModel);
        }

        // PATCH: odata/Pessoa(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PessoaModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PessoaModel pessoaModel = db.Pessoa.Find(key);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            patch.Patch(pessoaModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pessoaModel);
        }

        // DELETE: odata/Pessoa(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PessoaModel pessoaModel = db.Pessoa.Find(key);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            db.Pessoa.Remove(pessoaModel);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaModelExists(int key)
        {
            return db.Pessoa.Count(e => e.id == key) > 0;
        }
    }
}
