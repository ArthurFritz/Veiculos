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
    public class PessoaController : ApiController
    {
        private ContextoDb db = new ContextoDb();

        // GET: api/Pessoa
        public IQueryable<PessoaModel> GetPessoa()
        {
            return db.Pessoa;
        }

        // GET: api/Pessoa/5
        [ResponseType(typeof(PessoaModel))]
        public IHttpActionResult GetPessoaModel(int id)
        {
            PessoaModel pessoaModel = db.Pessoa.Find(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return Ok(pessoaModel);
        }

        // PUT: api/Pessoa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPessoaModel(int id, PessoaModel pessoaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoaModel.id)
            {
                return BadRequest();
            }

            db.Entry(pessoaModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaModelExists(id))
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

        // POST: api/Pessoa
        [ResponseType(typeof(PessoaModel))]
        public IHttpActionResult PostPessoaModel(PessoaModel pessoaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pessoa.Add(pessoaModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pessoaModel.id }, pessoaModel);
        }

        // DELETE: api/Pessoa/5
        [ResponseType(typeof(PessoaModel))]
        public IHttpActionResult DeletePessoaModel(int id)
        {
            PessoaModel pessoaModel = db.Pessoa.Find(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            db.Pessoa.Remove(pessoaModel);
            db.SaveChanges();

            return Ok(pessoaModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaModelExists(int id)
        {
            return db.Pessoa.Count(e => e.id == id) > 0;
        }
    }
}